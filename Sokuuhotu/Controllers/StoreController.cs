using BAW.Services;
using BAW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BAW.Controllers
{
    public class StoreController : Controller
    {


        private readonly StoreService _service;



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
    }
}