using LogicaDeNegocios.Entidades;

namespace LogicaDeNegocios.InterfacesRepositorio
{
    /// <summary>
    /// Interfaz que define un sujeto que emite notificaciones.
    /// Gestiona el registro y notificación de observadores.
    /// </summary>
    public interface IGeneradorNotificaciones
    {
        /// <summary>
        /// Registra un observador para escuchar notificaciones
        /// </summary>
        void SuscribirObservador(IObservadorNotificaciones observador);

        /// <summary>
        /// Desuscribe un observador
        /// </summary>
        void DesSuscribirObservador(IObservadorNotificaciones observador);

        /// <summary>
        /// Notifica a todos los observadores sobre una nueva notificación
        /// </summary>
        void NotificarObservadores(Notificacion notificacion);
    }
}