using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using DotNetNuke.Common;
using DotNetNuke.Services.Exceptions;
using System.Linq;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateStudentProfileDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Add teachers to the groups
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddToGroup : eCollection_TeachersModuleBase
    {
        #region Private Members
            private List<Groups> _groupList;
            private List<Groups> _classList;
            private List<Groups> _otherGroupList;
            private List<Groups> _otherClassList;
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

        #region Page Events
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
                catch (Exception exc)
                {
                    Messages.ShowWarning(exc.Message);
                    LogFileWrite(exc);
                }
            }
        #endregion

        #region Sort Events
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
        #endregion

        #region Button Events

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
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress();jQuery('#CheckAllDiv').show();jQuery('#UnCheckAllDiv').hide();</script>", false);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void AddTeacherToGroup_Click(object sender, EventArgs e)
            {
                Messages.ClearMessages();
                if (SelectedStudIds.Value != string.Empty)
                {
                    List<Teacher> TeacherList = new List<Teacher>();
                    var str = SelectedGrpIds.Value.Substring(1, SelectedGrpIds.Value.Length - 1).Split(',');
                    var names = SelectedStudIds.Value.Substring(1, SelectedStudIds.Value.Length - 1).Split(',');

                    for (int i = 0; i < names.Length; i++)
                    {
                        for (int j = 0; j < str.Length; j++)
                        {
                            Teacher addTeacher = new Teacher()
                            {
                                ClassId = int.Parse(str[j]),
                                DateModified = DateTime.Now,
                                UserModified = TeacherLoginName,
                                Active = (char)MyEnums.Active.Yes,
                                SubscriptionId = int.Parse(Session["Subscription"].ToString()),
                                CustSubUserSk = int.Parse(names[i])
                            };
                            TeacherList.Add(addTeacher);
                        }
                    }
                    try
                    {
                        if (teacherController.AddTeacherToGroup(TeacherList) == TeacherList.Count)
                        {
                            Session["SelectedTeachers"] = null;
                            Messages.ShowSuccess(GetMessage(Constants.TEACHERS_ADDED_TO_GRPS));
                            teacherController.ClearAllCache();
                            BuildSelectedItems();
                            SortAndBind();                          
                        }
                    }
                    catch (Exception ex)
                    {
                        Messages.ShowWarning(ex.Message);
                        LogFileWrite(ex);
                    }
                }
                else
                {
                    Session["SelectedTeachers"] = null;
                    BuildSelectedItems();
                    Messages.ShowWarning(GetMessage(Constants.SELECT_TEACHERS));
                }
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
        #endregion      

        #region Member Functions
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_OtherGroups"></param>
        /// <param name="RepeaterId"></param>
        private void FillList(List<Groups> _OtherGroups,Repeater RepeaterId,string tabid)
        {
            if (_OtherGroups != null && _OtherGroups.Count > 0)
            {
                RepeaterId.DataSource = _OtherGroups;
                RepeaterId.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), tabid+"1", "<script>showtabs(" + tabid + ",1)</script>", false);
            }
            else
            {
                RepeaterId.DataSource = null;
                RepeaterId.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), tabid + "2", "<script>showtabs(" + tabid + ",0)</script>", false);
            }
        }
     
        /// <summary>
        /// 
        /// </summary>
        private void SortAndBind()
        {
            try
            {
                if (SearchTextBox.Text.Trim() != string.Empty)
                    searchhdn.Value = "search";
                else
                    searchhdn.Value = "searchempty";
                
                if (Session["Subscription"] != null)
                {
                    Teacher teacher = new Teacher() { UserLoginName = TeacherLoginName, ProcName = Constants.SP_GETALLSUBSGROUPS, GrpType = 'N', ActionType = "FORSUBS", SubscriptionId = int.Parse(Session["Subscription"].ToString()), GrpCacheName = "GetGroupList" };
                    var EntireGroupslist = teacherController.GetGroupNames(teacher);
                    if (EntireGroupslist.Count > 0)
                    {
                        _groupList = EntireGroupslist.Where(x => x.ListType == "MYGROUPS").ToList();
                        _classList = EntireGroupslist.Where(x => x.ListType == "MYCLASS").ToList();
                        _otherGroupList = EntireGroupslist.Where(x => x.ListType == "OTHERGROUPS").ToList();
                        _otherClassList = EntireGroupslist.Where(x => x.ListType == "OTHERCLASSES").ToList();
                        switch (searchhdn.Value)
                        {
                            case "search":
                                var grpList = new List<Groups>();
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

                                var clsList = new List<Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        clsList.AddRange(_classList.Where(x => x.Name.Trim().ToLower() == text).Union(_classList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_classList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _classList = clsList.Distinct().ToList();

                                var othersList = new List<Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        othersList.AddRange(_otherGroupList.Where(x => x.Name.Trim().ToLower() == text).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _otherGroupList = othersList.Distinct().ToList();

                                var othersClsList = new List<Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        othersClsList.AddRange(_otherClassList.Where(x => x.Name.Trim().ToLower() == text).Union(_otherClassList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_otherClassList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _otherClassList = othersClsList.Distinct().ToList();
                                break;
                            default:
                                break;
                        }
                                               

                        switch (GroupsSortExpression)
                        {
                            case "Name":
                                _groupList.Sort(x => x.Name);
                                _classList.Sort(x => x.Name);
                                _otherGroupList.Sort(x => x.Name);
                                _otherClassList.Sort(x => x.Name);
                                break;
                            case "ReadingLevel":
                                _groupList.Sort(x => x.ReadingLevel);
                                _classList.Sort(x => x.ReadingLevel);
                                _otherGroupList.Sort(x => x.ReadingLevel);
                                _otherClassList.Sort(x => x.ReadingLevel);
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
                        FillList(_groupList, GroupsRepeater, "GroupsDivHdr");
                        FillList(_classList, ClassRepeater, "ClassDivHdr");
                        FillList(_otherGroupList, OtherGrpRepeater, "OtherGroupsDivHdr");
                        FillList(_otherClassList, OtherClsRepeater, "OtherClassDivHdr");
                        MessageOuterDiv.Style.Add("Display", "none");
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowGroupDivs", "<script>ShowHideGrpDivs()</script>", false);
                    }
                    else
                    {
                        SecondDiv.Visible = false;
                        MessageOuterDiv.Style.Add("Display", "block");
                        Message1.Text = Constants.NOGROUPINFO;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideGrpDivs", "<script>ShowHideGrpDivs()</script>", false);
                    }
                    SortingPanel.Update();
                }
                else
                    Response.Redirect(Globals.NavigateURL(DashboardsModule));
            }
            catch (Exception exc)
            {
                Messages.ShowWarning(exc.Message);
                LogFileWrite(exc);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildSelectedItems()
        {
            try
            {
                if (Session["SelectedTeachers"] != null)
                {
                    StringBuilder output = new StringBuilder();
                    List<Teacher> SelectedTeachers = Session["SelectedTeachers"] as List<Teacher>;
                    foreach (Teacher gm in SelectedTeachers)
                    {
                        if (gm.UserLoginName.Trim().Length > 0)
                        {
                            output.AppendFormat("<li class=\"SelectedTeacherItem\"><span title='{2}'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", gm.UserLoginName.Length >= 10 ? string.Concat(gm.UserLoginName.Substring(0, Math.Min(gm.UserLoginName.Length, 9)), " ..") : gm.UserLoginName, gm.CustSubUserSk, gm.UserLoginName);
                        }
                    }
                    SelectedTeacherList.InnerHtml = output.ToString();
                }
                else
                    SelectedTeacherList.InnerHtml = string.Empty;
            }
            catch (Exception exc)
            {
                Messages.ShowWarning(exc.Message);
                LogFileWrite(exc);
            }
        }

        #endregion
    }
}