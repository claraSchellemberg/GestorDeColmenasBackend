using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Sensores;
using LogicaDeServicios.DTOs.SensorPorCuadros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class SensorPorCuadroMapper
    {
        public static SensorPorCuadro FromDto(SensorPorCuadroSetDto sensorPorCuadroSetDto)
        {
            return new SensorPorCuadro(sensorPorCuadroSetDto.sensor, sensorPorCuadroSetDto.tempInterna1, sensorPorCuadroSetDto.tempInterna2, sensorPorCuadroSetDto.tempInterna3);
        }

        public static SensorPorCuadroGetDto ToDto(SensorPorCuadro sensorPorCuadro)
        {
            return new SensorPorCuadroGetDto(sensorPorCuadro.sensor, sensorPorCuadro.TempInterna1, sensorPorCuadro.TempInterna2, sensorPorCuadro.TempInterna3, sensorPorCuadro.FechaMedicion);
        }

        public static IEnumerable<SensorPorCuadroGetDto> ToListDto(IEnumerable<SensorPorCuadro> sensorPorCuadros)
        {
            List<SensorPorCuadroGetDto> sensorPorCuadroGetDto = new List<SensorPorCuadroGetDto>();
            foreach (SensorPorCuadro sensorPorCuadro in sensorPorCuadros)
            {
                sensorPorCuadroGetDto.Add(ToDto(sensorPorCuadro));
            }
            return sensorPorCuadroGetDto;
        }
    }
}
