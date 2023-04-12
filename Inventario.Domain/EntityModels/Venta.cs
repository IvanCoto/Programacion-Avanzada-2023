using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("IdFactura")]
        public int IdFactura { get; set; }

        public Factura Factura { get; set; }

        [Required]
        [ForeignKey("IdCliente")]
        public int IdCliente { get; set; }

        public Cliente Cliente { get; set; }

        [Required]
        public int MontoTotal { get; set; }
    }
}
