using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
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
    public class ObtenerLogin:ILogin<UsuarioLoginDto>
    {
        private IRepositorioUsuario _repo;
        //aca se le agrega el jwt para el token
        public ObtenerLogin(IRepositorioUsuario repo)
        {
            _repo = repo;
        }

        public string Execute(UsuarioLoginDto usuarioLoginDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioLoginDto.Email))
            {
                throw new UsuarioException("El email es requerido para el login.");
            }
            if (string.IsNullOrWhiteSpace(usuarioLoginDto.Contraseña))
            {
                throw new UsuarioException("La contraseña es requerida para el login.");
            }
            Usuario usuario = _repo.ObtenerPorEmail(usuarioLoginDto.Email);
            if (usuario == null)
            {
                throw new UsuarioException("No se encontró el usuario, verifique el email.");
            }
            if (usuario.Contraseña != usuarioLoginDto.Contraseña)
            {
                throw new UsuarioException("Contraseña incorrecta.");
            }
            // Convertir a DTO y retornar como JSON
            UsuarioGetDto usuarioGetDto = UsuarioMapper.ToDto(usuario);
            return System.Text.Json.JsonSerializer.Serialize(usuarioGetDto);
            //aquí se generaría un token JWT en lugar de retornar 
        }   
    }
}
