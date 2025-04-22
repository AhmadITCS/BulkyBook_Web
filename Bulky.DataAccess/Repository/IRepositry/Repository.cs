using BulkyBook.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.IRepositry
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyContext _context;
        internal DbSet<T> Set;
        public Repository(MyContext context)
        {
            _context = context;
            Set = _context.Set<T>();
            _context.products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
        }

        public void deleteRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> predicate, string? includeprop = null)
        {
            IQueryable<T> values = Set;
            values = values.Where(predicate);
            if (!string.IsNullOrEmpty(includeprop))
            {
                foreach (var item in includeprop
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    values = values.Include(item);
                }
            }
            return values.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeprop = null)
        {
            IQueryable<T> entities = Set;
            if (!string.IsNullOrEmpty(includeprop)) {
                foreach (var item in includeprop
                    .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    entities = entities.Include(item);
                }
            }
            return entities.ToList();
        }


    }
}
