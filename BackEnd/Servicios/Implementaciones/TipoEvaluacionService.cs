using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class TipoEvaluacionService : ITipoEvaluacionService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public TipoEvaluacionService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<TipoEvaluacioneDTO> GetAllTiposEvaluacion()
        {
            try
            {
                var tiposEvaluacion = _unidadDeTrabajo.TipoEvaluacioneDAL.Get();
                var tiposEvaluacionDTO = tiposEvaluacion.Select(t => ConvertToDTO(t)).ToList();
                return tiposEvaluacionDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TipoEvaluacioneDTO GetTipoEvaluacionById(int id)
        {
            try
            {
                var tipoEvaluacion = _unidadDeTrabajo.TipoEvaluacioneDAL.FindById(id);
                return tipoEvaluacion != null ? ConvertToDTO(tipoEvaluacion) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private TipoEvaluacioneDTO ConvertToDTO(TipoEvaluacione tipoEvaluacion)
        {
            return new TipoEvaluacioneDTO
            {
                TipEvaluacionId = tipoEvaluacion.TipEvaluacionId,
                Nombre = tipoEvaluacion.Nombre,
                Descripcion = tipoEvaluacion.Descripcion
            };
        }
        #endregion
    }
}