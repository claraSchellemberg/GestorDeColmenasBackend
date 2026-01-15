using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Notificaciones;
using LogicaDeServicios.DTOs.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class RegistroSensorMapper
    {
        public static RegistroSensorGetDto ToDto(RegistroSensor registroSensor)
        {
            return new RegistroSensorGetDto(registroSensor.SensorPorCuadro);
        }

        public static IEnumerable<RegistroSensorGetDto> ToListDto(IEnumerable<RegistroSensor> registroSensores)
        {
            List<RegistroSensorGetDto> registroSensorGetDto = new List<RegistroSensorGetDto>();
            foreach (RegistroSensor registroSensor in registroSensores)
            {
                registroSensorGetDto.Add(ToDto(registroSensor));
            }
            return registroSensorGetDto;

        }
    }
}
