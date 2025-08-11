using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Models
{
    public class OrderViewModel
    {
      
        public Orders Order { get; set; } // Thông tin đơn hàng
        public Users User { get; set; }   // Thông tin người dùng
        public List<OrderDetailProductViewModel> OrderDetails { get; set; }
    }
    public class OrderDetailProductViewModel
    {
        public string ProductName { get; set; } // Tên sản phẩm
        public int Quantity { get; set; }      // Số lượng
        public double Price { get; set; }      // Giá
        public double TotalPrice { get; set; } // Tổng giá
    }
}