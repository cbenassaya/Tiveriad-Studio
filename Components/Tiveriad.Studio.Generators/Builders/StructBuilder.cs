using Optional;
using Optional.Collections;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building structs. <see cref="StructBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class StructBuilder  : AbstractBuilder
    {
        private readonly List<MethodBuilder> _constructors = new List<MethodBuilder>();
        private readonly List<FieldBuilder> _fields = new List<FieldBuilder>();
        private readonly List<PropertyBuilder> _properties = new List<PropertyBuilder>();
        private readonly List<TypeParameterBuilder> _typeParameters = new List<TypeParameterBuilder>();

        internal StructBuilder()
        {
        }

        private Struct Struct { get; set; } = new Struct(AccessModifier.Public);

        /// <summary>
        /// Set the stereotype
        /// </summary>
        public StructBuilder WithStereotype(string value)
        {
            Stereotype = value;
            return this;
        }
        
        /// <summary>
        /// Sets the access modifier of the struct.
        /// </summary>
        /// <param name="accessModifier">
        /// The access modifier of the struct.
        /// </param>
        public StructBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Struct.Set(accessModifier: Option.Some(accessModifier));
            return this;
        }

        /// <summary>
        /// Sets the name of the struct.
        /// </summary>
        /// <param name="name">
        /// The name of the struct.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public StructBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Struct name must be a valid, non-empty string.", nameof(name));

            Struct.Set(name: Option.Some(name));
            return this;
        }
        
        /// <summary>
        /// Sets the namespace of the struct being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="namespace"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="namespace"/> is empty or invalid.
        /// </exception>
        public StructBuilder WithNamespace(string @namespace)
        {
            if (@namespace is null)
                throw new ArgumentNullException(nameof(@namespace));

            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

            Struct.Set(@namespace: Option.Some(@namespace));
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the struct.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Struct.Set(summary: Option.Some(summary));
            return this;
        }

        /// <summary>
        /// Adds a constructor to the struct.
        /// </summary>
        /// <param name="builder">
        /// The configuration of the constructor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithConstructor(MethodBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _constructors.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a field to the struct.
        /// </summary>
        /// <param name="builder">
        /// The configuration of the field.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithField(FieldBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _fields.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of fields to the struct.
        /// </summary>
        /// <param name="builders">
        /// A collection of fields.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithFields(IEnumerable<FieldBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _fields.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of fields to the struct.
        /// </summary>
        /// <param name="builders">
        /// A collection of fields.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithFields(params FieldBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _fields.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a property to the struct.
        /// </summary>
        /// <param name="builder">
        /// The configuration of the property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithProperty(PropertyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _properties.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of properties to the struct.
        /// </summary>
        /// <param name="builders">
        /// A collection of properties.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithProperties(IEnumerable<PropertyBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _properties.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of properties to the struct.
        /// </summary>
        /// <param name="builders">
        /// A collection of properties.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithProperties(params PropertyBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the builders is null.");

            _properties.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds an interface to the list of interfaces that the struct implements.
        /// </summary>
        /// <param name="interface">
        /// The name of the interface that the struct implements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="interface"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="interface"/> is empty or invalid.
        /// </exception>
        public StructBuilder WithImplementedInterface(Interface @interface)
        {
            if (@interface is null)
                throw new ArgumentNullException(nameof(@interface));

            if (@interface == null)
                throw new ArgumentException("Implemented interface name must be a valid, not null.", nameof(@interface));

            Struct.ImplementedInterfaces.Add(@interface);
            return this;
        }

        /// <summary>
        /// Adds a bunch of interfaces to the list of interfaces that the struct implements.
        /// </summary>
        /// <param name="interfaces">
        /// A collection with the names of interfaces that the struct implements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="interfaces"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="interfaces"/> is <c>null</c> or invalid.
        /// </exception>
        public StructBuilder WithImplementedInterfaces(IEnumerable<Interface> interfaces)
        {
            if (interfaces is null)
                throw new ArgumentNullException(nameof(interfaces));

            if (interfaces.Any(x => x==null))
                throw new ArgumentException("One of the interfaces is null or invalid.");

            Struct.ImplementedInterfaces.AddRange(interfaces);
            return this;
        }

        /// <summary>
        /// Adds a bunch of interfaces to the list of interfaces that the struct implements.
        /// </summary>
        /// <param name="interfaces">
        /// A collection with the names of interfaces that the struct implements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="interfaces"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="interfaces"/> is <c>null</c> or invalid.
        /// </exception>
        public StructBuilder WithImplementedInterfaces(params Interface[] interfaces)
        {
            if (interfaces is null)
                throw new ArgumentNullException(nameof(interfaces));

            if (interfaces.Any(x => x==null))
                throw new ArgumentException("One of the interfaces is null or invalid.");

            Struct.ImplementedInterfaces.AddRange(interfaces);
            return this;
        }

        /// <summary>
        /// Adds a type parameter to the struct being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithTypeParameter(TypeParameterBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _typeParameters.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the struct being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithTypeParameters(params TypeParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the struct being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public StructBuilder WithTypeParameters(IEnumerable<TypeParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Checks whether the described member exists in the struct structure.
        /// </summary>
        /// <param name="name">
        /// The name of the member.
        /// </param>
        /// <param name="memberType">
        /// The type of the member. By default all members will be taken into account.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the member. By default all access modifiers will be taken into account.
        /// </param>
        /// <param name="comparison">
        /// The comparision type to be performed when comparing the described name against the names of the actual
        /// members. By default casing is ignored.
        /// </param>
        public bool HasMember(
            string name,
            MemberType? memberType = default,
            AccessModifier? accessModifier = default,
            StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
        {
            if (memberType == MemberType.Field)
            {
                return _fields
                    .Where(x => !accessModifier.HasValue || x.Field.AccessModifier == accessModifier)
                    .Any(x => x.Field.Name.Exists(n => n.Equals(name, comparison)));
            }

            if (memberType == MemberType.Property)
            {
                return _properties
                    .Where(x => !accessModifier.HasValue || x.Property.AccessModifier == accessModifier)
                    .Any(x => x.Property.Name.Exists(n => n.Equals(name, comparison)));
            }

            if (!memberType.HasValue)
            {
                return HasMember(name, MemberType.Field, accessModifier, comparison) ||
                    HasMember(name, MemberType.Property, accessModifier, comparison);
            }

            return false;
        }

        /// <summary>
        /// Returns the source code of the built struct.
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
        /// Returns the source code of the built struct.
        /// </summary>
        /// <exception cref="MissingBuilderSettingException">
        /// A setting that is required to build a valid class structure is missing.
        /// </exception>
        /// <exception cref="SyntaxException">
        /// The class builder is configured in such a way that the resulting code would be invalid.
        /// </exception>
        public override string ToString() =>
            ToSourceCode();

        internal Struct Build()
        {
            var structName = Struct.Name.ValueOr(string.Empty);
            if (string.IsNullOrWhiteSpace(structName))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the struct is required when building a struct.");
            }

            Struct.Fields.AddRange(_fields.Select(field => field.Build()));
            Struct.Properties.AddRange(_properties.Select(prop => prop.Build()));
            Struct.Properties
                .FirstOrNone(x => x.DefaultValue.HasValue)
                .MatchSome(prop => throw new SyntaxException(
                    "Default property values are not allowed in structs. (CS0573)"));

            Struct.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));

            return Struct;
        }
  }
}
