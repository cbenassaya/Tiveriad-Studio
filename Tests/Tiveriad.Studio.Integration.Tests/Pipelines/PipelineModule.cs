using System.ComponentModel.Design.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Studio.Core.Services;
using Tiveriad.Studio.Infrastructure.Services;
using Tiveriad.UnitTests;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class Startup : StartupBase
{
    public override void Configure(IServiceCollection services)
    {
        services.AddScoped<ILoaderService, LoaderService>();
        services.AddScoped<IParserService, XmlParserService >();
    }
}


public class PipelineModule: TestBase<Startup>
{
    public void Integration_Test()
    {
        
    }
}