using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Servicios.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<UsuarioDTO> GetTodosLosUsuarios()
        {
            try
            {
                var usuarios = _unidadDeTrabajo.UsuarioDAL.Get();
                var usuariosDTO = usuarios.Select(u => ConvertToDTO(u)).ToList();
                return usuariosDTO;
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
                var todosLosUsuarios = _unidadDeTrabajo.UsuarioDAL.Get();
                var usuarios = todosLosUsuarios
                    .Where(u => u.Rol == rol && u.Carrera == carrera)
                    .ToList();

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
                var usuario = _unidadDeTrabajo.UsuarioDAL.FindById(id);
                if (usuario == null)
                    return null;

                return ConvertToDTO(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en GetUsuarioPorId: {ex.Message}", ex);
            }
        }


        public UsuarioDTO AddUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuariosExistentes = _unidadDeTrabajo.UsuarioDAL.Get();
                if (usuariosExistentes.Any(u => u.Correo.ToLower() == usuarioDTO.Correo.ToLower()))
                {
                    throw new Exception("El correo ya está registrado");
                }
                if (usuariosExistentes.Any(u => u.Identificacion == usuarioDTO.Identificacion))
                {
                    throw new Exception("La identificación ya está registrada");
                }

                var usuario = ConvertToEntity(usuarioDTO);
                usuario.CreatedAt = DateTime.Now;
                usuario.Activo = false;
                if (!usuario.NumeroVerificacion.HasValue || usuario.NumeroVerificacion.Value == 0)
                {
                    Random random = new Random();
                    usuario.NumeroVerificacion = random.Next(100000, 999999);
                }

                var resultado = _unidadDeTrabajo.UsuarioDAL.Add(usuario);
                if (resultado)
                {
                    _unidadDeTrabajo.Complete();
                    var usuarioCreado = _unidadDeTrabajo.UsuarioDAL.Get()
                        .FirstOrDefault(u => u.Correo == usuario.Correo);

                    if (usuarioCreado != null)
                    {
                        return ConvertToDTO(usuarioCreado);
                    }
                    else
                    {
                        throw new Exception("Usuario creado pero no se pudo recuperar");
                    }
                }

                throw new Exception("El método Add devolvió false");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}", ex);
            }
        }

        public UsuarioDTO UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuarioExistente = _unidadDeTrabajo.UsuarioDAL.GetUsuarioPorId(usuarioDTO.UsuarioId);
                if (usuarioExistente == null)
                    return null;

                // Actualizar solo los campos permitidos
                usuarioExistente.Nombre = usuarioDTO.Nombre;
                usuarioExistente.Apellido1 = usuarioDTO.Apellido1;
                usuarioExistente.Apellido2 = usuarioDTO.Apellido2;
                usuarioExistente.Identificacion = usuarioDTO.Identificacion;
                usuarioExistente.Rol = usuarioDTO.Rol;
                usuarioExistente.Carrera = usuarioDTO.Carrera;
                usuarioExistente.Correo = usuarioDTO.Correo;
                usuarioExistente.NumeroVerificacion = usuarioDTO.NumeroVerificacion;
                usuarioExistente.UpdatedAt = DateTime.Now;

                var resultado = _unidadDeTrabajo.UsuarioDAL.Update(usuarioExistente);
                if (resultado)
                {
                    _unidadDeTrabajo.Complete();
                    return ConvertToDTO(usuarioExistente);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerificarUsuario(int usuarioId, int numeroVerificacion)
        {
            try
            {
                var resultado = _unidadDeTrabajo.UsuarioDAL.VerificarUsuario(usuarioId, numeroVerificacion);
                if (resultado)
                {
                    _unidadDeTrabajo.Complete();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CambiarContrasena(int usuarioId, string contrasenaActual, string contrasenaNueva)
        {
            try
            {
                var resultado = _unidadDeTrabajo.UsuarioDAL.CambiarContrasena(usuarioId, contrasenaActual, contrasenaNueva);
                if (resultado)
                {
                    _unidadDeTrabajo.Complete();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UsuarioDTO LoginUsuario(string correo, string contrasena)
        {
            try
            {
                var usuario = _unidadDeTrabajo.UsuarioDAL.LoginUsuario(correo, contrasena);
                if (usuario == null)
                    return null;

                return ConvertToDTO(usuario);
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
                Contrasena = usuarioDTO.Contrasena,
                NumeroVerificacion = usuarioDTO.NumeroVerificacion,
                Activo = usuarioDTO.Activo
            };
        }
        #endregion
    }
}