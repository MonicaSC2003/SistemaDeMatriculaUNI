using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface ICursoService
    {
        List<CursoDTO> GetAllCursos();
        CursoDTO GetCursoById(int id);
        bool AddCurso(CursoDTO curso);
        bool UpdateCurso(CursoDTO curso);
        bool DeleteCurso(CursoDTO curso);
    }
}
