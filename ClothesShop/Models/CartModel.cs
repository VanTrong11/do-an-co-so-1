using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesShop.Models
{
    public class CartModel
    {
        [Key]
        public int IdCart { get; set; }
        public decimal Price { get; set; }
        public int Quanlity { get; set; }
        public decimal ThanhTien { get; set; }
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Product product { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser appUser { get; set; }
    }
}
