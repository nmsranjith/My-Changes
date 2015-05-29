using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;

namespace DotNetNuke.Modules.eCollection_Sessions.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateSessionDashBoardMenu" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel for create session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateSessionDashBoardMenu : eCollection_SessionsModuleBase
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
            Session["SelectedGroups"] = null;
            Session["SelectedProducts"] = null;
            Session["EditSelectedId"] = null;
            if (Request.QueryString["returnurl"] != null)
            {
                if (Request.QueryString["returnurl"].Equals("students"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(StudentsModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("GROUPS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(GroupsModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("TEACHERS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(TeachersModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("BOOKS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(BooksModule)));
                }
            }

            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }
    }
}