using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Colmena
    {
        public int Id { get; set; }
        public DateTime FechaInstalacionSensores { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }  
        public CondicionColmena Condicion { get; set; }  
        public Estado Estado { get; set; }
        public int ApiarioId { get; set; }
        public Apiario Apiario { get; set; }
        public List<Cuadro> Cuadros { get; set; } = new List<Cuadro>();
        public List<MedicionColmena> Mediciones { get; set; } = new List<MedicionColmena>();
        
        public Colmena() { }
        
        public Colmena(string descripcion, string nombre, int apiarioId)
        {
            this.Descripcion = descripcion;
            this.Nombre = nombre;
            this.ApiarioId = apiarioId;
            this.Condicion = CondicionColmena.OPTIMO; //lo deje como predeterminado cuando demos el alta de la colmena, lo vemos si es mejor que parta con otro estado
            this.Estado = Estado.ACTIVA;
            this.FechaInstalacionSensores = DateTime.Now;
        }

        public void ValidarColmena()
        {
            if (string.IsNullOrEmpty(Descripcion) || string.IsNullOrEmpty(Nombre) ||
                ApiarioId <= 0)
            {
                throw new ApiarioException("Todos los campos de la colmena son obligatorios.");
            }
        }
    }
}
