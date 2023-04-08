using Invoices.BL.Models;
using Invoices.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Interface
{
    public interface IProductRep
    {
        IEnumerable<ProductVM> Get(Expression<Func<Product, bool>> filter = null);
        ProductVM GetById(int id);
        void Create(ProductVM obj);
        void Edit(ProductVM obj);
        void Delete(ProductVM obj);
    }
}
