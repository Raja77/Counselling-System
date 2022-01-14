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

namespace CounsellingWeb
{
    public partial class Login : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {



          

        }

        protected void lnkLogIn_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("select * from tbusers where Email='" + txtLogIn.Value + "' and password='" + txtPassword.Value +"'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("DefaultConnection"))
                {
                    lblMsg.Text = ex.Message.ToString();
                    return;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Session["UserName"] = ds.Tables[0].Rows[0]["Name"];
                Session["Image"] = ds.Tables[0].Rows[0]["Image"];
                Session["UserID"] = ds.Tables[0].Rows[0]["UserID"];
                Session["UserRole"] = ds.Tables[0].Rows[0]["Convenor_Role"];

                if (Session["UserRole"].Equals("Divisional Head") || ds.Tables[0].Rows[0]["IsApproval"].ToString() == "True")
                {
                    Response.Redirect("~/Counselling/DashBoard.aspx");
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Font.Size = 14;
                    lblMsg.Text = "You are not yet authorised user to access the system!!!";
                }


              
            }
            else
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Font.Size = 20;
                lblMsg.Text = "Invalid User Name/Password";
            }
        }
    }
}