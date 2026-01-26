using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeServicios.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Usuarios
{
    public class AgregarUsuario: IAgregar<UsuarioSetDto, UsuarioGetDto>
    {
        private IRepositorioUsuario _repo;

        public AgregarUsuario(IRepositorioUsuario repo) 
        {
            _repo = repo;
        }

        public UsuarioGetDto Agregar(UsuarioSetDto usuarioSetDto)
        {
            UsuarioGetDto agregado;
            agregado = UsuarioMapper.ToDto(
                _repo.Agregar(UsuarioMapper.FromDto(usuarioSetDto))
                );
            return agregado;
        }
    }
}

