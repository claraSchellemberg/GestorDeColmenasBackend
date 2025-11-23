using AccesoDeDatos.Repositorios.Excepciones;
using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //el controlador se encarga tambien de la seguridad
    ///200 todo ok
    ///400 datos incorrectos
    /////500 problema con servidor
    public class RegistrosController : ControllerBase
    {
        IAgregar<RegistroSetDto> _add;
        IObtenerPorId<RegistroGetDto> _getPorId;
        IObtenerTodos<RegistroGetDto> _getTodos;

        public RegistrosController(IAgregar<RegistroSetDto> add,
                                    IObtenerPorId<RegistroGetDto> getPorId,
                                    IObtenerTodos<RegistroGetDto> getTodos)
        {
            _add = add;
            _getPorId = getPorId;
            _getTodos = getTodos;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RegistroSetDto registroSetDto)// el update es igual
        {
            try
            {
                if (registroSetDto == null) 
                {
                    throw new BadRequestException("Los datos recibido son incorrectos");
                }
                _add.Agregar(new RegistroSetDto(registroSetDto.Nombre, registroSetDto.TempInterna1, registroSetDto.TempInterna2, registroSetDto.TempInterna3, registroSetDto.TempExterna, registroSetDto.Peso));
                return Created();

            }
            catch (LogicaDeNegocioException e)
            {
                return StatusCode(400, e.Message);
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
                var registros = _getTodos.ObtenerTodos();
                if( registros.Count() == 0)
                {
                    return StatusCode(204);
                }
                return Ok(registros);
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
                int idRegistro;
                int.TryParse(id, out idRegistro);

                if(idRegistro == 0)
                {
                    throw new BadRequestException("El id recibido es incorrecto");
                }
                return Ok(_getPorId.ObtenerPorId(idRegistro));
            }
            catch(BadRequestException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch(NotFoundException e)
            {
                return StatusCode(e.StatusCode(), e.Error());
            }
            catch(Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }
    }
}

    

