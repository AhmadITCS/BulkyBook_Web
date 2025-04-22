using BulkyBook.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookDataAccess.Repository.UintOfWork
{
    public interface IUintOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepositry Product { get; }

        void Save();
    }
}
