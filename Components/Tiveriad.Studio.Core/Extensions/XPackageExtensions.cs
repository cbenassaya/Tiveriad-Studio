using Tiveriad.Studio.Core.Entities;

namespace Tiveriad.Studio.Core.Extensions;

public static class XPackageExtensions
{
    public static string GetNamespace(this XPackage package)
    {
        if (package.Parent != null)
            return $"{package.Parent.GetNamespace()}.{package.Name}";
        return
            $"{package?.Module?.Project?.RootNamespace ?? string.Empty}.{package?.Module?.Name ?? string.Empty}.{package.Name}";
    }

    public static string GetPartialNamespace(this XPackage package)
    {
        if (package.Parent != null)
            return $"{package.Parent.GetPartialNamespace()}.{package.Name}";
        return
            $"{package.Name}";
    }

    public static XModule GetModule(this XPackage package)
    {
        if (package.Parent != null)
            return package.Parent.GetModule();
        return
            package.Module;
    }

    public static XProject GetProject(this XPackage package)
    {
        if (package.Parent != null)
            return package.Parent.GetProject();
        return package?.Module?.Project;
    }

    public static List<XEntity> GetEntities(this XPackage package)
    {
        return package.Entities ?? new List<XEntity>();
    }

    public static List<XEnum> GetEnums(this XPackage package)
    {
        return package.Enums ?? new List<XEnum>();
    }

    public static List<XAction> GetActions(this XPackage package)
    {
        return package.Actions ?? new List<XAction>();
    }

    public static void Add(this XPackage package, XAction xAction)
    {
        if (package.Actions == null)
            package.Actions = new List<XAction>();

        package.Actions.Add(xAction);
    }

    public static List<XEndPoint> GetEndPoints(this XPackage package)
    {
        return package.EndPoints ?? new List<XEndPoint>();
    }

    public static void Add(this XPackage package, XEndPoint xEndPoint)
    {
        if (package.EndPoints == null)
            package.EndPoints = new List<XEndPoint>();

        package.EndPoints.Add(xEndPoint);
    }

    public static List<XContract> GetContracts(this XPackage package)
    {
        return package.Contracts ?? new List<XContract>();
    }

    public static void Add(this XPackage package, XContract xContract)
    {
        if (package.Contracts == null)
            package.Contracts = new List<XContract>();

        package.Contracts.Add(xContract);
    }

    public static List<XPackage> GetPackages(this XPackage package)
    {
        return package.Packages ?? new List<XPackage>();
    }

    public static void Add(this XPackage package, XPackage xPackage)
    {
        if (package.Packages == null)
            package.Packages = new List<XPackage>();

        package.Packages.Add(xPackage);
    }
}