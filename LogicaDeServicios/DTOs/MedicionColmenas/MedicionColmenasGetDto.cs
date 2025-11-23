using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.MedicionColmenas
{
    public record MedicionColmenasGetDto(float tempExterna, float peso, DateTime FechaMedicion)
    {
    }
}
