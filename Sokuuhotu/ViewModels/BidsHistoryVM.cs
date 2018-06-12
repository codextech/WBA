using BAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAW.ViewModels
{
    public class BidsHistoryVM
    {

        public IEnumerable<BidRequest> FirstTimeBid { get; set; }

        public IEnumerable<BidRequest> SecondTimeBid { get; set; }


    }
}