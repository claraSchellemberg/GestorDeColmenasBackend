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


            modelBuilder.Entity<Apiario>()
                //.HasOne(apiario => apiario.Usuario)
                //.WithMany(usuario => usuario.Apiarios)
                //.HasForeignKey(apiario => apiario.IdUsuario)-- esta restriccion no se peude eliminar un usuario si tiene apiarios asociados
                //.IsRequired();
                .HasMany(apiario => apiario.Colmenas)
                .WithOne(colmena => colmena.Apiario)
                .HasForeignKey(colmena => colmena.ApiarioId);// esta restriccion no se peude eliminar un apiario si tiene colmenas asociada
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
        }

    }
}
