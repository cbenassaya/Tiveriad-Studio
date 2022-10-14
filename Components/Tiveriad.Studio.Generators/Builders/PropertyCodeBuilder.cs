using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building properties. <see cref="PropertyCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class PropertyCodeBuilder : ICodeBuilder
{
    private readonly List<AttributeBuilder> _attributeBuilders = new();
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();

    private Property _property = new(
        AccessModifier.Public,
        AccessModifier.Public);

    /// <summary>
    ///     Sets the access modifier of the property being built.
    /// </summary>
    public PropertyCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _property = _property.Set(accessModifier, accessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the property being built.
    /// </summary>
    public PropertyCodeBuilder WithAccessModifier(AccessModifier getterAccessModifier,
        AccessModifier setterAccessModifier)
    {
        _property = _property.Set(getterAccessModifier, setterAccessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the type of the property being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> is empty or invalid.
    /// </exception>
    public PropertyCodeBuilder WithType(InternalType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        if (type == null)
            throw new ArgumentException("Property type must be a valid, not null.", nameof(type));

        _property = _property.Set(type: type);
        return this;
    }


    /// <summary>
    ///     Sets the name of the property being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public PropertyCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Property name must be a valid, non-empty string.", nameof(name));

        _property = _property.Set(name: name);
        return this;
    }

    /// <summary>
    ///     Sets the getter logic of the property being built.
    /// </summary>
    /// <param name="expression">
    ///     The expression or block body of the getter. If not specified the default getter will be used - <c>get;</c>.
    /// </param>
    /// <example>
    ///     This example shows the generated code for a property with a default getter.
    ///     <code>
    /// // PropertyBuilder.WithName("Identifier").WithGetter()
    /// public int Identifier
    /// {
    ///     get;
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a property with a getter that is an expression.
    ///     <code>
    /// // PropertyBuilder.WithName("Identifier").WithGetter("_id")
    /// public int Identifier
    /// {
    ///     get => _id;
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a property with a getter that has a block body.
    ///     <code>
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
    public PropertyCodeBuilder WithGetter(string? expression = null)
    {
        var expressionNormalized = string.IsNullOrWhiteSpace(expression)
            ? Property.AutoGetterSetter
            : expression!;
        _property = _property.Set(getter: expressionNormalized);
        return this;
    }

    /// <summary>
    ///     Specifies that the property being built should not have a getter.
    /// </summary>
    public PropertyCodeBuilder WithoutGetter()
    {
        _property = _property.Set(getter: string.Empty);
        return this;
    }

    /// <summary>
    ///     Sets the setter logic of the property being built.
    /// </summary>
    /// <param name="expression">
    ///     The expression or block body of the setter. If not specified the default setter will be used - <c>set;</c>
    ///     Custom setter logic can make use of the <c>value</c> provided to the property setter.
    /// </param>
    /// <example>
    ///     This example shows the generated code for a property with a default setter.
    ///     <code>
    /// // PropertyBuilder.WithName("Identifier").WithSetter()
    /// public int Identifier
    /// {
    ///     set;
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a property with a setter that is an expression.
    ///     <code>
    /// // PropertyBuilder.WithName("Identifier").WithSetter("_id = value")
    /// public int Identifier
    /// {
    ///     set => _id = value;
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a property with a setter that has a block body.
    ///     <code>
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
    public PropertyCodeBuilder WithSetter(string? expression = null)
    {
        var expressionNormalized = string.IsNullOrWhiteSpace(expression)
            ? Property.AutoGetterSetter
            : expression!;
        _property = _property.Set(setter: expressionNormalized);
        return this;
    }

    /// <summary>
    ///     Specifies that the property being built should not have a setter.
    /// </summary>
    public PropertyCodeBuilder WithoutSetter()
    {
        _property = _property.Set(setter: string.Empty);
        return this;
    }

    /// <summary>
    ///     Sets the default value of the property being built.
    /// </summary>
    /// <param name="defaultValue">
    ///     The default value of the property. The value is used as-is, therefore you should wrap any string values in
    ///     escaped quotes. For example,
    ///     <c>WithDefaultValue(defaultValue: "\"this will be generated as a string\"")</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="defaultValue" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="defaultValue" /> is empty or invalid.
    /// </exception>
    public PropertyCodeBuilder WithDefaultValue(string defaultValue)
    {
        if (defaultValue is null)
            throw new ArgumentNullException(nameof(defaultValue));

        if (string.IsNullOrWhiteSpace(defaultValue))
            throw new ArgumentException(
                "Property default value must be a valid, non-empty string. " +
                "For default value of an empty string please use WithDefaultValue(\"string.Empty\").",
                nameof(defaultValue));

        _property = _property.Set(defaultValue: defaultValue);
        return this;
    }

    /// <summary>
    ///     Sets whether the property being built should be static or not.
    /// </summary>
    /// <param name="makeStatic">
    ///     Indicates whether the property should be static or not.
    /// </param>
    public PropertyCodeBuilder MakeStatic(bool makeStatic = true)
    {
        _property = _property.Set(isStatic: makeStatic);
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the property.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public PropertyCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _property = _property.Set(summary: summary);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the property being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public PropertyCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _typeParameters.Add(codeBuilder);
        return this;
    }


    /// <summary>
    ///     Adds a bunch of type parameters to the property being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public PropertyCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    public PropertyCodeBuilder WithAttribute(AttributeBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        _attributeBuilders.Add(builder);
        return this;
    }

    public PropertyCodeBuilder WithAttributes(params AttributeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the attribute builders is null.");

        _attributeBuilders.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the property being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public PropertyCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
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
        if (string.IsNullOrWhiteSpace(_property.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the property is required when building a property.");

        if (_property.Type == null)
            throw new MissingBuilderSettingException(
                "Providing the type of the property is required when building a property.");


        _property.TypeParameters.Clear();
        _property.Attributes.Clear();
        _property.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));
        _property.Attributes.AddRange(_attributeBuilders.Select(builder => builder.Build()));

        return _property;
    }
}