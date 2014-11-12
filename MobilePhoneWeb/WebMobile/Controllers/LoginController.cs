using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMobile.Models;
using WebMobile.ServiceReferenceKH;

namespace WebMobile.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private ServiceReferenceKH.ServiceKhachHangClient db = new ServiceReferenceKH.ServiceKhachHangClient();
        public ActionResult Login()
        {
            if(Session["logedID"]==null)
            {
                return View();
            }
            return RedirectToAction("Index","Index");
        }
        [HttpPost]
        //phục vụ cho truong hop mua hang chưa dn, và đăng nhập k cần mua hàng
        public ActionResult Login(TaiKhoan tk)
        {
            if(ModelState.IsValid)
            {
                int id = db.Login(tk.UserName, tk.PassWord);
                var kh = db.GetKHbyId(id);
               
                if (id != 0 && Session["KtDangNhapDH"]==null)// đăng nhập nhưng chưa mua hàng
                {
                    Session["logedId"] = kh.MaKH;
                    Session["logedName"] = kh.HoTen;// lấy họ tên cho topheader
                    return RedirectToAction("Index","Index");
                }
                if (id != 0 && Session["KtDangNhapDH"] != null)//KTdangNhapHD có giá trị, dn thành công sẽ chuyển tới tạo hóa đơn
                { 
                    Session["logedId"] = kh.MaKH;
                    Session["logedName"] = kh.HoTen;
                    Session["KtDangNhapDH"] = null;
                    return RedirectToAction("CreateOrder", "MyCart");
                }
                ViewBag.Error = "<script language=javascript>alert('Tên đăng nhập hoặc mật khẩu không đúng');</script>";// thong bao khi sai mat khẩu va username
            }
            return View();
        }
        // đăng xuất
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Index");
        }
        public JsonResult KiemTraUser(string username)
        {
            return Json(!db.CheckUsername(username), JsonRequestBehavior.AllowGet);
        }
        //Đăng ký ng dùng
         public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(DangKy kh)
        {
            try
            {
                var db = new ServiceReferenceKH.ServiceKhachHangClient();
                if (ModelState.IsValid)
                {
                    var dk = new KhachHang();
                    dk.HoTen = kh.Name;
                    dk.Username = kh.UserName;
                    dk.Password = kh.PassWord;
                    dk.DiaChi = kh.Address;
                    dk.DienThoai = kh.Phone;
                    dk.Email = kh.Email;
                    if (db.InsertKhachHang(dk)==1)
                    {
                        return View("LogOnSuccess");
                    }
                }
                return View(kh);
            }
            catch
            {
                return View(kh);
            }
        }
        public ActionResult LogOnSuccess()
        {
            return View();
        }
        public ActionResult CustomerInfo()
        {
            int id = int.Parse(Session["logedId"].ToString());
            var kh = db.GetKHbyId(id);
            return View(kh);
        }
        public ActionResult ChangeInfo()
        {
            int id = int.Parse(Session["logedId"].ToString());
            var kh = db.GetKHbyId(id);
            var info = new DoiMatKhau();
            Session["UserName"] = kh.Username;
            info.Id = kh.MaKH;
            info.Name = kh.HoTen;
            info.PassWord = kh.Password;
            info.Phone = kh.DienThoai;
            info.Email = kh.Email;
            info.ConfirmPassword = kh.Password;
            info.Address = kh.DiaChi;
            return View(info);
        }
        [HttpPost]
        public ActionResult ChangeInfo(DoiMatKhau info)
        {
            var kh = new KhachHang();
            kh.MaKH = int.Parse(Session["logedId"].ToString());
            kh.HoTen = info.Name;
            kh.Username = Session["UserName"].ToString();
            kh.Password = info.PassWord;
            kh.DienThoai = info.Phone;
            kh.Email = info.Email;
            kh.Password = info.ConfirmPassword;
            kh.DiaChi = info.Address;
            db.UpdateKhachHang(kh);
            return RedirectToAction("Logout");
          
        }
    }
 }

