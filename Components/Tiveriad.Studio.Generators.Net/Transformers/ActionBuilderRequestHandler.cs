using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Sources;
using Tiveriad.TextTemplating;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class ActionBuilderRequestHandler : IRequestHandler<ActionBuilderRequest, ClassCodeBuilder>
{
    private readonly ITemplateRenderer _templateRenderer;

    public ActionBuilderRequestHandler(ITemplateRenderer templateRenderer)
    {
        _templateRenderer = templateRenderer;
    }

    public Task<ClassCodeBuilder> Handle(ActionBuilderRequest actionBuilderRequest,
        CancellationToken cancellationToken)
    {
        var xAction = actionBuilderRequest.Action;
        var request = actionBuilderRequest.Request;

        var body = _templateRenderer.RenderAsync($"{xAction.BehaviourType}Action.tpl",
            new { action = xAction, request });

        var res = Code
            .CreateInternalType(ComplexTypes.IREQUESTHANDLER).WithGenericArgument(Code.CreateInternalType(request));
        if (xAction.Response != null)
            res.WithGenericArgument(xAction.Response.Type.ToBuilder(xAction.BehaviourType));

        var classBuilder = Code
            .CreateClass($"{xAction.Name}RequestHandler")
            .WithReference(xAction)
            .WithNamespace(xAction.Namespace)
            .WithImplementedInterface(res);

        var constructorBuilder = Code
            .CreateMethod()
            .MakeConstructor(true);
        var constructorCodeBuilder = CodeBuilder.Instance();

        if (xAction.Entity.GetIds().Count == 1)
        {
            classBuilder.WithFields(
                Code
                    .CreateField(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type))
                            .Build(),
                        $"_{xAction.Entity.Name.ToCamelCase()}Repository").MakeReadonly()
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{xAction.Entity.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type))
                            .Build()
                    )
            );

            constructorCodeBuilder.Append(
                $"_{xAction.Entity.Name.ToCamelCase()}Repository={xAction.Entity.Name.ToCamelCase()}Repository;");
        }

        foreach (var xRelationShip in xAction.Entity.RelationShips)
        {
            var target = xRelationShip.Type as XEntity;
            if (target is not { IsBusinessEntity: true }) continue;
            if (xAction.Entity.Name.Equals(target.Name)) continue;
            if (!target.GetIds().Any()) continue;

            classBuilder.WithFields(
                Code
                    .CreateField(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(target))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type))
                            .Build(),
                        $"_{target.Name.ToCamelCase()}Repository").MakeReadonly()
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{target.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(target))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type))
                            .Build()
                    )
            );
            constructorCodeBuilder.Append(
                $"_{target.Name.ToCamelCase()}Repository={target.Name.ToCamelCase()}Repository;");
        }

        constructorBuilder.WithBody(constructorCodeBuilder.ToString());

        var handleMethod = Code
            .CreateMethod()
            .WithName("Handle")
            .MakeAsync(false)
            .WithBody(body.Result)
            .WithParameters(
                Code.CreateParameter().WithName("request").WithType(request),
                Code.CreateParameter().WithName("cancellationToken").WithType(ComplexTypes.CANCELLATIONTOKEN)
            );
        if (xAction.Response != null)
        {
            handleMethod.WithReturnType(Code.CreateInternalType(ComplexTypes.TASK).WithGenericArgument(xAction.Response.Type.ToBuilder(xAction.BehaviourType)).Build());

        }
        classBuilder.WithMethod(constructorBuilder);
        classBuilder.WithMethod(handleMethod);
        classBuilder.WithDependencies("Microsoft.EntityFrameworkCore");
        return Task.FromResult(classBuilder);
    }
}