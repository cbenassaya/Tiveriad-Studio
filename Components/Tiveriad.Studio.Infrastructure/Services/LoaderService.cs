using System.Security;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Infrastructure.Services;

public class LoaderService:ILoaderService
{
    public async Task<Stream> GetStreamAsync(string path, CancellationToken cancellationToken = default)
    {
        Stream input;
        if (path.StartsWith("http"))
            try
            {
                using var httpClient = new HttpClient();
                input = await httpClient.GetStreamAsync(path, cancellationToken );
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException(
                    $"Could not download the file at {path}, reason: {ex.Message}", ex);
            }
        else
            try
            {
                input = new FileStream(path, FileMode.Open);
            }
            catch (Exception ex) when (ex is FileNotFoundException ||
                                       ex is PathTooLongException ||
                                       ex is DirectoryNotFoundException ||
                                       ex is IOException ||
                                       ex is UnauthorizedAccessException ||
                                       ex is SecurityException ||
                                       ex is NotSupportedException)
            {
                throw new InvalidOperationException($"Could not open the file at {path}, reason: {ex.Message}", ex);
            }
        return input;
    }
}