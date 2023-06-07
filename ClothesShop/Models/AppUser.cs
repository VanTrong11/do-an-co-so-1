        using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesShop.Models
{
    public class AppUser : IdentityUser
    {
        
        public string HomeAdress { set; get; }

        public string Name { set; get; }
    }
}
