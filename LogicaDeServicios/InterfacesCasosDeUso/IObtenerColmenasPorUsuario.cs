using LogicaDeServicios.DTOs.Colmenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface IObtenerColmenasPorUsuario <ColmenaGetDto>
    {
        IEnumerable<ColmenaGetDto> ObtenerColmenasPorUsuarioCU(int usuarioId);
    }
}
