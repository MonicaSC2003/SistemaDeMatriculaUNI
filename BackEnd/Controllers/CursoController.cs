using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        // GET: api/Curso/GetAllCursos
        [HttpGet("GetAllCursos")]
        public IActionResult GetAllCursos()
        {
            try
            {
                var cursos = _cursoService.GetAllCursos();
                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Curso/GetCursoById/{id}
        [HttpGet("GetCursoById/{id}")]
        public IActionResult GetCursoById(int id)
        {
            try
            {
                var curso = _cursoService.GetCursoById(id);
                if (curso == null)
                    return NotFound($"Curso con ID {id} no encontrado");
                return Ok(curso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Curso/AddCurso
        [HttpPost("AddCurso")]
        public IActionResult AddCurso([FromBody] CursoDTO cursoDTO)
        {
            try
            {
                if (cursoDTO == null)
                    return BadRequest("El curso no puede ser nulo");
                var result = _cursoService.AddCurso(cursoDTO);
                if (result)
                    return CreatedAtAction(nameof(GetCursoById), new { id = cursoDTO.CursoId }, cursoDTO);
                else
                    return BadRequest("Error al agregar el curso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Curso/UpdateCurso
        [HttpPut("UpdateCurso")]
        public IActionResult UpdateCurso([FromBody] CursoDTO cursoDTO)
        {
            try
            {
                if (cursoDTO == null)
                    return BadRequest("El curso no puede ser nulo");
                var result = _cursoService.UpdateCurso(cursoDTO);
                if (result)
                    return NoContent();
                else
                    return NotFound($"Curso con ID {cursoDTO.CursoId} no encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Curso/DeleteCurso/{id}
        [HttpDelete("DeleteCurso/{id}")]
        public IActionResult DeleteCurso(int id)
        {
            try
            {
                var curso = _cursoService.GetCursoById(id);

                var result = _cursoService.DeleteCurso(curso);
                if (result)
                    return NoContent();
                else
                    return NotFound($"Curso con ID {id} no encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
