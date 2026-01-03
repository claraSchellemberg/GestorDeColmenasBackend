using LogicaDeServicios.DTOs.Arduino;
using LogicaDeServicios.InterfacesCasosDeUso;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicionSensoresController : ControllerBase
    {
        IAgregar<DataArduinoDto, DataArduinoDto> _addMedicion;
        public MedicionSensoresController(IAgregar<DataArduinoDto, DataArduinoDto> addMedicion)
        {
            _addMedicion = addMedicion;
        }
        [HttpPost]
        public IActionResult Create([FromBody] DataArduinoDto dataArduinoDto)
        {
            try
            {
                if (dataArduinoDto == null)
                {
                    return BadRequest("Los datos recibidos son incorrectos");
                }
                var medicionCreada = _addMedicion.Agregar(dataArduinoDto);
                return Created("", medicionCreada);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
