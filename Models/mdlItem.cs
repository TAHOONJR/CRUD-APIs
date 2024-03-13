using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class mdlItem
    {
        public int CategoryId { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        public double Price { get; set; }
        public string Notes { get; set; }
        public IFormFile Image { get; set; }
    }
}