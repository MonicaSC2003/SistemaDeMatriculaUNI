using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoEvaluacionController : ControllerBase
    {
        private readonly ITipoEvaluacionService _tipoEvaluacionService;

        public TipoEvaluacionController(ITipoEvaluacionService tipoEvaluacionService)
        {
            _tipoEvaluacionService = tipoEvaluacionService;
        }

        // GET: api/TipoEvaluacion/GetAllTiposEvaluacion
        [HttpGet("GetAllTiposEvaluacion")]
        public IActionResult GetAllTiposEvaluacion()
        {
            try
            {
                var tiposEvaluacion = _tipoEvaluacionService.GetAllTiposEvaluacion();
                return Ok(tiposEvaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/TipoEvaluacion/GetTipoEvaluacionById/{id}
        [HttpGet("GetTipoEvaluacionById/{id}")]
        public IActionResult GetTipoEvaluacionById(int id)
        {
            try
            {
                var tipoEvaluacion = _tipoEvaluacionService.GetTipoEvaluacionById(id);
                if (tipoEvaluacion == null)
                    return NotFound($"Tipo de evaluación con ID {id} no encontrado");
                return Ok(tipoEvaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}