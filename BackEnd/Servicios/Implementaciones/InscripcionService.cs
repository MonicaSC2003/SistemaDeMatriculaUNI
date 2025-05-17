using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public InscripcionService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public bool AddInscripcion(InscripcioneDTO inscripcionDTO)
        {
            try
            {
                var inscripcion = ConvertToEntity(inscripcionDTO);
                return _unidadDeTrabajo.InscripcioneDAL.Add(inscripcion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteInscripcion(InscripcioneDTO inscripcionDTO)
        {
            try
            {
                var inscripcion = ConvertToEntity(inscripcionDTO);
                return _unidadDeTrabajo.InscripcioneDAL.Remove(inscripcion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<InscripcioneDTO> GetInscripcionesPorUsuario(int id)
        {
            try
            {
                var inscripciones = _unidadDeTrabajo.InscripcioneDAL.GetInscripcionesPorUsuario(id);
                var inscripcionesDTO = inscripciones.Select(i => ConvertToDTO(i)).ToList();
                return inscripcionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<InscripcioneDTO> ListarUsuariosPorSeccion(int id)
        {
            try
            {
                var inscripciones = _unidadDeTrabajo.InscripcioneDAL.ListarUsuariosPorSeccion(id);
                var inscripcionesDTO = inscripciones.Select(i => ConvertToDTO(i)).ToList();
                return inscripcionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private InscripcioneDTO ConvertToDTO(Inscripcione inscripcione)
        {
            return new InscripcioneDTO
            {
                InscripcionId = inscripcione.InscripcionId,
                UsuarioId = inscripcione.UsuarioId,
                SeccionId = inscripcione.SeccionId,
            };
        }

        private Inscripcione ConvertToEntity(InscripcioneDTO inscripcionDTO)
        {
            return new Inscripcione
            {
                InscripcionId = inscripcionDTO.InscripcionId,
                UsuarioId = inscripcionDTO.UsuarioId,
                SeccionId = inscripcionDTO.SeccionId,
            };
        }
        #endregion
    }
}
