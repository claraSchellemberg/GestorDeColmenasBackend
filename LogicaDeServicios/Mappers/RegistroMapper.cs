using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class RegistroMapper
    {
        public static Registro FromDto(RegistroDto registroDto)
        {
            return new Registro(registroDto.TempInterna1,
                                registroDto.TempInterna2,
                                registroDto.TempInterna3,
                                registroDto.TempExterna,
                                registroDto.Peso);
        }

        public static RegistroListadoDto ToDto(Registro registro)
        {
            return new RegistroListadoDto(registro.Id,
                                          registro.TempInterna1,
                                          registro.TempInterna2,
                                          registro.TempInterna3,
                                          registro.TempExterna,
                                          registro.Peso,
                                          registro.FechaRegistro);
        }

        public static IEnumerable<RegistroListadoDto> ToListDto(IEnumerable<Registro> registros)
        {
            List<RegistroListadoDto> registrosListadoDto= new List<RegistroListadoDto>();
            foreach (Registro registro in registros)
            {
                registrosListadoDto.Add(ToDto(registro));
            }
            return registrosListadoDto;
        }
    }
}
