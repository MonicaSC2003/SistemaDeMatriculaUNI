using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.InterfacesDeEntidades
{
    public interface ISeccioneDAL : IDALGenerico<Seccione>
    {
        List<Seccione> GetSeccionesbyCarrera(string carrera);
    }
}
