using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using _Students = DotNetNuke.Modules.eCollection_Groups.Components.Modal.Students;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddStudentstoCreateGroup" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To add students to create group screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddStudentstoCreateGroup : eCollection_GroupsModuleBase
    {
        #region Private Members
        private List<_Students> _studentList = new List<_Students>();

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
            CustomPaging.OnbuttonClicked += new CustomPaging.ButtonClciked(SortAndBind);  
            if (!IsPostBack)
            {
                if (SelectedSubscription != null)
                {
                    Session["StudentSortDirection"] = null;
                    Session["StudentSortExpression"] = null;
                    SelectedID = null;
                    //GroupController.Instance.ClearAllCaches();
                    AllotherGroupDivBtn.Style.Add("display", "none");
                    CustomPaging.DisplayPropertyForPage("block");
                    BuildSelectedItems();
                    SortAndBind(10, 0, 0);
                }

            }
           
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

                List<_Students> studentcollection = new List<_Students>();
                int pageno = CustomPaging.GetCurrentPageNo();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    _Students students = new _Students();
                    students.UserID = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                    students.StudentNames = (myItemCollection[Stindex].Controls[4] as Label).Text;
                    students.StudentLoginName = (myItemCollection[Stindex].Controls[5] as Label).Text.Replace("(", "").Replace(")", "");
                    studentcollection.Add(students);
                }


                if (SortingButton.CommandName == "Ascending")
                {
                    StudentSortDirection = SortDirection.Descending;
                    SortingButton.CommandName = "Descending";
                    SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
                    FillStudentsList(studentcollection.OrderByDescending(s => s.StudentNames).ToList(), ConstRowCount, pageno);
                }
                else
                {
                    StudentSortDirection = SortDirection.Ascending;
                    SortingButton.CommandName = "Ascending";
                    SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());

                    FillStudentsList(studentcollection.OrderBy(s => s.StudentNames).ToList(), ConstRowCount, pageno);
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
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
                List<_Students> studentcollection = new List<_Students>();
                int pageno = CustomPaging.GetCurrentPageNo();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    Students students = new Students();
                    students.UserID = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                    students.StudentNames = (myItemCollection[Stindex].Controls[4] as Label).Text;
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
                studentcollection.Sort(Students.StudentReadingLevelSorter);
                FillStudentsList(studentcollection, ConstRowCount, pageno);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BacktoCreateBtn_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetStudentsBySubcription{0}", int.Parse(Session["Subscription"].ToString())));
            SelectedID = null;
            string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            if (addedStudent.Length > 0)
            {
               
                StudentList = AllOtherStudents.Where(u => addedStudent.Contains(u.UserID.ToString())).ToList();
            }
            else
            {
                StudentList = new List<_Students>();
            }
            if (EditClassId != -1 || EditGroupId != -1)
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + EDITGROUP);
            }
            else
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATEGROUP);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void classSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            int classId = 0;
            bool isValid = int.TryParse(classSearchTextBox.Text.Trim(), out classId);
            if (isValid)
            {
                List<_Students> studentlist = GroupController.Instance.GetStudentByGroup(classId,LoginName);
                SelectedID = classSearchTextBox.Text.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                if (studentlist.Count < 6)
                {
                    CustomPaging.DisplayPropertyForPage("none");
                }
                string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                studentlist.ForEach(x => { x.Checked = false; });
                if (addedStudent.Length > 0)
                {
                    foreach (string s in addedStudent)
                    {
                        studentlist.Where(x => x.UserID.ToString().Equals(s)).ToList().ForEach(x => { x.Checked = true; });
                    }
                }
                StudentList = AllOtherStudents.Where(u => addedStudent.Contains(u.UserID.ToString())).ToList();
                BuildSelectedItems();
                studentlist.Sort(_Students.StudentReadingLevelSorter);
                StudentRepeater.DataSource = studentlist;
                StudentRepeater.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildSelectedItems()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                if (StudentList != null && StudentList.Count != 0)
                {
                    foreach (Students idCollection in StudentList)
                    {
                        output.AppendFormat("<li class=\"SelectedItem\"><span title='{0}'>{2}</span><span title=\'UserID\' style=\'display:none\'>{1}</span><a onclick='javascript:Remove(this)'>x</a></li>", idCollection.StudentNames, idCollection.UserID, idCollection.StudentNames.Length > 10 ? idCollection.StudentNames.Substring(0, 9) + " ..." : idCollection.StudentNames);
                        SelectedValueTextBox.Text += idCollection.UserID + ",";
                    }
                    if (SelectedValueTextBox.Text.Length > 0)
                    {
                        SelectedStudentList.InnerHtml = output.ToString();
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
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
                _studentList = AllOtherStudents.OrderBy(o => o.StudentNames).Take(ConstRowCount).ToList();              
				if (ConstRowCount == 0)
            	{
	                MessageOuterDiv.Style.Add("Display", "block");
	                StudentsContentDiv.Style.Add("Display", "none");
                    Message1.Text = Constants.NOSTUDENTINFO; 
	            }
	            else
	            {

	                MessageOuterDiv.Style.Add("Display", "none");
	                StudentsContentDiv.Style.Add("Display", "block");
            	}
            switch (StudentSortExpression)
            {
                case "StudentName":
                    _studentList = _studentList.Take(take).Skip(pageSize).ToList();
                    break;
                case "ReadingLevel":
                    _studentList.Sort(_Students.StudentReadingLevelSorter);
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
        private void FillStudentsList(List<_Students> _studentList, int totalCount, int startCount)
        {
            try
            {
                if (_studentList != null & _studentList.Count > 0)
                {
                    CustomPaging.CreatePagingControl(totalCount, startCount);
                    CustomPaging.PageButtonStyle(startCount);
                    string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    _studentList.ForEach(x => { x.Checked = false; });
                    if (addedStudent.Length > 0)
                    {
                       foreach (string s in addedStudent)
                        {
                            _studentList.Where(x => x.UserID.ToString().Equals(s)).ToList().ForEach(x => { x.Checked = true; });
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
                    ClassContainer.Style.Add("display", "none");
                }

                StudentRepeater.DataSource = _studentList;
                StudentRepeater.DataBind();
                if (_studentList.Count > 0)
                    ScriptManager.RegisterStartupScript(Page, GetType(), "showleftline", "<script>showleftline(1)</script>", false);
                else
                    ScriptManager.RegisterStartupScript(Page, GetType(), "showleftline", "<script>showleftline(0)</script>", false);
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
            label.Text = label.Text.Length > 25 ? label.Text.Substring(0, 25) + "..." : label.Text;
            Label StudentLoginNamelabel = (Label)e.Item.FindControl("StudentLoginName");
            StudentLoginNamelabel.Text = StudentLoginNamelabel.Text.Length > 20 ? StudentLoginNamelabel.Text.Substring(0, 20) + "..." : StudentLoginNamelabel.Text;
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
                GroupController.Instance.GetSearchStudentDetails(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName, int.Parse(Session["Subscription"].ToString()));
                BuildSelectedItems();
                SortAndBind(10, 0, 0);
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
                AllotherGroupDivBtn.Style.Add("display", "none");
                if (ConstRowCount > 10)
                    CustomPaging.DisplayPropertyForPage("block");
                else
                    CustomPaging.DisplayPropertyForPage("none");
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}