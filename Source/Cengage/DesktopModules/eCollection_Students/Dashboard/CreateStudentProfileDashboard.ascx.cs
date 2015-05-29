using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

namespace DotNetNuke.Modules.eCollection_Students.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateStudentProfileDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel controller class for create student screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateStudentProfileDashboard : eCollection_StudentsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["username"] != null)
                {
                    EditProfileButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?username=" + Request.QueryString["username"].ToString() + "&pagename=" + EDITSTUDENTPROFILE;
                    if (Request.QueryString["bk"] != null && Request.QueryString["bk"] == STUDENTSLIST)
                        BackToProfileButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID);
                    else
                        BackToProfileButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + STUDENTPROFILE + "&username=" + Request.QueryString["username"].ToString();
                }
                //BulkUploadButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename="+STUDENTBULKUPLOAD;
            }
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
        protected void EditProfileButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=editprofile");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToProfileButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=profile");
        }
    }
}