using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Controllers
{
    public class ProductController : Controller
    {
        WebAspDbEntities1 objWebAspDbEntities = new WebAspDbEntities1();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objWebAspDbEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}