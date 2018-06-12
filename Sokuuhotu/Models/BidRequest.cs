using Sokuuhotu.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAW.Models
{
    public class BidRequest
    {

        [Key]
        public int BidRequestId { get; set; }

        public DateTime BidDate { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public int BidTimes { get; set; }


        public int ProductId { get; set; }


        // navigation property

        public virtual Product Product { get; set; }


        public ApplicationUser User { get; set; }




    }
}