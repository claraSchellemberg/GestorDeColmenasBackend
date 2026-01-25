using LogicaDeNegocios.Entidades;

namespace LogicaDeNegocios.InterfacesRepositorio.Notificaciones
{
    /// <summary>
    /// Interfaz que define un observador de notificaciones.
    /// Cualquier componente que quiera ser notificado sobre nuevas notificaciones debe implementar esta interfaz.
    /// </summary>
    public interface IObservadorNotificaciones
    {
        /// <summary>
        /// Se ejecuta cuando se genera una nueva notificación
        /// </summary>
        void ActualizarNotificacion(Notificacion notificacion);
    }
}