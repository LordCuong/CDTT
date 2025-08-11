using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Models
{
    public class CartModel
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}