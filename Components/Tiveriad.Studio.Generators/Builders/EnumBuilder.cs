using Optional;
using Optional.Collections;
using Optional.Unsafe;
using Tiveriad.Studio.Core.ToRefactor;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionalty for building enum structures. <see cref="EnumBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class EnumBuilder  : AbstractBuilder
    {
        private readonly List<EnumMemberBuilder> _members = new List<EnumMemberBuilder>();

        internal EnumBuilder()
        {
        }

        private Enumeration Enumeration { get; set; } = new Enumeration(AccessModifier.Public);

        /// <summary>
        /// Set the stereotype
        /// </summary>
        public EnumBuilder WithStereotype(string value)
        {
            Stereotype = value;
            return this;
        }
        
        /// <summary>
        /// Sets the access modifier of the enum being built.
        /// </summary>
        public EnumBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Enumeration = Enumeration.Set(accessModifier: Option.Some(accessModifier));
            return this;
        }

        /// <summary>
        /// Sets the name of the enum being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public EnumBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The enum name must be a valid, non-empty string.", nameof(name));

            Enumeration = Enumeration.Set(name: Option.Some(name));
            return this;
        }
        
        /// <summary>
        /// Sets the namespace of the enumeration being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="namespace"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="namespace"/> is empty or invalid.
        /// </exception>
        public EnumBuilder WithNamespace(string @namespace)
        {
            if (@namespace is null)
                throw new ArgumentNullException(nameof(@namespace));

            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

            Enumeration.Set(@namespace: Option.Some(@namespace));
            return this;
        }

        /// <summary>
        /// Adds a member to the enum being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public EnumBuilder WithMember(EnumMemberBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _members.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of members to the enum being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public EnumBuilder WithMembers(params EnumMemberBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _members.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of members to the enum being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public EnumBuilder WithMembers(IEnumerable<EnumMemberBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _members.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Specifies whether the enum being built represents a set of flags or not. A flags enum will be marked with
        /// the <see cref="FlagsAttribute"/> in the generated source code. The members of a flags enum will be assigned
        /// appropriate, auto-generated values. <b>Note</b> that explicitly set values will be overwritten.
        /// </summary>
        /// <param name="makeFlagsEnum">
        /// Indicates whether the enum represents a set of flags.
        /// </param>
        public EnumBuilder MakeFlagsEnum(bool makeFlagsEnum = true)
        {
            Enumeration = Enumeration.Set(isFlag: Option.Some(makeFlagsEnum));
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the enum.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public EnumBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Enumeration = Enumeration.Set(summary: Option.Some(summary));
            return this;
        }

        /// <summary>
        /// Checks whether the described member exists in the enum structure.
        /// </summary>
        /// <param name="name">
        /// The name of the member.
        /// </param>
        /// <param name="comparison">
        /// The comparision type to be performed when comparing the described name against the names of the actual
        /// members. By default casing is ignored.
        /// </param>
        public bool HasMember(
            string name,
            StringComparison comparison = StringComparison.InvariantCultureIgnoreCase) =>
            _members.Any(x => x.EnumerationMember.Name.Exists(n => n.Equals(name, comparison)));

        /// <summary>
        /// Returns the source code of the built enum.
        /// </summary>
        /// <exception cref="MissingBuilderSettingException">
        /// A setting that is required to build a valid class structure is missing.
        /// </exception>
        /// <exception cref="SyntaxException">
        /// The class builder is configured in such a way that the resulting code would be invalid.
        /// </exception>
        public string ToSourceCode() =>
            string.Empty;

        /// <summary>
        /// Returns the source code of the built enum.
        /// </summary>
        /// <exception cref="MissingBuilderSettingException">
        /// A setting that is required to build a valid class structure is missing.
        /// </exception>
        /// <exception cref="SyntaxException">
        /// The class builder is configured in such a way that the resulting code would be invalid.
        /// </exception>
        public override string ToString() =>
            ToSourceCode();

        public Enumeration Build()
        {
            if (string.IsNullOrWhiteSpace(Enumeration.Name.ValueOr(string.Empty)))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the enum is required when building an enum.");
            }

            if (Enumeration.IsFlag && _members.All(x => !x.EnumerationMember.Value.HasValue))
            {
                for (var i = 0; i < _members.Count; i++)
                {
                    _members[i] = _members[i].WithValue(i == 0 ? 0 : (int)Math.Pow(2, i - 1));
                }
            }

            Enumeration.Members.AddRange(_members.Select(x => x.Build()));
            Enumeration.Members
                .GroupBy(x => x.Name.ValueOrFailure())
                .Where(x => x.AtLeast(2))
                .Select(x => x.Key)
                .FirstOrNone()
                .MatchSome(duplicateMemberName => throw new SyntaxException(
                    $"The enum '{Enumeration.Name}' already contains a definition for '{duplicateMemberName}'."));

            return Enumeration;
        }
    }
}
