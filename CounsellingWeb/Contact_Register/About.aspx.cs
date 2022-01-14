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
    public partial class About : Page
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
                Response.Redirect("~/geSubjectSelection.aspx");
                drppref1_SelectedIndexChanged(null, null);

            }
        }

        protected void chkCourses(string courseName, DropDownList drpdwnID)
        {
            switch (courseName)
            {
                case "Integrated PG in Botany":
                case "BSC Botany(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules and Cell Biology_Bio-Technology", "1"));
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules_Bio-Chemistry", "2"));
                    drpdwnID.Items.Insert(0, new ListItem("Animal Diversity_Zoology", "3"));
                    drpdwnID.Items.Insert(0, new ListItem("Fundamental Concepts of Chemistry_Chemistry", "5"));
                    break;
                case "Integrated PG in Chemistry":
                case "BSC Chemistry(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules and Cell Biology_Bio-Technology", "1"));
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules_Bio-Chemistry", "2"));
                    drpdwnID.Items.Insert(0, new ListItem("Animal Diversity_Zoology", "3"));
                    drpdwnID.Items.Insert(0, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate)_Botany", "4"));
                    break;
                case "BSC Zoology(Hons)":
                case "Integrated PG in Zoology":
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules and Cell Biology_Bio-Technology", "1"));
                    drpdwnID.Items.Insert(0, new ListItem("Biomolecules_Bio-Chemistry", "2"));
                    drpdwnID.Items.Insert(0, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate)_Botany", "4"));
                    drpdwnID.Items.Insert(0, new ListItem("Fundamental Concepts of Chemistry_Chemistry", "5"));
                    break;
            }
        }      

        protected void drppref1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkCourses(programme, drppref1);
            if (ViewState["name"] != null)
            {
                lbl1.Text = ViewState["name"].ToString();
            }
            chkCourses(lbl1.Text, drppref1);
        }

        protected void drppref2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkCourses(programme, drppref2);
        }

        protected void drppref3_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkCourses(programme, drppref3);
        }

        protected void btnCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //create new sqlconnection and connection to database by using connection string from web.config file  
                SqlConnection con = new SqlConnection(strcon);

                SqlCommand com = new SqlCommand("select * from tbStudentDetails where classrollno=" + txtRollNo.Text.ToString().Trim(), con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dvPref.Visible = true;
                    foreach (GridViewRow r in grdStudentsDetail.Rows)
                    {
                        String programmeName = (r.FindControl("lblCourseapplied") as Label).Text;
                        programme = programmeName;

                        ViewState["name"] = programmeName;
                    }
                    drppref1_SelectedIndexChanged(null, null);
                }
                else
                {
                    dvPref.Visible = false;
                }
                grdStudentsDetail.DataSource = ds;
                grdStudentsDetail.DataBind();


               

            }
            catch (Exception ex)
            {

            }
        }
    }
}