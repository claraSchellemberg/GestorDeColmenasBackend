using LogicaDeNegocios.InterfacesRepositorio;
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
    /*public class ObtenerLogin:ILogin<UsuarioLoginDto>
    {
        private IRepositorioUsuario _repo;
        //aca se le agrega el jwt para el token
        public ObtenerLogin(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public string Execute(UsuarioLoginDto usuarioLoginDto)
        {
           UsuarioLoginDto dto= UsuarioMapper.ToDto(_repo.ObtenerPorEmail(usuarioLoginDto.Email));
            return dto.ToString();
        }
    }*/
}
