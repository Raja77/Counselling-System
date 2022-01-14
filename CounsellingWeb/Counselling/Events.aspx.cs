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
    public partial class Events : Page
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
            }
        }

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
                SqlCommand cmd = new SqlCommand("Insert into tbUserEvents (EventName,EventDescription,Image, UserID)" +
                   " values(@EventName,@EventDescription,@Image,@UserID)", conn);
                cmd.Parameters.AddWithValue("@EventName", txtEventName.Text);
                cmd.Parameters.AddWithValue("@EventDescription", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    lblmsg.Text = "Event has been Saved!";
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