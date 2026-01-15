using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Arduino
{
    public record DataArduinoDto(int idSensor,
                                 string tipoSensor,
                                 float? peso,
                                 float? tempExterna,
                                 float? tempInterna1,
                                 float? tempInterna2,
                                 float? tempInterna3)
    {

    }
}
