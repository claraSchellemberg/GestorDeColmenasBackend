using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Colmenas
{
    public record ColmenaGetDto(int Id,
                                    DateTime FechaInstalacionSensores,
                                    string Descripcion,
                                    string Nombre,
                                    int ApiarioId,
                                    CondicionColmena Estado)
    {
    }
}
