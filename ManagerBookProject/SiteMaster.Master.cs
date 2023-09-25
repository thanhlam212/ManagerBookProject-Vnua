    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManagerBookProject
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session != null)
                {
                    string role = Session["role"] as string;
                    if (String.IsNullOrEmpty(role))
                    {
                        userLink.Visible = true;
                        signUpLink.Visible = true;

                        logOutLink.Visible = false;
                        HelooUserLink.Visible = false;

                        adminLogin.Visible = true;
                        AuthorMng.Visible = false;
                        PublisherMng.Visible = false;
                        BookIvt.Visible = false;
                        BookIss.Visible = false;
                        MemberMng.Visible = false;
                    }
                    else if (role.Equals("user"))
                    {
                        userLink.Visible = false;
                        signUpLink.Visible = false;

                        logOutLink.Visible = true;
                        HelooUserLink.Visible = true;
                        HelooUserLink.Text = "Hello" + Session["username"].ToString();

                        adminLogin.Visible = true;
                        AuthorMng.Visible = false;
                        PublisherMng.Visible = false;
                        BookIvt.Visible = false;
                        BookIss.Visible = false;
                        MemberMng.Visible = false;
                    }
                    else if (role.Equals("admin"))
                    {
                        userLink.Visible = false;
                        signUpLink.Visible = false;

                        logOutLink.Visible = true;
                        HelooUserLink.Visible = true;
                        HelooUserLink.Text = "Hello Admin" + Session["fullname"].ToString();

                        adminLogin.Visible = false;
                        AuthorMng.Visible = true;
                        PublisherMng.Visible = true;
                        BookIvt.Visible = true;
                        BookIss.Visible = true;
                        MemberMng.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void adminLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLogin.aspx");
        }

        protected void AuthorMng_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAuthorManagement.aspx");
        }

        protected void PublisherMng_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPublisherManagement.aspx");
        }

        protected void BookIvt_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookInventory.aspx");
        }

        protected void BookIss_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookIssuing.aspx");
        }

        protected void MemberMng_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagement.aspx");
        }

        protected void logOutLink_Click(object sender, EventArgs e)
        {
            Session["username"] = " ";
            Session["fullname"] = " ";
            Session["role"] = " ";
            Session["status"] = " ";

            userLink.Visible = true;
            signUpLink.Visible = true;

            logOutLink.Visible = false;
            HelooUserLink.Visible = false;

            adminLogin.Visible = true;
            AuthorMng.Visible = false;
            PublisherMng.Visible = false;
            BookIvt.Visible = false;
            BookIss.Visible = false;
            MemberMng.Visible = false;
        }
    }
}