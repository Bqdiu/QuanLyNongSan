using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class user_order_product
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "ID Đơn hàng")]
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "không được nhập số âm")]
        public int user_order_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "ID Sản Phẩm")]
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "không được nhập số âm")]
        public int product_id { get; set; }

        [StringLength(255)]
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Không được để trống")]
        public string product_name { get; set; }
        [Display(Name = "Số sản phẩm")]
        [Required(ErrorMessage = "Không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "không được nhập số âm")]
        public double? order_product_amount { get; set; }
    }
    public partial class ListUser_Order_Product
    {
        DBConnection db;
        public ListUser_Order_Product()
        {
            db = new DBConnection();

        }
        public List<user_order_product> getData()
        {
            string sql;

            sql = "Select * from user_order_product";


            List<user_order_product> list = new List<user_order_product>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order_product item = new user_order_product();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_name = dr["product_name"].ToString();
                item.order_product_amount = double.Parse(dr["order_product_amount"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<user_order_product> Details(int id, int product_id)
        {
            string sql;

            sql = "Select * from user_order_product where user_order_id = '" + id + "' and product_id = '" + product_id + "'";


            List<user_order_product> list = new List<user_order_product>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order_product item = new user_order_product();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_name = dr["product_name"].ToString();
                item.order_product_amount = double.Parse(dr["order_product_amount"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;
        }

        public void update(user_order_product item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update user_order_product set user_order_id = @user_order_id,product_id =@product_id, product_name=@product_name,order_product_amount=@order_product_amount  where user_order_id = @user_order_id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@user_order_id", item.user_order_id);
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_name", item.product_name);
            cmd.Parameters.Add("@order_product_amount", item.order_product_amount);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void delete(int id, int product_id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from user_order_product where user_order_id =@id  and product_id = @product_id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", id);
            cmd.Parameters.Add("@product_id", product_id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(user_order_product item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into user_order_product values(@user_order_id,@product_id, @product_name,@order_product_amount)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@user_order_id", item.user_order_id);
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_name", item.product_name);
            cmd.Parameters.Add("@order_product_amount", item.order_product_amount);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public List<user_order_product> getUserOrderProduct(int user_order_id)
        {
            string sql;
            sql = "Select * from user_order_product where user_order_id = " + user_order_id;
            List<user_order_product> list = new List<user_order_product>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order_product item = new user_order_product();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_name = dr["product_name"].ToString();
                item.order_product_amount = double.Parse(dr["order_product_amount"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }


    }
}