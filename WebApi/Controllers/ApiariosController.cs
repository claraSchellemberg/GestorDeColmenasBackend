using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.CasosDeUso.Apiarios;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiariosController: ControllerBase
    {
        IAgregar<ApiarioSetDto> _add;
        IObtenerPorId<ApiarioGetDto> _getPorId;
        IObtenerTodos<ApiarioGetDto> _getTodos;
        IActualizar<ApiarioSetDto> _update;
        IEliminar _delete;

        public ApiariosController(IAgregar<ApiarioSetDto> add,
                                    IObtenerPorId<ApiarioGetDto> getPorId,
                                    IObtenerTodos<ApiarioGetDto> getTodos,
                                    IActualizar<ApiarioSetDto> update,
                                    EliminarApiario delete)
        {
            _add = add;
            _getPorId = getPorId;
            _getTodos = getTodos;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ApiarioSetDto apiarioSetDto)
        {
            try
            {
                if (apiarioSetDto == null)
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }
                _add.Agregar(new ApiarioSetDto(apiarioSetDto.Nombre, apiarioSetDto.Latitud, apiarioSetDto.Longitud, apiarioSetDto.UbicacionDeReferencia));
                return Created();
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

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            try
            {
                var apiarios = _getTodos.ObtenerTodos();
                if(apiarios.Count()==0)
                {
                    return StatusCode(204);
                }
                return Ok(apiarios);
            }
            catch(Exception) 

            {
                return StatusCode(500, "Intente nuevamente");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                if(id == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
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
        public IActionResult Actualizar(int id, ApiarioSetDto apiarioSetDto)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                if(apiarioSetDto == null)
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }
                _update.Actualizar(id, apiarioSetDto);
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
                if (id == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                _delete.Borrar(id);
                return Ok();
            }
            catch(BadRequestException ex)
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
