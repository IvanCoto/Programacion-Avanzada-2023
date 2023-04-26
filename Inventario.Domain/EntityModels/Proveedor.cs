using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels
{
    public class Proveedor
    {

        public Proveedor()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        [DisplayName("Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        [Required]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        

        //------Deserialización de Cliente---------------------------------------------------------------------------

        public static Proveedor Deserialize(string json)
        {
            var options =
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };

            return
                JsonSerializer.Deserialize<Proveedor>(json, options);
        }
    }
}
