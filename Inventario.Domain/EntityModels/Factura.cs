﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventario.Domain.EntityModels
{
    public class Factura
    {

        public Factura()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int CantidadProducto { get; set; }

        [Required]
        public int PrecioProducto { get; set; }

        [Required]
        public int TotalPagar { get; set; }

        //------Deserialización de Cliente---------------------------------------------------------------------------

        public static Factura Deserialize(string json)
        {
            var options =
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true };

            return
                JsonSerializer.Deserialize<Factura>(json, options);
        }



    }
}
