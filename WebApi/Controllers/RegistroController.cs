using LogicaDeServicios.DTOs.Registros;
using LogicaDeServicios.InterfacesCasosDeUso;
using LogicaDeNegocios.Excepciones;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistroController : Controller
    {
        private readonly IObtenerRegistrosPorColmena<RegistroPorColmenaDto> _obtenerRegistrosPorColmena;

        public RegistroController(IObtenerRegistrosPorColmena<RegistroPorColmenaDto> obtenerRegistrosPorColmena)
        {
            _obtenerRegistrosPorColmena = obtenerRegistrosPorColmena;
        }

        [HttpGet("colmena/{idColmena}")]
        public IActionResult ObtenerRegistrosPorColmena(int idColmena)
        {
            try
            {
                var registrosDto = _obtenerRegistrosPorColmena.ObtenerRegistrosPorIdColmena(idColmena)?.ToList();
                return Ok(registrosDto);
            }
            catch (ColmenaException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
