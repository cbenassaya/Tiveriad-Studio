using System.Collections;
using System.Text;
using Tiveriad.Commons.Extensions;

namespace Tiveriad.Studio.Generators.Sources;

public class CodeBuilder
{
    private readonly StringBuilder _builder = new();
    private string _indent;

    private CodeBuilder()
    {
        _indent = string.Empty;
    }

    public static CodeBuilder Instance()
    {
        return new CodeBuilder();
    }

    private bool IsNewLine()
    {
        if (_builder.Length == 0) return true;
        if (_builder.Length < Separator.NewLine.ToString().Length) return false;
        if (_builder.ToString().EndsWith(Separator.NewLine.ToString())) return true;
        return false;
    }

    public CodeBuilder Append(string value)
    {
        if (IsNewLine() && value != Separator.NewLine.ToString())
            _builder.Append(_indent);
        _builder.Append(value);
        return this;
    }

    public CodeBuilder SetIndent(int value)
    {
        _indent = string.Concat(Enumerable.Repeat("\t", value));
        return this;
    }

    public CodeBuilder Append<TModel>(IEnumerable<TModel> models, Func<TModel, string> code, Separator separator)
    {
        var sep = separator ?? Separator.EmptySpace;
        var builder = new StringBuilder();
        var separatorString = sep.ToString();
        if (sep.ToString().EndsWith(Separator.NewLine.ToString()))
            separatorString = separatorString + _indent;
        builder.AppendJoin(separatorString, models.Select(code).ToList());
        Append(builder.ToString());
        return this;
    }

    public CodeBuilder AppendNewLine(string value = "")
    {
        Append(Separator.NewLine.ToString());
        if (!string.IsNullOrEmpty(value))
            Append(value);
        return this;
    }

    public IfCodeBuilderUnit<TModel> If<TModel>(Func<TModel, bool> condition)
    {
        var unit = new IfCodeBuilderUnit<TModel>(condition, this);

        return unit;
    }

    public IfCodeBuilderUnit If(Func<bool> condition)
    {
        var unit = new IfCodeBuilderUnit(condition, this);

        return unit;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }

    public class IfCodeBuilderUnit
    {
        private readonly CodeBuilder _codeBuilder;
        private readonly Func<bool> _condition;

        public IfCodeBuilderUnit(Func<bool> condition, CodeBuilder codeBuilder)
        {
            _condition = condition;
            _codeBuilder = codeBuilder;
        }

        public CodeBuilder Append(string code)
        {
            if (_condition()) _codeBuilder.Append(code);

            return _codeBuilder;
        }

        public CodeBuilder Append(Func<string> code)
        {
            if (_condition()) _codeBuilder.Append(code());

            return _codeBuilder;
        }
        
        public CodeBuilder Append(IEnumerable models, Func<string, string> code, Separator separator = null)
        {
            if (_condition())
            {
                var sep = separator ?? Separator.EmptySpace;
                var builder = new StringBuilder();
                var separatorString = sep.ToString();
                if (sep.ToString().EndsWith(Separator.NewLine.ToString()))
                    separatorString = separatorString + _codeBuilder._indent;

                builder.AppendJoin(separatorString,
                    models.AsEnumerableItems().Select(x => code(x.ToString())).ToList());
                _codeBuilder.Append(builder.ToString());
            }

            return _codeBuilder;
        }
    }

    public class IfCodeBuilderUnit<TModel>
    {
        private readonly CodeBuilder _codeBuilder;
        private readonly Func<TModel, bool> _condition;

        public IfCodeBuilderUnit(Func<TModel, bool> condition, CodeBuilder codeBuilder)
        {
            _condition = condition;
            _codeBuilder = codeBuilder;
        }

        public CodeBuilder Append(TModel model, Func<TModel, string> code, Separator separator = null)
        {
            if (_condition(model)) _codeBuilder.Append(code(model));

            return _codeBuilder;
        }

        public CodeBuilder Append(IEnumerable<TModel> models, Func<TModel, string> code, Separator separator = null)
        {
            var sep = separator ?? Separator.EmptySpace;
            var builder = new StringBuilder();
            var separatorString = sep.ToString();
            if (sep.ToString().EndsWith(Separator.NewLine.ToString()))
                separatorString = separatorString + _codeBuilder._indent;
            ;
            builder.AppendJoin(separatorString, models.Where(_condition).Select(code).ToList());
            _codeBuilder.Append(builder.ToString());
            return _codeBuilder;
        }
    }

    public class Separator
    {
        public static Separator WhiteSpace = new(" ");
        public static Separator Comma = new(",");
        public static Separator EmptySpace = new("");
        public static Separator Semicolon = new(";");
        public static Separator NewLine = new(Environment.NewLine);
        public static Separator And = new(" && ");
        public static Separator Or = new(" || ");

        private readonly string _separator;


        private Separator(string separator)
        {
            _separator = separator;
        }

        protected bool Equals(Separator other)
        {
            return _separator == other._separator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Separator)obj);
        }

        public override int GetHashCode()
        {
            return _separator != null ? _separator.GetHashCode() : 0;
        }

        public static Separator Combine(Separator separator1, Separator separator2)
        {
            return new Separator(separator1._separator + separator2._separator);
        }

        public override string ToString()
        {
            return _separator;
        }
    }
}