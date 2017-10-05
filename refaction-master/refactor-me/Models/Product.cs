using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 99999)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 99999)]
        public decimal DeliveryPrice { get; set; }
    }

 
}