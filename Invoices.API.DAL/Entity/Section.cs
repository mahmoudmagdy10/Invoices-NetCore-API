using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.DAL.Entity
{
    public  class Section
    {
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }
        
        [Required, StringLength(100), AllowNull]
        public string Description { get; set; }

        public ICollection<Product> Product { get; set; }

    }
}
