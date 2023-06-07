using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Models
{
    public class Category
    {
        [Key]
        public int CateId { get; set; }
        public string? CateName { get; set; }
    }
}
