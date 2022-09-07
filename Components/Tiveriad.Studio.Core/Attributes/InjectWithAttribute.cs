namespace Tiveriad.Studio.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class InjectWithAttribute : Attribute
{
    public InjectWithAttribute(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string PropertyName { get; }
}