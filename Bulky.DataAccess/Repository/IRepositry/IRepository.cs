using BulkyBook.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepositry
{

    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll(string? includeprop = null);
        T Get(Expression<Func<T, bool>> predicate, string? includeprop = null);
        void Add(T entity);

        void Delete(T entity);
        void deleteRange(IEnumerable<T> entities);

    }
}
