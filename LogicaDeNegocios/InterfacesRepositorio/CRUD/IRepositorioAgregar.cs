using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.CRUD
{
    public interface IRepositorioAgregar<T> where T : class
    {
        T Agregar(T entidad);
    }
}
