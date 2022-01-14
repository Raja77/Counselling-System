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
    public partial class UserApproval : Page
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
                BindGrid();
            }
            if (IsPostBack)
            {
            }
        }

        protected void BindGrid()
        {
            try
            {

                SqlCommand com = new SqlCommand("select * from tbUsers", conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSystemUserDetails.DataSource = ds;
                    grdSystemUserDetails.DataBind();
                }
                else
                {
                    grdSystemUserDetails.DataBind();
                }
            }
            catch (Exception e)
            { }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in grdSystemUserDetails.Rows)
            {
                CheckBox chkPostItem = (CheckBox)row.FindControl("chkItem");
                HiddenField UserID = (HiddenField)row.FindControl("hdnUserID");
            }
            // int txt = Convert.ToInt32(((LinkButton)sender).CommandArgument, 0);
        }

        protected void grdSystemUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            bool chk = false;
            int UserID = 0;
            try
            {
                if (e.CommandName == "Approve")
                {
                    chk = true;
                    string rowCommand = e.CommandArgument.ToString();
                    string[] CommandArgument = rowCommand.Split(';');
                    UserID=int.Parse(CommandArgument[0]);
                }
                else
                {
                    string rowCommand = e.CommandArgument.ToString();
                    string[] CommandArgument = rowCommand.Split(';');
                    UserID = int.Parse(CommandArgument[0]);
                    chk = false;
                }
                conn.Open();
                SqlCommand cmd = new SqlCommand("Update tbUsers SET  IsApproval ='" + chk + "'" + "where UserID='" + UserID + "'", conn);
                cmd.Parameters.AddWithValue("@Approval", chk);

                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    lblmsg.Text = "Record has been Updated!";
                    lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
                    lblmsg.Font.Size = 20;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Font.Size = 20;
            }

        }
    }
}