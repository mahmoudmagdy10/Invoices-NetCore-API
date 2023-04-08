using Invoices.BL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Interface
{
    public interface IRoleRep
    {
        IEnumerable<IdentityRole> Get();
        Task<IdentityRole> Create(IdentityRole model);
        Task<List<UserInRoleVM>> AddOrRemoveUsers(string RoleId);
        Task<UserInRoleVM> EditUserInRole(List<UserInRoleVM> model, string RoleId);
        Task<IdentityRole> GetById(string Id);
        Task<IdentityRole> Delete(IdentityRole model);
    }
}
