using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using System.Configuration;
namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AppDataCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To export the eCollection usage report in Excel.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    
    public partial class AppDataCollection : eCollection_DashboardsModuleBase
    {
        public DataSet dsInfo;
        public DataSet dsAccount;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Messages.ClearMessages();
                var admins=ConfigurationManager.AppSettings["reportmgmtusers"].ToLower().Split(',');
                if (Session["UserName"] != null && admins.Contains(Null.SetNullString(Session["UserName"]).ToLower()))
                {
                    if (!IsPostBack)
                    {
                        DataTable dt = _dashboardController.GetAccountDetails();
                        ddlAccount.DataSource = dt;
                        ddlAccount.DataBind();
                        hdnFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                        hdnTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                else
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
            }
        }

        int chkCnt = 0;
        /// <summary>
        /// Report Generation button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Messages.ClearMessages();
            if (hdnFrom.Value == string.Empty || hdnTo.Value == string.Empty)
            {
                DateValidation.Text = "Please select Valid Date.";
                ScriptManager.RegisterStartupScript(Page, GetType(), "ValidateDateSelection", "<script>ValidateDateSelection()</script>", false);
                return;
            }

            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            if ((from > to))
            {
                DateValidation.Text = "From date should not be greater than To date.";
                ScriptManager.RegisterStartupScript(Page, GetType(), "ValidateDateSelection", "<script>ValidateDateSelection()</script>", false);
                return;
            }
            else
            {
                int accountSk = Convert.ToInt32(ddlAccount.SelectedValue);
                System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("EN-US", true);
                StringWriter sw = new StringWriter(myCItrad);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                Response.Clear();
                if (accountSk == -2)
                {
                    DateValidation.Text = "Please specify the account details.";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "ValidateDateSelection", "<script>ValidateDateSelection()</script>", false);
                    return;
                }
                else if (accountSk == -1)
                {
                    SingleAccountSection.Visible = false;
                    MultipleAccountsSection.Visible = true;
                    AccountSectionMainList.DataSource = _dashboardController.GetAccountDetails();
                    AccountSectionMainList.DataBind();

                    UserSectionList.DataSource = _dashboardController.GetAccountDetails();
                    UserSectionList.DataBind();

                    BooksSectionList.DataSource = _dashboardController.GetAccountDetails();
                    BooksSectionList.DataBind();

                    SessionsSectionList.DataSource = _dashboardController.GetAccountDetails();
                    SessionsSectionList.DataBind();

                    ErrorsSectionList.DataSource = _dashboardController.GetAccountDetails();
                    ErrorsSectionList.DataBind();
                }
                else
                {
                    SingleAccountSection.Visible = true;
                    MultipleAccountsSection.Visible = false;
                    SingleAccountSectionList.DataSource = _dashboardController.GetAccountDetails().AsEnumerable().Where(x => x.Field<int>("ANo") == accountSk).CopyToDataTable();
                    SingleAccountSectionList.DataBind();

                    SingleUserSectionList.DataSource = _dashboardController.GetAccountDetails().AsEnumerable().Where(x => x.Field<int>("ANo") == accountSk).CopyToDataTable();
                    SingleUserSectionList.DataBind();

                    SingleBooksSectionList.DataSource = _dashboardController.GetAccountDetails().AsEnumerable().Where(x => x.Field<int>("ANo") == accountSk).CopyToDataTable();
                    SingleBooksSectionList.DataBind();

                    SingleSessionsSectionList.DataSource = _dashboardController.GetAccountDetails().AsEnumerable().Where(x => x.Field<int>("ANo") == accountSk).CopyToDataTable();
                    SingleSessionsSectionList.DataBind();

                    SingleErrorsSectionList.DataSource = _dashboardController.GetAccountDetails().AsEnumerable().Where(x => x.Field<int>("ANo") == accountSk).CopyToDataTable();
                    SingleErrorsSectionList.DataBind();
                }
                maindiv.RenderControl(hw);
                if (chkCnt > 0)
                {
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=AppData_Report.xls");
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    Response.ContentType = "application/ms-excel";
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "ValidateSelection", "<script>ValidateSelection()</script>", false);
                }
            }
        }

        /// <summary>
        ///  Main ListView databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MainListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Convert.ToInt32((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable();
                if (check.Selected)
                {
                    chkCnt++;
                    dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                    if (check.Text == "Account Info")
                    {
                        ListView accountsListView = (ListView)e.Item.FindControl("AccountsListView");
                        accountsListView.DataSource = dsAccount.Tables[0];
                        accountsListView.DataBind();
                    }
                    else { }
                }
            }
        }

        /// <summary>
        /// Account Section databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AccountsSection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Null.SetNullInteger((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable(); 
                if (check.Text == "Account Info")
                {
                    if (check.Selected)
                    {
                        AccountSectionTable.Visible = true;
                        SingleAccountSectionTable.Visible = true;
                        chkCnt++;
                        dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                        ListView subscriptionsListView1 = (ListView)e.Item.FindControl("SubscriptionsListView1");
                        ListView subscriptionsListView2 = (ListView)e.Item.FindControl("SubscriptionsListView2");
                        for (int i = 1; i < dsAccount.Tables.Count; i++)
                        {
                            if (i == 1)
                            {
                                Label lblNoOfSubscripions = (Label)e.Item.FindControl("lblNoOfSubscripions");
                                lblNoOfSubscripions.Text = dsAccount.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 2)
                            {
                                subscriptionsListView1.DataSource = dsAccount.Tables[i];
                                subscriptionsListView1.DataBind();
                                subscriptionsListView2.DataSource = dsAccount.Tables[i];
                                subscriptionsListView2.DataBind();
                            }
                        }
                    }
                    else
                    {
                        AccountSectionTable.Visible = false;
                        SingleAccountSectionTable.Visible = false;       
                    }
                }
            }
        }

        /// <summary>
        /// User Section databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserSection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Null.SetNullInteger((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable();
                if (check.Text == "User")
                {
                    if (check.Selected)
                    {
                        UserSectionTable.Visible = true;
                        SingleUserSectionTable.Visible = true;
                        chkCnt++;
                        dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                        ListView teachersList1 = (ListView)e.Item.FindControl("TeachersList1");
                        ListView teachersList2 = (ListView)e.Item.FindControl("TeachersList2");
                        ListView studentList1 = (ListView)e.Item.FindControl("StudentList1");
                        ListView studentList2 = (ListView)e.Item.FindControl("StudentList2");
                        for (int i = 0; i < dsInfo.Tables.Count; i++)
                        {
                            if (i == 0)
                            {
                                Label noOfTeachers = (Label)e.Item.FindControl("NoOfTeachers");
                                noOfTeachers.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 1)
                            {
                                Label nooftimesTeacherloggedin = (Label)e.Item.FindControl("NooftimesTeacherloggedin");
                                nooftimesTeacherloggedin.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 2)
                            {
                                teachersList1.DataSource = dsInfo.Tables[i];
                                teachersList1.DataBind();
                                teachersList2.DataSource = dsInfo.Tables[i];
                                teachersList2.DataBind();
                            }
                            else if (i == 3)
                            {
                                Label noOfStudents = (Label)e.Item.FindControl("NoOfStudents");
                                noOfStudents.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 4)
                            {
                                Label nooftimesStudentloggedin = (Label)e.Item.FindControl("NooftimesStudentloggedin");
                                nooftimesStudentloggedin.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else
                            {
                                studentList1.DataSource = dsInfo.Tables[i];
                                studentList1.DataBind();
                                studentList2.DataSource = dsInfo.Tables[i];
                                studentList2.DataBind();
                            }
                        }
                    }
                    else
                    {                                                   
                        UserSectionTable.Visible = false;
                        SingleUserSectionTable.Visible = false;            
                    }
                }
            }
        }

        /// <summary>
        /// Books Section databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BooksSection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Null.SetNullInteger((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable();
                if (check.Text == "Books")
                {
                    if (check.Selected)
                    {
                        BooksSectionTable.Visible = true;
                        SingleBooksSectionTable.Visible = true;
                        chkCnt++;
                        dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                        for (int i = 0; i < dsInfo.Tables.Count; i++)
                        {
                            if (i == 0)
                            {
                                Label noOfbooksread = (Label)e.Item.FindControl("NoOfbooksread");
                                noOfbooksread.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 1)
                            {
                                Label numberofGuidedProfilebooks = (Label)e.Item.FindControl("NumberofGuidedProfilebooks");
                                numberofGuidedProfilebooks.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 2)
                            {
                                Label numberofIndependentProfilebooks = (Label)e.Item.FindControl("NumberofIndependentProfilebooks");
                                numberofIndependentProfilebooks.Text = dsInfo.Tables[i].Rows[0][0].ToString();

                            }

                            else if (i == 3)
                            {
                                ListView favouriteBooksList = (ListView)e.Item.FindControl("FavouriteBooksList");
                                favouriteBooksList.DataSource = dsInfo.Tables[i];
                                favouriteBooksList.DataBind();
                            }
                            else
                            {
                                ListView readingLevelList = (ListView)e.Item.FindControl("ReadingLevelList");
                                readingLevelList.DataSource = dsInfo.Tables[i];
                                readingLevelList.DataBind();
                            }
                        }
                    }
                    else
                    {
                        BooksSectionTable.Visible = false;
                        SingleBooksSectionTable.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        ///  Sessions Section databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SessionsSection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Null.SetNullInteger((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable();

                if (check.Text == "Reading Session")
                {
                    if (check.Selected)
                    {
                        SessionsSectionTable.Visible = true;
                        SingleSessionsSectionTable.Visible = true;
                        chkCnt++;
                        dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                        for (int i = 0; i < dsInfo.Tables.Count; i++)
                        {
                            if (i == 0)
                            {
                                Label numberofReadingSessions = (Label)e.Item.FindControl("NumberofReadingSessions");
                                numberofReadingSessions.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 1)
                            {
                                Label numberofGuidedProfilereadingsessions = (Label)e.Item.FindControl("NumberofGuidedProfilereadingsessions");
                                numberofGuidedProfilereadingsessions.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 2)
                            {
                                Label numberofIndependentProfilereadingsessions = (Label)e.Item.FindControl("NumberofIndependentProfilereadingsessions");
                                numberofIndependentProfilereadingsessions.Text = dsInfo.Tables[i].Rows[0][0].ToString();

                            }
                            else if (i == 3)
                            {
                                Label numberofCompletedReadingSessions = (Label)e.Item.FindControl("NumberofCompletedReadingSessions");
                                numberofCompletedReadingSessions.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 4)
                            {
                                Label numberofNotesforallReadingSessions = (Label)e.Item.FindControl("NumberofNotesforallReadingSessions");
                                numberofNotesforallReadingSessions.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 5)
                            {
                                Label numberofStudentsunderallReadingSession = (Label)e.Item.FindControl("NumberofStudentsunderallReadingSession");
                                numberofStudentsunderallReadingSession.Text = dsInfo.Tables[i].Rows[0][0].ToString();
                            }
                            else if (i == 6)
                            {
                                ListView sessionsList1 = (ListView)e.Item.FindControl("SessionsList1");
                                ListView sessionsList2 = (ListView)e.Item.FindControl("SessionsList2");
                                ListView sessionsList3 = (ListView)e.Item.FindControl("SessionsList3");
                                sessionsList1.DataSource = dsInfo.Tables[i];
                                sessionsList1.DataBind();
                                sessionsList2.DataSource = dsInfo.Tables[i];
                                sessionsList2.DataBind();
                                sessionsList3.DataSource = dsInfo.Tables[i];
                                sessionsList3.DataBind();
                            }
                        }
                    }
                    else
                    {
                        SessionsSectionTable.Visible = false;
                        SingleSessionsSectionTable.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Error Section databinding event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ErrorsSection_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            int accountSk = Null.SetNullInteger((sender as ListView).DataKeys[e.Item.DataItemIndex].Value);
            DataTable dtInfo;
            dsInfo = new DataSet();
            dsAccount = new DataSet();
            DateTime from = DateTime.ParseExact(hdnFrom.Value, "dd/MM/yyyy", null);
            DateTime to = DateTime.ParseExact(hdnTo.Value, "dd/MM/yyyy", null);
            dsAccount = _dashboardController.GetAccountInformation(accountSk, from, to);
            foreach (ListItem check in ckblInfo.Items)
            {
                dtInfo = new DataTable();
                if (check.Text == "Error Log")
                {
                    if (check.Selected)
                    {
                        ErrorsSectionTable.Visible = true;
                        SingleErrorsSectionTable.Visible = true;
                        chkCnt++;
                        dsInfo = _dashboardController.GetReportInfo(from, to, accountSk, check.Text);

                        Label errorLogCountLabel = (Label)e.Item.FindControl("ErrorLogCountLabel");
                        errorLogCountLabel.Text = dsInfo.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        ErrorsSectionTable.Visible = false;
                        SingleErrorsSectionTable.Visible = false;
                    }
                }
            }
        }
    }
}