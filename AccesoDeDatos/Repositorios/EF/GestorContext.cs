using LogicaDeNegocios;
using LogicaDeNegocios.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.EF
{
    public class GestorContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Apiario> Apiarios { get; set; }
        public DbSet<Colmena> Colmenas { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Cuadro> Cuadros { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public GestorContext(DbContextOptions<GestorContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory()) // esto referencia al startup project
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();

            var cs = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(cs))
            {
                optionsBuilder.UseSqlServer(cs);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Configuracion>()
                .HasKey(c => c.Nombre);
        }

    }
}
