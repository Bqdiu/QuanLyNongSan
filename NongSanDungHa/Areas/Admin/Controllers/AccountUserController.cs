using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class AccountUserController : Controller
    {
        // GET: Admin/AccountUser
        DBConnection db = new DBConnection();
        
        public ActionResult Index()
        {
            ListUserAccount user = new ListUserAccount();
            ViewBag.TotalUser = user.getData().Count();
            List< user_account> list = user.getData().ToList();
            return View(list);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( user_account user)
        {
            ListUserAccount list = new ListUserAccount();
            list.CreateUserAccount(user);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            ListUserAccount list = new ListUserAccount();
            user_account user = list.Details(id).SingleOrDefault();
            return View(user);
        }
        public ActionResult Edit(int id)
        {
            ListUserAccount list = new ListUserAccount();
            user_account item = list.Details(id).SingleOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( user_account user)
        {
            ListUserAccount list = new ListUserAccount();
            list.update(user);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListUserAccount list = new ListUserAccount();

            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}