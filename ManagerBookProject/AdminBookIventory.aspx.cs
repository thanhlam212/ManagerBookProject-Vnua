using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class AdminBookIventory : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            fillAuthorValues();
            fillPublisherValues();
            BookInventoryDataTable.DataBind();
        }

        protected void GoBtnClick(object sender, EventArgs e)
        {
            
        }

        //Add button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfBookExist())
            {
                Response.Write("<script>alert('Book Already exits, try some another Book ID.');</script>");
            }
            else
            {
                addNewBook();
            }
        }

        //Update Button click
        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        //Delete button Click
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        //User defined functions
        protected void fillAuthorValues()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT author_name FROM author_master_tbl", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                ddlAuthorName.DataSource = dt;
                ddlAuthorName.DataValueField = "author_name";
                ddlAuthorName.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void fillPublisherValues()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT publisher_name FROM publisher_master_tbl", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                ddlPublisherName.DataSource = dt;
                ddlPublisherName.DataValueField = "publisher_name";
                ddlPublisherName.DataBind();
            }
            catch(Exception ex) 
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            } 
        }

        protected bool checkIfBookExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl" +
                    " WHERE book_id = '" + tbBookID.Text.Trim() + "' OR book_name = '"+ tbBookName.Text.Trim() +"'", con);
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
                return false;
            }
        }

        protected void addNewBook()
        {
            try
            {
                string genres = "";
                foreach (int i in GenresBook.GetSelectedIndices())
                {
                    genres = genres + GenresBook.Items[i] + ",";
                }
                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/Image/books1";
                string filename = Path.GetFileName(UploadImageBook.PostedFile.FileName);
                UploadImageBook.SaveAs(Server.MapPath("Book_Inventory/" + filename));
                filepath = "~/Book_Inventory/" + filename;

                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl" +
                    "(boo_id, book_name, genre, author_name, publisher_name, publish_date, language," +
                    "edition, book_cost, no_of_pages, book_description, actual_stock, curent_stock, book_img_link)" +
                    "VALUES " +
                    "(@book_id, @book_name, @genre, @author_name, @publisher_name, @publisher_date," +
                    "@language, @edition, @book_cost, @no_of_pages, @book_description, @actual_stock, @current_stock," +
                    "@book_img_link)", con);

                cmd.Parameters.AddWithValue("@book_id", tbBookID.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", tbBookName.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);

                cmd.Parameters.AddWithValue("@author_name", ddlAuthorName.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", ddlPublisherName.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_date", tbBookPublisherDate.Text.Trim());
                cmd.Parameters.AddWithValue("@language", ddlBookLanguage.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", tbBookEdition.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", tbBookCost.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", tbBookNoPages.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", tbBookDerscription.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", tbBookActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", tbBookActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("book_img_link", filepath);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Book Added Successfully !');</script>");
                BookInventoryDataTable.DataBind();
                ClearField();
            }
            catch (Exception ex )
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void ClearField()
        {
            tbBookID.Text = "";
            tbBookName.Text = "";
            ddlAuthorName.SelectedItem.Value = "";
            ddlPublisherName.SelectedItem.Value = "";
            tbBookPublisherDate.Text = "";
            ddlBookLanguage.SelectedItem.Value = "";
            tbBookEdition.Text = "";
            tbBookCost.Text = "";
            tbBookNoPages.Text = "";
            tbBookDerscription.Text = "";
            tbBookActualStock.Text = "";
            tbBookActualStock.Text = "";
        }
    }
}