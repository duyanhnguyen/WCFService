using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceNhaSanXuat" in both code and config file together.
    [ServiceContract]
    public interface IServiceNhaSanXuat
    {
        [OperationContract]
        List<NhaSanXuat> SelectNhaSanXuat();

        [OperationContract]
        int InsertNhaSanXuat(NhaSanXuat info);

        [OperationContract]
        int UpdateNhaSanXuat(NhaSanXuat info);

        [OperationContract]
        int DeleteNhaSanXuat(NhaSanXuat info);

        [OperationContract]
        List<NhaSanXuat> SelectNhaSanXuatById(int id);
        [OperationContract]
        List<NhaSanXuat> GetListTKNSX();
    }
}
