using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceDonHang" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceDonHang.svc or ServiceDonHang.svc.cs at the Solution Explorer and start debugging.
    public class ServiceDonHang : IServiceDonHang
    {
        //lay toan bo don hang
        public List<DonHang> SelectDonHang()
        {
            var list = new List<DonHang>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var donhang = from p in db.DonHangs select p;
                    foreach (DonHang item in donhang)
                    {
                        DonHang dh = new DonHang()
                        {
                            MaDH = item.MaDH,
                            MaKH = item.MaKH,
                            Ngay = item.Ngay,
                            Trigia = item.Trigia,
                            TinhTrang = item.TinhTrang
                        };
                        list.Add(dh);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }
        //Them don hang, phục vụ cho đặt hàng ở trang chủ
        public int InsertDonHang(DonHang info)
        {
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    DonHang inserted = new DonHang
                    {
                        MaKH = info.MaKH,
                        Ngay = info.Ngay,
                        Trigia = info.Trigia,
                        TinhTrang = info.TinhTrang
                    };
                    db.Entry(inserted).State = EntityState.Added;
                    db.SaveChanges();
                    return inserted.MaDH;
                }
                catch
                {
                    return 0;
                }
            }
        }
        //Update trạng thái don hang
        public int UpdateDonHang(DonHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    DonHang updated = new DonHang
                    {
                        MaDH = info.MaDH,
                        MaKH = info.MaKH,
                        Ngay = info.Ngay,
                        Trigia = info.Trigia,
                        TinhTrang = info.TinhTrang
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

        public List<CT_DonHang> SelectCTDH()
        {
            var list = new List<CT_DonHang>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var chitietdonhang = from p in db.CT_DonHang select p;
                    foreach (CT_DonHang item in chitietdonhang)
                    {
                        CT_DonHang ctdh = new CT_DonHang()
                        {
                            MaDH = item.MaDH,
                            MaSP = item.MaSP,
                            SoLuong = item.SoLuong,
                            Gia = item.Gia
                        };
                        list.Add(ctdh);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertCTDH(CT_DonHang info)
        {
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    CT_DonHang inserted = new CT_DonHang
                    {
                        MaDH = info.MaDH,
                        MaSP = info.MaSP,
                        SoLuong = info.SoLuong,
                        Gia = info.Gia
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

        public int UpdateCTDH(CT_DonHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    CT_DonHang updated = new CT_DonHang
                    {
                        MaDH = info.MaDH,
                        MaSP = info.MaSP,
                        SoLuong = info.SoLuong,
                        Gia = info.Gia
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
        //Don han của khách hàng, lấy thông tin đầy đủ cho đơn hàng
        public List<DonHangKhachHang> SelectDonHangKhachHang()
        {
            var list = new List<DonHangKhachHang>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var donhang = from dh in db.DonHangs
                                  from kh in db.KhachHangs
                                  where (dh.MaKH == kh.MaKH)// có khóa ngoại là mã kh
                                  select new { dh.MaDH, dh.Ngay, kh.HoTen, kh.DienThoai, kh.DiaChi, dh.Trigia, dh.TinhTrang, dh.MaKH };

                    foreach (var item in donhang)
                    {
                        DonHangKhachHang tk = new DonHangKhachHang
                        {
                            MaDH = item.MaDH,
                            Ngay = (DateTime)item.Ngay,
                            HoTen = item.HoTen,
                            DienThoai = item.DienThoai,
                            DiaChi = item.DiaChi,
                            Trigia = (int)item.Trigia,
                            Tinhtrang = tinhtrang(item.TinhTrang)
                        };
                        list.Add(tk);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }
        //quy dinh trang thai don hàng để load lên form
        private string tinhtrang(int? tt)
        {
            string ttr;
            if (tt == 0) ttr = "Chưa giao";
            else if (tt == 1) ttr = "Đã giao";
            else ttr = "Đã hủy";
            return ttr;
        }
        //  Lấy chi tiết đơn hàng dựa vào mã đơn hàng
        public List<CT_DonHangSanPham> SelectCT_DonHangByDonHang(int id)
        {
            var list = new List<CT_DonHangSanPham>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var ctdonhang = from ctdh in db.CT_DonHang
                                    from sp in db.SanPhams
                                    where ctdh.MaDH == id && ctdh.MaSP == sp.MaSP
                                    select new { ctdh.MaSP, sp.TenSP, ctdh.SoLuong, sp.Gia };

                    foreach (var item in ctdonhang)
                    {
                        CT_DonHangSanPham dh = new CT_DonHangSanPham()
                        {
                            MaSP = item.MaSP,
                            TenSP = item.TenSP,
                            SoLuong = (int)item.SoLuong,
                            Gia = item.Gia,
                            ThanhTien = ((int)item.SoLuong * item.Gia)
                        };
                        list.Add(dh);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        // Update tình trạng đơn hàng
        //public int UpdateTinhTrangDonHang(DonHang info)
        //{
        //    using (var db = new QL_DienThoaiEntities())
        //    {
        //        try
        //        {
        //            DonHang updated = new DonHang
        //            {
        //                MaDH = info.MaDH,
        //                //MaKH = info.MaKH,
        //                //Ngay = info.Ngay,
        //                //Trigia = info.Trigia,
        //                TinhTrang = info.TinhTrang,
        //                MaNV = info.MaNV
        //            };
        //            db.Entry(updated).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return 1;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //}
        //Cập nhật trạng thái đơn hàng.
        public int UpdateTinhTrangDonHang(DonHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    DonHang dh = (from p in db.DonHangs where p.MaDH == info.MaDH select p).First();
                    dh.TinhTrang = info.TinhTrang;
                    dh.MaNV = info.MaNV;
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }


        // select makh by madh
        //public int SelectMakhByMadh(int madh)
        //{
        //    int makh;
        //    using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
        //    {
        //        try
        //        {
        //            var khachhang = from p in db.DonHangs where p.MaDH == madh select p.MaKH;

        //            makh = (int)khachhang.SingleOrDefault();

        //            return makh;
        //        }
        //        catch
        //        {
        //            return 0;
        //        }
        //    }
        //}


        // update số lượng sản phẩm
        public int UpdateSoLuongSanPham(DonHang info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    List<CT_DonHang> ctdh = (from ct in db.CT_DonHang where ct.MaDH == info.MaDH select ct).ToList();
                    foreach (var item in ctdh)
                    {
                        SanPham sp = (from p in db.SanPhams where p.MaSP == item.MaSP select p).First();
                        sp.SoLuong = (int)sp.SoLuong - (int)item.SoLuong;
                        db.SaveChanges();
                    }
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }


        // THống kê
        public List<Thongke> BaocaoNgay(string ngay1, string ngay2)
        {
            DateTime dt1 = DateTime.Parse(ngay1);// ép kiểu ngày để so sánh
            DateTime dt2 = DateTime.Parse(ngay2);
            var list = new List<Thongke>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var baocao = from dh in db.DonHangs
                                 from ctdh in db.CT_DonHang
                                 from sp in db.SanPhams
                                 where ((dh.Ngay >= dt1) && (dh.Ngay <= dt2) && (dh.MaDH == ctdh.MaDH) && (sp.MaSP == ctdh.MaSP))// hoa don co ngày trng khoản tg
                                 select new { ctdh.MaSP, sp.TenSP, ctdh.Gia, ctdh.SoLuong };

                    foreach (var item in baocao)
                    {
                        Thongke tk = new Thongke
                        {
                            ID = item.MaSP,
                            TEN = item.TenSP,
                            DONGIA = (int)item.Gia,
                            SOLUONG = (int)item.SoLuong,
                            THANHTIEN = (int)item.SoLuong * (int)item.Gia
                        };
                        list.Add(tk);
                    }

                }
                catch
                {
                    return null;
                }
            }
            return list;
        }
        //Thống kê sp nv ban dc trong 1 tháng=> dc tên nv, số luong sp bán dc
        public List<Thongke> ThongkeNVThang(int manv, int thang, int nam)
        {
            //            DateTime dt1 = DateTime.Parse(ngay1);
            //DateTime dt = DateTime.Parse(thang);
            //int month = dt.Month;
            var list = new List<Thongke>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var baocao = from sp in db.SanPhams
                                 from dh in db.DonHangs
                                 from ctdh in db.CT_DonHang
                                 //                                 where ((dh.Ngay >= dt1) && (dh.Ngay <= dt2) && (dh.MaDH == ctdh.MaDH) && (sp.MaSP == ctdh.MaSP))
                                 where ((dh.MaNV == manv) && (dh.Ngay.Value.Month == thang) && (dh.Ngay.Value.Year == nam) && (dh.MaDH == ctdh.MaDH) && (sp.MaSP == ctdh.MaSP))
                                 select new { ctdh.MaSP, sp.TenSP, ctdh.Gia, ctdh.SoLuong };//kiem tra thang và năm trùng thì lấy thong tin san pham bán dc

                    foreach (var item in baocao)
                    {
                        Thongke tk = new Thongke
                        {
                            ID = item.MaSP,
                            TEN = item.TenSP,
                            DONGIA = (int)item.Gia,
                            SOLUONG = (int)item.SoLuong,
                            THANHTIEN = (int)item.SoLuong * (int)item.Gia
                        };
                        list.Add(tk);
                    }

                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        //Sản phẩm dc đặt trong tháng
        public List<Thongke> ThongkeSPThang(int thang, int nam)
        {
            //            DateTime dt1 = DateTime.Parse(ngay1);
            //DateTime dt = DateTime.Parse(thang);
            //int month = dt.Month;
            var list = new List<Thongke>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var baocao = from dh in db.DonHangs
                                 from ctdh in db.CT_DonHang
                                 from sp in db.SanPhams
                                 where ((dh.Ngay.Value.Month == thang) && (dh.Ngay.Value.Year == nam) && (dh.MaDH == ctdh.MaDH) && (sp.MaSP == ctdh.MaSP))
                                 select new { ctdh.MaSP, sp.TenSP, ctdh.Gia, ctdh.SoLuong };//Tìm trong don hang sp có 

                    foreach (var item in baocao)
                    {
                        Thongke tk = new Thongke
                        {
                            ID = item.MaSP,
                            TEN = item.TenSP,
                            DONGIA = (int)item.Gia,
                            SOLUONG = (int)item.SoLuong,
                            THANHTIEN = (int)item.SoLuong * (int)item.Gia
                        };
                        list.Add(tk);
                    }

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
