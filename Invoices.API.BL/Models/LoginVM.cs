using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Models
{
    public class LoginVM
    {
        [EmailAddress(ErrorMessage = "Invalid Mail")]
        [Required(ErrorMessage = "This Field Is Required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Min Length Is 6")]
        [Required(ErrorMessage = "This Field Is Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
