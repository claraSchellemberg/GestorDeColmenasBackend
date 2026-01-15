using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface IObtenerColmenasPorApiario<T>
    {
        IEnumerable<T> ObtenerColmenas(int idApiario);
    }
}
