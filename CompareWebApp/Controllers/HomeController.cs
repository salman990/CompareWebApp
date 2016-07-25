using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompareWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
            /*

    
            MatchDB db = new MatchDB();
            ModelState.Clear();
            List<Categories> categoriesList = new List<Categories>();
            foreach (var parentCategory in db.GetParerntCategories())
            {
                Categories cats = new Categories();
                cats.ParentCategory = parentCategory;
                cats.ProductCategories = db.GetCategories(parentCategory.categoryID);
                categoriesList.Add(cats);
            }







            


        }

        public ActionResult Category(int id)
        {
            double pageSize = 4;
            MatchDB db = new MatchDB();
            HttpCookie categoryCookie = new HttpCookie("category");
            categoryCookie.Values["categoryID"] = id.ToString();
            double productCount = Convert.ToDouble(db.CountProductsByCategory(id));
            int pageCount = Convert.ToInt32(Math.Ceiling((productCount / pageSize)));
            categoryCookie.Values["pageCount"] = pageCount.ToString();
            Response.Cookies.Add(categoryCookie);
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.Products = db.GetProductsByCategory(id, pageCount, 1);
            productsCollection.PageCount = pageCount;
            productsCollection.CurrentPageIndex = 1;

            return View("Category", productsCollection);

        }

        public ActionResult Vendors(int id)
        {
            double pageSize = 10;
            MatchDB db = new MatchDB();
            HttpCookie vendorCookie = new HttpCookie("vendor");
            vendorCookie.Values["productID"] = id.ToString();
            double productCount = Convert.ToDouble(db.CountVendorsByProduct(id.ToString()));
            int pageCount = Convert.ToInt32(Math.Ceiling((productCount / pageSize)));
            vendorCookie.Values["pageCount"] = pageCount.ToString();
            Response.Cookies.Add(vendorCookie);
            ProductsCollection productsCollection = new ProductsCollection();
            productsCollection.Products = db.GetVendorsByProduct(id.ToString(), pageSize.ToString(), "1");
            productsCollection.PageCount = pageCount;
            productsCollection.CurrentPageIndex = 1;

            return View("Vendors", productsCollection);

        }

        [HttpPost]
        public ActionResult GetVendorsPage(string vendorCurrentPageIndex)
        {
            int pageSize = 10;
            MatchDB db = new MatchDB();
            ModelState.Clear();
            string pageCount = string.Empty;
            string productID = string.Empty;

            if (Request.Cookies["vendor"] != null)
            {
                pageCount =
                    Server.HtmlEncode(Request.Cookies["vendor"]["pageCount"]);
                productID =
                    Server.HtmlEncode(Request.Cookies["vendor"]["productID"]);

            }

            ProductsCollection productCollection = new ProductsCollection();

            productCollection.CurrentPageIndex = Convert.ToInt32(vendorCurrentPageIndex);
            productCollection.PageCount = Convert.ToInt32(pageCount);
            productCollection.Products = db.GetVendorsByProduct(productID, pageSize.ToString(), vendorCurrentPageIndex);

            return View("Vendors", productCollection);
        }

        [HttpPost]
        public ActionResult Index(string SearchTag)
        {
            MatchDB db = new MatchDB();
            double pageSize = 3;
            HttpCookie searchCookie = new HttpCookie("userInfo");
            searchCookie.Values["searchTerm"] = SearchTag;

            List<Category> list = db.SearchInCategory(SearchTag);

            double totalNumberOfProducts = Convert.ToDouble(list.Count);
            int pageCount = Convert.ToInt32(Math.Ceiling((totalNumberOfProducts / pageSize)));
            searchCookie.Values["searchHits"] = pageCount.ToString();
            Response.Cookies.Add(searchCookie);

        
            return (Search("0"));

        }


        [HttpPost]
        public ActionResult Search(string currentPageIndex)
        {

            MatchDB db = new MatchDB();
            ModelState.Clear();
            string searchTerm = string.Empty;
            string pageCount = string.Empty;

            if (Request.Cookies["userInfo"] != null)
            {
                searchTerm =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["searchTerm"]);

                pageCount =
                    Server.HtmlEncode(Request.Cookies["userInfo"]["searchHits"]);
            }

            ProductsCollection productCollection = new ProductsCollection();

            productCollection.CurrentPageIndex = Convert.ToInt32(currentPageIndex);
            productCollection.PageCount = Convert.ToInt32(pageCount);
            productCollection.SearchTerm = searchTerm;
            productCollection.Products = db.GetProductsByPage(searchTerm, productCollection.CurrentPageIndex);

            return View("Search", productCollection);
        }


        [HttpPost]
        public ActionResult GetProductsByCategory(string categoryCurrentPageIndex)
        {
            int pageSize = 4;
            MatchDB db = new MatchDB();
            ModelState.Clear();
            string pageCount = string.Empty;
            string categoryID = string.Empty;

            if (Request.Cookies["category"] != null)
            {
                pageCount =
                    Server.HtmlEncode(Request.Cookies["category"]["pageCount"]);
                categoryID =
                    Server.HtmlEncode(Request.Cookies["category"]["categoryID"]);

            }

            ProductsCollection productCollection = new ProductsCollection();

            productCollection.CurrentPageIndex = Convert.ToInt32(categoryCurrentPageIndex);
            productCollection.PageCount = Convert.ToInt32(pageCount);
            productCollection.Products = db.GetProductsByCategory(Convert.ToInt32(categoryID), pageSize, Convert.ToInt32(categoryCurrentPageIndex));
            return View("Category", productCollection);
        }
        */

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
    