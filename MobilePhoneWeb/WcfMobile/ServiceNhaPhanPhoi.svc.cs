using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceNhaPhanPhoi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceNhaPhanPhoi.svc or ServiceNhaPhanPhoi.svc.cs at the Solution Explorer and start debugging.
    public class ServiceNhaPhanPhoi : IServiceNhaPhanPhoi
    {
        public List<NhaPhanPhoi> SelectNhaPhanPhoi()
        {
            var list = new List<NhaPhanPhoi>();
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var nhaphanphoi = from p in db.NhaPhanPhois select p;
                    foreach (var item in nhaphanphoi)
                    {
                        NhaPhanPhoi npp = new NhaPhanPhoi()
                        {
                            MaNPP = item.MaNPP,
                            TenNPP = item.TenNPP,
                            DiaChi = item.DiaChi,
                            Email = item.Email,
                            SoDT = item.SoDT
                        };
                        list.Add(npp);
                    }
                }
                catch
                {
                    return null;
                }
            }
            return list;
        }

        public int InsertNhaPhanPhoi(NhaPhanPhoi info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhaPhanPhoi inserted = new NhaPhanPhoi
                    {
                        TenNPP = info.TenNPP,
                        DiaChi = info.DiaChi,
                        Email = info.Email,
                        SoDT = info.SoDT
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

        public int UpdateNhaPhanPhoi(NhaPhanPhoi info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    NhaPhanPhoi updated = new NhaPhanPhoi
                    {
                        MaNPP = info.MaNPP,
                        TenNPP = info.TenNPP,
                        DiaChi = info.DiaChi,
                        Email = info.Email,
                        SoDT = info.SoDT
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

        public int DeleteNhaPhanPhoi(NhaPhanPhoi info)
        {
            using (var db = new QL_DienThoaiEntities())
            {
                try
                {
                    var deleted = (from p in db.NhaPhanPhois
                                   where p.MaNPP == info.MaNPP
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
