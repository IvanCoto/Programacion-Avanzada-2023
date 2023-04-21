using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.InputModels
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress, Display(Name = "Registered Email Address")]
        public string Email { get; set; }

        public bool EmailSent { get; set; }

        public string Token { get; set; }
       

    }
}
