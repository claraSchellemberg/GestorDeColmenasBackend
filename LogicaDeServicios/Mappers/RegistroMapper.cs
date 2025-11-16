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
        public static Registro FromDto(RegistroSetDto registroSetDto)
        {
            return new Registro(registroSetDto.Nombre,
                                registroSetDto.TempInterna1,
                                registroSetDto.TempInterna2,
                                registroSetDto.TempInterna3,
                                registroSetDto.TempExterna,
                                registroSetDto.Peso);
        }

        public static RegistroGetDto ToDto(Registro registro)
        {
            return new RegistroGetDto(registro.Id,
                                          registro.NombreColmena,
                                          registro.TempInterna1,
                                          registro.TempInterna2,
                                          registro.TempInterna3,
                                          registro.TempExterna,
                                          registro.Peso,
                                          registro.FechaRegistro);
        }

        public static IEnumerable<RegistroGetDto> ToListDto(IEnumerable<Registro> registros)
        {
            List<RegistroGetDto> registrosGetDto= new List<RegistroGetDto>();
            foreach (Registro registro in registros)
            {
                registrosGetDto.Add(ToDto(registro));
            }
            return registrosGetDto;
        }
    }
}
