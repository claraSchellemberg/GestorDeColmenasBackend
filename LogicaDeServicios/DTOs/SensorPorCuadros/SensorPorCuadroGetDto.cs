using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.SensorPorCuadros
{
    public record SensorPorCuadroGetDto(Sensor sensor, float TempInterna1, 
                    float TempInterna2, float TempInterna3, DateTime FechaMedicion)
    {
    }
}
