using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building constructors. <see cref="MethodCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class MethodCodeBuilder : ICodeBuilder
{
    private readonly List<AttributeBuilder> _attributeBuilders = new();
    private readonly List<ParameterCodeBuilder> _parameters = new();
    private Method _method { get; } = new(AccessModifier.Public);

    /// <summary>
    ///     Sets the access modifier of the constructor being built.
    /// </summary>
    public MethodCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _method.Set(accessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the name of the method being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public MethodCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The method name must be a valid, non-empty string.", nameof(name));

        _method.Set(name: name);
        return this;
    }

    public MethodCodeBuilder WithAttribute(AttributeBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        _attributeBuilders.Add(builder);
        return this;
    }

    public MethodCodeBuilder WithAttributes(params AttributeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the attribute builders is null.");

        _attributeBuilders.AddRange(builders);
        return this;
    }


    /// <summary>
    ///     Adds a bunch of parameters to the method being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public MethodCodeBuilder WithParameters(params ParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the parameter builders is null.");

        _parameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of parameters to the method being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public MethodCodeBuilder WithParameters(IEnumerable<ParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the parameter builders is null.");

        _parameters.AddRange(builders);
        return this;
    }


    /// <summary>
    ///     Sets the <c>base()</c> call of the constructor with the specified <paramref name="passedParameter" />.
    /// </summary>
    /// <param name="passedParameter">
    ///     The parameter that will be passed to the <c>base()</c> call.
    /// </param>
    /// <example>
    ///     This example shows the generated code for a constructor with a base call.
    ///     <code>
    /// // ConstructorBuilder.WithBaseCall();
    /// public User(): base()
    /// {
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a constructor with a base call with passed parameters.
    ///     <code>
    /// // ConstructorBuilder.WithParameter("string", "username").WithBaseCall("username");
    /// public User(string username): base(username)
    /// {
    /// }
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="passedParameter" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified he <paramref name="passedParameter" /> value is empty or invalid.
    /// </exception>
    public MethodCodeBuilder WithBaseCall(string passedParameter)
    {
        if (passedParameter is null)
            throw new ArgumentNullException(nameof(passedParameter));

        if (string.IsNullOrWhiteSpace(passedParameter))
            throw new ArgumentException("The base call parameter must be a valid parameter name.",
                nameof(passedParameter));

        var items = new List<string> { passedParameter };
        _method.Set(baseCallParameters: items);
        return this;
    }

    /// <summary>
    ///     Sets the <c>base()</c> call of the constructor with the specified <paramref name="passedParameters" />.
    /// </summary>
    /// <param name="passedParameters">
    ///     The parameters that will be passed to the <c>base()</c> call.
    /// </param>
    /// <example>
    ///     This example shows the generated code for a constructor with a base call.
    ///     <code>
    /// // ConstructorBuilder.WithBaseCall();
    /// public User(): base()
    /// {
    /// }
    /// </code>
    /// </example>
    /// <example>
    ///     This example shows the generated code for a constructor with a base call with passed parameters.
    ///     <code>
    /// // ConstructorBuilder
    /// //   .WithParameter("string", "username")
    /// //   .WithParameter("int", "age")
    /// //   .WithBaseCall("username", "age);
    /// public User(string username, int age): base(username, age)
    /// {
    /// }
    /// </code>
    /// </example>
    /// <exception cref="ArgumentException">
    ///     One of the <paramref name="passedParameters" /> values is <c>null</c>.
    /// </exception>
    public MethodCodeBuilder WithBaseCall(params string[] passedParameters)
    {
        if (passedParameters.Any(x => string.IsNullOrWhiteSpace(x)))
            throw new ArgumentException($"One of the {nameof(passedParameters)} parameter values was null.");
        var items = new List<string>();
        items.AddRange(passedParameters);
        _method.Set(baseCallParameters: items);
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the constructor.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public MethodCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _method.Set(summary: summary);
        return this;
    }

    public MethodCodeBuilder WithBody(string body)
    {
        if (body is null)
            throw new ArgumentNullException(nameof(body));

        _method.Set(body: body);
        return this;
    }


    public MethodCodeBuilder MakeStatic(bool makeStatic)
    {
        _method.Set(isStatic: makeStatic);
        return this;
    }

    public MethodCodeBuilder MakeAsync(bool makeAsync)
    {
        _method.Set(isAsync: makeAsync);
        return this;
    }

    public MethodCodeBuilder MakeConstructor(bool makeConstructor)
    {
        _method.Set(isConstructor: makeConstructor);
        return this;
    }

    public MethodCodeBuilder WithReturnType(InternalType returnType)
    {
        _method.Set(returnType: returnType);
        return this;
    }

    internal MethodCodeBuilder WithParent(Class parent)
    {
        _method.Set(parent: parent);
        return this;
    }

    public Method Build()
    {
        if (_method.IsStatic)
        {
            if (_method.AccessModifier != AccessModifier.None)
                throw new SyntaxException("Access modifiers are not allowed on static method. (CS0515)");

            if (_method.Parameters.Any())
                throw new SyntaxException("Parameters are not allowed on static method. (CS0132)");

            if (_method.BaseCallParameters.Any())
                throw new SyntaxException("Static constructors cannot call base constructors. (CS0514)");
        }

        if (!_method.IsConstructor)
            if (_method.BaseCallParameters.Any())
                throw new SyntaxException("No base call parameters on none constructor method.");

        _method.Parameters.Clear();
        _method.Attributes.Clear();

        _method.Parameters.AddRange(_parameters.Select(builder => builder.Build()));
        _method.Attributes.AddRange(_attributeBuilders.Select(builder => builder.Build()));
        return _method;
    }
}