using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionalty for building type parameters.
///     <see cref="TypeParameterCodeBuilder" /> instances are <b>not</b> immutable.
/// </summary>
public class TypeParameterCodeBuilder : ICodeBuilder
{
    private TypeParameter _typeParameter = new(Option.None<string>());

    /// <summary>
    ///     Sets the name of the type parameter.
    /// </summary>
    /// <param name="name">The name of the type parameter.</param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public TypeParameterCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The type parameter name must be a valid, non-empty string.", nameof(name));

        _typeParameter = _typeParameter.With(Option.Some(name));
        return this;
    }

    /// <summary>
    ///     Adds a constraint to the the type parameter. See <see cref="TypeParameterConstraint" />
    ///     for various constraints and their explanations.
    /// </summary>
    /// <param name="constraint">
    ///     The constraint to be added to the type parameter.
    ///     Optionally, you can use <see cref="TypeParameterConstraint" /> members.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specific <paramref name="constraint" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specific <paramref name="constraint" /> is empty or invalid.
    /// </exception>
    public TypeParameterCodeBuilder WithConstraint(string constraint)
    {
        if (constraint is null)
            throw new ArgumentNullException(nameof(constraint));

        if (string.IsNullOrWhiteSpace(constraint))
            throw new ArgumentException("The type parameter constraint must be a valid, non-empty string.",
                nameof(constraint));

        _typeParameter.Constraints.Add(constraint);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of constraints to the type parameter. See <see cref="TypeParameterConstraint" />
    ///     for various constraints and their explanations.
    /// </summary>
    /// <param name="constraints">
    ///     The constraints to be added to the type parameter.
    ///     Optionally, you can use <see cref="TypeParameterConstraint" /> members.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="constraints" /> is <c>null</c> or
    ///     an empty (invalid) string.
    /// </exception>
    public TypeParameterCodeBuilder WithConstraints(params string[] constraints)
    {
        if (constraints.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("One of the constraints value is null or empty.");

        _typeParameter.Constraints.AddRange(constraints);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of constraints to the type parameter. See <see cref="TypeParameterConstraint" />
    ///     for various constraints and their explanations.
    /// </summary>
    /// <param name="constraints">
    ///     The constraints to be added to the type parameter.
    ///     Optionally, you can use <see cref="TypeParameterConstraint" /> members.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="constraints" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="constraints" /> is <c>null</c> or
    ///     an empty (invalid) string.
    /// </exception>
    public TypeParameterCodeBuilder WithConstraints(IEnumerable<string> constraints)
    {
        if (constraints is null)
            throw new ArgumentNullException(nameof(constraints));

        if (constraints.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("One of the constraints value is null or empty.");

        _typeParameter.Constraints.AddRange(constraints);
        return this;
    }

    public TypeParameter Build()
    {
        if (string.IsNullOrWhiteSpace(_typeParameter.Name.ValueOrDefault()))
            throw new MissingBuilderSettingException(
                "Providing the name of the type parameter is required when building a type parameter.");

        return _typeParameter;
    }
}