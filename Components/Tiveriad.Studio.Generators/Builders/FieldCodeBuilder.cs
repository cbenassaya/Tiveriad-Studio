using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building fields. <see cref="FieldCodeBuilder" /> instances are <b>not</b> immutable.
/// </summary>
public class FieldCodeBuilder : ICodeBuilder
{
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();

    private Field _field = new(AccessModifier.Private);

    /// <summary>
    ///     Sets accessibilty modifier of the field being built.
    /// </summary>
    public FieldCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _field = _field.Set(Option.Some(accessModifier));
        return this;
    }

    /// <summary>
    ///     Sets the type of the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> is empty or invalid.
    /// </exception>
    public FieldCodeBuilder WithType(InternalType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        if (type == null)
            throw new ArgumentException("Field type must be a valid, non-empty string.", nameof(type));

        _field = _field.Set(type: Option.Some(type));
        return this;
    }

    /// <summary>
    ///     Sets the name of the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public FieldCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Field name must be a valid, non-empty string.", nameof(name));

        _field = _field.Set(name: Option.Some(name));
        return this;
    }

    /// <summary>
    ///     Sets the readonly preference for the field being built.
    /// </summary>
    /// <param name="makeReadonly">
    ///     Indicates whether the field will be made readonly (<c>true</c>) or not (<c>false</c>).
    /// </param>
    public FieldCodeBuilder MakeReadonly(bool makeReadonly = true)
    {
        _field = _field.Set(isReadonly: Option.Some(makeReadonly));
        return this;
    }

    /// <summary>
    ///     Sets the initializeFromConstructor preference for the field being built.
    /// </summary>
    /// <param name="initializeFromConstructor">
    ///     Indicates whether the field will be initialize from constructor (<c>true</c>) or not (<c>false</c>).
    /// </param>
    public FieldCodeBuilder InitializeFromConstructor(bool initializeFromConstructor = false)
    {
        _field = _field.Set(initializeFromConstructor: Option.Some(initializeFromConstructor));
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the field.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public FieldCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _field = _field.Set(summary: Option.Some(summary));
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public FieldCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _typeParameters.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public FieldCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the field being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public FieldCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    public Field Build()
    {
        if (!_field.Type.HasValue)
            throw new MissingBuilderSettingException(
                "Providing the type of the field is required when building a field.");
        if (string.IsNullOrWhiteSpace(_field.Name.ValueOrDefault()))
            throw new MissingBuilderSettingException(
                "Providing the name of the field is required when building a field.");
        _field.TypeParameters.Clear();
        _field.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));

        return _field;
    }
}