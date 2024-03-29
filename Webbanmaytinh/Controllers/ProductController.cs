﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanmaytinh.Models;
using PagedList;
using PagedList.Mvc;

namespace Webbanmaytinh.Controllers
{
    public class ProductController : Controller
    {
        DBSportStoreEntities database = new DBSportStoreEntities();
        public ActionResult SearchOption(double min=double.MinValue,double max=double.MaxValue)
        {
            var list = database.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }
        public ActionResult Index(string currentFilter, string category, string searchBy, string search, int? page, double min = double.MinValue, double max = double.MaxValue)
        {
            ViewBag.currentFilter = category;
            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (search!=null)
            {
                var productList = database.Products.Where(s => s.NamePro.Contains(search)).OrderByDescending(x=>x.NamePro);
                return View(productList.ToPagedList(pageNum, pageSize));
                
            }
            if (category==null)
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro);
                return View(productList.ToPagedList(pageNum, pageSize));
                //return View(productList);
            }
            else
            {
                var productList = database.Products.OrderByDescending(x => x.NamePro)
                    .Where(x => x.Category == category);
                return View(productList.ToPagedList(pageNum,pageSize));
            }
            
        }
        public ActionResult Create()
        {
            List<Category> list = database.Categories.ToList();
            ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "");
            Product pro = new Product();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Product pro)
        {
            List<Category> list = database.Categories.ToList();
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/images/" + filename;
                    pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", 1);
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList<Category>();
            return PartialView(se_cate);
        }
        public ActionResult Details(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }


    }
}