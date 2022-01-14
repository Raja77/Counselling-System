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
    public partial class Home : Page
    {
        //Cargo crgo;
        public Home()
        {
          //  UrlMessage = new UrlMessage();
        }
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

       
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
       

    }
}