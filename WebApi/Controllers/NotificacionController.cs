using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Enums;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.InterfacesRepositorio.Entidades;
using LogicaDeServicios.DTOs.Notificaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificacionController : ControllerBase
    {
        private readonly IRepositorioNotificacion _repositorioNotificacion;

        public NotificacionController(IRepositorioNotificacion repositorioNotificacion)
        {
            _repositorioNotificacion = repositorioNotificacion;
        }

        // Obtiene todas las notificaciones de un usuario
        // <param name="usuarioId">ID del usuario</param>
        // <param name="soloNoLeidas">Si es true,
        // solo devuelve notificaciones ENVIADA (no leídas)</param>
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ObtenerPorUsuario(int usuarioId, [FromQuery] bool soloNoLeidas = false)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new BadRequestException("El id del usuario es incorrecto");
                }

                EstadoNotificacion? estadoFiltro = soloNoLeidas ? EstadoNotificacion.ENVIADA : null;
                var notificaciones = _repositorioNotificacion.ObtenerPorUsuarioYEstado(usuarioId, estadoFiltro);

                var resultado = notificaciones.Select(n => new NotificacionGetDto(
                    n.Id,
                    n.Mensaje,
                    n.FechaNotificacion,
                    n.Estado.ToString(),
                    n.RegistroAsociado?.Id
                ));

                return Ok(resultado);
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema, intente nuevamente.");
            }
        }

        // Marca una notificación como leída
        [HttpPatch("{id}/leido")]
        public IActionResult MarcarComoLeida(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BadRequestException("El id de la notificación es incorrecto");
                }

                _repositorioNotificacion.MarcarComoLeida(id);
                return NoContent();
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (NotificacionException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema, intente nuevamente.");
            }
        }

        // Marca múltiples notificaciones como leídas
        [HttpPatch("marcar-leidas")]
        public IActionResult MarcarVariasComoLeidas([FromBody] List<int> ids)
        {
            try
            {
                if (ids == null || ids.Count == 0)
                {
                    throw new BadRequestException("Debe proporcionar al menos un id de notificación");
                }

                _repositorioNotificacion.MarcarVariasComoLeidas(ids);
                return NoContent();
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema, intente nuevamente.");
            }
        }

        // Obtiene el conteo de notificaciones no leídas (para la campanita)
        [HttpGet("usuario/{usuarioId}/conteo-no-leidas")]
        public IActionResult ObtenerConteoNoLeidas(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new BadRequestException("El id del usuario es incorrecto");
                }

                var notificaciones = _repositorioNotificacion.ObtenerPorUsuarioYEstado(usuarioId, EstadoNotificacion.ENVIADA);
                return Ok(new { conteo = notificaciones.Count() });
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema, intente nuevamente.");
            }
        }
    }
}
