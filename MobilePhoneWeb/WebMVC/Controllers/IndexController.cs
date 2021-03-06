﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMobile.Models;

namespace WebMobile.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        private 
        public ActionResult Index()
        {
            ViewBag.Views = "Index";
            return View(db.SelectSanPham().OrderByDescending(i => i.MaSP).Take(8));
        }
        public ActionResult Category(int id, int? page)
        {
            var pros = db.SelectSanPhamByCategory(id);
            string cate = db.Category(id).ToString();
            ViewBag.Category = cate;
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(pros.ToPagedList(pageNumber, pageSize));
        }
        //[ChildActionOnly]
        public ActionResult SPcaocap()
        {
            var pr = db.SelectSanPham().OrderByDescending(i => i.Gia).Take(8);
            ViewBag.Views = "sanphamcaocap";
            return PartialView(pr);
        }
        public ActionResult Details(int id)
        {
            var p = new Products();
            var pro = db.GetSPbyID(id);
            p.ID = pro.MaSP ;
            p.NAME = pro.TenSP;
            p.PRICE = pro.Gia;
            p.IMAGES = pro.UrlHinh;
            p.DETAILS = pro.MoTa;
            return View(p);
        }
        //Tìm kiếm
        [HttpPost]
        public ActionResult Timkiem(FormCollection fc)
        {
            return RedirectToAction("KetQuaTK", new { id = int.Parse(fc["group_id"]), giatu = fc["giatu"], den = fc["den"] });
        }
        public ActionResult KetQuaTK(int id, string price_from, string to, int ? page)
        {
            //lấy giá từ đến trên querystring của droplist
            price_from = Request.QueryString["giatu"];
            to = Request.QueryString["den"];
            //tìm sp phẩm
            var pros = db.SearchSanPham(id, price_from, to);
            return View(pros.Take(8).OrderByDescending(i=>i.Gia).ToList());
        }
      
    }
}