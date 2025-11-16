using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Apiarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class ApiarioMapper
    {
        public static Apiario FromDto(ApiarioSetDto apiarioSetDto)
        {
            return new Apiario(apiarioSetDto.Nombre,
                                apiarioSetDto.Latitud,
                                apiarioSetDto.Longitud,
                                apiarioSetDto.UbicacionDeReferencia);
        }

        public static ApiarioGetDto ToDto(Apiario apiario)
        {
            return new ApiarioGetDto(apiario.Id,
                                        apiario.Nombre,
                                        apiario.Latitud,
                                        apiario.Longitud,
                                        apiario.UbicacionDeReferencia,
                                        apiario.FechaAlta);
        }

        public static IEnumerable<ApiarioGetDto> ToListDto(IEnumerable<Apiario> apiarios)
        {
            List<ApiarioGetDto> apiariosGetDto = new List<ApiarioGetDto>();
            foreach (Apiario apiario in apiarios)
            {
                apiariosGetDto.Add(ToDto(apiario));
            }
            return apiariosGetDto;
        }

    }
}
