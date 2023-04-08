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
    public interface IInvoiceDetailsRep
    {
        IEnumerable<InvoicesDetailsVM> Get(Expression<Func<InvoicesDetails, bool>> filter = null);
        InvoicesDetailsVM GetById(int id);
        void Create(InvoicesDetailsVM obj);
        void Edit(InvoicesDetailsVM obj);
        void Delete(InvoicesDetailsVM obj);
    }
}
