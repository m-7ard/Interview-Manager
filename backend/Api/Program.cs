using System.Globalization;
using Api.Services;
using Api.Validators.JobApplications;
using Application.Common;
using Application.Handlers.JobApplications.Create;
using Application.Interfaces.DomainServices;
using Application.Interfaces.Repositories;
using FluentValidation;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

// dotnet ef migrations add <Name> --project Infrastructure --startup-project Api

var builder = WebApplication.CreateBuilder(args);

var appSettings = BuilderUtils.ReadAppSettings(builder.Configuration);
var databaseProviderSingleton = new DatabaseProviderSingleton(appSettings.DatabaseProviderValue);

///
///
/// DB / database / dbcontext
/// 

builder.Services.AddDbContext<MainDbContext>(options =>
    {
        if (databaseProviderSingleton.IsSQLite)
        {
            options.UseSqlite(appSettings.ConnectionString);
        }
        if (databaseProviderSingleton.IsMySQL)
        {
            options.UseMySql(appSettings.ConnectionString, MySqlServerVersion.LatestSupportedServerVersion);
        }
        else
        {
            throw new InvalidOperationException("Unsupported database provider.");
        }
    }
);

var services = builder.Services;

///
///
/// Localisation
/// 

{
    services.AddLocalization(options => options.ResourcesPath = "Resources");

    services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[] { new CultureInfo("en-US") };
        options.DefaultRequestCulture = new RequestCulture("en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });

    services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        })
        .AddViewLocalization()
        .AddDataAnnotationsLocalization();
}

///
///
/// React Cors
/// 

var apiCorsPolicy = "AllowReactApp";
services.AddCors(options =>
{
    options.AddPolicy(apiCorsPolicy,
        builder =>
        {
            builder.WithOrigins("*") // React app URL "http://localhost:5173"
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader();
        });
});


///
///
/// Mediatr
/// 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateJobApplicationCommand).Assembly));

///
///
/// Dependency Injection / DI / Services
/// 

builder.Services.AddSingleton<IDatabaseProviderSingleton>(databaseProviderSingleton);

// Domain Services
builder.Services.AddScoped<IJobApplicationDomainService>();

// Repositories
builder.Services.AddScoped<IJobApplicationRepository>();

///
///
/// Fluent Validation DI / Dependency Injection
/// 

builder.Services.AddValidatorsFromAssembly(typeof(CreateJobApplicationValidator).Assembly);

builder.Services.AddAuthorization();
builder.Services.AddDirectoryBrowser(); // For media

var app = builder.Build();

app.UseRequestLocalization();

///
///
/// Startup behaviour
/// 

using (var scope = app.Services.CreateScope())
{
    var localService = scope.ServiceProvider;
    var context = localService.GetRequiredService<MainDbContext>();

    try
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = localService.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while resetting the database.");
    }
}


app.UseCors(apiCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();

///
///
/// Media config
/// 

var mediaProvider = new PhysicalFileProvider(DirectoryService.GetMediaDirectory());

app.UseFileServer(new FileServerOptions
{
    FileProvider = mediaProvider,
    RequestPath = "/media",
    EnableDirectoryBrowsing = true
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("react/index.html");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program {  }
