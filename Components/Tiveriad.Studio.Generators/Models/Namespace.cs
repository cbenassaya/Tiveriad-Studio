namespace Tiveriad.Studio.Generators.Models;

public class Namespace
{
    public Namespace(
        string? name = default,
        List<string>? usings = default,
        List<Class>? classes = default,
        List<Record>? records = default,
        List<Struct>? structs = default,
        List<Interface>? interfaces = default,
        List<Enumeration>? enums = default)
    {
        Name = name ?? string.Empty;
        Usings = usings ?? new List<string>();
        Classes = classes ?? new List<Class>();
        Records = records ?? new List<Record>();
        Structs = structs ?? new List<Struct>();
        Interfaces = interfaces ?? new List<Interface>();
        Enums = enums ?? new List<Enumeration>();
    }

    public string Name { get; }

    public List<string> Usings { get; }

    public List<Class> Classes { get; }

    public List<Record> Records { get; }

    public List<Struct> Structs { get; }

    public List<Interface> Interfaces { get; }

    public List<Enumeration> Enums { get; }

    public Namespace With(string name)
    {
        return new Namespace(name);
    }
}