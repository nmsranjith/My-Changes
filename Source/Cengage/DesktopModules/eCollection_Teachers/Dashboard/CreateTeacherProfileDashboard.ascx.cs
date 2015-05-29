using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

namespace DotNetNuke.Modules.eCollection_Teachers.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateTeacherProfileDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   Left panel for create teacher profile screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateTeacherProfileDashboard :eCollection_TeachersModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BulkUploadButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename="+TEACHERPROFILEBULKUPLOAD;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BulkUploadButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=bulkupload");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Backtocreategroupbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }
    }
}