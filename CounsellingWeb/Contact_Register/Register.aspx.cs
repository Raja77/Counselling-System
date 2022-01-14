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
    public partial class Register : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
         
            conn.Open();

             DataSet ds = new DataSet();
            try
            {
                SqlCommand com = new SqlCommand("select * from tbusers where Convenor_Role='Divisional Head'", conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().Contains("DefaultConnection"))
                {
                    return;
                }
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Convenor_Role"].Equals("Divisional Head"))
                {
                    drpConvenorRole.Items.Remove(new ListItem("Divisional Head", "1"));

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Close();
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into tbUsers (Name,Email,College,Designation,Convenor_Role,Password)" +
                   " values(@Name,@Email,@CollegeID,@Designation,@Convenor_Role,@Password)", conn);
                cmd.Parameters.AddWithValue("@Name", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@CollegeID", drpCollege.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@Convenor_Role", drpConvenorRole.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    lblmsg.Text = "Your details has been Saved!";
                    lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
                    lblmsg.Font.Size = 20;
                    //conn.Close();
                    //conn.Open();
                    //DataSet ds = new DataSet();
                    //try
                    //{
                    //    SqlCommand com = new SqlCommand("select * from tbusers where Email='" + txtEmail.Text + "' and Name='" + txtUserName.Text + "'", conn);
                    //    SqlDataAdapter sda = new SqlDataAdapter(com);
                    //    sda.Fill(ds);
                    //}
                    //catch (Exception ex)
                    //{
                    //    if (ex.Message.ToString().Contains("DefaultConnection"))
                    //    {
                    //        //dvAlert.Visible = true;
                    //        //ltrMessage.Text = "Invalid Login ID";
                    //        return;
                    //    }
                    //}
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    Session["UserName"] = ds.Tables[0].Rows[0]["Name"];
                    //    Session["Image"] = ds.Tables[0].Rows[0]["Image"];
                    //    Response.Redirect("~/DashBoard.aspx");
                    //}






                }
                cmd.Dispose();
            }


            catch (SqlException ex)
            {
                lblmsg.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}