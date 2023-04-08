using Invoices.API.BL.Interface;
using Invoices.API.BL.Models;
using Invoices.API.DAL.Extend;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Repository
{
    public class UserRep : IUserRep
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        #endregion

        #region Ctor
        public UserRep(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        #endregion

        #region Actions

        public IEnumerable<ApplicationUser> Get()
        {
            try
            {
                var data = userManager.Users;
                return data;
            }
            catch
            {
                return Enumerable.Empty<ApplicationUser>();
            }
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                if (user is null)
                    return new ApplicationUser();

                return user;
            }
            catch
            {
                return new ApplicationUser();
            }
        }

        public async Task<AuthVM> Create(RegisterVM obj)
        {
            try
            {
                if (await userManager.FindByEmailAsync(obj.Email) is not null)
                    return new AuthVM { Message = "This Email Alredy Registered !" };

                if (await userManager.FindByNameAsync(obj.UserName) is not null)
                    return new AuthVM { Message = "This Username Alredy Registered !" };

                var user = new ApplicationUser()
                {
                    UserName = obj.UserName,
                    Email = obj.Email,
                    IsAgree = obj.IsAgree
                };

                var AddUser = await userManager.CreateAsync(user, obj.Password);

                if (!AddUser.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in AddUser.Errors)
                    {
                        errors += $"{error} -- ";
                    }
                    return new AuthVM { Message = errors };
                }

                await userManager.AddToRoleAsync(user, "User");
                return new AuthVM
                {
                    Email = user.Email,
                    Roles = new List<string> { "User" },
                    Username = user.UserName
                };
            }
            catch
            {
                return new AuthVM { Message = "Invalid Data !" };
            }


        }
        public async Task<ApplicationUser> Edit(ApplicationUser obj)
        {
            try
            {
                var data = await userManager.FindByIdAsync(obj.Id);

                if (data is null)
                    return obj;

                data.UserName = obj.UserName;
                data.Email = obj.Email;

                var result = await userManager.UpdateAsync(data);

                if (!result.Succeeded)
                    return obj;

                return data;
            }
            catch
            {
                var EmptyModel = new ApplicationUser();
                return EmptyModel;
            }

        }

        public async Task<ApplicationUser> Delete(ApplicationUser obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);
            if (user is null)
                return obj;

            var result =await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return obj;
            
            return new ApplicationUser();
        }


        #endregion
    }
}
