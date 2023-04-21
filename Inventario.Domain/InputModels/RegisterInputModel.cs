using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.InputModels
{
    public class RegisterInputModel : LoginInputModel
    {
        [Required]
        [MaxLength(32)]
        [Compare("Password")]
        public String ConfirmPassword { get; set; }
    }
}
