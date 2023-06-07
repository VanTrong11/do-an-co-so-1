using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesShop.Models
{
    public class PayMent
    {
        [Key]
        public int PayMentId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public int Amount { get; set; }
        public string DateTime { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser appUser { get; set; }

    }
}
