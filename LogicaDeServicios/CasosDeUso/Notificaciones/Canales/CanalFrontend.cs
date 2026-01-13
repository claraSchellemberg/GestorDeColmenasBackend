using LogicaDeNegocios.Entidades;
using LogicaDeServicios.InterfacesCasosDeUso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.CasosDeUso.Notificaciones.Canales
{
    public class CanalFrontend : ICanalNotificacion
    {
        private readonly INotificacionPushService _pushService;
        public CanalFrontend(INotificacionPushService pushService)
        {
            _pushService = pushService;
        }

        public async Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            await _pushService.EnviarNotificacionAsync(usuario.Id, notificacion);
            Console.WriteLine($"🔔 Notificación push enviada al frontend para usuario: {usuario.Id}");
        }
    }
}
