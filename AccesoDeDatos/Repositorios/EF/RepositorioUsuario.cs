using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
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
            _context.Usuarios.Remove(usuario);
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
    }
}
