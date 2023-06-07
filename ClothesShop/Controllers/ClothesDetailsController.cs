using ClothesShop.Migrations;
using ClothesShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
namespace ClothesShop.Controllers
{
    public class ClothesDetailsController : Controller
    {
        private readonly ILogger<ClothesDetailsController> _logger;
        public readonly ShopContext _shopContext;
        public  UserManager<AppUser> _userManager;
        public  SignInManager<AppUser> _signInManager;
        public ClothesDetailsController(ILogger<ClothesDetailsController> logger, ShopContext shopContext , SignInManager<AppUser> signInManager , UserManager<AppUser> userManager)
        {
            _logger = logger;
            _shopContext = shopContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public class Input{
            public string Description { get; set; }
        }
        public List<Category> Category { get; set; }
        public List<AppUser> AppUser { get; set; }
        public Rating Rating { get; set; }
        public Product Products { get; set; }
        public IActionResult Index(int? id, int? totalStars)
        {
            //Load danh muc san pham
            Category = (from a in _shopContext.Categories
                        select a).ToList();
            ViewData["category"] = Category;

            //lấy sản phẩm theo người dùng link vào 
            Products = _shopContext.Products.Where(p => p.Id == id).FirstOrDefault();

            //Lấy sản phẩm cùng loại và bỏ đi sản phẩm có id người dùng xem chi tiết
            var products = (from p in _shopContext.Products
                            where p.CateId == Products.CateId && p.Id != Products.Id
                            select p).ToList();
            ViewData["products"] = products;

            var item = Rating;
            if (totalStars != null)
            {
                item = new Rating
                {
                    Star = (int)totalStars,
                    Name = _userManager.GetUserName(User),
                    Datetime = DateTime.Now.ToString(),
                    Id = (int)id,
                    UserId = _userManager.GetUserId(User)
                };
                _shopContext.Add(item);
                _shopContext.SaveChanges();
                var product = _shopContext.Products.Find(id);
                product.Evaluate = (int?)_shopContext.Rating.Where(r => r.Id == id).Average(s => s.Star);
                _shopContext.SaveChanges();
            }
            var ratings = (from r in _shopContext.Rating
                          where r.Id == id
                          select r).ToList();
            ViewData["ratings"] = ratings;
            var count = _shopContext.Rating.Where(r => r.Id == id).Count();
            ViewBag.Count = count;
            return View(Products);
        }
        

    }
}
