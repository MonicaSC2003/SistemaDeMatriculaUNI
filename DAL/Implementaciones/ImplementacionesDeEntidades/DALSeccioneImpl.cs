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
    public class DALSeccioneImpl : DALGenericoImpl<Seccione>, ISeccioneDAL
    {
        SistemaCursosContext _context;
        public DALSeccioneImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }

        public List<Seccione> GetAllSecciones()
        {
            var query = "EXEC sp_GetAllSecciones";
            var result = _context.Secciones.FromSqlRaw(query);
            return result.ToList();
        }

        public List<Seccione> GetSeccionesbyCarrera(string carrera)
        {
            var query = "EXEC sp_GetSeccionesByCarrera @Carrera";
            var parameter = new SqlParameter("@Carrera", System.Data.SqlDbType.NVarChar) { Value = carrera };
            var result = _context.Secciones.FromSqlRaw(query, parameter);
            return result.ToList();
        }

        public Seccione GetSeccionById(int id)
        {
            var query = "EXEC sp_GetSeccioneById @SeccionId";
            var parameter = new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = id };
            var result = _context.Secciones.FromSqlRaw(query, parameter);
            return result.FirstOrDefault();
        }

        public new bool Add(Seccione seccione)
        {
            try
            {
                var query = "EXEC sp_CreateSeccion @UsuarioID, @CursoID, @HorarioID, @Grupo, @Periodo, @CuposMax, @Carrera";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioID", System.Data.SqlDbType.Int) { Value = seccione.UsuarioId },
                    new SqlParameter("@CursoID", System.Data.SqlDbType.Int) { Value = seccione.CursoId },
                    new SqlParameter("@HorarioID", System.Data.SqlDbType.Int) { Value = seccione.HorarioId },
                    new SqlParameter("@Grupo", System.Data.SqlDbType.NVarChar, 20) { Value = seccione.Grupo },
                    new SqlParameter("@Periodo", System.Data.SqlDbType.NVarChar, 50) { Value = seccione.Periodo },
                    new SqlParameter("@CuposMax", System.Data.SqlDbType.Int) { Value = seccione.CuposMax },
                    new SqlParameter("@Carrera", System.Data.SqlDbType.NVarChar, 100) { Value = seccione.Carrera }
                };

                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Remove(Seccione seccione)
        {
            try
            {
                var query = "EXEC sp_DeleteSeccion @SeccioneID";
                var parameter = new SqlParameter("@SeccioneID", System.Data.SqlDbType.Int) { Value = seccione.SeccionId };
                _context.Database.ExecuteSqlRaw(query, parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Update(Seccione seccione)
        {
            try
            {
                var query = "EXEC sp_UpdateSeccion @SECCIONID, @USUARIOID, @CURSOID, @HORARIOID, @GRUPO, @PERIODO, @CARRERA, @CUPOSMAX";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@SECCIONID", System.Data.SqlDbType.Int) { Value = seccione.SeccionId },
                    new SqlParameter("@USUARIOID", System.Data.SqlDbType.Int) { Value = seccione.UsuarioId },
                    new SqlParameter("@CURSOID", System.Data.SqlDbType.Int) { Value = seccione.CursoId },
                    new SqlParameter("@HORARIOID", System.Data.SqlDbType.Int) { Value = seccione.HorarioId },
                    new SqlParameter("@GRUPO", System.Data.SqlDbType.NVarChar, 20) { Value = seccione.Grupo },
                    new SqlParameter("@PERIODO", System.Data.SqlDbType.NVarChar, 50) { Value = seccione.Periodo },
                    new SqlParameter("@CARRERA", System.Data.SqlDbType.NVarChar, 100) { Value = seccione.Carrera },
                    new SqlParameter("@CUPOSMAX", System.Data.SqlDbType.Int) { Value = seccione.CuposMax }
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
