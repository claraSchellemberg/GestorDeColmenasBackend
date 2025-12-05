using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
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
        public EstadoColmena Estado { get; set; }        
        public int ApiarioId { get; set; }
        public Apiario Apiario { get; set; }
        public List<Cuadro> Cuadros { get; set; } = new List<Cuadro>();
        public List<MedicionColmena> Mediciones { get; set; } = new List<MedicionColmena>();
        
        public Colmena() { }
        
        public Colmena(string descripcion, string nombre)
        {
            this.Descripcion = descripcion;
            this.Nombre = nombre;
            this.Estado = EstadoColmena.OPTIMO;
            this.FechaInstalacionSensores = DateTime.Now;
        }
    }
}
