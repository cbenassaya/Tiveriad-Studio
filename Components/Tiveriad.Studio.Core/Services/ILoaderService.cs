namespace Tiveriad.Studio.Core.Services;

public interface ILoaderService
{
    public Task<Stream> GetStreamAsync(string path, CancellationToken cancellationToken = default);
}