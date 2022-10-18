using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Builders;
using Tiveriad.Studio.Generators.Net.Extensions;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Transformers;

public class ValidatorBuilderRequestHandler : IRequestHandler<ValidatorBuilderRequest, ClassCodeBuilder>
{
    public Task<ClassCodeBuilder> Handle(ValidatorBuilderRequest validatorBuilderRequest,
        CancellationToken cancellationToken)
    {
        var xAction = validatorBuilderRequest.Action;
        var request = validatorBuilderRequest.Request;

        var res = Code
            .CreateInternalType(ComplexTypes.ABSTRACTVALIDATOR)
            .WithGenericArgument(Code.CreateInternalType(request));

        var classBuilder = Code
            .CreateClass($"{xAction.Name}PreValidator")
            .WithReference(xAction)
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
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity))
                            .WithGenericArgument(Code.CreateInternalType(xAction.Entity.GetIds().First().Type))
                            .Build(),
                        $"_{xAction.Entity.Name.ToCamelCase()}Repository")
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
                        $"_{target.Name.ToCamelCase()}Repository")
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
            handleMethod.WithReturnType(xAction.Response.Type.ToBuilder().Build());

        classBuilder.WithMethods(
            xAction.GetPreConditions().Where(x => x is XPredicateRule).Cast<XPredicateRule>().Select(
                x => Code
                    .CreateMethod()
                    .WithName(x.PredicateName)
                    .WithReturnType(DataTypes.BOOL)
                    .WithBody("return true;")
                    .WithParameters(Code.CreateParameter(x.RuleFor.Type.ToBuilder().Build(), "value"))
            ).ToArray());

        classBuilder.WithMethod(constructorBuilder);
        return Task.FromResult(classBuilder);
    }
}