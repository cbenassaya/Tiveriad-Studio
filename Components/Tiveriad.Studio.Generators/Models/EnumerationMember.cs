using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class EnumerationMember
{
    public EnumerationMember(Option<string> name, Option<int> value = default, Option<string> summary = default)
    {
        Name = name;
        Value = value;
        Summary = summary;
    }

    public Option<string> Name { get; private set; }

    public Option<int> Value { get; private set; }

    public Option<string> Summary { get; private set; }

    public EnumerationMember Set(
        Option<string> name = default,
        Option<int> value = default,
        Option<string> summary = default)
    {
        Name = name.Else(Name);
        Summary = summary.Else(Summary);
        Value = value.Else(Value);
        return this;
    }


    public static EnumerationMember With(
        Option<string> name = default,
        Option<int> value = default,
        Option<string> summary = default)
    {
        return new(
            name,
            value,
            summary);
    }
}