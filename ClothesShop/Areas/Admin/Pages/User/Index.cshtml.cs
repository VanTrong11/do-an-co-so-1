using ClothesShop.Areas.Admin.Pages.Role;
using ClothesShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Areas.Admin.Pages.User
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly ShopContext _shopContext;
        private readonly UserManager<AppUser> _userManager;
        public IndexModel(ShopContext shopContext, UserManager<AppUser> userManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
        }
        [TempData]
        public string StatusMessage { set; get; }
        public List<Category> Category { get; set; }
        public List<UserAndRole> Users { get; set; }

        public class UserAndRole : AppUser
        {
            public string RoleNames { set; get; }
        }
        public async Task OnGet()
        {
            Category = await _shopContext.Categories.Select(p => p).Where(c => c.CateId < 3).ToListAsync();
            ViewData["category"] = Category;
            var qr1 = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
            var User1 = qr1.Select(u => new UserAndRole { Id = u.Id, Email = u.Email });
            Users =  User1.ToList();
            foreach(var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleNames = string.Join(", ", roles);
            }
        }
        public void OnPost() => RedirectToPage();
    }
}
