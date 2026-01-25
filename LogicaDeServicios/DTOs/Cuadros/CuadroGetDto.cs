using LogicaDeServicios.DTOs.SensorPorCuadros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Cuadros
{
    public record CuadroGetDto(int Id, SensorPorCuadroGetDto UltimaMedicion)
    {
    }
}
