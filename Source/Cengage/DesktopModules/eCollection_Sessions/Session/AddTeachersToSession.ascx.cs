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
using DotNetNuke.Services.Exceptions;
using _IDCollection = DotNetNuke.Modules.eCollection_Sessions.Components.Common.IDCollection;
using System.Web.UI.HtmlControls;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddTeachersToSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Add teachers to session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddTeachersToSession : eCollection_SessionsModuleBase
    {

        #region Private Members

        private List<_IDCollection> _teachersList;

        private SortDirection TeachersSortDirection
        {
            get
            {
                if (Session["TeachersSortDirection"] == null)
                    Session["TeachersSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["TeachersSortDirection"];
            }
            set { Session["TeachersSortDirection"] = value; }
        }

        private string TeachersSortExpression
        {
            get
            {
                if (Session["TeachersSortExpression"] == null)
                    Session["TeachersSortExpression"] = "Text";
                return (string)Session["TeachersSortExpression"];
            }
            set { Session["TeachersSortExpression"] = value; }
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
                CustomPaging.OnbuttonClicked += new CustomPaging.ButtonClciked(SortAndBind);  
                if (!Page.IsPostBack)
                {
                    DataCache.ClearCache(string.Format("GetTeachers{0}", int.Parse(Session["Subscription"].ToString())));
                    BuildSelectedItems();
                    SortAndBind(10,0,0);  
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
        /// <param name="_teachers"></param>
        /// <param name="totalCount"></param>
        /// <param name="startCount"></param>
        private void FillTeachersList(List<IDCollection> _teachers, int totalCount, int startCount)
        {
            if (_teachers != null && _teachers.Count > 0)
            {
                CustomPaging.CreatePagingControl(totalCount, startCount);
                CustomPaging.PageButtonStyle(startCount);
                //Get Selected Value
                string[] addedTeacher= SelectedValueTeacherTextBox.Text.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                //Set false for all the data
                _teachers.ForEach(x => { x.Checked = false; });
                if (addedTeacher.Length > 0)
                {
                    //Set true for selected data
                    _teachers.Where(x => addedTeacher.Contains(x.Id.ToString())).ToList().ForEach(x => { x.Checked = true; });
                }
                if (!(ConstRowCount > 10))
                {
                    CustomPaging.DisplayPropertyForPage("none");
                }
                TeacherList.DataSource = _teachers.ToList();
                TeacherList.DataBind();
            }
            else
            {
                TeacherList.DataSource = null;
                TeacherList.DataBind();
                Page.ClientScript.RegisterStartupScript(GetType(), "showleftline", "javascript:showleftline(0);", true);

                //UpdatePanel2.Update();
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
            TeachersSortExpression = "Text";
            TeachersSortDirection = SortDirection.Descending;
            int pageno = CustomPaging.GetCurrentPageNo();
            RepeaterItemCollection myItemCollection;
            myItemCollection = TeacherList.Items;
            List<IDCollection> teachercollection = new List<IDCollection>();
            for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
            {
                IDCollection idcollection = new IDCollection();
                idcollection.Id = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                idcollection.Text = (myItemCollection[Stindex].Controls[2] as Label).Text;
                teachercollection.Add(idcollection);
            }
            FillTeachersList(teachercollection.OrderByDescending(s => s.Text).ToList(), ConstRowCount, pageno);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NamesDescButton_Click(object sender, EventArgs e)
        {
            NamesDescButton.Visible = false;
            NamesAscButton.Visible = true;
            TeachersSortExpression = "Text";
            TeachersSortDirection = SortDirection.Ascending;
            int pageno = CustomPaging.GetCurrentPageNo();
            RepeaterItemCollection myItemCollection;
            myItemCollection = TeacherList.Items;
            List<IDCollection> teachercollection = new List<IDCollection>();
            for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
            {
                IDCollection idcollection = new IDCollection();
                idcollection.Id = (int.Parse((myItemCollection[Stindex].Controls[1] as Label).Text));
                idcollection.Text = (myItemCollection[Stindex].Controls[2] as Label).Text;
                teachercollection.Add(idcollection);
            }
            FillTeachersList(teachercollection.OrderBy(s => s.Text).ToList(), ConstRowCount, pageno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToSession_Click(object sender, EventArgs e)
        {
            GetSelectedTeachers();
            DataCache.RemoveCache(string.Format("GetTeachers{0}", SelectedSubscriptionId));
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession&returnurl=" + Request.QueryString["returnurl"]);       

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
                SessionController.Instance.GetSearchTeacherDetails(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName, int.Parse(Session["Subscription"].ToString()));
                BuildSelectedItems();
                SortAndBind(10, 0, 0);
                if (ConstRowCount > 10)
                    CustomPaging.DisplayPropertyForPage("block");
                else
                    CustomPaging.DisplayPropertyForPage("none");
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        #endregion

        #region MemberFunctions
        /// <summary>
        /// 
        /// </summary>
        protected void GetSelectedTeachers()
        {
            try
            {
                List<SessionMembers> SelectedGroups = new List<SessionMembers>();

                string[] StudIDs = SelectedValueTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] GroupIDs = SelectedValueGroupTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] TeacherIDs = SelectedValueTeacherTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

                StringBuilder teachername = new StringBuilder();

                foreach (string s in TeacherIDs)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (Teacher lstItems in FullListTeachers)//RepeaterItem rpItm in TeacherList.Items)
                        {
                            if (lstItems.TeacherId == Convert.ToInt32(s))
                            {
                                SessionMembers sessionMembers = new SessionMembers()
                                    {
                                        CUST_SUBS_USER_SK =lstItems.TeacherId,// int.Parse((rpItm.FindControl("TeacherId") as Label).Text),
                                        TeacherName = lstItems.TeacherName,//(rpItm.FindControl("TeacherNameLabel") as Label).Text,
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

                foreach (string s in StudIDs)
                {
                    if (s.Trim().Length > 0)
                    {
                        foreach (SessionMembers sm in GroupsSelected)
                        {
                            if (sm.CUST_SUBS_USER_SK == Convert.ToInt32(s))
                            {
                                bool studentExists = SelectedGroups.Any(e => e.CUST_SUBS_USER_SK == sm.CUST_SUBS_USER_SK);
                                if (!studentExists)
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
        private void BuildSelectedItems()
        {
            StringBuilder output = new StringBuilder();
            List<SessionMembers> SelectedGroups = GroupsSelected;
            foreach (SessionMembers sm in SelectedGroups)
            {
                if (sm.MemberType == "GROUP" && sm.GroupName.Trim().Length > 0)
                {
                    output.AppendFormat("<li class=\"SelectedGroupItem\"><span title='" + sm.GroupName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveGroup(this);\'>x</a></li>", TruncateName(sm.GroupName), sm.GRP_MEM_SK);
                    SelectedValueGroupTextBox.Text += sm.GRP_MEM_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                {
                    output.AppendFormat("<li class=\"SelectedStudentItem\"><span title='" + sm.StudentName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", TruncateName(sm.StudentName), sm.CUST_SUBS_USER_SK);
                    SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                }
                if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                {
                    output.AppendFormat("<li class=\"SelectedTeacherItem\"><span title='" + sm.TeacherName + "'>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveTeacher(this);\'>x</a></li>", TruncateName(sm.TeacherName), sm.CUST_SUBS_USER_SK);
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
                _teachersList = Teachers;
                ConstRowCount = Teachers.Count;
                _teachersList = _teachersList.Take(ConstRowCount).ToList();
                if (ConstRowCount > 10)
                {
                    CustomPaging.DisplayPropertyForPage("block");
                }
                switch (TeachersSortExpression)
                {
                    case "Text":
                        _teachersList = _teachersList.Take(take).Skip(pageSize).ToList();
                        _teachersList.OrderBy(o => o.Text);
                        break;
                    case "PMReading":
                        _teachersList.Sort();
                        break;
                    default:
                        break;
                }

                if (TeachersSortDirection == SortDirection.Descending)
                {
                    _teachersList.Reverse();
                }
                FillTeachersList(_teachersList, ConstRowCount, startCount);
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

        #endregion
    }
}