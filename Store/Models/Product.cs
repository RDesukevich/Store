using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Category Type")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
