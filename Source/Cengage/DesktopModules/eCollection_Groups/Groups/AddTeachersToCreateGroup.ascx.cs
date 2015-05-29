using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Web.UI.HtmlControls;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using System.Text;
using _IDCollection = DotNetNuke.Modules.eCollection_Groups.Components.Common.IDCollection;
using DotNetNuke.Common.Utilities;


namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddTeachersToCreateGroup" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To add teachers to create group screen
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------    
    public partial class AddTeachersToCreateGroup : eCollection_GroupsModuleBase
    {
        #region Private Members
        private List<_IDCollection> _teacherList = new List<_IDCollection>();

        private SortDirection TeacherSortDirection
        {
            get
            {
                if (Session["TeacherSortDirection"] == null)
                    Session["TeacherSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["TeacherSortDirection"];
            }
            set { Session["TeacherSortDirection"] = value; }
        }

        private string TeacherSortExpression
        {
            get
            {
                if (Session["TeacherSortExpression"] == null)
                    Session["TeacherSortExpression"] = "TeacherName";
                return (string)Session["TeacherSortExpression"];
            }
            set { Session["TeacherSortExpression"] = value; }
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
                    BuildSelectedItems();
                    SortAndBind(10, 0, 0);
                }
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
                if (TeachersList != null && TeachersList.Count != 0)
                {
                    foreach (_IDCollection idCollection in TeachersList)
                    {
                        //if (idCollection.Id != userID)
                        //{
                        output.AppendFormat("<li class=\"SelectedItem SelectedTeacher\"><span title='{0}'>{2}</span><span title=\'UserID\' style=\'display:none\'>{1}</span><a onclick='javascript:Remove(this)'>x</a></li>", idCollection.Text, idCollection.Id, idCollection.Text.Length > 10 ? idCollection.Text.Substring(0, 9) + " ..." : idCollection.Text);
                        SelectedValueTextBox.Text += idCollection.Id + ",";
                        //}
                    }
                    if (SelectedValueTextBox.Text.Length > 0)
                    {
                        //SelectedValueTextBox.Text = SelectedValueTextBox.Text.Trim().Remove(SelectedValueTextBox.Text.Length - 1, 1);
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
                ConstRowCount = AllOtherTeachers.Count;
                _teacherList = AllOtherTeachers.Take(ConstRowCount).ToList();//Where(u => u.Id != userID).ToList();
                _teacherList.ForEach(u => { if (u.Id.ToString().ToLower() == userID.ToString().ToLower()) { u.Text = "You"; } });
                _teacherList = _teacherList.OrderBy(o => o.Text).ToList();
                if (ConstRowCount > 10)
                {
                    AllotherGroupDivBtn.Style.Add("display", "none");
                    CustomPaging.DisplayPropertyForPage("block");
                }

                switch (TeacherSortExpression)
                {
                    case "TeacherName":
                        _teacherList = _teacherList.Take(take).Skip(pageSize).ToList();
                        break;
                    default:
                        break;
                }

                if (TeacherSortDirection == SortDirection.Descending)
                {
                    _teacherList.Reverse();
                }
                FillTeachersList(_teacherList, ConstRowCount, startCount);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_teacherList"></param>
        /// <param name="totalCount"></param>
        /// <param name="startCount"></param>
        private void FillTeachersList(List<_IDCollection> _teacherList, int totalCount, int startCount)
        {
            try
            {
                if (_teacherList != null & _teacherList.Count > 0)
                {
                    CustomPaging.CreatePagingControl(totalCount, startCount);
                    CustomPaging.PageButtonStyle(startCount);
                    string[] addedTeacher = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    _teacherList.ForEach(x => { x.Checked = false; });
                    if (addedTeacher.Length > 0)
                    {
                        _teacherList.Where(x => addedTeacher.Contains(x.Id.ToString())).ToList().ForEach(x => { x.Checked = true; });
                    }


                }
                else
                {
                    //StudentDiv.Style.Add("display", "none");
                }
                StudentRepeater.DataSource = _teacherList;//.Where(u => u.Id != userID).ToList();
                StudentRepeater.DataBind();
                //CheckALLUpdatePanel.Update();
                //StudentRepeaterUpdatePanel.Update();
                if (_teacherList.Count > 0)
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
        protected void AllOtherGroupButton_Click(object sender, EventArgs e)
        {
            AllotherGroupDivBtn.Style.Add("display", "none");
            CustomPaging.DisplayPropertyForPage("block");
            if (SelectedSubscription != null)
            {
                _teacherList = AllOtherTeachers;

                switch (TeacherSortExpression)
                {
                    case "TeacherName":
                        _teacherList.Sort();
                        break;
                    default:
                        break;
                }

                if (TeacherSortDirection == SortDirection.Descending)
                {
                    _teacherList.Reverse();
                }
                SortAndBind(10, 0, 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SortingButton_Click(object sender, EventArgs e)
        {
            TeacherSortExpression = "TeacherName";
            List<IDCollection> teachercollection = new List<IDCollection>();
            int pageno = CustomPaging.GetCurrentPageNo();
            RepeaterItemCollection myItemCollection;
            myItemCollection = StudentRepeater.Items;
            for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
            {
                IDCollection idcollection = new IDCollection();
                idcollection.Id = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                idcollection.Text = (myItemCollection[Stindex].Controls[4] as Label).Text;
                teachercollection.Add(idcollection);
            }
            if (SortingButton.CommandName == "Ascending")
            {
                TeacherSortDirection = SortDirection.Descending;
                SortingButton.CommandName = "Descending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
                FillTeachersList(teachercollection.OrderByDescending(s => s.Text).ToList(), ConstRowCount, pageno);
            }
            else
            {
                TeacherSortDirection = SortDirection.Ascending;
                SortingButton.CommandName = "Ascending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
                FillTeachersList(teachercollection.OrderBy(s => s.Text).ToList(), ConstRowCount, pageno);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BacktoCreateBtn_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetTeachersbySubscription{0}", int.Parse(Session["Subscription"].ToString())));
            string[] addedTeacher = SelectedValueTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            if (addedTeacher.Length > 0)
            {
                TeachersList = AllOtherTeachers.Where(u => addedTeacher.Contains(u.Id.ToString())).ToList();
            }
            else
            {
                TeachersList = new List<_IDCollection>();
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
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                GroupController.Instance.GetSearchTeacherDetails(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName, int.Parse(Session["Subscription"].ToString()));
                SortAndBind(10, 0, 0);
                if (ConstRowCount > 10)
                    CustomPaging.DisplayPropertyForPage("block");
                else
                    CustomPaging.DisplayPropertyForPage("none");
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

    }
}