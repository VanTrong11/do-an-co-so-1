using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Areas.Admin.Pages.User
{
    public class AddRoleModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ShopContext _shopContext;
        public readonly RoleManager<IdentityRole> _roleManager;
        [TempData]
        public string StatusMessage { set; get; }
        public List<Category> Category { get; set; }
        public AppUser Users { get; set; }
        [BindProperty]
        [DisplayName("Các role gán cho user")]
        public string[] RoleName { get; set; }
        public SelectList allRoles { get; set; }
        public AddRoleModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ShopContext shopContext,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _shopContext = shopContext;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Category =  _shopContext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;

            if(string.IsNullOrEmpty(id))
            {
                return NotFound("Không có user");
            }
            Users = await _userManager.FindByIdAsync(id);

            if(User == null)
            {
                return NotFound($"Không tìm thấy user , id ={id}.");
            }
            RoleName= (await _userManager.GetRolesAsync(Users)).ToArray<string>();
            List<string> rolename = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(rolename);
            
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            Category = _shopContext.Categories.Select(p => p).Where(c => c.CateId < 3).ToList();
            ViewData["category"] = Category;

            if (string.IsNullOrEmpty(id))
            {
                return NotFound("Không có user");
            }
            Users = await _userManager.FindByIdAsync(id);

            if (User == null)
            {
                return NotFound($"Không tìm thấy user , id ={id}.");
            }
            var OldRoleNames = (await _userManager.GetRolesAsync(Users)).ToArray();
            var deleteRoles = OldRoleNames.Where(r => !RoleName.Contains(r));
            var addRoles = RoleName.Where(r => !OldRoleNames.Contains(r));
            List<string> rolename = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(rolename);

            var resultDelete = await _userManager.RemoveFromRolesAsync(Users, deleteRoles);
            if(!resultDelete.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }
            var resultAdd = await _userManager.AddToRolesAsync(Users, addRoles);
            if (!resultAdd.Succeeded)
            {
                resultAdd.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }
            StatusMessage = $"Vừa cập nhật role cho user : {Users.UserName}";
            return RedirectToPage("/.Index");
        }
    }
}
