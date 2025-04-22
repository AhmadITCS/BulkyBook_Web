using BulkyBook.DataAccess.Context;
using BulkyBook.DataAccess.Repository;
using BulkyBook.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.UintOfWork
{
    public class UintOfWork : IUintOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public IProductRepositry Product { get; private set; }

        private readonly MyContext _context;
        public UintOfWork(MyContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product=new ProductRepositry(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
