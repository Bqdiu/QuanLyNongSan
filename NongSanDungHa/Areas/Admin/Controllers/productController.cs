using NongSanDungHa.Models.ADO; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class productController : Controller
    {
        DBConnection db = new DBConnection();
        // GET: Admin/product
        public ActionResult Index()
        {
            ListProduct pro = new ListProduct();
            ViewBag.TotalProduct = pro.getData().Count();
            List< product> list = pro.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( product pro)
        {
            ListProduct list = new  ListProduct();
            pro.product_description = System.Net.WebUtility.HtmlDecode(pro.product_description);
            list.insert(pro);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id, int category_id)
        {
            ListProduct pro = new ListProduct();
             product product = pro.details(id,category_id).FirstOrDefault();
            return View(product);
        }
        public ActionResult Edit(int id, int category_id)
        {
           ListProduct list = new ListProduct();
            product item = list.details(id,category_id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        
        public ActionResult Edit( product pro)
        {
            ListProduct list = new ListProduct();
            pro.product_description = System.Net.WebUtility.HtmlDecode(pro.product_description);
            list.update(pro);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListProduct list = new ListProduct();
            
            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}