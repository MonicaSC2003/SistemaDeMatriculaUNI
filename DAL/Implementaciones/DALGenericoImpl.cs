using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Implementaciones
{
    public class DALGenericoImpl<T> : IDALGenerico<T> where T : class
    {

        SistemaCursosContext _context;


        public DALGenericoImpl(SistemaCursosContext context)
        {
            _context = context;
        }

        public bool Add(T entity)
        {
            try
            {
                _context.Add(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Get()
        {
            var result = _context.Set<T>().ToList();
            return result;
        }

        public bool Remove(T entity)
        {
            try
            {
                _context.Set<T>().Attach(entity);
                _context.Set<T>().Remove(entity);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(T entity)
        {

            try
            {
                _context.Entry(entity).State = Microsoft
                                               .EntityFrameworkCore
                                                .EntityState
                                                .Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
