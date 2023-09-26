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
    public partial class AdminPublisherManagement : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        //Add publisher button click
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Add publisher button click
        protected void btnAddPublisher_Click(object sender, EventArgs e)
        {
            if (checkPublisherExist())
            {
                Response.Write("<script>alert('Publisher with this ID is already Exits. You cannot add" +
                   " another Publisher with the same Publisher ID.');</script>");
            }
            else
            {
                addNewPublisher();  
            }
        }

        //Update publisher button click
        protected void btnUpdatePublisher_Click(object sender, EventArgs e)
        {
            if (checkPublisherExist())
            {
                updatePublisher();
            }
            else
            {
                Response.Write("<script>alert('Publisher does not exist.');</script>");
            }
        }

        //Delete publisher button click
        protected void btnDeletePublisher_Click(object sender, EventArgs e)
        {
            if (checkPublisherExist())
            {
                deletePublisher();
            }
            else
            {
                Response.Write("<script>alert('Publisher does not exist.');</script>");
            }
        }

        //Go publisher button click
        protected void btnGo_Click(object sender, EventArgs e)
        {
            getPublisherNameByID();
        }

        //user defiend function

        protected void getPublisherNameByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbL WHERE publisher_id = '" + tbPublisherID.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    tbPublisherName.Text = dt.Rows[0][1].ToString();
                    PublisherDataTable.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Publisher ID.');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void deletePublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM publisher_master_tbl WHERE publisher_id = '" + tbPublisherID.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('Publisher Deleted Successfully.');</script>");
                clearField();
                PublisherDataTable.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updatePublisher()
        {
            try
            {
            SqlConnection con = new SqlConnection(connectionString);

            if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

            SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET " +
                "publisher_name = @publisher_name WHERE publisher_id = '" + tbPublisherID.Text.Trim() + "'", con);

            cmd.Parameters.AddWithValue("@publisher_name", tbPublisherName.Text.Trim());

            cmd.ExecuteNonQuery();

            con.Close();
            Response.Write("<script>alert('Publisher Updated Successfully.');</script>");
            clearField();
            PublisherDataTable.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
}

        protected void addNewPublisher()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id, publisher_name)" +
                    " VALUES (@publisher_id, @publisher_name)", con);

                cmd.Parameters.AddWithValue("@publisher_id", tbPublisherID.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name", tbPublisherName.Text.Trim());

                cmd.ExecuteNonQuery();

                con.Close();
                Response.Write("<script>alert('Publisher Added Successfully.');</script>");
                clearField();
                PublisherDataTable.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected bool checkPublisherExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl WHERE publisher_id = '" + tbPublisherID.Text.Trim() + "'", con);
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

        protected void clearField()
        {
            tbPublisherID.Text = "";
            tbPublisherName.Text = "";
        }

    }
}