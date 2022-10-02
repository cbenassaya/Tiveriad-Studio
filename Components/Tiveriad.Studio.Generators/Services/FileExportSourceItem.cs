using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Services;

public class FileExportSourceItem : IExportSourceItem
{
    public bool Export(string content, string rootDirectory, string pathDirectory, string fileName, bool replaceIfExist)
    {
        var filePath = Path.Combine(GetOrCreateDirectoryInfo(rootDirectory, pathDirectory).FullName,
            fileName);

        var fileIsExist = File.Exists(filePath);
        /*if (fileIsExist && replaceIfExist)
        {
            var oldContent = GetCustomContent(File.ReadAllText(filePath));
            if (!string.IsNullOrEmpty(content))
                content = InsertCustomContent(content, oldContent);
            File.Delete(filePath);
        }*/
        if (fileIsExist)
            File.Delete(filePath);
        //if (!fileIsExist || replaceIfExist)
            File.WriteAllText(filePath, content);

        return true;
    }

    private string GetCustomContent(string content)
    {
        var start = content.IndexOf(CodeConstants.StartBalise, StringComparison.CurrentCulture);

        if (start < 0) return string.Empty;

        var end = content.IndexOf(CodeConstants.EndBalise, StringComparison.CurrentCulture);

        if (end < start + CodeConstants.StartBalise.Length) return string.Empty;

        return content.Substring(start + CodeConstants.StartBalise.Length,
            end - start - CodeConstants.StartBalise.Length);
    }

    private string InsertCustomContent(string content, string oldContent)
    {
        var start = content.IndexOf(CodeConstants.StartBalise, StringComparison.CurrentCulture);

        if (start < 0) return content;

        var end = content.IndexOf(CodeConstants.EndBalise, StringComparison.CurrentCulture);

        if (end < start + CodeConstants.StartBalise.Length) return content;

        return content.Substring(0, start + CodeConstants.StartBalise.Length) + oldContent +
               content.Substring(end, content.Length - end);
    }

    private DirectoryInfo GetOrCreateDirectoryInfo(string rootDirectory, string pathDirectory)
    {
        var directory = new DirectoryInfo(rootDirectory);

        foreach (var temp in pathDirectory.Split(Path.PathSeparator))
        {
            var path = Path.Combine(directory.FullName, temp);
            if (!Directory.Exists(path))
                directory = Directory.CreateDirectory(path);
            else
                directory = new DirectoryInfo(path);
        }

        return directory;
    }
}