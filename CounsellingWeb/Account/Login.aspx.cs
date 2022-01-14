using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CounsellingServer.BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CounsellingWeb.Account
{
    public partial class Login : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }



          

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
             DataSet ds = new DataSet();           
            try
            {
                SqlCommand com = new SqlCommand("select * from tbusers where Email=" + txtLogIn.Text + " and password=" + txtPassword.Text, conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("ConnectionString"))
                {
                    //dvAlert.Visible = true;
                    //ltrMessage.Text = "Invalid Login ID";
                    return;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("~/Account/Home.aspx");
            }
        }
    }
}