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
    public partial class ESS:Page
    {
        #region Properties

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public String programme
        {
            get
            {
                if (ViewState["programme"] == null)
                {
                    ViewState["programme"] = String.Empty;
                }
                return ViewState["programme"].ToString();
            }
            set
            {
                ViewState["programme"] = value;
            }
        }

        public String stuName
        {
            get
            {
                if (ViewState["stuName"] == null)
                {
                    ViewState["stuName"] = String.Empty;
                }
                return ViewState["stuName"].ToString();
            }
            set
            {
                ViewState["stuName"] = value;
            }
        }

        public String Marks
        {
            get
            {
                if (ViewState["Marks"] == null)
                {
                    ViewState["Marks"] = String.Empty;
                }
                return ViewState["Marks"].ToString();
            }
            set
            {
                ViewState["Marks"] = value;
            }
        }
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  drppref1_SelectedIndexChanged(null, null);

            }
            lblmsg.Text = string.Empty;
        }

        protected void drppref1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drppref1.SelectedIndex == 0)
            {
                drppref1.Enabled = true;
                drppref2.Enabled = false;
                drppref3.Enabled = false;
                drppref4.Enabled = false;
            }
            else
            {
                drppref1.Enabled = false;
                drppref2.Enabled = true;
                drppref2.Items.Remove(drppref1.SelectedItem);
                drppref3.Items.Remove(drppref1.SelectedItem);
                drppref4.Items.Remove(drppref1.SelectedItem);
            }
        }

        protected void drppref2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drppref2.SelectedIndex == 0)
            {
                drppref3.Enabled = false;
                drppref4.Enabled = false;
            }
            else
            {
                drppref2.Enabled = false;
                drppref3.Enabled = true;
                drppref3.Items.Remove(drppref2.SelectedItem);
                drppref4.Items.Remove(drppref2.SelectedItem);
            }
        }

        protected void drppref3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // drppref3.Items.Clear();
            // chkCourses(programme, drppref3);

            if (drppref3.SelectedIndex == 0)
            {
                drppref4.Enabled = false;
            }
            else
            {
                drppref3.Enabled = false;
                drppref4.Enabled = true;
                drppref4.Items.Remove(drppref3.SelectedItem);
            }
        }

        protected void drppref4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drppref4.SelectedIndex != 0)
            {
                drppref4.Enabled = false;
            }
        }

        protected void btnCheckDetails_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand("select * from tbStudentDetails where classrollno=" + txtRollNo.Text.ToString().Trim(), conn);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dvPref.Visible = true;
                }
                else
                {
                    dvPref.Visible = false;
                }
                grdStudentsDetail.DataSource = ds;
                grdStudentsDetail.DataBind();






                foreach (GridViewRow r in grdStudentsDetail.Rows)
                {
                    String programmeName = (r.FindControl("lblCourseapplied") as Label).Text;
                    programme = programmeName;

                    String StudentName = (r.FindControl("lblName") as Label).Text;
                    stuName = StudentName;

                    String mrks = (r.FindControl("lblMarks") as Label).Text;
                    Marks = mrks;
                }

                if (RecordExits())
                {
                    lblmsg.Text = "You have already made the GE selection!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    drppref1.Items.Clear();
                    chkCourses(programme, drppref1);
                    drppref1_SelectedIndexChanged(null, null);
                    drppref2.Items.Clear();
                    chkCourses(programme, drppref2);
                    drppref3.Items.Clear();
                    chkCourses(programme, drppref3);
                    drppref4.Items.Clear();
                    chkCourses(programme, drppref4);
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RecordExits())
                {
                    lblmsg.Text = "You have already made the GE selection!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into tbStudentChoices (RollNo,Name,Course,Merit1,Merit2,Merit3,Pref1,Pref2,Pref3,Pref4,Alloted )" +
                       " values(@RollNo,@Name, @Course,@Merit1,@Merit2,@Merit3,@Pref1,@Pref2,@Pref3,@Pref4,@Alloted)", conn);
                    cmd.Parameters.AddWithValue("@RollNo", Convert.ToInt32(txtRollNo.Text.Trim()));
                    cmd.Parameters.AddWithValue("@Name", stuName);
                    cmd.Parameters.AddWithValue("@Course", programme);
                    cmd.Parameters.AddWithValue("@Merit1", Convert.ToDouble(Marks));
                    cmd.Parameters.AddWithValue("@Merit2", Convert.ToDouble(0));
                    cmd.Parameters.AddWithValue("@Merit3", "");
                    cmd.Parameters.AddWithValue("@Pref1", drppref1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pref2", drppref2.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pref3", drppref3.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pref4", drppref4.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Alloted", "");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        lblmsg.Text = "Your GE preference has been recorded!";
                        lblmsg.ForeColor = System.Drawing.Color.CornflowerBlue;
                        lblmsg.Font.Size = 20;
                        btnClear.Visible = false;
                        btnSubmit.Visible = false;
                        btnPrint.Visible = true;
                    }
                    cmd.Dispose();
                }

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Request.Url.AbsoluteUri);
            drppref1.Items.Clear();
            chkCourses(programme, drppref1);
            drppref1_SelectedIndexChanged(null, null);
            drppref2.Items.Clear();
            chkCourses(programme, drppref2);
            drppref3.Items.Clear();
            chkCourses(programme, drppref3);
            drppref4.Items.Clear();
            chkCourses(programme, drppref4);
            drppref1.Enabled = true;
        }

        #endregion

        #region Methods
        protected void chkCourses(string courseName, DropDownList drpdwnID)
        {
            switch (courseName)
            {
                case "Integrated PG in Botany":
                case "BSC Botany(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    drpdwnID.Items.Insert(3, new ListItem("Animal Diversity [Department of Zoology]", "Animal Diversity"));
                    drpdwnID.Items.Insert(4, new ListItem("Fundamental Concepts of Chemistry [Department of Chemistry]", "Fundamental Concepts of Chemistry"));
                    dvNOPref.Visible = false;
                    dvPref.Visible = true;
                    break;
                case "Integrated PG in Chemistry":
                case "BSC Chemistry(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    drpdwnID.Items.Insert(3, new ListItem("Animal Diversity [Department of Zoology]", "Animal Diversity"));
                    drpdwnID.Items.Insert(4, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate) [Department of Botany]", "Biodiversity (Microbes, Algae, Fungi and Archegoniate)"));
                    dvNOPref.Visible = false;
                    dvPref.Visible = true;
                    break;
                case "BSC Zoology(Hons)":
                case "Integrated PG in Zoology":
                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    drpdwnID.Items.Insert(3, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate) [Department of Botany]", "Biodiversity (Microbes, Algae, Fungi and Archegoniate)"));
                    drpdwnID.Items.Insert(4, new ListItem("Fundamental Concepts of Chemistry [Department of Chemistry]", "Fundamental Concepts of Chemistry"));
                    dvNOPref.Visible = false;
                    dvPref.Visible = true;
                    break;
                default:
                    dvNOPref.Visible = true;
                    dvPref.Visible = false;
                    lblError.Text = "You are not eligible for GE subject";
                    break;
            }
        }
        private bool RecordExits()
        {
            SqlCommand cmd = new SqlCommand("Select *  from tbStudentChoices where RollNo =" + txtRollNo.Text.Trim(), conn);
            SqlDataReader sReader = null;
            Int32 numberOfRows = 0;
            btnSubmit.Visible = true;
            btnClear.Visible = true;
            btnPrint.Visible = false;

            try
            {
                conn.Open();
                sReader = cmd.ExecuteReader();

                while (sReader.Read())
                {
                    if (!(sReader.IsDBNull(0)))
                    {
                        numberOfRows = Convert.ToInt32(sReader[0]);
                        if (numberOfRows > 0)
                        {
                            btnPrint.Visible = true;
                            btnSubmit.Visible = false;
                            btnClear.Visible = false;
                            drppref1.Enabled = false;
                            drppref1.Items.Clear();
                            drppref1.Items.Add(sReader["Pref1"].ToString());
                            drppref2.Items.Clear();
                            drppref2.Items.Add(sReader["Pref2"].ToString());
                            drppref3.Items.Clear();
                            drppref3.Items.Add(sReader["Pref3"].ToString());
                            drppref4.Items.Clear();
                            drppref4.Items.Add(sReader["Pref4"].ToString());
                            return true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        private void GEAllocation()
        {
            SqlCommand cmd = new SqlCommand("Select *  from tbStudentChoices order by Merit1 desc", conn);
            SqlDataReader sReader = null;
            Int32 numberOfRows = 0;
            Int32 bt = 4, bc = 4, z = 2, c = 2, b = 2;
            Int32 a_bt = 0, a_bc = 0, a_z = 0, a_c = 0, a_b = 0;

            /*    < asp:ListItem Text = "Biomolecules and Enzymes" Value = "Bio-Technology" ></ asp:ListItem >        
                    < asp:ListItem Text = "Biomolecules" Value = "Bio-Chemistry" ></ asp:ListItem >               
                           < asp:ListItem Text = "Animal Diversity" Value = "Zoology" ></ asp:ListItem >                      
                                  < asp:ListItem Text = "Biodiversity (Microbes, Algae, Fungi and Archegoniate)" Value = "Botany" ></ asp:ListItem >                             
                                         < asp:ListItem Text = "Fundamental Concepts of Chemistry" Value = "Chemistry" ></ asp:ListItem > */

           

            int rollNo;
            string p1, p2, p3, p4;
            try
            {
                conn.Open();
                sReader = cmd.ExecuteReader();

           

                while (sReader.Read())
                {
                    if (!(sReader.IsDBNull(0)))
                    {
                        numberOfRows = Convert.ToInt32(sReader[0]);
                        if (numberOfRows > 0)
                        {
                            rollNo = (Convert.ToInt32(sReader["RollNo"].ToString()));
                            p1 = (sReader["Pref1"].ToString());
                            p2 = (sReader["Pref2"].ToString());
                            p3 = (sReader["Pref3"].ToString());
                            p4 = (sReader["Pref4"].ToString());

                            bool flag = false;
                            switch (p1)
                            {
                                case "Biomolecules and Enzymes":
                                    if (a_bt < bt)
                                    {
                                        allocate(p1, rollNo);
                                        a_bt++;
                                        flag = true;
                                    }
                                    break;
                                case "Biomolecules":
                                    if (a_bc < bc)
                                    {
                                        allocate(p1, rollNo);
                                        a_bc++;
                                        flag = true;
                                    }
                                    break;
                                case "Animal Diversity":
                                    if (a_z < z)
                                    {
                                        allocate(p1, rollNo);
                                        a_z++;
                                        flag = true;
                                    }
                                    break;
                                case "Biodiversity (Microbes, Algae, Fungi and Archegoniate)":
                                    if (a_b < b)
                                    {
                                        allocate(p1, rollNo);
                                        a_b++;
                                        flag = true;
                                    }
                                    break;
                                case "Fundamental Concepts of Chemistry":
                                    if (a_c < c)
                                    {
                                        allocate(p1, rollNo);
                                        a_c++;
                                        flag = true;
                                    }
                                    break;
                                default:
                                    break;
                            }

                            if (flag == false)
                            {
                                switch (p2)
                                {
                                    case "Biomolecules and Enzymes":
                                        if (a_bt < bt)
                                        {
                                            allocate(p2, rollNo);
                                            a_bt++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biomolecules":
                                        if (a_bc < bc)
                                        {
                                            allocate(p2, rollNo);
                                            a_bc++;
                                            flag = true;
                                        }
                                        break;
                                    case "Animal Diversity":
                                        if (a_z < z)
                                        {
                                            allocate(p2, rollNo);
                                            a_z++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biodiversity (Microbes, Algae, Fungi and Archegoniate)":
                                        if (a_b < b)
                                        {
                                            allocate(p2, rollNo);
                                            a_b++;
                                            flag = true;
                                        }
                                        break;
                                    case "Fundamental Concepts of Chemistry":
                                        if (a_c < c)
                                        {
                                            allocate(p2, rollNo);
                                            a_c++;
                                            flag = true;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (flag == false)
                            {
                                switch (p3)
                                {
                                    case "Biomolecules and Enzymes":
                                        if (a_bt < bt)
                                        {
                                            allocate(p3, rollNo);
                                            a_bt++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biomolecules":
                                        if (a_bc < bc)
                                        {
                                            allocate(p3, rollNo);
                                            a_bc++;
                                            flag = true;
                                        }
                                        break;
                                    case "Animal Diversity":
                                        if (a_z < z)
                                        {
                                            allocate(p3, rollNo);
                                            a_z++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biodiversity (Microbes, Algae, Fungi and Archegoniate)":
                                        if (a_b < b)
                                        {
                                            allocate(p3, rollNo);
                                            a_b++;
                                            flag = true;
                                        }
                                        break;
                                    case "Fundamental Concepts of Chemistry":
                                        if (a_c < c)
                                        {
                                            allocate(p3, rollNo);
                                            a_c++;
                                            flag = true;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (flag == false)
                            {
                                switch (p4)
                                {
                                    case "Biomolecules and Enzymes":
                                        if (a_bt < bt)
                                        {
                                            allocate(p4, rollNo);
                                            a_bt++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biomolecules":
                                        if (a_bc < bc)
                                        {
                                            allocate(p4, rollNo);
                                            a_bc++;
                                            flag = true;
                                        }
                                        break;
                                    case "Animal Diversity":
                                        if (a_z < z)
                                        {
                                            allocate(p4, rollNo);
                                            a_z++;
                                            flag = true;
                                        }
                                        break;
                                    case "Biodiversity (Microbes, Algae, Fungi and Archegoniate)":
                                        if (a_b < b)
                                        {
                                            allocate(p4, rollNo);
                                            a_b++;
                                            flag = true;
                                        }
                                        break;
                                    case "Fundamental Concepts of Chemistry":
                                        if (a_c < c)
                                        {
                                            allocate(p4, rollNo);
                                            a_c++;
                                            flag = true;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }

                            



                        }
                    }

                }
                sReader.Close();
                sReader = cmd.ExecuteReader();
                GridView1.DataSource = sReader;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void allocate(string choice_allocated, int rollNo)
        {
            SqlCommand cmd_update = new SqlCommand("Update tbStudentChoices SET Alloted='" + choice_allocated + "' where rollNo=" + rollNo, conn);
            int k = cmd_update.ExecuteNonQuery();
            if (k != 0)
            {
            }
            #endregion

        }

        protected void btnViewAllocation_Click(object sender, EventArgs e)
        {
            GEAllocation();
        }
    }
}