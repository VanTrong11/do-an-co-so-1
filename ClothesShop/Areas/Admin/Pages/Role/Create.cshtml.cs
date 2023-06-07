using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Areas.Admin.Pages.Role
{
    public class CreateModel : RolePageModel
    {
        public CreateModel(ShopContext shopContext, RoleManager<IdentityRole> roleManager) : base(shopContext, roleManager)
        {
        }

        public class InputModel
        {
            [Display(Name ="Tên role")]
            [Required(ErrorMessage ="Phải nhập {0}")]
            [StringLength(256,MinimumLength = 3 , ErrorMessage ="{0} phải dài từ {2} đến {1} ký tự")]
            public string Name { get; set; }
        }

        [BindProperty]
        public InputModel input { get; set; }
        public void OnGet()
        {
            Category = _shopcontext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Category = _shopcontext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;
            if(!ModelState.IsValid)
            {
                return Page();
            }
            var newRole = new IdentityRole(input.Name);
            var result = await _roleManager.CreateAsync(newRole);
            if(result.Succeeded)
            {
                StatusMessage = $"Bạn vừa tạo role mới: {input.Name}";
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
