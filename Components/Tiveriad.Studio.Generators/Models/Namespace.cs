using Optional;

namespace Tiveriad.Studio.Generators.Models;

public class Namespace
{
    public Namespace(
        Option<string> name = default,
        Option<List<string>> usings = default,
        Option<List<Class>> classes = default,
        Option<List<Record>> records = default,
        Option<List<Struct>> structs = default,
        Option<List<Interface>> interfaces = default,
        Option<List<Enumeration>> enums = default)
    {
        Name = name;
        Usings = usings.ValueOr(new List<string>());
        Classes = classes.ValueOr(new List<Class>());
        Records = records.ValueOr(new List<Record>());
        Structs = structs.ValueOr(new List<Struct>());
        Interfaces = interfaces.ValueOr(new List<Interface>());
        Enums = enums.ValueOr(new List<Enumeration>());
    }

    public Option<string> Name { get; }

    public List<string> Usings { get; }

    public List<Class> Classes { get; }

    public List<Record> Records { get; }

    public List<Struct> Structs { get; }

    public List<Interface> Interfaces { get; }

    public List<Enumeration> Enums { get; }

    public Namespace With(Option<string> name = default)
    {
        return new(
            name.Else(Name),
            Option.Some(Usings),
            Option.Some(Classes),
            Option.Some(Records),
            Option.Some(Structs),
            Option.Some(Interfaces),
            Option.Some(Enums));
    }
}