namespace Tiveriad.Studio.Generators.Models;

public class TypeParameter
{
    public TypeParameter(string name = default, List<string>? constraints = default)
    {
        Name = name;
        Constraints = constraints ?? new List<string>();
    }

    public string Name { get; private set; }

    public List<string> Constraints { get; }

    public TypeParameter Set(string name)
    {
        Name = name;
        return this;
    }

    public TypeParameter With(string name)
    {
        return new TypeParameter(name, new List<string>());
    }
}