using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DAL.Entity
{
    public class InvoicesDetails
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string InvoiceNumber { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; }

        [StringLength(100), AllowNull]
        public string Notes { get; set; }

        [StringLength(50)]
        public string User { get; set; } = "User";
        public int ValueStatus { get; set; }
        [AllowNull]
        public DateTime PayDate { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public decimal PartialPaiedAmount { get; set; } = 0;
        public decimal RestAmount { get; set; } = 0;
        public decimal AllocatedAmount { get; set; } = 0;

    }
}
