using BAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAW.ViewModels
{
    public class BidsByProduct
    {

        public IEnumerable<BidRequest> FirstBidsOfProduct { get; set; }
        public IEnumerable<BidRequest> SecondBidsOfProduct { get; set; }

        public decimal MaxBid { get; set; }

    }
}