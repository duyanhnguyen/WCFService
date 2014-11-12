using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicePhieuNhap" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicePhieuNhap.svc or ServicePhieuNhap.svc.cs at the Solution Explorer and start debugging.
    public class ServicePhieuNhap : IServicePhieuNhap
    {
        public List<PhieuNhap> SelectPhieuNhap()
        {
            var list = new List<PhieuNhap>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var phieunhap = from p in db.PhieuNhaps select p;
                    foreach (var item in phieunhap)
                    {
                        PhieuNhap pn = new PhieuNhap()
                        {
                            MaPN = item.MaPN,
                            MaNPP = item.MaNPP,
                            Ngay = item.Ngay,
                            Trigia = item.Trigia,
                            MaNV = item.MaNV
                        };
                        list.Add(pn);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertPhieuNhap(PhieuNhap info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    PhieuNhap inserted = new PhieuNhap
                    {
                        MaNPP = info.MaNPP,
                        Ngay = info.Ngay,
                        Trigia = info.Trigia,
                        MaNV = info.MaNV
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

        public int UpdatePhieuNhap(PhieuNhap info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    PhieuNhap updated = new PhieuNhap
                    {
                        MaPN = info.MaPN,
                        MaNPP = info.MaNPP,
                        Ngay = info.Ngay,
                        Trigia = info.Trigia,
                        MaNV = info.MaNV
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

        public int DeletePhieuNhap(PhieuNhap info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.PhieuNhaps
                                   where p.MaPN == info.MaPN
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

        public List<CT_PhieuNhap> SelectCTPN()
        {
            var list = new List<CT_PhieuNhap>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var chitietphieunhap = from p in db.CT_PhieuNhap select p;
                    foreach (var item in chitietphieunhap)
                    {
                        CT_PhieuNhap ctpn = new CT_PhieuNhap()
                        {
                            MaPN = item.MaPN,
                            MaSP = item.MaSP,
                            SoLuong = item.SoLuong,
                            Gia = item.Gia
                        };
                        list.Add(ctpn);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertCTPN(CT_PhieuNhap info)
        {
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    CT_PhieuNhap inserted = new CT_PhieuNhap
                    {
                        MaPN = info.MaPN,
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

        public int UpdateCTPN(CT_PhieuNhap info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    CT_PhieuNhap updated = new CT_PhieuNhap
                    {
                        MaPN = info.MaPN,
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

        public int DeleteCTPN(int mapn)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    while (db.CT_PhieuNhap.Where(x => x.MaPN == mapn).Count() != 0)
                    {
                        CT_PhieuNhap ct = db.CT_PhieuNhap.Where(x => x.MaPN == mapn).FirstOrDefault();
                        db.Entry(ct).State = EntityState.Deleted;
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

        public List<PhieuNhap_SanPham> SelectCTPN_ByMaPN(int ma)
        {
            var list = new List<PhieuNhap_SanPham>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var chitietphieunhap = from p in db.CT_PhieuNhap
                                           from m in db.SanPhams
                                           where p.MaPN == ma && m.MaSP == p.MaSP
                                           select new { p, m.TenSP };
                    foreach (var item in chitietphieunhap)
                    {
                        PhieuNhap_SanPham ctpn = new PhieuNhap_SanPham()
                        {
                            MaPN = item.p.MaPN,
                            MaSP = item.p.MaSP,
                            TenSP = item.TenSP,
                            SoLuong = int.Parse(item.p.SoLuong.ToString()),
                            Gia = item.p.Gia.Value
                        };
                        list.Add(ctpn);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int GetNewIDPhieuNhap()
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var value = (from p in db.PhieuNhaps
                                 select p.MaPN).Max();

                    return int.Parse(value.ToString());
                }
                catch
                {
                    return 0;
                }
            }
        }
        //cap nhat nếu có nhập thêm hàng
        public int UpdateSoLuongSanPham(int masp, int soluong)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    SanPham sp = (from p in db.SanPhams
                                  where p.MaSP == masp
                                  select p).First();
                    sp.SoLuong += soluong;// công thêm vào 
                    db.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
        }
        //Thống kê nhập cá sp trong tháng
        public List<ThongkePN> ThongkeSPNhapThang(int thang, int nam)
        {
            var list = new List<ThongkePN>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var phieunhap = from pn in db.PhieuNhaps
                                    from ctpn in db.CT_PhieuNhap
                                    from sp in db.SanPhams
                                    where ((pn.Ngay.Value.Month == thang) && (pn.Ngay.Value.Year == nam) && (pn.MaPN == ctpn.MaPN) && (sp.MaSP == ctpn.MaSP))
                                    select new { ctpn.MaSP, sp.TenSP, ctpn.Gia, ctpn.SoLuong };
                    foreach (var item in phieunhap)
                    {
                        ThongkePN tkpn = new ThongkePN()
                        {
                            ID = item.MaSP,
                            TEN = item.TenSP,
                            DONGIA = (int)item.Gia,
                            SOLUONG = (int)item.SoLuong,
                            THANHTIEN = (int)item.Gia * (int)item.SoLuong,
                        };
                        list.Add(tkpn);
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
