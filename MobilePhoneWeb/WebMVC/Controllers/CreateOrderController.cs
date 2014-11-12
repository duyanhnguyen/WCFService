using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMobile.Models;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.Text;

namespace WebMobile.Controllers
{
    public class CreateOrderController : Controller
    {
        //
        // GET: /CreateOrder/

        public ActionResult Index()
        {
            return View(db.vieworder());
        }

        public ActionResult ShowOrder(int idhd)
        {
            //            var dtod = db.detailorder(idhd);
            return View(db.detailorder(idhd));
        }
        public ActionResult Create()
        {
            if (Session["UserName"] != null)//đã đăng nhập.
            {
                 string username = Session["UserName"].ToString();// lấy tên của khách hàng
                 string password = Session["PassWord"].ToString();
                 using(var sl = new ServiceReferenceCustomer.ServiceCustomerClient())
                    {
                        string rule = "Kh";
                        var cus = sl.CheckLogin(username, password, rule);
                        var order = new OrderModel();
                        order.Customer_Name = cus.Name;
                        order.Address = cus.Address;
                        order.Phone = cus.Phone;
                        return View(order);
                    }
            }
            else
            {
                return View();
            }
           
           
        }
        [HttpPost]
        public ActionResult Create(OrderModel od)
        {
            if (ModelState.IsValid)
            {
                var order = new Order();
                order.Customer_Name = od.Customer_Name;
                order.Date = DateTime.Now;
                order.Phone = od.Phone;
                order.Status = "Chưa liên hệ";
                order.Address = od.Address;
                order.TotalMoney = MySession.COUNT;
                int id = db.InsertOrder(order);
                if (id!= 0)
                {
                    foreach (var i in MySession.GioHang)
                    {
                        var orderdt = new OrderDetail();
                        orderdt.Product_Id = i.ID.Value;
                        orderdt.Quantity = i.NUMBER.Value;
                        orderdt.Price = i.NUMBER.Value * i.PRICE.Value;
                        orderdt.Order_Id = id;
                        db.InsertOrderDetail(orderdt);
                    }
                    ResetCard();
                    return RedirectToAction("CreateSuccess");
                }
            }
            return View(od);   
        }
        public void ResetCard()
        {
            MySession.GioHang = null;
            Session[WebMobile.Models.MySession.TongSL] = null;
            MySession.COUNT = 0;
        }
        public ActionResult CreateSuccess()
        {
            return View();
        }

        public ActionResult ChangeStatus(int id)
        {

            if (db.ChangeStatus(id))
            {
                db.UpdateSoLuongSanPham(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        public ActionResult CancelOrder(int id)
        {
            var or = new ServiceReferenceCreateOrder.ServiceOrderClient();
            if (db.CancelOrder(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Timkiem(FormCollection fc)
        {
            return RedirectToAction("OrderMonth", new { month = fc["month"], year = fc["year"] });
        }

        public ActionResult ExportData(int id)
        {
             StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = "Hoadon.xls";
            //Bind data list from edmx
            var Data = db.detailorder(id);
            if (Data != null && Data.Any())
            {
                sb.Append("<table style='2px solid black; font-size:15px;'>");
                sb.Append("<tr ALIGN="+"CENTER"+">");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='width:90px;'><b>Mã Sản Phẩm</b></td>");
                sb.Append("<td style='width:150px;'><b>Tên Sản Phẩm</b></td>");
                sb.Append("<td style='width:80px;'><b>Đơn Gía</b></td>");
                sb.Append("<td style='width:80px;'><b>Số lượng</b></td>");
                sb.Append("<td style='width:100px;'><b>Thành tiền</b></td>");
                sb.Append("</tr>");

                foreach (var result in Data)
                {
                    sb.Append("<tr ALIGN=" + "CENTER" + ">");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td>" + result.MaSP + "</td>");
                    sb.Append("<td>" + result.TenSP + "</td>");
                    sb.Append("<td>" + result.Gia + "</td>");
                    sb.Append("<td>" + result.SoLuong + "</td>");
                    sb.Append("<td>" + result.ThanhTien + "</td>");
                    sb.Append("</tr>");
                }
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");          
        }

    }
}
