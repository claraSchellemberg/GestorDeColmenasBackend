using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Usuarios
{
    public class EliminarUsuario: IEliminar
    {
        private IRepositorioUsuario _repo;
        public EliminarUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public void Borrar(int id)
        {
            _repo.Eliminar(id);
        }
    }
}
