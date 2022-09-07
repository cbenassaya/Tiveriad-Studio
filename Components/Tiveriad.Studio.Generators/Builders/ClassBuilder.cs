using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building class structures. <see cref="ClassBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class ClassBuilder  : AbstractBuilder
    {
        private readonly List<FieldBuilder> _fields = new ();
        private readonly List<PropertyBuilder> _properties = new ();
        private readonly List<MethodBuilder> _methods = new ();
        private readonly List<TypeParameterBuilder> _typeParameters = new ();
        private readonly List<InternalTypeBuilder> _implementedInterfaces = new ();
        private readonly List<AttributeBuilder> _attributeBuilders = new List<AttributeBuilder>();

        internal ClassBuilder()
        {
        }

        internal Class Class { get; private set; } = new Class(AccessModifier.Public);

        /// <summary>
        /// Sets the access modifier of the class being built.
        /// </summary>
        public ClassBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Class.Set(accessModifier: Option.Some(accessModifier));
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
        public ClassBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The class name must be a valid, non-empty string.", nameof(name));

            Class.Set(name: Option.Some(name));
            return this;
        }
        
        /// <summary>
        /// Sets the namespace of the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="namespace"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="namespace"/> is empty or invalid.
        /// </exception>
        public ClassBuilder WithNamespace(string @namespace)
        {
            if (@namespace is null)
                throw new ArgumentNullException(nameof(@namespace));

            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

            Class.Set(@namespace: Option.Some(@namespace));
            return this;
        }

        /// <summary>
        /// Sets the class that the class being built inherits from.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="class"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithInheritedClass(InternalType @class)
        {
            if (@class is null)
                throw new ArgumentNullException(nameof(@class));

       
            Class.Set(inheritedClass: Option.Some(@class));
            return this;
        }

        /// <summary>
        /// Adds an interface to the list of interfaces that the class being built implements.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>

        public ClassBuilder WithImplementedInterface(InternalTypeBuilder builder)
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
        public ClassBuilder WithImplementedInterfaces(params InternalTypeBuilder[] builders)
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
        public ClassBuilder WithImplementedInterfaces(IEnumerable<InternalTypeBuilder> builders)
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
        public ClassBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Class = Class.Set(summary: Option.Some(summary));
            return this;
        }

        /// <summary>
        /// Adds a type parameter to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithTypeParameter(TypeParameterBuilder builder)
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
        public ClassBuilder WithTypeParameters(params TypeParameterBuilder[] builders)
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
        public ClassBuilder WithTypeParameters(IEnumerable<TypeParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }
        
        public ClassBuilder WithAttribute(AttributeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _attributeBuilders.Add(builder);
            return this;
        }
        
        public ClassBuilder WithAttributes(params AttributeBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));
            
            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the attribute builders is null.");

            _attributeBuilders.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a field to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithField(FieldBuilder builder)
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
        public ClassBuilder WithFields(params FieldBuilder[] builders)
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
        public ClassBuilder WithProperty(PropertyBuilder builder)
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
        public ClassBuilder WithProperties(params PropertyBuilder[] builders)
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
        public ClassBuilder WithProperties(IEnumerable<PropertyBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the property builders is null.");

            _properties.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a method to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithMethod(MethodBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _methods.Add(builder);
            return this;
        }
        
        /// <summary>
        /// Adds a bunch of methods to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithMethods(params MethodBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the field builders is null.");
            
            _methods.AddRange(builders);
            return this;
        }
        
        /// <summary>
        /// Adds a bunch of dependencies to the class being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="dependencies"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="dependencies"/> is <c>null</c>.
        /// </exception>
        public ClassBuilder WithDependencies(params string[] dependencies)
        {
            if (dependencies is null)
                throw new ArgumentNullException(nameof(dependencies));

            if (dependencies.Any(x => x is null))
                throw new ArgumentException("One of the field dependencies is null.");
            
            foreach (var dependency in dependencies)
            {
                 if (!Class.Dependencies.Contains(dependency))   
                     Class.Dependencies.Add(dependency);
            }
            return this;
        }

        /// <summary>
        /// Sets whether the class being built should be static or not.
        /// </summary>
        /// <param name="makeStatic">
        /// Indicates whether the class should be static or not.
        /// </param>
        public ClassBuilder MakeStatic(bool makeStatic = true)
        {
            Class.Set(isStatic: Option.Some(makeStatic));
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

        public Class Build()
        {
            if (string.IsNullOrWhiteSpace(Class.Name.ValueOrDefault()))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the class is required when building a class.");
            }
            else if (Class.IsStatic && _methods.Count > 1)
            {
                throw new SyntaxException("Static classes can have only 1 constructor.");
            }

            
            Class.Fields=_fields.Select(builder => builder.Build()).ToList();
            Class.Properties=_properties.Select(builder => builder.Build()).ToList();
            Class.TypeParameters=_typeParameters.Select(builder => builder.Build()).ToList();
            Class.ImplementedInterfaces=_implementedInterfaces.Select(builder => builder.Build()).ToList();
            Class.Attributes=_attributeBuilders.Select(builder => builder.Build()).ToList();
            Class.Methods=
                _methods.Select(builder => builder
                    .WithParent(Class)
                    .MakeStatic(Class.IsStatic)
                    .Build()).ToList();

            return Class;
        }
    }
}