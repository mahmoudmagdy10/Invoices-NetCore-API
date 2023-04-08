using Invoices.BL.Helper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Invoices.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceAttachmentsController : ControllerBase
    {
        #region Fields
        private readonly IInvoiceAttachmentsRep invoiceAttachments;
        #endregion

        #region Ctor
        public InvoiceAttachmentsController(IInvoiceAttachmentsRep invoiceAttachments)
        {
            this.invoiceAttachments = invoiceAttachments;
        }

        #endregion

        #region API

        [HttpPost]
        [Route("~/Api/InvoiceAttachments/Create")]
        public IActionResult Create(InvoiceAttachmentsVM obj)
        {
            try
            {
                invoiceAttachments.Create(obj);

                return Ok(new ApiResponse<InvoiceVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "invoice Attachment is Created Successfully",
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


        [HttpGet]
        [Route("~/Api/InvoiceAttachments/GetInvoiceAttachments/{id}")]
        public IActionResult GetInvoiceAttachments(int id)
        {
            try
            {
                var data = invoiceAttachments.Get(a => a.InvoiceId == id);
                return Ok(new ApiResponse<IEnumerable<InvoiceAttachmentsVM>>()
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


        [HttpDelete]
        [Route("~/Api/InvoiceAttachments/Delete")]
        public IActionResult Delete(InvoiceAttachmentsVM obj)
        {
            try
            {
                invoiceAttachments.Delete(obj);
                return Ok(new ApiResponse<string>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Invoice Attachment Is Deleted",
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
