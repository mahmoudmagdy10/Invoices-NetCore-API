using Invoices.API.BL.Interface;
using Invoices.API.BL.Models;
using Invoices.API.DAL.Extend;
using Invoices.BL.Helper;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        #region Fields 

        private readonly IUserRep user;
        private readonly UserManager<ApplicationUser> userManager;

        #endregion

        #region Ctor 

        public UserController(IUserRep user, UserManager<ApplicationUser> userManager)
        {
            this.user = user;
            this.userManager = userManager;
        }

        #endregion

        #region Actions 

        [HttpGet]
        [Route("~/Api/User/Get")]
        public IActionResult Index()
        {
            try
            {
                var users = user.Get();
                return Ok(new ApiResponse<IEnumerable<ApplicationUser>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
                    Data = users
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
        
        //[HttpGet]
        //[Route("~/Api/User/GetById/{Id}")]
        //public IActionResult GetById(string Id)
        //{
        //    try
        //    {
        //        var obj = user.GetById(Id);

        //        return Ok(new ApiResponse<Task<ApplicationUser>()
        //        {
        //            Code = "200",
        //            Status = "OK",
        //            Message = "User Is Found",
        //            Data = obj
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ApiResponse<string>()
        //        {
        //            Code = "404",
        //            Status = "Not Found",
        //            Message = "Data Not Found",
        //            Error = ex.Message
        //        });
        //    }
        //}

        [HttpPost]
        [Route("~/Api/User/Create")]
        public async Task<IActionResult> Create(RegisterVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid Data");

                var AddUser = await user.Create(obj);

                if (AddUser is null)
                    return BadRequest("Failed To Create");

                return Ok(new ApiResponse<AuthVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "User Is Created",
                    Data = AddUser
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

        [HttpPut]
        [Route("~/Api/User/Edit")]
        public async Task<IActionResult> Edit(ApplicationUser obj)
        {
            try
            {
                var result = await user.Edit(obj);
                if (result is null)
                    return BadRequest("Failed To Edit");

                return Ok(new ApiResponse<ApplicationUser>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
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
        
        [HttpDelete]
        [Route("~/Api/User/Delete")]
        public async Task<IActionResult> Delete(ApplicationUser obj)
        {
            try
            {
                var result = await user.Delete(obj);

                if(result is not null)
                    return BadRequest("Failed To Delete");

                return Ok(new ApiResponse<ApplicationUser>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "User Is Deleted",
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

        #endregion
        }
}