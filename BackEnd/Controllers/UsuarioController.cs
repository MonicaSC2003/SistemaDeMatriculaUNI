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

        // GET: api/Usuario/GetTodosLosUsuarios
        [HttpGet("GetTodosLosUsuarios")]
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

        // GET: api/Usuario/GetUsuarioPorId/{id}
        [HttpGet("GetUsuarioPorId/{id}")]
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

        // GET: api/Usuario/GetUsuariosByRolYCarrera
        [HttpGet("GetUsuariosByRolYCarrera")]
        public IActionResult GetUsuariosByRolYCarrera([FromQuery] string rol, [FromQuery] string carrera)
        {
            try
            {
                var usuarios = _usuarioService.GetUsuariosByRolYCarrera(rol, carrera);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/AddUsuario
        [HttpPost("AddUsuario")]
        public IActionResult AddUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                    return BadRequest("El usuario no puede ser nulo");

                var result = _usuarioService.AddUsuario(usuarioDTO);
                if (result)
                    return Ok("Usuario agregado exitosamente");
                return BadRequest("Error al agregar el usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Usuario/UpdateUsuario
        [HttpPut("UpdateUsuario")]
        public IActionResult UpdateUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                if (usuarioDTO == null)
                    return BadRequest("El usuario no puede ser nulo");

                var result = _usuarioService.UpdateUsuario(usuarioDTO);
                if (result)
                    return Ok("Usuario actualizado exitosamente");
                return BadRequest("Error al actualizar el usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequest.Correo) || string.IsNullOrEmpty(loginRequest.Contrasena))
                    return BadRequest("El correo y la contraseña son requeridos");

                var (estado, mensaje, usuario) = await _usuarioService.LoginUsuario(loginRequest.Correo, loginRequest.Contrasena);

                return Ok(new { Estado = estado, Mensaje = mensaje, Usuario = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/VerificarUsuario
        [HttpPost("VerificarUsuario")]
        public async Task<IActionResult> VerificarUsuario([FromBody] VerificarUsuarioDTO verificacionRequest)
        {
            try
            {
                var (estado, mensaje) = await _usuarioService.VerificarUsuario(verificacionRequest.UsuarioId, verificacionRequest.NumeroVerificacion);

                return Ok(new { Estado = estado, Mensaje = mensaje });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Usuario/CambiarContraseña
        [HttpPost("CambiarContrasena")]
        public async Task<IActionResult> CambiarContrasena([FromBody] CambiarContrasenaDTO cambioRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(cambioRequest.ContrasenaActual) || string.IsNullOrEmpty(cambioRequest.ContrasenaNueva))
                    return BadRequest("Las contraseñas son requeridas");

                var (estado, mensaje) = await _usuarioService.CambiarContrasena(cambioRequest.UsuarioId, cambioRequest.ContrasenaActual, cambioRequest.ContrasenaNueva);

                return Ok(new { Estado = estado, Mensaje = mensaje });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}