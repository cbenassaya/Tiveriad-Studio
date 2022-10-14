using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class InternalTypeCodeBuilder : ICodeBuilder
{
    private readonly List<InternalTypeCodeBuilder> _genericArgumentBuilders = new();


    private InternalType _internalType = new(string.Empty);

    /// <summary>
    ///     Sets the name of the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public InternalTypeCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Property name must be a valid, non-empty string.", nameof(name));

        _internalType = _internalType.Set(name);
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public InternalTypeCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("InternalType namespace must be a valid, non-empty string.",
                nameof(@namespace));

        _internalType = _internalType.Set(@namespace: @namespace);
        return this;
    }

    /// <summary>
    ///     Sets the XDataType's reference of the internalType being built.
    /// </summary>
    /// <param name="reference">
    ///     The content of the XDataType's reference.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="reference" /> is <c>null</c>.
    /// </exception>
    public InternalTypeCodeBuilder WithReference(XType reference)
    {
        if (reference is null)
            throw new ArgumentNullException(nameof(reference));

        _internalType = _internalType.Set(reference: reference);
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the class.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public InternalTypeCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _internalType = _internalType.Set(summary: summary);
        return this;
    }


    /// <summary>
    ///     Adds a generic argument to the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public InternalTypeCodeBuilder WithGenericArgument(InternalTypeCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _genericArgumentBuilders.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of generic argument to the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InternalTypeCodeBuilder WithGenericArguments(params InternalTypeCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _genericArgumentBuilders.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of generic argument to the internalType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InternalTypeCodeBuilder WithGenericArguments(IEnumerable<InternalTypeCodeBuilder> builders)
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
        if (string.IsNullOrWhiteSpace(_internalType.Namespace))
            throw new MissingBuilderSettingException(
                "Providing the namespace of the internalType is required when building a internalType.");
        if (string.IsNullOrWhiteSpace(_internalType.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the internalType is required when building a internalType.");

        _internalType.GenericArguments.AddRange(
            _genericArgumentBuilders.Select(builder => builder
                .Build()));
        return _internalType;
    }
}