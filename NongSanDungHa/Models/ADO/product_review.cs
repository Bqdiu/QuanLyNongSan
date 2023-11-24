using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class product_review
    {
        [Required(ErrorMessage = "Không được để trống")]

        [Range(1, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ.")]
        public int product_review_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]

        [Range(1, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ.")]
        [Display(Name = "ID người dùng")]
        public int? user_account_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ.")]
        [Display(Name = "ID sản phẩm")]
        public int? product_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        [Display(Name = "Nội dung")]
        public string product_review_content { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        [Display(Name = "Người đánh giá")]
        public string review_owner { get; set; }
    }
    public partial class ListProduct_review
    {
        DBConnection db;
        public ListProduct_review()
        {
            db = new DBConnection();

        }
        public List<product_review> getData()
        {
            string sql;

            sql = "Select * from product_review";


            List<product_review> list = new List<product_review>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_review item = new product_review();
                item.product_review_id = int.Parse(dr["product_review_id"].ToString());
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_review_content = dr["product_review_content"].ToString();
                item.review_owner = dr["review_owner"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<product_review> Details(int id)
        {
            string sql;

            sql = "Select * from product_review where product_review_id = " + id;


            List<product_review> list = new List<product_review>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_review item = new product_review();
                item.product_review_id = int.Parse(dr["product_review_id"].ToString());
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_review_content = dr["product_review_content"].ToString();
                item.review_owner = dr["review_owner"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;
        }

        public void update(product_review item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update product_review set user_account_id = @user_account_id,product_id =@product_id, product_review_content = @product_review_content, review_owner =@review_owner  where product_review_id = @product_review_id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@product_review_id", item.product_review_id);
            cmd.Parameters.Add("@user_account_id", item.user_account_id);
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_review_content", item.product_review_content);
            cmd.Parameters.Add("@review_owner", item.review_owner);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from product_review where product_review_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(product_review item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into product_review values(@user_account_id,@product_id,@product_review_content,@review_owner)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@user_account_id", item.user_account_id);
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_review_content", item.product_review_content);
            cmd.Parameters.Add("@review_owner", item.review_owner);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}