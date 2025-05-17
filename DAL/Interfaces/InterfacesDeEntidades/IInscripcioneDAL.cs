using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.InterfacesDeEntidades
{
    public interface IInscripcioneDAL : IDALGenerico<Inscripcione>
    {
        List<Inscripcione> GetInscripcionesPorUsuario(int id);
        List<Inscripcione> ListarUsuariosPorSeccion(int id);
    }
}
