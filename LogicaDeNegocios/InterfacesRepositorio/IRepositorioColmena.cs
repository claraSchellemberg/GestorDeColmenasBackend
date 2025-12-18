using LogicaDeNegocios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioColmena : IRepositorioAgregar<Colmena>,
                        IRepositorioActualizar<Colmena>, IRepositorioEliminar<Colmena>, 
                        IRepositorioObtenerPorId<Colmena>, IRepositorioObtenerTodos<Colmena>
    {
        IEnumerable<Colmena> ObtenerColmenasPorApiario(int id);
    }
}
