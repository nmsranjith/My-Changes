using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using _Groups = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Group;
using System.Text;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddGroupsToSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Add groups to session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddGroupsToSession : eCollection_SessionsModuleBase
    {
        #region Private Members
        private List<_Groups> _groupList;
        private List<_Groups> _classList;
        private List<_Groups> _otherGroupList;
        private List<_Groups> _otherClassList;
        private static List<SessionMembers> sessionMembersList = new List<SessionMembers>();
        private SortDirection GroupsSortDirection
        {
            get
            {
                if (Session["GrpsSortDirection"] == null)
                    Session["GrpsSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["GrpsSortDirection"];
            }
            set { Session["GrpsSortDirection"] = value; }
        }

        private string GroupsSortExpression
        {
            get
            {
                if (Session["GrpsSortExpression"] == null)
                    Session["GrpsSortExpression"] = "Name";
                return (string)Session["GrpsSortExpression"];
            }
            set { Session["GrpsSortExpression"] = value; }
        }

        #endregion

        #region Events
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Messages.ClearMessages();
                if (!Page.IsPostBack)
                {
                   SortAndBind();
                   BuildSelectedItems();
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                LogFileWrite(ex); 
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
            GroupsSortExpression = "Name";
            GroupsSortDirection = SortDirection.Descending;
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
            GroupsSortExpression = "Name";
            GroupsSortDirection = SortDirection.Ascending;
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PmAscButton_Click(object sender, EventArgs e)
        {
            PMAscButton.Visible = false;
            PMDescButton.Visible = true;
            GroupsSortExpression = "ReadingLevel";
            GroupsSortDirection = SortDirection.Descending;
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PmDescButton_Click(object sender, EventArgs e)
        {
            PMAscButton.Visible = true;
            PMDescButton.Visible = false;
            GroupsSortExpression = "ReadingLevel";
            GroupsSortDirection = SortDirection.Ascending;
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupSearch_Click(object sender, EventArgs e)
        {
            if (SearchTextBox.Text.Trim() != string.Empty)
                searchhdn.Value = "search";
            else
                searchhdn.Value = "searchempty";
            SortAndBind();
            ScriptManager.RegisterStartupScript(Page, GetType(), "LoadClick", "<script>LoadClick()</script>", false);
            SortingPanel.Update();
            ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToSession_Click(object sender, EventArgs e)
        {
            GetSelectedGroups();
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession&returnurl=" + Request.QueryString["returnurl"]);
        }
        #endregion

        #region member Functions
        /// <summary>
        /// 
        /// </summary>
        protected void GetSelectedGroups()
        {
            try
            {
                List<SessionMembers> SelectedGroups = new List<SessionMembers>();
                StringBuilder teachername = new StringBuilder();

                string[] StudIDs = SelectedValueTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] GroupIDs = SelectedValueGroupTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] TeacherIDs = SelectedValueTeacherTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                List<string> StudTeachIds = new List<string>();
                
                List<Group> a = GroupsList.Concat(ClassesList).ToList();
                foreach (string s in GroupIDs)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (Group groups in a)
                        {
                            if (Convert.ToInt32(groups.GroupId) == Convert.ToInt32(s))
                            {
                                SessionMembers sessionMembers = new SessionMembers()
                                {
                                    GRP_MEM_SK = groups.GroupId,
                                    GroupName = groups.Name,
                                    MemberType = "GROUP",
                                    Added_date = DateTime.Now,
                                    Active = "Y"
                                };
                                if (!SelectedGroups.Contains(sessionMembers))
                                {
                                    SelectedGroups.Add(sessionMembers);
                                }
                                break;
                            }
                        }   
                    }
                }

                StudTeachIds.AddRange(StudIDs);
                StudTeachIds.AddRange(TeacherIDs);

                foreach (string s in StudTeachIds)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (SessionMembers sm in GroupsSelected)
                        {
                            if (sm.CUST_SUBS_USER_SK == Convert.ToInt32(s))
                            {
                                bool teacherExists = SelectedGroups.Any(e => e.CUST_SUBS_USER_SK == sm.CUST_SUBS_USER_SK);
                                if (!teacherExists)
                                {
                                    SelectedGroups.Add(sm);
                                    break;
                                }
                            }
                        }
                    }
                }
                GroupsSelected = SelectedGroups;
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Classes"></param>
        private void FillClassList(List<_Groups> _Classes)
        {
            if (_Classes != null && _Classes.Count > 0)
            {
                ClassRepeater.DataSource = _Classes;
                ClassRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs1", "<script>showtabs(ClassDivHdr,1)</script>", false);
            }
            else
            {
                ClassRepeater.DataSource = null;
                ClassRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs2", "<script>showtabs(ClassDivHdr,0)</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillGroupsList(List<_Groups> _Groups)
        {
            if (_Groups != null && _Groups.Count > 0)
            {
                GroupsRepeater.DataSource = _Groups;
                GroupsRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs3", "<script>showtabs(GroupsDivHdr,1)</script>", false);
            }
            else
            {
                GroupsRepeater.DataSource = null;
                GroupsRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs4", "<script>showtabs(GroupsDivHdr,0)</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillOtherGroupsList(List<_Groups> _Groups)
        {
            if (_Groups != null && _Groups.Count > 0)
            {
                OtherGrpRepeater.DataSource = _Groups;
                OtherGrpRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs6", "<script>showtabs(OtherGroupsDivHdr,1)</script>", false);
            }
            else
            {
                OtherGrpRepeater.DataSource = null;
                OtherGrpRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs7", "<script>showtabs(OtherGroupsDivHdr,0)</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillOtherClassesList(List<_Groups> _Groups)
        {
            if (_Groups != null && _Groups.Count > 0)
            {
                OtherClsRepeater.DataSource = _Groups;
                OtherClsRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs8", "<script>showtabs(OtherClassDivHdr,1)</script>", false);
            }
            else
            {
                OtherClsRepeater.DataSource = null;
                OtherClsRepeater.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "showtabs9", "<script>showtabs(OtherClassDivHdr,0)</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SortAndBind()
        {
            try
            {
                if (Session["Subscription"] != null)
                {
                    var EntireGroupslist = GroupsList;
                    if (EntireGroupslist.Count > 0)
                    {
                        _groupList = EntireGroupslist.Where(x => x.ListType == "MYGROUPS").ToList();
                        _classList = EntireGroupslist.Where(x => x.ListType == "MYCLASS").ToList();
                        _otherGroupList = EntireGroupslist.Where(x => x.ListType == "OTHERGROUPS").ToList();
                        _otherClassList = EntireGroupslist.Where(x => x.ListType == "OTHERCLASSES").ToList();
                        switch (searchhdn.Value)
                        {
                            case "search":
                                var grpList = new List<_Groups>();
                                string[] name;
                                if (SearchTextBox.Text.Trim() != string.Empty && lastsearch.Value == newsearch.Value)
                                    name = SearchTextBox.Text.Split(',');
                                else
                                    name = lastsearch.Value.Split(',');
                                var text = string.Empty;
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        grpList.AddRange(_groupList.Where(x => x.Name.Trim().ToLower() == text).Union(_groupList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_groupList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _groupList = grpList.Distinct().ToList();

                                var clsList = new List<_Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        clsList.AddRange(_classList.Where(x => x.Name.Trim().ToLower() == text).Union(_classList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_classList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _classList = clsList.Distinct().ToList();

                                var otherList = new List<_Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        otherList.AddRange(_otherGroupList.Where(x => x.Name.Trim().ToLower() == text).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _otherGroupList = otherList.Distinct().ToList();

                                var otherClsList = new List<_Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        otherClsList.AddRange(_otherClassList.Where(x => x.Name.Trim().ToLower() == text).Union(_otherClassList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_otherClassList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _otherClassList = otherClsList.Distinct().ToList();
                                break;
                            default:
                                break;
                        }


                        switch (GroupsSortExpression)
                        {
                            case "Name":
                                _groupList.Sort();
                                _classList.Sort();
                                _otherGroupList.Sort();
                                _otherClassList.Sort();
                                break;
                            case "ReadingLevel":
                                _groupList.Sort(_Groups.GroupsReadingLevelSorter);
                                _classList.Sort(_Groups.GroupsReadingLevelSorter);
                                _otherGroupList.Sort(_Groups.GroupsReadingLevelSorter);
                                _otherClassList.Sort(_Groups.GroupsReadingLevelSorter);                                
                                break;
                            default:
                                break;
                        }

                        if (GroupsSortDirection == SortDirection.Descending)
                        {
                            _groupList.Reverse();
                            _classList.Reverse();
                            _otherGroupList.Reverse();
                            _otherClassList.Reverse();
                        }


                        FillGroupsList(_groupList);
                        FillClassList(_classList);
                        FillOtherGroupsList(_otherGroupList);
                        FillOtherClassesList(_otherClassList);

                        MessageOuterDiv.Style.Add("Display", "none");
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowGroupDivs", "<script>ShowHideGrpDivs()</script>", false);
                        
                    }
                    else
                    {
                        SecondDiv.Visible = false;
                        MessageOuterDiv.Style.Add("Display", "block");
                        Message1.Text = Constants.NOGROUPSINFO;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideGrpDivs", "<script>ShowHideGrpDivs()</script>", false);
                    }
                    SortingPanel.Update();
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(DashboardsModule));
                }
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void BuildSelectedItems()
        {
            StringBuilder output = new StringBuilder();
            List<SessionMembers> SelectedGroups = GroupsSelected;
            foreach (SessionMembers sm in SelectedGroups)
            {
                if (sm.MemberType == "GROUP" && sm.GroupName.Trim().Length > 0)
                {
                    output.AppendFormat("<li id='G{1}' class=\"SelectedGroupItem\"><span title='" + sm.GroupName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(G{1});\'>x</a></li>", TruncateName(sm.GroupName), sm.GRP_MEM_SK);
                    SelectedValueTextBox.Text += sm.GRP_MEM_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                {
                    output.AppendFormat("<li id='S{1}' class=\"SelectedStudentItem\"><span title='" + sm.StudentName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(S{1});\'>x</a></li>", TruncateName(sm.StudentName), sm.CUST_SUBS_USER_SK);
                    SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                {
                    output.AppendFormat("<li id='T{1}' class=\"SelectedTeacherItem\"><span title='" + sm.TeacherName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(T{1});\'>x</a></li>", TruncateName(sm.TeacherName), sm.CUST_SUBS_USER_SK);
                    SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                }
            }
            SelectedStudentList.InnerHtml = output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string TruncateName(string Name)
        {
            return (Name.ToString().Length >= 10 ? Name.ToString().Substring(0, 9) + " ..." : Name.ToString());
        }
        
        #endregion
        
    }
}