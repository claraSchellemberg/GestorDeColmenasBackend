using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Usuarios
{
    public class ActualizarUsuario: IActualizar<UsuarioSetDto>
    {
        private IRepositorioUsuario _repo;
        public ActualizarUsuario(IRepositorioUsuario repo)
        {
            _repo = repo;
        }
        public void Actualizar(int id, UsuarioSetDto usuarioSetDto)
        {
            Usuario usuario= _repo.ObtenerElementoPorId(id);
            _repo.Actualizar(Mappers.UsuarioMapper.UpdateFromDto(usuario, usuarioSetDto));
        }
    }
}
