using Tiveriad.Commons.Extensions;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionalty for building enum structures. <see cref="EnumCodeBuilder" /> instances are <b>not</b>
///     immutable.
/// </summary>
public class EnumCodeBuilder : ICodeBuilder
{
    private readonly List<EnumMemberCodeBuilder> _members = new();

    private Enumeration _enumeration = new(AccessModifier.Public);

    /// <summary>
    ///     Set the stereotype
    /// </summary>
    public EnumCodeBuilder WithStereotype(string value)
    {
        _enumeration.Set(stereotype: value);
        return this;
    }

    /// <summary>
    ///     Sets the access modifier of the enum being built.
    /// </summary>
    public EnumCodeBuilder WithAccessModifier(AccessModifier accessModifier)
    {
        _enumeration = _enumeration.Set(accessModifier);
        return this;
    }

    /// <summary>
    ///     Sets the name of the enum being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public EnumCodeBuilder WithName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("The enum name must be a valid, non-empty string.", nameof(name));

        _enumeration = _enumeration.Set(name: name);
        return this;
    }

    /// <summary>
    ///     Sets the namespace of the enumeration being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="namespace" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="namespace" /> is empty or invalid.
    /// </exception>
    public EnumCodeBuilder WithNamespace(string @namespace)
    {
        if (@namespace is null)
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentException("The namespace must be a valid, non-empty string.", nameof(@namespace));

        _enumeration.Set(@namespace: @namespace);
        return this;
    }
    
    /// <summary>
    ///     Sets the reference of the XType being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="type" /> is <c>null</c>.
    /// </exception>
    /// </exception>
    
    public EnumCodeBuilder WithReference(XType type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        _enumeration.Set(reference: @type);
        return this;
    }

    /// <summary>
    ///     Adds a member to the enum being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="codeBuilder" /> is <c>null</c>.
    /// </exception>
    public EnumCodeBuilder WithMember(EnumMemberCodeBuilder codeBuilder)
    {
        if (codeBuilder is null)
            throw new ArgumentNullException(nameof(codeBuilder));

        _members.Add(codeBuilder);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of members to the enum being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public EnumCodeBuilder WithMembers(params EnumMemberCodeBuilder[] builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _members.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Adds a bunch of members to the enum being built.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     One of the specified <paramref name="builders" /> is <c>null</c>.
    /// </exception>
    public EnumCodeBuilder WithMembers(IEnumerable<EnumMemberCodeBuilder> builders)
    {
        if (builders is null)
            throw new ArgumentNullException(nameof(builders));

        if (builders.Any(x => x is null))
            throw new ArgumentException("One of the builders is null.");

        _members.AddRange(builders);
        return this;
    }

    /// <summary>
    ///     Specifies whether the enum being built represents a set of flags or not. A flags enum will be marked with
    ///     the <see cref="FlagsAttribute" /> in the generated source code. The members of a flags enum will be assigned
    ///     appropriate, auto-generated values. <b>Note</b> that explicitly set values will be overwritten.
    /// </summary>
    /// <param name="makeFlagsEnum">
    ///     Indicates whether the enum represents a set of flags.
    /// </param>
    public EnumCodeBuilder MakeFlagsEnum(bool makeFlagsEnum = true)
    {
        _enumeration = _enumeration.Set(isFlag: makeFlagsEnum);
        return this;
    }

    /// <summary>
    ///     Adds XML summary documentation to the enum.
    /// </summary>
    /// <param name="summary">
    ///     The content of the summary documentation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="summary" /> is <c>null</c>.
    /// </exception>
    public EnumCodeBuilder WithSummary(string summary)
    {
        if (summary is null)
            throw new ArgumentNullException(nameof(summary));

        _enumeration = _enumeration.Set(summary: summary);
        return this;
    }

    /// <summary>
    ///     Checks whether the described member exists in the enum structure.
    /// </summary>
    /// <param name="name">
    ///     The name of the member.
    /// </param>
    /// <param name="comparison">
    ///     The comparision type to be performed when comparing the described name against the names of the actual
    ///     members. By default casing is ignored.
    /// </param>
    public bool HasMember(
        string name,
        StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        return _members.Any(x => x.Build().Name.Equals(name, comparison));
    }


    public Enumeration Build()
    {
        if (string.IsNullOrWhiteSpace(_enumeration.Name))
            throw new MissingBuilderSettingException(
                "Providing the name of the enum is required when building an enum.");

        if (_enumeration.IsFlag && _members.All(x => !x.Build().Value.HasValue))
            for (var i = 0; i < _members.Count; i++)
                _members[i] = _members[i].WithValue(i == 0 ? 0 : (int)Math.Pow(2, i - 1));

        _enumeration.Members.AddRange(_members.Select(x => x.Build()));

        return _enumeration;
    }
}