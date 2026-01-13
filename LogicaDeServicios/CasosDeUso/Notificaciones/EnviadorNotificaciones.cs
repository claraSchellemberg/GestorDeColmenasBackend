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

        public EnviadorNotificaciones(
            ICanalNotificacion canalSms,
            ICanalNotificacion canalEmail,
            ICanalNotificacion canalWhatsApp)
        {
            _canales = new Dictionary<CanalPreferidoNotificacion, ICanalNotificacion>
            {
                { CanalPreferidoNotificacion.SMS, canalSms },
                { CanalPreferidoNotificacion.EMAIL, canalEmail },
                { CanalPreferidoNotificacion.WHATSAPP, canalWhatsApp }
            };
        }

        public void ActualizarNotificacion(Notificacion notificacion)
        {
            if (notificacion == null)
                throw new NotificacionException("La notificación no puede ser nula.");

            if (notificacion.UsuarioRecipiente == null)
                throw new NotificacionException("La notificación debe tener un usuario destinatario.");

            var usuario = notificacion.UsuarioRecipiente;
            var canalPreferido = usuario.MedioDeComunicacionDePreferencia;

            if (!_canales.TryGetValue(canalPreferido, out var canal))
                throw new NotificacionException($"No se encontró un canal configurado para: {canalPreferido}");

            Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine($"📤 Intentando enviar SMS a: {usuario.NumeroTelefono}");
                    await canal.EnviarAsync(notificacion, usuario);
                    Console.WriteLine($"✅ SMS enviado exitosamente");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            });
        }
    }
}
