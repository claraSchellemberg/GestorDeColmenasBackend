using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class RegistroSensor: Registro
    {
        public SensorPorCuadro sensorPorCuadro;
        public RegistroSensor() { }

        public RegistroSensor(SensorPorCuadro sensorPorCuadro)
        {
            this.sensorPorCuadro = sensorPorCuadro;
        }
    }
}
