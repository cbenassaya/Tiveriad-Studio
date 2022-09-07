namespace Tiveriad.Studio.Core.Exceptions;

public class TiveriadStudioError
{
    private TiveriadStudioError(string code, string label)
    {
        Code = code;
        Label = label;
    }

    public string Code { get; }

    public string Label { get; }

    public static TiveriadStudioError BUSINESS_ERROR(string message)
    {
        return new TiveriadStudioError("BUSINESS", message);
    }

    public override string ToString()
    {
        return $"{Code} - {Label}";
    }
}