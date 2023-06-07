using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ClothesShop.Areas.Admin.Pages.Role
{
    public class RolePageModel: PageModel
    {
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly ShopContext _shopcontext;
        public List<Category> Category { get; set; }
        [TempData]
        public string StatusMessage { set; get; }
        public RolePageModel(ShopContext shopContext, RoleManager<IdentityRole> roleManager)
        {
            _shopcontext = shopContext;
            _roleManager = roleManager;
        }
    }
}
