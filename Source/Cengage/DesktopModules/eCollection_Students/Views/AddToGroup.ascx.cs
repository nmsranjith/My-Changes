using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Common;
using System.Text;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Linq;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyGroups" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the groups to which the selected students can be added.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddToGroup : eCollection_StudentsModuleBase
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
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sort all groups and classes by Names in Asc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesAscButton_Click(object sender, EventArgs e)
        {
            try
            {
                NamesAscButton.Visible = false;
                NamesDescButton.Visible = true;
                GroupsSortExpression = "Name";
                GroupsSortDirection = SortDirection.Descending;
                SortAndBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sort all groups and classes by Names in Desc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesDescButton_Click(object sender, EventArgs e)
        {
            try
            {
                NamesAscButton.Visible = true;
                NamesDescButton.Visible = false;
                GroupsSortExpression = "Name";
                GroupsSortDirection = SortDirection.Ascending;
                SortAndBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sort all groups and classes by PM Reading Level in Asc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PmAscButton_Click(object sender, EventArgs e)
        {
            try
            {
                PMAscButton.Visible = false;
                PMDescButton.Visible = true;
                GroupsSortExpression = "ReadingLevel";
                GroupsSortDirection = SortDirection.Descending;
                SortAndBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sort all groups and classes by PM Reading Level in Desc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PmDescButton_Click(object sender, EventArgs e)
        {
            try
            {
                PMAscButton.Visible = true;
                PMDescButton.Visible = false;
                GroupsSortExpression = "ReadingLevel";
                GroupsSortDirection = SortDirection.Ascending;
                SortAndBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Method to Search Groups and Classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchTextBox.Text.Trim() != string.Empty)
                    searchhdn.Value = "search";
                else
                    searchhdn.Value = "searchempty";
                SortAndBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "LoadClick", "<script>LoadClick()</script>", false);
                SortingPanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress();jQuery('#CheckAllDiv').show();jQuery('#UnCheckAllDiv').hide();</script>", false);
            }
        }
        
        /// <summary>
        ///  Adds student/s to the selected groups and classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentToGroup_Click(object sender, EventArgs e)
        {
            try
            {
                Messages.ClearMessages();
                if (SelectedStudIds.Value != string.Empty)
                {
                    List<Student> StudentList = new List<Student>();
                    var str = SelectedGrpIds.Value.Substring(1, SelectedGrpIds.Value.Length - 1).Split(',');
                    var names = SelectedStudIds.Value.Substring(1, SelectedStudIds.Value.Length - 1).Split(',');

                    for (int i = 0; i < names.Length; i++)
                    {
                        for (int j = 0; j < str.Length; j++)
                        {
                            Student addStudent = new Student()
                            {
                                ClassId = int.Parse(str[j]),
                                DateModified = DateTime.Now,
                                UserModified = TeacherLoginName,
                                Active = (char)MyEnums.Active.Yes,
                                SubscriptionId = int.Parse(Session["Subscription"].ToString()),
                                CustSubUserSk = int.Parse(names[i])
                            };
                            StudentList.Add(addStudent);
                        }
                    }

                    if (studentController.AddStudentToGroup(StudentList) == StudentList.Count)
                    {
                        Session["SelectedStudents"] = null;
                        Messages.ShowSuccess(GetMessage(Constants.STUDENTS_ADDED_TO_GRPS));
                        studentController.ClearAllCache();
                        BuildSelectedItems();
                        SortAndBind();
                        // AllOtherGroupsDiv.Visible = true;
                    }
                }
                else
                {
                    Session["SelectedStudents"] = null;
                    BuildSelectedItems();
                    Messages.ShowWarning(GetMessage(Constants.SELECT_STUDENTS));
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Cancel button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
        }
   
        #endregion

        #region member Functions
        /// <summary>
        ///  Fills class list of the current user
        /// </summary>
        /// <param name="_Classes"></param>
        private void FillClassList(List<Groups> _Classes)
        {
            try
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
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Fills group list of the current user
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillGroupsList(List<Groups> _Groups)
        {
            try
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
            catch (Exception ex){LogFileWrite(ex);}
        }

        /// <summary>
        ///  Fills all other groups list
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillOtherGroupsList(List<Groups> _Groups)
        {
            try
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
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Fills all other classes list
        /// </summary>
        /// <param name="_Classes"></param>
        private void FillOtherClassList(List<Groups> _Classes)
        {
            try
            {
                if (_Classes != null && _Classes.Count > 0)
                {
                    OtherClsRepeater.DataSource = _Classes;
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
            catch (Exception ex) { LogFileWrite(ex); }
        }
     
        /// <summary>
        ///  Sorts and binds classes and groups
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
                    Student stu = new Student() { UserLoginName = TeacherLoginName, GrpType = 'N', ActionType = "FORSUBS", SubscriptionId = int.Parse(Session["Subscription"].ToString()) };
                    var EntireGroupslist = studentController.GetGroupNames(stu);
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

                                var otherList = new List<Groups>();
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        otherList.AddRange(_otherGroupList.Where(x => x.Name.Trim().ToLower() == text).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_otherGroupList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
                                    }
                                }
                                _otherGroupList = otherList.Distinct().ToList();

                                var otherClsList = new List<Groups>();
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


                        FillGroupsList(_groupList);
                        FillClassList(_classList);
                        FillOtherGroupsList(_otherGroupList);
                        FillOtherClassList(_otherClassList);

                        MessageOuterDiv.Style.Add("Display", "none");
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowGroupDivs", "<script>ShowHideGrpDivs()</script>", false);

                    }
                    else
                    {
                        AddToGroupsMainDiv.Visible = false;
                        MessageOuterDiv.Style.Add("Display", "block");
                        Message1.Text = Constants.NOGROUPINFO;
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
        ///  Bind the selected students in the to box
        /// </summary>
        private void BuildSelectedItems()
        {
            try
            {
                if (Session["SelectedStudents"] != null)
                {
                    StringBuilder output = new StringBuilder();
                    List<Student> selectedStudents = Session["SelectedStudents"] as List<Student>;
                    foreach (Student sm in selectedStudents)
                    {
                        if (sm.FullName.Trim().Length > 0)
                        {
                            output.AppendFormat("<li class=\"SelectedStudentItem\"><span title='{2}'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", sm.FullName.Length >= 10 ? string.Concat(sm.FullName.Substring(0, Math.Min(sm.FullName.Length, 9)), " ..") : sm.FullName, sm.CustSubUserSk, sm.FullName);
                            SelectedValueTextBox.Text += sm.CustSubUserSk + ",";
                        }
                    }
                    SelectedStudentList.InnerHtml = output.ToString();
                }
                else
                {
                    SelectedStudentList.InnerHtml = string.Empty;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        #endregion
       
    }
}