using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioApiario : IRepositorioAgregar<Apiario>,
                            IRepositorioActualizar<Apiario>, IRepositorioEliminar<Apiario>,
                            IRepositorioObtenerPorId<Apiario>, IRepositorioObtenerTodos<Apiario>
    {
    }
}
