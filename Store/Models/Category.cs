using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName ("Display Order")]
        [Required]
        [Range(1,int .MaxValue, ErrorMessage = "Display Order for category must be greater than 0")]
        public string DisplayOrder { get; set; }
        public List<Product> Product { get; set; } = new List<Product> ();
    }
}
