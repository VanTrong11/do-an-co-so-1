using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClothesShop.Areas.Admin.Pages.Role
{
    public class DeleteModel : RolePageModel
    {
        public DeleteModel(ShopContext shopContext, RoleManager<IdentityRole> roleManager) : base(shopContext, roleManager)
        {
        }
        public IdentityRole role { get; set; }

        public async Task<IActionResult> OnGet(string roleid)
        {
            Category = _shopcontext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;
            if (roleid == null ) return NotFound("Không tìm thấy role");
            role =  await _roleManager.FindByIdAsync(roleid);
            if(role == null)
            {
                return NotFound("Không tìm thấy role");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            Category = _shopcontext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;
            if (roleid == null) return  NotFound("Không timg thấy role");
             role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy role");
            var result = await _roleManager.DeleteAsync(role);
            if(result.Succeeded)
            {
                StatusMessage = $"Bạn vừa xóa: {role.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            return Page();
        }
    }
}
