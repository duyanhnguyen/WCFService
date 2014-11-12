using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceSanPham" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceSanPham.svc or ServiceSanPham.svc.cs at the Solution Explorer and start debugging.
    public class ServiceSanPham : IServiceSanPham
    {
        public List<SanPham> SelectSanPham()
        {
            var list = new List<SanPham>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = from p in db.SanPhams select p;
                    foreach (SanPham item in sanpham)
                    {
                        SanPham sp = new SanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            UrlHinh = item.UrlHinh,
                            Gia = item.Gia,
                            MoTa = item.MoTa,
                            MaNSX = item.MaNSX,
                            SoLuong = item.SoLuong
                        };
                        list.Add(sp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public List<SanPham> SelectSanPhamTheoNSX(int ma)
        {
            var list = new List<SanPham>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = from p in db.SanPhams where p.MaNSX == ma select p;
                    foreach (SanPham item in sanpham)
                    {
                        SanPham sp = new SanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            UrlHinh = item.UrlHinh,
                            Gia = item.Gia,
                            MoTa = item.MoTa,
                            MaNSX = item.MaNSX,
                            SoLuong = item.SoLuong
                        };
                        list.Add(sp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public List<SanPham> SelectSanPhamByID(int ma)
        {
            var list = new List<SanPham>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = (from p in db.SanPhams
                                   where p.MaSP == ma
                                   select p).FirstOrDefault();
                    SanPham sp = new SanPham()
                    {
                        MaSP = sanpham.MaSP,
                        TenSP = sanpham.TenSP,
                        UrlHinh = sanpham.UrlHinh,
                        Gia = sanpham.Gia,
                        MoTa = sanpham.MoTa,
                        MaNSX = sanpham.MaNSX,
                        SoLuong = sanpham.SoLuong
                    };
                    list.Add(sp);
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertSanPham(SanPham info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    SanPham inserted = new SanPham
                    {
                        TenSP = info.TenSP,
                        UrlHinh = info.UrlHinh,
                        Gia = info.Gia,
                        MoTa = info.MoTa,
                        MaNSX = info.MaNSX,
                        SoLuong = info.SoLuong
                    };
                    db.Entry(inserted).State = EntityState.Added;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int UpdateSanPham(SanPham info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    SanPham updated = new SanPham
                    {
                        MaSP = info.MaSP,
                        TenSP = info.TenSP,
                        UrlHinh = info.UrlHinh,
                        Gia = info.Gia,
                        MoTa = info.MoTa,
                        MaNSX = info.MaNSX,
                        SoLuong = info.SoLuong
                    };
                    db.Entry(updated).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int DeleteSanPham(SanPham info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.SanPhams
                                   where p.MaSP == info.MaSP
                                   select p).FirstOrDefault();
                    db.Entry(deleted).State = EntityState.Deleted;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }
        // tim sp theo nsx
        public List<SanPham> SelectSanPhamByCategory(int id)
        {
            var list = new List<SanPham>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = db.SanPhams.Where(i=>i.MaNSX==id).ToList();
                    foreach (SanPham item in sanpham)
                    {
                        SanPham sp = new SanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            UrlHinh = item.UrlHinh,
                            Gia = item.Gia,
                            MoTa = item.MoTa,
                            MaNSX = item.MaNSX,
                            SoLuong = item.SoLuong
                        };
                        list.Add(sp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public List<SanPham> DetailSanPham(int id)
        {
            var list = new List<SanPham>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = from p in db.SanPhams where p.MaSP == id select p;
                    foreach (SanPham item in sanpham)
                    {
                        SanPham sp = new SanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            UrlHinh = item.UrlHinh,
                            Gia = item.Gia,
                            MoTa = item.MoTa,
                            MaNSX = item.MaNSX,
                            SoLuong = item.SoLuong
                        };
                        list.Add(sp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        // TÌM KIẾM NÂNG CAO
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        //quy trình, k nhap gì he thog se thay tổng sp, nhập giá từ, lấy sp>gia từ, nhập giá đến=> lay sp nhỏ hon gia den, từ và đến thì lấy trong khoảng
        public List<SanPham> SearchSanPham(int nhasanxuat, string Str_giatu, string Str_den)
        {
            var list = new List<SanPham>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var sanpham = (from s in db.SanPhams select s).ToList();
                    int giatu, den;
                    giatu = int.Parse(Str_giatu);
                    den = int.Parse(Str_den);
                    if (nhasanxuat == 0)
                    {
                        sanpham = sanpham.ToList();// lấy hết nếu k chọn hãng
                    }
                    else
                    {
                        sanpham = sanpham.Where(s => s.MaNSX == nhasanxuat).ToList();// lấy theo mã nếu chọn hãng
                    }
                    if (IsNumber(Str_giatu))
                    {
                        sanpham = sanpham.Where(s => s.Gia >= giatu).ToList();// lấy sp có giá từ
                    }

                    if (IsNumber(Str_den) && den > 0)
                    {
                        sanpham = sanpham.Where(s => s.Gia <= den).ToList();// lấy sp có giá đến
                    }
                    foreach (SanPham item in sanpham)
                    {
                        SanPham sp = new SanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            UrlHinh = item.UrlHinh,
                            Gia = item.Gia,
                            MoTa = item.MoTa,
                            MaNSX = item.MaNSX,
                            SoLuong = item.SoLuong
                        };
                        list.Add(sp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        private QL_DienThoaiEntities db = new QL_DienThoaiEntities();
        public string Category(int id)
        {
            try
            {
                var cate = db.NhaSanXuats.Single(i => i.MaNSX == id);
                return cate.TenNSX;
            }
            catch
            {
                return null;
            }
        }
        public SanPham GetSPbyID(int id)
        {
            try
            {
                var item = db.SanPhams.Single(i => i.MaSP == id);
                var sp = new SanPham();
                sp.MaSP = item.MaSP;
                sp.TenSP = item.TenSP;
                sp.UrlHinh = item.UrlHinh;
                sp.Gia = item.Gia;
                sp.MoTa = item.MoTa;
                sp.MaNSX = item.MaNSX;
                sp.SoLuong = item.SoLuong;
                return sp;               
            }
            catch
            {
                return null;
            }
        }

        // load category tìm kím cho nha san xuat
        public List<NhaSanXuat> GetListTKNSX()
        {
            var list = new List<NhaSanXuat>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var nhasanxuat = from p in db.NhaSanXuats select p;
                    foreach (var item in nhasanxuat)
                    {
                        NhaSanXuat nsx = new NhaSanXuat()
                        {
                            MaNSX = item.MaNSX,
                            TenNSX = item.TenNSX
                        };
                        list.Add(nsx);
                    }
                    var nhasx = new NhaSanXuat();
                    nhasx.MaNSX = 0;
                    nhasx.TenNSX = "Tất cả sản phẩm";
                    list.Add(nhasx);
                }
                catch
                {
                    return null;
                }
            }
            return list;    
        }
    }
}
