using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.CRUD
{
    public interface IRepositorioObtenerPorIdUsuario<T> where T : class
    {
        T ObtenerElementoPorIdUsuario(int idUsuario);
    }
}
