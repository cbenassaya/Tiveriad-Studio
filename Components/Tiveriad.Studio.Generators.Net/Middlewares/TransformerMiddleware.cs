using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Transformers;
using Tiveriad.Studio.Generators.Services;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class TransformerMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly ISender _sender;
    private IList<InternalType> _internalTypes;

    
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> _projectTemplateService;

    public TransformerMiddleware(ISender sender,
        IProjectTemplateService<InternalType, ProjectDefinition> projectTemplateService)
    {
        _sender = sender;
        _projectTemplateService = projectTemplateService;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        ArgumentNullException.ThrowIfNull("IInternalTypeFormatter");
        _internalTypes= context.Properties.GetOrAdd("InternalTypes", ()=> new List<InternalType>()) as IList<InternalType>;
        Traverse(model.Project);
        return Task.CompletedTask;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is
            XEnum or
            XEntity or
            XEndPoint or
            XAction or
            XService or
            XContract;
    }

    protected override void DoApply(XElementBase value)
    {
        if (value is XEnum @enum)
            DoApply(@enum);

        if (value is XEntity entity)
            DoApply(entity);

        if (value is XService service)
            DoApply(service);

        if (value is XEndPoint endPoint)
            DoApply(endPoint);

        if (value is XAction action)
            DoApply(action);

        if (value is XContract contract)
            DoApply(contract);
    }


    private async void DoApply(XEnum @enum)
    {
        var builder = await _sender.Send(new EnumBuilderRequest(@enum))
                      ?? throw new NullReferenceException("EnumBuilder is null");
        builder.WithStereotype("Enum");

        var internalType = builder.Build();
        internalType.Set(
            @namespace:
            $"{@enum.GetProject().RootNamespace}.{@enum.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype)}.{@enum.GetPartialNamespace()}");
        _internalTypes.Add(internalType);
    }

    private async void DoApply(XEntity item)
    {
        var entityBuilder = await _sender.Send(new EntityBuilderRequest(item))
                            ?? throw new NullReferenceException("EntityBuilder is null");
        entityBuilder.WithStereotype("Entity");
        var entity = entityBuilder.Build();
        entity.Set(
            @namespace:
            $"{item.GetProject().RootNamespace}.{item.GetModule().Name}.{_projectTemplateService.GetLayer(entity.Stereotype)}.{item.GetPartialNamespace()}");
        _internalTypes.Add(entity);

        var persistenceBuilder = await _sender.Send(new PersistenceBuilderRequest(item))
                                 ?? throw new NullReferenceException("PersistenceBuilder is null");
        persistenceBuilder.WithStereotype("Persistence");
        var persistence = persistenceBuilder.Build();
        persistence.Set(
            @namespace:
            $"{item.GetProject().RootNamespace}.{item.GetModule().Name}.{_projectTemplateService.GetLayer(persistence.Stereotype)}.{item.GetPartialNamespace()}");
        _internalTypes.Add(persistence);
    }

    private void DoApply(XService entity)
    {
    }

    private async void DoApply(XEndPoint endPoint)
    {
        var builder = await _sender.Send(new EndPointBuilderRequest(endPoint))
                      ?? throw new NullReferenceException("EndPointBuilder is null");
        builder.WithStereotype("Endpoint");
        var internalType = builder.Build();
        internalType.Set(
            @namespace:
            $"{endPoint.GetProject().RootNamespace}.{endPoint.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype)}.{endPoint.GetPartialNamespace()}");
        _internalTypes.Add(internalType);
    }

    private async void DoApply(XAction entity)
    {
        var recordBuilder = await _sender.Send(new RequestBuilderRequest(entity))
                            ?? throw new NullReferenceException("RequestBuilder is null");
        recordBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Request");
        var record = recordBuilder.Build();
        record.Set(
            @namespace:
            $"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(record.Stereotype)}.{entity.GetPartialNamespace()}");
        _internalTypes.Add(record);

        var actionBuilder = await _sender.Send(new ActionBuilderRequest(entity, record))
                            ?? throw new NullReferenceException("ActionBuilder is null");
        actionBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Action");
        var action = actionBuilder.Build();
        action.Set(
            @namespace:
            $"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(action.Stereotype)}.{entity.GetPartialNamespace()}");
        _internalTypes.Add(action);


        var validatorBuilder = await _sender.Send(new ValidatorBuilderRequest(entity, record))
                               ?? throw new NullReferenceException("ActionBuilder is null");
        validatorBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Validator");
        var validator = validatorBuilder.Build();
        validator.Set(
            @namespace:
            $"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(validator.Stereotype)}.{entity.GetPartialNamespace()}");
        _internalTypes.Add(validator);
    }

    private async void DoApply(XContract contract)
    {
        var builder = await _sender.Send(new ContractBuilderRequest(contract))
                      ?? throw new NullReferenceException("ContractBuilder is null");

        builder.WithStereotype("Contract");
        var internalType = builder.Build();
        internalType.Set(
            @namespace:
            $"{contract.GetProject().RootNamespace}.{contract.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype)}.{contract.GetPartialNamespace()}");
        _internalTypes.Add(internalType);
    }
}