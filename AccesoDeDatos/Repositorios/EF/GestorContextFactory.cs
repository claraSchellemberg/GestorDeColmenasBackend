using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AccesoDeDatos.Repositorios.EF
{
    public class GestorContextFactory : IDesignTimeDbContextFactory<GestorContext>
    {
        public GestorContext CreateDbContext(string[] args)
        {
            // Busca appsettings.json en la carpeta WebApi
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApi");
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            var optionsBuilder = new DbContextOptionsBuilder<GestorContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new GestorContext(optionsBuilder.Options);
        }
    }
}