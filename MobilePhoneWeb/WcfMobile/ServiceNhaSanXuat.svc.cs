using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceNhaSanXuat" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceNhaSanXuat.svc or ServiceNhaSanXuat.svc.cs at the Solution Explorer and start debugging.
    public class ServiceNhaSanXuat : IServiceNhaSanXuat
    {

        public List<NhaSanXuat> SelectNhaSanXuat()
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
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }
        //load ds tìm kiếm cho sp
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
        public int InsertNhaSanXuat(NhaSanXuat info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhaSanXuat inserted = new NhaSanXuat
                    {
                        TenNSX = info.TenNSX
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

        public int UpdateNhaSanXuat(NhaSanXuat info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhaSanXuat updated = new NhaSanXuat
                    {
                        MaNSX = info.MaNSX,
                        TenNSX = info.TenNSX
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

        public int DeleteNhaSanXuat(NhaSanXuat info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.NhaSanXuats
                                   where p.MaNSX == info.MaNSX
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

        public List<NhaSanXuat> SelectNhaSanXuatById(int id)
        {
            var list = new List<NhaSanXuat>();
            using (QL_DienThoaiEntities db = new QL_DienThoaiEntities())
            {
                try
                {
                    var nhasanxuat = from p in db.NhaSanXuats where p.MaNSX == id select p;
                    foreach (NhaSanXuat item in nhasanxuat)
                    {
                        NhaSanXuat nsx = new NhaSanXuat()
                        {
                            MaNSX = item.MaNSX,
                            TenNSX = item.TenNSX
                        };
                        list.Add(nsx);
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
