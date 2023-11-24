using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using NongSanDungHa.Models;
using PagedList;

namespace NongSanDungHa.Controllers
{
    public class HomeController : Controller
    {

        DBNongSanDungHaDataContext db = new DBNongSanDungHaDataContext();
  
        // GET: Home
        public ActionResult Index()
        {   
          
            List<product_discount_event> DiscountEvent = db.product_discount_events.Take(4).ToList();
            List<product> products = db.products.ToList();
            List<product_category> Categories = db.product_categories.ToList();
            ViewBag.DiscountEvent = DiscountEvent;
            ViewBag.products = products;
            ViewBag.Category = Categories;
            return View();
        }

        public ActionResult Navigator()
        {
           
            List<product_category> listCategory = db.product_categories.ToList();
            return PartialView(listCategory);
        }

        public ActionResult ShowDetailProduct(int id, int product_category_id)
        {
          
            product Detail = db.products.SingleOrDefault(x => x.product_id == id && x.product_category_id == product_category_id);
            product_category Category = db.product_categories.SingleOrDefault(x => x.product_category_id == product_category_id);
            List<product_image> Images = db.product_images.Where(x => x.product_id == id).ToList();
            ViewBag.NameCategory = Category;
            ViewBag.ImageCount = Images.Count;
            ViewBag.Image = Images;
            ViewBag.DiscountEvent = db.product_discount_events.Take(4).ToList();
            ViewBag.Reviews = db.product_reviews.Where(x => x.product_id == id).ToList();
            return View(Detail);
        }
        public ActionResult ShowCategory(int id, int? page,string filter)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<filter> fil = new List<filter>
            {
                new filter("all","Tất cả ( mới nhất)"),
                new filter("price_max","Giá mắc nhất"),
                new filter("price_min","Giá rẻ nhất")
            };
            ViewBag.FilterItems = new SelectList(fil, "id", "name", filter);
            List<product> ListCategory = new List<product>();
            switch (filter)
            {
               
                case "price_max":
                    ListCategory = db.products.Where(x => x.product_category_id == id).OrderByDescending(x => x.product_price).ToList(); break;
                case "price_min":
                    ListCategory = db.products.Where(x => x.product_category_id == id).OrderBy(x => x.product_price).ToList(); break;
                default:
                    ListCategory = db.products.Where(x => x.product_category_id == id).ToList();
                    break;

            }
            product_category Category = db.product_categories.SingleOrDefault(x => x.product_category_id == id);
            PagedList<product> lst = new PagedList<product>(ListCategory, pageNumber, pageSize);
            ViewBag.NameCategory = Category;
            ViewBag.pageSize = pageSize;
            ViewBag.CountProducts = db.products.Where(x => x.product_category_id == id).Count();
         
            return View(lst);
        }
        [HttpGet]
        public ActionResult SearchResult(string searchKey)
        {
            
            List<product> list = new List<product>();
            if (!string.IsNullOrEmpty(searchKey))
            {
                list = db.products.Where(x => x.product_name.Contains(searchKey)).ToList();
            }
            ViewBag.SearchKey = searchKey;
            return View(list);
        }
        [HttpPost]
        public ActionResult Comment(FormCollection c)
        {
            product_review review = new product_review();
            product product = db.products.SingleOrDefault(x => x.product_id == int.Parse(c["ProductID"]));
            user_account user = db.user_accounts.SingleOrDefault(x => x.user_account_id == int.Parse(c["UserID"]));
            review.user_account_id = int.Parse(c["UserID"]);
            review.product_id = int.Parse(c["ProductID"]);
            review.product_review_content = c["comment-text"].ToString();
            review.review_owner = user.user_firstname + " " + user.user_lastname;
            review.review_time = DateTime.Now;
            db.product_reviews.InsertOnSubmit(review);
            db.SubmitChanges();
            return RedirectToAction("ShowDetailProduct", "Home",new {id = product.product_id, product_category_id = product.product_category_id });
        }
    }
}