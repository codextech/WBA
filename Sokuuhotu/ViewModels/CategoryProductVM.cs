using BAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAW.ViewModels
{
    public class CategoryProductVM
    {

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Product> Products { get; set; }


    }
}