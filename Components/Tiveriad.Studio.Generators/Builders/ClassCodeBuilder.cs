using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for building class structures. <see cref="ClassCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class ClassCodeBuilder : ICodeBuilder
{
    private readonly List<AttributeBuilder> _attributeBuilders = new();
    private readonly List<FieldCodeBuilder> _fields = new();
    private readonly List<InternalTypeCodeBuilder> _implementedInterfaces = new();
    private readonly List<MethodCodeBuilder> _methods = new();
    private readonly List<PropertyCodeBuilder> _properties = new();
    private readonly List<TypeParameterCodeBuilder> _typeParameters = new();

    private Class _class = new(AccessModifier.Public);

    /// <summary>
    ///     Set the stereotype
    /// </summary>
    public ClassCodeBuilder WithStereotype(string value)
    {
        _class.Set(stereotype: value);
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the class being built.
    /// </summary>
    public ClassCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _class.Set(accessModifier);
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
    public ClassCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The class name must be a valid, non-empty string.", nameof(name));

        _class.Set(name: name);
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public ClassCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

        _class.Set(@namespace: @namespace);
        return this;
    }

    /// <summary>
    ///     Sets the class that the class being built inherits from.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="class" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithInheritedClass(InternalType @class)
    {
        if (@class is null)
            throw new ArgumentNullException(nameof(@class));


        _class.Set(inheritedClass: @class);
        return this;
    }

    /// <summary>
    ///     Adds an interface to the list of interfaces that the class being built implements.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithImplementedInterface(InternalTypeCodeBuilder codeBuilder)
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
    public ClassCodeBuilder WithImplementedInterfaces(params InternalTypeCodeBuilder[] builders)
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
    public ClassCodeBuilder WithImplementedInterfaces(IEnumerable<InternalTypeCodeBuilder> builders)
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
    public ClassCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _class = _class.Set(summary: summary);
        return this;
    }
    
    /// <summary>
    ///     Sets the reference of the XType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// </exception>
    
    public ClassCodeBuilder WithReference(XType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        _class.Set(reference: @type);
        return this;
    }

    /// <summary>
    ///     Adds a type parameter to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithTypeParameter(TypeParameterCodeBuilder codeBuilder)
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
    public ClassCodeBuilder WithTypeParameters(params TypeParameterCodeBuilder[] builders)
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
    public ClassCodeBuilder WithTypeParameters(IEnumerable<TypeParameterCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the type parameter builders is null.");

        _typeParameters.AddRange(builders);
        return this;
    }

    public ClassCodeBuilder WithAttribute(AttributeBuilder builder)
    {
        if (builder is null)
            throw new ArgumentNullException(nameof(builder));

        _attributeBuilders.Add(builder);
        return this;
    }

    public ClassCodeBuilder WithAttributes(params AttributeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the attribute builders is null.");

        _attributeBuilders.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a field to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithField(FieldCodeBuilder codeBuilder)
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
    public ClassCodeBuilder WithFields(params FieldCodeBuilder[] builders)
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
    public ClassCodeBuilder WithProperty(PropertyCodeBuilder codeBuilder)
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
    public ClassCodeBuilder WithProperties(params PropertyCodeBuilder[] builders)
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
    public ClassCodeBuilder WithProperties(IEnumerable<PropertyCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the property builders is null.");

        _properties.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a method to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithMethod(MethodCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _methods.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of methods to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithMethods(params MethodCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the field builders is null.");

        _methods.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of dependencies to the class being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="dependencies" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="dependencies" /> is <c>null</c>.
    /// </exception>
    public ClassCodeBuilder WithDependencies(params string[] dependencies)
    {
        if (dependencies is null)
            throw new ArgumentNullException(nameof(dependencies));

        if (dependencies.Any(x => x is null))
            throw new ArgumentException("One of the dependencies is null.");

        foreach (var dependency in dependencies)
            if (!_class.Dependencies.Contains(dependency))
                _class.Dependencies.Add(dependency);
        return this;
    }
    
    
    public ClassCodeBuilder WithUsing(params InternalType[] usings)
    {
        if (usings is null)
            throw new ArgumentNullException(nameof(usings));

        if (usings.Any(x => x is null))
            throw new ArgumentException("One of the usings is null.");

        foreach (var @using in usings)
            if (!_class.Usings.Contains(@using))
                _class.Usings.Add(@using);
        return this;
    }

    /// <summary>
    ///     Sets whether the class being built should be static or not.
    /// </summary>
    /// <param name="makeStatic">
    ///     Indicates whether the class should be static or not.
    /// </param>
    public ClassCodeBuilder MakeStatic(bool makeStatic = true)
    {
        _class.Set(isStatic: makeStatic);
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

    public Class Build()
    {
        if (string.IsNullOrWhiteSpace(_class.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the class is required when building a class.");
        if (_class.IsStatic && _methods.Count > 1)
            throw new SyntaxException("Static classes can have only 1 constructor.");


        _class.Fields = _fields.Select(builder => builder.Build()).ToList();
        _class.Properties = _properties.Select(builder => builder.Build()).ToList();
        _class.TypeParameters = _typeParameters.Select(builder => builder.Build()).ToList();
        _class.ImplementedInterfaces = _implementedInterfaces.Select(builder => builder.Build()).ToList();
        _class.Attributes = _attributeBuilders.Select(builder => builder.Build()).ToList();
        _class.Methods =
            _methods.Select(builder => builder
                .WithParent(_class)
                .MakeStatic(_class.IsStatic)
                .Build()).ToList();

        return _class;
    }
}