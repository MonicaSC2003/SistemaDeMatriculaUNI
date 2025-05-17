using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        // POST: api/Inscripcion/AddInscripcion
        [HttpPost("AddInscripcion")]
        public IActionResult AddInscripcion([FromBody] InscripcioneDTO inscripcionDTO)
        {
            try
            {
                if (inscripcionDTO == null)
                    return BadRequest("La inscripción no puede ser nula");
                var result = _inscripcionService.AddInscripcion(inscripcionDTO);
                if (result)
                    return Ok("Inscripción agregada exitosamente");
                return BadRequest("Error al agregar la inscripción");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Inscripcion/DeleteInscripcion
        [HttpDelete("DeleteInscripcion")]
        public IActionResult DeleteInscripcion([FromBody] InscripcioneDTO inscripcionDTO)
        {
            try
            {
                if (inscripcionDTO == null)
                    return BadRequest("La inscripción no puede ser nula");
                var result = _inscripcionService.DeleteInscripcion(inscripcionDTO);
                if (result)
                    return Ok("Inscripción eliminada exitosamente");
                return BadRequest("Error al eliminar la inscripción");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Inscripcion/GetInscripcionesPorUsuario
        [HttpGet("GetInscripcionesPorUsuario")]
        public IActionResult GetInscripcionesPorUsuario(int id)
        {
            try
            {
                var inscripciones = _inscripcionService.GetInscripcionesPorUsuario(id);
                return Ok(inscripciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Inscripcion/ListarUsuariosPorSeccion
        [HttpGet("ListarUsuariosPorSeccion")]
        public IActionResult ListarUsuariosPorSeccion(int id)
        {
            try
            {
                var inscripciones = _inscripcionService.ListarUsuariosPorSeccion(id);
                return Ok(inscripciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
