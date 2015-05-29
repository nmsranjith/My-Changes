using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupListDashBoardMenu" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //  Left panel controller for group list screen.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupListDashBoardMenu : eCollection_GroupsModuleBase
    {
        GroupController groupcontroller = GroupController.Instance;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateGroupButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATEGROUP;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateGroupButton_Click(object sender, EventArgs e)
        {
            StudentList = null;
            TeachersList = null;
            EditClassId = null;
            EditGroupId = null;
            GroupName = string.Empty;
            List<IDCollection> currentUserCollection = new List<IDCollection>();
            IDCollection currentUser = new IDCollection() { Id = userID, Text = "You" };
            currentUserCollection.Add(currentUser);
            TeachersList = currentUserCollection;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATEGROUP);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedId = 0;
                StudentList = null;
                TeachersList = null;
                EditClassId = null;
                EditGroupId = null;
                string[] selectedIDArray = selectedGroupID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                if (selectedIDArray.Length != 0)
                {
                    if (int.TryParse(selectedIDArray[0].Trim(), out selectedId))
                    {
                        char type = char.Parse(selectedIDArray[1].Trim());
                        if (type == ClassType)
                        {
                            EditClassId = selectedId;
                        }
                        else
                        {
                            EditGroupId = selectedId;
                        }
                    }
                }
                selectedGroupID.Value = string.Empty;
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + EDITGROUP);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MergeGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                StudentList = null;
                TeachersList = null;
                EditClassId = null;
                EditGroupId = null;
                GroupName = string.Empty;
                MergeClassId = selectedGroupID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                AllOther = true;
                List<Components.Groups> MergedGroups = new List<Components.Groups>();
                MergedGroups = groupcontroller.GetGroupList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()));
                List<Components.Groups> MergedGroupClass = new List<Components.Groups>();
                GroupType = 'C';
                MergedGroupClass = MergedGroups.Concat(groupcontroller.GetClassList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()))).ToList<Components.Groups>();
                var text=string.Empty;
                foreach(Components.Groups g in MergedGroupClass)
                {
                    text = string.Concat(text, ", Group-->", Null.SetNullString(g.GroupId), ",SUBS->", g.SubscriptionId, ",CustomerSubId-->", Null.SetNullString(g.CustomerSubId));
                }
                LogFileWrite(new Exception(text.Trim(',')));
                int h = MergedGroupClass.Where(u => MergeClassId.Contains(u.GroupId.ToString())).ToList().GroupBy(x => x.CustomerSubId).ToList().Count();
                //int h = MergedGroupClass.GroupBy(x => x.CustomerSubId).Count();
                if (h != 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallSetPopupFlag", "SetPopUpFlag()", true);
                    MergeClassId = null;
                    return;
                }
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATEGROUP);
            }
            catch (Exception ex){ LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StartReadingSessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SelectedGroups"] = null;
                Session["SelectedProducts"] = null;
                Session["EditSelectedId"] = null;
                Session["SessionNotes"] = string.Empty;
                Session["SelectedSubscriptionId"] = null;
                List<Components.Groups> MergedGroups = new List<Components.Groups>();
                MergeClassId = selectedGroupID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                AllOther = true;
                MergedGroups = groupcontroller.GetGroupList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()));
                List<Components.Groups> MergedGroupClass = new List<Components.Groups>();
                GroupType = 'C';
                MergedGroupClass = MergedGroups.Concat(groupcontroller.GetClassList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()))).ToList<Components.Groups>();
                MergedGroupClass = MergedGroupClass.Where(u => MergeClassId.Contains(u.GroupId.ToString())).ToList();
                int h = MergedGroupClass.Where(u => MergeClassId.Contains(u.GroupId.ToString())).ToList().GroupBy(x => x.CustomerSubId).ToList().Count();
                //int h = MergedGroupClass.GroupBy(x => x.CustomerSubId).Count();
                if (h != 1)
                {
                    AlertmessageLabel.Text = "Cannot create a reading session, the groups having different subscription";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallSetPopupFlag", "SetPopUpFlag()", true);
                    return;
                }
                List<SessionMembers> sessionCollection = new List<SessionMembers>();
                int customerSubID = 0;
                foreach (Components.Groups group in MergedGroupClass)
                {
                    SessionMembers sessionMembers = new SessionMembers();
                    sessionMembers.GRP_MEM_SK = group.GroupId;
                    sessionMembers.GroupName = group.Name;
                    sessionMembers.MemberType = "GROUP";
                    sessionMembers.Added_date = DateTime.Now;
                    sessionMembers.Active = "Y";
                    customerSubID = group.CustomerSubId.Value;
                    sessionCollection.Add(sessionMembers);
                }
                Session["SelectedGroups"] = sessionCollection;
                Session["SelectedSubscriptionId"] = customerSubID;
                Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + SessionParameter + "&returnurl=groups");
            }
            catch (Exception ex)
            {               
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedID = selectedGroupID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                Components.Groups _groups = new Components.Groups();
                _groups.ActiveFlag = 'N';
                _groups.LoginName = LoginName;
                _groups.DateCreated = DateTime.Now;
                Components.GroupController groupcontroller = Components.GroupController.Instance;
                groupcontroller.DeleteGroup(_groups, SelectedID.ToList());
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
            catch (Exception ex)
            {               
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CengageStagingButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CENGAGESTAGING);
        }
    }
}