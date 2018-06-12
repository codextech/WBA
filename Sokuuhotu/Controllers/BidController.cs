using BAW.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BAW.Controllers
{
    public class BidController : Controller
    {




        private readonly BidService _service;



        public BidController(BidService service)
        {
            _service = service;
        }


        public BidController() : this(new BidService())
        {

        }



        public async Task<ActionResult> BidReceived(int proId, int price)
        {

            var userId = User.Identity.GetUserId();

            var mesg = await _service.AcceptBidAsync(proId, userId, price);

            if (mesg != "")
            {
                TempData["Message"] = "only 2 bid";

                return RedirectToAction("ProductDetails", "Store", new { Id = proId });




            }


            return RedirectToAction("BidsHistory");


        }



        public async Task<ActionResult> BidsHistory()
        {


            var userId = User.Identity.GetUserId();

            var history = await _service.BidsHistoryAsync(userId);

            return View(history);

        }



        public async Task<ActionResult> PostedProduct()
        {


            var userId = User.Identity.GetUserId();

            var products = await _service.GetPostedProductsAsync(userId);

            return View(products);

        }



        public async Task<ActionResult> AllBidsByProduct(int proId)
        {


            //var userId = User.Identity.GetUserId();

            var bidrequest = await _service.GetProductBidsByIdAsync(proId);

            return View(bidrequest);

        }




    }
}


