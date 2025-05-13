using BackEnd.DTO;
using BackEnd.Servicios.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Servicios.Implementaciones
{
    public class CursoService : ICursoService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CursoService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public List<CursoDTO> GetAllCursos()
        {
            try
            {
                var cursos = _unidadDeTrabajo.CursoDAL.Get();
                var cursosDTO = cursos.Select(c => ConvertToDTO(c)).ToList();
                return cursosDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CursoDTO GetCursoById(int id)
        {
            try
            {
                var curso = _unidadDeTrabajo.CursoDAL.FindById(id);
                return ConvertToDTO(curso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddCurso(CursoDTO cursoDTO)
        {
            try
            {
                var curso = ConvertToEntity(cursoDTO);
                return _unidadDeTrabajo.CursoDAL.Add(curso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCurso(CursoDTO cursoDTO)
        {
            try
            {
                var curso = ConvertToEntity(cursoDTO);
                return _unidadDeTrabajo.CursoDAL.Update(curso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCurso(CursoDTO cursoDTO)
        {
            try
            {
                var curso = ConvertToEntity(cursoDTO);
                return _unidadDeTrabajo.CursoDAL.Remove(curso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Métodos Privados de Conversión
        private CursoDTO ConvertToDTO(Curso curso)
        {
            return new CursoDTO
            {
                CursoId = curso.CursoId,
                Nombre = curso.Nombre,
                Descripcion = curso.Descripcion,
                Creditos = curso.Creditos
            };
        }

        private Curso ConvertToEntity(CursoDTO cursoDTO)
        {
            return new Curso
            {
                CursoId = cursoDTO.CursoId,
                Nombre = cursoDTO.Nombre,
                Descripcion = cursoDTO.Descripcion,
                Creditos = cursoDTO.Creditos
            };
        }
        #endregion
    }
}
