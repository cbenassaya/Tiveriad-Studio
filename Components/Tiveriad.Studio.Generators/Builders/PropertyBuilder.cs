using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building properties. <see cref="PropertyBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class PropertyBuilder  : AbstractBuilder
    {
        private readonly List<TypeParameterBuilder> _typeParameters = new List<TypeParameterBuilder>();
        private readonly List<AttributeBuilder> _attributeBuilders = new List<AttributeBuilder>();

        internal PropertyBuilder()
        {
        }

        internal Property Property { get; private set; } = new Property(
            accessModifier: AccessModifier.Public,
            setterAccessModifier: AccessModifier.Public,
            getter: Option.Some(Property.AutoGetterSetter),
            setter: Option.Some(Property.AutoGetterSetter));

        /// <summary>
        /// Sets the access modifier of the property being built.
        /// </summary>
        public PropertyBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Property = Property.Set(accessModifier: Option.Some(accessModifier), setterAccessModifier: Option.Some(accessModifier));
            return this;
        }

        /// <summary>
        /// Sets the access modifier of the property being built.
        /// </summary>
        public PropertyBuilder WithAccessModifier(AccessModifier getterAccessModifier, AccessModifier setterAccessModifier)
        {
            Property = Property.Set(accessModifier: Option.Some(getterAccessModifier), setterAccessModifier: Option.Some(setterAccessModifier));
            return this;
        }

        /// <summary>
        /// Sets the type of the property being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="type"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="type"/> is empty or invalid.
        /// </exception>
        public PropertyBuilder WithType(InternalType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type ==null)
                throw new ArgumentException("Property type must be a valid, not null.", nameof(type));

            Property = Property.Set(type: Option.Some(type));
            return this;
        }


        /// <summary>
        /// Sets the name of the property being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public PropertyBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Property name must be a valid, non-empty string.", nameof(name));

            Property = Property.Set(name: Option.Some(name));
            return this;
        }

        /// <summary>
        /// Sets the getter logic of the property being built.
        /// </summary>
        /// <param name="expression">
        /// The expression or block body of the getter. If not specified the default getter will be used - <c>get;</c>.
        /// </param>
        /// <example>
        /// This example shows the generated code for a property with a default getter.
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithGetter()
        /// public int Identifier
        /// {
        ///     get;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a getter that is an expression.
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithGetter("_id")
        /// public int Identifier
        /// {
        ///     get => _id;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a getter that has a block body.
        /// <code>
        /// // PropertyBuilder.WithName("IsCreated").WithGetter("{ if (_id == -1) { return false; } else { return true; } }")
        /// public int Identifier
        /// {
        ///     get
        ///     {
        ///         if (_id == -1)
        ///         {
        ///             return false;
        ///         }
        ///         else
        ///         {
        ///             return true;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public PropertyBuilder WithGetter(string? expression = null)
        {
            var expressionNormalized = string.IsNullOrWhiteSpace(expression)
                ? Property.AutoGetterSetter
                : expression!;
            Property = Property.Set(getter: Option.Some(Option.Some(expressionNormalized)));
            return this;
        }

        /// <summary>
        /// Specifies that the property being built should not have a getter.
        /// </summary>
        public PropertyBuilder WithoutGetter()
        {
            Property = Property.Set(getter: Option.Some(Option.None<string>()));
            return this;
        }

        /// <summary>
        /// Sets the setter logic of the property being built.
        /// </summary>
        /// <param name="expression">
        /// The expression or block body of the setter. If not specified the default setter will be used - <c>set;</c>
        /// Custom setter logic can make use of the <c>value</c> provided to the property setter.
        /// </param>
        /// <example>
        /// This example shows the generated code for a property with a default setter.
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithSetter()
        /// public int Identifier
        /// {
        ///     set;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a setter that is an expression.
        /// <code>
        /// // PropertyBuilder.WithName("Identifier").WithSetter("_id = value")
        /// public int Identifier
        /// {
        ///     set => _id = value;
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a property with a setter that has a block body.
        /// <code>
        /// // PropertyBuilder.WithName("Value")
        /// //    .WithSetter("{ if (value &lt;= 0) { throw new Exception(); } _value = value; }")
        /// public int Identifier
        /// {
        ///     set
        ///     {
        ///         if (_id == -1)
        ///         {
        ///             return false;
        ///         }
        ///         else
        ///         {
        ///             return true;
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public PropertyBuilder WithSetter(string? expression = null)
        {
            var expressionNormalized = string.IsNullOrWhiteSpace(expression)
                ? Property.AutoGetterSetter
                : expression!;
            Property = Property.Set(setter: Option.Some(Option.Some(expressionNormalized)));
            return this;
        }

        /// <summary>
        /// Specifies that the property being built should not have a setter.
        /// </summary>
        public PropertyBuilder WithoutSetter()
        {
            Property = Property.Set(setter: Option.Some(Option.None<string>()));
            return this;
        }

        /// <summary>
        /// Sets the default value of the property being built.
        /// </summary>
        /// <param name="defaultValue">
        /// The default value of the property. The value is used as-is, therefore you should wrap any string values in
        /// escaped quotes. For example,
        /// <c>WithDefaultValue(defaultValue: "\"this will be generated as a string\"")</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="defaultValue"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="defaultValue"/> is empty or invalid.
        /// </exception>
        public PropertyBuilder WithDefaultValue(string defaultValue)
        {
            if (defaultValue is null)
                throw new ArgumentNullException(nameof(defaultValue));

            if (string.IsNullOrWhiteSpace(defaultValue))
            {
                throw new ArgumentException(
                    "Property default value must be a valid, non-empty string. " +
                        "For default value of an empty string please use WithDefaultValue(\"string.Empty\").",
                    nameof(defaultValue));
            }

            Property = Property.Set(defaultValue: Option.Some(defaultValue));
            return this;
        }

        /// <summary>
        /// Sets whether the property being built should be static or not.
        /// </summary>
        /// <param name="makeStatic">
        /// Indicates whether the property should be static or not.
        /// </param>
        public PropertyBuilder MakeStatic(bool makeStatic = true)
        {
            Property = Property.Set(isStatic: Option.Some(makeStatic));
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the property.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public PropertyBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Property = Property.Set(summary: Option.Some<string>(summary));
            return this;
        }

        /// <summary>
        /// Adds a type parameter to the property being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public PropertyBuilder WithTypeParameter(TypeParameterBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _typeParameters.Add(builder);
            return this;
        }
        
        


        /// <summary>
        /// Adds a bunch of type parameters to the property being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public PropertyBuilder WithTypeParameters(params TypeParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }
        
        public PropertyBuilder WithAttribute(AttributeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _attributeBuilders.Add(builder);
            return this;
        }
        
        public PropertyBuilder WithAttributes(params AttributeBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));
            
            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the attribute builders is null.");

            _attributeBuilders.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the property being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public PropertyBuilder WithTypeParameters(IEnumerable<TypeParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }


        public Property Build()
        {
            if (string.IsNullOrWhiteSpace(Property.Name.ValueOrDefault()))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the property is required when building a property.");
            }
            else if (!Property.Type.HasValue)
            {
                throw new MissingBuilderSettingException(
                    "Providing the type of the property is required when building a property.");
            }
            else if (Property.Setter.Exists(value => value.Equals(Property.AutoGetterSetter)))
            {
                if (!Property.Getter.HasValue)
                {
                    throw new SyntaxException(
                        "Properties with auto implemented setters must also have auto implemented getters. (CS8051)");
                }
                else if (Property.Getter.Exists(value => !value.Equals(Property.AutoGetterSetter)))
                {
                    throw new SyntaxException(
                        "Properties with custom getters cannot have auto implemented setters.");
                }
            }
            else if (Property.DefaultValue.HasValue)
            {
                if (Property.Getter.Exists(value => !value.Equals(Property.AutoGetterSetter)) ||
                    Property.Setter.Exists(value => !value.Equals(Property.AutoGetterSetter)))
                {
                    throw new SyntaxException("Only auto implemented properties can have a default value. (CS8050)");
                }
            }

            Property.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));
            
            Property.Attributes.AddRange(_attributeBuilders.Select(builder => builder.Build()));


            return Property;
        }
    }
}