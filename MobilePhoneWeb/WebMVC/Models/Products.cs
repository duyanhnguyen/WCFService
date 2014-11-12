using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebMobile.Models
{
    public class Products
    {
        public int? ID { get; set; }
        public string NAME { get; set; }
        public double? PRICE { get; set; }
        public string IMAGES { get; set; }
        public string DETAILS { get; set; }
        public int? NUMBER { get; set; }
        public double? COUNT { get; set; }
    }   
}