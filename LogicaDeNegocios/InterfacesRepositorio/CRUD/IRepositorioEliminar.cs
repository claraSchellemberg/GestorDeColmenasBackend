using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.CRUD
{
    public interface IRepositorioEliminar<T> where T : class
    {
        void Eliminar(int id);
    }
}
