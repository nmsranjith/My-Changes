using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Linq;
using System.Data;

namespace DotNetNuke.Modules.eCollection_Dashboards.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="LeftDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A class for after renewal popup screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class LeftDashboard : eCollection_DashboardsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Null.SetNullString(Request.QueryString["pagename"]).ToLower() == "ecollectionreport")
                {
                    BackToDashboard.HRef = Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + DASHBOARD;
                    BackButton.Visible = true;
                    RenewalButton.Visible = false;
                    return;
                }
                else
                {
                    BackButton.Visible = false;
                }
                if ((StaffDetail != null && Session["UserName"].ToString().ToLower()=="bbolton") || Allrenewels == null)
                {
                    RenewalButton.Visible = true;
                }
                if (Allrenewels != null && Allrenewels.Count>0)
                {
                    var subst = (from subs in _dashboardController.GetSubscriptionsList(new Users()
                    {
                        UserLoginName = Session["UserName"].ToString(),
                        Active = 'Y'
                    }).AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == Null.SetNullInteger(Session["Subscription"]))
                                 select subs.Field<string>("SubscriptionType")).ToList().Distinct();

                    if (subst.ToList()[0].ToLower() == "personal subscriptions")
                        AdvancedSetupLink.HRef = string.Concat("/subscription.aspx?pagename=managelicense&custsubsk=", Null.SetNullInteger(Session["Subscription"]), "&tab=1");
                    else
                        AdvancedSetupLink.HRef = string.Concat("/subscription.aspx?pagename=managelicense&custsubsk=", Null.SetNullInteger(Session["Subscription"]), "&tab=2");                   

                    ScriptManager.RegisterStartupScript(Page, GetType(), "AfterRenewal", "<script>Open()</script>", false);
                    AfterRenewelYear.Text = DateTime.Now.Year.ToString();
                    StartButton.Text = "START " + DateTime.Now.Year.ToString();
                    //if (renewalSubscriptionName.Trim().Length > 0)
                    //{
                    if (OLD_CUST_SUBS_SK_NAME.Trim().Length == 0)
                    {
                        if (Allrenewels.Count > 0)
                        {
                            lblSubscriptionName.Text = Allrenewels[RENEWELCOUNT].OLD_SUBS_NAME.ToString();
                            NEW_CUST_SUBS_SK = Allrenewels[RENEWELCOUNT].NEW_CUST_SUBS_SK;
                            OLD_CUST_SUBS_SK = Allrenewels[RENEWELCOUNT].OLD_CUST_SUBS_SK;
                        }
                    }
                    //}
                    if (RenewalLicenseCount > 0)
                    {
                        licencesCount.Text = "(you have " + RenewalLicenseCount + " more students than licences)";
                    }
                    else
                    {
                        licencesCount.Text = string.Empty;
                    }
                    if (RenewalBookCount > 0)
                    {
                        BooksRenewel.Visible = false;
                        CheckStart.Checked = false;
                    }
                    else
                    {
                        BooksRenewel.Visible = true;
                        CheckStart.Checked = true;
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkStartButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StartButton_Click(object sender, EventArgs e)
        {
            DashboardController dashBoardController = DashboardController.Instance;

            if (CheckAllTeachers.Checked)
            {
                try
                {
                    dashBoardController.StartRenewal(LoginName, "Move_Teacher_Profile", NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex) { LogFileWrite(ex); }
            }

            if (CheckAllStudents.Checked)
            {
                try
                {
                    dashBoardController.StartRenewal(LoginName, "Student_Profile", NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex) { LogFileWrite(ex); }
            }

            if (CheckMoveall.Checked)
            {
                try
                {
                    dashBoardController.StartRenewal(LoginName, "Up_One_Year_Level", NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex) { LogFileWrite(ex); }
            }

            if (CheckStart.Checked)
            {
                try
                {
                    dashBoardController.StartRenewal(LoginName, "Book_Renewel", NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex) { LogFileWrite(ex); }
            }

            try
            {
                dashBoardController.UpdateInActiveAndArchive(LoginName, NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
            }
            catch (Exception ex) { LogFileWrite(ex); }

            RENEWELCOUNT += 1;
            if (RENEWELCOUNT < Allrenewels.Count)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "setPopupFlag", "setPopupFlag();", true);
                OLD_CUST_SUBS_SK_NAME = Allrenewels[RENEWELCOUNT].OLD_SUBS_NAME.ToString();
                NEW_CUST_SUBS_SK = Allrenewels[RENEWELCOUNT].NEW_CUST_SUBS_SK;
                OLD_CUST_SUBS_SK = Allrenewels[RENEWELCOUNT].OLD_CUST_SUBS_SK;
                lblSubscriptionName.Text = OLD_CUST_SUBS_SK_NAME;
                Session["AddedStudentList"] = null;
                Session["AddedStudentCount"] = null;
            }
            else
            {
                RENEWELCOUNT = 0;
                NEW_CUST_SUBS_SK = 0;
                OLD_CUST_SUBS_SK = 0;
                OLD_CUST_SUBS_SK_NAME = string.Empty;
                dashBoardController.ClearAllCache();
                Session["Subscription"] = null;
                Session["AddedStudentList"] = null;
                Session["AddedStudentCount"] = null;
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
            }
        }
    }

}