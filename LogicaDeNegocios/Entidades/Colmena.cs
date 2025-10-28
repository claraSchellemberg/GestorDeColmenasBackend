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
        private static int ultimoId = 0;
        public DateTime FechaInstalacionSensores { get; set; }
        public string Descripcion { get; set; }
        public EstadoColmena Estado { get; set; }
        public List<Registro> Registros { get; set; } = new List<Registro>();
        public Colmena(string descripcion, EstadoColmena estado)
        {
            this.Id = ultimoId++;
            this.Descripcion = descripcion;
            this.Estado = estado;
            this.FechaInstalacionSensores = DateTime.Now;
        }
    }
}
