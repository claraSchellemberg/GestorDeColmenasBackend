using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Apiario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string UbicacionDeReferencia { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public bool Activo { get; set; } = true;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Colmena> Colmenas { get; set; } = new List<Colmena>();
        public Apiario() { }
        
        public Apiario(string nombre, string latitud, string longitud, string ubicacion,
                        int usuarioId)
        {
            this.Nombre = nombre;
            this.UbicacionDeReferencia = ubicacion;
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.UsuarioId = usuarioId;
        }
        public void AgregarColmena(Colmena colmena)
        {
            Colmenas.Add(colmena);
        }
        public void ValidarApiario()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Latitud) ||
                string.IsNullOrEmpty(Longitud) || string.IsNullOrEmpty(UbicacionDeReferencia))
            {
                throw new ApiarioException("Todos los campos del apiario son obligatorios.");
            }
        }
    }
}
