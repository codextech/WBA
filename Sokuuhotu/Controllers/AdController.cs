using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BAW.Models;
using BAW.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BAW.Controllers
{

    //It Handels all activity related to post an "Ad"   
    public class AdController : Controller
    {



        private readonly StoreService _service;

      



        public AdController(StoreService service)
        {
            _service = service;

        }


        public AdController() : this(new StoreService())
        {

        }







        public async Task<ActionResult> PostedProduct()
        {


            var userId = User.Identity.GetUserId();

            var products = await _service.GetPostedProductsAsync(userId);

            return View(products);

        }




        [Display(Name = "Add-Item")]
        // GET: Ad/Create
        public async Task<ActionResult> Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1 Day", Value = "1" });
            items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
            items.Add(new SelectListItem { Text = "1 week", Value = "7", });
            items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });

            ViewBag.closeDate = items;

            ViewBag.Categorylist =  new  SelectList(await _service.CategoriesAsync(), "CategoryId", "CategoryName");
            return View();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, int closeDate)
        {
          var  userId = User.Identity.GetUserId();

            product.UserId = userId;
            if (ModelState.IsValid)
            {
                await _service.AddProductAsync(product, userId, closeDate);


                return RedirectToAction("PostedProduct");
        }

          //Closing days Static list

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1 Day", Value = "1" });
            items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
            items.Add(new SelectListItem { Text = "1 week", Value = "7", });
            items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });

            ViewBag.closeDate = items;

            ViewBag.Categorylist = new SelectList(await _service.CategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Ad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _service.FindProduct(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1 Day", Value = "1" });
            items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
            items.Add(new SelectListItem { Text = "1 week", Value = "7", });
            items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });
            ViewBag.closeDate = items;

            ViewBag.Categorylist = new SelectList(await _service.CategoriesAsync(), "CategoryId", "CategoryName");
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product, int closeDate)
        {
            if (ModelState.IsValid)
            {
                await _service.ProductEditAsync(product, closeDate);
                return RedirectToAction("PostedProduct");
            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1 Day", Value = "1" });
            items.Add(new SelectListItem { Text = "2 Days", Value = "2" });
            items.Add(new SelectListItem { Text = "1 week", Value = "7", });
            items.Add(new SelectListItem { Text = "2 weeks", Value = "14", });

            ViewBag.closeDate = items;
            ViewBag.Categorylist = new SelectList(await _service.CategoriesAsync(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        //// GET: Ad/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Ad/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
