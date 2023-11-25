
using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        // GET: Admin/AccountAdmin
        DBConnection db = new DBConnection();

        public ActionResult Index(int ?page)
        {
           
            ListAdminAccount user = new ListAdminAccount();
            ViewBag.TotalAdmin = user.getData().Count();
            List<admin_account> list = user.getData().ToList();
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<admin_account> lst = new PagedList<admin_account>(list, pageNumber, pageSize);
            return View(lst);
        }
        public ActionResult CreateNew()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateNew( admin_account user)
        {
            ListAdminAccount list = new ListAdminAccount();
            list.CreateAdminAccount(user);

            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            ListAdminAccount list = new ListAdminAccount();
             admin_account user = list.Details(id).FirstOrDefault();
            return View(user);
        }
        public ActionResult Edit(int id)
        {
            ListAdminAccount list = new ListAdminAccount();
             admin_account item = list.Details(id).FirstOrDefault();

            return View(item);
        }
        [HttpPost]
        public ActionResult Edit( admin_account user)
        {
            ListAdminAccount list = new ListAdminAccount();
            list.update(user);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ListAdminAccount list = new ListAdminAccount();

            list.delete(id);
            return RedirectToAction("Index");
        }
    }
}