using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateGroupDashBoard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   This page is used for locating the buttons on left panel of create group screen.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public partial class CreateGroupDashBoard : eCollection_GroupsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EditGroupId != -1 || EditClassId != -1)
            {
                Backtocreategroupbtn.Text = "CANCEL EDITING GROUP";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Backtocreategroupbtn_Click(object sender, EventArgs e)
        {
            StudentList = null;
            TeachersList = null;
            EditClassId = null;
            EditGroupId = null;
            SelectedID = null;
            SelectedSubscription = null;
            GroupName = null;
            Session["ClassType"] = null;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }
    }
}