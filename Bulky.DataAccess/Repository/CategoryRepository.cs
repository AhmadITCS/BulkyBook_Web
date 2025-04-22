using BulkyBook.DataAccess.Context;
using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.IRepositry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private readonly MyContext _context;
        
        public CategoryRepository(MyContext context):base(context) 
        {
            _context = context;
          
        }
       

        public void UpDate(Category category)
        {
           _context.Update(category);
        }

       
    }
}
