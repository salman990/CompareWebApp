using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompareWebApp.Models
{
    public class Pager
    {
        public string PagerInformation { get; set; }
        public int TotalNoOfPages { get; set; }
        public int CurrentPageIndex { get; set; }



    }
}