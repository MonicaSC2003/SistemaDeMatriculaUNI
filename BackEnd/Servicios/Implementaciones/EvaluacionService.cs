using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public EvaluacionService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<EvaluacioneDTO> GetAllEvaluaciones()
        {
            try
            {
                var evaluaciones = _unidadDeTrabajo.EvaluacioneDAL.Get();
                var evaluacionesDTO = evaluaciones.Select(e => ConvertToDTO(e)).ToList();
                return evaluacionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EvaluacioneDTO GetEvaluacionById(int id)
        {
            try
            {
                var evaluacion = _unidadDeTrabajo.EvaluacioneDAL.FindById(id);
                return evaluacion != null ? ConvertToDTO(evaluacion) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddEvaluacion(EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                var evaluacion = ConvertToEntity(evaluacionDTO);
                return _unidadDeTrabajo.EvaluacioneDAL.Add(evaluacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateEvaluacion(EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                var evaluacion = ConvertToEntity(evaluacionDTO);
                return _unidadDeTrabajo.EvaluacioneDAL.Update(evaluacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteEvaluacion(EvaluacioneDTO evaluacionDTO)
        {
            try
            {
                var evaluacion = ConvertToEntity(evaluacionDTO);
                return _unidadDeTrabajo.EvaluacioneDAL.Remove(evaluacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EvaluacioneDTO> ObtenerEvaluacionesPorSeccion(int seccionId)
        {
            try
            {
                var evaluaciones = _unidadDeTrabajo.EvaluacioneDAL.ObtenerEvaluacionesPorSeccion(seccionId);
                var evaluacionesDTO = evaluaciones.Select(e => ConvertToDTO(e)).ToList();
                return evaluacionesDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private EvaluacioneDTO ConvertToDTO(Evaluacione evaluacion)
        {
            return new EvaluacioneDTO
            {
                EvaluacionId = evaluacion.EvaluacionId,
                SeccionId = evaluacion.SeccionId ?? 0,
                TipEvaluacionId = evaluacion.TipEvaluacionId ?? 0,
                Porcentaje = evaluacion.Porcentaje
            };
        }

        private Evaluacione ConvertToEntity(EvaluacioneDTO evaluacionDTO)
        {
            return new Evaluacione
            {
                EvaluacionId = evaluacionDTO.EvaluacionId,
                SeccionId = evaluacionDTO.SeccionId,
                TipEvaluacionId = evaluacionDTO.TipEvaluacionId,
                Porcentaje = evaluacionDTO.Porcentaje
            };
        }
        #endregion
    }
}