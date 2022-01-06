using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class ApplicationType
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
