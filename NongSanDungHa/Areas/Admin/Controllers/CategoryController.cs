using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        DBConnection db = new DBConnection();
        // GET: Admin/product
        public ActionResult Index()
        {
            ListProductCategory pro = new ListProductCategory();
            ViewBag.TotalCategory = pro.getData().Count();
            List< product_category> list = pro.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( product_category cate)
        {
            ListProductCategory list = new ListProductCategory();
            list.insert(cate);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            ListProductCategory cate = new ListProductCategory();
             product_category category = cate.Details(id).FirstOrDefault();
            return View(category);
        }
        public ActionResult Edit(int id)
        {
            ListProductCategory list = new ListProductCategory();
             product_category item = list.Details(id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( product_category cate)
        {
            ListProductCategory list = new ListProductCategory();
            list.update(cate);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListProductCategory list = new ListProductCategory();

            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}