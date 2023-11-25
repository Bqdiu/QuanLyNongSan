using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class user_account
    {
        public user_account() { }

        [Key]
        [Display(Name = "ID")]

        public int user_account_id { get; set; }

        [Display(Name = "Tên tài khoản")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string user_username { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập vào mật khẩu")]
        public string user_password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("user_password", ErrorMessage = "Mật khẩu không trùng khớp")]
        public string RetypePassword { get; set; }



        [Display(Name = "Giới tính")]
        [StringLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập vào giới tính")]
        [RegularExpression("^(Nam|Nữ|Giới tính thứ 3|nam|nữ|giới tính thứ 3)$", ErrorMessage = "Giới tính không hợp lệ (Các giới tính hợp lệ như: Nam/Nữ/Giới tính thứ 3)")]
        public string user_gender { get; set; }

        [Display(Name = "Email")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập vào email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string user_email { get; set; }

        [Display(Name = "SĐT")]
        [StringLength(20)]
        [Required(ErrorMessage = "Vui lòng nhập SĐT")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string user_phonenumber { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string user_address { get; set; }
        [Display(Name = "Tên")]
        [StringLength(255)]
        [Required(ErrorMessage = "Không được để trống ô này")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Dữ liệu không được chứa kí tự số.")]
        public string user_firstname { get; set; }
        [Display(Name = "Họ")]
        [StringLength(255)]
        [Required(ErrorMessage = "Không được để trống ô này")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Dữ liệu không được chứa kí tự số.")]
        public string user_lastname { get; set; }

        [Display(Name = "Hạng")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Dữ liệu không được chứa kí tự số")]
        [StringLength(50)]
        public string user_member_tier { get; set; }
        [Display(Name = "Điểm")]
        [Range(0, int.MaxValue, ErrorMessage = "Dữ liệu không được là số âm")]
        public int? user_point { get; set; }
    }
    public partial class ListUserAccount
    {
        DBConnection db;
        public ListUserAccount()
        {
            db = new DBConnection();

        }
        public List<user_account> getData()
        {
            string sql;

            sql = "Select * from user_account";


            List<user_account> list = new List<user_account>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_account item = new user_account();
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.user_username = dr["user_username"].ToString();
                item.user_password = dr["user_password"].ToString();
                item.user_gender = dr["user_gender"].ToString();
                item.user_email = dr["user_email"].ToString();
                item.user_phonenumber = dr["user_phonenumber"].ToString();
                item.user_address = dr["user_address"].ToString();
                item.user_firstname = dr["user_firstname"].ToString();
                item.user_lastname = dr["user_lastname"].ToString();
                item.user_member_tier = dr["user_member_tier"].ToString();
                item.user_point = int.Parse(dr["user_point"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public int CreateUserAccount(user_account a)
        {
            int kt1, kt2;
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from user_account where user_username = @user_username", con);
            SqlCommand cmd2 = new SqlCommand("select count(*) from admin_account where admin_username = @user_username", con);
            cmd.Parameters.AddWithValue("@user_username", a.user_username);
            cmd2.Parameters.AddWithValue("@user_username", a.user_username);
            kt1 = (int)cmd.ExecuteScalar();
            kt2 = (int)cmd2.ExecuteScalar();
            if (kt1 == 0 && kt2 == 0)
            {
                string insert = "INSERT INTO user_account (user_username, user_password, user_gender, user_email, user_phonenumber,user_address, user_firstname, user_lastname) VALUES (@user_username, @user_password, @user_gender, @user_email, @user_phonenumber,@user_address, @user_firstname, @user_lastname)";
                SqlCommand cmd1 = new SqlCommand(insert, con);
                cmd1.Parameters.Clear();

                cmd1.Parameters.Add("@user_username", a.user_username);
                cmd1.Parameters.Add("@user_password", a.user_password);
                cmd1.Parameters.Add("@user_gender", a.user_gender);
                cmd1.Parameters.Add("@user_email", a.user_email);
                cmd1.Parameters.Add("@user_phonenumber", a.user_phonenumber);
                cmd1.Parameters.Add("@user_address", a.user_address);
                cmd1.Parameters.Add("@user_firstname", a.user_firstname);
                cmd1.Parameters.Add("@user_lastname", a.user_lastname);
                cmd1.CommandType = CommandType.Text;
                kt1 = cmd1.ExecuteNonQuery();
                con.Close();
                return 0;
            }
            else
            {
                con.Close();
                return 1;
            }



        }
        public List<user_account> Details(int id)
        {
            string sql;

            sql = "Select * from user_account where user_account_id = " + id;


            List<user_account> list = new List<user_account>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                user_account item = new user_account();
                item.user_account_id = int.Parse(dr["user_account_id"].ToString());
                item.user_username = dr["user_username"].ToString();
                item.user_password = dr["user_password"].ToString();
                item.user_gender = dr["user_gender"].ToString();
                item.user_email = dr["user_email"].ToString();
                item.user_phonenumber = dr["user_phonenumber"].ToString();
                item.user_address = dr["user_address"].ToString();
                item.user_firstname = dr["user_firstname"].ToString();
                item.user_lastname = dr["user_lastname"].ToString();
                item.user_member_tier = dr["user_member_tier"].ToString();
                item.user_point = int.Parse(dr["user_point"].ToString());
                list.Add(item);
            }
            con.Close();
            return list;
        }
        public void update(user_account item)
        {


            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update user_account set user_username = @user_username,user_password = @user_password,user_gender = @user_gender,user_email = @user_email,user_phonenumber = @user_phonenumber,user_address = @user_address, user_firstname = @user_firstname, user_lastname = @user_lastname where user_account_id = @user_account_id ";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Add("@user_account_id", item.user_account_id);
            cmd.Parameters.Add("@user_username", item.user_username);
            cmd.Parameters.Add("@user_password", item.user_password);
            cmd.Parameters.Add("@user_gender", item.user_gender);
            cmd.Parameters.Add("@user_email", item.user_email);
            cmd.Parameters.Add("@user_phonenumber", item.user_phonenumber);
            cmd.Parameters.Add("@user_address", item.user_address);
            cmd.Parameters.Add("@user_firstname", item.user_firstname);
            cmd.Parameters.Add("@user_lastname", item.user_lastname);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void updatePassword(int id, string password)
        {


            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update user_account set user_password = @NewPassword where user_account_id = @user_account_id ";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Add("@NewPassword", password);
            cmd.Parameters.Add("@user_account_id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from user_account where user_account_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public int TotalGetTotalAccounts()
        {
            SqlConnection con = db.GetConnection();

            try
            {
                con.Open();

                string sql = "SELECT COUNT(*) FROM user_account";
                SqlCommand cmd = new SqlCommand(sql, con);
                int totalAccounts = (int)cmd.ExecuteScalar();

                return totalAccounts;
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