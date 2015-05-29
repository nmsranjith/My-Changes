using System;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DotNetNuke.Modules.eCollection_Students.Components.Controllers;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyGroups" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the groups where the student are a part
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class MyGroups : eCollection_StudentsModuleBase
    {
        private List<Groups> _groupList;
        public bool IsNoGroupExist { get; set; }
        private SortDirection GroupsSortDirection
        {
            get
            {
                if (Session["GrpsSrtDir"] == null)
                    Session["GrpsSrtDir"] = SortDirection.Ascending;
                return (SortDirection)Session["GrpsSrtDir"];
            }
            set { Session["GrpsSrtDir"] = value; }
        }

        private string GroupsSortExpression
        {
            get
            {
                if (Session["GrpsSrtExp"] == null)
                    Session["GrpsSrtExp"] = "Name";
                return (string)Session["GrpsSrtExp"];
            }
            set { Session["GrpsSrtExp"] = value; }
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
                SortAndBind();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// Sorting all groups in asc order of Reading level of students/// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLevelAscButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReadingLevelAscButton.Visible = false;
                ReadingLevelDescButton.Visible = true;
                GroupsSortExpression = "ReadingLevel";
                GroupsSortDirection = SortDirection.Descending;
                SortAndBind();
                PMRdSrt.Update(); NmeSrt.Update();
                GrpListPanel.Update();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Sorting all groups in asc order of Group names
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
                PMRdSrt.Update(); NmeSrt.Update();
                GrpListPanel.Update();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Sorting all groups in desc order of Reading level of students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLevelDescButton_Click(object sender, EventArgs e)
        {
            try
            {
                ReadingLevelAscButton.Visible = true;
                ReadingLevelDescButton.Visible = false;
                GroupsSortExpression = "ReadingLevel";
                GroupsSortDirection = SortDirection.Ascending;
                SortAndBind();
                PMRdSrt.Update(); NmeSrt.Update();
                GrpListPanel.Update();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Sorting all groups in desc order of Group names
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
                PMRdSrt.Update(); NmeSrt.Update();
                GrpListPanel.Update();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Group search event
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
                PMRdSrt.Update(); NmeSrt.Update();
                ScriptManager.RegisterStartupScript(Page, GetType(), "LoadClick", "<script>LoadClick()</script>", false);
                GrpListPanel.Update();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
        }

        /// <summary>
        /// Group Search textbox text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GrpSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchTextBox.Text.Trim() == string.Empty)
                {
                    UpdatePanel3.Update();
                    MidleftLine.Visible = true;
                    SortAndBind();
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// To navigate to the selected group profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToGroupProfile(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            try
            {
                Session["EditGroupID"] = int.Parse(((e.Item as ListViewItem).FindControl("classid") as Label).Text);
               // Response.Redirect(Globals.NavigateURL(GetTabID(GroupsModule)) + "?pagename=" + GROUPPROFILE);
                Response.Write("<script>");
                Response.Write("window.open('" + Globals.NavigateURL(GetTabID(GroupsModule)) + "?pagename=" + GROUPPROFILE + "','_blank')");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// To navigate to the selected group profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProfileButton_Click(object sender, EventArgs e)
        {
            Session["EditGroupID"] = Null.SetNullInteger((sender as Button).CommandArgument);
            Response.Redirect(Globals.NavigateURL(GetTabID(GroupsModule)) + "?pagename=" + GROUPPROFILE);                
        }

        /// <summary>
        ///  Fill all Groups of the student
        /// </summary>
        /// <param name="_groups"></param>
        private void FillGroupsList(List<Groups> _groups)
        {
            try
            {
                if (_groups != null && _groups.Count > 0)
                {
                    GroupsList.DataSource = _groups;
                    GroupsList.DataBind();
                    IsNoGroupExist = false;
                }
                else
                {
                    GroupsList.DataSource = null;
                    GroupsList.DataBind();
                    IsNoGroupExist = true;
                }
                if (_groups.Count > 0)
                    MidleftLine.Visible = true;
                else
                    MidleftLine.Visible = false;
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        ///  Sort and bind the groups and classes
        /// </summary>
        private void SortAndBind()
        {
            try
            {
                Student stu = new Student() { UserLoginName = selectedStudent().UserLoginName, GrpType = 'N', ActionType = "FORSTUDENTS", SubscriptionId = int.Parse(Session["Subscription"].ToString()), GrpCacheName = "GetGroupList" };
                _groupList = studentController.GetGroupNames(stu);
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
                        if (grpList.Count == 0)
                            MidleftLine.Visible = false;
                        else
                            MidleftLine.Visible = true;
                        break;
                    default:
                        break;
                }

                switch (GroupsSortExpression)
                {
                    case "Name":
                        _groupList.Sort(x => x.Name);
                        break;
                    case "ReadingLevel":
                        _groupList.Sort(x => x.ReadingLevel);
                        break;
                    default:
                        break;
                }

                if (GroupsSortDirection == SortDirection.Descending)
                {
                    _groupList.Reverse();
                }
                GrpCount.Value = _groupList.Count.ToString();
                FillGroupsList(_groupList);
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

    }
}