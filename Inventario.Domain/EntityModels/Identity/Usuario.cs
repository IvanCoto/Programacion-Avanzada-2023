using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels.Identity
{
    public class Usuario : IdentityUser
    {

        public List<Permiso> Permisos { get; set; }
    }
}
