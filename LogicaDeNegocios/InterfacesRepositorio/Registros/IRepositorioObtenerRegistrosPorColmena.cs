using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Registros
{
    public interface IRepositorioObtenerRegistrosPorColmena<T>
    {
        IEnumerable<T> ObtenerRegistrosPorIdColmenaRepo(int idColmena);
    }
}
