using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

namespace CounsellingWeb
{
    public partial class GEAllocate : Page
    {
        public static Dictionary<string, ArrayList> Choices = new Dictionary<string, ArrayList>();
     
        Dictionary<string, ArrayList> Choices1 = new Dictionary<string, ArrayList>();

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
            //if (!IsPostBack)
            //{
            //  drppref1_SelectedIndexChanged(null, null);
            //SubjectOfferings[0] = new ArrayList() { "GE Subject Name", "Department", Intake, allotted};
            //SubjectOfferings[0] = new ArrayList() { "Biomolecules and Enzymes", "Bio - Technology", 86, 0};
            //SubjectOfferings[1] = new ArrayList() { "Biomolecules", "Bio-Chemistry", 86, 0 };
            //SubjectOfferings[2] = new ArrayList() { "Animal Diversity", "Zoology", 50, 0 };
            //SubjectOfferings[3] = new ArrayList() { "Fundamental Concepts of Chemistry", "Chemistry", 50, 0 };
            //SubjectOfferings[4] = new ArrayList() { "Biodiversity (Microbes, Algae, Fungi and Archegoniate)", "Botany", 50, 0 };
            Choices.Clear();
            Choices.Add("bt_bme", new ArrayList() { "Biomolecules and Enzymes", "Bio-Technology", 86, 0 });
            Choices.Add("bc_bmo", new ArrayList() { "Biomolecules", "Bio-Chemistry", 86, 0 });
            Choices.Add("zo_adv", new ArrayList() { "Animal Diversity", "Zoology", 50, 0 });
            Choices.Add("ch_fcc", new ArrayList() { "Fundamental Concepts of Chemistry", "Chemistry", 50, 0 });
            Choices.Add("by_bdv", new ArrayList() { "Biodiversity (Microbes, Algae, Fungi and Archegoniate)", "Botany", 50, 0 });
            //}

           

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
                    SqlCommand cmd = new SqlCommand("Insert into tbStudentChoices_New (RollNo,Name,Course,Merit1,Merit2,Merit3,Pref1,Pref2,Pref3,Pref4,Alloted )" +
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



        protected void vacancies(string courseName, DropDownList drpdwnID)
        {

        }


        #region Methods
        protected void chkCourses(string courseName, DropDownList drpdwnID)
        {
            ArrayList item;
            switch (courseName)
            {
                case "Integrated PG in Botany":
                case "BSC Botany(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    item = (ArrayList)Choices["bt_bme"];
                    drpdwnID.Items.Insert(1, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["bc_bmo"];
                    drpdwnID.Items.Insert(2, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["zo_adv"];
                    drpdwnID.Items.Insert(3, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["ch_fcc"];
                    drpdwnID.Items.Insert(4, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));


                    //drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    //drpdwnID.Items.Insert(3, new ListItem("Animal Diversity [Department of Zoology]", "Animal Diversity"));
                    //drpdwnID.Items.Insert(4, new ListItem("Fundamental Concepts of Chemistry [Department of Chemistry]", "Fundamental Concepts of Chemistry"));


                    //drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    //drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    //drpdwnID.Items.Insert(3, new ListItem("Animal Diversity [Department of Zoology]", "Animal Diversity"));
                    //drpdwnID.Items.Insert(4, new ListItem("Fundamental Concepts of Chemistry [Department of Chemistry]", "Fundamental Concepts of Chemistry"));
                    dvNOPref.Visible = false;
                    dvPref.Visible = true;
                    break;
                case "Integrated PG in Chemistry":
                case "BSC Chemistry(Hons)":
                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    item = (ArrayList)Choices["bt_bme"];
                    drpdwnID.Items.Insert(1, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["bc_bmo"];
                    drpdwnID.Items.Insert(2, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["zo_adv"];
                    drpdwnID.Items.Insert(3, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["by_bdv"];
                    drpdwnID.Items.Insert(4, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));

                    //drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    //drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    //drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    //drpdwnID.Items.Insert(3, new ListItem("Animal Diversity [Department of Zoology]", "Animal Diversity"));
                    //drpdwnID.Items.Insert(4, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate) [Department of Botany]", "Biodiversity (Microbes, Algae, Fungi and Archegoniate)"));
                    dvNOPref.Visible = false;
                    dvPref.Visible = true;
                    break;
                case "BSC Zoology(Hons)":
                case "Integrated PG in Zoology":

                    drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    item = (ArrayList)Choices["bt_bme"];
                    drpdwnID.Items.Insert(1, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["bc_bmo"];
                    drpdwnID.Items.Insert(2, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["by_bdv"];
                    drpdwnID.Items.Insert(3, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));
                    item = (ArrayList)Choices["ch_fcc"];
                    drpdwnID.Items.Insert(4, new ListItem(item[0] + " [Department of " + item[1] + "] ", item[0].ToString()));

                    //drpdwnID.Items.Insert(0, new ListItem("Select Preference", "-1"));
                    //drpdwnID.Items.Insert(1, new ListItem("Biomolecules and Enzymes [Department of Bio-Technology]", "Biomolecules and Enzymes"));
                    //drpdwnID.Items.Insert(2, new ListItem("Biomolecules [Department of Bio-Chemistry]", "Biomolecules"));
                    //drpdwnID.Items.Insert(3, new ListItem("Biodiversity (Microbes, Algae, Fungi and Archegoniate) [Department of Botany]", "Biodiversity (Microbes, Algae, Fungi and Archegoniate)"));
                    //drpdwnID.Items.Insert(4, new ListItem("Fundamental Concepts of Chemistry [Department of Chemistry]", "Fundamental Concepts of Chemistry"));
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
            SqlCommand cmd = new SqlCommand("Select *  from tbStudentChoices_New where RollNo =" + txtRollNo.Text.Trim(), conn);
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
                            lblAllocatedPref.Text = sReader["Alloted"].ToString();
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

        private string getSubjectName(string p)
        {
            if (p.Equals("Bio-Technology"))
                return "Biomolecules and Enzymes";
            else if (p.Equals("Bio-Chemistry"))
                return "Biomolecules";
            else if (p.Equals("Zoology"))
                return "Animal Diversity";
            else if (p.Equals("Chemistry"))
                return "Fundamental Concepts of Chemistry";
            else if (p.Equals("Botany"))
                return "Biodiversity (Microbes, Algae, Fungi and Archegoniate)";
            else return "Subject not found";
        }

        private void allocate()
        {      
            //In existing table / tbGE table should contain extra column named 'Alloted'
            Dictionary<string, ArrayList> ChoicesCheck = new Dictionary<string, ArrayList>();
            ChoicesCheck.Clear();
            //# Code - Subject , Total Seats, Alloted seats
            ChoicesCheck.Add("bt", new ArrayList() { "Bio-Technology", 86, 0 });
            ChoicesCheck.Add("bc", new ArrayList() { "Bio-Chemistry", 86, 0 });
            ChoicesCheck.Add("zo", new ArrayList() { "Zoology", 50, 0 });
            ChoicesCheck.Add("ch", new ArrayList() { "Chemistry", 50, 0 });
            ChoicesCheck.Add("bo", new ArrayList() { "Botany", 50, 0 });

            try
            {
                
                conn.Open();
                SqlCommand cmd_update = new SqlCommand("Update tbGE SET Alloted=''" , conn);
                int k = cmd_update.ExecuteNonQuery(); //Clear previous allocation              
                SqlCommand cmd = new SqlCommand(" Select *  from tbGE order by twelvemarks desc, classrollno asc, priority asc", conn);
                SqlDataReader sReader = null;
                string rollno, preference, priority;
                string alloted = "", allotedrollno="";
                rollno = preference = priority = "0";
                sReader = cmd.ExecuteReader();
                while (sReader.Read())
                {
                    if (!(sReader.IsDBNull(0)))
                    {
                        rollno = sReader["classrollno"].ToString();
                        preference = sReader["subname"].ToString();
                        priority = sReader["priority"].ToString();
                        alloted = sReader["Alloted"].ToString();

                        //Check if there is any other roll no other than the following series
                        /*
                            BSC Chemistry(Hons)				21120022
                            BSC Zoology(Hons)				21130059    
                            BSC Botany(Hons)				21140001   
                            Integrated PG in Chemistry		21150001
                            Integrated PG in Zoology		21160001	
                            Integrated PG in Botany			21170001	
                         */
                        if (rollno.Substring(0, 4).Equals("2112") || rollno.Substring(0, 4).Equals("2113") ||
               rollno.Substring(0, 4).Equals("2114") || rollno.Substring(0, 4).Equals("2115") ||
                rollno.Substring(0, 4).Equals("2116") || rollno.Substring(0, 4).Equals("2117"))
                        {
                            if (!rollno.Equals(allotedrollno)) //Check to avoid duplicate/ multiple allotment for the same roll no
                            {

                                foreach (KeyValuePair<string, ArrayList> entry in ChoicesCheck)
                                {
                                    if (entry.Value.Contains(preference)) //get preference  choice seat details
                                    {
                                        if (Convert.ToInt32(entry.Value[2]) < Convert.ToInt32(entry.Value[1])) //if(allocated < seats)
                                        {
                                            cmd_update = new SqlCommand("Update tbGE SET Alloted='" + preference + "' where classrollNo=" + rollno, conn);
                                            cmd_update.ExecuteNonQuery();
                                            entry.Value[2] = Convert.ToInt32(entry.Value[2]) + 1;
                                            allotedrollno = rollno;
                                        }
                                    }
                                    if (rollno.Equals(allotedrollno)) //skip checking other subjects for a roll no when allotment alredy made!
                                        break;
                                    else
                                        continue;
                                }
                            }
                        }
                    }
                }

                sReader.Close();
                conn.Close();
                conn.Open();
                sReader = cmd.ExecuteReader();
                GridView1.DataSource = sReader;
                GridView1.DataBind();
                TableRow row;
                TableCell cell;
                int seatTotal = 0, allocateTotal = 0;
                foreach (KeyValuePair<string, ArrayList> entry in ChoicesCheck)
                {
                    row = new TableRow();
                    for (int i = 0; i < 3; i++)
                    {
                        cell = new TableCell();
                        cell.Text = entry.Value[i].ToString();
                        row.Cells.Add(cell);
                        AllocationSummary.Rows.Add(row);
                        if (i == 2)
                            seatTotal += Convert.ToInt32(entry.Value[i]);
                        if (i == 3)
                            allocateTotal += Convert.ToInt32(entry.Value[i]);
                    }
                }
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "<strong>Total</strong>";
                row.Cells.Add(cell);
                row.Cells.Add(new TableCell());//blank cell
                cell = new TableCell();
                cell.Text = seatTotal.ToString();
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = allocateTotal.ToString();
                row.Cells.Add(cell);
                AllocationSummary.Rows.Add(row);





            }
            catch (Exception ex) { }
            finally
            {
                conn.Close();
            }
            }

  



        private void TryNewAllocation()
        {
           // fillAllocationTable();
            SqlCommand cmd = new SqlCommand("Select *  from tbStudentChoices_New order by Merit1 desc", conn);
            SqlDataReader sReader = null;
            Int32 numberOfRows = 0;
            int rollNo;
            string[] prefs = new string[4];
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
                            prefs[0] = (sReader["Pref1"].ToString());
                            prefs[1] = (sReader["Pref2"].ToString());
                            prefs[2] = (sReader["Pref3"].ToString());
                            prefs[3] = (sReader["Pref4"].ToString());

                            bool choiceAllocated = false;
                            foreach (string pref in prefs)
                            {
                                foreach (KeyValuePair<string, ArrayList> entry in Choices)
                                {
                                    if (entry.Value.Contains(pref)) //get preference  choice seat details
                                    {
                                        if (Convert.ToInt32(entry.Value[3]) < Convert.ToInt32(entry.Value[2])) //if(allocated < seats)
                                        {
                                          //  allocate(pref, rollNo);
                                            entry.Value[3] = Convert.ToInt32(entry.Value[3]) + 1;
                                            choiceAllocated = true;
                                        }
                                    }
                                }
                                if (choiceAllocated)
                                    break;
                                else
                                    continue;
                            }
                        }
                    }

                }
                sReader.Close();
                sReader = cmd.ExecuteReader();
                GridView1.DataSource = sReader;
                GridView1.DataBind();
                TableRow row;
                TableCell cell;
                int seatTotal = 0, allocateTotal = 0;
                foreach (KeyValuePair<string, ArrayList> entry in Choices)
                {
                    row = new TableRow();
                    for (int i = 0; i < 4; i++)
                    {
                        cell = new TableCell();
                        cell.Text = entry.Value[i].ToString();
                        row.Cells.Add(cell);
                        AllocationSummary.Rows.Add(row);
                        if (i == 2)
                            seatTotal += Convert.ToInt32(entry.Value[i]);
                        if (i == 3)
                            allocateTotal += Convert.ToInt32(entry.Value[i]);
                    }
                }
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "<strong>Total</strong>";
                row.Cells.Add(cell);
                row.Cells.Add(new TableCell());//blank cell
                cell = new TableCell();
                cell.Text = seatTotal.ToString();
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = allocateTotal.ToString();
                row.Cells.Add(cell);
                AllocationSummary.Rows.Add(row);
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

        protected void allocate(string choice_allocated, string rollNo)
        {
            SqlCommand cmd_update = new SqlCommand("Update tbGE SET Alloted='" + choice_allocated + "' where classrollNo=" + rollNo, conn);
            int k = cmd_update.ExecuteNonQuery();
            if (k != 0)
            {
            }
        #endregion

        }

        protected void btnViewAllocation_Click(object sender, EventArgs e)
        {
            //s GEAllocation();
          //  TryNewAllocation();
            allocate();
        }
    }
}