using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Models
{
    public class Slide
    {
        [Key]
        public int Id { set; get; }
        public string? Img { set; get; }
    }
}
