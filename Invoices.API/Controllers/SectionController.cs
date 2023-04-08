using AutoMapper;
using Invoices.BL.Helper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        #region Fields
        
        private readonly ISectionRep section;

        #endregion

        #region Ctor
        public SectionController(ISectionRep section)
        {
            this.section = section;
        }
        #endregion
        #region API

        [HttpGet]
        [Route("~/Api/Section/Get")]
        public IActionResult Get()
        {
            try
            {
                var data = section.Get();
                return Ok(new ApiResponse<IEnumerable<SectionVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrive",
                    Data = data
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
        [Route("~/Api/Section/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = section.GetById(id);
                return Ok(new ApiResponse<SectionVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrive",
                    Data = data
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
        [Route("~/Api/Section/Create")]
        public IActionResult Create(SectionVM model)
        {
            try
            {
                section.Create(model);
                return Ok(new ApiResponse<SectionVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Section Created Successfully",
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Failed To Create",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("~/Api/Section/Edit")]
        public IActionResult Edit(SectionVM model)
        {
            try
            {
                section.Edit(model);
                return Ok(new ApiResponse<SectionVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Section Edited Successfully",
                    Data = model
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Failed To Create",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("~/Api/Section/Delete")]
        public IActionResult DeleteEmployee(SectionVM model)
        {
            try
            {
                section.Delete(model);
                return Ok(new ApiResponse<SectionVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Section Deleted",
                });

            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Failed To Delete",
                    Error = ex.Message
                });
            }
        }

        #endregion

    }
}
