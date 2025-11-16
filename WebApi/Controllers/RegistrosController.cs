using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult Create([FromBody] RegistroSetDto registroSetDto)
        {
            try
            {
                _add.Agregar(new RegistroSetDto(registroSetDto.Nombre, registroSetDto.TempInterna1, registroSetDto.TempInterna2, registroSetDto.TempInterna3, registroSetDto.TempExterna, registroSetDto.Peso));
                return Created();
            }
            catch(RegistroException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }

    }
}

    

