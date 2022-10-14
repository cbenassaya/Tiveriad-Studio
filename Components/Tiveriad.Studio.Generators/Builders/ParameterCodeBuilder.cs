using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class ParameterCodeBuilder : ICodeBuilder
{
    private readonly List<AttributeBuilder> _attributeBuilders = new();
    private Parameter _parameter = new();

    /// <summary>
    ///     Sets the type of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> is empty or invalid.
    /// </exception>
    public ParameterCodeBuilder WithType(InternalType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        if (type == null)
            throw new ArgumentException("Field type must be a valid, non-empty string.", nameof(type));

        _parameter = _parameter.Set(Option.Some(type));
        return this;
    }
    
    public ParameterCodeBuilder WithAttribute(AttributeBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        _attributeBuilders.Add(builder);
        return this;
    }

    public ParameterCodeBuilder WithAttributes(params AttributeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the attribute builders is null.");

        _attributeBuilders.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Sets the name of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public ParameterCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Field name must be a valid, non-empty string.", nameof(name));

        _parameter = _parameter.Set(name: Option.Some(name));
        return this;
    }

    /// <summary>
    ///     Sets the receiving member of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="receivingMember" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="receivingMember" /> is empty or invalid.
    /// </exception>
    public ParameterCodeBuilder WithReceivingMember(string receivingMember)
    {
        if (receivingMember is null)
            throw new ArgumentNullException(nameof(receivingMember));

        if (string.IsNullOrWhiteSpace(receivingMember))
            throw new ArgumentException("Receiving member must be a valid, non-empty string.", nameof(receivingMember));

        _parameter = _parameter.Set(receivingMember: Option.Some(receivingMember));
        return this;
    }

    public Parameter Build()
    {
        if (!_parameter.Type.HasValue)
            throw new MissingBuilderSettingException(
                "Providing the type of the field is required when building a field.");
        if (string.IsNullOrWhiteSpace(_parameter.Name.ValueOrDefault()))
            throw new MissingBuilderSettingException(
                "Providing the name of the field is required when building a field.");
        
        _parameter.Attributes.Clear();
        _parameter.Attributes.AddRange(_attributeBuilders.Select(builder => builder.Build()));
        return _parameter;
    }
}