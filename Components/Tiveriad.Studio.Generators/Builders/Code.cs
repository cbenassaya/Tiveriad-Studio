using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders;

/// <summary>
///     Provides functionality for creating various source code builders.
/// </summary>
public static class Code
{
    /// <summary>
    ///     Creates a new <see cref="InternalTypeCodeBuilder" /> instance for building InternalType.
    /// </summary>
    public static InternalTypeCodeBuilder CreateInternalType()
    {
        return new InternalTypeCodeBuilder();
    }

    /// <summary>
    ///     Creates a new <see cref="InternalTypeCodeBuilder" /> instance for building InternalType.
    /// </summary>
    public static InternalTypeCodeBuilder CreateInternalType(string name, string @namespace)
    {
        return new InternalTypeCodeBuilder().WithName(name).WithNamespace(@namespace);
    }

    /// <summary>
    ///     Creates a new <see cref="InternalTypeCodeBuilder" /> instance for building InternalType.
    /// </summary>
    public static InternalTypeCodeBuilder CreateInternalType(string name, string @namespace, XType reference)
    {
        return new InternalTypeCodeBuilder().WithName(name).WithNamespace(@namespace).WithReference(reference);
    }
    
    public static InternalTypeCodeBuilder CreateInternalType( XType reference)
    {
        return new InternalTypeCodeBuilder().WithName(reference.Name).WithNamespace(reference.Namespace).WithReference(reference);
    }

    /// <summary>
    ///     Creates a new <see cref="InternalTypeCodeBuilder" /> instance for building InternalType.
    /// </summary>
    public static InternalTypeCodeBuilder CreateInternalType(InternalType internalType)
    {
        return new InternalTypeCodeBuilder()
            .WithName(internalType.Name)
            .WithNamespace(internalType.Namespace);
    }

    /// <summary>
    ///     Creates a new <see cref="EnumCodeBuilder" /> instance for building enums.
    /// </summary>
    public static EnumCodeBuilder CreateEnum()
    {
        return new EnumCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="EnumCodeBuilder" /> instance for building enums. Configures the
    ///     <see cref="EnumCodeBuilder" /> instance with the specified <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the enum.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the enum.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static EnumCodeBuilder CreateEnum(string name, AccessModifier accessModifier = AccessModifier.Public)
    {
        return new EnumCodeBuilder().WithName(name).WithAccessModifier(accessModifier);
    }

    /// <summary>
    ///     Creates a new <see cref="EnumMemberCodeBuilder" /> instance for building enum members.
    /// </summary>
    public static EnumMemberCodeBuilder CreateEnumMember()
    {
        return new EnumMemberCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="EnumMemberCodeBuilder" /> instance for building enum members. Configures
    ///     the <see cref="EnumMemberCodeBuilder" /> instance with the specified <paramref name="name" /> and
    ///     <paramref name="value" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the enum member.
    /// </param>
    /// <param name="value">
    ///     Optional. Explicitly specifies the value of the enum member. <b>Note</b> that if the enum is marked as
    ///     flags, via <see cref="EnumCodeBuilder.MakeFlagsEnum(bool)" />, you need to explicitly provide the values of all
    ///     members to ensure correct functionality. Flags enums for which no member has an explicit value will
    ///     auto-generate appropriate values for each member.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static EnumMemberCodeBuilder CreateEnumMember(string name, int? value = null)
    {
        return new EnumMemberCodeBuilder().WithName(name).WithValue(value);
    }

    /// <summary>
    ///     Creates a new <see cref="InterfaceCodeBuilder" /> instance for building interface structures.
    /// </summary>
    public static InterfaceCodeBuilder CreateInterface()
    {
        return new InterfaceCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="InterfaceCodeBuilder" /> instance for building interface structures.
    ///     Configures the <see cref="InterfaceCodeBuilder" /> with the specified <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the interface. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the interface.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static InterfaceCodeBuilder CreateInterface(
        string name,
        AccessModifier accessModifier = AccessModifier.Public)
    {
        return new InterfaceCodeBuilder().WithName(name).WithAccessModifier(accessModifier);
    }

    /// <summary>
    ///     Creates a new <see cref="ClassCodeBuilder" /> instance for building class structures.
    /// </summary>
    public static ClassCodeBuilder CreateClass()
    {
        return new ClassCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="ClassCodeBuilder" /> instance for building class structures. Configures
    ///     the <see cref="ClassCodeBuilder" /> with the specified <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the class. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the class.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static ClassCodeBuilder CreateClass(string name, AccessModifier accessModifier = AccessModifier.Public)
    {
        return new ClassCodeBuilder().WithName(name).WithAccessModifier(accessModifier);
    }


    /// <summary>
    ///     Creates a new pre-configured <see cref="RecordCodeBuilder" /> instance for building record structures. Configures
    ///     the <see cref="RecordCodeBuilder" /> with the specified <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the class. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the class.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static RecordCodeBuilder CreateRecord(string name, AccessModifier accessModifier = AccessModifier.Public)
    {
        return new RecordCodeBuilder().WithName(name).WithAccessModifier(accessModifier);
    }

    /// <summary>
    ///     Creates a new <see cref="StructCodeBuilder" /> instance for building structs.
    /// </summary>
    public static StructCodeBuilder CreateStruct()
    {
        return new StructCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="StructCodeBuilder" /> instance for building structs. Configures the
    ///     <see cref="StructCodeBuilder" /> instance with the specified <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="name">
    ///     The name of the struct.
    /// </param>
    /// <param name="accessModifier">
    ///     The access modifier of the struct.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     The specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static StructCodeBuilder CreateStruct(string name, AccessModifier accessModifier = AccessModifier.Public)
    {
        return new StructCodeBuilder().WithName(name).WithAccessModifier(accessModifier);
    }


    /// <summary>
    ///     Creates a new <see cref="ParameterCodeBuilder" /> instance for building parameters.
    /// </summary>
    public static ParameterCodeBuilder CreateParameter()
    {
        return new ParameterCodeBuilder();
    }

    /// <summary>
    ///     Creates a new <see cref="ParameterCodeBuilder" /> instance for building parameters.
    /// </summary>
    public static ParameterCodeBuilder CreateParameter(
        InternalType type,
        string name)
    {
        return new ParameterCodeBuilder().WithType(type).WithName(name);
    }

    /// <summary>
    ///     Creates a new <see cref="FieldCodeBuilder" /> instance for building fields.
    /// </summary>
    public static FieldCodeBuilder CreateField()
    {
        return new FieldCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="FieldCodeBuilder" /> instance for building fields. Configures the
    ///     <see cref="FieldCodeBuilder" /> with the specified <paramref name="type" />, <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="type">
    ///     The type of the field, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
    /// </param>
    /// <param name="name">
    ///     The name of the field. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access of modifier of the field.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="type" /> or <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> or <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static FieldCodeBuilder CreateField(
        InternalType type,
        string name,
        AccessModifier accessModifier = AccessModifier.Private)
    {
        return new FieldCodeBuilder().WithType(type).WithName(name).WithAccessModifier(accessModifier);
    }


    /// <summary>
    ///     Creates a new <see cref="MethodCodeBuilder" /> instance for building constructors.
    /// </summary>
    public static MethodCodeBuilder CreateMethod(AccessModifier accessModifier = AccessModifier.Public)
    {
        return new MethodCodeBuilder().WithAccessModifier(accessModifier);
    }

    /// <summary>
    ///     Creates a new <see cref="PropertyCodeBuilder" /> instance for building properties.
    /// </summary>
    public static PropertyCodeBuilder CreateProperty()
    {
        return new PropertyCodeBuilder();
    }

    /// <summary>
    ///     Creates a new <see cref="AttributeBuilder" /> instance for building properties.
    /// </summary>
    public static AttributeBuilder CreateAttribute()
    {
        return new AttributeBuilder();
    }


    /// <summary>
    ///     Creates a new <see cref="AttributeArgumentBuilder" /> instance for building properties.
    /// </summary>
    public static AttributeArgumentBuilder CreateAttributeArgument()
    {
        return new AttributeArgumentBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="PropertyCodeBuilder" /> instance for building properties. Configures the
    ///     <see cref="PropertyCodeBuilder" /> with the specified <paramref name="type" />, <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="type">
    ///     The type of the property, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
    /// </param>
    /// <param name="name">
    ///     The name of the property. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access of modifier of the property.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="type" /> or <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> or <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static PropertyCodeBuilder CreateProperty(
        InternalType type,
        string name,
        AccessModifier accessModifier = AccessModifier.Public)
    {
        return new PropertyCodeBuilder().WithType(type).WithName(name).WithAccessModifier(accessModifier);
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="PropertyCodeBuilder" /> instance for building properties. Configures the
    ///     <see cref="PropertyCodeBuilder" /> with the specified <paramref name="type" />, <paramref name="name" /> and
    ///     <paramref name="accessModifier" />.
    /// </summary>
    /// <param name="type">
    ///     The type of the property, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
    /// </param>
    /// <param name="name">
    ///     The name of the property. Used as-is.
    /// </param>
    /// <param name="accessModifier">
    ///     The access of modifier of the property.
    /// </param>
    /// <param name="setterAccessModifier">
    ///     The access of modifier of the property setter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="type" /> or <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="type" /> or <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static PropertyCodeBuilder CreateProperty(
        InternalType type,
        string name,
        AccessModifier accessModifier,
        AccessModifier setterAccessModifier)
    {
        return new PropertyCodeBuilder()
            .WithType(type)
            .WithName(name)
            .WithAccessModifier(accessModifier, setterAccessModifier);
    }


    /// <summary>
    ///     Creates a new <see cref="TypeParameterCodeBuilder" /> instance for building type parameters.
    /// </summary>
    public static TypeParameterCodeBuilder CreateTypeParameter()
    {
        return new TypeParameterCodeBuilder();
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="TypeParameterCodeBuilder" /> instance for building type parameters.
    ///     Configures the <see cref="TypeParameterCodeBuilder" /> instance with the
    ///     specified <paramref name="name" />.
    /// </summary>
    /// <param name="name">The name of the type parameter.</param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static TypeParameterCodeBuilder CreateTypeParameter(string name)
    {
        return new TypeParameterCodeBuilder().WithName(name);
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="TypeParameterCodeBuilder" /> instance for building type parameters.
    ///     Configures the <see cref="TypeParameterCodeBuilder" /> instance with the
    ///     specified <paramref name="name" /> and <paramref name="constraints" />.
    /// </summary>
    /// <param name="name">The name of the type parameter.</param>
    /// <param name="constraints">The constraints of the type parameter.</param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static TypeParameterCodeBuilder CreateTypeParameter(string name, params string[] constraints)
    {
        return new TypeParameterCodeBuilder().WithName(name).WithConstraints(constraints);
    }

    /// <summary>
    ///     Creates a new pre-configured <see cref="TypeParameterCodeBuilder" /> instance for building type parameters.
    ///     Configures the <see cref="TypeParameterCodeBuilder" /> instance with the
    ///     specified <paramref name="name" /> and <paramref name="constraints" />.
    /// </summary>
    /// <param name="name">The name of the type parameter.</param>
    /// <param name="constraints">The constraints of the type parameter.</param>
    /// <exception cref="ArgumentNullException">
    ///     If the specified <paramref name="name" /> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The specified <paramref name="name" /> is empty or invalid.
    /// </exception>
    public static TypeParameterCodeBuilder CreateTypeParameter(string name, IEnumerable<string> constraints)
    {
        return new TypeParameterCodeBuilder().WithName(name).WithConstraints(constraints);
    }
}