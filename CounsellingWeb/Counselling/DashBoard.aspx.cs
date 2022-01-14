using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CounsellingWeb
{
    public partial class DashBoard : Page
    {
        public String programme
        {


            get
            {
                if (Session["programme"] == null)
                {
                    Session["programme"] = String.Empty;
                }
                return Session["programme"].ToString();
            }
            set
            {
                Session["programme"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public string MakeShort(String str)
        {

            if (str.Length > 20)
            {

                str = str.Substring(0, 20) + "...";

            }

            return str;

        }
        protected void BindGrid()
        {
            SqlCommand com;
            try
            {
                if (Session["UserRole"].Equals("Divisional Head"))
                {
                    com = new SqlCommand("select * from tbUserEvents  order by CreationDate desc", conn);
                }
                else {
                    userEvents.InnerText = "My Events";
                    com = new SqlCommand("select * from tbUserEvents where UserID='" + Session["UserID"] + "'  order by CreationDate desc", conn);
                }
                
                
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdUserEvents.DataSource = ds;
                    grdUserEvents.DataBind();
                }
                else
                {
                    grdUserEvents.DataBind();
                }
            }
            catch (Exception e)
            { }
        }
    }
}