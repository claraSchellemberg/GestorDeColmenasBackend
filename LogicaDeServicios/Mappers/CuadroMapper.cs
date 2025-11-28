using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Cuadros;
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

        public static CuadroGetDto ToDto(Cuadro cuadro)
        {
            return new CuadroGetDto(cuadro.Id);
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
