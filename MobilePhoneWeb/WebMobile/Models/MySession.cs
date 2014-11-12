using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebMobile.Models
{
    public class MySession
    {
        public static string MaSanPham
        {
            get { return "0"; }
        }
        public static string TongSL
        {
            get { return "1"; }
        }
        public static double? COUNT { get; set; }

        public static List<Products> GioHang { get; set; }
    }
}