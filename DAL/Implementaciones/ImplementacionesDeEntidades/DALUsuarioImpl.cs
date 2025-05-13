using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones.ImplementacionesDeEntidades
{
    public class DALUsuarioImpl : DALGenericoImpl<Usuario>, IUsuarioDAL
    {
        SistemaCursosContext _context;
        public DALUsuarioImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }
        /*Bueno gente, a lo que vi aqui se comienzan a implementar las cosas con los sp, por orden:
         * SP_GetTodosLosUsuarios
         * SP_GetUsuariosByRolYCarrera
         * SP_GetUsuarioPorID
         * Add (SP_CreateUsuario)
         * SP_VerificarUsuario
         * SP_CambiarContraseña
         * SP_Login
         */

        public List<Usuario> GetTodosLosUsuarios()
        {
            var query = "EXEC sp_GetTodosLosUsuarios";
            var result = _context.Usuarios.FromSqlRaw(query);
            return result.ToList();
        }

        public List<Usuario> GetUsuariosByRolYCarrera(string rol, string carrera)
        {
            var query = "EXEC sp_GetUsuariosByRolYCarrera @Rol, @Carrera";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Rol", System.Data.SqlDbType.NVarChar) { Value = rol },
                new SqlParameter("@Carrera", System.Data.SqlDbType.NVarChar) { Value = carrera }
            };

            var result = _context.Usuarios.FromSqlRaw(query, parameters);
            return result.ToList();
        }

        public Usuario GetUsuarioPorId(int id)
        {
            var query = "EXEC sp_GetUsuarioPorId @UsuarioId";

            var parameter = new SqlParameter("@UsuarioId", System.Data.SqlDbType.Int) { Value = id };

            var result = _context.Usuarios.FromSqlRaw(query, parameter).AsEnumerable().FirstOrDefault();
            return result;
        }

        public new bool Add(Usuario usuario)
        {
            try
            {
                string query = "EXEC sp_CreateUsuario @Nombre, @Apellido1, @Apellido2, @Identificacion, @Rol, @Carrera, @Correo, @Contrasena, @NumeroVerificacion";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", System.Data.SqlDbType.NVarChar) { Value = usuario.Nombre },
                    new SqlParameter("@Apellido1", System.Data.SqlDbType.NVarChar) { Value = usuario.Apellido1 },
                    new SqlParameter("@Apellido2", System.Data.SqlDbType.NVarChar) { Value = usuario.Apellido2 },
                    new SqlParameter("@Identificacion", System.Data.SqlDbType.NVarChar) { Value = usuario.Identificacion },
                    new SqlParameter("@Rol", System.Data.SqlDbType.NVarChar) { Value = usuario.Rol },
                    new SqlParameter("@Carrera", System.Data.SqlDbType.NVarChar) { Value = usuario.Carrera },
                    new SqlParameter("@Correo", System.Data.SqlDbType.NVarChar) { Value = usuario.Correo },
                    new SqlParameter("@Contrasena", System.Data.SqlDbType.NVarChar) { Value = usuario.Contrasena },
                    new SqlParameter("@NumeroVerificacion", System.Data.SqlDbType.Int) { Value = usuario.NumeroVerificacion ?? (object)DBNull.Value }
                };

                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerificarUsuario(int usuarioId, int numeroVerificacion)
        {
            try
            {
                string query = "EXEC sp_VerificarUsuario @UsuarioId, @NumeroVerificacion";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioId", System.Data.SqlDbType.Int) { Value = usuarioId },
                    new SqlParameter("@NumeroVerificacion", System.Data.SqlDbType.Int) { Value = numeroVerificacion }
                };

                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                string query = "EXEC sp_CambiarContrasena @UsuarioId, @ContrasenaActual, @ContrasenaNueva";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioId", System.Data.SqlDbType.Int) { Value = usuarioId },
                    new SqlParameter("@ContrasenaActual", System.Data.SqlDbType.NVarChar) { Value = contrasenaActual },
                    new SqlParameter("@ContrasenaNueva", System.Data.SqlDbType.NVarChar) { Value = contrasenaNueva }
                };

                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Usuario LoginUsuario(string correo, string contrasena)
        {
            try
            {
                string query = "EXEC sp_LoginUsuario @Correo, @Contrasena";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Correo", System.Data.SqlDbType.NVarChar) { Value = correo },
                    new SqlParameter("@Contrasena", System.Data.SqlDbType.NVarChar) { Value = contrasena }
                };

                var result = _context.Usuarios.FromSqlRaw(query, parameters).AsEnumerable().FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
