using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Instrumentation;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Dashboards.Components;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="UpgradeStep2" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Provides the step two of Upgrade trail wizard
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class UpgradeStep2 : eCollection_DashboardsModuleBase 
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
                renewupgradehdn1.Value = Session["renewupgradetype"].ToString();
                if (Session["UpgradeFlagList"] != null)
                {
                    UpgradeFlags flags = new UpgradeFlags();
                    flags = Session["UpgradeFlagList"] as UpgradeFlags;
                    upgradeFlaghdn.Value = flags.TeacherFlag.ToString() + '-' + flags.StudentFlag.ToString() + '-' + flags.GroupsFlag.ToString() + '-' + flags.BooksFlag.ToString()+'-'+flags.UpgradeYearLevel.ToString();
                }
                if (Session["renewupgradetype"].ToString() == "renew")
                {
                    UpgradeFlags flags = new UpgradeFlags();
                    flags = Session["UpgradeFlagList"] as UpgradeFlags;
                    if (flags.UpgradeYearLevel.ToString() == "Y")
                    {
                        MoveStudArchDiv.Style.Add("display", "none");
                        MoveStudDiv.Style.Add("display", "block");
                        
                    }
                    else
                    {
                        MoveStudDiv.Style.Add("display", "none");
                        MoveStudArchDiv.Style.Add("display", "block");
                    }
                }
                else if (Session["renewupgradetype"].ToString() == "upgrade")
                {
                    MoveStudDiv.Style.Add("display", "none");
                    MoveStudArchDiv.Style.Add("display", "none");
                }
                else { }
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
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UpgradeFlagList"] != null)
                {
                    UpgradeFlags flags = new UpgradeFlags();
                    flags = Session["UpgradeFlagList"] as UpgradeFlags;
                    if (flags.FromMainScreen == 'N')
                        Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradestepone");
                    else Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradesteps");

                }
            }
            catch (Exception ex){LogFileWrite(ex);}
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FinishStepsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] != null && Session["Subscription"] != null)
                {
                    
                    if (renewupgradehdn1.Value == "renew")
                    {
                        UpgradeFlags flags = new UpgradeFlags();
                        flags = Session["UpgradeFlagList"] as UpgradeFlags;
                        flags.UserName = Session["UserName"].ToString();
                        flags.CustSubsSk = Convert.ToInt32(Session["Subscription"].ToString());
                        DnnLog.Error("andrew renew proceed__" + upgradeFlaghdn.Value + "UserName" + flags.UserName + "CustSubsSk" + flags.CustSubsSk);
                        flags.ActionType = renewupgradehdn1.Value;
                        if (_dashboardController.MigrateRenewalSubscription(flags))
                        {
                            DnnLog.Error("andrew renew completed");
                            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=dashboard");
                        }
                        else
                            DnnLog.Error("andrew not renew proceed");

                    }
                    else
                    {
                        UpgradeFlags flags = new UpgradeFlags();
                        flags = Session["UpgradeFlagList"] as UpgradeFlags;
                        flags.UserName = Session["UserName"].ToString();
                        string[] str = upgradeFlaghdn.Value.Split('-');
                        flags.UpgradeYearLevel = str.Length >= 9 ? Convert.ToChar(str[4].ToString()) : 'N';
                        flags.ActionType = renewupgradehdn1.Value;
                        flags.CustSubsSk = Convert.ToInt32(Session["Subscription"].ToString());


                        if (_dashboardController.UpgradeSubscription(flags))
                        {

                            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=dashboard");
                        }
                    }
                    
                }
                else
                {
                    
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
                }
            }
            catch (Exception ex)
            {
                
                LogFileWrite(ex);
            }          
        }
    }
}