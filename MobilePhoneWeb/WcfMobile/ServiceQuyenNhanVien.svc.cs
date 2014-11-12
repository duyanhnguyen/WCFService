using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceQuyenNhanVien" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceQuyenNhanVien.svc or ServiceQuyenNhanVien.svc.cs at the Solution Explorer and start debugging.
    public class ServiceQuyenNhanVien : IServiceQuyenNhanVien
    {
        public List<QuyenNhanVien> SelectQuyenNhanVien()
        {
            var list = new List<QuyenNhanVien>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var quyennhanvien = from p in db.QuyenNhanViens select p;
                    foreach (var item in quyennhanvien)
                    {
                        QuyenNhanVien qnv = new QuyenNhanVien()
                        {
                            MaQ = item.MaQ,
                            TenQ = item.TenQ
                        };
                        list.Add(qnv);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertQuyenNhanVien(QuyenNhanVien info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    QuyenNhanVien inserted = new QuyenNhanVien
                    {

                        TenQ = info.TenQ
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

        public int UpdateQuyenNhanVien(QuyenNhanVien info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    QuyenNhanVien updated = new QuyenNhanVien
                    {
                        MaQ = info.MaQ,
                        TenQ = info.TenQ
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

        public int DeleteQuyenNhanVien(QuyenNhanVien info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.QuyenNhanViens
                                   where p.MaQ == info.MaQ
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
    }
}
