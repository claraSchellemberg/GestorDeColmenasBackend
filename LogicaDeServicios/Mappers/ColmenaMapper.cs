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
        public static Colmena FromDto(ColmenaSetDto colmenaSetDto) 
        {
            return new Colmena(colmenaSetDto.Descripcion);
        }

        public static ColmenaGetDto ToDto(Colmena colmena)
        {
            return new ColmenaGetDto (colmena.Id,
                                          colmena.FechaInstalacionSensores,
                                          colmena.Descripcion,
                                          colmena.Estado);
        }

        public static IEnumerable<ColmenaGetDto> ToListDto(IEnumerable<Colmena> colmenas)
        {
            List<ColmenaGetDto> colmenasGetDto = new List<ColmenaGetDto>();
            foreach(Colmena colmena in colmenas)
            {
                colmenasGetDto.Add(ToDto(colmena));
            }
            return colmenasGetDto;
        }
    }
}
