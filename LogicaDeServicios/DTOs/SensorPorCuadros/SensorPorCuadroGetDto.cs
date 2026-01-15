using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.SensorPorCuadros
{
    public record SensorPorCuadroGetDto(Sensor sensor, float tempInterna1, float tempInterna2, float tempInterna3, DateTime FechaMedicion)
    {
    }
}
