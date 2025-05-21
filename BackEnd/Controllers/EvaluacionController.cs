using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService _evaluacionService;

        public EvaluacionController(IEvaluacionService evaluacionService)
        {
            _evaluacionService = evaluacionService;
        }

        // GET: api/Evaluacion/GetAllEvaluaciones
        [HttpGet("GetAllEvaluaciones")]
        public IActionResult GetAllEvaluaciones()
        {
            try
            {
                var evaluaciones = _evaluacionService.GetAllEvaluaciones();
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Evaluacion/GetEvaluacionById/{id}
        [HttpGet("GetEvaluacionById/{id}")]
        public IActionResult GetEvaluacionById(int id)
        {
            try
            {
                var evaluacion = _evaluacionService.GetEvaluacionById(id);
                if (evaluacion == null)
                    return NotFound($"Evaluación con ID {id} no encontrada");
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Evaluacion/AddEvaluacion
        [HttpPost("AddEvaluacion")]
        public IActionResult AddEvaluacion([FromBody] EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                if (evaluacionDTO == null)
                    return BadRequest("La evaluación no puede ser nula");

                // Validar que el porcentaje esté entre 0 y 100
                if (evaluacionDTO.Porcentaje < 0 || evaluacionDTO.Porcentaje > 100)
                    return BadRequest("El porcentaje debe estar entre 0 y 100");

                var result = _evaluacionService.AddEvaluacion(evaluacionDTO);
                if (result)
                    return Ok("Evaluación agregada correctamente");
                return BadRequest("Error al agregar la evaluación");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Evaluacion/UpdateEvaluacion
        [HttpPut("UpdateEvaluacion")]
        public IActionResult UpdateEvaluacion([FromBody] EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                if (evaluacionDTO == null)
                    return BadRequest("La evaluación no puede ser nula");

                // Validar que el porcentaje esté entre 0 y 100
                if (evaluacionDTO.Porcentaje < 0 || evaluacionDTO.Porcentaje > 100)
                    return BadRequest("El porcentaje debe estar entre 0 y 100");

                var result = _evaluacionService.UpdateEvaluacion(evaluacionDTO);
                if (result)
                    return Ok("Evaluación actualizada correctamente");
                return BadRequest("Error al actualizar la evaluación");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Evaluacion/DeleteEvaluacion
        [HttpDelete("DeleteEvaluacion")]
        public IActionResult DeleteEvaluacion([FromBody] EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                if (evaluacionDTO == null)
                    return BadRequest("La evaluación no puede ser nula");

                var result = _evaluacionService.DeleteEvaluacion(evaluacionDTO);
                if (result)
                    return Ok("Evaluación eliminada correctamente");
                return BadRequest("Error al eliminar la evaluación");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Evaluacion/ObtenerEvaluacionesPorSeccion/{seccionId}
        [HttpGet("ObtenerEvaluacionesPorSeccion/{seccionId}")]
        public IActionResult ObtenerEvaluacionesPorSeccion(int seccionId)
        {
            try
            {
                var evaluaciones = _evaluacionService.ObtenerEvaluacionesPorSeccion(seccionId);
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Evaluacion/ValidarPorcentajes/{seccionId}
        [HttpGet("ValidarPorcentajes/{seccionId}")]
        public IActionResult ValidarPorcentajes(int seccionId)
        {
            try
            {
                var evaluaciones = _evaluacionService.ObtenerEvaluacionesPorSeccion(seccionId);
                var totalPorcentaje = evaluaciones.Sum(e => e.Porcentaje);

                var resultado = new
                {
                    SeccionId = seccionId,
                    TotalPorcentaje = totalPorcentaje,
                    EsCorrecto = totalPorcentaje == 100,
                    Mensaje = totalPorcentaje == 100
                        ? "El porcentaje total es correcto (100%)"
                        : $"El porcentaje total es {totalPorcentaje}%. Debe ser 100%"
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}