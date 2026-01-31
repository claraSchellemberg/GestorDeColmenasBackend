using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin<UsuarioLoginDto> _login;
        public LoginController(ILogin<UsuarioLoginDto> login)
        {
            _login = login;
        }
        // aca va el HttpContext.Session para quedarme con el token

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            try
            {
                if (usuarioLoginDto == null)
                {
                    throw new BadRequestException("Los datos recibidos son incorrectos.");
                }
                var resultado = _login.Execute(usuarioLoginDto);
                return Ok(new { Token = resultado });
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (NotFoundException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (LogicaDeNegocioException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception exc)
            {
                return StatusCode(500, exc.Message);
            }
        }
    }
}
