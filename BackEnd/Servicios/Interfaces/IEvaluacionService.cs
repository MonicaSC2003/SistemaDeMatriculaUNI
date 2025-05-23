using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface IEvaluacionService
    {
        List<EvaluacioneDTO> GetAllEvaluaciones();
        EvaluacioneDTO GetEvaluacionById(int id);
        bool AddEvaluacion(EvaluacioneDTO evaluacion);
        bool UpdateEvaluacion(EvaluacioneDTO evaluacion);
        bool DeleteEvaluacion(EvaluacioneDTO evaluacion);
        List<EvaluacioneDTO> ObtenerEvaluacionesPorSeccion(int seccionId);
    }
}
