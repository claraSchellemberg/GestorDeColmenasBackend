using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Cuadros;
using LogicaDeServicios.DTOs.SensorPorCuadros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class CuadroMapper
    {
        public static Cuadro FromDto(CuadroSetDto cuadroSetDto)
        {
            return new Cuadro();
        }
        public static Cuadro FromDto(CuadroGetDto cuadroGetDto)
        {
            return new Cuadro();
        }

        public static CuadroGetDto ToDto(Cuadro cuadro)
        {
            var mediciones = new List<SensorPorCuadroGetDto>();
            foreach (var medicion in cuadro.Mediciones)
            {
                mediciones.Add(SensorPorCuadroMapper.ToDto(medicion));
            }
            return new CuadroGetDto(cuadro.Id, mediciones.LastOrDefault());
        }

        public static IEnumerable<CuadroGetDto> ToListDto(IEnumerable<Cuadro> cuadros)
        {
            List<CuadroGetDto> lista = new List<CuadroGetDto>();
            foreach (var cuadro in cuadros)
            {
                lista.Add(ToDto(cuadro));
            }
            return lista;
        }
    }
}
