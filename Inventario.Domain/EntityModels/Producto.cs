using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Inventario.Domain.EntityModels
{
    public class Producto
    {

        public Producto()
        {

        }

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
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [DisplayName("Precio del Costo")]
        public int PrecioCosto { get; set; }

        [Required]
        [DisplayName("Precio Publico")]
        public int PrecioPublico { get; set; }

        [Required]
        [DisplayName("Cantidad de Articulos")]
        public int CantidadArticulos { get; set; }

        


        //------Deserialización de Cliente---------------------------------------------------------------------------

        public static Producto Deserialize(string json)
        {
            var options =
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };

            return
                JsonSerializer.Deserialize<Producto>(json, options);
        }

    }

}
