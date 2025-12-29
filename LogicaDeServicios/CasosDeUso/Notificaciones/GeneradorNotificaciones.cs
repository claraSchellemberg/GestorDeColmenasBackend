using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicaDeServicios.CasosDeUso.Notificaciones
{
    /// <summary>
    /// Implementa el patrón Subject/Observable para las notificaciones.
    /// Gestiona los observadores y notifica cuando se genera una nueva notificación.
    /// </summary>
    public class GeneradorNotificaciones : IGeneradorNotificaciones
    {
        private List<IObservadorNotificaciones> _observadores;

        public GeneradorNotificaciones()
        {
            _observadores = new List<IObservadorNotificaciones>();
        }

        /// <summary>
        /// Suscribe un observador a las notificaciones
        /// </summary>
        public void SuscribirObservador(IObservadorNotificaciones observador)
        {
            if (observador == null)
                throw new ArgumentNullException(nameof(observador), "El observador no puede ser nulo.");

            // Evitar duplicados
            if (!_observadores.Contains(observador))
            {
                _observadores.Add(observador);
            }
        }

        /// <summary>
        /// Desuscribe un observador
        /// </summary>
        public void DesSuscribirObservador(IObservadorNotificaciones observador)
        {
            if (observador != null)
            {
                _observadores.Remove(observador);
            }
        }

        /// <summary>
        /// Notifica a todos los observadores suscriptos
        /// </summary>
        public void NotificarObservadores(Notificacion notificacion)
        {
            if (notificacion == null)
                throw new ArgumentNullException(nameof(notificacion), "La notificación no puede ser nula.");

            // Crear una copia de la lista para evitar problemas si alguien se desuscribe durante la iteración
            var observadoresCopia = _observadores.ToList();

            foreach (var observador in observadoresCopia)
            {
                observador.ActualizarNotificacion(notificacion);
            }
        }
    }
}