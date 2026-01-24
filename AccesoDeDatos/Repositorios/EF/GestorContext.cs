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
        public DbSet<Configuracion> Configuracions { get; set; }
        public DbSet<Cuadro> Cuadros { get; set; }
        public DbSet<MedicionColmena> MedicionColmenas { get; set; }
        public DbSet<RegistroSensor> RegistroSensors { get; set; }
        public DbSet<RegistroMedicionColmena> RegistroMedicionColmenas { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<SensorPorCuadro> SensorPorCuadros { get; set; }

        public GestorContext(DbContextOptions<GestorContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Configuracion>()
                .HasKey(c => c.Nombre);

            //paneo de relacionamient apiario -> usuario
            modelBuilder.Entity<Apiario>()
                .HasOne(apiario => apiario.Usuario)
                .WithMany(usuario => usuario.Apiarios)
                .HasForeignKey(apiario => apiario.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); //evita que se pueda borrar un usuario con apiarios

            //paneo de relacionamiento apiario -> colmena
            modelBuilder.Entity<Apiario>()
                .HasMany(apiario => apiario.Colmenas)
                .WithOne(colmena => colmena.Apiario)
                .HasForeignKey(colmena => colmena.ApiarioId)
                .OnDelete(DeleteBehavior.Restrict); // evita que se pueda borrar un apiario con colmenas

            // Índice único: Nombre de Apiario único por Usuario
            modelBuilder.Entity<Apiario>()
                .HasIndex(a => new { a.UsuarioId, a.Nombre })
                .IsUnique()
                .HasDatabaseName("IX_Apiario_UsuarioId_Nombre_Unique");

            // Índice único: Nombre de Colmena único por Apiario
            modelBuilder.Entity<Colmena>()
                .HasIndex(c => new { c.ApiarioId, c.Nombre })
                .IsUnique()
                .HasDatabaseName("IX_Colmena_ApiarioId_Nombre_Unique");

            // Configurar Sensor con ambas FKs en NoAction
            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Colmena)
                .WithMany()
                .HasForeignKey(s => s.ColmenaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.Cuadro)
                .WithMany()
                .HasForeignKey(s => s.CuadroId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configurar SensorPorCuadro FKs para evitar cascades múltiples en SQL Server
            modelBuilder.Entity<SensorPorCuadro>()
                .HasOne(spc => spc.Sensor)
                .WithMany()
                .HasForeignKey(spc => spc.SensorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SensorPorCuadro>()
                .HasOne(spc => spc.Cuadro)
                .WithMany(c => c.Mediciones)
                .HasForeignKey(spc => spc.CuadroId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configurar RegistroSensor -> SensorPorCuadro (evitar cascade delete)
            modelBuilder.Entity<RegistroSensor>()
                .HasOne(rs => rs.SensorPorCuadro)
                .WithMany() // SensorPorCuadro no tiene colección de registros en las entidades proporcionadas
                .HasForeignKey("SensorPorCuadroId") // shadow FK name matches migration
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RegistroMedicionColmena>()
                .HasOne(rmc => rmc.MedicionColmena)
                .WithMany() // MedicionColmena no tiene colección de registros en las entidades proporcionadas
                .HasForeignKey("MedicionColmenaId") // shadow FK name matches migration
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
