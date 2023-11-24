using NongSanDungHa.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace NongSanDungHa.Controllers
{
    public class UserController : Controller
    {
        DBNongSanDungHaDataContext db = new DBNongSanDungHaDataContext();

        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(NongSanDungHa.Models.ADO.user_account u)
        {
            if (ModelState.IsValid)
            {


                NongSanDungHa.Models.ADO.ListUserAccount user = new NongSanDungHa.Models.ADO.ListUserAccount();
                int kt = user.CreateUserAccount(u);
                ViewBag.kt = kt;
                if (kt == 0)
                    ViewBag.message = "Tạo tài khoản thành công";
                else if (kt != 0)
                    ViewBag.message = "Đã có tài khoản này rồi";
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
           
            var userCheck = db.user_accounts.SingleOrDefault(x => x.user_username.Equals(Username) && x.user_password.Equals(Password));
            var adminCheck = db.admin_accounts.SingleOrDefault(x => x.admin_username.Equals(Username) && x.admin_password.Equals(Password));
            if (userCheck != null)
            {
                Session["User"] = userCheck;
                return RedirectToAction("Index", "Home");
            }
            if (adminCheck != null)
            {
                Session["User"] = adminCheck;
                return Redirect("/Admin/Dashboard/Index");
            }    
            else
            {
                ViewBag.message = "Vui lòng nhập đúng tài khoản và mật khẩu";
            } 
                

            return View();
        }
 
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult ChangePassword()
        {
         
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword,string newPassword,string RetypePassword)
        {
            if(ModelState.IsValid)
            {
                var user = Session["User"] as  user_account;
                int kt = 1;
                if (user.user_password == oldPassword && newPassword == RetypePassword)
                {
                    kt = 0;
                    ViewBag.kt = kt;
                    NongSanDungHa.Models.ADO.ListUserAccount list = new NongSanDungHa.Models.ADO.ListUserAccount();
                    list.updatePassword(user.user_account_id, newPassword);
                    ViewBag.message = "Đổi mật khẩu thành công";

                }
                else if (user.user_password != oldPassword)
                {

                    ViewBag.kt = kt;
                    ViewBag.message = "Đổi mật khẩu không thành công, vui lòng kiểm tra lại mật khẩu cũ";
                }
                else if (newPassword != RetypePassword)
                {
                    ViewBag.message = kt;
                    ViewBag.message = "New Password và Retype Password không trùng khớp";
                }
            }
           
                
          
               
               
           

            return View();
        }

        public ActionResult UserInformation(int UserID)
        {
          
            NongSanDungHa.Models.ADO.ListUserAccount list = new Models.ADO.ListUserAccount();
            NongSanDungHa.Models.ADO.user_account user = list.Details(UserID).FirstOrDefault();
            return View(user);
        }
        public ActionResult ChangeUserInformation (int UserID)
        {

            NongSanDungHa.Models.ADO.ListUserAccount list = new Models.ADO.ListUserAccount();
            NongSanDungHa.Models.ADO.user_account user = list.Details(UserID).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangeUserInformation(NongSanDungHa.Models.ADO.user_account user)
        {

            NongSanDungHa.Models.ADO.ListUserAccount userUpdate = new Models.ADO.ListUserAccount();
            userUpdate.update(user);


            return RedirectToAction("UserInformation", "User", new { UserID = user.user_account_id });
        }
    }
}