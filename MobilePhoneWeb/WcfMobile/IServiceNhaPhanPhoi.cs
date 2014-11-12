using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceNhaPhanPhoi" in both code and config file together.
    [ServiceContract]
    public interface IServiceNhaPhanPhoi
    {
        [OperationContract]
        List<NhaPhanPhoi> SelectNhaPhanPhoi();

        [OperationContract]
        int InsertNhaPhanPhoi(NhaPhanPhoi info);

        [OperationContract]
        int UpdateNhaPhanPhoi(NhaPhanPhoi info);

        [OperationContract]
        int DeleteNhaPhanPhoi(NhaPhanPhoi info);
    }
}
