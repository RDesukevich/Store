using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,int .MaxValue, ErrorMessage = "Display Order for category must be greater than 0")]
        public string DisplayOrder { get; set; }
    }
}
