using AutoMapper;
using Invoices.BL.Helper;
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
    public class InvoiceAttachmentsRep : IInvoiceAttachmentsRep
    {
        #region Fields
        
        private readonly InvoiceContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public InvoiceAttachmentsRep(InvoiceContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
        public IEnumerable<InvoiceAttachmentsVM> Get(Expression<Func<InvoiceAttachments, bool>> filter = null)
        {
            if (filter == null)
            {
                var data = GetInvoices();
                var invoices = mapper.Map<IEnumerable<InvoiceAttachmentsVM>>(data);
                return invoices;
            }
            else
            {
                var data = db.InvoiceAttachments.Where(filter);
                var invoices = mapper.Map<IEnumerable<InvoiceAttachmentsVM>>(data);
                return invoices;
            }
        }

        public void Create(InvoiceAttachmentsVM obj)
        {
            var AttachmentName = UploadingService.UploadFile("/wwwroot/Files/InvoicesAttachments", obj.Attachment);
            var Invoice = db.Invoice.FirstOrDefault(a => a.InvoiceNumber == obj.InvoiceNumber);

            InvoiceAttachmentsVM AttachmentObj = new InvoiceAttachmentsVM()
            {
                FileName = AttachmentName,
                InvoiceNumber = obj.InvoiceNumber,
                InvoiceId = Invoice.Id
            };

            var data = mapper.Map<InvoiceAttachments>(AttachmentObj);
            db.InvoiceAttachments.Add(data);
            db.SaveChanges();
        }

        public void Delete(InvoiceAttachmentsVM obj)
        {
            var data = mapper.Map<InvoiceAttachments>(obj);

            UploadingService.RemoveFile("/wwwroot/Files/InvoicesAttachments/", obj.FileName);
            db.InvoiceAttachments.Remove(data);
            db.SaveChanges();
        }

        public void Edit(InvoiceAttachmentsVM obj)
        {
            var data = mapper.Map<InvoiceAttachments>(obj);
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
        }


        public InvoiceAttachmentsVM GetById(int id)
        {
            var data = db.InvoiceAttachments.Where(a => a.Id == id).FirstOrDefault();
            var model = mapper.Map<InvoiceAttachmentsVM>(data);
            return model;
        }

        #endregion

        #region Refactory Methods
        private IQueryable<InvoiceAttachments> GetInvoices()
        {
            return db.InvoiceAttachments.Select(a => a);
        }

        #endregion
    }
}
