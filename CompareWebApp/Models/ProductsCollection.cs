using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareWebApp.Models
{
    public class ProductsCollection
    {
        public string CategoryName { get; set; }
        public IEnumerable<Product> Products { get; set;}
        public Pager Pager { get; set; }
    }
}