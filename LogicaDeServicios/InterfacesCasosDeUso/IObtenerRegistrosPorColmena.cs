using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.InterfacesCasosDeUso
{
    public interface IObtenerRegistrosPorColmena<T> where T : class
    {
        IEnumerable<T> ObtenerRegistrosPorIdColmena(int idColmena);
}
}
