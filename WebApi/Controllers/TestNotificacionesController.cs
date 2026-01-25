using LogicaDeNegocios.Entidades;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.InterfacesRepositorio.Notificaciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio;
using LogicaDeServicios.InterfacesCasosDeUso;
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
        private readonly IServicioEmail _servicioEmail;

        public TestNotificacionesController(IGeneradorNotificaciones generadorNotificaciones, IServicioEmail servicioEmail)
        {
            _generadorNotificaciones = generadorNotificaciones;
            _servicioEmail = servicioEmail;
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

        [HttpPost("email")]
        public async Task<IActionResult> EnviarEmailTest([FromBody] EmailTestDto emailTestDto)
        {
            try
            {
                if (emailTestDto == null)
                {
                    return BadRequest("Los datos recibidos son incorrectos");
                }

                if (string.IsNullOrWhiteSpace(emailTestDto.EmailDestino))
                {
                    return BadRequest("El email de destino es requerido");
                }

                var usuario = new Usuario
                {
                    Nombre = emailTestDto.NombreDestino ?? "Usuario Test",
                    Email = emailTestDto.EmailDestino
                };

                var notificacion = new Notificacion
                {
                    Mensaje = emailTestDto.Mensaje ?? "Este es un mensaje de prueba del Gestor de Apiarios",
                    FechaNotificacion = DateTime.Now,
                    Estado = EstadoNotificacion.PENDIENTE
                };

                await _servicioEmail.EnviarAsync(notificacion, usuario);

                return Ok(new { mensaje = "Email enviado exitosamente", destino = emailTestDto.EmailDestino });
            }
            catch (NotificacionException ex)
            {
                return StatusCode(500, new { error = "Error de configuración", detalle = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { error = "Error enviando email", detalle = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error inesperado", detalle = ex.Message });
            }
        }
    }

    public class TestSmsRequest
    {
        public string NumeroTelefono { get; set; }
        public string Mensaje { get; set; }
    }

    public class EmailTestDto
    {
        public string EmailDestino { get; set; } = string.Empty;
        public string? NombreDestino { get; set; }
        public string? Mensaje { get; set; }
    }
}
