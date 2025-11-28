using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Arduino
{
    public record DataArduinoDto(int idSensor,
                                 string tipoSensor,
                                 float peso,
                                 float tempExterna,
                                 float temp1,
                                 float temp2,
                                 float temp3)
    {

    }
}
