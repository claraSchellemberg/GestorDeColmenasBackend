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
            return new Colmena(colmenaSetDto.Descripcion, colmenaSetDto.Nombre, colmenaSetDto.ApiarioId);
        }

        public static Colmena UpdateFromDto(Colmena colmena, ColmenaSetDto colmenaSetDto)
        {
            colmena.Descripcion = colmenaSetDto.Descripcion;
            colmena.Nombre = colmenaSetDto.Nombre;
            colmena.ApiarioId = colmenaSetDto.ApiarioId;
            return colmena;
        }
        public static Colmena FromDto(ColmenaGetDto colmenaGetDto)
        {
            return new Colmena(colmenaGetDto.Descripcion, colmenaGetDto.Nombre, colmenaGetDto.ApiarioId);
        }

        public static ColmenaGetDto ToDto(Colmena colmena)
        {
            return new ColmenaGetDto (colmena.Id,
                                          colmena.FechaInstalacionSensores,
                                          colmena.Descripcion,
                                          colmena.Nombre,
                                          colmena.ApiarioId,
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
