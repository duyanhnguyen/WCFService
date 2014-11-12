using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMobile
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceDonHang" in both code and config file together.
    [ServiceContract]
    public interface IServiceDonHang
    {
        [OperationContract]
        List<DonHang> SelectDonHang();

        [OperationContract]
        int InsertDonHang(DonHang info);

        [OperationContract]
        int UpdateDonHang(DonHang info);

        [OperationContract]
        List<CT_DonHang> SelectCTDH();

        [OperationContract]
        int InsertCTDH(CT_DonHang info);

        [OperationContract]
        int UpdateCTDH(CT_DonHang info);

        //  HOAI
        [OperationContract]
        List<DonHangKhachHang> SelectDonHangKhachHang();

        [OperationContract]
        List<CT_DonHangSanPham> SelectCT_DonHangByDonHang(int id);

        [OperationContract]
        int UpdateTinhTrangDonHang(DonHang info);

        //[OperationContract]
        //int SelectMakhByMadh(int madh);

        [OperationContract]
        int UpdateSoLuongSanPham(DonHang info);

        [OperationContract]
        List<Thongke> BaocaoNgay(string ngay1, string ngay2);

        [OperationContract]
        List<Thongke> ThongkeNVThang(int manv, int thang, int nam);

        [OperationContract]
        List<Thongke> ThongkeSPThang(int thang, int nam);
    }

    [DataContract]
    public class DonHangKhachHang
    {
        int madh;
        [DataMember]
        public int MaDH
        {
            get { return madh; }
            set { madh = value; }
        }
        DateTime ngay;
        [DataMember]
        public DateTime Ngay
        {
            get { return ngay; }
            set { ngay = value; }
        }
        string hoten;
        [DataMember]
        public string HoTen
        {
            get { return hoten; }
            set { hoten = value; }
        }
        string dienthoai;
        [DataMember]
        public string DienThoai
        {
            get { return dienthoai; }
            set { dienthoai = value; }
        }
        string diachi;
        [DataMember]
        public string DiaChi
        {
            get { return diachi; }
            set { diachi = value; }
        }
        int trigia;
        [DataMember]
        public int Trigia
        {
            get { return trigia; }
            set { trigia = value; }
        }
        string tinhtrang;
        [DataMember]
        public string Tinhtrang
        {
            get { return tinhtrang; }
            set { tinhtrang = value; }
        }
    }


    [DataContract]
    public class CT_DonHangSanPham
    {
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
        int thanhtien;
        [DataMember]
        public int ThanhTien
        {
            get { return thanhtien; }
            set { thanhtien = value; }
        }
    }

    [DataContract]
    public class Thongke
    {
        int id;
        string ten;
        int dongia;
        int soluong;
        int thanhtien;


        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string TEN
        {
            get { return ten; }
            set { ten = value; }
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

    [DataContract]
    public class UpdateDonHang
    {
        int madh;
        [DataMember]
        public int MaDH
        {
            get { return madh; }
            set { madh = value; }
        }
        int tinhtrang;
        [DataMember]
        public int TinhTrang
        {
            get { return tinhtrang; }
            set { tinhtrang = value; }
        }
        int manv;
        [DataMember]
        public int MaNV
        {
            get { return manv; }
            set { manv = value; }
        }
    }
}
