using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones.ImplementacionesDeEntidades
{
   public class DALEvaluacioneImpl: DALGenericoImpl<Evaluacione>, IEvaluacioneDAL
    {
        SistemaCursosContext _context;
        public DALEvaluacioneImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }
    }
}
