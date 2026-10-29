using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CasinoPlatform.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private const string ConnectionStringName = "DefaultConnection";
    private const string DefaultServerVersion = "8.0.36-mysql";

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var basePath = ResolveBasePath();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? throw new InvalidOperationException(
                $"Connection string '{ConnectionStringName}' nebyl nalezen (hledáno v '{basePath}' a v proměnných prostředí).");

        var serverVersion = ServerVersion.Parse(configuration["MySql:ServerVersion"] ?? DefaultServerVersion);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connectionString, serverVersion,
            mySql => mySql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static string ResolveBasePath()
    {
        var current = Directory.GetCurrentDirectory();

        if (File.Exists(Path.Combine(current, "appsettings.json")))
            return current;

        var apiPath = Path.GetFullPath(Path.Combine(current, "..", "CasinoPlatform.API"));
        if (File.Exists(Path.Combine(apiPath, "appsettings.json")))
            return apiPath;

        var apiFromRoot = Path.GetFullPath(Path.Combine(current, "src", "CasinoPlatform.API"));
        if (File.Exists(Path.Combine(apiFromRoot, "appsettings.json")))
            return apiFromRoot;

        return current;
    }
}
