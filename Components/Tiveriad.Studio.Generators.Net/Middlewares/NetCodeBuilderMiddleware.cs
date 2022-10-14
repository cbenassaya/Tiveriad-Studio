using Optional;
using Optional.Unsafe;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Net.Transformers;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;


public class NetCodeBuilderMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IList<SourceItem> _sourceItems = new List<SourceItem>();
    
    private IProjectTemplateService<InternalType,ProjectDefinition> _projectTemplateService;

    private readonly ISender _sender;

    public NetCodeBuilderMiddleware(ISender sender, IProjectTemplateService<InternalType, ProjectDefinition> projectTemplateService)
    {
        _sender = sender;
        _projectTemplateService = projectTemplateService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        ArgumentNullException.ThrowIfNull("IInternalTypeFormatter");
        Traverse(model.Project);
        context.Properties.SourceItems = _sourceItems;
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
        internalType.Set(reference: Option.Some( @enum as XType));
        internalType.Set(@namespace: Option.Some($"{@enum.GetProject().RootNamespace}.{@enum.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype.ValueOrFailure())}.{@enum.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(internalType);
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }

    private async void DoApply(XEntity item)
    {
        var entityBuilder = await _sender.Send(new EntityBuilderRequest(item))
                            ?? throw new NullReferenceException("EntityBuilder is null");
        entityBuilder.WithStereotype("Entity");
        var entity = entityBuilder.Build();
        entity.Set(reference: Option.Some(item as XType));
        entity.Set(@namespace: Option.Some($"{item.GetProject().RootNamespace}.{item.GetModule().Name}.{_projectTemplateService.GetLayer(entity.Stereotype.ValueOrFailure())}.{item.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(entity);
        _sourceItems.Add(new SourceItem(entity, entity.ToSourceCode()));
        
        var persistenceBuilder = await _sender.Send(new PersistenceBuilderRequest(item))
                                 ?? throw new NullReferenceException("PersistenceBuilder is null");
        persistenceBuilder.WithStereotype("Persistence");
        var persistence = persistenceBuilder.Build();
        persistence.Set(reference: Option.Some(item as XType));
        persistence.Set(@namespace: Option.Some($"{item.GetProject().RootNamespace}.{item.GetModule().Name}.{_projectTemplateService.GetLayer(persistence.Stereotype.ValueOrFailure())}.{item.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(persistence);
        _sourceItems.Add(new SourceItem(persistence, persistence.ToSourceCode()));
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
        internalType.Set(reference: Option.Some(endPoint as XType));
        internalType.Set(@namespace: Option.Some($"{endPoint.GetProject().RootNamespace}.{endPoint.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype.ValueOrFailure())}.{endPoint.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(internalType);
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }

    private async void DoApply(XAction entity)
    {
        var recordBuilder = await _sender.Send(new RequestBuilderRequest(entity))
            ?? throw new NullReferenceException("RequestBuilder is null");
        recordBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Request");
        var record = recordBuilder.Build();
        record.Set(reference: Option.Some(entity as XType));
        record.Set(@namespace: Option.Some($"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(record.Stereotype.ValueOrFailure())}.{entity.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(record);
        _sourceItems.Add(new SourceItem(record, record.ToSourceCode()));
        
        var actionBuilder = await _sender.Send(new ActionBuilderRequest(entity,record))
                            ?? throw new NullReferenceException("ActionBuilder is null");
        actionBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Action");
        var action = actionBuilder.Build();
        action.Set(reference: Option.Some(entity as XType));
        action.Set(@namespace: Option.Some($"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(action.Stereotype.ValueOrFailure())}.{entity.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(action);
        _sourceItems.Add(new SourceItem(action, action.ToSourceCode()));
        
        
        var validatorBuilder = await _sender.Send(new ValidatorBuilderRequest(entity,record))
                            ?? throw new NullReferenceException("ActionBuilder is null");
        validatorBuilder.WithStereotype($"{entity.BehaviourType.ToCqrs()}Validator");
        var validator = validatorBuilder.Build();
        validator.Set(reference: Option.Some(entity as XType));
        validator.Set(@namespace: Option.Some($"{entity.GetProject().RootNamespace}.{entity.GetModule().Name}.{_projectTemplateService.GetLayer(validator.Stereotype.ValueOrFailure())}.{entity.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(validator);
        _sourceItems.Add(new SourceItem(validator, validator.ToSourceCode()));
    }

    private async void DoApply(XContract contract)
    {
        var builder = await _sender.Send(new ContractBuilderRequest(contract))
                      ?? throw new NullReferenceException("ContractBuilder is null");
        
        builder.WithStereotype("Contract");
        var internalType = builder.Build();
        internalType.Set(reference: Option.Some(contract as XType));
        internalType.Set(@namespace: Option.Some($"{contract.GetProject().RootNamespace}.{contract.GetModule().Name}.{_projectTemplateService.GetLayer(internalType.Stereotype.ValueOrFailure())}.{contract.GetPartialNamespace()}"));
        NamespaceProcessor.UpdateDependencies(internalType);
        _sourceItems.Add(new SourceItem(internalType, internalType.ToSourceCode()));
    }
}