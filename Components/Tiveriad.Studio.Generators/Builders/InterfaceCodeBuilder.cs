using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building interface structures. <see cref="InterfaceCodeBuilder" /> instances are
///     <b>not</b> immutable.
/// </summary>
public class InterfaceCodeBuilder : ICodeBuilder
{
    private readonly List<PropertyCodeBuilder> _properties = new();
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();


    private Interface _interface = new(AccessModifier.Public);

    /// <summary>
    ///     Set the stereotype
    /// </summary>
    public InterfaceCodeBuilder WithStereotype(string value)
    {
        _interface.Set(stereotype: value);
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the interface being built.
    /// </summary>
    public InterfaceCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _interface = _interface.Set(accessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the name of the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public InterfaceCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Interface name must be a valid, non-empty string.", nameof(name));

        _interface = _interface.Set(name: name);
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public InterfaceCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

        _interface.Set(@namespace: @namespace);
        return this;
    }

    /// <summary>
    ///     Adds a property to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithProperty(PropertyCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _properties.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the builders provided with <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithProperties(params PropertyCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the provided property builders is null.", nameof(builders));

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the builders provided with the <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithProperties(IEnumerable<PropertyCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the property builders is null.", nameof(builders));

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds an interface to the list of interfaces that the interface implements.
    /// </summary>
    /// <param name="interface">
    ///     The name of the interface that the interface implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interface" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="interface" /> is empty or invalid.
    /// </exception>
    public InterfaceCodeBuilder WithExtentedInterface(Interface @interface)
    {
        if (@interface is null)
            throw new ArgumentNullException(nameof(@interface));

        if (@interface == null)
            throw new ArgumentException("Implemented interface must be a valid, non null.", nameof(@interface));

        _interface.ExtentedInterfaces.Add(@interface);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of interfaces to the list of interfaces that the interface implements.
    /// </summary>
    /// <param name="interfaces">
    ///     A collection with the names of interfaces that the interface implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interfaces" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="interfaces" /> is <c>null</c> or empty.
    /// </exception>
    public InterfaceCodeBuilder WithExtendedInterfaces(IEnumerable<Interface> interfaces)
    {
        if (interfaces is null)
            throw new ArgumentNullException(nameof(interfaces));

        if (interfaces.Any(x => x == null))
            throw new ArgumentException("One of the provided names is null.", nameof(interfaces));

        _interface.ExtentedInterfaces.AddRange(interfaces);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of interfaces to the list of interfaces that the interface implements.
    /// </summary>
    /// <param name="interfaces">
    ///     A collection with the names of interfaces that the interface implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interfaces" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="interfaces" /> is <c>null</c> or empty.
    /// </exception>
    public InterfaceCodeBuilder WithExtendedInterfaces(params Interface[] interfaces)
    {
        if (interfaces is null)
            throw new ArgumentNullException(nameof(interfaces));

        if (interfaces.Any(x => x == null))
            throw new ArgumentException("One of the provided names is null.", nameof(interfaces));

        _interface.ExtentedInterfaces.AddRange(interfaces);
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the interface.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _interface = _interface.Set(summary: summary);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _typeParameters.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.", nameof(builders));

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the interface being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public InterfaceCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.", nameof(builders));

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Checks whether the described member exists in the interface structure.
    /// </summary>
    /// <param name="name">
    ///     The name of the member.
    /// </param>
    /// <param name="memberType">
    ///     The type of the member. By default all members will be taken into account.
    /// </param>
    /// <param name="comparison">
    ///     The comparision type to be performed when comparing the described name against the names of the actual
    ///     members. By default casing is ignored.
    /// </param>
    public bool HasMember(
        string name,
        MemberType? memberType = default,
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        if (memberType == MemberType.Property)
            return _properties.Any(x => x.Build().Name.Equals(name, comparison));

        if (!memberType.HasValue) return HasMember(name, MemberType.Property, comparison);

        return false;
    }

    /// <summary>
    ///     Returns the source code of the built interface.
    /// </summary>
    /// <exception cref="MissingBuilderSettingException">
    ///     A setting that is required to build a valid class structure is missing.
    /// </exception>
    /// <exception cref="SyntaxException">
    ///     The class builder is configured in such a way that the resulting code would be invalid.
    /// </exception>
    public string ToSourceCode()
    {
        return string.Empty;
    }

    /// <summary>
    ///     Returns the source code of the built interface.
    /// </summary>
    /// <exception cref="MissingBuilderSettingException">
    ///     A setting that is required to build a valid class structure is missing.
    /// </exception>
    /// <exception cref="SyntaxException">
    ///     The class builder is configured in such a way that the resulting code would be invalid.
    /// </exception>
    public override string ToString()
    {
        return ToSourceCode();
    }

    public Interface Build()
    {
        if (string.IsNullOrWhiteSpace(_interface.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the interface is required when building an interface.");

        _interface.Properties.AddRange(
            _properties.Select(builder => builder
                .WithAccessModifier(AccessModifier.None)
                .Build()));
        _interface.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));

        return _interface;
    }
}