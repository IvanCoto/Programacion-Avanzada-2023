using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels
{
    public class Venta
    {
        public Venta()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Factura")]
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        public int MontoTotal { get; set; }



        //------Deserialización ---------------------------------------------------------------------------

        public static Venta Deserialize(string json)
        {
            var options =
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };

            return
                JsonSerializer.Deserialize<Venta>(json, options);
        }


    }

}
