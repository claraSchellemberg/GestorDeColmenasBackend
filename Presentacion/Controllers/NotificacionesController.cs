using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.InterfacesRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase, IObservadorNotificaciones
    {
        private IRepositorioNotificacion _repoNotificaciones;
        private IHubContext<NotificacionHub> _hubContext;
        private IGeneradorNotificaciones _generadorNotificaciones;

        public NotificacionesController(
            IRepositorioNotificacion repoNotificaciones,
            IHubContext<NotificacionHub> hubContext,
            IGeneradorNotificaciones generadorNotificaciones)
        {
            _repoNotificaciones = repoNotificaciones;
            _hubContext = hubContext;
            _generadorNotificaciones = generadorNotificaciones;

            // PASO 1: El controlador se suscribe a las notificaciones
            // Cada vez que se genere una notificación, se ejecutará ActualizarNotificacion()
            _generadorNotificaciones.SuscribirObservador(this);
        }

        /// <summary>
        /// Se ejecuta cuando hay una nueva notificación
        /// Implementa IObservadorNotificaciones
        /// </summary>
        public async void ActualizarNotificacion(Notificacion notificacion)
        {
            try
            {
                // PASO 2: Enviar la notificación al front a través de SignalR
                // Todos los clientes conectados la recibirán en tiempo real
                await _hubContext.Clients.All.SendAsync("RecebirNotificacion", 
                    new 
                    { 
                        id = notificacion.Id,
                        mensaje = notificacion.Mensaje,
                        fecha = notificacion.FechaNotificacion
                    });

                Console.WriteLine($"[NOTIFICACION ENVIADA] {notificacion.Mensaje}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar notificación al front: {ex.Message}");
            }
        }

        [HttpGet("pendientes/{usuarioId}")]
        public IActionResult ObtenerNotificacionesPendientes(int usuarioId)
        {
            var notificaciones = _repoNotificaciones.ObtenerNotificacionesPendientes(usuarioId);
            return Ok(notificaciones);
        }

        [HttpPut("marcar-leida/{notificacionId}")]
        public IActionResult MarcarComoLeida(int notificacionId)
        {
            var notificacion = _repoNotificaciones.ObtenerElementoPorId(notificacionId);
            if (notificacion == null)
                return NotFound("Notificación no encontrada.");

            // Aquí actualizarías el estado de la notificación
            _repoNotificaciones.Actualizar(notificacion);
            return Ok("Notificación marcada como leída.");
        }
    }
}