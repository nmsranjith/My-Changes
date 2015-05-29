using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Dashboards.Components;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="UpgradeStepOne" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Provides the step one of Upgrade trail wizard
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class UpgradeStepOne :eCollection_DashboardsModuleBase     
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
                renewupgradehdn.Value=Session["renewupgradetype"].ToString();
                if (Session["renewupgradetype"].ToString() == "renew")
                {
                    MOveAllStudentDiv.Style.Add("display", "block");
                   // GrpsCheckDiv.Style.Add("display", "none");
                    //GroupsCheck.Checked = false;
                    MOveAllStudent.Checked = true;
                }
                else if (Session["renewupgradetype"].ToString() == "upgrade")
                {
                    MOveAllStudentDiv.Style.Add("display", "none");
                   // GrpsCheckDiv.Style.Add("display", "block");
                   // GroupsCheck.Checked = true;
                    MOveAllStudent.Checked = false;
                }
                else { }
                if (Session["UpgradeFlagList"] != null)
                {
                    UpgradeFlags Flags = new UpgradeFlags();
                    Flags = Session["UpgradeFlagList"] as UpgradeFlags;
                    BackClickhdn.Value = Flags.TeacherFlag.ToString() + '-' + Flags.StudentFlag.ToString() + '-' + Flags.GroupsFlag.ToString() + '-' + Flags.BooksFlag.ToString()+'-'+Flags.UpgradeYearLevel.ToString();
                }
                IsLicLesshdn.Value = subValidation[1].ToString();
                IsQtyLesshdn.Value = subValidation[2].ToString();
            }
            catch (Exception ex){LogFileWrite(ex);}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradesteps");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContinueStepsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UpgradeFlags flags = new UpgradeFlags();
                string[] fg = Upgradeflaghdn.Value.Split('-');
                flags.TeacherFlag = char.Parse(fg[0]);
                flags.StudentFlag = char.Parse(fg[1]);
                flags.GroupsFlag = char.Parse(fg[2]);
                flags.BooksFlag = char.Parse(fg[3]);
                flags.UpgradeYearLevel = char.Parse(fg[4]);
                flags.FromMainScreen = 'N';
                Session["UpgradeFlagList"] = flags;
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=upgradesteptwo");
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}