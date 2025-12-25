using LogicaDeNegocios.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Colmenas
{
    public record DetalleColmenaDto(int Id, string Nombre, string NombreApiario,
        string Descripcion, DateTime FechaInstalaciones, CondicionColmena Estado, 
            int CantidadCuadros, int CantidadRegistros, float TempInterna1, 
            float TempInterna2, float TempInterna3, float TempExterna, float Peso)
    {
    }
}
