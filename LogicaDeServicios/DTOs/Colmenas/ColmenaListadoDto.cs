using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Colmenas
{
    public record ColmenaListadoDto(int Id,
                                    DateTime FechaInstalacionSensores,
                                    string Descripcion,
                                    EstadoColmena Estado)
    {
    }
}
