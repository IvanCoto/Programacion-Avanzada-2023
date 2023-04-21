using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.InputModels
{
    public class LoginInputModel
    {
        [EmailAddress]
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Token { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
