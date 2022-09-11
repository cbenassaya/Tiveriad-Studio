using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XEndPointExtensions
{
    public static ClassBuilder ToBuilder(this XEndPoint xEndPoint)
    {
        var handleMethod = Code
            .CreateMethod()
            .WithName("HandleAsync")
            .MakeAsync(true)
            .WithParameters(
                xEndPoint
                    .Parameters
                    .Select(x => Code.CreateParameter(XTypeExtensions.ToBuilder(x.Type).Build(), x.Name))
                    .ToList()
            )
            .WithParameters(
                Code.CreateParameter(ComplexTypes.CANCELLATIONTOKEN, "cancellationToken"))
            .WithBody("_mediator=mediator;_mapper=mapper;")
            .WithAttributes(
                Code.CreateAttribute()
                    .WithType(ComplexTypes.Get(Enum.GetName(typeof(XHttpMethod), xEndPoint.HttpMethod) ?? "HttpGet"  ))
                    .WithAttributeArgument(
                        Code.CreateAttributeArgument().WithValue($"\"{xEndPoint.Route}\""))
            );

        if (xEndPoint.Response != null)
            handleMethod.WithReturnType(XTypeExtensions.ToBuilder(xEndPoint.Response.Type).Build());

        var classBuilder = Code.CreateClass(xEndPoint.Name);
        classBuilder
            .WithNamespace(xEndPoint.Namespace)
            .WithDependencies(
                Enumerable.ToArray<string>(xEndPoint
                        .Mappings
                        .Select(x => x.From.Namespace))
            )
            .WithDependencies(
                Enumerable.ToArray<string>(xEndPoint
                        .Mappings
                        .Select(x => x.To.Namespace))
            )
            .WithInheritedClass(ComplexTypes.CONTROLLERBASE)
            .WithFields(
                Code.CreateField(ComplexTypes.IMEDIATOR, "_mediator", AccessModifier.Private),
                Code.CreateField(ComplexTypes.IMAPPER, "_mapper", AccessModifier.Private))
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

        return classBuilder;
    }
}