using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeHungCuong_ASP_CDTT.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult ListingProduct()
        {
            return View();
        }
    }
}