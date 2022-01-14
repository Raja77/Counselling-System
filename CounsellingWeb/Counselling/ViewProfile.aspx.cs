using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;

namespace CounsellingWeb
{
    public partial class ViewProfile : Page
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
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                DataSet ds = new DataSet();
                try
                {
                    SqlCommand com = new SqlCommand("select * from tbusers where UserID='" + Session["UserID"] + "'", conn);
                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    sda.Fill(ds);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString().Contains("DefaultConnection"))
                    {
                        lblmsg.Text = ex.Message.ToString();
                        return;
                    }
                }
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    txtUserName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                    txtCollege.Text = ds.Tables[0].Rows[0]["College"].ToString();
                    txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                    txtPhone.Text = ds.Tables[0].Rows[0]["Phone"].ToString();
                }




            }
            if (IsPostBack)
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
            }
        }


        //protected void drpCollege_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    drpCollege.Items.Insert(1, new ListItem("Womens College MA Road", "1"));
        //    drpCollege.Items.Insert(2, new ListItem("SP College", "2"));
        //    drpCollege.Items.Insert(3, new ListItem("ICSC, University of Kashmir", "3"));
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string image=string.Empty;
            try
            {
                conn.Open();

                
                if (fuImage.HasFile)
                {
                    try
                    {
                        sb.AppendFormat(" Uploading file: {0}", fuImage.FileName);

                        //saving the file
                        fuImage.PostedFile.SaveAs(Server.MapPath("~/UploadFiles/" + fuImage.FileName));

                        //Showing the file information
                        sb.AppendFormat("<br/> Save As: {0}", fuImage.PostedFile.FileName);
                        sb.AppendFormat("<br/> File type: {0}", fuImage.PostedFile.ContentType);
                        sb.AppendFormat("<br/> File length: {0}", fuImage.PostedFile.ContentLength);
                        sb.AppendFormat("<br/> File name: {0}", fuImage.PostedFile.FileName);
                        image = "~/UploadFiles/" + fuImage.FileName;

                    }
                    catch (Exception ex)
                    {
                        sb.Append("<br/> Error <br/>");
                        sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
                    }
                }
                else
                {
                    lblmsg.Text = sb.ToString();
                }

               // SqlCommand cmd = new SqlCommand("Insert into tbUsers (Name,Email,Phone,Password,Image)" +
                 //  " values(@Name,@Email, @Phone,@Password,@Image)", conn);
                //string SID = "4"; 
                SqlCommand cmd = new SqlCommand("Update tbUsers SET  Name = @Name,Email=@Email,Phone=@Phone,Image=@Image, Password=@Password where UserID='" + Session["UserID"] + "'", conn);
                cmd.Parameters.AddWithValue("@Name", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                //cmd.Parameters.AddWithValue("@CollegeID", drpCollege.SelectedValue.ToString());
                //cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                //cmd.Parameters.AddWithValue("@Convenor_Role", drpConvenorRole.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Image", image);

                Session["UserName"] = txtUserName.Text;
                Session["Image"] = image;

                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    lblmsg.Text = "Record has been Saved!";
                    lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
                    lblmsg.Font.Size = 20;
                    // BindGrid();
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
            Response.Redirect("DashBoard.aspx");
        }

        //protected void drpConvenorRole_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // drpConvenorRole.Items.Clear();
        //    drpConvenorRole.Items.Insert(1, new ListItem("District Coordinator", "1"));
        //    drpConvenorRole.Items.Insert(2, new ListItem("College Coordinator", "2"));
        //    drpConvenorRole.Items.Insert(3, new ListItem("Event Coordinator", "3"));
        //}

    }
}