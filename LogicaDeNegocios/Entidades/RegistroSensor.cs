using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.Entidades
{
    public class RegistroSensor: Registro
    {
        public int? SensorPorCuadroId { get; set; }
        public SensorPorCuadro SensorPorCuadro { get; set; } 
        public bool ValorEstaEnRangoBorde { get; set; }
        public List<string> MensajesAlerta { get; set; } = new List<string>();

        private readonly ILogger<RegistroSensor> _logger;


        public RegistroSensor() { }

        public RegistroSensor(SensorPorCuadro sensorPorCuadro)
        {
            SensorPorCuadro = sensorPorCuadro;
            ValorEstaEnRangoBorde = false;
        }

        //controlador que acepta logs para ver desde azure
        public RegistroSensor(SensorPorCuadro sensorPorCuadro, ILogger<RegistroSensor> logger)
        {
            SensorPorCuadro = sensorPorCuadro;
            ValorEstaEnRangoBorde = false;
            _logger = logger ?? NullLogger<RegistroSensor>.Instance;
        }

        public override void ControlarValores()
        {
            float tempHipotermia = float.Parse(
                Configuracion.GetValorPorNombre("TempHipotermia"));
            _logger.LogInformation($"TempHipotermia trae: {tempHipotermia}");

            if (SensorPorCuadro.TempInterna1 <= tempHipotermia ||
                SensorPorCuadro.TempInterna2 <= tempHipotermia ||
                SensorPorCuadro.TempInterna3 <= tempHipotermia)
            {
                ValorEstaEnRangoBorde = true;
                MensajesAlerta.Add("Alerta: Temperatura en rango de hipotermia detectada.");
            }

            float tempCrias = float.Parse(
                Configuracion.GetValorPorNombre("TempCrias"));
            _logger.LogInformation($"TempCrias trae: {tempCrias }");

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