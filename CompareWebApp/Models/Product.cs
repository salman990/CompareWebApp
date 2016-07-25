using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareWebApp.Models
{
    public class Product
    {

        public int productID { get; set; }
        public string productName { get; set; }
        public string vendorName { get; set; }
        public decimal price { get; set; }

        public string productAttributes { get; set; }
    }
}