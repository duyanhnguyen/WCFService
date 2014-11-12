using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicePhieuNhap" in both code and config file together.
    [ServiceContract]
    public interface IServicePhieuNhap
    {
        [OperationContract]
        List<PhieuNhap> SelectPhieuNhap();

        [OperationContract]
        int InsertPhieuNhap(PhieuNhap info);

        [OperationContract]
        int UpdatePhieuNhap(PhieuNhap info);

        [OperationContract]
        int DeletePhieuNhap(PhieuNhap info);

        [OperationContract]
        List<CT_PhieuNhap> SelectCTPN();

        [OperationContract]
        List<PhieuNhap_SanPham> SelectCTPN_ByMaPN(int ma);

        [OperationContract]
        int InsertCTPN(CT_PhieuNhap info);

        [OperationContract]
        int UpdateCTPN(CT_PhieuNhap info);

        [OperationContract]
        int DeleteCTPN(int mapn);

        [OperationContract]
        int GetNewIDPhieuNhap();

        [OperationContract]
        int UpdateSoLuongSanPham(int masp, int soluong);

        [OperationContract]
        List<ThongkePN> ThongkeSPNhapThang(int thang, int nam);
    }

    [DataContract]
    public class PhieuNhap_SanPham
    {
        int mapn;
        [DataMember]
        public int MaPN
        {
            get { return mapn; }
            set { mapn = value; }
        }

        int masp;
        [DataMember]
        public int MaSP
        {
            get { return masp; }
            set { masp = value; }
        }

        string tensp;
        [DataMember]
        public string TenSP
        {
            get { return tensp; }
            set { tensp = value; }
        }

        int soluong;
        [DataMember]
        public int SoLuong
        {
            get { return soluong; }
            set { soluong = value; }
        }

        int gia;
        [DataMember]
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
    }

    public class ThongkePN
    {
        int masp;
        string tensp;
        int dongia;
        int soluong;
        int thanhtien;
        [DataMember]
        public int ID
        {
            get { return masp; }
            set { masp = value; }
        }

        [DataMember]
        public string TEN
        {
            get { return tensp; }
            set { tensp = value; }
        }

        [DataMember]
        public int DONGIA
        {
            get { return dongia; }
            set { dongia = value; }
        }

        [DataMember]
        public int SOLUONG
        {
            get { return soluong; }
            set { soluong = value; }
        }

        [DataMember]
        public int THANHTIEN
        {
            get { return thanhtien; }
            set { thanhtien = value; }
        }
    }
}
