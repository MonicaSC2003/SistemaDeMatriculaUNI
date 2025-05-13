using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones.ImplementacionesDeEntidades
{
    public class DALCursoImpl : DALGenericoImpl<Curso>, ICursoDAL
    {
        SistemaCursosContext _context;
        public DALCursoImpl(SistemaCursosContext context) : base(context)
        {

            _context = context;

        }


        public List<Curso> GetAllCursos()
        {
            var query = "EXEC sp_GetAllCursos";
            var result = _context.Cursos.FromSqlRaw(query);
            return result.ToList();
        }

        public Curso GetCursoById(int id)
        {
            var query = "EXEC sp_GetCursoById @CursoId";
            var parameter = new SqlParameter("@CursoId", System.Data.SqlDbType.Int) { Value = id };
            var result = _context.Cursos.FromSqlRaw(query, parameter).AsEnumerable().FirstOrDefault();
            return result;
        }

        public new bool Add(Curso curso)
        {
            try
            {
                var query = "EXEC sp_CreateCurso @Nombre, @Descripcion, @Creditos";
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@Nombre", System.Data.SqlDbType.NVarChar) { Value = curso.Nombre },
                new SqlParameter("@Descripcion", System.Data.SqlDbType.NVarChar) { Value = curso.Descripcion },
                new SqlParameter("@Creditos", System.Data.SqlDbType.Int) { Value = curso.Creditos }
                };
                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Remove(Curso curso)
        {
            try
            {
                var query = "EXEC sp_DeleteCurso @CursoId";
                var parameter = new SqlParameter("@CursoId", System.Data.SqlDbType.Int) { Value = curso.CursoId };
                _context.Database.ExecuteSqlRaw(query, parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Curso curso)
        {
            try
            {
                var query = "EXEC sp_UpdateCurso @CursoId, @Nombre, @Descripcion, @Creditos";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@CursoId", System.Data.SqlDbType.Int) { Value = curso.CursoId },
                    new SqlParameter("@Nombre", System.Data.SqlDbType.NVarChar) { Value = curso.Nombre },
                    new SqlParameter("@Descripcion", System.Data.SqlDbType.NVarChar) { Value = curso.Descripcion },
                    new SqlParameter("@Creditos", System.Data.SqlDbType.Int) { Value = curso.Creditos }
                };
                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
