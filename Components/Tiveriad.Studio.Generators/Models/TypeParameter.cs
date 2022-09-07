using Optional;

namespace Tiveriad.Studio.Generators.Models
{
    public class TypeParameter
    {
        public TypeParameter(Option<string> name = default, Option<List<string>> constraints = default)
        {
            Name = name;
            Constraints = constraints.ValueOr(new List<string>());
        }

        public Option<string> Name { get;  private set; }

        public List<string> Constraints { get;  private set; }

        public TypeParameter Set(Option<string> name)
        {
            Name = name.Else(Name);
            return this;
        }
        public TypeParameter With(Option<string> name) =>
            new TypeParameter(
                name: name.Else(Name),
                constraints: Option.Some(Constraints));
    }
}