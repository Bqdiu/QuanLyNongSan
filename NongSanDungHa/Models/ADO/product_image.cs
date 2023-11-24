using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class product_image
    {
        public product_image() { }
        [Key]

        public int product_image_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số không được âm")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "Chỉ được nhập số")]
        [Display(Name = "ID sản phẩm")]
        public int? product_id { get; set; }

        [StringLength(1000)]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Hình ảnh")]
        public string product_image_file_name { get; set; }
    }
    public partial class ListProductImage
    {
        DBConnection db;
        public ListProductImage()
        {
            db = new DBConnection();

        }
        public List<product_image> getData()
        {
            string sql;

            sql = "Select * from product_image";


            List<product_image> list = new List<product_image>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_image item = new product_image();
                item.product_image_id = int.Parse(dr["product_image_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_image_file_name = dr["product_image_file_name"].ToString();

                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<product_image> getImageProduct(int id_product)
        {
            string sql;

            sql = "Select * from product_image where product_id = " + id_product;


            List<product_image> list = new List<product_image>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_image item = new product_image();
                item.product_image_id = int.Parse(dr["product_image_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_image_file_name = dr["product_image_file_name"].ToString();

                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<product_image> Details(int id)
        {
            string sql;

            sql = "Select * from product_image where product_image_id = " + id;


            List<product_image> list = new List<product_image>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_image item = new product_image();
                item.product_image_id = int.Parse(dr["product_image_id"].ToString());
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_image_file_name = dr["product_image_file_name"].ToString();

                list.Add(item);
            }
            con.Close();
            return list;
        }

        public void update(product_image item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update product_image set product_id = @product_id, product_image_file_name = @product_image_file_name where product_image_id = @id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", item.product_image_id);
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_image_file_name", item.product_image_file_name);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from product_image where product_image_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(product_image item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into product_image values(@product_id,@product_image_file_name)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@product_id", item.product_id);
            cmd.Parameters.Add("@product_image_file_name", item.product_image_file_name);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}