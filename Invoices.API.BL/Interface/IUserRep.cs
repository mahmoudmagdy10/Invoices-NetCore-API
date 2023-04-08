using Invoices.API.BL.Models;
using Invoices.API.DAL.Extend;
using Invoices.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Interface
{
    public interface IUserRep
    {
        IEnumerable<ApplicationUser> Get();
        Task<ApplicationUser> GetById(string id);
        Task<AuthVM> Create(RegisterVM obj);
        Task<ApplicationUser> Edit(ApplicationUser obj);
        Task<ApplicationUser> Delete(ApplicationUser obj);
    }
}
