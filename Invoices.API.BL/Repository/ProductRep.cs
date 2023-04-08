using AutoMapper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Invoices.DAL.Database;
using Invoices.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Products.BL.Repository
{
    public class ProductRep : IProductRep
    {
        #region Fields
        
        private readonly InvoiceContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public ProductRep(InvoiceContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
        public IEnumerable<ProductVM> Get(Expression<Func<Product, bool>> filter = null)
        {
            try
            {
                if (filter == null)
                {
                    var data = GetProducts();
                    var model = mapper.Map<IEnumerable<ProductVM>>(data);
                    return model;
                }
                else
                {
                    var data = db.Product.Where(filter);
                    var model = mapper.Map<IEnumerable<ProductVM>>(data);
                    return model;
                }
            }
            catch
            {
                return Enumerable.Empty<ProductVM>();
            }
        }


        public void Create(ProductVM obj)
        {
            var model = mapper.Map<Product>(obj);
            db.Product.Add(model);
            db.SaveChanges();

        }

        public void Delete(ProductVM obj)
        {
            var model = mapper.Map<Product>(obj);
            db.Product.Remove(model);
            db.SaveChanges();
        }

        public void Edit(ProductVM obj)
        {
            var model = mapper.Map<Product>(obj);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

        }


        public ProductVM GetById(int id)
        {
            try
            {
                var data = db.Product.Where(a => a.Id == id).FirstOrDefault();
                var model = mapper.Map<ProductVM>(data);
                return model;
            }
            catch
            {
                var EmptyModel = new ProductVM();
                return EmptyModel;
            }

        }

        #endregion

        #region Refactory Methods
        private IQueryable<Product> GetProducts()
        {
            return db.Product.Select(a => a);
        }

        #endregion
    }
}
