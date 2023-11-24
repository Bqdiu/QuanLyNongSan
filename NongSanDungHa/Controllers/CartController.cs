using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using NongSanDungHa.common;
using NongSanDungHa.Models;


namespace NongSanDungHa.Controllers
{
    public class CartController : Controller
    {
        DBNongSanDungHaDataContext db = new DBNongSanDungHaDataContext();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // GET: Cart
        public ActionResult Index()
        {
           
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddToCart(int product_id, double quantity)
        {
            var pro = db.products.SingleOrDefault(x => x.product_id == product_id);
        
          
                if (pro != null)
                {
                    GetCart().Add(pro, quantity);
                }
          
          
       
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public ActionResult Update_Quantity_Increase(int product_id,double quantity)
        {
            var pro = db.products.SingleOrDefault(x => x.product_id == product_id);
            if (pro != null)
            {
                GetCart().Update_Quantity_Increase(pro.product_id,quantity);
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult Update_Quantity_Decrease(int product_id, double quantity)
        {
            var pro = db.products.SingleOrDefault(x => x.product_id == product_id);
            if (pro != null)
            {
                GetCart().Update_Quantity_Decrease(pro.product_id, quantity);
            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public ActionResult Remove_CartItem(int product_id)
        {
            var pro = db.products.SingleOrDefault(x => x.product_id == product_id);
            if(pro != null && GetCart().Items.Count() > 1)
            {
                GetCart().Delete(pro.product_id);

            }
            else if (pro != null && GetCart().Items.Count() == 1)
            {
                GetCart().Delete(pro.product_id);
                
                Session["Cart"] = null;
            }    
            
            
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Remove_AllCartItem()
        {
            Session["Cart"] = null;
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult ConfirmPayment()
        {
            return View();
        }
        public ActionResult DetailCart(int UserID)
        {
            List<user_order> order = db.user_orders.Where(x=> x.user_account_id == UserID).ToList();
            return View(order);
        }
        public ActionResult DetailCartItems(int OrderID)
        {
           
            var OrderItems = db.user_order_products.Include(x=> x.product).Where(x=> x.user_order_id == OrderID).ToList();
           
            return View(OrderItems);
        }
        public ActionResult Payment()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult Payment(int UserID,NongSanDungHa.Models.ADO.user_order user1, string payments, string accept)
        {

            user_account user = db.user_accounts.SingleOrDefault(x => x.user_account_id == UserID);
            int kt = 0;
            if (user != null && accept == "true")
            {
                user_order userOrder = new user_order();
                userOrder.user_account_id = user.user_account_id;
                userOrder.user_order_address = user1.user_order_address;
                userOrder.user_order_phonenumber = user1.user_order_phonenumber;
                userOrder.user_order_buyer_name = user1.user_order_buyer_name;
                userOrder.user_order_email = user1.user_order_email;
                userOrder.order_time = DateTime.Now;
                userOrder.order_total_value = GetCart().Total_Money();
                userOrder.is_delivered = "Đang chuẩn bị hàng";
                userOrder.is_processed = "Đơn hàng đang xử lý";
                userOrder.payments = payments;
                db.user_orders.InsertOnSubmit(userOrder);
                db.SubmitChanges();
                foreach(var item in GetCart().Items)
                {
                    
                    user_order_product order = new user_order_product();
                    order.product_id = item._shopping_product.product_id;
                    order.user_order_id = userOrder.user_order_id;
                    order.order_product_amount = item._shopping_quantity;
                    order.product_name = item._shopping_product.product_name;
                    db.user_order_products.InsertOnSubmit(order);
                    db.SubmitChanges();
                }
                //Send Email
                var strSanPham = "";
                foreach(var item in GetCart().Items)
                {
                    strSanPham += "<tr style=\"text-align:center\">";
                    strSanPham += "<th scope=\"row\" ></th>";
                    strSanPham += "<td style=\"font-weight:bold;color:red;\">" + item._shopping_product.product_name+"</td>";
                    strSanPham += "<td>" + item._shopping_quantity + "</td>";
                    strSanPham += "<td>" + item._shopping_product.product_price + "đ" + "</td>";
                    strSanPham += "<td></td>";
                    strSanPham += "<td>" + (item._shopping_product.product_price * Convert.ToDecimal(item._shopping_quantity)) + "đ" + "</td>";
                    strSanPham += "</tr>";
                }    
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/template-Email/neworder.html"));
                content = content.Replace("{{SanPham}}", strSanPham);    
                content = content.Replace("{{TongTien}}", Convert.ToString(userOrder.order_total_value));
                content = content.Replace("{{TenKhachHang}}", Convert.ToString(userOrder.user_order_buyer_name));
                content = content.Replace("{{SDT}}", Convert.ToString(userOrder.user_order_phonenumber));
                content = content.Replace("{{Email}}", Convert.ToString(userOrder.user_order_email));
                content = content.Replace("{{DiaChi}}", Convert.ToString(userOrder.user_order_address));
                content = content.Replace("{{TrangThai}}", Convert.ToString(userOrder.is_processed));
                content = content.Replace("{{HinhThuc}}", Convert.ToString(userOrder.payments));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(userOrder.user_order_email, "Đơn Hàng mới từ Nông Sản Dung Hà", content);
                new MailHelper().SendMail(toEmail, "Đơn Hàng mới từ Nông Sản Dung Hà", content);
                Session["Cart"] = null;
            }
            else if(accept != "true")
            {
                kt = 1;
                ViewBag.kt = kt;
                ViewBag.Accept = "Bạn chưa đồng ý với tất cả chính sách";
            }
            
                
            return RedirectToAction("Payment", "Cart");
        }
    }
}