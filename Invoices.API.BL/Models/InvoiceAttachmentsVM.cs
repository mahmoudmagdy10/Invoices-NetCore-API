using Invoices.DAL.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Models
{
    public class InvoiceAttachmentsVM
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        [Required, StringLength(50)]
        public string InvoiceNumber { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Required, FileExtensions(ErrorMessage ="This File Type Isn't Allowed")]
        public IFormFile Attachment { get; set; }


    }
}
