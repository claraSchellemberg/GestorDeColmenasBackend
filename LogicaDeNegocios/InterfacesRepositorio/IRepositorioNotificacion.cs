using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio.CRUD;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    public interface IRepositorioNotificacion : IRepositorioAgregar<Notificacion>,
                                                IRepositorioObtenerPorId<Notificacion>,
                                                IRepositorioObtenerTodos<Notificacion>,
                                                IRepositorioActualizar<Notificacion>
    {
        // Obtiene todas las notificaciones de un usuario filtradas por estado
        IEnumerable<Notificacion> ObtenerPorUsuarioYEstado(int usuarioId, EstadoNotificacion? estado = null);

        // Marca una notificación como leída
        void MarcarComoLeida(int notificacionId);

        // Marca múltiples notificaciones como 
        void MarcarVariasComoLeidas(IEnumerable<int> notificacionIds);
    }
}
