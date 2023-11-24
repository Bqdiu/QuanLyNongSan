using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NongSanDungHa.Models.ADO
{
    public class admin_account
    {
        [Key]
        [Display(Name = "ID")]
        public int admin_account_id { get; set; }

        [Display(Name = "Tên Tài khoản")]
        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string admin_username { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(255, ErrorMessage = "Vui lòng nhập tối đa 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string admin_password { get; set; }
    }
    class ListAdminAccount
    {
        DBConnection db;
        public ListAdminAccount()
        {
            db = new DBConnection();

        }
        public List< admin_account> getData()
        {
            string sql;

            sql = "Select * from admin_account";


            List< admin_account> list = new List< admin_account>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                 admin_account item = new  admin_account();
                item.admin_account_id = int.Parse(dr["admin_account_id"].ToString());
                item.admin_username = dr["admin_username"].ToString();
                item.admin_password = dr["admin_password"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;

        }
        public int CreateAdminAccount( admin_account a)
        {
            int kt1, kt2;
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from admin_account where admin_username = @admin_username", con);
            SqlCommand cmd2 = new SqlCommand("select count(*) from user_account where user_username = @admin_username", con);
            cmd.Parameters.AddWithValue("@admin_username", a.admin_username);
            cmd2.Parameters.AddWithValue("@admin_username", a.admin_username);
            kt1 = (int)cmd.ExecuteScalar();
            kt2 = (int)cmd2.ExecuteScalar();
            if (kt1 == 0 && kt2 == 0)
            {
                string insert = "INSERT INTO admin_account (admin_username, admin_password) VALUES (@admin_username, @admin_password)";
                SqlCommand cmd1 = new SqlCommand(insert, con);
                cmd1.Parameters.Clear();

                cmd1.Parameters.Add("@admin_username", a.admin_username);
                cmd1.Parameters.Add("@admin_password", a.admin_password);

                cmd1.CommandType = CommandType.Text;
                kt1 = cmd1.ExecuteNonQuery();
                con.Close();
                return kt1;
            }
            else
            {
                con.Close();
                return -1;
            }



        }
        public List< admin_account> Details(int id)
        {
            string sql;

            sql = "Select * from admin_account where admin_account_id = " + id;


            List< admin_account> list = new List< admin_account>();
            SqlConnection con = db.GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                 admin_account item = new  admin_account();
                item.admin_account_id = int.Parse(dr["admin_account_id"].ToString());
                item.admin_username = dr["admin_username"].ToString();
                item.admin_password = dr["admin_password"].ToString();
                list.Add(item);
            }
            con.Close();
            return list;
        }
        public void update( admin_account item)
        {


            SqlConnection con = db.GetConnection();
            con.Open();
            string update = "Update admin_account set admin_username = @admin_username,admin_password = @admin_password where admin_account_id = @admin_account_id ";
            SqlCommand cmd = new SqlCommand(update, con);
            cmd.Parameters.Add("@admin_account_id", item.admin_account_id);
            cmd.Parameters.Add("@admin_username", item.admin_username);
            cmd.Parameters.Add("@admin_password", item.admin_password);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }
        public void delete(int id)
        {
            SqlConnection con = db.GetConnection();
            con.Open();
            string sql = "Delete from admin_account where admin_account_id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

    }
}