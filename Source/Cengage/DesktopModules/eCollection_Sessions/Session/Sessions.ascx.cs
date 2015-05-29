using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Sessions.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Services.Exceptions;
using _Sessions = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using DotNetNuke.Modules.eCollection_Sessions.Dashboard;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Common.Utilities;


namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Sessions" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Sessions List Screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class Sessions : eCollection_SessionsModuleBase
    {
        #region Private Members

        private List<_Sessions> _activeSessionList;
        private List<_Sessions> _finishedSessionList;
        private List<_Sessions>  _archievedSessionList;

        private SortDirection SessionsSortDirection
        {
            get
            {
                if (Session["SessionsSortDirection"] == null)
                    Session["SessionsSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["SessionsSortDirection"];
            }
            set { Session["SessionsSortDirection"] = value; }
        }
        
        private string SessionsSortExpression
        {
            get
            {
                if (Session["SessionsSortExpression"] == null)
                    Session["SessionsSortExpression"] = "Name";
                return (string)Session["SessionsSortExpression"];
            }
            set { Session["SessionsSortExpression"] = value; }
        }

       
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassProfileButton_Click(object sender, EventArgs e)
        {
            //GetSelectedSession();
            try
            {
                var argValue = ((Button)sender).CommandArgument;
                Button button = (sender as Button);
                //Get the command argument
                EditSessionId = Convert.ToInt32(button.CommandArgument);             
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                LogFileWrite(exc); 
            }
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=sessionprofile");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassProfileButton2_Click(object sender, EventArgs e)
        {
            try
            {
                var argValue = ((Button)sender).CommandArgument;
                Button button = (sender as Button);
                //Get the command argument
                EditSessionId = Convert.ToInt32(button.CommandArgument);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                LogFileWrite(exc); 
            }
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=sessionprofile"); ;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               

                if (!Page.IsPostBack)
                {
                    Session["SessionsSortDirection"] = null;
                    Session["SessionsSortExpression"] = null;
                    Session["StartSessionId"] = null;
                    Session["EditSelectedId"] = null;
                    string GetAllActiveSessionsCacheKey = string.Format("GetAllActiveSessions{0}", UserId);
                    string GetAllFinishedSessionsCacheKey = string.Format("GetAllFinishedSessions{0}", UserId);
                    string GetAllArchivedSessionsCacheKey = string.Format("GetAllArchivedSessions{0}", UserId);
                    string GetAllStudentList = string.Format("GetStudentsList{0}", SelectedSubscriptionId);
                    string cacheKey = string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId);
                    DataCache.RemoveCache(GetAllActiveSessionsCacheKey);
                    DataCache.RemoveCache(GetAllFinishedSessionsCacheKey);
                    DataCache.RemoveCache(GetAllArchivedSessionsCacheKey);
                    DataCache.RemoveCache(GetAllStudentList);
                    DataCache.RemoveCache(string.Format("GetGroups{0}", (char)MyEnums.GroupType.N));
                    DataCache.RemoveCache(string.Format("GetGroups{0}", (char)MyEnums.GroupType.C));
                    DataCache.RemoveCache(string.Format("GetTeachers{0}", SelectedSubscriptionId));
                    DataCache.RemoveCache(string.Format("GetSearchedStudentsList{0}", SelectedSubscriptionId));  
                    DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));                    
                    DataCache.RemoveCache(cacheKey);
                    FillActiveSessionsList(ActiveSessions.OrderBy(u => u.Name).ToList());
                    lblActiveSessionCount.Text = ActiveSessions.Count.ToString();
                    lblFinishedSessionCount.Text = FinishedSessions.Count.ToString();
                    lblArchivedSessionsCount.Text = (ArchivedSessions.Count).ToString();
                    ActiveSessionList.DataSource = ActiveSessions;
                    ActiveSessionList.DataBind();
                    FinishedSessionList.DataSource = FinishedSessions;
                    FinishedSessionList.DataBind();
                    ArchivedSessionList.DataSource = ArchivedSessions;
                    ArchivedSessionList.DataBind();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideActivePanel", "<script>$('#ActiveSessionUpdatePanel').css('display','none')</script>", false);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideFinishedPanel", "<script>$('#FinishedSessionUpdatePanel').css('display','none')</script>", false);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideArchPanel", "<script>$('#ArchivedSessionUpdatePanel').css('display','none')</script>", false);                        
                    if (ActiveSessions.Count == 0 && ArchivedSessions.Count == 0 && FinishedSessions.Count == 0)
                    {
                        MessageOuterDiv.Style.Add("Display", "block");
                        SessionListDiv.Style.Add("Display", "none");
                        Message1.Text = Constants.NOSESSIONINFO;
                        CreateLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession";
                    }
                    else
                    {
                        MessageOuterDiv.Style.Add("Display", "none");
                        SessionListDiv.Style.Add("Display", "block");
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                LogFileWrite(exc); 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesAscButton_Click(object sender, EventArgs e)
        {
            NamesAscButton.Visible = false;
            NamesDescButton.Visible = true;
            SessionsSortExpression = "Name";
            SessionsSortDirection = SortDirection.Descending;            
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesDescButton_Click(object sender, EventArgs e)
        {
            NamesAscButton.Visible = true;
            NamesDescButton.Visible = false;
            SessionsSortExpression = "Name";
            SessionsSortDirection = SortDirection.Ascending;            
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DateCreatedAscButton_Click(object sender, EventArgs e)
        {
            DateCreatedAscButton.Visible = false;
            DateCreatedDescButton.Visible = true;
            SessionsSortExpression = "SessionCreatedDate";
            SessionsSortDirection = SortDirection.Descending;
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DateCreatedDescButton_Click(object sender, EventArgs e)
        {
            DateCreatedAscButton.Visible = true;
            DateCreatedDescButton.Visible = false;
            SessionsSortExpression = "SessionCreatedDate";
            SessionsSortDirection = SortDirection.Ascending;
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindUpdatePanel()
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            SessionController.Instance.GetNamesForSearch(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), UserId, LoginName,SelectedSubscriptionId);
            FillActiveSessionsList(ActiveSessions);
            FillFinishedSessionsList(FinishedSessions);
            lblActiveSessionCount.Text = ActiveSessions.Count.ToString();
            FillArchivedSessionsList(ArchivedSessions);
            lblFinishedSessionCount.Text = FinishedSessions.Count.ToString();
            lblArchivedSessionsCount.Text = (ArchivedSessions.Count).ToString();
            SortAndBind();
           
            ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);

        }

        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_AllSessions"></param>      
        private void FillActiveSessionsList(List<_Sessions> _AllSessions)
        {
            if (_AllSessions != null && _AllSessions.Count > 0)
            {             
                ActiveSessionList.DataSource = _AllSessions;
                ActiveSessionList.DataBind();                
            }
            else
            {
                ActiveSessionList.DataSource = null;
                ActiveSessionList.DataBind();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_AllSessions"></param>
        private void FillFinishedSessionsList(List<_Sessions> _AllSessions)
        {
            if (_AllSessions != null && _AllSessions.Count > 0)
            {
                FinishedSessionList.DataSource = _AllSessions;
                FinishedSessionList.DataBind();
            }
            else
            {
                FinishedSessionList.DataSource = null;
                FinishedSessionList.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_AllSessions"></param>
        private void FillArchivedSessionsList(List<_Sessions> _AllSessions)
        {
            if (_AllSessions != null && _AllSessions.Count > 0)
            {
                ArchivedSessionList.DataSource = _AllSessions;
                ArchivedSessionList.DataBind();
            }
            else
            {
                ArchivedSessionList.DataSource = null;
                ArchivedSessionList.DataBind();
            }
        }    

        /// <summary>
        /// 
        /// </summary>
        private void SortAndBind()
        {
            try
            {
                _activeSessionList = ActiveSessions;
                _finishedSessionList = FinishedSessions;
                _archievedSessionList = ArchivedSessions;

                switch (SessionsSortExpression)
                {
                    case "Name":
                        _activeSessionList.Sort();
                        _finishedSessionList.Sort();
                        _archievedSessionList.Sort();
                        break;
                    case "SessionCreatedDate":
                        _activeSessionList.Sort(_Sessions.SessionCreatedDateSorter);
                        _finishedSessionList.Sort(_Sessions.SessionCreatedDateSorter);
                        _archievedSessionList.Sort(_Sessions.SessionCreatedDateSorter);
                        break;
                    default:
                        break;
                }

                if (SessionsSortDirection == SortDirection.Descending)
                {
                    _activeSessionList.Reverse();
                    _finishedSessionList.Reverse();
                    _archievedSessionList.Reverse();
                }
                if (ActiveSessionList.Items.Count > 1)
                {
                    FillActiveSessionsList(_activeSessionList);
                }
                if (FinishedSessionList.Items.Count > 1)
                {
                    FillFinishedSessionsList(_finishedSessionList);
                }
                if (ArchivedSessionList.Items.Count > 1)
                {
                    FillArchivedSessionsList(_archievedSessionList);
                }
            }
            catch (Exception exc) { LogFileWrite(exc); }
        }                               
        #endregion
    }
}