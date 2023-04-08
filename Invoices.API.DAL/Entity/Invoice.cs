using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DAL.Entity
{
    public class Invoice
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string InvoiceNumber { get; set; }
        
        [Required]
        public string RateVat { get; set; }
        
        [Required, StringLength(50)]
        public string Status { get; set; }

        [StringLength(100), AllowNull]
        public string Notes { get; set; }

        public string User { get; set; } = "User";
        public int ValueStatus { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        
        [AllowNull]
        public DateTime PayDate { get; set; }

        [Required,Precision(8,2)]
        public decimal Discount { get; set; }
        
        [Required, Precision(8, 2),AllowNull]
        public decimal AmountCollection { get; set; }
        
        [Required, Precision(8, 2)]
        public decimal AmountCommission { get; set; }
        
        [Required, Precision(8, 2)]
        public decimal ValueVat { get; set; }

        [Required, Precision(8, 2)]
        public decimal Total { get; set; }

        //public int SectionId { get; set; }
        //public Section Section { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<InvoicesDetails> InvoicesDetails { get; set; }
        public ICollection<InvoiceAttachments> InvoiceAttachments { get; set; }

        public decimal PartialPaiedAmount { get; set; } = 0;
        public decimal RestAmount { get; set; } = 0;
        public decimal AllocatedAmount { get; set; } = 0;

    }
}
