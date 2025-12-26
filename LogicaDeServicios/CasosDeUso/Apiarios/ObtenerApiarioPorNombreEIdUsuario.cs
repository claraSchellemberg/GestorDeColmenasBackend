using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Apiarios
{
    public class ObtenerApiarioPorNombreEIdUsuario : IObtenerPorNombreApiarioEIdUsuario<ApiarioGetDto>
    {
        public ApiarioGetDto ObtenerPorNombreEIdUsuario(string nombre, int idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
