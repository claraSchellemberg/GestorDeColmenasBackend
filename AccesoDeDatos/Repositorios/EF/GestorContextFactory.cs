using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AccesoDeDatos.Repositorios.EF
{
    public class GestorContextFactory : IDesignTimeDbContextFactory<GestorContext>
    {
        public GestorContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GestorContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=GestorApiariosDB;Trusted_Connection=True;");

            return new GestorContext(optionsBuilder.Options);
        }
    }
}