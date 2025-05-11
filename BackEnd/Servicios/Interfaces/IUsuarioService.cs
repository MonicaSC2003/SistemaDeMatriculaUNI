using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> GetTodosLosUsuarios();
        List<UsuarioDTO> GetUsuariosByRolYCarrera(string rol, string carrera);
        UsuarioDTO GetUsuarioPorId(int id);
        UsuarioDTO AddUsuario(UsuarioDTO usuario);
        UsuarioDTO UpdateUsuario(UsuarioDTO usuario);
        bool VerificarUsuario(int usuarioId, int numeroVerificacion);
        bool CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva);
        UsuarioDTO LoginUsuario(string correo, string contrasena);
    }
}
