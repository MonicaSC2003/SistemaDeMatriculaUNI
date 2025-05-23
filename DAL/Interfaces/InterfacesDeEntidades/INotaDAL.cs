using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.InterfacesDeEntidades
{
    public interface INotaDAL: IDALGenerico<Nota>
    {
        List<Nota> ListarNotasPorInscripcion(int inscripcionId);
        List<Nota> ListarNotasPorSeccion(int seccionId);
    }
}
