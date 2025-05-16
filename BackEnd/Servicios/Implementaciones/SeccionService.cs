using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class SeccionService : ISeccionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public SeccionService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<SeccioneDTO> GetAllSecciones()
        {
            try
            {
                var secciones = _unidadDeTrabajo.SeccioneDAL.Get();
                var seccionesDTO = secciones.Select(s => ConvertToDTO(s)).ToList();
                return seccionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SeccioneDTO GetSeccionById(int id)
        {
            try
            {
                var seccion = _unidadDeTrabajo.SeccioneDAL.FindById(id);
                return ConvertToDTO(seccion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddSeccion(SeccioneDTO seccionDTO)
        {
            try
            {
                var seccion = ConvertToEntity(seccionDTO);
                return _unidadDeTrabajo.SeccioneDAL.Add(seccion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateSeccion(SeccioneDTO seccionDTO)
        {
            try
            {
                var seccion = ConvertToEntity(seccionDTO);
                return _unidadDeTrabajo.SeccioneDAL.Update(seccion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteSeccion(SeccioneDTO seccione)
        {
            try
            {
                var seccion = ConvertToEntity(seccione);
                return _unidadDeTrabajo.SeccioneDAL.Remove(seccion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SeccioneDTO> GetSeccionesbyCarrera(string carrera)
        {
            try
            {
                var secciones = _unidadDeTrabajo.SeccioneDAL.GetSeccionesbyCarrera(carrera);
                var seccionesDTO = secciones.Select(s => ConvertToDTO(s)).ToList();
                return seccionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private SeccioneDTO ConvertToDTO(Seccione seccion)
        {
            return new SeccioneDTO
            {
                SeccionId = seccion.SeccionId,
                UsuarioId = seccion.UsuarioId,
                CursoId = seccion.CursoId,
                HorarioId = seccion.HorarioId,
                Grupo = seccion.Grupo,
                Periodo = seccion.Periodo,
                CuposMax = seccion.CuposMax,
                Carrera = seccion.Carrera
            };
        }

        private Seccione ConvertToEntity(SeccioneDTO seccionDTO)
        {
            return new Seccione
            {
                SeccionId = seccionDTO.SeccionId,
                UsuarioId = seccionDTO.UsuarioId,
                CursoId = seccionDTO.CursoId,
                HorarioId = seccionDTO.HorarioId,
                Grupo = seccionDTO.Grupo,
                Periodo = seccionDTO.Periodo,
                CuposMax = seccionDTO.CuposMax,
                Carrera = seccionDTO.Carrera
            };
        }
        #endregion
    }
}
