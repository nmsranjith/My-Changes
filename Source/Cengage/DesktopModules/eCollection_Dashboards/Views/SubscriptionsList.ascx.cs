using System;
using DotNetNuke.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Common;
namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SubscriptionsList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Displays all the personal and school subscriptions of current academic year and future inactive subscriptions.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SubscriptionsList : eCollection_DashboardsModuleBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillSubscription();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void FillSubscription()
        {
            try
            {
                if (SubsList.Rows.Count == 1)
                    SubsHeadingid.Visible = false;
                else
                    SubsHeadingid.Visible = true;
                var subst=(from subs in _dashboardController.GetSubscriptionsList(new Users()
                                            {
                                                UserLoginName = Session["UserName"].ToString(),
                                                Active = 'Y'
                                            }).AsEnumerable()
                           select subs.Field<string>("SubscriptionType")).ToList().Distinct();              
                List<Subscription> subsHeaderList = new List<Subscription>();
                subst.ToList().ForEach(x => subsHeaderList.Add(new Subscription() { SubscriptionType = x }));               

                if (subsHeaderList.Count == 0)
                {
                    SubstopDiv.Style.Add("Display", "none");
                    MessageOuterDiv.Style.Add("Display", "block");
                    Message1.Text = Constants.NOSUBSCRIPTIONSINFO;
                    CreateLink.NavigateUrl = (new Uri(Request.Url.AbsoluteUri)).Host;
                }
                else
                {                    
                    MessageOuterDiv.Style.Add("Display", "none");
                    SubscriptionTypeRptr.DataSource = subsHeaderList;
                    SubscriptionTypeRptr.DataBind();
                }

            }
            catch (Exception ex) { //LogFileWrite(ex); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UseButton_Click(object sender, EventArgs e)
        {
            try
            {
                _dashboardController.ClearAllCache();
                Session["Subscription"] = (sender as Button).CommandArgument;
                Session["AddedStudentList"] = null;
                Session["AddedStudentCount"] = null;
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + DASHBOARD);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubscriptionTypeRptr_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            try
            {
                Repeater subsListRptr = e.Item.FindControl("SubsListRptr") as Repeater;
                HtmlGenericControl subsHeader = e.Item.FindControl("SubscriptionType") as HtmlGenericControl;               
                EnumerableRowCollection<DataRow> query =  _dashboardController.GetSubscriptionsList(new Users()
                                            {
                                                UserLoginName = Session["UserName"].ToString(),
                                                Active = 'Y'
                                            }).AsEnumerable().Where(x => x.Field<string>("SubscriptionType") == subsHeader.InnerText.Trim());    
                subsListRptr.DataSource = query.AsDataView();                                        
                subsListRptr.DataBind();
            }
            catch(Exception ex){LogFileWrite(ex);}
        }

        protected void SubsListRptr_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName == manageTag)
            //    NavigateToView(MANAGELICENSE_VIEW);

            //Repeater SubscriptionTypeRptr1 = (Repeater)e.Item.FindControl("SubscriptionTypeRptr");
            
            if (e.CommandName == "RENEW")
            {
                Repeater SubsListRptr1 = (Repeater)e.Item.FindControl("SubsListRptr");
                HiddenField custsubsskhdn1 =  (HiddenField)e.Item.FindControl("custsubsskhdn");

                Session["eCollRenewSubsSk"] = custsubsskhdn1.Value;
                renewPanePlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/Subscriptions/Views/eCollectionRenew.ascx"));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "popupNonTrialrenew", "ecollpopuprenewal(1);", true);
            }

            //if (e.CommandName == visitTag)
            //    NavigateToView(string.Empty);

        }
    }
}