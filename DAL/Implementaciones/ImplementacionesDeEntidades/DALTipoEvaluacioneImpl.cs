using DAL.Interfaces.InterfacesDeEntidades;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementaciones.ImplementacionesDeEntidades
{
    public class DALTipoEvaluacioneImpl : DALGenericoImpl<TipoEvaluacione>, ITipoEvaluacioneDAL
    {
        SistemaCursosContext _context;
        public DALTipoEvaluacioneImpl(SistemaCursosContext context) : base(context)
        {
            _context = context;
        }
    }
}
