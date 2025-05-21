using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface ITipoEvaluacionService
    {
        List<TipoEvaluacioneDTO> GetAllTiposEvaluacion();
        TipoEvaluacioneDTO GetTipoEvaluacionById(int id);
    }
}
