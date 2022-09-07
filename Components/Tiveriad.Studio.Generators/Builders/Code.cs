using Optional.Unsafe;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for creating various source code builders.
    /// </summary>
    public static class Code
    {

        /// <summary>
        /// Creates a new <see cref="InternalTypeBuilder"/> instance for building InternalType.
        /// </summary>
        public static InternalTypeBuilder CreateInternalType() => new();
        
        /// <summary>
        /// Creates a new <see cref="InternalTypeBuilder"/> instance for building InternalType.
        /// </summary>
        public static InternalTypeBuilder CreateInternalType(string name,string @namespace) =>
            new InternalTypeBuilder().WithName(name).WithNamespace(@namespace);
        
        /// <summary>
        /// Creates a new <see cref="InternalTypeBuilder"/> instance for building InternalType.
        /// </summary>
        public static InternalTypeBuilder CreateInternalType(string name,string @namespace, XType reference ) =>
            new InternalTypeBuilder().WithName(name).WithNamespace(@namespace).WithReference(reference);
        
        /// <summary>
        /// Creates a new <see cref="InternalTypeBuilder"/> instance for building InternalType.
        /// </summary>
        public static InternalTypeBuilder CreateInternalType(InternalType internalType)
            =>
                new InternalTypeBuilder()
                    .WithName(internalType.Name.ValueOrFailure())
                    .WithNamespace(internalType.Namespace.ValueOrFailure());
        
        /// <summary>
        /// Creates a new <see cref="EnumBuilder"/> instance for building enums.
        /// </summary>
        public static EnumBuilder CreateEnum() =>
            new EnumBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="EnumBuilder"/> instance for building enums. Configures the
        /// <see cref="EnumBuilder"/> instance with the specified <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the enum.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the enum.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static EnumBuilder CreateEnum(string name, AccessModifier accessModifier = AccessModifier.Public) =>
            new EnumBuilder().WithName(name).WithAccessModifier(accessModifier);

        /// <summary>
        /// Creates a new <see cref="EnumMemberBuilder"/> instance for building enum members.
        /// </summary>
        public static EnumMemberBuilder CreateEnumMember() =>
            new EnumMemberBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="EnumMemberBuilder"/> instance for building enum members. Configures
        /// the <see cref="EnumMemberBuilder"/> instance with the specified <paramref name="name"/> and
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the enum member.
        /// </param>
        /// <param name="value">
        /// Optional. Explicitly specifies the value of the enum member. <b>Note</b> that if the enum is marked as
        /// flags, via <see cref="EnumBuilder.MakeFlagsEnum(bool)"/>, you need to explicitly provide the values of all
        /// members to ensure correct functionality. Flags enums for which no member has an explicit value will
        /// auto-generate appropriate values for each member.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static EnumMemberBuilder CreateEnumMember(string name, int? value = null) =>
            new EnumMemberBuilder().WithName(name).WithValue(value);

        /// <summary>
        /// Creates a new <see cref="InterfaceBuilder"/> instance for building interface structures.
        /// </summary>
        public static InterfaceBuilder CreateInterface() =>
            new InterfaceBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="InterfaceBuilder"/> instance for building interface structures.
        /// Configures the <see cref="InterfaceBuilder"/> with the specified <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the interface. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the interface.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static InterfaceBuilder CreateInterface(
            string name,
            AccessModifier accessModifier = AccessModifier.Public) =>
            new InterfaceBuilder().WithName(name).WithAccessModifier(accessModifier);

        /// <summary>
        /// Creates a new <see cref="ClassBuilder"/> instance for building class structures.
        /// </summary>
        public static ClassBuilder CreateClass() =>
            new ClassBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="ClassBuilder"/> instance for building class structures. Configures
        /// the <see cref="ClassBuilder"/> with the specified <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the class. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the class.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static ClassBuilder CreateClass(string name, AccessModifier accessModifier = AccessModifier.Public) =>
            new ClassBuilder().WithName(name).WithAccessModifier(accessModifier);
        
        
        /// <summary>
        /// Creates a new pre-configured <see cref="RecordBuilder"/> instance for building record structures. Configures
        /// the <see cref="RecordBuilder"/> with the specified <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the class. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the class.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static RecordBuilder CreateRecord(string name, AccessModifier accessModifier = AccessModifier.Public) =>
            new RecordBuilder().WithName(name).WithAccessModifier(accessModifier);

        /// <summary>
        /// Creates a new <see cref="StructBuilder"/> instance for building structs.
        /// </summary>
        public static StructBuilder CreateStruct() =>
            new StructBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="StructBuilder"/> instance for building structs. Configures the
        /// <see cref="StructBuilder"/> instance with the specified <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the struct.
        /// </param>
        /// <param name="accessModifier">
        /// The access modifier of the struct.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static StructBuilder CreateStruct(string name, AccessModifier accessModifier = AccessModifier.Public) =>
            new StructBuilder().WithName(name).WithAccessModifier(accessModifier);

        
        /// <summary>
        /// Creates a new <see cref="ParameterBuilder"/> instance for building parameters.
        /// </summary>
        public static ParameterBuilder CreateParameter() =>
            new ParameterBuilder();
        
        /// <summary>
        /// Creates a new <see cref="ParameterBuilder"/> instance for building parameters.
        /// </summary>
        public static ParameterBuilder CreateParameter(
            InternalType type,
            string name) =>
            new ParameterBuilder().WithType(type).WithName(name);
        
        /// <summary>
        /// Creates a new <see cref="FieldBuilder"/> instance for building fields.
        /// </summary>
        public static FieldBuilder CreateField() =>
            new FieldBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="FieldBuilder"/> instance for building fields. Configures the
        /// <see cref="FieldBuilder"/> with the specified <paramref name="type"/>, <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the field, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
        /// </param>
        /// <param name="name">
        /// The name of the field. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access of modifier of the field.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="type"/> or <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="type"/> or <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static FieldBuilder CreateField(
            InternalType type,
            string name,
            AccessModifier accessModifier = AccessModifier.Private) =>
            new FieldBuilder().WithType(type).WithName(name).WithAccessModifier(accessModifier);
        

        /// <summary>
        /// Creates a new <see cref="MethodBuilder"/> instance for building constructors.
        /// </summary>
        public static MethodBuilder CreateMethod(AccessModifier accessModifier = AccessModifier.Public) =>
            new MethodBuilder().WithAccessModifier(accessModifier);

        /// <summary>
        /// Creates a new <see cref="PropertyBuilder"/> instance for building properties.
        /// </summary>
        public static PropertyBuilder CreateProperty() =>
            new PropertyBuilder();

        /// <summary>
        /// Creates a new <see cref="AttributeBuilder"/> instance for building properties.
        /// </summary>
        public static AttributeBuilder CreateAttribute() =>
            new AttributeBuilder();
        
        
        /// <summary>
        /// Creates a new <see cref="AttributeArgumentBuilder"/> instance for building properties.
        /// </summary>
        public static AttributeArgumentBuilder CreateAttributeArgument() =>
            new AttributeArgumentBuilder();
        /// <summary>
        /// Creates a new pre-configured <see cref="PropertyBuilder"/> instance for building properties. Configures the
        /// <see cref="PropertyBuilder"/> with the specified <paramref name="type"/>, <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the property, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
        /// </param>
        /// <param name="name">
        /// The name of the property. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access of modifier of the property.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="type"/> or <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="type"/> or <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static PropertyBuilder CreateProperty(
            InternalType type,
            string name,
            AccessModifier accessModifier = AccessModifier.Public) =>
            new PropertyBuilder().WithType(type).WithName(name).WithAccessModifier(accessModifier);

        /// <summary>
        /// Creates a new pre-configured <see cref="PropertyBuilder"/> instance for building properties. Configures the
        /// <see cref="PropertyBuilder"/> with the specified <paramref name="type"/>, <paramref name="name"/> and
        /// <paramref name="accessModifier"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the property, eg. <c>int</c>, <c>string</c>, <c>User</c>. Used as-is.
        /// </param>
        /// <param name="name">
        /// The name of the property. Used as-is.
        /// </param>
        /// <param name="accessModifier">
        /// The access of modifier of the property.
        /// </param>
        /// <param name="setterAccessModifier">
        /// The access of modifier of the property setter.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="type"/> or <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="type"/> or <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static PropertyBuilder CreateProperty(
            InternalType type,
            string name,
            AccessModifier accessModifier,
            AccessModifier setterAccessModifier) =>
            new PropertyBuilder()
                .WithType(type)
                .WithName(name)
                .WithAccessModifier(accessModifier, setterAccessModifier);


        /// <summary>
        /// Creates a new <see cref="TypeParameterBuilder"/> instance for building type parameters.
        /// </summary>
        public static TypeParameterBuilder CreateTypeParameter() =>
            new TypeParameterBuilder();

        /// <summary>
        /// Creates a new pre-configured <see cref="TypeParameterBuilder"/> instance for building type parameters.
        /// Configures the <see cref="TypeParameterBuilder"/> instance with the
        /// specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the type parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static TypeParameterBuilder CreateTypeParameter(string name) =>
            new TypeParameterBuilder().WithName(name);

        /// <summary>
        /// Creates a new pre-configured <see cref="TypeParameterBuilder"/> instance for building type parameters.
        /// Configures the <see cref="TypeParameterBuilder"/> instance with the
        /// specified <paramref name="name"/> and <paramref name="constraints"/>.
        /// </summary>
        /// <param name="name">The name of the type parameter.</param>
        /// <param name="constraints">The constraints of the type parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static TypeParameterBuilder CreateTypeParameter(string name, params string[] constraints) =>
            new TypeParameterBuilder().WithName(name).WithConstraints(constraints);

        /// <summary>
        /// Creates a new pre-configured <see cref="TypeParameterBuilder"/> instance for building type parameters.
        /// Configures the <see cref="TypeParameterBuilder"/> instance with the
        /// specified <paramref name="name"/> and <paramref name="constraints"/>.
        /// </summary>
        /// <param name="name">The name of the type parameter.</param>
        /// <param name="constraints">The constraints of the type parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// If the specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public static TypeParameterBuilder CreateTypeParameter(string name, IEnumerable<string> constraints) =>
            new TypeParameterBuilder().WithName(name).WithConstraints(constraints);


    }
}
