using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Admin/Discount
        DBConnection db = new DBConnection();
        // GET: Admin/product
        public ActionResult Index()
        {
            ListDiscountEvent pro = new ListDiscountEvent();
            ViewBag.TotalDiscount = pro.getData().Count();
            List<product_discount_event> list = pro.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( product_discount_event disc)
        {
            ListDiscountEvent list = new ListDiscountEvent();
            list.insert(disc);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            ListDiscountEvent disc = new ListDiscountEvent();
             product_discount_event item = disc.Details(id).FirstOrDefault();
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            ListDiscountEvent list = new ListDiscountEvent();
             product_discount_event item = list.Details(id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( product_discount_event disc)
        {
            ListDiscountEvent list = new ListDiscountEvent();
            list.update(disc);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListDiscountEvent list = new ListDiscountEvent();

            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}