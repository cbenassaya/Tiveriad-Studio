namespace Tiveriad.Studio.Generators.Services;

public interface IExportSourceItem
{
    bool Export(string content, string rootDirectory, string pathDirectory, string fileName, bool replaceIfExist);
}