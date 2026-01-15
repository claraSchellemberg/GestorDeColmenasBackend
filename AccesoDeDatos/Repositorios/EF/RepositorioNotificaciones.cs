using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;

namespace AccesoDeDatos.Repositorios.EF
{
    public class RepositorioNotificaciones : IRepositorioNotificacion
    {
        private GestorContext _context;

        public RepositorioNotificaciones(GestorContext context)
        {
            _context = context;
        }

        public Notificacion Agregar(Notificacion entidad)
        {
            if (entidad != null)
            {
                _context.Notificaciones.Add(entidad);
                _context.SaveChanges();
                return entidad;
            }
            else
            {
                throw new NotificacionException("La notificación no puede estar vacía");
            }
        }

        public Notificacion ObtenerElementoPorId(int id)
        {
            Notificacion? notificacion = _context.Notificaciones
                .Include(n => n.UsuarioReceptor)
                .Include(n => n.RegistroAsociado)
                .FirstOrDefault(a => a.Id == id);

            if (notificacion != null)
            {
                return notificacion;
            }
            else
            {
                throw new NotificacionException("La notificación no existe");
            }
        }

        public IEnumerable<Notificacion> ObtenerPorUsuarioYEstado(int usuarioId, EstadoNotificacion? estado = null)
        {
            var query = _context.Notificaciones
                .Include(n => n.RegistroAsociado)
                .Where(n => n.UsuarioReceptor.Id == usuarioId);

            if (estado.HasValue)
            {
                query = query.Where(n => n.Estado == estado.Value);
            }

            return query.OrderByDescending(n => n.FechaNotificacion).ToList();
        }

        public void MarcarComoLeida(int notificacionId)
        {
            var notificacion = _context.Notificaciones.Find(notificacionId);
            if (notificacion == null)
            {
                throw new NotificacionException("La notificación no existe");
            }

            notificacion.Estado = EstadoNotificacion.LEIDA;
            _context.SaveChanges();
        }

        public void MarcarVariasComoLeidas(IEnumerable<int> notificacionIds)
        {
            var notificaciones = _context.Notificaciones
                .Where(n => notificacionIds.Contains(n.Id))
                .ToList();

            foreach (var notificacion in notificaciones)
            {
                notificacion.Estado = EstadoNotificacion.LEIDA;
            }
            _context.SaveChanges();
        }

        public IEnumerable<Notificacion> ObtenerTodosLosElementos()
        {
            return _context.Notificaciones
                .Include(n => n.UsuarioReceptor)
                .Include(n => n.RegistroAsociado)
                .OrderByDescending(n => n.FechaNotificacion)
                .ToList();
        }

        public void Actualizar(Notificacion entidad)
        {
            _context.Notificaciones.Update(entidad);
            _context.SaveChanges();
        }
    }
}
