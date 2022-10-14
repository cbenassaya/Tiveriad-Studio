namespace Tiveriad.Studio.Generators.Models;

public class EnumerationMember
{
    public EnumerationMember(string name, int? value = default, string? summary = default)
    {
        Name = name;
        Value = value;
        Summary = summary;
    }

    public string Name { get; private set; }

    public int? Value { get; private set; }

    public string? Summary { get; private set; }

    public EnumerationMember Set(
        string? name = default,
        int? value = default,
        string? summary = default)
    {
        Name = name ?? Name;
        Summary = summary ?? Summary;
        Value = value ?? Value;
        return this;
    }


    public static EnumerationMember With(
        string name,
        int? value = default,
        string? summary = default)
    {
        return new EnumerationMember(
            name,
            value,
            summary);
    }
}