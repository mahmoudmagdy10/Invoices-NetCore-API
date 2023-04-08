using Invoices.BL.Models;
using Invoices.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Interface
{
    public interface ISectionRep
    {
        IEnumerable<SectionVM> Get();
        SectionVM GetById(int id);
        void Create(SectionVM obj);
        void Edit(SectionVM obj);
        void Delete(SectionVM obj);
    }
}
