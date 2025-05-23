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
    public class DALEvaluacioneImpl : DALGenericoImpl<Evaluacione>, IEvaluacioneDAL
    {
        SistemaCursosContext _context;
        public DALEvaluacioneImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }

        public List<Evaluacione> ObtenerEvaluacionesPorSeccion(int seccionId)
        {
            try
            {
                var query = "EXEC sp_ObtenerEvaluacionesPorSeccion @SeccionId";
                var parameter = new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = seccionId };
                var result = _context.Evaluaciones.FromSqlRaw(query, parameter);
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public new bool Add(Evaluacione evaluacion)
        {
            try
            {
                var query = "EXEC sp_InsertarEvaluacion @SeccionId, @TipEvaluacionId, @Porcentaje";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = evaluacion.SeccionId },
                    new SqlParameter("@TipEvaluacionId", System.Data.SqlDbType.Int) { Value = evaluacion.TipEvaluacionId },
                    new SqlParameter("@Porcentaje", System.Data.SqlDbType.Decimal) { Value = evaluacion.Porcentaje }
                };

                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Remove(Evaluacione evaluacion)
        {
            try
            {
                var query = "EXEC sp_EliminarEvaluacion @EvaluacionId";
                var parameter = new SqlParameter("@EvaluacionId", System.Data.SqlDbType.Int) { Value = evaluacion.EvaluacionId };

                _context.Database.ExecuteSqlRaw(query, parameter);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Update(Evaluacione evaluacion)
        {
            try
            {
                var query = "EXEC sp_ActualizarEvaluacion @EvaluacionId, @SeccionId, @TipEvaluacionId, @Porcentaje";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EvaluacionId", System.Data.SqlDbType.Int) { Value = evaluacion.EvaluacionId },
                    new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = evaluacion.SeccionId },
                    new SqlParameter("@TipEvaluacionId", System.Data.SqlDbType.Int) { Value = evaluacion.TipEvaluacionId },
                    new SqlParameter("@Porcentaje", System.Data.SqlDbType.Decimal) { Value = evaluacion.Porcentaje }
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