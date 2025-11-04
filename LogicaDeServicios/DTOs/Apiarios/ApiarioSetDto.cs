using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.DTOs.Apiarios
{
    public record ApiarioSetDto (string Nombre, 
                                string Latitud, 
                                string Longitud, 
                                string UbicacionDeReferencia)
    {
    }
}
