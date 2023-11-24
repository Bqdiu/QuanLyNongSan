using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public partial class product_discount_event
    {
        public product_discount_event() { }
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Không được để trống")]

        public int discount_event_id { get; set; }

        [StringLength(1000)]
        [Display(Name = "Hình ảnh")]
        [Required(ErrorMessage = "Vui lòng thêm hình ảnh")]
        public string discount_event_image { get; set; }

        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Display(Name = "Tên sự kiện")]

        [Required(ErrorMessage = "Tên sự kiện không được thiếu")]
        public string discount_event_name { get; set; }

        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Display(Name = "Nội dung sự kiện")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung sự kiện")]
        public string discount_event_description { get; set; }
        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Vui lòng nhập ngày bắt đầu")]
        [DataType(DataType.Date)]
        public DateTime? discount_event_DateCreated { get; set; }
        [Display(Name = "Mã mặt hàng")]
        [Required(ErrorMessage = "Vui lòng nhập mã mặt hàng được khuyến mãi")]
        [Range(0, int.MaxValue, ErrorMessage = "Số không được âm")]
        public int? product_category_id { get; set; }
    }
    class ListDiscountEvent
    {
        DBConnection db;
        public ListDiscountEvent()
        {
            db = new DBConnection();

        }
        public List<product_discount_event> getData()
        {
            string sql;

            sql = "Select * from product_discount_event";


            List<product_discount_event> list = new List<product_discount_event>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_discount_event item = new product_discount_event();
                item.discount_event_id = int.Parse(dr["discount_event_id"].ToString());
                item.discount_event_image = dr["discount_event_image"].ToString();
                item.discount_event_description = dr["discount_event_description"].ToString();
                item.discount_event_DateCreated = DateTime.Parse(dr["discount_event_DateCreated"].ToString());
                item.discount_event_name = dr["discount_event_name"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<product_discount_event> Details(int id)
        {
            string sql;

            sql = "Select * from product_discount_event where discount_event_id = " + id;


            List<product_discount_event> list = new List<product_discount_event>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product_discount_event item = new product_discount_event();
                item.discount_event_id = int.Parse(dr["discount_event_id"].ToString());
                item.discount_event_image = dr["discount_event_image"].ToString();
                item.discount_event_description = dr["discount_event_description"].ToString();
                item.discount_event_DateCreated = DateTime.Parse(dr["discount_event_DateCreated"].ToString());
                item.discount_event_name = dr["discount_event_name"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;
        }
        public void update(product_discount_event item)
        {


            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update product_discount_event set discount_event_image = @image, discount_event_DateCreated = @DateCreated, discount_event_name = @name, discount_event_description = @description where discount_event_id = @id";
            SqlCommand cmd = new SqlCommand(update, con);

            cmd.Parameters.Add("@name", item.discount_event_name);
            cmd.Parameters.Add("@description", item.discount_event_description);
            cmd.Parameters.Add("@image", item.discount_event_image);
            cmd.Parameters.Add("@id", item.discount_event_id);
            cmd.Parameters.Add("@DateCreated", item.discount_event_DateCreated);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from product_discount_event where discount_event_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(product_discount_event item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into product_discount_event(discount_event_image,discount_event_name,discount_event_description) values(@image,@Name,@description)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@name", item.discount_event_name);
            cmd.Parameters.Add("@image", item.discount_event_image);
            cmd.Parameters.Add("@description", item.discount_event_description);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}