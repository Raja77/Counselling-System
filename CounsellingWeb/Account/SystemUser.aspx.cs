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
    public partial class SystemUser : Page
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
             //   drpCollege.Items.Clear();
                drpCollege_SelectedIndexChanged(null,null);

                drpConvenorRole_SelectedIndexChanged(null, null);
            }
            if (IsPostBack)
            {
                if (!(String.IsNullOrEmpty(txtPassword.Text.Trim())))
                {
                    txtPassword.Attributes["value"] = txtPassword.Text;
                }
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

        

        protected void drpCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            drpCollege.Items.Insert(1, new ListItem("Womens College MA Road", "1"));
            drpCollege.Items.Insert(2, new ListItem("SP College", "2"));
            drpCollege.Items.Insert(3, new ListItem("ICSC, University of Kashmir", "3"));
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

            protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into tbUsers (Name,Email,Phone,CollegeID,Designation,Convenor_Role,Password)" +
                       " values(@Name,@Email, @Phone,@CollegeID,@Designation,@Convenor_Role,@Password)", conn);
                    cmd.Parameters.AddWithValue("@Name", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@CollegeID", drpCollege.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@Convenor_Role", drpConvenorRole.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        lblmsg.Text = "Record has been Saved!";
                        lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
                        lblmsg.Font.Size = 20;
                        BindGrid();
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

            protected void drpConvenorRole_SelectedIndexChanged(object sender, EventArgs e)
            {
               // drpConvenorRole.Items.Clear();
                drpConvenorRole.Items.Insert(1, new ListItem("District Coordinator", "1"));
                drpConvenorRole.Items.Insert(2, new ListItem("College Coordinator", "2"));
                drpConvenorRole.Items.Insert(3, new ListItem("Event Coordinator", "3"));
            }

    }
}