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
        public List<Cuadro> cuadros { get; set; }
        public List<MedicionColmena> mediciones { get; set; }
       
        public Colmena() { }
        
        public Colmena(string descripcion, string nombre)
        {
            this.Descripcion = descripcion;
            this.Nombre = nombre;
            this.Estado = EstadoColmena.OPTIMO; //lo deje como predeterminado cuando demos el alta de la colmena, lo vemos si es mejor que parta con otro estado
            this.FechaInstalacionSensores = DateTime.Now;
        }
    }
}
