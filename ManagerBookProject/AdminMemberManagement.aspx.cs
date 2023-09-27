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
    public partial class AdminMemberManagement : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            MemberDataTable.DataBind();
        }

        //Go button click
        protected void GoIDMemberBtn_Click(object sender, EventArgs e)
        {
            if (checkMemberIDExist())
            {
                getMemberByID();
            }
            else
            {
                Response.Write("<script>alert('Member ID not exist.');</script>");
            }
        }

        //Active Button click
        protected void btnSuccess_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusByID("Active");
        }

        //Warning Button click
        protected void btnWarning_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusByID("Pending");
        }

        //Dissable Button click
        protected void btnDissable_Click(object sender, EventArgs e)
        {
            UpdateMemberStatusByID("Dissable");
        }

        //Delete Button click
        protected void deleteMemberBtn_Click(object sender, EventArgs e)
        {
            deleteMemberById();
        }

        //User denfined function

        protected bool checkMemberIDExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id = '" + tbMemberID.Text.Trim() + "'", con);
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


        protected void getMemberByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id = '" + tbMemberID.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while (dr.Read())
                    {
                        tbMemberName.Text = dr.GetValue(0).ToString();
                        tbAccountStatus.Text = dr.GetValue(10).ToString();
                        tbMemberDOB.Text = dr.GetValue(1).ToString();
                        tbMemberContactNo.Text = dr.GetValue(2).ToString();
                        tbMemberEmail.Text = dr.GetValue(3).ToString();
                        tbMemberState.Text = dr.GetValue(4).ToString();
                        tbMemberCity.Text = dr.GetValue(5).ToString();
                        tbMemberPinCode.Text = dr.GetValue(6).ToString();
                        tbMemberFullAdress.Text = dr.GetValue(7).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void UpdateMemberStatusByID(string status)
        {
            if (checkMemberIDExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status = '" + status + "' " +
                        "WHERE member_id = '" + tbMemberID.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    MemberDataTable.DataBind();
                    Response.Write("<script>alert('Member Update Successfully.');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID.');</script>");
            }
        }

        protected void deleteMemberById()
        {
            if (checkMemberIDExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM member_master_tbl WHERE member_id = '" + tbMemberID.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    MemberDataTable.DataBind();

                    Response.Write("<script>alert('Member Delete Successfully.');</script>");
                    clearField();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
               
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID.');</script>");
            }
        }

       
        protected void clearField()
        {
            tbMemberID.Text = "";
            tbMemberName.Text = "";
            tbMemberDOB.Text = "";
            tbMemberContactNo.Text = "";
            tbAccountStatus.Text = "";
            tbMemberEmail.Text = "";
            tbMemberFullAdress.Text = "";
            tbMemberCity.Text = "";
            tbMemberPinCode.Text = "";
            tbAccountStatus.Text = "";
        }

    }
}