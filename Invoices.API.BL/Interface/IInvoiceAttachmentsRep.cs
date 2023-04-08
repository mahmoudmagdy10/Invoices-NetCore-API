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
    public interface IInvoiceAttachmentsRep
    {
        IEnumerable<InvoiceAttachmentsVM> Get(Expression<Func<InvoiceAttachments, bool>> filter = null);
        InvoiceAttachmentsVM GetById(int id);
        void Create(InvoiceAttachmentsVM obj);
        void Edit(InvoiceAttachmentsVM obj);
        void Delete(InvoiceAttachmentsVM obj);
    }
}
