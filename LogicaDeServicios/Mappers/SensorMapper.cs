using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.DTOs.Cuadros;
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
            Colmena colmena = ColmenaMapper.FromDto(sensorSetDto.colmenaDto);
            Cuadro cuadro = CuadroMapper.FromDto(sensorSetDto.cuadroDto);
            return new Sensor(sensorSetDto.tipoSensor, sensorSetDto.sensorId, colmena,
                                cuadro);
        }

        public static SensorGetDto ToDto(Sensor sensor)
        {
            ColmenaGetDto colmena = ColmenaMapper.ToDto(sensor.colmena);
            CuadroGetDto cuadro = CuadroMapper.ToDto(sensor.cuadro);
            return new SensorGetDto(sensor.TipoSensor, sensor.SensorId, cuadro, colmena);
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
