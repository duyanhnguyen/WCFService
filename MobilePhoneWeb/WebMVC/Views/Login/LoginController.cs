using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMobile.Models;

namespace WebMobile.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        ServiceReferenceCustomer.ServiceCustomerClient db = new ServiceReferenceCustomer.ServiceCustomerClient();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TaiKhoan tk)
        {
            if (ModelState.IsValid)//k lỗi
            {
                string rule1 = "Kh";
                var v1 = db.CheckLogin(tk.UserName,tk.PassWord,rule1);
                if (v1 != null)
                {
                    Session["Name"] = v1.Name;
                    Session["UserName"] = v1.UserName;
                    Session["PassWord"] = v1.PassWord;
                    return RedirectToAction("Index", "Index");       
                }
                else
                {
                    string rule2 = "Admin";
                    var v2 = db.CheckLogin(tk.UserName, tk.PassWord, rule2);
                    if(v2 != null)
                    {
                        Session["Name"] = v2.Name;
                        return RedirectToAction("Index", "Customer");
                    }
                }
                ViewBag.Error = "<script language=javascript>alert('Tên đăng nhập hoặc mật khẩu không đúng');</script>";
            }
            return View();
        }
     
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Index");
        }
        public ActionResult LogOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOn(KhachHangModel kh)
        {
            try
            {
                var db = new ServiceReferenceCustomer.ServiceCustomerClient();
                if (ModelState.IsValid)
                {
                    if (db.Insert(kh.Name, kh.UserName, kh.PassWord, kh.Address, kh.Phone, kh.Email, "Kh") == true)
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
    }
}
