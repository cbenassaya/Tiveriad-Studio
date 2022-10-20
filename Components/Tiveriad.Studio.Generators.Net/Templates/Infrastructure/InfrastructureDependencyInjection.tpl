using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Repositories.EntityFrameworkCore.Repositories;
using Tiveriad.Repositories.Microsoft.DependencyInjection;
{{for dependency in model.dependencies}} 
using {{dependency}};
{{end}}

namespace {{model.itemnamespace}};

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //services.AddTransient<IConfigurationService, ConfigurationService>();

        services.AddDbContextPool<DbContext, DefaultContext>(options =>
        {
            options.LogTo(Console.WriteLine).EnableSensitiveDataLogging().EnableDetailedErrors();
            options.UseMySql(
                services.GetConnectionString(nameof(DefaultContext)),
                new MySqlServerVersion(new Version(8, 0, 28)), builder => { builder.EnableRetryOnFailure(); });
        });
        services.AddRepositories(typeof(EFRepository<,>), typeof({{model.firstentity}}));
        //services.AddTransient<IAuthoriseService, AuthoriseService>();
        
        //services.AddTransient<IUserManagerService, UserManagerService>();


        return services;
    }
    
    public static string GetConnectionString(this IServiceCollection services, string name)
    {
        return services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>()
            .GetConnectionString(name);
    }
}