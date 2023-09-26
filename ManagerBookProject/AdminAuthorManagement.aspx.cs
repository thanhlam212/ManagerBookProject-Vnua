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
    public partial class AdminAuthor : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Add author button click 
        protected void AddAuthorBtn_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExits())
            {
                Response.Write("<script>alert('Author with this ID is already Exits. You cannot add" +
                    " another Author with the same Author ID.');</script>");
            }
            else
            {
                addNewAuthor();
            }
        }

        //Update author button click
        protected void UpdateAuthorBtn_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExits())
            {
                updateAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author does not exist.');</script>");
            }
        }

        //Delete author button click
        protected void DeleteAuthorBtn_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExits())
            {
                deleteAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author does not exist.');</script>");
            }
        }

        //Go button click
        protected void GoBtn_Click(object sender, EventArgs e)
        {
            AuthorDataTable.DataBind();
        }

        //user defiend function

        protected void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM author_master_tbl WHERE author_id = '" + tbAuthorId.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Delete Successfully.');</script>");
                clearForm();
                AuthorDataTable.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET " +
                    "author_name = @author_name WHERE author_id = '"+ tbAuthorId.Text.Trim() +"'", con);

               
                cmd.Parameters.AddWithValue("@author_name", tbAuthorName.Text.Trim());

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author Updated Successfully.');</script>");
                clearForm();
                AuthorDataTable.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }   

                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id, author_name) " +
                    "VALUES (@author_id, @author_name)", con);

                cmd.Parameters.AddWithValue("@author_id", tbAuthorId.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", tbAuthorName.Text.Trim());

                cmd.ExecuteNonQuery();
                
                con.Close();
                Response.Write("<script>alert('Author Added Successfully.');</script>");
                clearForm();
                AuthorDataTable.DataBind(); 
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected bool checkIfAuthorExits()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from author_master_tbl WHERE author_id = '" + tbAuthorId.Text.Trim() + "';", con);
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

        protected void clearForm()
        {
            tbAuthorId.Text = " ";
            tbAuthorName.Text = " ";
        }
    }
}