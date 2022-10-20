using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using {{model.itemnamespace}}.Filters;
using {{model.itemnamespace}}.Mappings;
{{for dependency in model.dependencies}} 
using {{dependency}};
{{end}}


namespace {{model.itemnamespace}};

public static class Extensions
{
    public static void UseCorsAllowAny(this IApplicationBuilder application)
    {
        application.UseCors();
    }

    public static void UseDevelopmentEnvironment(this IApplicationBuilder application)
    {
        var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        if (!environment.IsDevelopment()) return;
        application.UseDeveloperExceptionPage();
        application.UseSwagger();
        application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Extensions).Assembly.FullName));
    }

    public static void GenerateSqlScript(this IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var defaultContext = serviceProvider.GetRequiredService<DbContext>();
        var sql = defaultContext.Database.GenerateCreateScript();
        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(),"script.sql"),sql);
    }

    public static void UseLoggerFile(this IApplicationBuilder application)
    {
        var loggerFactory = application.ApplicationServices.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddFile("Logs/Log-{Date}.txt");
    }

    public static void UseHttps(this IApplicationBuilder application)
    {
        application.UseHsts();
        application.UseHttpsRedirection();
    }

    public static void UseEndpoints(this IApplicationBuilder application)
    {
        application.UseEndpoints(builder => builder.MapControllers());
    }

    public static void AddUserResolverService(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Extensions).Assembly);
    }

    public static void AddController(this IServiceCollection services)
    {
        services
            .AddMvc(opt => opt.Filters.Add(typeof(TransactionActionFilter)));

        services.AddControllers().AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = typeof(Extensions).Assembly.FullName, Version = "v1" });
        });
    }

    public static void AddCorsMethod(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });
    }
}