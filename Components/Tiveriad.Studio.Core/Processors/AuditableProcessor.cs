using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Processors;

public class AuditableProcessor: AbstractProcessor<XElementBase, XNamedElement>, IProcessor
{
    private readonly XTypeLoader _typeLoader;

    public AuditableProcessor(XTypeLoader typeLoader)
    {
        _typeLoader = typeLoader;
    }

    protected override bool ApplyIf(XElementBase value)
    {
        return value is XEntity { Persistence: { IsAuditable: true } };
    }

    protected override void DoApply(XElementBase value)
    {
        var entity = value as XEntity;
        entity.Properties.Add(new XProperty()
        {
            Type = XDataTypes.STRING,
            Name = "CreatedBy",
            Constraints = new List<XConstraint> { new XRequiredConstraint()}
        });
        
        entity.Properties.Add(new XProperty()
        {
            Type = XDataTypes.DATETIME,
            Name = "Created",
            Constraints = new List<XConstraint> { new XRequiredConstraint()}
        });
        entity.Properties.Add(new XProperty()
        {
            Type = XDataTypes.STRING,
            Name = "LastModifiedBy"
        });
        
        entity.Properties.Add(new XProperty()
        {
            Type = XDataTypes.DATETIME,
            Name = "LastModified"
        });
    }
}