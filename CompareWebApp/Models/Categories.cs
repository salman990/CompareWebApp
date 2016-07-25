using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareWebApp.Models
{
    public class Categories
    {
        public IEnumerable<Category> ProductCategories { get; set; }
        public Category ParentCategory { get; set; }
    }
}