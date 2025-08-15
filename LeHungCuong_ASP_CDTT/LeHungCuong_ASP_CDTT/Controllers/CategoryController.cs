using LeHungCuong_ASP_CDTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Controllers
{
    public class CategoryController : Controller
    {
        WebAspDbEntities1 objWebAspDbEntities = new WebAspDbEntities1();
        // GET: Category
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objWebAspDbEntities.Categories.ToList();
            return View(objHomeModel);
        }
    }
}