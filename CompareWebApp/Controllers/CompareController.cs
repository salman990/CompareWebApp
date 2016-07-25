using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareWebApp.Models;
using CompareWebApp.Repository;

namespace CompareWebApp.Controllers
{
    public class CompareController : Controller
    {
        // GET: Compare
        public ActionResult Index()
        {
            HttpCookie pageInformationCookie = new HttpCookie("pageInformation");
            Response.Cookies.Add(pageInformationCookie);
            List<Categories> categoriesList = new List<Categories>();
            foreach (var parentCategory in DBManager.GetParerntCategories())
            {
                Categories cats = new Categories();
                cats.ParentCategory = parentCategory;
                cats.ProductCategories = DBManager.GetCategories(parentCategory.categoryID);
                categoriesList.Add(cats);
            }

            return View(categoriesList);

        }

    }
}