using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.API.BL.Models
{
    public class TokenVM
    {
        [Required(ErrorMessage ="Email Is Required"), EmailAddress(ErrorMessage = "Email Is Invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required"),MinLength(6, ErrorMessage = "Min Length IS 6 Chars")]
        public string Password { get; set; }
    }
}
