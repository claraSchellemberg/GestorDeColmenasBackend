using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Cuadro
    {
        public int Id;
        public List<SensorPorCuadro> Mediciones= new List<SensorPorCuadro> ();
        public Cuadro()
        {
        }
    }
}
