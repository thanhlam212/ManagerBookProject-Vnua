using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class UserSignUp : System.Web.UI.Page
    {

        string conStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        //Sign Up button click event
        protected void Button1_Click(object sender, EventArgs e)
        {
                if (checkEmailExits())
                {
                    Response.Write("<script>alert('Member Already Exits with this Member ID, Try Orther ID');</script>");
                }
                else
                {
                    SignUpMember();
                }
        }

        protected bool checkEmailExits()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from member_master_tbl WHERE member_id = '" + tbPassword.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            return false;
        }

        protected void SignUpMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl" +
                    "(full_name, dob, contact_no, email, state, city, pincode, full_address, member_id, password, account_status)" +
                    " values(@full_name, @dob, @contact_no, @email, @state, @city, @pincode, @full_address, @member_id, @password, @account_status)", con);

                cmd.Parameters.AddWithValue("@full_name", tbFName.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", tbDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", tbContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@email", tbEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@state", ddListState.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", tbCity.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", tbPincode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", tbFAdress.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", tbUserID.Text.Trim());
                cmd.Parameters.AddWithValue("@password", tbPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Sign Up Successfully ! Go to User Login to Login');</script>");
                clearField();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void clearField()
        {
            tbFName.Text = "";
            tbDOB.Text = "";
            tbCity.Text = "";
            tbContactNo.Text = "";
            tbEmail.Text = "";
            tbFAdress.Text = "";
            tbUserID.Text = "";
            tbPassword.Text = "";
            tbPincode.Text = "";
            ddListState.SelectedItem.Text = "";
        }
    }
}