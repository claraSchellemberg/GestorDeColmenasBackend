using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class Cuadro
    {
        public int id;
        public List<SensorPorCuadro> mediciones= new List<SensorPorCuadro> ();
        public Cuadro()
        {
        }
    }
}
