using System.ComponentModel.DataAnnotations;

namespace AmazonAPI.Models
{
    public class Category
    {

        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; }
        public virtual ICollection<Product> ? Products { get; set; }
    }
}
