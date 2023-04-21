using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.InputModels
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password), MaxLength(32), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), MaxLength(32), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), MaxLength(32), Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Confirm New Password does not match with the New Password")]
        public string ConfirmNewPassword { get; set; }



    }
}
