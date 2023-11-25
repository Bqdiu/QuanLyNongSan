using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class product
    {
        public product()
        {

        }
        [Key]

        public int product_id { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập vào mã danh mục")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Chỉ cho phép nhập số")]
        [Range(0, int.MaxValue, ErrorMessage = "Số không được âm")]
        [Display(Name = "Mã danh mục")]
        public int? product_category_id { get; set; }
        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Display(Name = "Tên Sản Phẩm")]

        [Required(ErrorMessage = "Bạn cần nhập vào tên sản phẩm")]
        public string product_name { get; set; }

        [Display(Name = "Miêu tả sản phẩm")]
        [StringLength(10000000, ErrorMessage = "Vui lòng nhập tối đa 10 000 000 ký tự")]
        [Required(ErrorMessage = "Vui lòng miêu tả sơ lược sản phẩm")]
        public string product_description { get; set; }
        [Display(Name = "Hình ảnh sản phẩm")]
        [StringLength(1000)]
        [Required(ErrorMessage = "Vui lòng thêm hình ảnh sản phẩm")]
        public string product_thumbnail_image { get; set; }
        [Required(ErrorMessage = "Bạn cần nhập vào giá của sản phẩm")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0.")]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "Chỉ được nhập kí tự số.")]
        [Display(Name = "Giá")]
        public decimal? product_price { get; set; }
    }
    class ListProduct
    {

        DBConnection db;
        public ListProduct()
        {
            db = new DBConnection();

        }
        public List<product> getData()
        {
            string sql;

            sql = "Select * from product";


            List<product> list = new List<product>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                product item = new product();

                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_price = decimal.Parse(dr["product_price"].ToString());
                item.product_description = dr["product_description"].ToString();
                item.product_thumbnail_image = dr["product_thumbnail_image"].ToString();
                item.product_name = dr["product_name"].ToString();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                list.Add(item);
            }

            con.Close();
            return list;

        }
        public List<product> search(string key)
        {
            List<product> list = new List<product>();

            string sql = "Select * from product where product_name like '%' + @searchKey + '%'";
            SqlConnection con = db.GetConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@searchKey", key);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                product item = new product();
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_price = decimal.Parse(dr["product_price"].ToString());
                item.product_description = dr["product_description"].ToString();
                item.product_thumbnail_image = dr["product_thumbnail_image"].ToString();
                item.product_name = dr["product_name"].ToString();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<product> details(int id, int category_id)
        {
            string sql;

            sql = "Select * from product where product_id = " + id + "and product_category_id = " + category_id;


            List<product> list = new List<product>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                product item = new product();
                item.product_id = int.Parse(dr["product_id"].ToString());
                item.product_price = decimal.Parse(dr["product_price"].ToString());
                item.product_description = dr["product_description"].ToString();
                item.product_thumbnail_image = dr["product_thumbnail_image"].ToString();
                item.product_name = dr["product_name"].ToString();
                item.product_category_id = int.Parse(dr["product_category_id"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;
        }
        public void update(product item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update product set product_category_id = @category_id, product_name = @name, product_description = @description, product_thumbnail_image = @thumbnail, product_price = @price where product_id = @id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Add("@category_id", item.product_category_id);
            cmd.Parameters.Add("@name", item.product_name);
            cmd.Parameters.Add("@description", item.product_description);
            cmd.Parameters.Add("@thumbnail", item.product_thumbnail_image);
            cmd.Parameters.Add("@price", item.product_price);
            cmd.Parameters.Add("@id", item.product_id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from product where product_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(product item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into product values(@category_id,@name,@description,@thumbnail,@price)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@category_id", item.product_category_id);
            cmd.Parameters.Add("@name", item.product_name);
            cmd.Parameters.Add("@description", item.product_description);
            cmd.Parameters.Add("@thumbnail", item.product_thumbnail_image);
            cmd.Parameters.Add("@price", item.product_price);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public int TotalProducts()
        {
            SqlConnection con = db.GetConnection();

            try
            {
                con.Open();

                string sql = "SELECT COUNT(*) FROM product";
                SqlCommand cmd = new SqlCommand(sql, con);

                // Thực hiện truy vấn và trả về giá trị
                int totalProducts = (int)cmd.ExecuteScalar();

                return totalProducts;
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu có
                Console.WriteLine(ex.Message);
                return -1; // Hoặc trả về một giá trị đặc biệt để biểu thị lỗi
            }
            finally
            {
                con.Close(); // Đảm bảo rằng kết nối được đóng ngay cả khi có lỗi xảy ra
            }
        }
    }
}