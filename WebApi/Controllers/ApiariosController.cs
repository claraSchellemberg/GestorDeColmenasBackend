using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
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

        public ApiariosController(IAgregar<ApiarioSetDto> add,
                                    IObtenerPorId<ApiarioGetDto> getPorId,
                                    IObtenerTodos<ApiarioGetDto> getTodos)
        {
            _add = add;
            _getPorId = getPorId;
            _getTodos = getTodos;
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
            catch (LogicaDeNegocioException ex)
            {
                return StatusCode(400, ex.Message);
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
        public IActionResult ObtenerPorId(string id)
        {
            try
            {
                int idApiario;
                int.TryParse(id, out idApiario);

                if(idApiario == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                return Ok(_getPorId.ObtenerPorId(idApiario));
            }
            catch (BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (NotFoundException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }
    }
}
