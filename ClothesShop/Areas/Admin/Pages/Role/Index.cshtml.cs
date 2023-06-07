using ClothesShop.Controllers;
using ClothesShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Areas.Admin.Pages.Role
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : RolePageModel
    {
        public IndexModel(ShopContext shopContext, RoleManager<IdentityRole> roleManager) : base(shopContext, roleManager)
        {
        }

        public List<IdentityRole> roles { get; set; }
        public async Task OnGet()
        {
            Category =  await _shopcontext.Categories.Select(p => p).Where(c => c.CateId < 3).ToListAsync();
            ViewData["category"] = Category;
            roles = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }
        public void OnPost() => RedirectToPage();
    }
}
