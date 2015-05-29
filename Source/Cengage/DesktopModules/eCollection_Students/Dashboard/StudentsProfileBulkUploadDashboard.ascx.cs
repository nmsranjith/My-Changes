using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

namespace DotNetNuke.Modules.eCollection_Students.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsProfileBulkUploadDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel controller class for student bulk upload screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class StudentsProfileBulkUploadDashboard : eCollection_StudentsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

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