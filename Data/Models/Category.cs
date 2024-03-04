#nullable enable

using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}