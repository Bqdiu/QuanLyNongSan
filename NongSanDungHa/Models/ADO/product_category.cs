using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class product_category
    {
        [Key]
        [Display(Name = "Mã loại hàng")]
        [Required(ErrorMessage = "Không được để trống")]
        public int product_category_id { get; set; }

        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Display(Name = "Tên loại hàng")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Dữ liệu không hợp lệ")]
        [Required(ErrorMessage = "Vui lòng tên loại hàng")]

        public string product_category_name { get; set; }
        [Display(Name = "Mô tả")]
        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng mô tả loại hàng")]
        public string product_category_description { get; set; }
    }
    class ListProductCategory
    {
        DBConnection db;
        public ListProductCategory()
        {
            db = new DBConnection();

        }
        public List<product_category> getData()
        {
            string sql;

            sql = "Select * from product_category";


            List<product_category> list = new List<product_category>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_category item = new product_category();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                item.product_category_name = dr["product_category_name"].ToString();
                item.product_category_description = dr["product_category_description"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;

        }

        public List<product_category> Details(int category_id)
        {
            string sql;

            sql = "Select * from product_category where product_category_id = " + category_id;


            List<product_category> list = new List<product_category>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_category item = new product_category();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                item.product_category_name = dr["product_category_name"].ToString();
                item.product_category_description = dr["product_category_description"].ToString();
                list.Add(item);
            }

            con.Close();
            return list;
        }
        public void update(product_category item)
        {


            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update product_category set product_category_name = @name, product_category_description = @description where product_category_id = @id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Add("@id", item.product_category_id);
            cmd.Parameters.Add("@name", item.product_category_name);
            cmd.Parameters.Add("@description", item.product_category_description);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from product_category where product_category_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(product_category item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into product_category values(@name,@description)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@name", item.product_category_name);
            cmd.Parameters.Add("@description", item.product_category_description);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public List<product_category> search(string key)
        {
            List<product_category> list = new List<product_category>();

            string sql = "Select * from product_category where product_category_name like '%' + @searchKey + '%'";
            SqlConnection con = db.GetConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@searchKey", key);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                product_category item = new product_category();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                item.product_category_name = dr["product_category_name"].ToString();
                item.product_category_description = dr["product_category_description"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public int GetTotalCategories()
        {
            SqlConnection con = db.GetConnection();

            try
            {
                con.Open();
                string sql = "SELECT COUNT(*) FROM product_category";
                SqlCommand cmd = new SqlCommand(sql, con);
                int totalCategories = (int)cmd.ExecuteScalar();

                return totalCategories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
    }

}