using BAW.Models;
using BAW.Services;
using BAW.ViewModels;
using Microsoft.AspNet.Identity;
using Sokuuhotu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BAW.Controllers
{
    public class StoreController : Controller
    {


        private readonly StoreService _service;

        private string userId;

        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = User.Identity.GetUserId();
            }
        }



        public StoreController(StoreService service)
        {
            _service = service;
            
        }


        public StoreController() : this(new StoreService())
        {

        }



        // GET: Store/products
        public async Task<ActionResult> Index()
        {
            var products = await _service.ProductsAsync();

            var categories = await _service.CategoriesAsync();


            var vm = new CategoryProductVM()
            {
                Categories = categories,
                Products = products
            };


            return View(vm);
        }

        // Get: Store/productdetail
        public async Task<ActionResult> ProductDetails(int id)
        {
            var product = await _service.ProductByIdAsync(id);


            var t = TempData["Message"];

            if (t != null)

                ViewData["Message"] = t;

            return View(product);
        }


        public async Task<ActionResult> ProductsByCategory(int CategoryId)
        {
            var products = await _service.ProductsByCategoryIdAsync(CategoryId);


            return View(products);

        }


        //[Display(Name = "Add-Item")]
        //[HttpGet]
        //public ActionResult AddProduct()
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    items.Add(new SelectListItem { Text = "1 Day", Value = "1" , Selected = true });
        //    items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
        //    items.Add(new SelectListItem { Text = "1 week", Value = "7",  });
        //    items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });



        //    ViewBag.closeDate = items;
        //    return View();
        //}

        //[Display(Name ="Add-Item")]
        //public async Task<ActionResult> AddProduct(Product model, int closeinDays)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        List<SelectListItem> items = new List<SelectListItem>();
        //        items.Add(new SelectListItem { Text = "1 Day", Value = "1", Selected = true });
        //        items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
        //        items.Add(new SelectListItem { Text = "1 week", Value = "7", });
        //        items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });

        //        ViewBag.closeDate = items;

        //        return View(model);

        //    }

        //    await _service.AddProductAsync(model ,UserId, closeinDays);


        //    return RedirectToAction("PostedProduct");

        //}




        [HttpGet]
        public ActionResult Search()
        {
            return PartialView("_SearchBarPartial");
        }




        [HttpPost]
        public async Task<ActionResult> Search(string search)
        {
            if (search != null)
            {
                try
                {
                    var searchlist = await _service.SearchAsync(search);

                    return PartialView("ProductsByCategory", searchlist);
                }
                catch (Exception e)
                {
                    // handle exception
                }
            }
            return PartialView("Error");
        }


        public async Task<ActionResult> CategoriesDropDown()
        {
            var model = await _service.CategoriesAsync();
            return PartialView("_CategoriesDropDown", model);
        }




    }


    

}