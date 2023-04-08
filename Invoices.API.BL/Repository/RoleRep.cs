using Invoices.API.BL.Interface;
using Invoices.API.BL.Models;
using Invoices.API.DAL.Extend;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Repository
{
    public class RoleRep : IRoleRep
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        #endregion

        #region Ctor
        public RoleRep(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        #endregion

        #region Actions

        public IEnumerable<IdentityRole> Get()
        {
            try
            {
                var roles = roleManager.Roles;
                return roles;
            }
            catch
            {
                return Enumerable.Empty<IdentityRole>();
            }
        }

        public async Task<IdentityRole> Create(IdentityRole model)
        {
            try
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper()
                };

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                    return new IdentityRole();

                return role;
            }
            catch
            {
                return new IdentityRole();
            }

        }

        public async Task<List<UserInRoleVM>> AddOrRemoveUsers(string RoleId)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(RoleId);
                var Users = new List<UserInRoleVM>();

                if (role is null)
                    return new List<UserInRoleVM>();

                foreach (var user in userManager.Users)
                {
                    var UserInRole = new UserInRoleVM()
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        UserInRole.IsSelected = true;
                    }
                    else
                    {
                        UserInRole.IsSelected = false;
                    }

                    Users.Add(UserInRole);

                }

                return Users;
            }
            catch
            {
                 return new List<UserInRoleVM>();
            }

        }

        public async Task<IdentityRole> GetById(string Id)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(Id);

                if (role is null)
                    return new IdentityRole();

                return role;
            }
            catch (Exception)
            {
                return new IdentityRole();
            }
        }

        public async Task<IdentityRole> Delete(IdentityRole model)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(model.Id);

                if (role is null)
                    return new IdentityRole();

                var result = await roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                    return model;

                return role;
            }
            catch (Exception)
            {
                return new IdentityRole();
            }
 
        }

        public async Task<UserInRoleVM> EditUserInRole(List<UserInRoleVM> model, string RoleId)
        {
            var role = await roleManager.FindByIdAsync(RoleId);

            if (role is null)
                return new UserInRoleVM() { Message = "No Role With Role" };

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);


                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (model[i].IsSelected == false && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < model.Count)
                    continue;
            }
            return new UserInRoleVM() { Message = "Successfully Editing" };
        }
        #endregion
    }
}
