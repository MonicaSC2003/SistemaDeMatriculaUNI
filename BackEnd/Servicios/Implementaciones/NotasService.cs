using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class NotaService : INotaService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public NotaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<NotaDTO> GetAllNotas()
        {
            try
            {
                var notas = _unidadDeTrabajo.NotaDAL.Get();
                var notasDTO = notas.Select(n => ConvertToDTO(n)).ToList();
                return notasDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotaDTO GetNotaById(int id)
        {
            try
            {
                var nota = _unidadDeTrabajo.NotaDAL.FindById(id);
                return nota != null ? ConvertToDTO(nota) : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddNota(NotaDTO notaDTO)
        {
            try
            {
                var nota = ConvertToEntity(notaDTO);
                var result = _unidadDeTrabajo.NotaDAL.Add(nota);
                if (result)
                {
                    _unidadDeTrabajo.Complete();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateNota(NotaDTO notaDTO)
        {
            try
            {
                var nota = ConvertToEntity(notaDTO);
                var result = _unidadDeTrabajo.NotaDAL.Update(nota);
                if (result)
                {
                    _unidadDeTrabajo.Complete();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNota(NotaDTO notaDTO)
        {
            try
            {
                var nota = ConvertToEntity(notaDTO);
                var result = _unidadDeTrabajo.NotaDAL.Remove(nota);
                if (result)
                {
                    _unidadDeTrabajo.Complete();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotaDTO> GetNotasPorInscripcion(int inscripcionId)
        {
            try
            {
                var notas = _unidadDeTrabajo.NotaDAL.ListarNotasPorInscripcion(inscripcionId);
                var notasDTO = notas.Select(n => ConvertToDTO(n)).ToList();
                return notasDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotaDTO> GetNotasPorSeccion(int seccionId)
        {
            try
            {
                var notas = _unidadDeTrabajo.NotaDAL.ListarNotasPorSeccion(seccionId);
                var notasDTO = notas.Select(n => ConvertToDTO(n)).ToList();
                return notasDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private NotaDTO ConvertToDTO(Nota nota)
        {
            return new NotaDTO
            {
                NotaId = nota.NotaId,
                EvaluacionId = nota.EvaluacionId ?? 0,
                InscripcionId = nota.InscripcionId ?? 0,
                Total = nota.Total
            };
        }

        private Nota ConvertToEntity(NotaDTO notaDTO)
        {
            return new Nota
            {
                NotaId = notaDTO.NotaId,
                EvaluacionId = notaDTO.EvaluacionId,
                InscripcionId = notaDTO.InscripcionId,
                Total = notaDTO.Total
            };
        }
        #endregion
    }
}