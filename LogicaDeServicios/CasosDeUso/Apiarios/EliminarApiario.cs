using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Apiarios
{
    public class EliminarApiario: IEliminar
    {
        private IRepositorioApiario _repo;
        public EliminarApiario(IRepositorioApiario repo)
        {
            _repo = repo;
        }
        public void Borrar(int id)
        {
            _repo.Eliminar(id);
        }
    }
}
