using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones.ImplementacionesDeEntidades
{
    public class DALNotaImpl : DALGenericoImpl<Nota>, INotaDAL
    {
        SistemaCursosContext _context;

        public DALNotaImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }

        public new bool Add(Nota nota)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_CrearNota";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@EvaluacionId", SqlDbType.Int) { Value = nota.EvaluacionId });
                command.Parameters.Add(new SqlParameter("@InscripcionId", SqlDbType.Int) { Value = nota.InscripcionId });
                command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = nota.Total });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creando nota: {ex.Message}");
                return false;
            }
        }

        public new bool Update(Nota nota)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_EditarNota";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@NotaId", SqlDbType.Int) { Value = nota.NotaId });
                command.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = nota.Total });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando nota: {ex.Message}");
                return false;
            }
        }

        public new bool Remove(Nota nota)
        {
            try
            {
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_EliminarNota";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@NotaId", SqlDbType.Int) { Value = nota.NotaId });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando nota: {ex.Message}");
                return false;
            }
        }

        public List<Nota> ListarNotasPorInscripcion(int inscripcionId)
        {
            try
            {
                var notas = new List<Nota>();
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_ListarNotasPorInscripcion";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@InscripcionId", SqlDbType.Int) { Value = inscripcionId });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    notas.Add(new Nota
                    {
                        NotaId = reader.GetInt32("NotaId"),
                        EvaluacionId = reader.GetInt32("EvaluacionId"),
                        InscripcionId = inscripcionId,
                        Total = reader.GetDecimal("Total"),
                        CreatedAt = reader.IsDBNull("Created_at") ? null : reader.GetDateTime("Created_at"),
                        UpdatedAt = reader.IsDBNull("Updated_at") ? null : reader.GetDateTime("Updated_at")
                    });
                }
                return notas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo notas por inscripción: {ex.Message}");
            }
        }

        public List<Nota> ListarNotasPorSeccion(int seccionId)
        {
            try
            {
                var notas = new List<Nota>();
                using var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "sp_ListarNotasPorSeccion";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@SeccionId", SqlDbType.Int) { Value = seccionId });

                if (command.Connection?.State != ConnectionState.Open)
                    command.Connection!.Open();

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    notas.Add(new Nota
                    {
                        NotaId = reader.GetInt32("NotaId"),
                        EvaluacionId = reader.GetInt32("EvaluacionId"),
                        InscripcionId = reader.GetInt32("InscripcionId"),
                        Total = reader.GetDecimal("Total"),
                        CreatedAt = reader.IsDBNull("Created_at") ? null : reader.GetDateTime("Created_at"),
                        UpdatedAt = reader.IsDBNull("Updated_at") ? null : reader.GetDateTime("Updated_at")
                    });
                }
                return notas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo notas por sección: {ex.Message}");
            }
        }
    }
}