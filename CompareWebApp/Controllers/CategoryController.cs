using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareWebApp.Models;
using CompareWebApp.Repository;


namespace CompareWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public const int noOfProductsPerPage = 4;
        public ActionResult Index(string categoryName, int currentPageIndex)
        {
            HttpCookie pageInformationCookie = Request.Cookies["pageInformation"];
            string _categoryName    = string.Empty;
            int _totalNoOfPages     = 0;

            if (pageInformationCookie["categoryName"] != null && pageInformationCookie["totalNoOfPages"] != null)
            {
                _categoryName =
                    Server.HtmlEncode(pageInformationCookie["categoryName"]);
                _totalNoOfPages =
                    Convert.ToInt32(Server.HtmlEncode(pageInformationCookie["totalNoOfPages"]));
            }
            // if you are visiting this category first time then you have save totalNoOfPages in Cookie along with the categoryName 
            if (!(_categoryName == categoryName && _totalNoOfPages != 0))
            {
                _categoryName = categoryName;
                _totalNoOfPages = Convert.ToInt32(Math.Ceiling((DBManager.GetProductsCount(categoryName) / Convert.ToDouble(noOfProductsPerPage))));
                pageInformationCookie["categoryName"]   = _categoryName;
                pageInformationCookie["totalNoOfPages"] = _totalNoOfPages.ToString();
                Response.SetCookie(pageInformationCookie);

            }

            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.CategoryName = _categoryName;
            productsCollection.Products = DBManager.GetProductsByCategory(_categoryName, noOfProductsPerPage, currentPageIndex);
            productsCollection.Pager = new Pager() { TotalNoOfPages = _totalNoOfPages, CurrentPageIndex = currentPageIndex };
            
            // for each categoryName you have to create a view 
            return View(_categoryName, productsCollection);
        }

    }
}