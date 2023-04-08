using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.BL.Models
{
    public class ResetPasswordVM
    {
        [MinLength(6, ErrorMessage = "Min Length Is 6")]
        [Required(ErrorMessage = "This Field Is Required")]
        public string Password { get; set; }

        [MinLength(6, ErrorMessage = "Min Length Is 6")]
        [Required(ErrorMessage = "This Field Is Required")]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
