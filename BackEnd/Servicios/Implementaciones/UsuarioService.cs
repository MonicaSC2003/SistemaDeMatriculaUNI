using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using BackEnd.Helpers.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IMailHelper _mailHelper;

        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo, IMailHelper mailHelper)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _mailHelper = mailHelper;
        }

        public List<UsuarioDTO> GetTodosLosUsuarios()
        {
            try
            {
                var usuarios = _unidadDeTrabajo.UsuarioDAL.GetTodosLosUsuarios();
                var usuariosDTO = usuarios.Select(u => ConvertToDTO(u)).ToList();
                return usuariosDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsuarioDTO GetUsuarioPorId(int id)
        {
            try
            {
                var usuario = _unidadDeTrabajo.UsuarioDAL.GetUsuarioPorId(id);
                return ConvertToDTO(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UsuarioDTO> GetUsuariosByRolYCarrera(string rol, string carrera)
        {
            try
            {
                var usuarios = _unidadDeTrabajo.UsuarioDAL.GetUsuariosByRolYCarrera(rol, carrera);
                var usuariosDTO = usuarios.Select(u => ConvertToDTO(u)).ToList();
                return usuariosDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = ConvertToEntity(usuarioDTO);

                // Si el usuario tiene número de verificación, enviar correo
                if (usuario.NumeroVerificacion.HasValue)
                {
                    _ = Task.Run(async () =>
                    {
                        await _mailHelper.SendVerificationCodeAsync(
                            usuario.Correo,
                            $"{usuario.Nombre} {usuario.Apellido1}",
                            usuario.NumeroVerificacion.Value
                        );
                    });
                }

                var result = _unidadDeTrabajo.UsuarioDAL.Add(usuario);
                if (result)
                    _unidadDeTrabajo.Complete();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuario = ConvertToEntity(usuarioDTO);
                var result = _unidadDeTrabajo.UsuarioDAL.Update(usuario);
                if (result)
                    _unidadDeTrabajo.Complete();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int Estado, string Mensaje, UsuarioDTO? Usuario)> LoginUsuario(string correo, string contrasena)
        {
            try
            {
                var (estado, mensaje, usuario) = await _unidadDeTrabajo.UsuarioDAL.LoginUsuario(correo, contrasena);
                var usuarioDTO = usuario != null ? ConvertToDTO(usuario) : null;
                return (estado, mensaje, usuarioDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int Estado, string Mensaje)> VerificarUsuario(int usuarioId, int numeroVerificacion)
        {
            try
            {
                // Obtener datos del usuario antes de verificar
                var usuario = _unidadDeTrabajo.UsuarioDAL.GetUsuarioPorId(usuarioId);

                var (estado, mensaje) = await _unidadDeTrabajo.UsuarioDAL.VerificarUsuario(usuarioId, numeroVerificacion);

                if (estado == 1 && usuario != null)
                {
                    _unidadDeTrabajo.Complete();

                    // Enviar correo de confirmación de verificación
                    _ = Task.Run(async () =>
                    {
                        await _mailHelper.SendVerificationSuccessAsync(
                            usuario.Correo,
                            $"{usuario.Nombre} {usuario.Apellido1}"
                        );
                    });
                }

                return (estado, mensaje);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(int Estado, string Mensaje)> CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                // Obtener datos del usuario antes del cambio
                var usuario = _unidadDeTrabajo.UsuarioDAL.GetUsuarioPorId(usuarioId);

                var (estado, mensaje) = await _unidadDeTrabajo.UsuarioDAL.CambiarContrasena(usuarioId, contrasenaActual, contrasenaNueva);

                if (estado == 1 && usuario != null)
                {
                    _unidadDeTrabajo.Complete();

                    // Enviar correo de notificación de cambio de contraseña
                    _ = Task.Run(async () =>
                    {
                        await _mailHelper.SendPasswordChangeNotificationAsync(
                            usuario.Correo,
                            $"{usuario.Nombre} {usuario.Apellido1}"
                        );
                    });
                }

                return (estado, mensaje);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private UsuarioDTO ConvertToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido1 = usuario.Apellido1,
                Apellido2 = usuario.Apellido2,
                Identificacion = usuario.Identificacion,
                Rol = usuario.Rol,
                Carrera = usuario.Carrera,
                Correo = usuario.Correo,
                Contrasena = usuario.Contrasena,
                NumeroVerificacion = usuario.NumeroVerificacion,
                Activo = usuario.Activo
            };
        }

        private Usuario ConvertToEntity(UsuarioDTO usuarioDTO)
        {
            return new Usuario
            {
                UsuarioId = usuarioDTO.UsuarioId,
                Nombre = usuarioDTO.Nombre,
                Apellido1 = usuarioDTO.Apellido1,
                Apellido2 = usuarioDTO.Apellido2,
                Identificacion = usuarioDTO.Identificacion,
                Rol = usuarioDTO.Rol,
                Carrera = usuarioDTO.Carrera,
                Correo = usuarioDTO.Correo,
                Contrasena = usuarioDTO.Contrasena ?? string.Empty,
                NumeroVerificacion = usuarioDTO.NumeroVerificacion,
                Activo = usuarioDTO.Activo
            };
        }
        #endregion
    }
}