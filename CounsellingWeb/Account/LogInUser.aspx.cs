using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;

namespace CounsellingWeb
{
    public partial class LogInUser : Page
    {
        //Cargo crgo;
        public LogInUser()
        {
          //  UrlMessage = new UrlMessage();
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
           

            HttpCookie c = Request.Cookies["LastLogin"];
            //crgo = new Cargo(this.ViewState);
            //this.RequireAuthentication = false;
            //this.IsFirstPage = true;
            //PageCode = "Home";
            //PageName = "Home";
            if (!Page.IsPostBack)
            {
                System.Web.HttpContext.Current.Session["UniqueSiteId"] = null;
            }
            // base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {

                if (c == null)
                {
                    txtLogIn.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    txtLogIn.Text = c.Values["UserId"];
                    txtPassword.Text = c.Values["Password"];
                }
            }
            
        }
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("select * from tbusers where Email='" + txtLogIn.Text + "' and password='" + txtPassword.Text + "'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("ConnectionString"))
                {
                    //dvAlert.Visible = true;
                    lblmsg.Text = "Invalid Login ID";
                    return;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("~/Account/Home.aspx");
            }
            else
            {
                lblmsg.Text = "Please check credentials";
            }
        }

        protected void LnkForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoginAssistence/ForGetPassword.aspx");

        }
        protected void LnkChangepassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ChangePassword.aspx?");
        }

    }
}