using Invoices.BL.Helper;
using Invoices.BL.Interface;
using Invoices.BL.Models;
using Invoices.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Invoices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        #region Fields
        private readonly IInvoiceRep invoice;
        private readonly IInvoiceDetailsRep invoiceDetails;
        private readonly IInvoiceAttachmentsRep invoiceAttachments;
        #endregion

        #region Ctor
        public InvoiceController(IInvoiceRep invoice, IInvoiceDetailsRep invoiceDetails, IInvoiceAttachmentsRep invoiceAttachments)
        {
            this.invoice = invoice;
            this.invoiceDetails = invoiceDetails;
            this.invoiceAttachments = invoiceAttachments;
        }

        #endregion

        #region API
        [HttpGet]
        [Route("~/Api/Invoice/Get")]
        public IActionResult Get()
        {
            try
            {
                var data = invoice.Get();
                return Ok(new ApiResponse<IEnumerable<InvoiceVM>>()
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
        [Route("~/Api/Invoice/GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = invoice.GetById(id);
                return Ok(new ApiResponse<InvoiceVM>()
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
        [Route("~/Api/Invoice/Create")]
        public IActionResult Create(InvoiceVM obj)
        {
            try
            {
                obj.RestAmount = obj.AmountCollection;

                var Invoice = invoice.Create(obj);

                var NewInvoiceDetailsObj = new InvoicesDetailsVM()
                {
                    InvoiceId = Invoice.Id,
                    InvoiceNumber = obj.InvoiceNumber,
                    Notes = obj.Notes,
                    PayDate = obj.PayDate
                };
                invoiceDetails.Create(NewInvoiceDetailsObj);

                var NewInvoiceAttachmentsObj = new InvoiceAttachmentsVM()
                {
                    InvoiceId = Invoice.Id,
                    InvoiceNumber = obj.InvoiceNumber,
                    Attachment = obj.Attachment,
                };
                invoiceAttachments.Create(NewInvoiceAttachmentsObj);

                return Ok(new ApiResponse<string>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Invoice and It's Details is Created Successfully",
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
        [Route("~/Api/Invoice/Edit")]
        public IActionResult Edit(InvoiceVM model)
        {
            try
            {
                invoice.Edit(model);
                return Ok(new ApiResponse<InvoiceVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Invoice Is Edited Successfully",
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
        [Route("~/Api/Invoice/Delete")]
        public IActionResult Delete(InvoiceVM model)
        {
            try
            {
                invoice.Delete(model);
                return Ok(new ApiResponse<string>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Invoice Is Deleted",
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

        [HttpPost]
        [Route("~/Api/Invoice/Payment")]
        public IActionResult Payment(InvoiceVM obj)
        {
            var OldInvoice = invoice.GetById(obj.Id);
            try
            {
                if (obj.ValueStatus == 2)
                {
                    var NewInvoiceObj = new InvoiceVM()
                    {
                        Id = obj.Id,
                        Status = "مدفوعة",
                        ValueStatus = 2,
                        PayDate = obj.PayDate
                    };

                    var NewInvoiceDetailsObj = new InvoicesDetailsVM()
                    {
                        InvoiceId = obj.Id,
                        InvoiceNumber = obj.InvoiceNumber,
                        Notes = obj.Notes,
                        Status = "مدفوعة",
                        ValueStatus = 2,
                        PayDate = obj.PayDate
                    };

                    invoice.AfterPay(NewInvoiceObj);
                    invoiceDetails.Create(NewInvoiceDetailsObj);

                    return Ok(new ApiResponse<InvoiceVM>()
                    {
                        Code = "200",
                        Status = "OK",
                        Message = "The Invoice Is Paied",
                    });
                }

                else if (obj.ValueStatus == 3)
                {

                    var NewAllocatedAmount = OldInvoice.AllocatedAmount + obj.PartialPaiedAmount;

                    if (obj.RestAmount == 0)
                    {
                        var NewInvoiceDetailsObj = new InvoicesDetailsVM()
                        {
                            InvoiceId = obj.Id,
                            InvoiceNumber = obj.InvoiceNumber,
                            Notes = obj.Notes,
                            Status = "مدفوعة",
                            ValueStatus = 2,
                            PayDate = obj.PayDate,
                            AllocatedAmount = NewAllocatedAmount,
                            RestAmount = obj.RestAmount,
                            PartialPaiedAmount = obj.PartialPaiedAmount
                        };
                        var NewInvoiceObj = new InvoiceVM()
                        {
                            Id = obj.Id,
                            Status = "مدفوعة",
                            ValueStatus = 2,
                            PayDate = obj.PayDate,
                            AllocatedAmount = NewAllocatedAmount,
                            RestAmount = obj.RestAmount,
                            PartialPaiedAmount = obj.PartialPaiedAmount
                        };

                        invoice.AfterPay(NewInvoiceObj);
                        invoiceDetails.Create(NewInvoiceDetailsObj);

                        return Ok(new ApiResponse<InvoiceVM>()
                        {
                            Code = "200",
                            Status = "OK",
                            Message = "The Invoice Is Paied",
                        });
                    }
                    else
                    {
                        var NewInvoiceDetailsObj = new InvoicesDetailsVM()
                        {
                            InvoiceId = obj.Id,
                            InvoiceNumber = obj.InvoiceNumber,
                            Notes = obj.Notes,
                            Status = "مدفوع جزئياً",
                            ValueStatus = 3,
                            PayDate = obj.PayDate,
                            AllocatedAmount = NewAllocatedAmount,
                            RestAmount = obj.RestAmount,
                            PartialPaiedAmount = obj.PartialPaiedAmount
                        };

                        var NewInvoiceObj = new InvoiceVM()
                        {
                            Id = obj.Id,
                            Status = "مدفوع جزئياً",
                            ValueStatus = 3,
                            PayDate = obj.PayDate,
                            AllocatedAmount = NewAllocatedAmount,
                            RestAmount = obj.RestAmount,
                            PartialPaiedAmount = obj.PartialPaiedAmount
                        };

                        invoice.AfterPay(NewInvoiceObj);
                        invoiceDetails.Create(NewInvoiceDetailsObj);

                        return Ok(new ApiResponse<InvoiceVM>()
                        {
                            Code = "200",
                            Status = "OK",
                            Message = "The Invoice Is Partially Paied",
                        });
                    }
                }

                return Ok(new ApiResponse<InvoiceVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "The Payment Process Is Done Successfully",
                });
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<string>()
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Payment Process Is Failed",
                    Error = ex.Message
                });
            }

            #endregion
        }

    }
}
