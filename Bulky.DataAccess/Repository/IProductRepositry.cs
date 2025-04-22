using BulkyBook.Models1;
using BulkyBookDataAccess.Repository.IRepositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository
{
    public interface IProductRepositry :IRepository<Product>
    {
        void UpDate(Product product);   
    }
}
