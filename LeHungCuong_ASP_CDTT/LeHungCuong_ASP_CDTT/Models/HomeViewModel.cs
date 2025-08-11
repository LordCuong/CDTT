using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Models
{
    public class HomeViewModel
    {
        public List<Products> Products { get; set; }
        public List<Brands> Brands { get; set; }
        public List<Categories> Categories { get; set; }
        public List<Users> Users { get; set; }
        public List<Orders> Orders { get; set; }
    }
}