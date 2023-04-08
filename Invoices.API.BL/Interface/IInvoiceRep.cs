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
    public interface IInvoiceRep
    {
        IEnumerable<InvoiceVM> Get(Expression<Func<Invoice, bool>> filter = null);
        InvoiceVM GetById(int id);
        Invoice Create(InvoiceVM obj);
        void Edit(InvoiceVM obj);
        void AfterPay(InvoiceVM obj);
        void Delete(InvoiceVM obj);
        IEnumerable<InvoiceVM> InvoicesByDate(ReportSearchVM model);
        IEnumerable<InvoiceVM> UsersReportsSearch(ReportSearchVM model);
    }
}
