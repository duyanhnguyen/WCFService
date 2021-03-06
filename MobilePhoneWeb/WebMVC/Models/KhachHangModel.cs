﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WebMobile.Models
{
    public class KhachHangModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng UserName")]
        [StringLength(100, ErrorMessage = "Mat khẩu phải có ít nhất {2}.", MinimumLength = 6)]
        [Remote("KiemTraUser", "Customer", ErrorMessage = "UserName đã được sử dụng!!!!")]
        [RegularExpression(@"^[\S]*$", ErrorMessage = "Không được sử dụng khoảng trắng")]
        public string UserName { get; set; }

        [RegularExpression(@"^[\S]*$", ErrorMessage = "Không được sử dụng khoảng trắng")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, ErrorMessage = "Mat khẩu phải có ít nhất {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                   ErrorMessage = "Email không đúng định dạng.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression("(09[0-9]{8}|01[2-9]{1}[0-9]{8})",
                   ErrorMessage = "so diên thoại không đúng định dạng.")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<int> Status { get; set; }
        public string Rules_Id { get; set; }

//        public virtual Rule Rule { get; set; }
    }
}