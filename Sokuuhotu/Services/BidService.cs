using BAW.Models;
using BAW.ViewModels;
using Sokuuhotu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BAW.Services
{
    public class BidService
    {

        private readonly ApplicationDbContext _db;

        public BidService(ApplicationDbContext context)
        {
            _db = context;
        }

        public BidService() : this(new ApplicationDbContext())
        {

        }



        public async Task<string> AcceptBidAsync(int proId, string userId, int price)
        {



            var message = string.Empty;




            var bidProduct = await _db.BidRequests.Where(p => p.ProductId == proId && p.UserId == userId).ToListAsync();

            if (bidProduct.Count() == 0)
            {

                var bidRequest = new BidRequest
                {
                    ProductId = proId,
                    Price = price,
                    UserId = userId,
                    BidDate = DateTime.Now,
                    BidTimes = 1

                };



                _db.BidRequests.Add(bidRequest);
            }





            else if (bidProduct.Count() == 1)
            {
                var bidRequest = new BidRequest
                {
                    ProductId = proId,
                    Price = price,
                    UserId = userId,
                    BidDate = DateTime.Now,
                    BidTimes = 2


                };
                _db.BidRequests.Add(bidRequest);

            }



            else
            {
                message = "More than 2 bids";
            }






            await _db.SaveChangesAsync();


            return message;


        }




        public async Task<BidsHistoryVM> BidsHistoryAsync(string userId)
        {

            var firstTime = await _db.BidRequests.Where(p => p.UserId == userId && p.BidTimes == 1).ToArrayAsync();

            var secondTime = await _db.BidRequests.Where(p => p.UserId == userId && p.BidTimes == 2).ToArrayAsync();


            var bidVM = new BidsHistoryVM()
            {
                FirstTimeBid = firstTime,
                SecondTimeBid = secondTime
            };


            return bidVM;

        }




        public async Task<IEnumerable<Product>> GetPostedProductsAsync(string userId)
        {
            return await _db.Products.Where(p => p.UserId == userId).ToArrayAsync();

        }




        public async Task<BidsByProduct> GetProductBidsByIdAsync(int proId)
        {
            var bids = await _db.BidRequests.Include("User").Where(p => p.ProductId == proId).ToArrayAsync();

            var firstTimebids = bids.Where(p => p.BidTimes == 1).ToList();

            var secondTimebids = bids.Where(p => p.BidTimes == 2).ToList();

            var maxBid = bids.Max(p => p.Price);

            var vm = new BidsByProduct()
            {

                FirstBidsOfProduct = firstTimebids,
                SecondBidsOfProduct = secondTimebids,
                MaxBid = maxBid
            };

            return vm;

        }


    }
}
