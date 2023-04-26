using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Proveedor")]
        [DisplayName("Proveedor")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Proveedor Proveedor { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Descripcion { get; set; }

        [Required]
        public int PrecioCosto { get; set; }

        [Required]
        public int PrecioPublico { get; set; }

        [Required]
        public int CantidadArticulos { get; set; }

    }

}
