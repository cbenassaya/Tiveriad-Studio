using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Attributes;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application.Middlewares;

public class InjectorMiddleware : AbstractProcessor<XElementBase, XNamedElement>,
    IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IXTypeService _typeService;

    public InjectorMiddleware(IXTypeService typeService)
    {
        _typeService = typeService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        Traverse(model.Project);
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XNamedElement;
    }

    protected override void DoApply(XElementBase elementBase)
    {
        var propertyInfos = elementBase.GetType().GetProperties()
            .Where(x => x.CanRead && x.GetIndexParameters().Length == 0).ToList();
        foreach (var propertyInfo in propertyInfos)
        {
            var attribute =
                propertyInfo.GetCustomAttributes(typeof(InjectWithAttribute), true).FirstOrDefault() as
                    InjectWithAttribute;
            if (attribute == null)
                continue;

            var target = elementBase.GetType().GetProperty(attribute.PropertyName);
            if (target == null)
                continue;

            var complexTypeReference = target.GetValue(elementBase) as string;

            if (string.IsNullOrEmpty(complexTypeReference))
                continue;

            var xComplexType = _typeService.Get(complexTypeReference);
            propertyInfo.SetValue(elementBase, xComplexType);
        }
    }
}