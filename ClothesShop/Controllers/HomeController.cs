using ClothesShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly ShopContext _shopContext;

        public HomeController(ILogger<HomeController> logger,ShopContext shopContext)
        {
            _logger = logger;
            _shopContext = shopContext;
        }
        public List<Category> Category { get; set; }
        public List<Slide> Slides { get; set; }
        public IActionResult Index(string? searchString)
        {
            var query = (from p in _shopContext.Products
                         where p.Evaluate == 5
                        select p).Take(8).ToList();
            ViewData["query"] = query;
            if (!string.IsNullOrEmpty(searchString))
            {
                return LocalRedirect(Url.Action("Index","Shop"));
                
            }
            Slides = (from a in _shopContext.Slides
                     select a).ToList();
            ViewData["Slide"] = Slides;
            Category = (from a in _shopContext.Categories
                        where a.CateId < 3
                       select a).ToList();
            ViewData["category"] = Category;
            return View();
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
