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
        public static Usuario FromDto(UsuarioDto usuarioDto)
        {
            return new Usuario(usuarioDto.Nombre,
                                usuarioDto.Email,
                                usuarioDto.Contraseña);
        }

        public static UsuarioListadoDto ToDto(Usuario usuario)
        {
            return new UsuarioListadoDto(usuario.Id,
                                        usuario.Nombre,
                                        usuario.Email,
                                        usuario.Contraseña);
        }

        public static IEnumerable<UsuarioListadoDto> ToListDto(IEnumerable<Usuario> usuarios)
        {
            List<UsuarioListadoDto> usuariosListadoDto= new List<UsuarioListadoDto>();
            foreach (Usuario usuario in usuarios)
            {
                usuariosListadoDto.Add(ToDto(usuario));
            }
            return usuariosListadoDto;
        }
            
    }
}
