using NongSanDungHa.Models;
using NongSanDungHa.Models.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NongSanDungHa.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ListProduct p = new ListProduct();
            int totalProducts = p.TotalProducts();
            ViewBag.TotalProducts = totalProducts;
            ListUser_Order order = new ListUser_Order();
            int totalOrders = order.GetTotalOrders();
            ViewBag.TotalOrders = totalOrders;
            ListUserAccount account = new ListUserAccount();
            int totalAccounts = account.TotalGetTotalAccounts();
            ViewBag.TotalAccounts = totalAccounts;
            ListProductCategory category = new ListProductCategory();
            int totalCategories = category.GetTotalCategories();
            ViewBag.TotalCategories = totalCategories;

            DateTime now = DateTime.Now;
            int current_month = now.Month;
            ViewBag.current_month = current_month;
            decimal MonthlyRevenue = order.GetMonthlyRevenue();
            ViewBag.MonthlyRevenue = MonthlyRevenue;

            int current_year = now.Year;
            ViewBag.current_year = current_year;
            decimal YearlyRevenue = order.GetYearlyRevenue();
            ViewBag.YearlyRevenue = YearlyRevenue;
            return View();
        }


    }
}