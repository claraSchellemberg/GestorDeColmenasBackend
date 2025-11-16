using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Registros
{
    public record RegistroSetDto(string Nombre,
                             float TempInterna1,
                             float TempInterna2, 
                             float TempInterna3, 
                             float TempExterna, 
                             float Peso)
    {
    }
}
