using DAL.Interfaces.InterfacesDeEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        ICursoDAL CursoDAL { get; }
        IEvaluacioneDAL EvaluacioneDAL { get; }
        IHorarioDAL HorarioDAL { get; }
        IInscripcioneDAL InscripcioneDAL { get; }
        INotaDAL NotaDAL { get; }
        ISeccioneDAL SeccioneDAL { get; }   
        ITipoEvaluacioneDAL TipoEvaluacioneDAL { get; }
        IUsuarioDAL UsuarioDAL { get; }
        void Complete();
    }
}
