using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.MedicionColmenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class MedicionColmenaMapper
    {
        public static MedicionColmena FromDto(MedicionColmenasSetDto medicionColmenasSetDto)
        {
            return new MedicionColmena(medicionColmenasSetDto.tempExterna, medicionColmenasSetDto.peso);
        }

        public static MedicionColmenasGetDto ToDto(MedicionColmena medicionColmena)
        {
            return new MedicionColmenasGetDto(medicionColmena.TempExterna, medicionColmena.Peso, medicionColmena.FechaMedicion);
        }

        public static IEnumerable<MedicionColmenasGetDto> ToListDto(IEnumerable<MedicionColmena> medicionColmenas)
        {
            List<MedicionColmenasGetDto> medicionColmenasGetDto = new List<MedicionColmenasGetDto>();
            foreach (MedicionColmena medicionColmena in medicionColmenas)
            {
                medicionColmenasGetDto.Add(ToDto(medicionColmena));
            }
            return medicionColmenasGetDto;
        }
    }
}
