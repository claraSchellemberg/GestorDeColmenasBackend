using LogicaDeServicios.InterfacesCasosDeUso;
using Vonage;
using Vonage.Request;
using Vonage.Messaging;

namespace WebApi.Servicios.Sms
{
    public class VonageServicioSms : IServicioSms
    {
        private readonly VonageClient _client;
        private readonly string _remitente;

        public VonageServicioSms()
        {
            var apiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("VONAGE_API_SECRET");
            _remitente = Environment.GetEnvironmentVariable("VONAGE_FROM_NUMBER") ?? "GestorColmenas";

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException(
                    "Las variables de entorno VONAGE_API_KEY y VONAGE_API_SECRET son requeridas.");
            }

            var credentials = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);
            _client = new VonageClient(credentials);
        }

        public async Task EnviarAsync(string numeroDestino, string mensaje)
        {
            var response = await _client.SmsClient.SendAnSmsAsync(new SendSmsRequest
            {
                To = numeroDestino,
                From = _remitente,
                Text = mensaje
            });

            var firstMessage = response.Messages?.FirstOrDefault();
            if (firstMessage?.Status != "0")
            {
                throw new InvalidOperationException(
                    $"Error enviando SMS via Vonage: {firstMessage?.ErrorText ?? "Unknown error"}");
            }
        }
    }
}
