using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.InterfacesDeEntidades
{
    public interface IUsuarioDAL : IDALGenerico<Usuario>
    {
        List<Usuario> GetUsuariosByRolYCarrera(string rol, string carrera);
        Task<(int Estado, string Mensaje, Usuario? Usuario)> LoginUsuario(string correo, string contrasena);
        Task<(int Estado, string Mensaje)> VerificarUsuario(int usuarioId, int numeroVerificacion);
        Task<(int Estado, string Mensaje)> CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva);
        List<Usuario> GetTodosLosUsuarios();
        Usuario GetUsuarioPorId(int id);
    }
}
