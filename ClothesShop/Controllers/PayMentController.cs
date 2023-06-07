using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ClothesShop.Controllers
{
    public class PayMentController : Controller
    {
        public readonly ShopContext _shopContext;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;

        public PayMentController(ShopContext shopContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
            [DisplayName("Họ và Tên")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage ="Vui lòng nhập địa chỉ nhà của bạn")]
            [DisplayName("Địa chỉ")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn")]
            [DisplayName("Số điện thoại")]
            public string PhoneNumber { get; set; }
        }
        public List<Category> Category { get; set; }
        public IActionResult Index()
        {
            //lấy loại sản phẩm
            Category = _shopContext.Categories.Where(p => p.CateId <3).ToList();
            ViewData["category"] = Category;
            //lấy người dùng hiện tại đang hoạt động
            var userid = _userManager.GetUserId(User);

            //lấy danh sách sản phẩm
            var product = _shopContext.Products.Select(p => p).ToList();
            ViewData["product"] = product;

            //lấy giỏ hảng theo người dùng đăng nhập
            var cart = _shopContext.Carts.Where(p => p.UserId == userid).ToList();
            ViewData["cart"] = cart;

            //tính tổng tiền trong giỏ hàng 
            var sum = _shopContext.Carts.Where(s => s.UserId == userid).Sum(x => x.ThanhTien);
            ViewBag.Sum = sum;
            return View();
        }
    }
}
