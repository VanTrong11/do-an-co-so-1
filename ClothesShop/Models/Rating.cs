using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesShop.Models
{
    public class Rating
    {
        [Key]
        public int IdRating { get; set; }
        [Required]
        public int Star { set; get; }
        public string Name { get; set; }
        public string Datetime { get; set; }
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        public virtual Product product { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser appUser { get; set; }
    }
}
