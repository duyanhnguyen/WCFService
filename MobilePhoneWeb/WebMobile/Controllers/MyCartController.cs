using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMobile.Models;
using WebMobile.ServiceReferenceDH;

namespace WebMobile.Controllers
{
    public class MyCartController : Controller
    {
        //
        // GET: /MyCart/
        private ServiceReferenceSanPham.ServiceSanPhamClient db = new ServiceReferenceSanPham.ServiceSanPhamClient();
        private ServiceReferenceKH.ServiceKhachHangClient info = new ServiceReferenceKH.ServiceKhachHangClient();
        private ServiceReferenceDH.ServiceDonHangClient order = new ServiceReferenceDH.ServiceDonHangClient();
        public ActionResult MyCart()
        {
            try
            {
                int num = WebMobile.Models.MySession.GioHang.Count;
                if (num > 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Giohangnull");
                }
            }
            catch
            {
                return RedirectToAction("Giohangnull");
            }
        }
        public ActionResult Giohangnull()
        {
            return View();
        }
        public ActionResult AddToCart(int id)
        {
            //Lưu các mã sản phẩm
            string ma = Session[WebMobile.Models.MySession.MaSanPham] + id.ToString();
            Session[WebMobile.Models.MySession.MaSanPham] = ma;
            //Số lượng sản phẩm có trong giỏ hàng
            int sl = 1 + Convert.ToInt32(Session[WebMobile.Models.MySession.TongSL]);
            Session[WebMobile.Models.MySession.TongSL] = sl.ToString();
            // tìm kiếm ds sp 
            var query = (from p in db.SelectSanPham()
                         where p.MaSP == id
                         select new Products
                         {
                             ID = p.MaSP,
                             NAME = p.TenSP,
                             PRICE = (double)p.Gia + 0,
                             COUNT = p.Gia + 0,
                             NUMBER = 1,
                         }).ToList();
            try
            {
                bool flag = false;
                for (int i = 0; i < MySession.GioHang.Count; i++) // cập nhật số lượng sp trong gh
                {
                    if (MySession.GioHang[i].ID == query[0].ID)
                    {
                        MySession.GioHang[i].NUMBER = MySession.GioHang[i].NUMBER + 1;
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    foreach (var s in query)
                    {
                        MySession.GioHang.Add(s);
                    }
                }
                MySession.COUNT = MySession.COUNT + Convert.ToDouble(query[0].COUNT.ToString());
            }
            catch
            {
                MySession.GioHang = query;
                MySession.COUNT = Convert.ToDouble(query[0].COUNT.ToString());
            }

            return Redirect("../mycart");
        }
        //Cập nhật số lượng sản phẩm trong giỏ
        [HttpPost]
        public ActionResult UpdateCart(FormCollection formCollection)
        {
            int sl = int.Parse(formCollection["USoLuong"]);
            int masp = int.Parse(formCollection["UMaSanPham"]);
            Session[WebMobile.Models.MySession.TongSL] = "0";
            MySession.COUNT = 0;
            for (int i = 0; i < MySession.GioHang.Count; i++)
            {
                if (MySession.GioHang[i].ID == masp)// kiem tra xe có mua trùng sp k
                {
                    if (sl < 1 | sl > 100)// cho mua tôi da 100sp
                    {
                        MySession.GioHang[i].NUMBER = 1;
                        Session[WebMobile.Models.MySession.TongSL] = ((Convert.ToInt32(Session[WebMobile.Models.MySession.TongSL]) + 1)).ToString();
                        MySession.GioHang[i].COUNT = MySession.GioHang[i].PRICE;

                    }
                    else
                    {
                        MySession.GioHang[i].NUMBER = sl;
                        Session[WebMobile.Models.MySession.TongSL] = ((Convert.ToInt32(Session[WebMobile.Models.MySession.TongSL]) + sl)).ToString();
                        MySession.GioHang[i].COUNT = MySession.GioHang[i].PRICE * sl;
                    }
                }
                else
                {
                    Session[WebMobile.Models.MySession.TongSL] = ((Convert.ToInt32(Session[WebMobile.Models.MySession.TongSL]) + MySession.GioHang[i].NUMBER)).ToString();

                }
                MySession.COUNT = MySession.COUNT + MySession.GioHang[i].COUNT;
            }
            return RedirectToAction("MyCart");
        }


        //Xóa sản phẩm trong giỏ
        public ActionResult DeleteCart(int id)
        {
            Session[WebMobile.Models.MySession.TongSL] = "0";
            List<Products> lst = new List<Products>();
            int i = 0;
            MySession.COUNT = 0;
            foreach (var s in MySession.GioHang)
            {
                if (MySession.GioHang[i].ID != id)//
                {
                    lst.Add(s);
                    MySession.COUNT = MySession.COUNT + (MySession.GioHang[i].PRICE * MySession.GioHang[i].NUMBER);
                    Session[WebMobile.Models.MySession.TongSL] = (Convert.ToInt32(Session[WebMobile.Models.MySession.TongSL]) + MySession.GioHang[i].NUMBER).ToString();
                }
                i++;
            }
            MySession.GioHang = lst;
            if (MySession.GioHang.Count == 0)// xoa het thì tra ve trang chủ
            {
                return RedirectToAction("Index", "Index");
            }
            else
            {
                return RedirectToAction("mycart");//k thì load loại giỏ hàng
            }
        }
        private void resetCart()// xoa sach gio hang
        {
            MySession.GioHang = null;
            Session[WebMobile.Models.MySession.TongSL] = null;
            MySession.COUNT = null;
        }
        public ActionResult ClearAll()
        {
            try
            {
                resetCart();
                return RedirectToAction("Giohangnull", "MyCart");
            }
            catch
            {
                return RedirectToAction("Index", "Index");
            }
        }
        // nếu đã đăng nhập thì hóa đơn tự động tạo, không đăng nhập thì trả về trang đăng nhập.
        public ActionResult CreateOrder()// tạo hóa don sau khi dat hàng
        {
            var dh = new DonHang();
            if(Session["logedId"]!=null)
            {
                dh.MaKH = int.Parse(Session["logedId"].ToString());
                dh.Ngay = DateTime.Now;
                dh.Trigia = Convert.ToInt32(MySession.COUNT);
                dh.MaNV = null;
                dh.TinhTrang = 0;
                int id = order.InsertDonHang(dh);
                if(id!=0)
                {
                    foreach(var cthd in MySession.GioHang)
                    {
                        var od = new CT_DonHang();
                        od.MaDH = id;
                        od.MaSP = cthd.ID.Value;
                        od.SoLuong = cthd.NUMBER;
                        od.Gia = Convert.ToInt32(cthd.PRICE);
                        order.InsertCTDH(od);
                    }
                    resetCart();
                    return RedirectToAction("CreateSuccess");// tạo thanh cong
                }
                else
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else// trả về trang đăng nhập
            {
                Session["KtDangNhapDH"] = "needlogin";
                return RedirectToAction("Login","Login");
            }
        }
        public ActionResult CreateSuccess()
        {
            return View();
        }
    }
}
