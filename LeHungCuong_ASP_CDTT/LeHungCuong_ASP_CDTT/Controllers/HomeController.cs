using LeHungCuong_ASP_CDTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Controllers
{
    public class HomeController : Controller
    {
        WebAspDbEntities1 objWebAspDbEntities = new WebAspDbEntities1();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = objWebAspDbEntities.Categories.Where(c => c.ShowOnHomePage.HasValue && c.ShowOnHomePage.Value == true).ToList();
            objHomeModel.ListProduct = objWebAspDbEntities.Products.Where(p => p.ShowOnHomePage.HasValue && p.ShowOnHomePage.Value == true).ToList();
            objHomeModel.ListBrand = objWebAspDbEntities.Brands.Where(b => b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true).ToList();

            // Truyền dữ liệu vào ViewBag
            //ViewBag.ListCategory = objHomeModel.ListCategory;
            //ViewBag.ListProduct = objHomeModel.ListProduct;

            return View(objHomeModel);
        }

        public ActionResult Large(int Id, int page = 1, int pageSize = 6)
        {
            List<Products> listProduct;

            if (Id == 0) // Nếu ID là 0, lấy tất cả sản phẩm
            {
                listProduct = objWebAspDbEntities.Products.Where(b => b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true).ToList();
            }
            else
            {
                listProduct = objWebAspDbEntities.Products.Where(b => b.CategoryId == Id && b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true).ToList();
            }

            // Tính tổng số sản phẩm
            int totalProducts = listProduct.Count;

            // Phân trang
            var paginatedProducts = listProduct.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Truyền dữ liệu cần thiết sang ViewBag
            ViewBag.CurrentCategoryId = Id;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.PageSize = pageSize;

            return View(paginatedProducts);
        }



        public ActionResult Grid(int Id, string type = "category", int page = 1, int pageSize = 8)
        {
            List<Products> listProduct;

            if (Id == 0)
            {
                listProduct = objWebAspDbEntities.Products
                    .Where(b => b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true)
                    .ToList();
            }
            else
            {
                if (type == "brand")
                {
                    listProduct = objWebAspDbEntities.Products
                        .Where(b => b.BrandId == Id && b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true)
                        .ToList();
                }
                else // mặc định là category
                {
                    listProduct = objWebAspDbEntities.Products
                        .Where(b => b.CategoryId == Id && b.ShowOnHomePage.HasValue && b.ShowOnHomePage.Value == true)
                        .ToList();
                }
            }

            int totalProducts = listProduct.Count;
            var paginatedProducts = listProduct.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentType = type;
            ViewBag.CurrentId = Id;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.PageSize = pageSize;

            return View(paginatedProducts);
        }



        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebAspDbEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.IsAdmin = false;
                    _user.CreatedAt = DateTime.Now;
                    objWebAspDbEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebAspDbEntities.Users.Add(_user);
                    objWebAspDbEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại!";
                    return View();
                }


            }
            return View();


        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = objWebAspDbEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    Session["IsAdmin"] = data.FirstOrDefault().IsAdmin;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleFirebaseLogin(FirebaseUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra người dùng trong database hoặc tạo mới
                var user = objWebAspDbEntities.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new Users
                    {
                        DisplayName = model.DisplayName,
                        Email = model.Email,
                        Provider = model.Provider,
                        FirebaseUid = model.Uid
                    };
                    objWebAspDbEntities.Users.Add(user);
                    objWebAspDbEntities.SaveChanges();
                }

                // Lưu thông tin đăng nhập vào session
                Session["UserId"] = user.Id;
                Session["UserEmail"] = user.Email;
                Session["UserName"] = user.DisplayName;

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public JsonResult CheckLoginStatus()
        {
            bool isLoggedIn = Session["idUser"] != null;
            return Json(isLoggedIn, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LiveSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Content(""); // Nếu không có từ khóa, trả về rỗng
            }

            // Tìm kiếm trong database
            var results = objWebAspDbEntities.Products
                .Where(p => (p.Name.Contains(query) || p.ShortDes.Contains(query)) && p.ShowOnHomePage.HasValue && p.ShowOnHomePage.Value == true)
                .Take(10) // Giới hạn số kết quả (ví dụ: 10 kết quả)
                .ToList();

            // Trả về một PartialView chứa danh sách kết quả
            return PartialView("_SearchResults", results);
        }
        private WebAspDbEntities1 _context;

        public HomeController()
        {
            _context = new WebAspDbEntities1();
        }

        // Action trả về danh sách danh mục dưới dạng JSON
        [HttpGet]
        public JsonResult GetCategories()
        {
            try
            {
                var categories = _context.Categories
                    .Select(c => new {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();

                return Json(categories, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Getbrands()
        {
            try
            {
                var brands = _context.Brands
                    .Select(c => new {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();

                return Json(brands, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}