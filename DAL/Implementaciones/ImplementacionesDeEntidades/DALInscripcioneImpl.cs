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
    public class DALInscripcioneImpl: DALGenericoImpl<Inscripcione>, IInscripcioneDAL
    {
        SistemaCursosContext _context;
        public DALInscripcioneImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }

        public List<Inscripcione> GetInscripcionesPorUsuario(int id)
        {
            var query = "EXEC sp_ObtenerInscripcionesPorUsuario @UsuarioId";
            var parameter = new SqlParameter("@UsuarioId", System.Data.SqlDbType.Int) { Value = id };
            var result = _context.Inscripciones.FromSqlRaw(query, parameter);
            return result.ToList();
        }

        public List<Inscripcione> ListarUsuariosPorSeccion(int id)
        {
            var query = "EXEC sp_ListarUsuariosPorSeccion @SeccionId";
            var parameter = new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = id };
            var result = _context.Inscripciones.FromSqlRaw(query, parameter) ;
            return result.ToList();
        }

        public new bool Add(Inscripcione inscripcione)
        {
            try
            {
                var query = "EXEC sp_CrearInscripcion @UsuarioId, @SeccionId";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioId", System.Data.SqlDbType.Int) { Value = inscripcione.UsuarioId },
                    new SqlParameter("@SeccionId", System.Data.SqlDbType.Int) { Value = inscripcione.SeccionId }
                };
                _context.Database.ExecuteSqlRaw(query, parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public new bool Remove(Inscripcione inscripcione)
        {
            try
            {
                var query = "EXEC sp_EliminarInscripcion @InscripcionId";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@InscripcionId", System.Data.SqlDbType.Int) { Value = inscripcione.InscripcionId }
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
