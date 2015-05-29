using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="UpgradeMainScreen" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Provides the first page of Upgrade trail wizard where user can skip or continue the upgrade process
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class UpgradeMainScreen : eCollection_DashboardsModuleBase 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FreshStartBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] != null && Session["Subscription"] != null)
                {
                    UpgradeFlags flags = new UpgradeFlags();
                    flags.TeacherFlag = 'N';
                    flags.StudentFlag = 'N';
                    flags.GroupsFlag = 'N';
                    flags.BooksFlag = 'N';
                    flags.UpgradeYearLevel = 'N';
                    flags.FromMainScreen = 'Y';
                    flags.UserName = Session["UserName"].ToString();
                    flags.CustSubsSk = Convert.ToInt32(Session["Subscription"].ToString());
                    Session["UpgradeFlagList"] = flags;
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradesteptwo");
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpgradeWizardBtn_Click(object sender, EventArgs e)
        {
            Session["UpgradeFlagList"] = null;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradestepone");
        }
    }
}