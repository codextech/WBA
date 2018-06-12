using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BAW.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImagesId { get; set; }

        public string ImageUrl { get; set; }

        public int ProductId { get; set; }



        public virtual Product Product { get; set; }



    }
}