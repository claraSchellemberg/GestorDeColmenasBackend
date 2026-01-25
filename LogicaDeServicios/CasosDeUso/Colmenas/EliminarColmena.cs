using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Colmenas
{
    public class EliminarColmena: IEliminar
    {
        private IRepositorioColmena _repo;
        public EliminarColmena(IRepositorioColmena repo)
        {
            _repo = repo;
        }
        public void Borrar(int id)
        {
            _repo.Eliminar(id);
        }
    }
}
