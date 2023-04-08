using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Models
{
    public class ReportSearchVM
    {
        public int SearchRadio { get; set; }
        public int InvoiceType { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int SectionId { get; set; }
        public int ProductId { get; set; }

    }
}
