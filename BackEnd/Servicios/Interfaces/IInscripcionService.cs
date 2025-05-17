using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface IInscripcionService
    {
        bool AddInscripcion(InscripcioneDTO inscripcion);
        bool DeleteInscripcion(InscripcioneDTO inscripcion);
        List<InscripcioneDTO> GetInscripcionesPorUsuario(int id);
        List<InscripcioneDTO> ListarUsuariosPorSeccion(int id);
    }
}
