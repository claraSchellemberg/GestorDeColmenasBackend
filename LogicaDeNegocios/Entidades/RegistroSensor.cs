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
        public bool ValorEstaEnRangoBorde { get; set; }
        public RegistroSensor() { }

        public RegistroSensor(SensorPorCuadro sensorPorCuadro)
        {
            this.sensorPorCuadro = sensorPorCuadro;
            ValorEstaEnRangoBorde = false;
        }

        public override void ControlarValores()
        {
            float tempHipotermia = float.Parse(
                Configuracion.Instancia.GetValorPorNombre("TempHipotermia"));

            if (sensorPorCuadro.TempInterna1 <= tempHipotermia ||
                sensorPorCuadro.TempInterna2 <= tempHipotermia ||
                sensorPorCuadro.TempInterna3 <= tempHipotermia)
            {
                ValorEstaEnRangoBorde = true;
            }
            float tempCrias = float.Parse(
                Configuracion.Instancia.GetValorPorNombre("TempCrias"));

            if (sensorPorCuadro.TempInterna1 <= tempCrias ||
                sensorPorCuadro.TempInterna2 <= tempCrias ||
                sensorPorCuadro.TempInterna3 <= tempCrias) 
            {
                ValorEstaEnRangoBorde = true;
            }
        }
    }
}
