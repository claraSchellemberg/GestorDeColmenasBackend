using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioObtenerPorId<T> where T : class
    {
        T ObtenerElementoPorId(int id);
    }
}
