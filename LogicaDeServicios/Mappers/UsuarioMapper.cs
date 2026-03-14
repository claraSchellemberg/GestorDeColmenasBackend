using LogicaDeNegocios.Entidades;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.Encription;
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
            string contraseñaHasheada = Hash.HashPassword(usuarioSetDto.Contraseña);
            var usuario = new Usuario(usuarioSetDto.Nombre,
                                usuarioSetDto.Email,
                                contraseñaHasheada,
                                usuarioSetDto.NumeroTelefono,
                                usuarioSetDto.NumeroApicultor,
                                usuarioSetDto.MedioDeComunicacionDePreferencia);
            return usuario;
        }

        public static Usuario UpdateFromDto(Usuario usuario,UsuarioSetDto usuarioSetDto)
        {
            usuario.Nombre = usuarioSetDto.Nombre;
            usuario.Email = usuarioSetDto.Email;
            usuario.Contraseña = Hash.HashPassword(usuarioSetDto.Contraseña);
            usuario.NumeroTelefono = usuarioSetDto.NumeroTelefono;
            usuario.NumeroApicultor = usuarioSetDto.NumeroApicultor;
            usuario.MedioDeComunicacionDePreferencia = usuarioSetDto.MedioDeComunicacionDePreferencia;
            return usuario;

        }

        public static UsuarioGetDto ToDto(Usuario usuario)
        {
            return new UsuarioGetDto(usuario.Id,
                                        usuario.Nombre,
                                        usuario.Email,
                                        usuario.Contraseña,
                                        usuario.NumeroTelefono,
                                        usuario.NumeroApicultor,
                                        usuario.MedioDeComunicacionDePreferencia);
        }

        public static IEnumerable<UsuarioGetDto> ToListDto(IEnumerable<Usuario> usuarios)
        {
            List<UsuarioGetDto> usuariosGetDto = new List<UsuarioGetDto>();
            foreach (Usuario usuario in usuarios)
            {
                usuariosGetDto.Add(ToDto(usuario));
            }
            return usuariosGetDto;
        }   
    }
}
