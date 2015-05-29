using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;

namespace DotNetNuke.Modules.eCollection_Sessions.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionDashBoardMenu" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel for session list screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SessionDashBoardMenu : eCollection_SessionsModuleBase
    {
        public delegate void PassSessionSelectedValues();
        public static event PassSessionSelectedValues sessionSelected;
        public static event PassSessionSelectedValues DeleteSession;
        
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLiteralEnd.InnerText = GetMessage(Constants.MESSAGES_END);
            MessageLiteralDelete.InnerHtml = GetMessage(Constants.MESSAGES_DELETE);
            if (!IsPostBack)
                StartReadingSessionButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateSessionButton_Click(object sender, EventArgs e)
        {
            Session["SelectedGroups"] = null;
            Session["SelectedProducts"] = null;
            Session["SelectedSubscriptionId"] = null;
            //SelectedValueTextBox.Text = string.Empty;
            //SelectedValueGroupTextBox.Text = string.Empty;
            Session["EditSelectedId"] = null;
            Session["SessionNotes"] = string.Empty;
            Session["SessionType"] = string.Empty;
            Session["SessionExpiryDate"] = null;
            Session["SessionName"] = string.Empty;
           
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
                //sessionSelected();
                string[] SelectedID = selectedSessionID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                List<IDCollection> IDCollectionList = new List<IDCollection>();
                SessionController _sessionController = SessionController.Instance;
                foreach (string sessionId in SelectedID)
                {
                    //SelectedSessionId.Append(int.Parse((rpItm.FindControl("SessionId") as Label).Text));                   
                    IDCollectionList.Add(new IDCollection(int.Parse(sessionId), ""));
                }
                if (IDCollectionList.Count > 0)
                    _sessionController.UpdateSessionExpiryDate(IDCollectionList, LoginName);
                _sessionController.ClearAllCaches();
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
                // Response.Redirect(Globals.NavigateURL(564));
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteSessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteSession();
                string[] SelectedID = selectedSessionID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                List<IDCollection> IDCollectionList = new List<IDCollection>();
                SessionController _sessionController = SessionController.Instance;
                foreach (string sessionId in SelectedID)
                {
                    //SelectedSessionId.Append(int.Parse((rpItm.FindControl("SessionId") as Label).Text));                   
                    IDCollectionList.Add(new IDCollection(int.Parse(sessionId), ""));
                }
                if (IDCollectionList.Count > 0)
                    _sessionController.DeleteSession(IDCollectionList, LoginName);
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}