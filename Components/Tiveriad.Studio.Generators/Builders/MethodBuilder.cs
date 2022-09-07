using Optional;
using Tiveriad.Studio.Generators.Models;

namespace Tiveriad.Studio.Generators.Builders
{
    /// <summary>
    /// Provides functionality for building constructors. <see cref="MethodBuilder"/> instances are <b>not</b>
    /// immutable.
    /// </summary>
    public class MethodBuilder  : AbstractBuilder
    {
        private readonly List<AttributeBuilder> _attributeBuilders = new List<AttributeBuilder>();
        private readonly List<ParameterBuilder> _parameters = new ();
        
        internal MethodBuilder()
        {
        }

        internal Method Method { get; private set; } = new Method(AccessModifier.Public);

        /// <summary>
        /// Sets the access modifier of the constructor being built.
        /// </summary>
        public MethodBuilder WithAccessModifier(AccessModifier accessModifier)
        {
            Method.Set(accessModifier: Option.Some(accessModifier));
            return this;
        }
        
        /// <summary>
        /// Sets the name of the method being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="name"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified <paramref name="name"/> is empty or invalid.
        /// </exception>
        public MethodBuilder WithName(string name)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The method name must be a valid, non-empty string.", nameof(name));

            Method.Set(name: Option.Some(name));
            return this;
        }
        
        public MethodBuilder WithAttribute(AttributeBuilder builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            _attributeBuilders.Add(builder);
            return this;
        }
        
        public MethodBuilder WithAttributes(params AttributeBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));
            
            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the attribute builders is null.");

            _attributeBuilders.AddRange(builders);
            return this;
        }
        
        
        /// <summary>
        /// Adds a bunch of parameters to the method being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public MethodBuilder WithParameters(params ParameterBuilder[] builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the parameter builders is null.");

            _parameters.AddRange(builders);
            return this;
        }

        /// <summary>
        /// Adds a bunch of parameters to the method being built.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the specified <paramref name="builders"/> is <c>null</c>.
        /// </exception>
        public MethodBuilder WithParameters(IEnumerable<ParameterBuilder> builders)
        {
            if (builders is null)
                throw new ArgumentNullException(nameof(builders));

            if (builders.Any(x => x is null))
                throw new ArgumentException("One of the parameter builders is null.");

            _parameters.AddRange(builders);
            return this;
        }
        

        /// <summary>
        /// Sets the <c>base()</c> call of the constructor with the specified <paramref name="passedParameter" />.
        /// </summary>
        /// <param name="passedParameter">
        /// The parameter that will be passed to the <c>base()</c> call.
        /// </param>
        /// <example>
        /// This example shows the generated code for a constructor with a base call.
        ///
        /// <code>
        /// // ConstructorBuilder.WithBaseCall();
        /// public User(): base()
        /// {
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a constructor with a base call with passed parameters.
        ///
        /// <code>
        /// // ConstructorBuilder.WithParameter("string", "username").WithBaseCall("username");
        /// public User(string username): base(username)
        /// {
        /// }
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="passedParameter"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The specified he <paramref name="passedParameter"/> value is empty or invalid.
        /// </exception>
        public MethodBuilder WithBaseCall(string passedParameter)
        {
            if (passedParameter is null)
                throw new ArgumentNullException(nameof(passedParameter));

            if (string.IsNullOrWhiteSpace(passedParameter))
                throw new ArgumentException("The base call parameter must be a valid parameter name.", nameof(passedParameter));
            
            var items = new List<string> { passedParameter };
            Method.Set(baseCallParameters: Option.Some(items));
            return this;
        }

        /// <summary>
        /// Sets the <c>base()</c> call of the constructor with the specified <paramref name="passedParameters" />.
        /// </summary>
        /// <param name="passedParameters">
        /// The parameters that will be passed to the <c>base()</c> call.
        /// </param>
        /// <example>
        /// This example shows the generated code for a constructor with a base call.
        ///
        /// <code>
        /// // ConstructorBuilder.WithBaseCall();
        /// public User(): base()
        /// {
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// This example shows the generated code for a constructor with a base call with passed parameters.
        ///
        /// <code>
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
        /// One of the <paramref name="passedParameters"/> values is <c>null</c>.
        /// </exception>
        public MethodBuilder WithBaseCall(params string[] passedParameters)
        {
            if (passedParameters.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException($"One of the {nameof(passedParameters)} parameter values was null.");
            var items = new List<string>();
            items.AddRange(passedParameters);
            Method.Set(baseCallParameters: Option.Some(items));
            return this;
        }

        /// <summary>
        /// Adds XML summary documentation to the constructor.
        /// </summary>
        /// <param name="summary">
        /// The content of the summary documentation.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The specified <paramref name="summary"/> is <c>null</c>.
        /// </exception>
        public MethodBuilder WithSummary(string summary)
        {
            if (summary is null)
                throw new ArgumentNullException(nameof(summary));

            Method.Set(summary: Option.Some(summary));
            return this;
        }
        
        public MethodBuilder WithBody(string body)
        {
            if (body is null)
                throw new ArgumentNullException(nameof(body));

            Method.Set(body: Option.Some(body));
            return this;
        }


        public MethodBuilder MakeStatic(bool makeStatic)
        {
            Method.Set(isStatic: Option.Some(makeStatic));
            return this;
        }
        
        public MethodBuilder MakeAsync(bool makeAsync)
        {
            Method.Set(isAsync: Option.Some(makeAsync));
            return this;
        }
        
        public MethodBuilder MakeConstructor(bool makeConstructor)
        {
            Method.Set(isConstructor: Option.Some(makeConstructor));
            return this;
        }

        public MethodBuilder WithReturnType(InternalType returnType)
        {
            Method.Set(returnType: Option.Some(returnType));
            return this;
        }
        
        internal MethodBuilder WithParent(Class parent)
        {
            Method.Set(parent: Option.Some(parent));
            return this;
        }

        public Method Build()
        {
            if (Method.IsStatic)
            {
                if (Method.AccessModifier != AccessModifier.None)
                    throw new SyntaxException("Access modifiers are not allowed on static method. (CS0515)");

                if (Method.Parameters.Any())
                    throw new SyntaxException("Parameters are not allowed on static method. (CS0132)");

                if (Method.BaseCallParameters.Any())
                    throw new SyntaxException("Static constructors cannot call base constructors. (CS0514)");
            }

            if (!Method.IsConstructor)
            {
                if (Method.BaseCallParameters.Any())
                    throw new SyntaxException("No base call parameters on none constructor method.");
            }
            
            Method.Parameters.AddRange(_parameters.Select(builder => builder.Build()));
            Method.Attributes.AddRange(_attributeBuilders.Select(builder => builder.Build()));
            return Method;
        }
    }
}
