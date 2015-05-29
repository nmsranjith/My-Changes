using System;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Linq;
using System.Web.UI;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyGroups" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the groups where the teacher are a part
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class MyGroups : eCollection_TeachersModuleBase
    {

        public bool IsNoGroupExist { get; set; }

        #region Private Members
            private List<Groups> _groupList;
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
        #endregion

        #region Page Events
                /// -----------------------------------------------------------------------------
                /// <summary>
                /// Page_Load runs when the control is loaded
                /// </summary>
                /// -----------------------------------------------------------------------------
                protected void Page_Load(object sender, EventArgs e)
                {            
                    SortAndBind();
                }       
         #endregion

        #region ButtonEvents
                /// <summary>
                /// 
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                protected void ReadingLevelAscButton_Click(object sender, EventArgs e)
                {
                    ReadingLevelAscButton.Visible = false;
                    ReadingLevelDescButton.Visible = true;
                    GroupsSortExpression = "ReadingLevel";
                    GroupsSortDirection = SortDirection.Descending;
                    SortAndBind();
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
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
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
                }
                /// <summary>
                /// 
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                protected void ReadingLevelDescButton_Click(object sender, EventArgs e)
                {
                    ReadingLevelAscButton.Visible = true;
                    ReadingLevelDescButton.Visible = false;
                    GroupsSortExpression = "ReadingLevel";
                    GroupsSortDirection = SortDirection.Ascending;
                    SortAndBind();
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
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
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
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
                    ScriptManager.RegisterStartupScript(Page, GetType(), "LoadClick", "<script>LoadClick()</script>", false);
                    SortAndBind();
                    PMRdSrt.Update(); NmeSrt.Update();
                    GrpListPanel.Update();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
                }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToGroupProfile(object sender, System.Web.UI.WebControls.ListViewCommandEventArgs e)
        {
            Session["EditGroupID"] = int.Parse(((e.Item as ListViewItem).FindControl("classid") as Label).Text);
            Response.Redirect(Globals.NavigateURL(GetTabID(GroupsModule)) + "?pagename=" + GROUPPROFILE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillGroupsList(List<Groups> _Groups)
        {
            if (_Groups != null && _Groups.Count > 0)
            {
                GroupsList.DataSource = _Groups;
                IsNoGroupExist = false;
                GroupsList.DataBind();
            }
            else
            {
                GroupsList.DataSource = null;
                IsNoGroupExist = true;
                GroupsList.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SortAndBind()
        {
            try
            {
                Teacher stu = new Teacher() { UserLoginName = selectedTeacher().UserLoginName, GrpType = 'N', ActionType = "FORTEACHERS", SubscriptionId = int.Parse(Session["Subscription"].ToString()), GrpCacheName = "GetGroupList" };
                _groupList = teacherController.GetGroupNames(stu);
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
                                grpList.AddRange(_groupList.Where(x => x.Name.Trim().ToLower() == text).Union(_groupList.Where(x => x.Name.Trim().ToLower().Contains(text))).Union(_groupList.Where(x => x.Name.Trim().ToLower().StartsWith(text))));
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

                FillGroupsList(_groupList);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}