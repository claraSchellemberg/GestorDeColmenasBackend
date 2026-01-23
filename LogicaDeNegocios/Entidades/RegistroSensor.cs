using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class RegistroSensor: Registro
    {
        public SensorPorCuadro SensorPorCuadro { get; set; }
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string> MensajesAlerta { get; set; } = new List<string>();

        public RegistroSensor() { }

        public RegistroSensor(SensorPorCuadro sensorPorCuadro)
        {
            SensorPorCuadro = sensorPorCuadro;
            ValorEstaEnRangoBorde = false;
        }

        public override void ControlarValores()
        {
            float tempHipotermia = float.Parse(
                Configuracion.GetValorPorNombre("TempHipotermia"));

            if (SensorPorCuadro.TempInterna1 <= tempHipotermia ||
                SensorPorCuadro.TempInterna2 <= tempHipotermia ||
                SensorPorCuadro.TempInterna3 <= tempHipotermia)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Temperatura en rango de hipotermia detectada.");
            }

            float tempCrias = float.Parse(
                Configuracion.GetValorPorNombre("TempCrias"));

            if (SensorPorCuadro.TempInterna1 >= tempCrias &&
                SensorPorCuadro.TempInterna2 >= tempCrias &&
                SensorPorCuadro.TempInterna3 >= tempCrias)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Todo el cuadro presenta temperatura de crías, vaya a revisarlo para prevenir enjambramiento");
            }
        }
    }
}