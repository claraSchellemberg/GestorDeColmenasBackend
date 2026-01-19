using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.InterfacesCasosDeUso;
using SendGrid;

namespace WebApi.Servicios.Notificaciones
{
    public class ServicioEmail : IServicioEmail
    {
        private readonly SendGridClient _cliente;
        private string _remitente;
        private string _nombreRemitente;
        public ServicioEmail()
        {
            var apikey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            _remitente = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL")
                ?? "notificaciones.gestordeapiarios@gmail.com";
            _nombreRemitente = Environment.GetEnvironmentVariable("SENDGRID_FROM_NAME")
                ?? "Gestor de Apiarios";
            if (string.IsNullOrEmpty(apikey))
            {
                throw new NotificacionException("se necesita variable de entorno para poder enviar email");
            }
            _cliente = new SendGridClient(apikey);
        }

        public async Task EnviarAsync(Notificacion notificacion, Usuario usuario)
        {
            var from = new SendGrid.Helpers.Mail.EmailAddress(_remitente, _nombreRemitente);
            var subject = "Notificación de Gestor de Apiarios";
            var to = new SendGrid.Helpers.Mail.EmailAddress(usuario.Email, usuario.Nombre);
            var plainTextContent = notificacion.Mensaje;
            var msg = SendGrid.Helpers.Mail.MailHelper.
                CreateSingleEmail(from, to, subject, plainTextContent, null);
            var response = await _cliente.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Body.ReadAsStringAsync();
                throw new InvalidOperationException(
                    $"Error enviando email via SendGrid: {response.StatusCode} - {errorBody}");
            }
        }
    }
}
