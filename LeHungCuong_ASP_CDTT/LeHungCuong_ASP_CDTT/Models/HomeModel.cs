using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LeHungCuong_ASP_CDTT.Context;

namespace LeHungCuong_ASP_CDTT.Models
{
    public class HomeModel
    {
        public List<Products> ListProduct { get; set; }
        public List<Categories> ListCategory { get; set; }
        public List<Brands> ListBrand { get; set; }
    }
}