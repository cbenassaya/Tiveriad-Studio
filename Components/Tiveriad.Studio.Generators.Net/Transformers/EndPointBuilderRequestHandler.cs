using System.Diagnostics;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.TextTemplating;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class EndPointBuilderRequestHandler : IRequestHandler<EndPointBuilderRequest, ClassCodeBuilder>
{
    private ITemplateRenderer _templateRenderer;

    public EndPointBuilderRequestHandler(ITemplateRenderer templateRenderer)
    {
        _templateRenderer = templateRenderer;
    }

    private  InternalType ToInternalType(XBehaviourType behaviourType) => behaviourType switch
    {
        XBehaviourType.Delete => ComplexTypes.FROMROUTE,
        XBehaviourType.Query  => ComplexTypes.FROMBODY,
        XBehaviourType.GetMany  => ComplexTypes.FROMROUTE,
        XBehaviourType.GetOne  => ComplexTypes.FROMROUTE,
        XBehaviourType.SaveOrUpdate  => ComplexTypes.FROMBODY,
        _ => throw new ArgumentOutOfRangeException(nameof(behaviourType), $"Not expected internal type value: {behaviourType}"),
    };
    
    public  Task<ClassCodeBuilder> Handle(EndPointBuilderRequest request, CancellationToken cancellationToken)
    {
        var xEndPoint = request.EndPoint;
        var body =  _templateRenderer.RenderAsync($"{xEndPoint.Action.BehaviourType}EndPoint.tpl",
            new { endpoint = xEndPoint });
        var handleMethod = Code
            .CreateMethod()
            .WithName("HandleAsync")
            .MakeAsync(true)
            .WithParameters(
                xEndPoint
                    .Parameters
                    .Select(x => Code.CreateParameter(x.Type.ToBuilder().Build(), x.Name).WithAttribute(Code.CreateAttribute().WithType(ToInternalType(xEndPoint.Action.BehaviourType))))
                    .ToList()
            )
            .WithParameters(
                Code.CreateParameter(ComplexTypes.CANCELLATIONTOKEN, "cancellationToken"))
            .WithBody(body.Result )
            .WithAttributes(
                Code.CreateAttribute()
                    .WithType(ComplexTypes.Get(Enum.GetName(typeof(XHttpMethod), xEndPoint.HttpMethod) ?? "HttpGet"))
                    .WithAttributeArgument(
                        Code.CreateAttributeArgument().WithValue($"\"{xEndPoint.Route}\""))
            );

        if (xEndPoint.Response != null)
            handleMethod.WithReturnType(xEndPoint.Response.Type.ToBuilder().Build());

        var classBuilder = Code.CreateClass(xEndPoint.Name);
        classBuilder
            .WithNamespace(xEndPoint.Namespace)
            .WithDependencies(
                xEndPoint
                    .Mappings
                    .Select(x => x.From.Namespace).ToArray()
            )
            .WithDependencies(
                xEndPoint
                    .Mappings
                    .Select(x => x.To.Namespace).ToArray()
            )
            .WithInheritedClass(ComplexTypes.CONTROLLERBASE)
            .WithFields(
                Code.CreateField(ComplexTypes.IMEDIATOR, "_mediator").MakeReadonly(),
                Code.CreateField(ComplexTypes.IMAPPER, "_mapper").MakeReadonly())
            .WithMethod(
                Code
                    .CreateMethod()
                    .MakeConstructor(true)
                    .WithParameters(
                        Code.CreateParameter(ComplexTypes.IMAPPER, "mapper"),
                        Code.CreateParameter(ComplexTypes.IMEDIATOR, "mediator"))
                    .WithBody("_mediator=mediator;_mapper=mapper;"))
            .WithMethod(
                handleMethod
            );
        return Task.FromResult(classBuilder);
    }
}

