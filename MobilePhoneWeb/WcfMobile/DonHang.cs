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
    
    public partial class DonHang
    {
        public DonHang()
        {
            this.CT_DonHang = new HashSet<CT_DonHang>();
        }
    
        public int MaDH { get; set; }
        public Nullable<int> MaKH { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public Nullable<int> Trigia { get; set; }
        public Nullable<int> TinhTrang { get; set; }
        public Nullable<int> MaNV { get; set; }
    
        public virtual ICollection<CT_DonHang> CT_DonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual NhanVien NhanVien { get; set; }
    }
}