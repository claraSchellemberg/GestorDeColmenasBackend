using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.DTOs.Usuarios;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController: ControllerBase
    {
        IAgregar<UsuarioSetDto, UsuarioGetDto> _add;
        IActualizar<UsuarioSetDto> _update;
        IObtenerPorId<UsuarioGetDto> _getPorId;
        IEliminar _delete;

        public UsuariosController(IAgregar<UsuarioSetDto, UsuarioGetDto> add,
                                    IActualizar<UsuarioSetDto> update,
                                    IObtenerPorId<UsuarioGetDto> getPorId,
                                    IEliminar delete)
        {
            _add = add;
            _update = update;
            _getPorId = getPorId;
            _delete = delete;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsuarioSetDto usuarioSetDto)
        {
            try
            {
                if (usuarioSetDto == null)
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }
                var usuarioCreado = _add.Agregar(new UsuarioSetDto(
                    usuarioSetDto.Nombre,
                    usuarioSetDto.Email,
                    usuarioSetDto.Contraseña,
                    usuarioSetDto.NumeroTelefono,
                    usuarioSetDto.NumeroApicultor,
                    usuarioSetDto.MedioDeComunicacionDePreferencia));
                return Created($"/Usuarios/{usuarioCreado.Id}", usuarioCreado);
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

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException("El id proporcionado no es valido");
                }
                return Ok(_getPorId.ObtenerPorId(id));
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
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }

        [HttpPut]
        public IActionResult Actualizar(int id, UsuarioSetDto usuarioSetDto)
        {
            try
            { 
                if (id == 0)
                {
                    throw new BadRequestException("El id proporcionado no es valido");
                }
                if (usuarioSetDto == null)
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }
                _update.Actualizar(id,usuarioSetDto);
                return NoContent();
            }
            catch (BadRequestException ex)
            {
                return StatusCode(ex.StatusCode(), ex.Error());
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.StatusCode(), ex.Error());
            }
            catch (LogicaDeNegocioException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            try
            {
                if( id == 0)
                {
                    throw new BadRequestException("El id proporcionado no es valido");
                }
                _delete.Borrar(id);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return StatusCode(ex.StatusCode(), ex.Error());
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.StatusCode(), ex.Error());
            }
            catch (LogicaDeNegocioException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }
    }
}
