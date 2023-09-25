using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class UserLogin : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE " +
                    "member_id = '"+ tbUserID.Text.Trim() +"' AND password = '"+ tbPassword.Text.Trim() +"'",
                    con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Response.Write("<script>alert('Login Successful !');</script>");
                        Session["username"] = dr.GetValue(8).ToString();
                        Session["fullname"] = dr.GetValue(0).ToString();
                        Session["role"] = "User";
                        Session["status"] = dr.GetValue(10).ToString();
                    }
                    Response.Redirect("HomePage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Wrong UserID or Password, Please try again !');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            

            //Response.Write("<script>alert('Button Click');</script>");
        }
    }
}