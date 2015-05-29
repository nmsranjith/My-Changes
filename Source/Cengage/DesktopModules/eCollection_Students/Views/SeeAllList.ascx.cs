using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SeeAllList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the students in created in the current session.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SeeAllList : eCollection_StudentsModuleBase
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
                if (Session["AddedStudentList"] != null)
                {
                    if (!IsPostBack)
                        StudentsCount.Value = (Session["AddedStudentList"] as List<Student>).Count.ToString();
                }
                else
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                FillStudentList();
            }
            catch (Exception ex) {LogFileWrite(ex); }
        }

        /// <summary>
        ///  Fill the students list added in the current session
        /// </summary>
        protected void FillStudentList()
        {
            try
            {

                StudentsListRepeater.DataSource = SortAndReturn();
                StudentsListRepeater.DataBind();
                StudentsPanel.Update();
            }
            catch (Exception ex) {LogFileWrite(ex); }
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
            }
            catch (Exception ex) {LogFileWrite(ex); }
        }

        /// <summary>
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
                ReadingLvlAscButton.Visible = false;
                ReadingLvlDescButton.Visible = true;
            }
            catch (Exception ex) {LogFileWrite(ex); }
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
                NamesAscButton.Visible = false;
                NamesDescButton.Visible = true;
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
            }
            catch (Exception ex) {LogFileWrite(ex); }
        }

        /// <summary>
        ///  Method to search the students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                FillStudentList();
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowClass", "<script>ShowClass()</script>", false);
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
        }

        /// <summary>
        ///  Method to navigate to profile page of the selected student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProfileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["selStudID"] != null)
                    studentController.ClearMyCache(Session["selStudID"].ToString());
                Session["selStudID"] = SelStudid.Value;
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + STUDENTPROFILE);
            }
            catch (Exception ex){LogFileWrite(ex);}
        }

        /// <summary>
        ///  Method to  construct the Student name
        /// </summary>
        /// <param name="stuList"></param>
        /// <returns></returns>
        private List<Student> StudentNameArrangement(List<Student> stuList)
        {
            try
            {
                var fullName = string.Empty;
                var loginName = string.Empty;
                foreach (Student stu in stuList)
                {
                    fullName = string.Concat(stu.FirstName, ' ', stu.LastName);
                    loginName = stu.UserLoginName.Length > 15 ? string.Concat(stu.UserLoginName.Substring(0, 15), "..") : stu.UserLoginName;
                    stu.FullName = (stu.FullNameFlag == null) ? string.Concat(fullName, loginName).Length > 27 ? (string.Concat(fullName.Substring(0, Math.Min(fullName.Length, 23)), ".. ", " (", loginName, ")")) : string.Concat(fullName, " (", loginName, ")") : stu.FullName;
                    stu.FullNameFlag = "FIXED";
                }                
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return stuList;
        }

        /// <summary>
        ///  Sort and return the students list
        /// </summary>
        /// <returns></returns>
        private List<Student> SortAndReturn()
        {
            List<Student> stuList = new List<Student>();
            try
            {
                var sortClassMembers = StudentNameArrangement(Session["AddedStudentList"] as List<Student>);

                string[] name = SearchTextBox.Text.Split(',');
                name = name.Where(s => s != string.Empty).ToArray();
                if (name.Length > 0)
                {                    
                    foreach (string s in name)
                    {
                        var text = s.Trim().ToLower();
                        stuList.AddRange(sortClassMembers.Where(x => x.LastName.Trim().ToLower() == text).Union(
                                         sortClassMembers.Where(x => x.FirstName.Trim().ToLower() == text)).Union(
                                         sortClassMembers.Where(x => x.FirstName.Trim().ToLower().Contains(text))).Union(
                                         sortClassMembers.Where(x => x.LastName.Trim().ToLower().Contains(text))).Union(
                                         sortClassMembers.Where(x => string.Concat(x.FirstName.Trim().ToLower(), ' ', x.LastName.Trim().ToLower()) == text)).Union(
                                         sortClassMembers.Where(x => x.FirstName.Trim().ToLower().StartsWith(text))).Union(
                                         sortClassMembers.Where(x => x.LastName.Trim().ToLower().StartsWith(text))).Union(
                                         sortClassMembers.Where(x => x.UserLoginName.Trim().ToLower().StartsWith(text))).Union(
                                         sortClassMembers.Where(x => x.UserLoginName.Trim().ToLower().Contains(text))).Union(
                                         sortClassMembers.Where(x => x.UserLoginName.Trim().ToLower() == text)).Union(
                                         sortClassMembers.Where(x => string.Concat(x.FirstName, ' ', x.LastName, " (", x.UserLoginName, ")").Trim().ToLower() == text)).Union(
                                         sortClassMembers.Where(x => x.FullName.ToLower().Equals(text))).Union(
                                         sortClassMembers.Where(x => x.FullName.ToLower().Contains(text))));
                        stuList = stuList.Distinct().ToList();
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
    }
}