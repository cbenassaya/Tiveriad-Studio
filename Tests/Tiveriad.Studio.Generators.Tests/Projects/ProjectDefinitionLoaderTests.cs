using System.Xml.Serialization;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Projects;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Projects;

public class ProjectDefinitionLoaderTests
{
    [Fact]
    public async Task Write_File()
    {
        var projectTemplate = new ProjectDefinitionTemplate
        {
            ComponentDefinitions = new List<ComponentDefinition>
            {
                new()
                {
                    ComponentItems = new List<ComponentItem>
                    {
                        new()
                        {
                            Pattern = "Pattern1",
                            Stereotype = "Stereotype1"
                        },
                        new()
                        {
                            Pattern = "Pattern2",
                            Stereotype = "Stereotype2"
                        }
                    },
                    Dependencies = new List<Dependency>
                    {
                        new()
                        {
                            Include = "Include1",
                            Version = "Version1"
                        },
                        new()
                        {
                            Include = "Include2",
                            Version = "Version2"
                        }
                    },
                    Type = "Type1"
                },
                new()
                {
                    ComponentItems = new List<ComponentItem>
                    {
                        new()
                        {
                            Pattern = "Pattern1",
                            Stereotype = "Stereotype1"
                        },
                        new()
                        {
                            Pattern = "Pattern2",
                            Stereotype = "Stereotype2"
                        }
                    },
                    Dependencies = new List<Dependency>
                    {
                        new()
                        {
                            Include = "Include1",
                            Version = "Version1"
                        },
                        new()
                        {
                            Include = "Include2",
                            Version = "Version2"
                        }
                    },
                    Type = "Type2"
                }
            }
        };

        XmlSerializer serializer = new(typeof(ProjectDefinitionTemplate));
        using (var textWriter = new StringWriter())
        {
            serializer.Serialize(textWriter, projectTemplate);
            var result = textWriter.ToString();
        }
    }


    [Fact]
    public async Task Read_File()
    {
        var xmlSerializer = new XmlSerializer(typeof(ProjectDefinitionTemplate));
        await using var stream = new FileStream("Samples/ProjectGenerated.xml", FileMode.Open);
        var project = xmlSerializer.Deserialize(stream) as ProjectDefinitionTemplate;
        stream.Close();
    }


    [Fact]
    public async Task GetPath()
    {
        var assembly = typeof(DataTypes).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectDefinitionTemplate));
        await using var stream =
            assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectDefinitionTemplate.xml");
        var project = xmlSerializer.Deserialize(stream) as ProjectDefinitionTemplate;
        stream.Close();

        var projectTemplateService = new NetProjectTemplateService(project);
        /*Assert.Equal("ComponentDefinitions/{projectName}Api/EndPoints/{entity}EndPoints/{endpoint}", projectTemplateService.GetItemPath("Endpoint"));
        Assert.Equal("ComponentDefinitions/{projectName}Api/Contracts/{entity}/{contract}", projectTemplateService.GetItemPath("Contract"));
        Assert.Equal("ComponentDefinitions/{projectName}Core/Entities/{entity}", projectTemplateService.GetItemPath("Entity"));
        Assert.Equal("ComponentDefinitions/{projectName}Core/Entities/{enum}", projectTemplateService.GetItemPath("Enum"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Requests/{entity}Requests/{request}", projectTemplateService.GetItemPath("Request"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Requests/{entity}Requests/{action}", projectTemplateService.GetItemPath("RequestAction"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Requests/{entity}Requests/{validator}", projectTemplateService.GetItemPath("RequestValidator"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Commands/{entity}Commands/{request}", projectTemplateService.GetItemPath("Command"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Commands/{entity}Commands/{action}", projectTemplateService.GetItemPath("CommandAction"));
        Assert.Equal("ComponentDefinitions/{projectName}Application/Commands/{entity}Commands/{validator}", projectTemplateService.GetItemPath("CommandValidator"));
        Assert.Equal("ComponentDefinitions/{projectName}Persistence/Configurations/{entity}Configuration", projectTemplateService.GetItemPath("Persistence"));*/
    }


    [Fact]
    public async Task Load_XmlModel_Work()
    {
        var assembly = typeof(DataTypes).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectDefinitionTemplate));
        await using var stream =
            assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectDefinitionTemplate.xml");
        var project = xmlSerializer.Deserialize(stream) as ProjectDefinitionTemplate;
        stream.Close();
    }
}