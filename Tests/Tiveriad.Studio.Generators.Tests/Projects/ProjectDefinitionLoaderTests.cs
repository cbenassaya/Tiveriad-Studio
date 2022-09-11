using System.Xml;
using System.Xml.Serialization;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Generators.Net.InternalTypes;
using Tiveriad.Studio.Generators.Projects;
using Xunit;

namespace Tiveriad.Studio.Generators.Tests.Projects;

public class ProjectDefinitionLoaderTests
{
    [Fact]
    public async Task Write_File()
    {
        var projectTemplate = new ProjectTemplate()
        {
            Components = new List<Component>()
            {
                new Component
                {
                    ComponentItems = new List<ComponentItem>()
                    {
                        new ComponentItem()
                        {
                            Pattern = "Pattern1",
                            Stereotype = "Stereotype1"
                        },
                        new ComponentItem()
                        {
                            Pattern = "Pattern2",
                            Stereotype = "Stereotype2"
                        }
                    },
                    Dependencies = new List<Dependency>
                    {
                        new Dependency
                        {
                            Include = "Include1",
                            Version = "Version1"
                        },
                        new Dependency
                        {
                            Include = "Include2",
                            Version = "Version2"
                        }
                        
                    },
                    Type = "Type1"
                },
                new Component
                {
                    ComponentItems = new List<ComponentItem>()
                    {
                        new ComponentItem()
                        {
                            Pattern = "Pattern1",
                            Stereotype = "Stereotype1"
                        },
                        new ComponentItem()
                        {
                            Pattern = "Pattern2",
                            Stereotype = "Stereotype2"
                        }
                    },
                    Dependencies = new List<Dependency>
                    {
                        new Dependency
                        {
                            Include = "Include1",
                            Version = "Version1"
                        },
                        new Dependency
                        {
                            Include = "Include2",
                            Version = "Version2"
                        }
                        
                    },
                    Type = "Type2"
                }
            }
        };
        
        XmlSerializer serializer = new(typeof(ProjectTemplate));
        using(StringWriter textWriter = new StringWriter())
        {
            serializer.Serialize(textWriter, projectTemplate);
            var result =  textWriter.ToString();
        }
    }
    
    
    [Fact]
    public async Task Read_File()
    {
        var xmlSerializer = new XmlSerializer(typeof(ProjectTemplate));
        await using var stream = new FileStream("Samples/ProjectGenerated.xml", FileMode.Open);
        var project = xmlSerializer.Deserialize(stream) as ProjectTemplate;
        stream.Close();
        
    }
    
    
    [Fact]
    public async Task GetPath()
    {
        var assembly = typeof(DataTypes).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectTemplate));
        await using var stream =  assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectTemplate.xml");
        var project = xmlSerializer.Deserialize(stream) as ProjectTemplate;
        stream.Close();
        
        var projectTemplateService = new DefaultProjectTemplateService(project);
        Assert.Equal("Components/{projectName}Api/EndPoints/{entity}EndPoints/{endpoint}", projectTemplateService.GetPath("Endpoint"));
        Assert.Equal("Components/{projectName}Api/Contracts/{entity}/{contract}", projectTemplateService.GetPath("Contract"));
        Assert.Equal("Components/{projectName}Core/Entities/{entity}", projectTemplateService.GetPath("Entity"));
        Assert.Equal("Components/{projectName}Core/Entities/{enum}", projectTemplateService.GetPath("Enum"));
        Assert.Equal("Components/{projectName}Application/Requests/{entity}Requests/{request}", projectTemplateService.GetPath("Request"));
        Assert.Equal("Components/{projectName}Application/Requests/{entity}Requests/{action}", projectTemplateService.GetPath("RequestAction"));
        Assert.Equal("Components/{projectName}Application/Requests/{entity}Requests/{validator}", projectTemplateService.GetPath("RequestValidator"));
        Assert.Equal("Components/{projectName}Application/Commands/{entity}Commands/{request}", projectTemplateService.GetPath("Command"));
        Assert.Equal("Components/{projectName}Application/Commands/{entity}Commands/{action}", projectTemplateService.GetPath("CommandAction"));
        Assert.Equal("Components/{projectName}Application/Commands/{entity}Commands/{validator}", projectTemplateService.GetPath("CommandValidator"));
        Assert.Equal("Components/{projectName}Persistence/Configurations/{entity}Configuration", projectTemplateService.GetPath("Persistence"));

    }


    [Fact]
    public async Task Load_XmlModel_Work()
    {
        var assembly = typeof(DataTypes).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectTemplate));
        await using var stream =  assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectTemplate.xml");
        var project = xmlSerializer.Deserialize(stream) as ProjectTemplate;
        stream.Close();
    }
}