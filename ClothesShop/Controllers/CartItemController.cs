using ClothesShop.Migrations;
using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace ClothesShop.Controllers
{
    public class CartItemController : Controller
    {
        public readonly ShopContext _shopContext;
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public List<Category> Category { get; set; }
        public List<CartModel> cartModels { set; get; }
        public CartItemController(ShopContext shopContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _shopContext = shopContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            //lấy loại sản phẩm
            Category = (from a in _shopContext.Categories
                        where a.CateId < 3
                        select a).ToList();
            ViewData["category"] = Category;
            //lấy người dùng hiện tại đang hoạt động
            var userid = _userManager.GetUserId(User);

            //lấy danh sách sản phẩm 
            var z = _shopContext.Products.Select(p => p).ToList();
            ViewData["product"] = z;

            //lấy giỏ hảng theo người dùng đăng nhập
            cartModels = _shopContext.Carts.Where(p => p.UserId == userid).ToList();

            //tính tổng tiền trong giỏ hàng 
            var sum = _shopContext.Carts.Where(s => s.UserId == userid).Sum(x => x.ThanhTien);
            ViewBag.Sum = sum;
            return View(cartModels);
        }

        public IActionResult AddToCart(int id , int SoLuong)
        {
            
            var item = _shopContext.Carts.SingleOrDefault(p => p.Id == id);
            if(item == null)
            {
                if(SoLuong < 1)
                {
                    SoLuong = 1;
                    var product = _shopContext.Products.SingleOrDefault(p => p.Id == id);
                    var total = product.Price.Value - (product.Price.Value * product.Discount.Value);
                    item = new CartModel()
                    {
                        Price = total,
                        Quanlity = SoLuong,
                        ThanhTien = SoLuong * total,
                        Id = id,
                        UserId = _userManager.GetUserId(User),
                    };
                    _shopContext.Carts.Add(item);
                    _shopContext.SaveChanges();
                }
            }
            else
            {
                item.Quanlity++;
                _shopContext.SaveChanges();

            }
            
            return RedirectToAction("Index");
        }
        public IActionResult RemoveItem(int id) 
        {
            var item = _shopContext.Carts.SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                _shopContext.Carts.Remove(item);
            }
            _shopContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UpdateItem(int id, int SoLuong)
        {
            var item = _shopContext.Carts.SingleOrDefault(x => x.Id == id);
            var product = _shopContext.Products.SingleOrDefault(p => p.Id == id);
            var total = product.Price.Value - (product.Price.Value * product.Discount.Value);
            if (item != null)
            {
                item.Quanlity = SoLuong;
                item.ThanhTien = SoLuong * total;
                _shopContext.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
