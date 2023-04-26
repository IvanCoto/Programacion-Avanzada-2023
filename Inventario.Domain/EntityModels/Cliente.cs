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
    public class Cliente
    {
        public Cliente()
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

        [Required]
        [StringLength(256, MinimumLength = 2)]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        [DisplayName("País")]
        public string Pais { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Provincia { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        [DisplayName("Cantón")]
        public string Canton { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 2)]
        public string Distrito { get; set; }

       


        //------Deserialización de Cliente---------------------------------------------------------------------------

        public static Cliente Deserialize(string json)
        {
            var options =
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };

            return
                JsonSerializer.Deserialize<Cliente>(json, options);
        }
    }
}
