using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building fields. <see cref="FieldBuilder"/> instances are <b>not</b> immutable.
    /// </summary>
    public class FieldBuilder  : AbstractBuilder
    {
        private readonly List<TypeParameterBuilder> _typeParameters = new List<TypeParameterBuilder>();

        internal FieldBuilder()
        {
        }

        internal Field Field { get; private set; } = new Field(AccessModifier.Private);

        /// <summary>
        /// Sets accessibilty modifier of the field being built.
        /// </summary>
        public FieldBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Field = Field.Set(accessModifier: Option.Some(accessModifier));
            return this;
        }

        /// <summary>
        /// Sets the type of the field being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="type"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="type"/> is empty or invalid.
        /// </exception>
        public FieldBuilder WithType(InternalType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (type==null)
                throw new ArgumentException("Field type must be a valid, non-empty string.", nameof(type));

            Field = Field.Set(type: Option.Some(type));
            return this;
        }

        /// <summary>
        /// Sets the name of the field being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public FieldBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Field name must be a valid, non-empty string.", nameof(name));

            Field = Field.Set(name: Option.Some(name));
            return this;
        }

        /// <summary>
        /// Sets the readonly preference for the field being built.
        /// </summary>
        /// <param name="makeReadonly">
        /// Indicates whether the field will be made readonly (<c>true</c>) or not (<c>false</c>).
        /// </param>
        public FieldBuilder MakeReadonly(bool makeReadonly = true)
        {
            Field = Field.Set(isReadonly: Option.Some(makeReadonly));
            return this;
        }
        
        /// <summary>
        /// Sets the initializeFromConstructor preference for the field being built.
        /// </summary>
        /// <param name="initializeFromConstructor">
        /// Indicates whether the field will be initialize from constructor (<c>true</c>) or not (<c>false</c>).
        /// </param>
        public FieldBuilder InitializeFromConstructor(bool initializeFromConstructor = false)
        {
            Field = Field.Set(initializeFromConstructor: Option.Some(initializeFromConstructor));
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the field.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public FieldBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Field = Field.Set(summary: Option.Some(summary));
            return this;
        }

        /// <summary>
        /// Adds a type parameter to the field being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public FieldBuilder WithTypeParameter(TypeParameterBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _typeParameters.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the field being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public FieldBuilder WithTypeParameters(params TypeParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of type parameters to the field being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public FieldBuilder WithTypeParameters(IEnumerable<TypeParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _typeParameters.AddRange(builders);
            return this;
        }

        internal Field Build()
        {
            if (!Field.Type.HasValue)
            {
                throw new MissingBuilderSettingException(
                    "Providing the type of the field is required when building a field.");
            }
            else if (string.IsNullOrWhiteSpace(Field.Name.ValueOrDefault()))
            {
                throw new MissingBuilderSettingException(
                    "Providing the name of the field is required when building a field.");
            }

            Field.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));

            return Field;
        }
    }
}
