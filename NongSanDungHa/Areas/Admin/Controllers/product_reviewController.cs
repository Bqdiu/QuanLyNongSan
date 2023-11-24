using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class product_reviewController : Controller
    {
        // GET: Admin/product_review
        DBConnection db = new DBConnection();
        // GET: Admin/product
        public ActionResult Index()
        {
            ListProduct_review proRe = new ListProduct_review();
            ViewBag.TotalProductReview = proRe.getData().Count();
            List< product_review> list = proRe.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( product_review proRe)
        {
            ListProduct_review list = new ListProduct_review();
            list.insert(proRe);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            ListProduct_review list = new ListProduct_review();
             product_review proRe = list.Details(id).FirstOrDefault();
            return View(proRe);
        }
        public ActionResult Edit(int id)
        {
            ListProduct_review list = new ListProduct_review();
             product_review item = list.Details(id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( product_review proRe)
        {
            ListProduct_review list = new ListProduct_review();
            list.update(proRe);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListProduct_review list = new ListProduct_review();

            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}