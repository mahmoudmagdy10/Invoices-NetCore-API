using AutoMapper;
using Azure.Core;
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
    public class InvoiceRep : IInvoiceRep
    {
        #region Fields
        
        private readonly InvoiceContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public InvoiceRep(InvoiceContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
        public IEnumerable<InvoiceVM> Get(Expression<Func<Invoice, bool>> filter = null)
        {
            try
            {
                if (filter == null)
                {
                    var data = GetInvoices();
                    var invoices = mapper.Map<IEnumerable<InvoiceVM>>(data);
                    return invoices;
                }
                else
                {
                    var data = db.Invoice.Where(filter);
                    var model = mapper.Map<IEnumerable<InvoiceVM>>(data);
                    return model;
                }
            }
            catch
            {
                return Enumerable.Empty<InvoiceVM>();
            }
            
        }
        public Invoice Create(InvoiceVM obj)
        {
            try
            {
                var data = mapper.Map<Invoice>(obj);
                db.Invoice.Add(data);
                db.SaveChanges();

                var AddedInvoice = db.Invoice.OrderBy(a => a.Id).Last();
                return AddedInvoice;
            }
            catch
            {
                var EmptyModel = new Invoice();
                return EmptyModel;
            }
        }
        public void Delete(InvoiceVM obj)
        {
            var data = mapper.Map<Invoice>(obj);
            db.Invoice.Remove(data);
            db.SaveChanges();
        }
        public void Edit(InvoiceVM obj)
        {
            var data = mapper.Map<Invoice>(obj);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void AfterPay(InvoiceVM obj)
        {
            var model = db.Invoice.Where(a => a.Id == obj.Id).FirstOrDefault();
            var data = mapper.Map<Invoice>(obj);

            if(data.ValueStatus == 2)
            {
                model.Status = data.Status;
                model.ValueStatus = data.ValueStatus;
                model.PayDate = data.PayDate;
                model.AllocatedAmount = data.AllocatedAmount;
                model.RestAmount = data.RestAmount;
                model.PartialPaiedAmount = data.PartialPaiedAmount;

                db.SaveChanges();
            }
            else if (data.ValueStatus == 3)
            {
                if(data.RestAmount == 0)
                {
                    model.Status = data.Status;
                    model.ValueStatus = data.ValueStatus;
                    model.PayDate = data.PayDate;
                    model.AllocatedAmount = data.AllocatedAmount;
                    model.PartialPaiedAmount = data.PartialPaiedAmount;
                    db.SaveChanges();
                }
                else
                {
                    model.Status = data.Status;
                    model.ValueStatus = data.ValueStatus;
                    model.PayDate = data.PayDate;
                    model.AllocatedAmount = data.AllocatedAmount;
                    model.RestAmount = data.RestAmount;
                    model.PartialPaiedAmount = data.PartialPaiedAmount;
                    db.SaveChanges();
                }

            }
        }
        public InvoiceVM GetById(int id)
        {
            try
            {
                var data = db.Invoice.Where(a => a.Id == id).FirstOrDefault();
                var model = mapper.Map<InvoiceVM>(data);
                return model;
            }
            catch
            {
                var EmptyModel = new InvoiceVM();
                return EmptyModel;
            }
        }
        public IEnumerable<InvoiceVM> InvoicesByDate(ReportSearchVM model)
        {
            try
            {
                var DefaultDate = new DateTime(0001, 01, 01);
                var Result = DateTime.Compare(model.StartAt, DefaultDate);

                if (model.InvoiceType != 0 && Result == 0)
                {
                    var ValueStatus = model.InvoiceType;
                    var objs = db.Invoice.Where(a => a.ValueStatus == ValueStatus);
                    var Invoices = mapper.Map<IEnumerable<InvoiceVM>>(objs);
                    return Invoices;
                }
                else
                {
                    var StartDate = model.StartAt;
                    var EndDate = model.EndAt;
                    var ValueStatus = model.InvoiceType;
                    var objs = db.Invoice.Where(a => a.ValueStatus == ValueStatus).Where(a => a.InvoiceDate >= StartDate).Where(a => a.InvoiceDate <= EndDate);
                    var Invoices = mapper.Map<IEnumerable<InvoiceVM>>(objs);
                    return Invoices;
                }
            }
            catch
            {
                return Enumerable.Empty<InvoiceVM>();
            }
        }
        public IEnumerable<InvoiceVM> UsersReportsSearch(ReportSearchVM model)
        {
            try
            {
                var DefaultDate = new DateTime(0001, 01, 01);
                var Result = DateTime.Compare(model.StartAt, DefaultDate);

                if (model.SectionId != 0 && model.ProductId != 0 && Result != 0)
                {
                    var StartDate = model.StartAt;
                    var EndDate = model.EndAt;
                    var objs = db.Invoice.Where(a => a.InvoiceDate >= StartDate).Where(a => a.InvoiceDate <= EndDate);
                    var Invoices = mapper.Map<IEnumerable<InvoiceVM>>(objs);
                    return Invoices;
                }
                else
                {
                    var SectionId = model.SectionId;
                    var ProductId = model.ProductId;
                    var objs = db.Invoice.Where(a => a.ProductId == ProductId);
                    var Invoices = mapper.Map<IEnumerable<InvoiceVM>>(objs);
                    return Invoices;
                }
            }
            catch
            {
                return Enumerable.Empty<InvoiceVM>();
            }
        }
        #endregion

        #region Refactory Methods
        private IQueryable<Invoice> GetInvoices()
        {
            return db.Invoice.Include("InvoicesDetails").Select(a => a);
        }

        #endregion
    }
}
