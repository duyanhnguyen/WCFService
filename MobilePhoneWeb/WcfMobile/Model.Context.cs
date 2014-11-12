﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class QL_DienThoaiEntities : DbContext
    {
        public QL_DienThoaiEntities()
            : base("name=QL_DienThoaiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CT_DonHang> CT_DonHang { get; set; }
        public DbSet<CT_PhieuNhap> CT_PhieuNhap { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<NhaPhanPhoi> NhaPhanPhois { get; set; }
        public DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<QuyenNhanVien> QuyenNhanViens { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
    
        public virtual ObjectResult<TimKiemSP_Result> TimKiemSP(Nullable<int> hang, Nullable<int> giaTu, Nullable<int> giaDen)
        {
            var hangParameter = hang.HasValue ?
                new ObjectParameter("Hang", hang) :
                new ObjectParameter("Hang", typeof(int));
    
            var giaTuParameter = giaTu.HasValue ?
                new ObjectParameter("GiaTu", giaTu) :
                new ObjectParameter("GiaTu", typeof(int));
    
            var giaDenParameter = giaDen.HasValue ?
                new ObjectParameter("GiaDen", giaDen) :
                new ObjectParameter("GiaDen", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TimKiemSP_Result>("TimKiemSP", hangParameter, giaTuParameter, giaDenParameter);
        }
    }
}
