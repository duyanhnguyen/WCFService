using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMobile.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin sản phẩm")]
        public string Detail { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(1, 1000000000, ErrorMessage = "Giá không hợp lý")]
        public double ? Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Date { get; set; }
        public int ? Order { get; set; }
        public int ? Status { get; set; }
        public int ? GroupProduct_Id { get; set; }
    
    }
}