using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;

namespace DotNetNuke.Modules.eCollection_Sessions.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionProfileDashBoardMenu" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel for session profile screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SessionProfileDashBoardMenu : eCollection_SessionsModuleBase
    {
        public delegate void PassSessionSelectedValues();
        public static event PassSessionSelectedValues sessionSelected;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              EditSessionButton.NavigateUrl= Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession";
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditSessionButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Globals.NavigateURL(621));       
            Session["SelectedGroups"] = null;
            Session["SelectedProducts"] = null;
            //sessionSelected();
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EndSessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                SessionController _sessionController = SessionController.Instance;
                List<IDCollection> IDCollectionList = new List<IDCollection>();
                if (EditSessionId.HasValue)
                {
                    IDCollectionList.Add(new IDCollection(EditSessionId.Value, ""));
                    //_sessionController.DeleteSession(IDCollectionList,LoginName);
                    _sessionController.UpdateSessionExpiryDate(IDCollectionList, LoginName);
                    _sessionController.ClearAllCaches();
                    eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}