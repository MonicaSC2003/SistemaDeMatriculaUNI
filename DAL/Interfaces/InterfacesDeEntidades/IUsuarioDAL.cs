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
        Usuario GetUsuarioPorId(int id);
        bool VerificarUsuario(int usuarioId, int numeroVerificacion);
        bool CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva);
        Usuario LoginUsuario(string correo, string contrasena);
    }
}
