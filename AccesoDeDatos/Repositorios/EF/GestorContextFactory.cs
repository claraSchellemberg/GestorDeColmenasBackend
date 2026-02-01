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
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApi");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Prefer config ConnectionStrings:DefaultConnection, then common env names
            var connectionString = configuration.GetConnectionString("DefaultConnection")
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