using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario/obtenertodoslosusuarios
        [HttpGet("obtenertodoslosusuarios")]
        public IActionResult GetTodosLosUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.GetTodosLosUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Usuario/obtenerporid/{id}
        [HttpGet("obtenerporid/{id}")]
        public IActionResult GetUsuarioPorId(int id)
        {
            try
            {
                var usuario = _usuarioService.GetUsuarioPorId(id);
                if (usuario == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Usuario/obtenerporrolycarrera
        [HttpGet("obtenerporrolycarrera")]
        public IActionResult GetUsuariosByRolYCarrera([FromQuery] string rol, [FromQuery] string carrera)
        {
            try
            {
                if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(carrera))
                    return BadRequest("Rol y carrera son requeridos");

                var usuarios = _usuarioService.GetUsuariosByRolYCarrera(rol, carrera);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/crear
        [HttpPost("crear")]
        public IActionResult AddUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );
                    return BadRequest(new { Errors = errors });
                }

                var usuario = _usuarioService.AddUsuario(usuarioDTO);
                if (usuario == null)
                    return BadRequest("No se pudo crear el usuario");

                return CreatedAtAction(nameof(GetUsuarioPorId), new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Error al crear usuario",
                    message = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }


        // PUT: api/Usuario/actualizar/{id}
        [HttpPut("actualizar/{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != usuarioDTO.UsuarioId)
                    return BadRequest("El ID de la URL no coincide con el ID del usuario");

                var usuarioActualizado = _usuarioService.UpdateUsuario(usuarioDTO);
                if (usuarioActualizado == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/login
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string correo, [FromQuery] string contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
                    return BadRequest("Correo y contraseña son requeridos");

                var usuario = _usuarioService.LoginUsuario(correo, contrasena);
                if (usuario == null)
                    return Unauthorized("Correo o contraseña incorrectos");

                // No devolver la contraseña en la respuesta
                usuario.Contrasena = null;
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/verificar/{id}
        [HttpPost("verificar/{id}")]
        public IActionResult VerificarUsuario(int id, [FromQuery] int numeroVerificacion)
        {
            try
            {
                if (numeroVerificacion < 100000 || numeroVerificacion > 999999)
                    return BadRequest("El número de verificación debe ser de 6 dígitos");

                var resultado = _usuarioService.VerificarUsuario(id, numeroVerificacion);
                if (!resultado)
                    return BadRequest("No se pudo verificar el usuario");

                return Ok(new { message = "Usuario verificado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Usuario/cambiarcontrasena/{id}
        [HttpPut("cambiarcontrasena/{id}")]
        public IActionResult CambiarContrasena(int id, [FromQuery] string contrasenaActual, [FromQuery] string contrasenaNueva)
        {
            try
            {
                if (string.IsNullOrEmpty(contrasenaActual) || string.IsNullOrEmpty(contrasenaNueva))
                    return BadRequest("Contraseña actual y nueva contraseña son requeridas");

                if (contrasenaNueva.Length < 6)
                    return BadRequest("La nueva contraseña debe tener al menos 6 caracteres");

                var resultado = _usuarioService.CambiarContrasena(id, contrasenaActual, contrasenaNueva);

                if (!resultado)
                    return BadRequest("No se pudo cambiar la contraseña");

                return Ok(new { message = "Contraseña cambiada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}