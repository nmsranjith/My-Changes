using System;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common.Utilities;
using System.Data;
using System.Linq;
using System.Web.UI;
using DotNetNuke.Instrumentation;
namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SubscriptionDetails" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Displays the subscription details only for the subscription admin at the top of the dashboard screen and hides for teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SubscriptionDetails : eCollection_DashboardsModuleBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri);
                    var subst1 = (from subs in _dashboardController.GetSubscriptionsList(new Users()
                    {
                        UserLoginName = Session["UserName"].ToString(),
                        Active = 'Y'
                    }).AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == Null.SetNullInteger(Session["Subscription"]))
                                  select subs.Field<string>("remiderdate")).ToList().Distinct();

                    DnnLog.Error("gnana__" + subst1.ToList()[0].ToLower());

                    if (subst1.ToList()[0].ToLower().Trim() == "renewnotdisplay")
                    {
                        renewDiv.Style.Add("display", "none");
                        upgradeDivEcoll.Style.Add("display", "block");
                    }
                    else
                    {
                        renewDiv.Style.Add("display", "block");
                        upgradeDivEcoll.Style.Add("display", "none");
                    }

                    var Expiredate = (from subs in _dashboardController.GetSubscriptionsList(new Users()
                    {
                        UserLoginName = Session["UserName"].ToString(),
                        Active = 'Y'
                    }).AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == Null.SetNullInteger(Session["Subscription"]))
                                  select subs.Field<string>("EndDate")).ToList().Distinct();

                    expdate.Text = "Subscription Expires: " + Expiredate.ToList()[0].ToLower().Trim();
                    string[] nextyear;
                    nextyear =  Expiredate.ToList()[0].ToLower().Trim().ToString().Split('/');
                    int year = Convert.ToInt16(nextyear[2]);
                    year++;
                    btnRenew.Text = "Renew for " + year;

                    if (subValidation[3] == 1)
                    {
                        if (Session["Subscription"] != null)
                            parentsubsid.Value = Session["Subscription"].ToString();
                        UpgradeLink.Visible = false;
                        UpgradeTrialLink.Visible = true;
                        //DnnLog.Error("gnana__" + subst1.ToList()[0].ToLower());
                    }                    
                    else
                    {
                        UpgradeLink.Visible = true;
                        UpgradeTrialLink.Visible = false;
                        
                   

                        var subst = (from subs in _dashboardController.GetSubscriptionsList(new Users()
                        {
                            UserLoginName = Session["UserName"].ToString(),
                            Active = 'Y'
                        }).AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == Null.SetNullInteger(Session["Subscription"]))
                        select subs.Field<string>("SubscriptionType")).ToList().Distinct();

                        if (subst.ToList()[0].ToLower() == "personal subscriptions")
                            UpgradeLink.HRef = string.Concat("/subscription.aspx?pagename=upgradesubscription&custsubsk=", Null.SetNullInteger(Session["Subscription"]), "&tab=1");
                        else
                            UpgradeLink.HRef = string.Concat("/subscription.aspx?pagename=upgradesubscription&custsubsk=", Null.SetNullInteger(Session["Subscription"]), "&tab=2");
                   
                    }

                    FillSubscriptionDetails();
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }

        protected void btnRenew_Click(object sender, EventArgs e)
        {
            Session["eCollRenewSubsSk"] = Session["Subscription"].ToString();
            renewPanePlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/Subscriptions/Views/eCollectionRenew.ascx"));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "popupNonTrialrenew", "ecollpopuprenewal(1);", true);
        }

        #region Member functions
        /// <summary>
        /// Fill the Subscription Details
        /// </summary>
        protected void FillSubscriptionDetails()
        {
            try
            {
                //Get Subscription details              
                Subscription subsDetails = _dashboardController.SubscriptionDetails(new Users() { SubscriptionId = Null.SetNullInteger(Session["Subscription"]), UserLoginName = LoginName });               
                if (subsDetails == null)
                    subsDetails = new Subscription() { TotalLicenses = 0, AvailableLicenses = 0, BooksAdded = 0, DaysLeft = 0, GraceDaysLeft = 0 };
                TotalLicenses.InnerText = subsDetails.TotalLicenses.ToString();
                UsedLicenses.InnerText = subsDetails.AvailableLicenses.ToString();
                AddedBooks.InnerText = subsDetails.BooksAdded.ToString();
                DaysLeft.InnerText = subsDetails.DaysLeft.ToString();
                Session["GraceDaysLeft"] = subsDetails.GraceDaysLeft;
                Subscription subGetStarted = _dashboardController.GetStartedDetails(int.Parse(Session["Subscription"].ToString()));
                if (subGetStarted.TotalBooks == subGetStarted.BooksAdded)
                {
                    DaysLeftDiv.Attributes.Add("Class", "zeroheight");
                }
                else
                {
                    if (subsDetails.GraceDaysLeft > 0)
                    {
                        DaysLeftDiv.Visible = true;
                        GraceDaysLeft.InnerText = subsDetails.GraceDaysLeft.ToString();
                    }
                    else
                    {
                        DaysLeftDiv.Attributes.Add("Class", "zeroheight");
                    }
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }
        #endregion
    }
}