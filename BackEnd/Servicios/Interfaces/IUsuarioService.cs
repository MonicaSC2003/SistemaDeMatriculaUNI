using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> GetTodosLosUsuarios();
        UsuarioDTO GetUsuarioPorId(int id);
        List<UsuarioDTO> GetUsuariosByRolYCarrera(string rol, string carrera);
        bool AddUsuario(UsuarioDTO usuario);
        bool UpdateUsuario(UsuarioDTO usuario);
        Task<(int Estado, string Mensaje, UsuarioDTO? Usuario)> LoginUsuario(string correo, string contrasena);
        Task<(int Estado, string Mensaje)> VerificarUsuario(int usuarioId, int numeroVerificacion);
        Task<(int Estado, string Mensaje)> CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva);
    }
}
