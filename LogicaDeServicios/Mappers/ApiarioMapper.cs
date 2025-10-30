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
        public static Apiario FromDto(ApiarioDto apiarioDto)
        {
            return new Apiario(apiarioDto.Nombre,
                                apiarioDto.Latitud,
                                apiarioDto.Longitud,
                                apiarioDto.UbicacionDeReferencia);
        }

        public static ApiarioListadoDto ToDto(Apiario apiario)
        {
            return new ApiarioListadoDto(apiario.Id,
                                        apiario.Nombre,
                                        apiario.Latitud,
                                        apiario.Longitud,
                                        apiario.UbicacionDeReferencia,
                                        apiario.fechaAlta);
        }

        public static IEnumerable<ApiarioListadoDto> ToListDto(IEnumerable<Apiario> apiarios)
        {
            List<ApiarioListadoDto> apiarioListadoDto = new List<ApiarioListadoDto>();
            foreach (Apiario apiario in apiarios)
            {
                apiarioListadoDto.Add(ToDto(apiario));
            }
            return apiarioListadoDto;
        }

    }
}
