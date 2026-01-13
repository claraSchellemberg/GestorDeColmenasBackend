using LogicaDeNegocios.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Servicios.Notificaciones
{
    public class SignalRNotificacionPushService : INotificacionPushService
    {
        private readonly IHubContext<NotificacionHub> _hubContext;

        public SignalRNotificacionPushService(IHubContext<NotificacionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task EnviarNotificacionAsync(int usuarioId, Notificacion notificacion)
        {
            //al enviar la notificacion por signalR,
            //se envia un objeto anonimo con los datos necesarios a los usuarios que están
            //guardados como logeados en el hub, es decir
            //imaginemos que tenemos un usuario con id 5 logueado, cuando una notificación se triggerea
            // SignalR se fija: "Quien está en el grupo 'usuario_5'?"
            // (el cual guardó previamente en el NotificationHub)
            // Le llega que es la conexion "abc123"
            // SignalR hace push del mensaje a la conexion "abc123" que es la conexion
            // con el front de ese usuario
            await _hubContext.Clients
                .Group($"usuario_{usuarioId}")
                .SendAsync("RecibirNotificacion", new
                {
                    id = notificacion.Id,
                    mensaje = notificacion.Mensaje,
                    fecha = notificacion.FechaNotificacion,
                    estado = notificacion.Estado.ToString()
                });
        }
    }
}