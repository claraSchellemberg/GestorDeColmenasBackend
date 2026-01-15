using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.SensorPorCuadros
{
    public record SensorPorCuadroSetDto(Sensor sensor, float tempInterna1, float tempInterna2, float tempInterna3)
    {
    }
}
