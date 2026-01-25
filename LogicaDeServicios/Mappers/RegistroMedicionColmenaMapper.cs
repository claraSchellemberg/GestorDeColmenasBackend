using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public class RegistroMedicionColmenaMapper
    {
        public static RegistroMedicionColmenaGetDto ToDto(RegistroMedicionColmena registro)
        {
            if (registro == null) return null!;
            return new RegistroMedicionColmenaGetDto(registro);
        }
        public static IEnumerable<RegistroMedicionColmenaGetDto> ToListDto(IEnumerable<RegistroMedicionColmena> registros)
        {
            var list = new List<RegistroMedicionColmenaGetDto>();
            if (registros == null) return list;
            foreach (var r in registros)
            {
                list.Add(ToDto(r));
            }
            return list;
        }
    }
}
