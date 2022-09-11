using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.ToRefactor;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Extensions;

public static class XActionExtension
{
    public static RecordBuilder ToRequestBuilder(this XAction xAction)
    {
        var res = Code
            .CreateInternalType(ComplexTypes.IREQUEST);
        if (xAction.Response != null)
            res.WithGenericArgument(XTypeExtensions.ToBuilder(xAction.Response.Type));

        var recordBuilder = Code
            .CreateRecord($"{xAction.Name}Request")
            .WithNamespace(xAction.Namespace)
            .WithImplementedInterface(res);
        if (xAction.Parameters != null)
            recordBuilder.WithParameters(Enumerable.ToArray<ParameterBuilder>(xAction.Parameters.Select(x => XParameterExtension.ToBuilder(x))));
        return recordBuilder;
    }

    public static ClassBuilder ToActionBuilder(this XAction xAction, Record request)
    {
        var res = Code
            .CreateInternalType(ComplexTypes.IREQUESTHANDLER);
        if (xAction.Response != null)
            res.WithGenericArgument(XTypeExtensions.ToBuilder(xAction.Response.Type));

        var classBuilder = Code
            .CreateClass($"{xAction.Name}RequestHandler")
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
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.Name, xAction.Entity.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type.Name,
                                xAction.Entity.GetIds().First().Type.Namespace))
                            .Build(),
                        $"_{xAction.Entity.Name.ToCamelCase()}Repository",
                        AccessModifier.Private)
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{xAction.Entity.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.Name, xAction.Entity.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type.Name,
                                xAction.Entity.GetIds().First().Type.Namespace))
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
                            .WithGenericArgument(Code.CreateInternalType(target.Name, target.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type.Name,
                                target.GetIds().First().Type.Namespace))
                            .Build(),
                        $"_{target.Name.ToCamelCase()}Repository",
                        AccessModifier.Private)
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{target.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(target.Name, target.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type.Name,
                                target.GetIds().First().Type.Namespace))
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
            .MakeAsync(true)
            .WithParameters(
                Code.CreateParameter().WithName("request").WithType(request),
                Code.CreateParameter().WithName("cancellationToken").WithType(ComplexTypes.CANCELLATIONTOKEN)
            );
        if (xAction.Response != null)
            handleMethod.WithReturnType(XTypeExtensions.ToBuilder(xAction.Response.Type).Build());
        classBuilder.WithMethod(constructorBuilder);
        classBuilder.WithMethod(handleMethod);
        return classBuilder;
    }

    public static ClassBuilder ToValidatorBuilder(this XAction xAction, Record request)
    {
        var res = Code
            .CreateInternalType(ComplexTypes.ABSTRACTVALIDATOR)
            .WithGenericArgument(Code.CreateInternalType(request));

        var classBuilder = Code
            .CreateClass($"{xAction.Name}PreValidator")
            .WithNamespace(xAction.Namespace)
            .WithInheritedClass(res.Build());

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
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.Name, xAction.Entity.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type.Name,
                                xAction.Entity.GetIds().First().Type.Namespace))
                            .Build(),
                        $"_{xAction.Entity.Name.ToCamelCase()}Repository",
                        AccessModifier.Private)
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{xAction.Entity.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.Name, xAction.Entity.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type.Name,
                                xAction.Entity.GetIds().First().Type.Namespace))
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
                            .WithGenericArgument(Code.CreateInternalType(target.Name, target.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type.Name,
                                target.GetIds().First().Type.Namespace))
                            .Build(),
                        $"_{target.Name.ToCamelCase()}Repository",
                        AccessModifier.Private)
            );
            constructorBuilder.WithParameters(
                Code
                    .CreateParameter()
                    .WithName($"{target.Name.ToCamelCase()}Repository")
                    .WithType(
                        Code
                            .CreateInternalType(ComplexTypes.IREPOSITORY)
                            .WithGenericArgument(Code.CreateInternalType(target.Name, target.Namespace))
                            .WithGenericArgument(Code.CreateInternalType(target.GetIds().First().Type.Name,
                                target.GetIds().First().Type.Namespace))
                            .Build()
                    )
            );
            constructorCodeBuilder.Append(
                $"_{target.Name.ToCamelCase()}Repository={target.Name.ToCamelCase()}Repository;");
        }

        constructorCodeBuilder.Append(xAction.GetPreConditions(), rule => rule.GetRuleDeclaration(),
            CodeBuilder.Separator.NewLine);

        constructorBuilder.WithBody(constructorCodeBuilder.ToString());

        var handleMethod = Code
            .CreateMethod()
            .WithName("Handle")
            .MakeAsync(true)
            .WithParameters(
                Code.CreateParameter().WithName("request").WithType(request),
                Code.CreateParameter().WithName("cancellationToken").WithType(ComplexTypes.CANCELLATIONTOKEN)
            );
        if (xAction.Response != null)
            handleMethod.WithReturnType(XTypeExtensions.ToBuilder(xAction.Response.Type).Build());
        classBuilder.WithMethods(
            xAction.GetPreConditions().Where(x => x is XPredicateRule).Cast<XPredicateRule>().Select(
                x => Code.CreateMethod().WithName(x.PredicateName).WithReturnType(DataTypes.BOOL)
                    .WithBody("return true;").WithParameters(
                        Code.CreateParameter(XTypeExtensions.ToBuilder(x.RuleFor.Type).Build(), "value"))
            ).ToArray());
        classBuilder.WithMethod(constructorBuilder);
        return classBuilder;
    }
}