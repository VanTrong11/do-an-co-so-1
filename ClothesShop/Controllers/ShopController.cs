using ClothesShop.Helpers;
using ClothesShop.Migrations;
using ClothesShop.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ClothesShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        public readonly ShopContext _shopcontext;

        public const int ITEMS_PER_PAGE = 9;
        [BindProperty(SupportsGet=true ,Name = "p")]
        public int currentPage { get; set; }
        public int countPage { get; set; }

        public ShopController(ILogger<ShopController> logger, ShopContext shopContext)
        {
            _logger = logger;
            _shopcontext = shopContext;
        }
        public List<Product> Products { get; set; }
        public List<Category> Category { get; set; } 

        public async Task<IActionResult> Man(int id,string sortOrder , string? searchString)
        {
            Category = (from a in _shopcontext.Categories
                        where a.CateId < 3
                        select a).ToList();
            ViewData["category"] = Category;
            if (id == 3)
            {
                Products = await (from c in _shopcontext.Products
                            where c.CateId == id
                            select c).ToListAsync();
            }
            else if (id == 2 || id == 1)
            {
                Products = await (from c in _shopcontext.Products
                            where c.CateId == id
                            where c.Gender == null || c.Gender == true
                            select c).ToListAsync();
            }

                        //==================SearchString======================
            ViewBag.url = "Man";
            if (!string.IsNullOrEmpty(searchString))
            {
                Products = Products.Where(a => a.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                Products = Products.ToList();
            }

                        //==================SortOrder======================

            ViewBag.Price = sortOrder == "price" ? "price" : "price";
            ViewBag.Price_desc = sortOrder == "price_desc" ? "price_desc" : "price_desc";

            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo Title
                case "price_desc":
                    Products = Products.OrderByDescending(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
                // 3.2 Sắp tăng dần theo price
                case "price":
                    Products = Products.OrderBy(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
            }


                         //===================== Paging ========================

            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = searchString;
            int totalProduct = Products.Count;
            countPage = (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (currentPage > countPage)
            {
                currentPage = countPage;
            }
            var pagingmodel = new PagingModel()
            {
                currentpage = currentPage,
                countpage = countPage,
                generetUrl = (pag , sortOrder , searchString) => Url.Action("Man", new { p = pag , sortOrder = sortOrder , searchString  = searchString })
            };
            Products = Products.Skip((currentPage - 1) * 9).Take(ITEMS_PER_PAGE).ToList();
            ViewBag.pagingmodel = pagingmodel;
            return View("Index",Products);
        }


        public async Task<IActionResult> Women(int id , string sortOrder, string? searchString)
        {

            Category = (from a in _shopcontext.Categories
                        where a.CateId < 3
                        select a).ToList();
            ViewData["category"] = Category;
            ViewBag.url = "Women";
            if (id == 3)
            {
                Products = await (from c in _shopcontext.Products
                                  where c.CateId == id
                                  select c).ToListAsync();
            }
            else if (id == 2 || id == 1)
            {
                Products = await (from c in _shopcontext.Products
                                  where c.CateId == id
                                  where c.Gender == null || c.Gender == false
                                  select c).ToListAsync();
            }

                                //==================SearchString======================
            if (!string.IsNullOrEmpty(searchString))
            {
                Products = Products.Where(a => a.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                Products = Products.ToList();
            }


                                //==================SortOrder======================

            ViewBag.Price = sortOrder == "price" ? "price" : "price";
            ViewBag.Price_desc = sortOrder == "price_desc" ? "price_desc" : "price_desc";
            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo Title
                case "price_desc":
                    Products = Products.OrderByDescending(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
                // 3.2 Sắp tăng dần theo price
                case "price":
                    Products = Products.OrderBy(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
            }


                                //===================== Paging ========================

            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = searchString;
            int totalProduct = Products.Count;
            countPage = (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (currentPage > countPage)
            {
                currentPage = countPage;
            }
            var pagingmodel = new PagingModel()
            {
                currentpage = currentPage,
                countpage = countPage,
                generetUrl = (pag, sortOrder , searchString) => Url.Action("Women", new { p = pag, sortOrder = sortOrder , searchString = searchString })
            };
            Products = Products.Skip((currentPage - 1) * 9).Take(ITEMS_PER_PAGE).ToList();
            ViewBag.pagingmodel = pagingmodel;

            return View("Index",Products);
        }


        public async Task<IActionResult> Index(string? searchString ,string sortOrder)
        {
            Category = await (from a in _shopcontext.Categories
                        where a.CateId < 3
                        select a).ToListAsync();
            ViewData["category"] = Category;
            ViewBag.url = "Index";

            //==================SearchString======================
            
            if (!string.IsNullOrEmpty(searchString))
            {
                Products = await _shopcontext.Products.Where(a => a.Name.ToLower().Contains(searchString.ToLower())).ToListAsync();
            }
            else
            {
                Products = await _shopcontext.Products.ToListAsync();
            }


                                //==================SortOrder======================

            ViewBag.Price = sortOrder == "price" ? "price" : "price";
            ViewBag.Price_desc = sortOrder == "price_desc" ? "price_desc" : "price_desc";

            switch (sortOrder)
            {
                // 3.1 Nếu biến sortOrder sắp giảm thì sắp giảm theo Title
                case "price_desc":
                    Products = Products.OrderByDescending(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
                // 3.2 Sắp tăng dần theo price
                case "price":
                    Products = Products.OrderBy(p => p.Price - (p.Price * p.Discount)).ToList();
                    break;
            }

                                //===================== Paging ========================


            ViewBag.CurrentSort = sortOrder;
            ViewBag.SearchString = searchString;
            int totalProduct = Products.Count;
            countPage = (int)Math.Ceiling((double)totalProduct / ITEMS_PER_PAGE);
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (currentPage > countPage)
            {
                currentPage = countPage;
            }
            var pagingmodel = new PagingModel()
            {
                currentpage = currentPage,
                countpage = countPage,
                generetUrl = (pag, sortOrder,searchString) => Url.Action("Index", new { p = pag,sortOrder = sortOrder, searchString = searchString })
            };
            Products = Products.Skip((currentPage - 1) * 9).Take(ITEMS_PER_PAGE).ToList();
            ViewBag.pagingmodel = pagingmodel;

            return View(Products);

        }
    }
}
