using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;
using LogicaDeNegocios.InterfacesRepositorio.Mediciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocios.InterfacesRepositorio.Entidades
{
    public interface IRepositorioColmena : IRepositorioAgregar<Colmena>,
                        IRepositorioActualizar<Colmena>, IRepositorioEliminar<Colmena>, 
                        IRepositorioObtenerPorId<Colmena>, IRepositorioObtenerTodos<Colmena>,
                        IRepositorioAgregarMedicionColmena
    {
        IEnumerable<Colmena> ObtenerColmenasPorApiario(int id);
    }
}
