using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AccesoDeDatos.Repositorios.EF
{
    public class GestorContextFactory : IDesignTimeDbContextFactory<GestorContext>
    {
        public GestorContext CreateDbContext(string[] args)
        {
            // 1) Try to read connection string passed via CLI args (dotnet ef --connection "...")
            string connectionFromArgs = null;
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    var a = args[i];
                    if (string.Equals(a, "--connection", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(a, "-c", StringComparison.OrdinalIgnoreCase))
                    {
                        if (i + 1 < args.Length)
                        {
                            connectionFromArgs = args[i + 1];
                            break;
                        }
                    }
                    else if (a.StartsWith("--connection=", StringComparison.OrdinalIgnoreCase) ||
                             a.StartsWith("-c=", StringComparison.OrdinalIgnoreCase))
                    {
                        var parts = a.Split(new[] { '=' }, 2);
                        if (parts.Length == 2)
                        {
                            connectionFromArgs = parts[1];
                            break;
                        }
                    }
                }
            }

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApi");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Prefer CLI arg, then config ConnectionStrings:DefaultConnection, then common env names
            var connectionString = connectionFromArgs
                                   ?? configuration.GetConnectionString("DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found. Set it in appsettings.json, set env var ConnectionStrings__DefaultConnection or DB_CONNECTION_STRING, or pass --connection to dotnet ef.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<GestorContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new GestorContext(optionsBuilder.Options);
        }
    }
}