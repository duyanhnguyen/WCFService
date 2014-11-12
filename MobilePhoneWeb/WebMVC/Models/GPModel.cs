using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMobile.Models
{
    public class GPModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Vui lòng nhập tên nhóm sp.")]
        public string Name { get; set; }
        public string Content { get; set; }
    }
}