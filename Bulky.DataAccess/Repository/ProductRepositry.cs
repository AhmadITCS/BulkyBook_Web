using BulkyBook.DataAccess.Context;
using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public class ProductRepositry : Repository<Product>, IProductRepositry
    {
        private readonly MyContext _context;

        public ProductRepositry(MyContext context) : base(context)
        {
            _context = context;

        }
        public void UpDate(Product product)
        {
             var objFromDB=_context.products.FirstOrDefault(u => u.Id == product.Id);
            if (objFromDB != null) { 
            objFromDB.Author = product.Author;
                objFromDB.Description = product.Description;    
                objFromDB.ListPrice = product.ListPrice;
                objFromDB.Price = product.Price;
                objFromDB.Price50 = product.Price50;
                objFromDB.Price100 = product.Price100;
                objFromDB.CategoryId = product.CategoryId;
                objFromDB.Title = product.Title;
                objFromDB.ISBN = product.ISBN;
                if (product.ImageUrl != null) { 
                
                objFromDB.ImageUrl = product.ImageUrl;
                }
            
            
            }
        }
    }
}
