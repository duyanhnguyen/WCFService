using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceNhanVien" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceNhanVien.svc or ServiceNhanVien.svc.cs at the Solution Explorer and start debugging.
    public class ServiceNhanVien : IServiceNhanVien
    {
        public List<NhanVien> SelectNhanVien()
        {
            var list = new List<NhanVien>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var nhanvien = from p in db.NhanViens select p;
                    foreach (NhanVien item in nhanvien)
                    {
                        NhanVien nv = new NhanVien()
                        {
                            MaNV = item.MaNV,
                            TenNV = item.TenNV,
                            Username = item.Username,
                            Password = item.Password,
                            Email = item.Email,
                            QuyenNV = item.QuyenNV

                        };
                        list.Add(nv);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertNhanVien(NhanVien info)
        {
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhanVien inserted = new NhanVien
                    {
                        TenNV = info.TenNV,
                        Username = info.Username,
                        Password = info.Password,
                        Email = info.Email,
                        QuyenNV = info.QuyenNV
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

        public int UpdateNhanVien(NhanVien info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhanVien updated = new NhanVien
                    {
                        MaNV = info.MaNV,
                        TenNV = info.TenNV,
                        Username = info.Username,
                        Password = info.Password,
                        Email = info.Email,
                        QuyenNV = info.QuyenNV
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

        public int DeleteNhanVien(NhanVien info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.NhanViens
                                   where p.MaNV == info.MaNV
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
        //Dang nhap
        public bool Login(string username, string password)
        {
            bool userValid;
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    userValid = db.NhanViens.Any(user => user.Username == username && user.Password == password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    return false;
                }
            }
            return userValid;
        }
        // select manv by username
        public int SelectIdByUsername(string username)
        {
            int id;
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var nhanvien = from p in db.NhanViens where p.Username == username select p.MaNV;

                    id = nhanvien.SingleOrDefault();

                    return id;
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
