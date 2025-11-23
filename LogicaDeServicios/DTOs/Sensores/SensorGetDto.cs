using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Sensores
{
    public record SensorGetDto(string tipoSensor, int sensorId)
    {
    }
}
