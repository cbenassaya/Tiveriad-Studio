using Optional;
using Optional.Collections;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building structs. <see cref="StructCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class StructCodeBuilder : ICodeBuilder
{
    private readonly List<MethodCodeBuilder> _constructors = new();
    private readonly List<FieldCodeBuilder> _fields = new();
    private readonly List<PropertyCodeBuilder> _properties = new();
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();
    private readonly Struct _struct = new(AccessModifier.Public);

    /// <summary>
    ///     Set the stereotype
    /// </summary>
    public StructCodeBuilder WithStereotype(string value)
    {
        _struct.Set(stereotype: Option.Some(value));
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the struct.
    /// </summary>
    /// <param name="accessModifier">
    ///     The access modifier of the struct.
    /// </param>
    public StructCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _struct.Set(Option.Some(accessModifier));
        return this;
    }

    /// <summary>
    ///     Sets the name of the struct.
    /// </summary>
    /// <param name="name">
    ///     The name of the struct.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public StructCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Struct name must be a valid, non-empty string.", nameof(name));

        _struct.Set(name: Option.Some(name));
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the struct being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public StructCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

        _struct.Set(@namespace: Option.Some(@namespace));
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the struct.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _struct.Set(summary: Option.Some(summary));
        return this;
    }

    /// <summary>
    ///     Adds a constructor to the struct.
    /// </summary>
    /// <param name="codeBuilder">
    ///     The configuration of the constructor.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithConstructor(MethodCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _constructors.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a field to the struct.
    /// </summary>
    /// <param name="codeBuilder">
    ///     The configuration of the field.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithField(FieldCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _fields.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of fields to the struct.
    /// </summary>
    /// <param name="builders">
    ///     A collection of fields.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithFields(IEnumerable<FieldCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _fields.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of fields to the struct.
    /// </summary>
    /// <param name="builders">
    ///     A collection of fields.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithFields(params FieldCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _fields.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a property to the struct.
    /// </summary>
    /// <param name="codeBuilder">
    ///     The configuration of the property.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithProperty(PropertyCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _properties.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the struct.
    /// </summary>
    /// <param name="builders">
    ///     A collection of properties.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithProperties(IEnumerable<PropertyCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the struct.
    /// </summary>
    /// <param name="builders">
    ///     A collection of properties.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithProperties(params PropertyCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds an interface to the list of interfaces that the struct implements.
    /// </summary>
    /// <param name="interface">
    ///     The name of the interface that the struct implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interface" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="interface" /> is empty or invalid.
    /// </exception>
    public StructCodeBuilder WithImplementedInterface(Interface @interface)
    {
        if (@interface is null)
            throw new ArgumentNullException(nameof(@interface));

        if (@interface == null)
            throw new ArgumentException("Implemented interface name must be a valid, not null.", nameof(@interface));

        _struct.ImplementedInterfaces.Add(@interface);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of interfaces to the list of interfaces that the struct implements.
    /// </summary>
    /// <param name="interfaces">
    ///     A collection with the names of interfaces that the struct implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interfaces" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="interfaces" /> is <c>null</c> or invalid.
    /// </exception>
    public StructCodeBuilder WithImplementedInterfaces(IEnumerable<Interface> interfaces)
    {
        if (interfaces is null)
            throw new ArgumentNullException(nameof(interfaces));

        if (interfaces.Any(x => x == null))
            throw new ArgumentException("One of the interfaces is null or invalid.");

        _struct.ImplementedInterfaces.AddRange(interfaces);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of interfaces to the list of interfaces that the struct implements.
    /// </summary>
    /// <param name="interfaces">
    ///     A collection with the names of interfaces that the struct implements.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="interfaces" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="interfaces" /> is <c>null</c> or invalid.
    /// </exception>
    public StructCodeBuilder WithImplementedInterfaces(params Interface[] interfaces)
    {
        if (interfaces is null)
            throw new ArgumentNullException(nameof(interfaces));

        if (interfaces.Any(x => x == null))
            throw new ArgumentException("One of the interfaces is null or invalid.");

        _struct.ImplementedInterfaces.AddRange(interfaces);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the struct being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _typeParameters.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the struct being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the struct being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public StructCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Checks whether the described member exists in the struct structure.
    /// </summary>
    /// <param name="name">
    ///     The name of the member.
    /// </param>
    /// <param name="memberType">
    ///     The type of the member. By default all members will be taken into account.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the member. By default all access modifiers will be taken into account.
    /// </param>
    /// <param name="comparison">
    ///     The comparision type to be performed when comparing the described name against the names of the actual
    ///     members. By default casing is ignored.
    /// </param>
    public bool HasMember(
        string name,
        MemberType? memberType = default,
        AccessModifier? accessModifier = default,
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        if (memberType == MemberType.Field)
            return _fields
                .Where(x => !accessModifier.HasValue || x.Build().AccessModifier == accessModifier)
                .Any(x => x.Build().Name.Exists(n => n.Equals(name, comparison)));

        if (memberType == MemberType.Property)
            return _properties
                .Where(x => !accessModifier.HasValue || x.Build().AccessModifier == accessModifier)
                .Any(x => x.Build().Name.Exists(n => n.Equals(name, comparison)));

        if (!memberType.HasValue)
            return HasMember(name, MemberType.Field, accessModifier, comparison) ||
                   HasMember(name, MemberType.Property, accessModifier, comparison);

        return false;
    }

    /// <summary>
    ///     Returns the source code of the built struct.
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
    ///     Returns the source code of the built struct.
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

    public Struct Build()
    {
        var structName = _struct.Name.ValueOr(string.Empty);
        if (string.IsNullOrWhiteSpace(structName))
            throw new MissingBuilderSettingException(
                "Providing the name of the struct is required when building a struct.");

        _struct.Fields.AddRange(_fields.Select(field => field.Build()));
        _struct.Properties.AddRange(_properties.Select(prop => prop.Build()));
        _struct.Properties
            .FirstOrNone(x => x.DefaultValue.HasValue)
            .MatchSome(prop => throw new SyntaxException(
                "Default property values are not allowed in structs. (CS0573)"));

        _struct.TypeParameters.AddRange(_typeParameters.Select(builder => builder.Build()));

        return _struct;
    }
}