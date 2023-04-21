using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels.Identity
{
    public class Permiso
    {
        [Key]
        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public List<Usuario> Usuarios { get; set; }

    }
}
