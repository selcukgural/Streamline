using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Streamline.Infrastructure.Persistence;

/// <summary>
/// Factory for creating StreamlineDbContext instances during design time (e.g., for EF Core Migrations).
/// This allows tools like 'dotnet ef migrations add' to discover and configure the DbContext
/// without needing a fully configured application host (like the API project).
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StreamlineDbContext>
{
    public StreamlineDbContext CreateDbContext(string[] args)
    {
        // Note: This factory is primarily used by design-time tools.
        // It might not have access to the final application's configuration (appsettings.json in API).
        // Therefore, we often use a hardcoded or simplified connection string here specifically for migrations.
        // Alternatively, construct a ConfigurationBuilder to read appsettings from a known location.

        // Simple approach: Hardcode a design-time connection string (adjust path as needed)
        // This assumes the command is run from the solution root or a predictable location.
        var connectionString = "Data Source=../../streamline_design.db"; // Creates DB in solution root for design time
            
        // More robust approach: Try to find appsettings.json relative to this project
        /*
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var basePath = Directory.GetCurrentDirectory(); // Usually the project dir when run from tools

        // Adjust path to find the API project's settings file if needed
        // This can be brittle depending on where the command is run from.
         var configPath = Path.GetFullPath(Path.Combine(basePath, "../Streamline.Api"));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(configPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Could not find database connection string 'DefaultConnection' for design time.");
        }
        */

        var optionsBuilder = new DbContextOptionsBuilder<StreamlineDbContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new StreamlineDbContext(optionsBuilder.Options);
    }
}