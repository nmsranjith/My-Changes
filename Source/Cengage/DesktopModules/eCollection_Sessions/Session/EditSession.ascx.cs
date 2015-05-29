using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using _Sessions = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Components.ExceptionHandling;
using System.Text;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="EditSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Edit Session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class EditSession : eCollection_SessionsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {           
            try
            {
                this.Messages.ClearMessages();
                if (!Page.IsPostBack)
                {
                    BindSubscription();
                    if (EditSessionId.HasValue)
                    {
                        BindSession(EditSessionId.Value);
                    }                    
                }
                if (GroupsSelected.Count > 0)
                {
                    txtSessionName.Text = GenerateSessionName(GroupsSelected, DateTime.Now,"true");
                }
            }
            catch (Exception ec)
            {
                Messages.ShowError(Localization.GetString("EditSessionSuccess", LocalResourceFile));    
                LogFileWrite(ec);   
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        private void BindSession(int SessionId)
        {
            try
            {
                _Sessions sessions = AllActiveSessions.Where(u => u.SessionId == SessionId).FirstOrDefault();
                SessionController sessionController = SessionController.Instance;
                if (sessions != null)
                {
                    txtSessionName.Text = sessions.Name;
                    txtNotes.Value = sessions.Notes;
                    this.DrpSubscription.SelectedValue = sessions.CUST_SUBS_SK.ToString();
                    this.SessionDropDownList.DataValueField = "Next Week";
                    if (sessions.SessionType == MyEnums.SessionType.Guided)
                    {
                        //Switch.Attributes.Add("style", "background-image:url('../Portals/0/images/guided_off.png');background-position: center; background-repeat: no-repeat; display: inline-block;");
                        lblSessiontype.Value = "1";
                    }
                    else
                    {
                        //Switch.Attributes.Add("style", "background-image:url('../Portals/0/images/session_switch.png');background-position: center; background-repeat: no-repeat; display: inline-block;");
                        lblSessiontype.Value = "2";
                    }
                    GroupsSelected = sessionController.GetSessionMembers(sessions.SessionId.Value);
                    BuildSelectedItems();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }     
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindSubscription()
        {
            List<IDCollection> listToBind = Subscription;
            this.DrpSubscription.DataTextField = "Text";
            this.DrpSubscription.DataValueField = "Id"; // optional depending on your needs
            
            this.DrpSubscription.DataSource = listToBind;
            this.DrpSubscription.DataBind();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelMakingSessionLinkButton_Click(object sender, EventArgs e)
        {
            Session["SelectedGroups"] = null;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelCreateSession_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateSessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                _Sessions sessions = new _Sessions
                {
                    SessionId = EditSessionId,
                    Name = txtSessionName.Text,
                    SessionType = (MyEnums.SessionType)Convert.ToInt32(lblSessiontype.Value),
                    CUST_SUBS_SK =  Convert.ToInt32(DrpSubscription.SelectedValue),
                    CREATED_BY_CUST_SUBS_USER_SK = 5,
                    ENDED_BY_CUST_SUBS_USER_SK = 12,
                    WorkType = "sess",
                    SessionCreatedDate = DateTime.Now,
                    SessionExpiryDate = DateTime.Now.AddDays(6),
                    SessionStatus = "Active",
                    IsNameManualOverride = "Y",
                    Notes = txtNotes.Value,
                    IsActive = "Y",
                    CreatedByUserId = 1,
                    LastModifiedByUserId = 1,
                    CreatedByUserName = "Admin",
                    SessionMembers = GroupsSelected,
                    SessionProducts = ProductsSelected,                  
                };
                SessionController sessionController = SessionController.Instance;
                sessionController.Update(sessions);

            }
            catch (SessionValidationException exc)
            {
                if (exc.getErrorState() == MyEnums.CrudState.Update)
                {
                    Messages.ShowWarning(exc.getErrorMessage());
                    LogFileWrite(exc);
                }

            }
            catch (Exception exc)
            {
                this.Messages.ShowError(exc.Message);
                LogFileWrite(exc);     
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addstudenttosession");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddGroupsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addgroupstosession");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTeachersButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addteacherstosession");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AttachBooks_Click1(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addbookstosession");
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildSelectedItems()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                List<SessionMembers> SelectedGroups = GroupsSelected;
                foreach (SessionMembers sm in SelectedGroups)
                {
                    if (sm.MemberType == "GROUP" && sm.GroupName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedGroupItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveGroup(this);\'>x</a></li>", sm.GroupName, sm.GRP_MEM_SK);
                        SelectedValueGroupTextBox.Text += sm.GRP_MEM_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedStudentItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", sm.StudentName, sm.CUST_SUBS_USER_SK);
                        SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedTeacherItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", sm.TeacherName, sm.CUST_SUBS_USER_SK);
                        SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }
                }
                SelectedStudentList.InnerHtml = output.ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }     
        }
    }
}