using Invoices.BL.Helper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly IProductRep product;
        #endregion

        #region Ctor
        public ProductController(IProductRep product)
        {
            this.product = product;
        }
        #endregion

        #region API
        [HttpGet]
        [Route("~/Api/Product/Get")]
        public IActionResult Get()
        {
            try
            {
                var data = product.Get();
                return Ok(new ApiResponse<IEnumerable<ProductVM>>()
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
        [Route("~/Api/Product/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = product.GetById(id);
                return Ok(new ApiResponse<ProductVM>()
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
        [Route("~/Api/Product/Create")]
        public IActionResult Create(ProductVM model)
        {
            try
            {
                product.Create(model);
                return Ok(new ApiResponse<ProductVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "product Created Successfully",
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
        [Route("~/Api/Product/Edit")]
        public IActionResult Edit(ProductVM model)
        {
            try
            {
                product.Edit(model);
                return Ok(new ApiResponse<ProductVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "product Edited Successfully",
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
        [Route("~/Api/Product/Delete")]
        public IActionResult Delete(ProductVM model)
        {
            try
            {
                product.Delete(model);
                return Ok(new ApiResponse<ProductVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "product Deleted",
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
