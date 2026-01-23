using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio.Notificaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controller temporal para probar el flujo de notificaciones.
    /// ELIMINAR antes de producción.
    /// </summary>
    [ApiController]
    [Route("api/test/notificaciones")]
    public class TestNotificacionesController : ControllerBase
    {
        private readonly IGeneradorNotificaciones _generadorNotificaciones;

        public TestNotificacionesController(IGeneradorNotificaciones generadorNotificaciones)
        {
            _generadorNotificaciones = generadorNotificaciones;
        }

        [HttpPost("sms")]
        public IActionResult EnviarSmsDePrueba([FromBody] TestSmsRequest request)
        {
            // Crear usuario de prueba con el número proporcionado
            var usuario = new Usuario("Test User", "test@test.com", "password123", request.NumeroTelefono, "123")
            {
                Id = 1,
                MedioDeComunicacionDePreferencia = CanalPreferidoNotificacion.SMS
            };

            // Crear registro ficticio
            var registro = new RegistroMedicionColmena
            {
                Id = 1,
                FechaRegistro = DateTime.Now,
                EstaPendiente = true
            };

            // Crear notificación
            var notificacion = new Notificacion(request.Mensaje, registro, usuario);

            // Disparar el flujo completo
            _generadorNotificaciones.NotificarObservadores(notificacion);

            return Ok(new { message = "Notificación enviada", destino = request.NumeroTelefono });
        }
    }

    public class TestSmsRequest
    {
        public string NumeroTelefono { get; set; } // E.164 format: +59899123456
        public string Mensaje { get; set; }
    }
}
