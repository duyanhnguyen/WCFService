//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfMobile
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVien
    {
        public NhanVien()
        {
            this.DonHangs = new HashSet<DonHang>();
            this.PhieuNhaps = new HashSet<PhieuNhap>();
        }
    
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int QuyenNV { get; set; }
    
        public virtual ICollection<DonHang> DonHangs { get; set; }
        public virtual QuyenNhanVien QuyenNhanVien { get; set; }
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}
