using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Usuario> GetUsuariosByRolYCarrera(string rol, string carrera)
        {
            try
            {
                var usuarios = new List<Usuario>();
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_GetUsuariosByRolYCarrera";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Rol", SqlDbType.NVarChar, 50) { Value = rol });
                command.Parameters.Add(new SqlParameter("@Carrera", SqlDbType.NVarChar, 100) { Value = carrera });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        UsuarioId = reader.GetInt32("UsuarioId"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido1 = reader.GetString("Apellido1"),
                        Apellido2 = reader.GetString("Apellido2"),
                        Identificacion = reader.GetString("Identificacion"),
                        Rol = reader.GetString("Rol"),
                        Carrera = reader.GetString("Carrera"),
                        Correo = reader.GetString("Correo"),
                        NumeroVerificacion = reader.IsDBNull("NumeroVerificacion") ? null : reader.GetInt32("NumeroVerificacion"),
                        Activo = reader.GetBoolean("Activo"),
                        Contrasena = string.Empty // No retornamos la contraseña por seguridad
                    });
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo usuarios por rol y carrera: {ex.Message}");
            }
        }

        public async Task<(int Estado, string Mensaje, Usuario? Usuario)> LoginUsuario(string correo, string contrasena)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_LoginUsuario";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Correo", SqlDbType.NVarChar, 100) { Value = correo });
                command.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar, 100) { Value = contrasena });

                if (command.Connection?.State != ConnectionState.Open)
                    await command.Connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var estado = reader.GetInt32("Estado");
                    var mensaje = reader.GetString("Mensaje");

                    if (estado == 1) // Login exitoso
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = reader.GetInt32("UsuarioId"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido1 = reader.GetString("Apellido1"),
                            Apellido2 = reader.GetString("Apellido2"),
                            Identificacion = reader.GetString("Identificacion"),
                            Rol = reader.GetString("Rol"),
                            Carrera = reader.GetString("Carrera"),
                            Correo = reader.GetString("Correo"),
                            Contrasena = string.Empty // Por seguridad
                        };
                        return (estado, mensaje, usuario);
                    }
                    else if (estado == -3) // Usuario pendiente de verificación
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = reader.GetInt32("UsuarioId"),
                            NumeroVerificacion = reader.IsDBNull("NumeroVerificacion") ? null : reader.GetInt32("NumeroVerificacion"),
                            Contrasena = string.Empty
                        };
                        return (estado, mensaje, usuario);
                    }

                    return (estado, mensaje, null);
                }

                return (-1, "Error al procesar el login", null);
            }
            catch (Exception ex)
            {
                return (-1, $"Error en el login: {ex.Message}", null);
            }
        }

        public async Task<(int Estado, string Mensaje)> VerificarUsuario(int usuarioId, int numeroVerificacion)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_VerificarUsuario";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = usuarioId });
                command.Parameters.Add(new SqlParameter("@NumeroVerificacion", SqlDbType.Int) { Value = numeroVerificacion });

                if (command.Connection?.State != ConnectionState.Open)
                    await command.Connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var estado = reader.GetInt32("Estado");
                    var mensaje = reader.GetString("Mensaje");
                    return (estado, mensaje);
                }

                return (-1, "Error al procesar la verificación");
            }
            catch (Exception ex)
            {
                return (-1, $"Error en la verificación: {ex.Message}");
            }
        }

        public async Task<(int Estado, string Mensaje)> CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_CambiarContrasena";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = usuarioId });
                command.Parameters.Add(new SqlParameter("@ContrasenaActual", SqlDbType.NVarChar, 100) { Value = contrasenaActual });
                command.Parameters.Add(new SqlParameter("@ContrasenaNueva", SqlDbType.NVarChar, 100) { Value = contrasenaNueva });

                if (command.Connection?.State != ConnectionState.Open)
                    await command.Connection!.OpenAsync();

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var estado = reader.GetInt32("Estado");
                    var mensaje = reader.GetString("Mensaje");
                    return (estado, mensaje);
                }

                return (-1, "Error al procesar el cambio de contraseña");
            }
            catch (Exception ex)
            {
                return (-1, $"Error en el cambio de contraseña: {ex.Message}");
            }
        }

        public List<Usuario> GetTodosLosUsuarios()
        {
            try
            {
                var usuarios = new List<Usuario>();
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_GetTodosLosUsuarios";
                command.CommandType = CommandType.StoredProcedure;

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usuarios.Add(new Usuario
                    {
                        UsuarioId = reader.GetInt32("UsuarioId"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido1 = reader.GetString("Apellido1"),
                        Apellido2 = reader.GetString("Apellido2"),
                        Identificacion = reader.GetString("Identificacion"),
                        Rol = reader.GetString("Rol"),
                        Carrera = reader.GetString("Carrera"),
                        Correo = reader.GetString("Correo"),
                        NumeroVerificacion = reader.IsDBNull("NumeroVerificacion") ? null : reader.GetInt32("NumeroVerificacion"),
                        Activo = reader.GetBoolean("Activo"),
                        Contrasena = string.Empty // No retornamos la contraseña por seguridad
                    });
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo todos los usuarios: {ex.Message}");
            }
        }

        public Usuario GetUsuarioPorId(int id)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_GetUsuarioPorId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = id });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Usuario
                    {
                        UsuarioId = reader.GetInt32("UsuarioId"),
                        Nombre = reader.GetString("Nombre"),
                        Apellido1 = reader.GetString("Apellido1"),
                        Apellido2 = reader.GetString("Apellido2"),
                        Identificacion = reader.GetString("Identificacion"),
                        Rol = reader.GetString("Rol"),
                        Carrera = reader.GetString("Carrera"),
                        Correo = reader.GetString("Correo"),
                        NumeroVerificacion = reader.IsDBNull("NumeroVerificacion") ? null : reader.GetInt32("NumeroVerificacion"),
                        Activo = reader.GetBoolean("Activo"),
                        Contrasena = string.Empty // No retornamos la contraseña por seguridad
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo usuario por ID: {ex.Message}");
            }
        }

        public new bool Add(Usuario usuario)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_CreateUsuario";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 100) { Value = usuario.Nombre });
                command.Parameters.Add(new SqlParameter("@Apellido1", SqlDbType.NVarChar, 100) { Value = usuario.Apellido1 });
                command.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.NVarChar, 100) { Value = usuario.Apellido2 });
                command.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.NVarChar, 50) { Value = usuario.Identificacion });
                command.Parameters.Add(new SqlParameter("@Rol", SqlDbType.NVarChar, 50) { Value = usuario.Rol });
                command.Parameters.Add(new SqlParameter("@Carrera", SqlDbType.NVarChar, 100) { Value = usuario.Carrera });
                command.Parameters.Add(new SqlParameter("@Correo", SqlDbType.NVarChar, 100) { Value = usuario.Correo });
                command.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar, 100) { Value = usuario.Contrasena });
                command.Parameters.Add(new SqlParameter("@NumeroVerificacion", SqlDbType.NVarChar, 10) { Value = (object?)usuario.NumeroVerificacion ?? DBNull.Value });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando usuario: {ex.Message}");
                return false;
            }
        }

        public new bool Update(Usuario usuario)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_UpdateUsuario";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = usuario.UsuarioId });
                command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 100) { Value = usuario.Nombre });
                command.Parameters.Add(new SqlParameter("@Apellido1", SqlDbType.NVarChar, 100) { Value = usuario.Apellido1 });
                command.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.NVarChar, 100) { Value = usuario.Apellido2 });
                command.Parameters.Add(new SqlParameter("@Identificacion", SqlDbType.NVarChar, 50) { Value = usuario.Identificacion });
                command.Parameters.Add(new SqlParameter("@Rol", SqlDbType.NVarChar, 50) { Value = usuario.Rol });
                command.Parameters.Add(new SqlParameter("@Carrera", SqlDbType.NVarChar, 100) { Value = usuario.Carrera });
                command.Parameters.Add(new SqlParameter("@Correo", SqlDbType.NVarChar, 100) { Value = usuario.Correo });
                command.Parameters.Add(new SqlParameter("@NumeroVerificacion", SqlDbType.NVarChar, 10) { Value = (object?)usuario.NumeroVerificacion ?? DBNull.Value });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando usuario: {ex.Message}");
                return false;
            }
        }
    }
}