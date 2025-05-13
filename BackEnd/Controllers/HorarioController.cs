using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class HorarioController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorarioController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        // GET: api/Horario/GetAllHorarios
        [HttpGet("GetAllHorarios")]
        public IActionResult GetAllHorarios()
        {
            try
            {
                var horarios = _horarioService.GetAllHorarios();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
