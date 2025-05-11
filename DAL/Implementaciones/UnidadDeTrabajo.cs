using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.InterfacesDeEntidades;

namespace DAL.Implementaciones
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public ICursoDAL CursoDAL { get; set; }
        public IHorarioDAL HorarioDAL { get; set; } 

        public IInscripcioneDAL InscripcioneDAL { get;set;}

        public INotaDAL NotaDAL { get; set; }

        public ISeccioneDAL SeccioneDAL { get; set; }

        public ITipoEvaluacioneDAL TipoEvaluacioneDAL { get; set; }
        public IUsuarioDAL UsuarioDAL { get; set; }
        public IEvaluacioneDAL EvaluacioneDAL { get; set; }


        SistemaCursosContext context;




        public UnidadDeTrabajo(ICursoDAL cursoDAL, SistemaCursosContext context
            , IEvaluacioneDAL evaluacioneDAL,
               IHorarioDAL horarioDAl,
               IInscripcioneDAL inscripcioneDAL,
               INotaDAL notaDAL,
               ISeccioneDAL seccioneDAL,
               ITipoEvaluacioneDAL tipoEvaluacioneDAL,
               IUsuarioDAL usuarioDAL
            )
        {
            CursoDAL = cursoDAL;
            this.context = context;
            EvaluacioneDAL = evaluacioneDAL;
            HorarioDAL = horarioDAl;
            InscripcioneDAL = inscripcioneDAL;
            NotaDAL = notaDAL;
            SeccioneDAL = seccioneDAL;
            TipoEvaluacioneDAL  = tipoEvaluacioneDAL;
            UsuarioDAL  = usuarioDAL;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public void Complete()
        {
            context.SaveChanges();
        }
    }
}
