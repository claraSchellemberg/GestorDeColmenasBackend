using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.CasosDeUso.Colmenas;
using LogicaDeServicios.DTOs.Apiarios;
using LogicaDeServicios.DTOs.Colmenas;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColmenasController : ControllerBase
    {
        IAgregar<ColmenaSetDto, ColmenaGetDto> _add;
        IObtenerPorId<ColmenaGetDto> _getPorId;
        IObtenerTodos<ColmenaGetDto> _getTodos;
        IObtenerColmenasPorApiario<ColmenaGetDto> _getColmenasPorApiario;
        IActualizar<ColmenaSetDto> _update;
        IEliminar _delete;
        IObtenerDetalleColmena<DetalleColmenaDto> _getDetalleColmena;

        public ColmenasController(IAgregar<ColmenaSetDto, ColmenaGetDto> add,
                                    IObtenerPorId<ColmenaGetDto> getPorId,
                                    IObtenerTodos<ColmenaGetDto> getTodos,
                                    IObtenerColmenasPorApiario<ColmenaGetDto> getColmenasPorApiario,
                                    IActualizar<ColmenaSetDto> update,
                                    EliminarColmena delete,
                                    IObtenerDetalleColmena<DetalleColmenaDto> getDetalleColmena)
        {
            _add = add;
            _getPorId = getPorId;
            _getTodos = getTodos;
            _getColmenasPorApiario = getColmenasPorApiario;
            _update = update;
            _delete = delete;
            _getDetalleColmena = getDetalleColmena;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ColmenaSetDto colmenaSetDto)
        {
            try
            {
                if (colmenaSetDto == null)
                {
                    throw new BadRequestException("Los datos recibidos son incorrectos");
                }
                var colmenaCreada = _add.Agregar(new ColmenaSetDto(colmenaSetDto.Descripcion, colmenaSetDto.Nombre, colmenaSetDto.ApiarioId));
                return Created($"/Colmenas/{colmenaCreada.Id}", colmenaCreada);
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
                var colmenas = _getTodos.ObtenerTodos();
                if (colmenas.Count() == 0)
                {
                    return StatusCode(204);
                }
                return Ok(colmenas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Intente nuevamente");
            }

        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            try
            {
                if (id == 0)
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

        [HttpGet("apiario/{apiarioId}")]
        public IActionResult ObtenerColmenasPorApiario(int apiarioId)
        {
            try
            {
                if (apiarioId == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                var colmenas = _getColmenasPorApiario.ObtenerColmenas(apiarioId);
                if (colmenas.Count() == 0)
                {
                    return StatusCode(204);
                }
                return Ok(colmenas);
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
        public IActionResult Actualizar(int id, [FromBody] ColmenaSetDto colmenaSetDto)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                if (colmenaSetDto == null)
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }

                _update.Actualizar(id, colmenaSetDto);
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
        [HttpGet("{id}/detalle")]
        public IActionResult ObtenerDetalleColmena(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                var colmenaDetalle = _getDetalleColmena.ObtenerDetalleColmena(id);
                return Ok(colmenaDetalle);
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
    }
}
