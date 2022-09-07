using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

public class ParameterBuilder  : AbstractBuilder
{

    internal ParameterBuilder()
    {
    }

    internal Parameter Parameter { get; private set; } = new Parameter();

    /// <summary>
    /// Sets the type of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="type"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="type"/> is empty or invalid.
    /// </exception>
    public ParameterBuilder WithType(InternalType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        if (type==null)
            throw new ArgumentException("Field type must be a valid, non-empty string.", nameof(type));

        Parameter = Parameter.Set(type: Option.Some(type));
        return this;
    }

    /// <summary>
    /// Sets the name of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="name"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> is empty or invalid.
    /// </exception>
    public ParameterBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Field name must be a valid, non-empty string.", nameof(name));

        Parameter = Parameter.Set(name: Option.Some(name));
        return this;
    }
    
    /// <summary>
    /// Sets the receiving member of the parameter being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="receivingMember"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="receivingMember"/> is empty or invalid.
    /// </exception>
    public ParameterBuilder WithReceivingMember(string receivingMember)
    {
        if (receivingMember is null)
            throw new ArgumentNullException(nameof(receivingMember));

        if (string.IsNullOrWhiteSpace(receivingMember))
            throw new ArgumentException("Receiving member must be a valid, non-empty string.", nameof(receivingMember));

        Parameter = Parameter.Set(receivingMember: Option.Some(receivingMember));
        return this;
    }

    internal Parameter Build()
    {
        if (!Parameter.Type.HasValue)
        {
            throw new MissingBuilderSettingException(
                "Providing the type of the field is required when building a field.");
        }
        else if (string.IsNullOrWhiteSpace(Parameter.Name.ValueOrDefault()))
        {
            throw new MissingBuilderSettingException(
                "Providing the name of the field is required when building a field.");
        }
        return Parameter;
    }
}