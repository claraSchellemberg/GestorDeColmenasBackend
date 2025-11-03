using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioObtenerTodos<T> where T : class
    {
        IEnumerable<T> ObtenerTodosLosElementos();
    }
}
