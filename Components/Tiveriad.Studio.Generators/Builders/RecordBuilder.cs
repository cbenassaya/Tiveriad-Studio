using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building class structures. <see cref="RecordBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class RecordBuilder  : AbstractBuilder
    {
        private readonly List<FieldBuilder> _fields = new ();
        private readonly List<PropertyBuilder> _properties = new ();
        private readonly List<MethodBuilder> _constructors = new ();
        private readonly List<TypeParameterBuilder> _typeParameters = new ();
        private readonly List<InternalTypeBuilder> _implementedInterfaces = new ();
        private readonly List<ParameterBuilder> _parameters = new ();

        internal RecordBuilder()
        {
        }

        internal Record Record { get; private set; } = new Record(AccessModifier.Public);

        /// <summary>
        /// Sets the access modifier of the class being built.
        /// </summary>
        public RecordBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Record.Set(accessModifier: Option.Some(accessModifier));
            return this;
        }

        /// <summary>
        /// Sets the name of the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public RecordBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The class name must be a valid, non-empty string.", nameof(name));

            Record.Set(name: Option.Some(name));
            return this;
        }
        
        /// <summary>
        /// Sets the namespace of the record being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="namespace"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="namespace"/> is empty or invalid.
        /// </exception>
        public RecordBuilder WithNamespace(string @namespace)
        {
            if (@namespace is null)
                throw new ArgumentNullException(nameof(@namespace));

            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

            Record.Set(@namespace: Option.Some(@namespace));
            return this;
        }

        /// <summary>
        /// Sets the class that the class being built inherits from.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="class"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="class"/> is empty or invalid.
        /// </exception>
        public RecordBuilder WithInheritedRecord(Class @class)
        {
            if (@class is null)
                throw new ArgumentNullException(nameof(@class));

            if (@class ==null)
                throw new ArgumentException("The implemented class name must be a valid, not null.", nameof(@class));

            Record.Set(inheritedClass: Option.Some(@class));
            return this;
        }

        /// <summary>
        /// Adds an interface to the list of interfaces that the class being built implements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>

        public RecordBuilder WithImplementedInterface(InternalTypeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _implementedInterfaces.Add( builder);
            
            return this;
        }
        
        /// <summary>
        /// Adds a bunch of implemented interfaces to the record being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithImplementedInterfaces(params InternalTypeBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _implementedInterfaces.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of implemented interfaces to the record being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithImplementedInterfaces(IEnumerable<InternalTypeBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the internal type builders is null.");

            _implementedInterfaces.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the class.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Record.Set(summary: Option.Some(summary));
            return this;
        }

        /// <summary>
        /// Adds a type parameter to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithTypeParameter(TypeParameterBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _typeParameters.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithTypeParameters(params TypeParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithTypeParameters(IEnumerable<TypeParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a parameter to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithParameter(ParameterBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _parameters.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of parameters to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithParameters(params ParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the parameter builders is null.");

            _parameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of parameters to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithParameters(IEnumerable<ParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the parameter builders is null.");

            _parameters.AddRange(builders);
            return this;
        }
        
        
        
        /// <summary>
        /// Adds a field to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithField(FieldBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _fields.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of fields to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithFields(params FieldBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the field builders is null.");

            _fields.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a property to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithProperty(PropertyBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _properties.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of properties to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithProperties(params PropertyBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the property builders is null.");

            _properties.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of properties to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithProperties(IEnumerable<PropertyBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the property builders is null.");

            _properties.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a constructor to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public RecordBuilder WithConstructor(MethodBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _constructors.Add(builder);
            return this;
        }

        /// <summary>
        /// Sets whether the class being built should be static or not.
        /// </summary>
        /// <param name="makeStatic">
        /// Indicates whether the class should be static or not.
        /// </param>
        public RecordBuilder MakeStatic(bool makeStatic = true)
        {
            Record.Set(isStatic: Option.Some(makeStatic));
            return this;
        }

        /// <summary>
        /// Checks whether the described member exists in the class structure.
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
                // Lookup all possible members
                return HasMember(name, MemberType.Field, accessModifier, comparison) ||
                       HasMember(name, MemberType.Property, accessModifier, comparison);
            }

            return false;
        }


        public Record Build()
        {
            if (string.IsNullOrWhiteSpace(Record.Name.ValueOrDefault()))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the class is required when building a class.");
            }
            else if (Record.IsStatic && _constructors.Count > 1)
            {
                throw new SyntaxException("Static classes can have only 1 constructor.");
            }

            Record.Fields=_fields.Select(builder => builder.Build()).ToList();
            Record.Properties=_properties.Select(builder => builder.Build()).ToList();
            Record.TypeParameters=_typeParameters.Select(builder => builder.Build()).ToList();
            Record.ImplementedInterfaces=_implementedInterfaces.Select(builder => builder.Build()).ToList();
            Record.Parameters=_parameters.Select(builder => builder.Build()).ToList();
            
            return Record;
        }
    }
}