using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired Login Again');</script>");
                    Response.Redirect("UserLogin.aspx");
                }
                else
                {
                    getUserBookData();
                    if(!Page.IsPostBack)
                    {
                        getUserPersonalDetail();
                    }
                }
            }
            catch(Exception ex)
            {
               
            }
        }

        //Update button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["username"].ToString() == "" || Session["username"] == null)
            {
                Response.Write("<script>alert('Session Expired Login Again');</script>");
                Response.Redirect("UserLogin.aspx");
            }
            else
            {
                UpdateUserPersonalDetail();
            }
        }

        //USer Denfined Function

        protected void UpdateUserPersonalDetail()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = tbUserPassword.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET" +
                    " full_name=@full_name, dob=@dob, contact_no=@contact_no," +
                    " email=@email, state=@state, city=@city, pincode=@pincode," +
                    " full_address=@full_address, password=@password, account_status=@account_status" +
                    " WHERE member_id='" + Session["username"].ToString().Trim() + "'", con);

                cmd.Parameters.AddWithValue("@full_name", tbUserFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", tbUserDOB.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", tbUserContactNo.Text.Trim());
                cmd.Parameters.AddWithValue("@email", tbUserEmailID.Text.Trim());
                cmd.Parameters.AddWithValue("@state", ddlUserState.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", tbUserCity.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", tbUserPinCode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", tbUserFullAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@account_status", "pending");

                int result = cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {

                    Response.Write("<script>alert('Your Details Updated Successfully');</script>");
                    getUserPersonalDetail();
                    getUserBookData();
                }
                else
                {
                    Response.Write("<script>alert('Invaid entry');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void getUserPersonalDetail()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id = '" + Session["username"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                tbUserFullName.Text = dt.Rows[0]["full_name"].ToString();
                tbUserDOB.Text = dt.Rows[0]["dob"].ToString();
                tbUserContactNo.Text = dt.Rows[0]["contact_no"].ToString();
                tbUserEmailID.Text = dt.Rows[0]["email"].ToString();
                ddlUserState.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                tbUserCity.Text = dt.Rows[0]["city"].ToString();
                tbUserPinCode.Text = dt.Rows[0]["pincode"].ToString();
                tbUserFullAddress.Text = dt.Rows[0]["full_address"].ToString();
                tbUserID.Text = dt.Rows[0]["member_id"].ToString();
                tbUserPassword.Text = dt.Rows[0]["password"].ToString().Trim();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();

                if (dt.Rows[0]["account_status"].ToString().Trim() == "Active")
                {
                    Label1.Attributes.Add("class", "badge rounded-pill text-bg-success");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge rounded-pill text-bg-warning");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "Dissable")
                {
                    Label1.Attributes.Add("class", "badge rounded-pill text-bg-danger");
                }
                else
                {
                    Label1.Attributes.Add("class", "badge badge-pill badge-info");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        
        protected void getUserBookData()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE member_id = '" + Session["username"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                UserBookIssuedDataTable.DataSource = dt;
                UserBookIssuedDataTable.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void UserBookIssuedDataTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Check your condition here
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today > dt)
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[1].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[2].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[3].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[4].BackColor = System.Drawing.Color.PaleVioletRed;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                    else
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.PaleGreen;
                        e.Row.Cells[1].BackColor = System.Drawing.Color.PaleGreen;
                        e.Row.Cells[2].BackColor = System.Drawing.Color.PaleGreen;
                        e.Row.Cells[3].BackColor = System.Drawing.Color.PaleGreen;
                        e.Row.Cells[4].BackColor = System.Drawing.Color.PaleGreen;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.PaleGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}