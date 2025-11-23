using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Sensores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class SensorMapper
    {
        public static Sensor FromDto(SensorSetDto sensorSetDto)
        {
            return new Sensor(sensorSetDto.tipoSensor, sensorSetDto.sensorId);
        }

        public static SensorGetDto ToDto(Sensor sensor)
        {
            return new SensorGetDto(sensor.TipoSensor, sensor.SensorId);
        }

        public static IEnumerable<SensorGetDto> ToListDto(IEnumerable<Sensor> sensores)
        {
            List<SensorGetDto> sensorGetDto = new List<SensorGetDto>();
            foreach (Sensor sensor in sensores)
            {
                sensorGetDto.Add(ToDto(sensor));
            }
            return sensorGetDto;
        }
    }
}
