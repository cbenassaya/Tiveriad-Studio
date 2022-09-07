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

    public Option<string> Name { get;  private set; }

    public List<string> Usings { get;  private set; }

    public List<Class> Classes { get;  private set; }
    
    public List<Record> Records { get;  private set; }

    public List<Struct> Structs { get;  private set; }

    public List<Interface> Interfaces { get;  private set; }

    public List<Enumeration> Enums { get;  private set; }

    public Namespace With(Option<string> name = default) =>
        new Namespace(
            name: name.Else(Name),
            usings: Option.Some(Usings),
            classes: Option.Some(Classes),
            records:Option.Some(Records),
            structs: Option.Some(Structs),
            interfaces: Option.Some(Interfaces),
            enums: Option.Some(Enums));
}