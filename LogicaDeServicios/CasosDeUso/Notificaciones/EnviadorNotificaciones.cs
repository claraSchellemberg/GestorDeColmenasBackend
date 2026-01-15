using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.CasosDeUso.Notificaciones.Canales;
using System;
using System.Collections.Generic;

namespace LogicaDeServicios.CasosDeUso.Notificaciones
{
    public class EnviadorNotificaciones : IObservadorNotificaciones
    {
        private readonly Dictionary<CanalPreferidoNotificacion, ICanalNotificacion> _canales;
        private readonly ICanalNotificacion _canalFrontend;

        public EnviadorNotificaciones(
            ICanalNotificacion canalSms,
            ICanalNotificacion canalEmail,
            ICanalNotificacion canalWhatsApp,
            ICanalNotificacion canalFrontend)
        {
            _canales = new Dictionary<CanalPreferidoNotificacion, ICanalNotificacion>
            {
                { CanalPreferidoNotificacion.SMS, canalSms },
                { CanalPreferidoNotificacion.EMAIL, canalEmail },
                { CanalPreferidoNotificacion.WHATSAPP, canalWhatsApp }
            };
            _canalFrontend = canalFrontend;
        }

        public void ActualizarNotificacion(Notificacion notificacion)
        {
            if (notificacion == null)
                throw new NotificacionException("La notificación no puede ser nula.");

            if (notificacion.UsuarioReceptor == null)
                throw new NotificacionException("La notificación debe tener un usuario destinatario.");

            var usuario = notificacion.UsuarioReceptor;
            var canalPreferido = usuario.MedioDeComunicacionDePreferencia;

            if (!_canales.TryGetValue(canalPreferido, out var canal))
                throw new NotificacionException($"No se encontró un canal configurado para: {canalPreferido}");

            Task.Run(async () =>
            {
                try
                {
                    await canal.EnviarAsync(notificacion, usuario);

                    // Siempre enviar también al frontend
                    await _canalFrontend.EnviarAsync(notificacion, usuario);

                    notificacion.Estado = EstadoNotificacion.ENVIADA;
                    Console.WriteLine($"✅ Notificación enviada exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error enviando notificación: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            });
        }
    }
}
