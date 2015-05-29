using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Data;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupProfileDashBoard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel controller class for group profile.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupProfileDashBoard : eCollection_GroupsModuleBase
    {
        int? groupId = null;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    EditGroupButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + EDITGROUP;
                }

                if (EditClassId.Value != -1)
                {
                    groupId = EditClassId.Value;
                }
                else if (EditGroupId.Value != -1)
                {
                    groupId = EditGroupId.Value;
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
                }
                DataTable groupStudent = GroupController.Instance.GetGroupNameByGroupId(groupId.Value, "Student");
                if (groupStudent.Rows.Count == 0)
                {
                    PrintCredentialButton.Enabled = false;
                    PrintCredentialButton.CssClass = "DbldPrintBtn startreadingsesionbtn";
                    PrintCredentialButtonDiv.Attributes.Add("class", "DisabledAddButtonHolder");
                }
            }
            catch (Exception ex) {LogFileWrite(ex);}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditGroupButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + EDITGROUP);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PrintCredentialButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupId.HasValue)
                {

                    DataTable groupStudent = GroupController.Instance.GetGroupNameByGroupId(groupId.Value, "Student");
                    if (groupStudent.Rows.Count > 0)
                    {
                        PrintStudentCard(groupStudent);
                    }

                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }

    }
}