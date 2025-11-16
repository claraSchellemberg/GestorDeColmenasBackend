using LogicaDeNegocios.Excepciones;
using LogicaDeServicios.DTOs.Apiarios;
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
                _add.Agregar(new ApiarioSetDto(apiarioSetDto.Nombre, apiarioSetDto.Latitud, apiarioSetDto.Longitud, apiarioSetDto.UbicacionDeReferencia));
                return Created();
            } 
            catch (ApiarioException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Hubo un problema intente nuevamente.");
            }
        }

    }
}
