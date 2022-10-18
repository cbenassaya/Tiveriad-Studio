using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building class structures. <see cref="RecordCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class RecordCodeBuilder : ICodeBuilder
{
    private readonly List<MethodCodeBuilder> _constructors = new();
    private readonly List<FieldCodeBuilder> _fields = new();
    private readonly List<InternalTypeCodeBuilder> _implementedInterfaces = new();
    private readonly List<ParameterCodeBuilder> _parameters = new();
    private readonly List<PropertyCodeBuilder> _properties = new();


    private readonly Record _record = new(AccessModifier.Public);
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();


    /// <summary>
    ///     Set the stereotype
    /// </summary>
    public RecordCodeBuilder WithStereotype(string value)
    {
        _record.Set(stereotype: value);
        return this;
    }
    
    /// <summary>
    ///     Sets the reference of the XType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// </exception>
    
    public RecordCodeBuilder WithReference(XType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        _record.Set(reference: @type);
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the class being built.
    /// </summary>
    public RecordCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _record.Set(accessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the name of the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public RecordCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The class name must be a valid, non-empty string.", nameof(name));

        _record.Set(name: name);
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the record being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public RecordCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

        _record.Set(@namespace: @namespace);
        return this;
    }

    /// <summary>
    ///     Adds an interface to the list of interfaces that the class being built implements.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithImplementedInterface(InternalTypeCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _implementedInterfaces.Add(codeBuilder);

        return this;
    }

    /// <summary>
    ///     Adds a bunch of implemented interfaces to the record being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithImplementedInterfaces(params InternalTypeCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _implementedInterfaces.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of implemented interfaces to the record being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithImplementedInterfaces(IEnumerable<InternalTypeCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the internal type builders is null.");

        _implementedInterfaces.AddRange(builders);
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
    public RecordCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _record.Set(summary: summary);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _typeParameters.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of type parameters to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a parameter to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithParameter(ParameterCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _parameters.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of parameters to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithParameters(params ParameterCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the parameter builders is null.");

        _parameters.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of parameters to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithParameters(IEnumerable<ParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the parameter builders is null.");

        _parameters.AddRange(builders);
        return this;
    }


    /// <summary>
    ///     Adds a field to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithField(FieldCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _fields.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of fields to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithFields(params FieldCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the field builders is null.");

        _fields.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a property to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithProperty(PropertyCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _properties.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithProperties(params PropertyCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the property builders is null.");

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of properties to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithProperties(IEnumerable<PropertyCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the property builders is null.");

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a constructor to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public RecordCodeBuilder WithConstructor(MethodCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _constructors.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Sets whether the class being built should be static or not.
    /// </summary>
    /// <param name="makeStatic">
    ///     Indicates whether the class should be static or not.
    /// </param>
    public RecordCodeBuilder MakeStatic(bool makeStatic = true)
    {
        _record.Set(isStatic: makeStatic);
        return this;
    }

    /// <summary>
    ///     Checks whether the described member exists in the class structure.
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
                .Any(x => x.Build().Name.Equals(name, comparison));

        if (memberType == MemberType.Property)
            return _properties
                .Where(x => !accessModifier.HasValue || x.Build().AccessModifier == accessModifier)
                .Any(x => x.Build().Name.Equals(name, comparison));

        if (!memberType.HasValue)
            // Lookup all possible members
            return HasMember(name, MemberType.Field, accessModifier, comparison) ||
                   HasMember(name, MemberType.Property, accessModifier, comparison);

        return false;
    }


    public Record Build()
    {
        if (string.IsNullOrWhiteSpace(_record.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the class is required when building a class.");
        if (_record.IsStatic && _constructors.Count > 1)
            throw new SyntaxException("Static classes can have only 1 constructor.");

        _record.Fields = _fields.Select(builder => builder.Build()).ToList();
        _record.Properties = _properties.Select(builder => builder.Build()).ToList();
        _record.TypeParameters = _typeParameters.Select(builder => builder.Build()).ToList();
        _record.ImplementedInterfaces = _implementedInterfaces.Select(builder => builder.Build()).ToList();
        _record.Parameters = _parameters.Select(builder => builder.Build()).ToList();

        return _record;
    }
}