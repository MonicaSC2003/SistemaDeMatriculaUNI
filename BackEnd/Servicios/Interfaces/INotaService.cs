using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface INotaService
    {
        List<NotaDTO> GetAllNotas();
        NotaDTO GetNotaById(int id);
        bool AddNota(NotaDTO nota);
        bool UpdateNota(NotaDTO nota);
        bool DeleteNota(NotaDTO nota);
        List<NotaDTO> GetNotasPorInscripcion(int inscripcionId);
        List<NotaDTO> GetNotasPorSeccion(int seccionId);
    }
}
