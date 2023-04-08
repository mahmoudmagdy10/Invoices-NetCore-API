using Invoices.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Models
{
    public class InvoiceVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "InvoiceNumber Is Required"), StringLength(50,ErrorMessage ="Max Length 50")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "RateVat Is Required")]
        public string RateVat { get; set; }

        [StringLength(100, ErrorMessage = "Max Length 50"), AllowNull]
        public string Notes { get; set; }
        public string User { get; set; } = "User";

        public string Status { get; set; } = "غير مدفوعة";
        public int ValueStatus { get; set; } = 1;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        [AllowNull]
        public DateTime PayDate { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required, AllowNull]
        public decimal AmountCollection { get; set; }

        [Required]
        public decimal AmountCommission { get; set; }

        [Required(ErrorMessage ="Value Vat Is Required")]
        public decimal ValueVat { get; set; }

        [Required(ErrorMessage = "Total Is Required")]
        public decimal Total { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public IFormFile Attachment { get; set; }

        public decimal PartialPaiedAmount { get; set; }
        public decimal RestAmount { get; set; }
        public decimal AllocatedAmount { get; set; }

    }
}
