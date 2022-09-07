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
                    .Select(x => Code.CreateParameter(InternalTypeBuilderExtensions.ToBuilder(x.Type).Build(), x.Name))
                    .ToList()
            )
            .WithParameters(
                Code.CreateParameter(NComplexTypes.CANCELLATIONTOKEN, "cancellationToken"))
            .WithBody("_mediator=mediator;_mapper=mapper;")
            .WithAttributes(
                Code.CreateAttribute()
                    .WithType(NComplexTypes.Get(Enum.GetName(typeof(XHttpMethod), xEndPoint.HttpMethod) ?? "HttpGet"  ))
                    .WithAttributeArgument(
                        Code.CreateAttributeArgument().WithValue($"\"{xEndPoint.Route}\""))
            );

        if (xEndPoint.Response != null)
            handleMethod.WithReturnType(InternalTypeBuilderExtensions.ToBuilder(xEndPoint.Response.Type).Build());

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
            .WithInheritedClass(NComplexTypes.CONTROLLERBASE)
            .WithFields(
                Code.CreateField(NComplexTypes.IMEDIATOR, "_mediator", AccessModifier.Private),
                Code.CreateField(NComplexTypes.IMAPPER, "_mapper", AccessModifier.Private))
            .WithMethod(
                Code
                    .CreateMethod()
                    .MakeConstructor(true)
                    .WithParameters(
                        Code.CreateParameter(NComplexTypes.IMAPPER, "mapper"),
                        Code.CreateParameter(NComplexTypes.IMEDIATOR, "mediator"))
                    .WithBody("_mediator=mediator;_mapper=mapper;"))
            .WithMethod(
                handleMethod
            );

        return classBuilder;
    }
}