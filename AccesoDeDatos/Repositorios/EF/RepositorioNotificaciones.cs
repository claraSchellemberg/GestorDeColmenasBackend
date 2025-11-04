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
    public class RepositorioNotificaciones : IRepositorioNotificacion
    {
        private GestorContext _context;
        public RepositorioNotificaciones(GestorContext context)
        {
            _context = context;
        }
        public void Agregar(Notificacion entidad)
        {
            if (entidad != null)
            {
                _context.Notificaciones.Add(entidad);
                _context.SaveChangesAsync();
            }
            else
            {
                throw new NotificacionException("La notificación no puede estar vacía");
            }
        }

        public void Eliminar(int id)
        {
            Notificacion notificacion = ObtenerElementoPorId(id);
            _context.Notificaciones.Remove(notificacion);
            _context.SaveChanges();
        }

        public Notificacion ObtenerElementoPorId(int id)
        {
            Notificacion notificacion = _context.Notificaciones.FirstOrDefault(a => a.Id == id);
            if (notificacion != null)
            {
                return notificacion;
            }
            else
            {
                throw new NotificacionException("La notificación no existe");
            }
        }
    }
}
