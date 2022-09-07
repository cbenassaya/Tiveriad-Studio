using System;

namespace Tiveriad.Studio.Core.Exceptions;

public class TiveriadStudioException : Exception
{
    public TiveriadStudioException(string message) : base(message)
    {
        Error = TiveriadStudioError.BUSINESS_ERROR(message);
    }

    public TiveriadStudioException(TiveriadStudioError error) : base(error.ToString())
    {
        Error = error;
    }

    public TiveriadStudioError Error { get; }
}