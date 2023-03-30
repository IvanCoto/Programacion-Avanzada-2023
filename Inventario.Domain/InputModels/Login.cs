using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.InputModels
{
    public class Login
    {
        [EmailAddress]
        [Required]
        [MaxLength(256)]

        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
    }
}
