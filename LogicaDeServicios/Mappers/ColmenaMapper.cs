using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Colmenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class ColmenaMapper
    {
        public static Colmena FromDto(ColmenaDto colmenaDto) 
        {
            return new Colmena(colmenaDto.Descripcion);
        }

        public static ColmenaListadoDto ToDto(Colmena colmena)
        {
            return new ColmenaListadoDto (colmena.Id,
                                          colmena.FechaInstalacionSensores,
                                          colmena.Descripcion,
                                          colmena.Estado);
        }

        public static IEnumerable<ColmenaListadoDto> ToListDto(IEnumerable<Colmena> colmenas)
        {
            List<ColmenaListadoDto> colmenasListadoDto = new List<ColmenaListadoDto>();
            foreach(Colmena colmena in colmenas)
            {
                colmenasListadoDto.Add(ToDto(colmena));
            }
            return colmenasListadoDto;
        }
    }
}
