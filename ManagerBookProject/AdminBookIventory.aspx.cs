using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class AdminBookIventory : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath = "";
        static int global_actual_stock;
        static int global_current_stock;
        static int global_issued_books;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillAuthorValues();
                fillPublisherValues();
            }
            BookInventoryDataTable.DataBind();
        }

        //Go Button Click
        protected void GoBtnClick(object sender, EventArgs e)
        {
            GetBookByID();
        }

        //Update Button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            updateBooksByID();
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

        //Delete button Click
        protected void Button2_Click(object sender, EventArgs e)
        {
            DeleteBookByID();
        }

        //User defined functions

        protected void DeleteBookByID()
        {
            if (checkIfBookExist())
            {
                try
                {
                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("DELETE FROM book_master_tbl WHERE boo_id = '" + tbBookID.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Book Delete Successfully.');</script>");
                    BookInventoryDataTable.DataBind();
                    ClearField();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID.');</script>");
            }
        }

        protected void updateBooksByID()
        {
            if (checkIfBookExist())
            {
                try
                {
                    int actual_stock = Convert.ToInt32(tbBookActualStock.Text.Trim());
                    int current_stock = Convert.ToInt32(tbBookCurrentStock.Text.Trim());

                    if (global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if (actual_stock < global_issued_books)
                        {
                            Response.Write("<script>alert('Actual Stock value cannot be less than the Issued books');</script>");
                            return;
                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            tbBookCurrentStock.Text = "" + current_stock;
                        }
                    }

                    string genres = "";
                    foreach (int i in GenresBook.GetSelectedIndices())
                    {
                        genres = genres + GenresBook.Items[i] + ",";
                    }
                    genres = genres.Remove(genres.Length - 1);


                    string filepath = "~/Image/books1";
                    string filename = Path.GetFileName(UploadImageBook.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = global_filepath;
                    }
                    else
                    {
                        UploadImageBook.SaveAs(Server.MapPath("Book_Inventory/" + filename));
                        filepath = "~/Book_Inventory/" + filename;
                    }

                    SqlConnection con = new SqlConnection(connectionString);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl SET" +
                        " book_name = @book_name, genre = @genre, author_name = @author_name," +
                        "publisher_name = @publisher_name, " +
                        "publish_date = @publish_date, language = @language, edition = @edition, book_cost = @book_cost, no_of_pages = @no_of_pages," +
                        "book_description = @book_description, actual_stock = @actual_stock, curent_stock = @curent_stock," +
                        "book_img_link = @book_img_link WHERE boo_id = '" + tbBookID.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@book_name", tbBookName.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@author_name", ddlAuthorName.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publisher_name", ddlPublisherName.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@publish_date", tbBookPublisherDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", ddlBookLanguage.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", tbBookEdition.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_cost", tbBookCost.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", tbBookNoPages.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", tbBookDerscription.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    cmd.Parameters.AddWithValue("@curent_stock", current_stock.ToString());
                    cmd.Parameters.AddWithValue("@book_img_link", filepath);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    BookInventoryDataTable.DataBind();
                    Response.Write("<script>alert('Book Update Successfully.');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Book ID.');</script>");
            }
        }

        protected void GetBookByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE boo_id = '"+ tbBookID.Text.Trim() +"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    tbBookName.Text = dt.Rows[0]["book_name"].ToString();
                    tbBookPublisherDate.Text = dt.Rows[0]["publish_date"].ToString();
                    tbBookEdition.Text = dt.Rows[0]["edition"].ToString();
                    tbBookCost.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    tbBookNoPages.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    tbBookActualStock.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    tbBookCurrentStock.Text = dt.Rows[0]["curent_stock"].ToString().Trim();
                    tbBookDerscription.Text = dt.Rows[0]["book_description"].ToString();
                    tbBookIssued.Text = "" + ( Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["curent_stock"].ToString()));

                    ddlBookLanguage.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    ddlPublisherName.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();
                    ddlAuthorName.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();

                    GenresBook.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for(int i = 0; i < genre.Length; i++)
                    {
                        for(int j = 0; j < GenresBook.Items.Count; j++)
                        {
                            if (GenresBook.Items[j].ToString() == genre[i])
                            {
                                GenresBook.Items[j].Selected = true;
                            }
                        }
                    }
                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID.');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

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
                    " WHERE boo_id = '" + tbBookID.Text.Trim() + "' OR book_name = '"+ tbBookName.Text.Trim() +"'", con);
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
            ddlAuthorName.SelectedItem.Text = "";
            ddlPublisherName.SelectedItem.Text = "";
            tbBookPublisherDate.Text = "";
            ddlBookLanguage.SelectedItem.Text = "";
            tbBookEdition.Text = "";
            tbBookCost.Text = "";
            tbBookNoPages.Text = "";
            tbBookDerscription.Text = "";
            tbBookActualStock.Text = "";
            tbBookActualStock.Text = "";
        }
    }
}