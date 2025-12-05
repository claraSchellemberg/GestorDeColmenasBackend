using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Cuadro
    {
        public int Id { get; set; }
        public int ColmenaId { get; set; }
        public Colmena Colmena { get; set; }
        public List<SensorPorCuadro> Mediciones { get; set; } = new List<SensorPorCuadro>();
        
        public Cuadro()
        {
        }
    }
}
