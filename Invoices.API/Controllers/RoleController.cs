using Invoices.API.BL.Interface;
using Invoices.API.DAL.Extend;
using Invoices.BL.Helper;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRep role;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(IRoleRep role, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.role = role;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("~/Api/Role/Get")]
        public IActionResult Index()
        {
            try
            {
                var roles = role.Get();
                return Ok(new ApiResponse<IEnumerable<IdentityRole>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
                    Data = roles
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("~/Api/Role/Create")]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Data");

                var RoleModel = await role.Create(model);

                if (RoleModel is null)
                    return BadRequest("Failed To Create");

                return Ok(new ApiResponse<IdentityRole>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
                    Data = RoleModel
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("~/Api/Role/GetUserInRole/{RoleId}")]
        public async Task<IActionResult> GetUserInRole(string RoleId)
        {
            try
            {
                var model = await role.AddOrRemoveUsers(RoleId);
                return Ok(new ApiResponse<List<UserInRoleVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Error = ex.Message
                });
            }

        }
        [HttpPost]
        [Route("~/Api/Role/AddOrRemoveUsers")]
        public async Task<IActionResult> AddOrRemoveUsers([FromBody] List<UserInRoleVM> users, string RoleId)
        {
            try
            {
                var UserInRole = await role.EditUserInRole(users, RoleId);

                if (UserInRole is null)
                    return BadRequest("Failed To Edit Users In Role");

                return Ok(new ApiResponse<List<UserInRoleVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Successfully Edited Users In Role",
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("~/Api/Role/Delete")]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            try
            {
                var result = await role.Delete(model);

                if (result is null)
                    return BadRequest("Failed To Delete");

                return Ok(new ApiResponse<IdentityRole>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Role Is Deleted",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Error = ex.Message
                });
            }
        }
    }
}
