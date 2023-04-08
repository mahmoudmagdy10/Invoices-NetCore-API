using Invoices.API.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Interface
{
    public interface IAuthRep
    {
        //Task<AuthModel> RegisterAsync(RegisterationVM model);
        Task<AuthVM> GetTokenAsync(TokenVM model);

        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}
