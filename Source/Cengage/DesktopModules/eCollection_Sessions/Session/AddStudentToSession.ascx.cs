using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using DotNetNuke.Common;
using System.Configuration;
using System.Data;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common ;
using System.Web.UI.HtmlControls;
using _Student = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Student;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddStudentToSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Add students to session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddStudentToSession : eCollection_SessionsModuleBase
    {
        #region Private Members
        private List<_Student> _studentList = new List<_Student>();

        private SortDirection StudentSortDirection
        {
            get
            {
                if (Session["StudentSortDirection"] == null)
                    Session["StudentSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["StudentSortDirection"];
            }
            set { Session["StudentSortDirection"] = value; }
        }

        private string StudentSortExpression
        {
            get
            {
                if (Session["StudentSortExpression"] == null)
                    Session["StudentSortExpression"] = "StudentName";
                return (string)Session["StudentSortExpression"];
            }
            set { Session["StudentSortExpression"] = value; }
        }

        #endregion

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CustomPaging.OnbuttonClicked += new CustomPaging.ButtonClciked(SortAndBind);
                if (!IsPostBack)
                {
                    if (SelectedSubscriptionId != null)
                    {
                        DataCache.ClearCache(string.Format("GetStudentsList{0}", int.Parse(Session["Subscription"].ToString())));
                        DataCache.ClearCache(string.Format("GetSearchedStudentsList{0}", int.Parse(Session["Subscription"].ToString())));
                        BuildSelectedItems();
                        SortAndBind(10, 0, 0);
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AllOtherGroupButton_Click(object sender, EventArgs e)
        {
            AllotherGroupDivBtn.Style.Add("display", "none");
            CustomPaging.DisplayPropertyForPage("block");
            SortAndBind(10, 0, 0);
            AllotherGroupUpdatePanel.Update();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SortingButton_Click(object sender, EventArgs e)
        {
            try
            {
                StudentSortExpression = "StudentName";
                List<_Student> studentcollection = new List<_Student>();
                int pageno = CustomPaging.GetCurrentPageNo();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    _Student students = new _Student();
                    students.CUST_SUBS_SK = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                    students.StudentName = (myItemCollection[Stindex].Controls[4] as Label).Text;
                    students.StudentLoginName = (myItemCollection[Stindex].Controls[5] as Label).Text.Replace("(", "").Replace(")", "");
                    studentcollection.Add(students);
                }

                if (SortingButton.CommandName == "Ascending")
                {
                    StudentSortDirection = SortDirection.Descending;
                    SortingButton.CommandName = "Descending";
                    SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());

                    FillStudentsList(studentcollection.OrderByDescending(s => s.StudentName).ToList(), ConstRowCount, pageno);

                    StudentRepeaterUpdatePanel.Update();
                }
                else
                {
                    StudentSortDirection = SortDirection.Ascending;
                    SortingButton.CommandName = "Ascending";
                    SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());

                    FillStudentsList(studentcollection.OrderBy(s => s.StudentName).ToList(), ConstRowCount, pageno);

                    StudentRepeaterUpdatePanel.Update();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLevelButton_Click(object sender, EventArgs e)
        {
            try
            {
                StudentSortExpression = "ReadingLevel";
                List<_Student> studentcollection = new List<_Student>();
                int pageno = CustomPaging.GetCurrentPageNo();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    _Student students = new _Student();
                    students.CUST_SUBS_SK = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                    students.StudentName = (myItemCollection[Stindex].Controls[4] as Label).Text;
                    students.StudentLoginName = (myItemCollection[Stindex].Controls[5] as Label).Text.Replace("(", "").Replace(")", "");
                    studentcollection.Add(students);
                }

                if (ReadingLevelButton.CommandName == "Ascending")
                {
                    StudentSortDirection = SortDirection.Descending;
                    ReadingLevelButton.CommandName = "Descending";
                    ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
                }
                else
                {
                    StudentSortDirection = SortDirection.Ascending;
                    ReadingLevelButton.CommandName = "Ascending";
                    ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
                }
                studentcollection.Sort(_Student.SessionReadingLevelSorter);
                FillStudentsList(studentcollection, ConstRowCount, pageno);
                StudentRepeaterUpdatePanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToSession_Click(object sender, EventArgs e)
        {
            GetSelectedStudents();
            DataCache.RemoveCache(string.Format("GetStudentsList{0}", SelectedSubscriptionId));
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession&returnurl=" + Request.QueryString["returnurl"]);

            SelectedID = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void classSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int classId = 0;
                bool isValid = int.TryParse(classSearchTextBox.Text.Trim(), out classId);
                if (isValid)
                {
                    List<_Student> studentlist = SessionController.Instance.GetStudentByGroup(classId);
                    SelectedID = classSearchTextBox.Text.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray(); ;
                    CustomPaging.DisplayPropertyForPage("none");
                    string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    studentlist.ForEach(x => { x.Checked = false; });
                    if (addedStudent.Length > 0)
                    {
                        foreach (string s in addedStudent)
                        {
                            studentlist.Where(x => x.CUST_SUBS_SK.ToString().Equals(s)).ToList().ForEach(x => { x.Checked = true; });
                        }
                    }
                    GetSelectedStudents();
                    BuildSelectedItems();
                    StudentRepeater.DataSource = studentlist;
                    StudentRepeater.DataBind();
                    ConstRowCount = studentlist.Count;
                    StudentRepeaterUpdatePanel.Update();
                    CheckALLUpdatePanel.Update();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                SessionController.Instance.GetSearchStudentDetails(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName, int.Parse(Session["Subscription"].ToString()));
                BuildSelectedItems();
                SortAndBind(10, 0, 0);
                AllotherGroupDivBtn.Style.Add("display", "none");
                if (ConstRowCount > 10)
                    CustomPaging.DisplayPropertyForPage("block");
                else
                    CustomPaging.DisplayPropertyForPage("none");                
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
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
                    output.AppendFormat("<li class=\"SelectedGroupItem\"><span title=" + sm.GroupName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveGroup(this);\'>x</a></li>", TruncateName(sm.GroupName), sm.GRP_MEM_SK);
                    SelectedValueGroupTextBox.Text += sm.GRP_MEM_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                {
                    output.AppendFormat("<li class=\"SelectedStudentItem\"><span title=" + sm.StudentName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", TruncateName(sm.StudentName), sm.CUST_SUBS_USER_SK);
                    SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                {
                    output.AppendFormat("<li class=\"SelectedTeacherItem\"><span title=" + sm.TeacherName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveTeacher(this);\'>x</a></li>", TruncateName(sm.TeacherName), sm.CUST_SUBS_USER_SK);
                    SelectedValueTeacherTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                }
            }
            SelectedStudentList.InnerHtml = output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="take"></param>
        /// <param name="pageSize"></param>
        /// <param name="startCount"></param>
        private void SortAndBind(int take, int pageSize, int startCount)
        {
            try
            {                
                ConstRowCount = AllOtherStudents.Count;
                _studentList = AllOtherStudents.OrderBy(o => o.StudentName).Take(ConstRowCount).ToList();               
            if (ConstRowCount == 0)
            {
                MessageOuterDiv.Style.Add("Display", "block");
                StudentDiv.Style.Add("Display", "none");
                Message1.Text = Constants.NOSTUDENTSINFO;
            }
            else
            {
                MessageOuterDiv.Style.Add("Display", "none");
                StudentDiv.Style.Add("Display", "block");
            }
            switch (StudentSortExpression)
            {
                case "StudentName":
                    _studentList = _studentList.Take(take).Skip(pageSize).ToList();
                    _studentList.Sort();
                    break;
                case "ReadingLevel":
                     _studentList.Sort(_Student.SessionReadingLevelSorter);
                    _studentList = _studentList.Take(take).Skip(pageSize).ToList();
                    break;
                default:
                    break;
            }

                if (StudentSortDirection == SortDirection.Descending)
                {
                    _studentList.Reverse();
                }
                FillStudentsList(_studentList, ConstRowCount, startCount);
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_studentList"></param>
        /// <param name="totalCount"></param>
        /// <param name="startCount"></param>
        private void FillStudentsList(List<_Student> _studentList, int totalCount, int startCount)
        {
            try
            {
                if (_studentList != null && _studentList.Count > 0)
                {
                    CustomPaging.CreatePagingControl(totalCount, startCount);
                    CustomPaging.PageButtonStyle(startCount);
                    string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    _studentList.ForEach(x => { x.Checked = false; });
                    if (addedStudent.Length > 0)
                    {
                        foreach (string s in addedStudent)
                        {
                            _studentList.Where(x => x.CUST_SUBS_SK.ToString().Equals(s)).ToList().ForEach(x => { x.Checked = true; });
                        }
                    }
                    if (!(ConstRowCount > 10))
                    {
                        AllotherGroupDivBtn.Style.Add("display", "none");
                        CustomPaging.DisplayPropertyForPage("none");
                    }
                }
                else
                {
                    StudentDiv.Style.Add("display", "none");
                    ClassContainer.Style.Add("display", "none");
                }
                StudentRepeater.DataSource = _studentList;
                StudentRepeater.DataBind();
                CheckALLUpdatePanel.Update();
                StudentRepeaterUpdatePanel.Update();
                AllotherGroupUpdatePanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StudentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("StudentNameLabel");
            label.ToolTip = label.Text;
            label.Text = label.Text.Length > 20 ? label.Text.Substring(0, 20) + " ..." : label.Text;
            Label StudentLoginNamelabel = (Label)e.Item.FindControl("StudentLoginName");
            StudentLoginNamelabel.Text = StudentLoginNamelabel.Text.Length > 15 ?"("+ StudentLoginNamelabel.Text.Replace("(","").Replace(")","").Substring(0, 15) + "...)" : StudentLoginNamelabel.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void GetSelectedStudents()
        {
            try
            {
                List<SessionMembers> SelectedGroups = new List<SessionMembers>();

                string[] StudIDs = SelectedValueTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] GroupIDs = SelectedValueGroupTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] TeacherIDs = SelectedValueTeacherTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

                foreach (string s in StudIDs)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (Student lstItems in FullListStudents)
                        {
                            if (lstItems.CUST_SUBS_SK == Convert.ToInt32(s))
                            {
                                SessionMembers sessionMembers = new SessionMembers()
                                {
                                    CUST_SUBS_USER_SK = lstItems.CUST_SUBS_SK,
                                    StudentName = lstItems.StudentName,
                                    MemberType = "USER",
                                    Added_date = DateTime.Now,
                                    Active = "Y"
                                };
                                bool studentExists = SelectedGroups.Any(e => e.CUST_SUBS_USER_SK == sessionMembers.CUST_SUBS_USER_SK);
                                if (!studentExists)
                                {
                                    SelectedGroups.Add(sessionMembers);
                                }
                                break;
                            }
                        }
                    }
                }

                foreach (string s in TeacherIDs)
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

                foreach (string s in GroupIDs)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (SessionMembers sm in GroupsSelected)
                        {
                            if (sm.GRP_MEM_SK == Convert.ToInt32(s))
                            {
                                if (!SelectedGroups.Contains(sm))
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
        /// <param name="Name"></param>
        /// <returns></returns>
        private string TruncateName(string Name)
        {
            return (Name.ToString().Length >= 10 ? Name.ToString().Substring(0, 9) + " ..." : Name.ToString());
        }




    }
}