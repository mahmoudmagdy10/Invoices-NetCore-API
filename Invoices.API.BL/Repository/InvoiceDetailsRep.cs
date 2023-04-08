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

namespace Invoices.BL.Repository
{
    public class InvoiceDetailsRep : IInvoiceDetailsRep
    {
        #region Fields
        
        private readonly InvoiceContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public InvoiceDetailsRep(InvoiceContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
        public IEnumerable<InvoicesDetailsVM> Get(Expression<Func<InvoicesDetails, bool>> filter = null)
        {
            try
            {
                if (filter == null)
                {
                    var data = GetInvoices();
                    var invoices = mapper.Map<IEnumerable<InvoicesDetailsVM>>(data);
                    return invoices;
                }
                else
                {
                    var data = db.InvoicesDetails.Where(filter);
                    var invoices = mapper.Map<IEnumerable<InvoicesDetailsVM>>(data);
                    return invoices;
                }
            }
            catch
            {
                return Enumerable.Empty<InvoicesDetailsVM>();
            }
        }

        public void Create(InvoicesDetailsVM obj)
        {
            var data = mapper.Map<InvoicesDetails>(obj);
            db.InvoicesDetails.Add(data);
            db.SaveChanges();
        }

        public void Delete(InvoicesDetailsVM obj)
        {
            var data = mapper.Map<InvoicesDetails>(obj);
            db.InvoicesDetails.Remove(data);
            db.SaveChanges();
        }

        public void Edit(InvoicesDetailsVM obj)
        {
            var data = mapper.Map<InvoicesDetails>(obj);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
        }


        public InvoicesDetailsVM GetById(int id)
        {
            try
            {
                var data = db.InvoicesDetails.Where(a => a.Id == id).FirstOrDefault();
                var model = mapper.Map<InvoicesDetailsVM>(data);
                return model;
            }
            catch
            {
                var EmptyModel = new InvoicesDetailsVM();
                return EmptyModel;
            }
        }

        #endregion

        #region Refactory Methods
        private IQueryable<InvoicesDetails> GetInvoices()
        {
            return db.InvoicesDetails.Select(a => a);
        }
        #endregion
    }
}
