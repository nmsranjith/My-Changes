using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Web.UI;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the students in class wisely
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class StudentsList : eCollection_StudentsModuleBase
    {
        #region Class Members
            int cnt = 0;
            string loadType = "";
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
                if (!IsPostBack)
                {
                    if (Session["SName" + HttpContext.Current.Session.SessionID] != null && Session["SName" + HttpContext.Current.Session.SessionID] != string.Empty)
                    {
                        MovedMessageSpan.InnerHtml = "<span class=\"movedmsgstudname\">" + Session["SName" + HttpContext.Current.Session.SessionID] + "</span><span class=\"movedmsg1\"> has been moved successfully</span>";
                        MovedMessageSpan.Visible = true;
                        Session["SName" + HttpContext.Current.Session.SessionID] = null;
                    }
                    Session["SortingOrder"] = "ASC";
                    Session["SortingExpression"] = "Names";
                    StudentsCount.Value = AllOtherStudents.Count.ToString();
                    if (AllOtherStudents.Count == 0)
                    {
                        StudentListDiv.Style.Add("Display", "none");
                        MessageOuterDiv.Style.Add("Display", "block");
                        Message1.Text = Constants.NOSTUDENTSINFO;
                        CreateLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATESTUDENTPROFILE;
                    }
                    else
                    {
                        StudentListDiv.Style.Add("Display", "block");
                        MessageOuterDiv.Style.Add("Display", "none");
                    }
                    FillClassList();
                }
            }
            catch (Exception ex) { Messages.ShowWarning("Error in student list screen."); LogFileWrite(ex); }
        }

        /// <summary>
        /// Method to show all the students of the expanded class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClickedGroup_Click(object sender, EventArgs e)
        {
            try
            {
                Repeater StudentsListRptr = (Repeater)ClassListView.Items[Null.SetNullInteger(ContainerId.Value)].FindControl("StudentsList");
                int classId = Null.SetNullInteger(ClassId.Value);
                StudentsListRptr.DataSource = SortAndReturn(classId);
                StudentsListRptr.DataBind();
                ((UpdatePanel)ClassListView.Items[Null.SetNullInteger(ContainerId.Value)].FindControl("StudentsPanel")).Update();             
            }
            catch (Exception ex) { LogFileWrite(ex); }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "SetStudentsListHeight", "<script>SetStudentsListHeight()</script>", false);
             //   ScriptManager.RegisterStartupScript(Page, GetType(), "SetArrowType", "<script>SetArrowType(" + ClassId.Value + ")</script>", false);
            }
        }    

        /*/// <summary>
        ///  Sorts the students list by Reading level in asc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLvlAscButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadType = "Sorting";
                Session["SortingOrder"] = "DESC";
                Session["SortingExpression"] = "ReadingLevel";
                //FillClassList();
                GetClassId(sender, e);
                ReadingLvlAscButton.Visible = false;
                ReadingLvlDescButton.Visible = true;
                //Sortingpanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sorts the students list by Reading level in desc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLvlDescButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadType = "Sorting";
                Session["SortingOrder"] = "ASC";
                Session["SortingExpression"] = "ReadingLevel";
                ReadingLvlAscButton.Visible = true;
                ReadingLvlDescButton.Visible = false;
                //FillClassList();
                GetClassId(sender, e);
                //Sortingpanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Sorts the students list by Names in asc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesAscButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadType = "Sorting";
                Session["SortingOrder"] = "DESC";
                Session["SortingExpression"] = "Names";
                //FillClassList();
                GetClassId(sender,e);
                NamesAscButton.Visible = false;
                NamesDescButton.Visible = true;
                //Sortingpanel.Update();
            }
            catch (Exception ex) {LogFileWrite(ex); }
        }


        /// <summary>
        ///  Sorts the students list by Names in desc order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesDescButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadType = "Sorting";
                Session["SortingOrder"] = "ASC";
                Session["SortingExpression"] = "Names";
                NamesAscButton.Visible = true;
                NamesDescButton.Visible = false;
               // FillClassList();
                GetClassId(sender, e);
                //Sortingpanel.Update();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }*/

        /// <summary>
        ///  Method to search the students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                FillClassList();
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowClass", "<script>ShowClass()</script>", false);
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
        }

        /// <summary>
        ///  Fill all the class names if the subscription
        /// </summary>
        protected void FillClassList()
        {
            try
            {
                ClassListView.DataSource = studentController.GetGroupNames(new Student()
                {
                    UserLoginName = TeacherLoginName,
                    SubscriptionId = Null.SetNullInteger(Session["Subscription"]),
                    GrpCacheName = "GetGroupList",
                    ActionType = "FORTEACHERS",
                    GrpType = 'C'
                });
                ClassListView.DataBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }       

        /// <summary>
        ///  Sort and returns the Students list
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        private List<Student> SortAndReturn(int classid)
        {
            List<Student> stuList = new List<Student>();
            try
            {
                var sortClassMembers = AllOtherStudents.FindAll(x => x.ClassId == classid).ToList();
                string[] name = SearchTextBox.Text.Split(',');                
                name = name.Where(s => s != string.Empty).ToArray();
                if (name.Length > 0)
                {                  
                    foreach (string s in name)
                    {
                        var text = s.Trim().ToLower();
                        stuList.AddRange(sortClassMembers.Where(x => x.DisplayName.ToLower().Contains(text)));
                        stuList=stuList.Distinct().ToList();
                    }
                }
                else
                    stuList = sortClassMembers;
                switch (Session["SortingExpression"].ToString())
                {
                    case "ReadingLevel":
                        stuList.Sort(Student => Student.CurrentReadingLevel);
                        break;
                    default:
                        stuList.Sort(Student => Student.FullName);
                        break;
                }

                if (Session["SortingOrder"] == "DESC")
                    stuList.Reverse();
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return stuList;
        }

        /// <summary>
        ///  Gets the Class ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetClassId(object sender, EventArgs e)
        {
            try
            {
                var a = studentController.GetGroupNames(new Student()
                {
                    UserLoginName = TeacherLoginName,
                    SubscriptionId = Null.SetNullInteger(Session["Subscription"]),
                    GrpCacheName = "GetGroupList",
                    ActionType = "FORTEACHERS",
                    GrpType = 'C'
                }).Select(x => x.GroupId).ToList();

                a.ForEach(x =>
                {
                    ContainerId.Value = a.IndexOf(x).ToString();
                    ClassId.Value = x.ToString();
                    ClickedGroup_Click(sender, e);
                });
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        ///  Binds respective class members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassMembers_DataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                Repeater StudentsListRptr = (Repeater)e.Item.FindControl("StudentsList");
                int classId = Null.SetNullInteger(ClassListView.DataKeys[e.Item.DataItemIndex].Value);
                StudentsListRptr.DataSource = SortAndReturn(classId);
                StudentsListRptr.DataBind();           
            }
            catch (Exception ex) { LogFileWrite(ex); }          
        }

        protected void studentssortdpn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
                switch (studentssortdpn.SelectedValue)
                {
                    case "3":
                        loadType = "Sorting";
                        Session["SortingOrder"] = "DESC";
                        Session["SortingExpression"] = "Names";
                        GetClassId(sender, e);  
                        break;
                    case "2":
                        loadType = "Sorting";
                        Session["SortingOrder"] = "ASC";
                        Session["SortingExpression"] = "Names";
                        GetClassId(sender, e);
                        break;
                    default:
                        loadType = "Sorting";
                        Session["SortingOrder"] = "ASC";
                        Session["SortingExpression"] = "ReadingLevel";
                        GetClassId(sender, e);
                        break;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

       
    }
}