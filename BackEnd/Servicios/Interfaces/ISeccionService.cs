using BackEnd.DTO;

namespace BackEnd.Servicios.Interfaces
{
    public interface ISeccionService
    {
        List<SeccioneDTO> GetAllSecciones();
        SeccioneDTO GetSeccionById(int id);
        bool AddSeccion(SeccioneDTO seccion);
        bool UpdateSeccion(SeccioneDTO seccion);
        bool DeleteSeccion(SeccioneDTO seccion);
        List<SeccioneDTO> GetSeccionesbyCarrera(string carrera);
    }
}
