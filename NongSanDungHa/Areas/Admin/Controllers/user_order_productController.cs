using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class user_order_productController : Controller
    {
        // GET: Admin/user_order_product
        DBConnection db = new DBConnection();
        // GET: Admin/product
        public ActionResult Index()
        {
            ListUser_Order_Product proRe = new ListUser_Order_Product();
            ViewBag.TotalUserOrderProduct = proRe.getData().Count();
            List< user_order_product> list = proRe.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( user_order_product proRe)
        {
            ListUser_Order_Product list = new ListUser_Order_Product();
            list.insert(proRe);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id, int product_id)
        {
            ListUser_Order_Product list = new ListUser_Order_Product();
             user_order_product proRe = list.Details(id, product_id).FirstOrDefault();
            return View(proRe);
        }
        public ActionResult Edit(int id, int product_id)
        {
            ListUser_Order_Product list = new ListUser_Order_Product();
             user_order_product item = list.Details(id, product_id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( user_order_product proRe)
        {
            ListUser_Order_Product list = new ListUser_Order_Product();
            list.update(proRe);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id, int product_id)
        {
            ListUser_Order_Product list = new ListUser_Order_Product();

            list.delete(id, product_id);
            return RedirectToAction("Index");
        }
    }
}