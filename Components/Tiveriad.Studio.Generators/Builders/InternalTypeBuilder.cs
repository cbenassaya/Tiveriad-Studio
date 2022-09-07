using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class InternalTypeBuilder  : AbstractBuilder
{
    
    private readonly List<InternalTypeBuilder> _genericArgumentBuilders = new List<InternalTypeBuilder>();
    
    
    internal InternalType InternalType { get; private set; } = new InternalType(name: Option.None<string>());
    
    /// <summary>
    /// Sets the name of the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="name"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> is empty or invalid.
    /// </exception>
    public InternalTypeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Property name must be a valid, non-empty string.", nameof(name));

        InternalType = InternalType.Set(name: Option.Some(name));
        return this;
    }
    
    /// <summary>
    /// Sets the namespace of the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="namespace"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="namespace"/> is empty or invalid.
    /// </exception>
    public InternalTypeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("InternalType namespace must be a valid, non-empty string.", nameof(@namespace));

        InternalType = InternalType.Set(@namespace: Option.Some(@namespace));
        return this;
    }
    
    /// <summary>
    /// Sets the XDataType's reference of the internalType being built.
    /// </summary>
    /// <param name="reference">
    /// The content of the XDataType's reference.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="reference"/> is <c>null</c>.
    /// </exception>
    public InternalTypeBuilder WithReference(XType reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        InternalType = InternalType.Set(reference: Option.Some(reference));
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
    public InternalTypeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        InternalType = InternalType.Set(summary: Option.Some(summary));
        return this;
    }
    
    
        /// <summary>
        /// Adds a generic argument to the internalType being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public InternalTypeBuilder WithGenericArgument(InternalTypeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _genericArgumentBuilders.Add(builder);
            return this;
        }

        /// <summary>
        /// Adds a bunch of generic argument to the internalType being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public InternalTypeBuilder WithGenericArguments(params InternalTypeBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _genericArgumentBuilders.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of generic argument to the internalType being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public InternalTypeBuilder WithGenericArguments(IEnumerable<InternalTypeBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the type parameter builders is null.");

            _genericArgumentBuilders.AddRange(builders);
            return this;
        }
    
    public InternalType Build()
    {
        if  (string.IsNullOrWhiteSpace(InternalType.Namespace.ValueOrDefault()))
        {
            throw new MissingBuilderSettingException(
                "Providing the namespace of the internalType is required when building a internalType.");
        }
        else if (string.IsNullOrWhiteSpace(InternalType.Name.ValueOrDefault()))
        {
            throw new MissingBuilderSettingException(
                "Providing the name of the internalType is required when building a internalType.");
        }

        InternalType.GenericArguments.AddRange(
            _genericArgumentBuilders.Select(builder => builder
                .Build()));
        return InternalType;
    }
}