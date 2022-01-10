using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class ApplicationType
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Product> Product { get; set; } = new List<Product>();
    }
}
