using Invoices.BL.Helper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        #region Fields
        private readonly IInvoiceDetailsRep invoiceDetails;
        #endregion

        #region Ctor
        public InvoiceDetailsController(IInvoiceDetailsRep invoiceDetails)
        {
            this.invoiceDetails = invoiceDetails;
        }

        #endregion

        #region API
        [HttpGet]
        [Route("~/Api/InvoiceDetails/Get")]
        public IActionResult Get()
        {
            try
            {
                var data = invoiceDetails.Get();
                return Ok(new ApiResponse<IEnumerable<InvoicesDetailsVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
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
        [Route("~/Api/InvoiceDetails/GetInvoiceDetails/{id}")]
        public IActionResult GetInvoiceDetails(int id)
        {
            try
            {
                var data = invoiceDetails.Get(a=>a.InvoiceId == id);
                return Ok(new ApiResponse<IEnumerable<InvoicesDetailsVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrived",
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

        #endregion
    }
}
