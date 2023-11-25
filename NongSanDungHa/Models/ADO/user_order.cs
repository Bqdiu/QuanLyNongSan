using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class user_order
    {
        public user_order()
        {

        }
        [Key]
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ.")]
        [Display(Name = "ID Đơn hàng")]
        public int user_order_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]

        [Range(1, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ.")]
        [Display(Name = "ID khách hàng")]
        public int? user_account_id { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Column(TypeName = "date")]
        [Display(Name = "Thời gian đặt hàng")]
        [DataType(DataType.Date)]
        public DateTime? order_time { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [RegularExpression(@"^[^0-9]+$", ErrorMessage = "Không được chứa kí tự số.")]
        [StringLength(255)]
        [Display(Name = "Họ Tên người nhận")]
        public string user_order_buyer_name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        [Display(Name = "Địa chỉ nhận hàng")]
        public string user_order_address { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string user_order_email { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(20)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Display(Name = "SĐT")]
        public string user_order_phonenumber { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(20)]
        [Display(Name = "Tình trạng đơn hàng")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Không được chưa kí tự số")]
        public string is_processed { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(20)]
        [Display(Name = "Trình trạng vận chuyển")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Không được chưa kí tự số")]
        public string is_delivered { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Tổng Tiền")]
        [Range(0, int.MaxValue, ErrorMessage = "Tổng tiền không hợp lệ")]
        public decimal? order_total_value { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Phương thức thanh toán")]
        [StringLength(20)]
        [RegularExpression("@^[^0-9]*$", ErrorMessage = "Không được chứa kí tự số")]
        public string payments { get; set; }
    }
    public partial class ListUser_Order
    {
        DBConnection db;
        public ListUser_Order()
        {
            db = new DBConnection();

        }
        public List<user_order> getData()
        {
            string sql;

            sql = "Select * from user_order";


            List<user_order> list = new List<user_order>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order item = new user_order();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.order_time = DateTime.Parse(dr["order_time"].ToString());
                item.user_order_buyer_name = dr["user_order_buyer_name"].ToString();
                item.user_order_address = dr["user_order_address"].ToString();
                item.user_order_email = dr["user_order_email"].ToString();
                item.user_order_phonenumber = dr["user_order_phonenumber"].ToString();
                item.is_processed = dr["is_processed"].ToString();
                item.is_delivered = dr["is_delivered"].ToString();
                item.payments = dr["payments"].ToString();
                item.order_total_value = decimal.Parse(dr["order_total_value"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public List<user_order> Details(int id)
        {
            string sql;

            sql = "Select * from user_order where user_order_id = " + id;


            List<user_order> list = new List<user_order>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order item = new user_order();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.order_time = DateTime.Parse(dr["order_time"].ToString());
                item.user_order_buyer_name = dr["user_order_buyer_name"].ToString();
                item.user_order_address = dr["user_order_address"].ToString();
                item.user_order_email = dr["user_order_email"].ToString();
                item.user_order_phonenumber = dr["user_order_phonenumber"].ToString();
                item.is_processed = dr["is_processed"].ToString();
                item.is_delivered = dr["is_delivered"].ToString();
                item.payments = dr["payments"].ToString();
                item.order_total_value = decimal.Parse(dr["order_total_value"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;
        }

        public void update(user_order item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update user_order set user_account_id = @user_account_id,order_time =@order_time, user_order_buyer_name = @user_order_buyer_name, user_order_address =@user_order_address,user_order_email=@user_order_email,user_order_phonenumber=@user_order_phonenumber,is_processed=@is_processed,is_delivered=@is_delivered, order_total_value=@order_total_value, payments=@payments where user_order_id = @user_order_id";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@user_order_id", item.user_order_id);
            cmd.Parameters.Add("@user_account_id", item.user_account_id);
            cmd.Parameters.Add("@order_time", item.order_time);
            cmd.Parameters.Add("@user_order_buyer_name", item.user_order_buyer_name);
            cmd.Parameters.Add("@user_order_address", item.user_order_address);
            cmd.Parameters.Add("@user_order_email", item.user_order_email);
            cmd.Parameters.Add("@user_order_phonenumber", item.user_order_phonenumber);
            cmd.Parameters.Add("@is_processed", item.is_processed);
            cmd.Parameters.Add("@is_delivered", item.is_delivered);
            cmd.Parameters.Add("@order_total_value", item.order_total_value);
            cmd.Parameters.Add("payments", item.payments);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from user_order where user_order_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void insert(user_order item)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Insert into user_order values(@user_account_id,@order_time, @user_order_buyer_name, @user_order_address,@user_order_email,@user_order_phonenumber,@is_processed,@is_delivered, @order_total_value)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@user_account_id", item.user_account_id);
            cmd.Parameters.Add("@order_time", item.order_time);
            cmd.Parameters.Add("@user_order_buyer_name", item.user_order_buyer_name);
            cmd.Parameters.Add("@user_order_address", item.user_order_address);
            cmd.Parameters.Add("@user_order_email", item.user_order_email);
            cmd.Parameters.Add("@user_order_phonenumber", item.user_order_phonenumber);
            cmd.Parameters.Add("@is_processed", item.is_processed);
            cmd.Parameters.Add("@is_delivered", item.is_delivered);
            cmd.Parameters.Add("@order_total_value", item.order_total_value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public int GetTotalOrders()
        {
            SqlConnection con = db.GetConnection();

            try
            {
                con.Open();

                string sql = "SELECT COUNT(*) FROM user_order";
                SqlCommand cmd = new SqlCommand(sql, con);
                int totalOrders = (int)cmd.ExecuteScalar();

                return totalOrders;
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
        public List<user_order> getUserOrder(int user_account_id)
        {
            string sql;
            sql = "Select * from user_order where user_account_id = " + user_account_id;
            List<user_order> list = new List<user_order>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_order item = new user_order();
                item.user_order_id = int.Parse(dr["user_order_id"].ToString());
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.order_time = DateTime.Parse(dr["order_time"].ToString());
                item.user_order_buyer_name = dr["user_order_buyer_name"].ToString();
                item.user_order_address = dr["user_order_address"].ToString();
                item.user_order_email = dr["user_order_email"].ToString();
                item.user_order_phonenumber = dr["user_order_phonenumber"].ToString();
                item.is_processed = dr["is_processed"].ToString();
                item.is_delivered = dr["is_delivered"].ToString();
                item.order_total_value = decimal.Parse(dr["order_total_value"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public decimal GetMonthlyRevenue()
        {
            SqlConnection con = db.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCurrentMonthRevenue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal monthlyRevenue = (decimal)cmd.ExecuteScalar();
                monthlyRevenue = Math.Round(monthlyRevenue, 0);
                return monthlyRevenue;
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
        public decimal GetYearlyRevenue()
        {
            SqlConnection con = db.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCurrentYearlyRevenue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                decimal yearyRevenue = (decimal)cmd.ExecuteScalar();
                yearyRevenue = Math.Round(yearyRevenue, 0);
                return yearyRevenue;
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