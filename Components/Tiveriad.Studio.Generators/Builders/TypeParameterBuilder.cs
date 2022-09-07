using Optional;
using Optional.Unsafe;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
/// Provides functionalty for building type parameters.
/// <see cref="TypeParameterBuilder"/> instances are <b>not</b> immutable.
/// </summary>
public class TypeParameterBuilder  : AbstractBuilder
{
    internal TypeParameterBuilder()
    {
    }

    internal TypeParameter TypeParameter { get; private set; } = new TypeParameter(name: Option.None<string>());

    /// <summary>
    /// Sets the name of the type parameter.
    /// </summary>
    /// <param name="name">The name of the type parameter.</param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="name"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specified <paramref name="name"/> is empty or invalid.
    /// </exception>
    public TypeParameterBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The type parameter name must be a valid, non-empty string.", nameof(name));

        TypeParameter = TypeParameter.With(name: Option.Some(name));
        return this;
    }

    /// <summary>
    /// Adds a constraint to the the type parameter. See <see cref="TypeParameterConstraint"/>
    /// for various constraints and their explanations.
    /// </summary>
    /// <param name="constraint">
    /// The constraint to be added to the type parameter.
    /// Optionally, you can use <see cref="TypeParameterConstraint"/> members.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specific <paramref name="constraint"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The specific <paramref name="constraint"/> is empty or invalid.
    /// </exception>
    public TypeParameterBuilder WithConstraint(string constraint)
    {
        if (constraint is null)
            throw new ArgumentNullException(nameof(constraint));

        if (string.IsNullOrWhiteSpace(constraint))
            throw new ArgumentException("The type parameter constraint must be a valid, non-empty string.", nameof(constraint));

        TypeParameter.Constraints.Add(constraint);
        return this;
    }

    /// <summary>
    /// Adds a bunch of constraints to the type parameter. See <see cref="TypeParameterConstraint"/>
    /// for various constraints and their explanations.
    /// </summary>
    /// <param name="constraints">
    /// The constraints to be added to the type parameter.
    /// Optionally, you can use <see cref="TypeParameterConstraint"/> members.
    /// </param>
    /// <exception cref="ArgumentException">
    /// One of the specified <paramref name="constraints"/> is <c>null</c> or
    /// an empty (invalid) string.
    /// </exception>
    public TypeParameterBuilder WithConstraints(params string[] constraints)
    {
        if (constraints.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("One of the constraints value is null or empty.");

        TypeParameter.Constraints.AddRange(constraints);
        return this;
    }

    /// <summary>
    /// Adds a bunch of constraints to the type parameter. See <see cref="TypeParameterConstraint"/>
    /// for various constraints and their explanations.
    /// </summary>
    /// <param name="constraints">
    /// The constraints to be added to the type parameter.
    /// Optionally, you can use <see cref="TypeParameterConstraint"/> members.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The specified <paramref name="constraints"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// One of the specified <paramref name="constraints"/> is <c>null</c> or
    /// an empty (invalid) string.
    /// </exception>
    public TypeParameterBuilder WithConstraints(IEnumerable<string> constraints)
    {
        if (constraints is null)
            throw new ArgumentNullException(nameof(constraints));

        if (constraints.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("One of the constraints value is null or empty.");

        TypeParameter.Constraints.AddRange(constraints);
        return this;
    }

    internal TypeParameter Build()
    {
        if (string.IsNullOrWhiteSpace(TypeParameter.Name.ValueOrDefault()))
        {
            throw new MissingBuilderSettingException(
                "Providing the name of the type parameter is required when building a type parameter.");
        }

        return TypeParameter;
    }
}