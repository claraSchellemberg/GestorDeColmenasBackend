using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario FromDto(UsuarioSetDto usuarioSetDto)
        {
            return new Usuario(usuarioSetDto.Nombre,
                                usuarioSetDto.Email,
                                usuarioSetDto.Contraseña,
                                usuarioSetDto.NumeroTelefono);
        }

        public static UsuarioGetDto ToDto(Usuario usuario)
        {
            return new UsuarioGetDto(usuario.Id,
                                        usuario.Nombre,
                                        usuario.Email,
                                        usuario.Contraseña,
                                        usuario.NumeroTelefono);
        }

        public static IEnumerable<UsuarioGetDto> ToListDto(IEnumerable<Usuario> usuarios)
        {
            List<UsuarioGetDto> usuariosGetDto= new List<UsuarioGetDto>();
            foreach (Usuario usuario in usuarios)
            {
                usuariosGetDto.Add(ToDto(usuario));
            }
            return usuariosGetDto;
        }
            
    }
}
