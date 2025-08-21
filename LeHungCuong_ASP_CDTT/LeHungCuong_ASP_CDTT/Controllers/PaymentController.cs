using LeHungCuong_ASP_CDTT.Context;
using LeHungCuong_ASP_CDTT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeHungCuong_ASP_CDTT.Controllers
{
    public class PaymentController : Controller
    {
        WebAspDbEntities1 db = new WebAspDbEntities1();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var listCart = (List<CartModel>)Session["cart"];
                Orders order = new Orders();
                order.Name = "GVN-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                order.UserId = int.Parse(Session["idUser"].ToString());
                order.CreatedAt = DateTime.Now;
                order.Status = 1;
                //order.Price = listCart.Sum(n => n.Product.Price * n.Quantity);
                db.Orders.Add(order);
                db.SaveChanges();
                int intOrderId = order.Id;
                List<OrderDetails> listOrderDetail = new List<OrderDetails>();

                foreach (var item in listCart)
                {
                    OrderDetails orderDetail = new OrderDetails();
                    orderDetail.OrderId = intOrderId;
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.UserId = int.Parse(Session["idUser"].ToString());
                    orderDetail.TotalPrice = item.Quantity * item.Product.Price;
                    listOrderDetail.Add(orderDetail);
                }
                db.OrderDetails.AddRange(listOrderDetail);
                db.SaveChanges();
                // Xóa giỏ hàng
                Session["cart"] = null;
                Session["count"] = 0;
            }
            return View();
        }
    }
}