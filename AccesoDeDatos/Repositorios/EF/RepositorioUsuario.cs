using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDeDatos.Repositorios.EF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private GestorContext _context;
        public RepositorioUsuario(GestorContext context)
        {
            _context = context;
        }
        public void Actualizar(Usuario entidad)
        {
            if (entidad != null)
            {
                _context.Usuarios.Update(entidad);
                entidad.ValidarUsuario();
                _context.SaveChanges();
            }
            else
            {
                throw new UsuarioException("El usuario no puede estar vacío");
            }
        }
        public Usuario Agregar(Usuario entidad)
        {
            if (entidad != null)
            {
                entidad.ValidarUsuario();
                _context.Usuarios.Add(entidad);
                _context.SaveChanges();
                return entidad;
            }
            else
            {
                throw new UsuarioException("El usuario no puede estar vacío");
            }
        }
        public void Eliminar(int id)
        {
            Usuario usuario= ObtenerElementoPorId(id);
            usuario.Estado = LogicaDeNegocios.Enums.Estado.INACTIVA;
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
        public Usuario ObtenerElementoPorId(int id)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(a => a.Id == id);
            if (usuario != null)
            {
                return usuario;
            }
            else
            {
                throw new UsuarioException("El usuario no existe");
            }
        }

        public Usuario ObtenerPorEmail(string email)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(u => u.Email.ToLower().Contains(email.ToLower()));
            if (usuario == null)
            {
                throw new NotFoundException($"No se encontró el email {email}"); 
            }
            return usuario; 
        }
    }
}
