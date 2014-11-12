using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceQuyenNhanVien" in both code and config file together.
    [ServiceContract]
    public interface IServiceQuyenNhanVien
    {
        [OperationContract]
        List<QuyenNhanVien> SelectQuyenNhanVien();

        [OperationContract]
        int InsertQuyenNhanVien(QuyenNhanVien info);

        [OperationContract]
        int UpdateQuyenNhanVien(QuyenNhanVien info);

        [OperationContract]
        int DeleteQuyenNhanVien(QuyenNhanVien info);
    }
}
