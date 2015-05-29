using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using System.Web.UI.HtmlControls;
using _Groups = DotNetNuke.Modules.eCollection_Groups.Components.Groups;
using System.Text;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common.Utilities;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //  This screen lists all the groups/classes.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupList : eCollection_GroupsModuleBase
    {
        #region Private Members
        private List<_Groups> _groupList;
        private List<_Groups> _classList;

        private SortDirection GroupsSortDirection
        {
            get
            {
                if (Session["GroupsSortDirection"] == null)
                    Session["GroupsSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["GroupsSortDirection"];
            }
            set { Session["GroupsSortDirection"] = value; }
        }

        private string GroupsSortExpression
        {
            get
            {
                if (Session["GroupsSortExpression"] == null)
                    Session["GroupsSortExpression"] = "Name";
                return (string)Session["GroupsSortExpression"];
            }
            set { Session["GroupsSortExpression"] = value; }
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
                if (!IsPostBack)
                {
                    StudentList = null;
                    TeachersList = null;
                    EditClassId = null;
                    EditGroupId = null;
                    SelectedSubscription = null;
                    GroupName = null;
                    SelectedID = null;
                    MergeClassId = null;
                    AllOther = false;
                    DataCache.RemoveCache(string.Format("GetGroupList{0}", LoginName + int.Parse(Session["Subscription"].ToString())));
                    DataCache.RemoveCache(string.Format("GetClassList{0}", LoginName + int.Parse(Session["Subscription"].ToString())));
                    DataCache.RemoveCache(string.Format("GetStudentsBySubcription{0}", int.Parse(Session["Subscription"].ToString())));
                    DataCache.RemoveCache(string.Format("GetTeachersbySubscription{0}", int.Parse(Session["Subscription"].ToString())));
                    SortAndBind();
                    Type = false;
                    Session["MergeClassId"] = null;
                    Session["GroupsSortDirection"] = null;
                    Session["GroupsSortExpression"] = null;
                    Session["userID"] = null;
                    Session["GrpLoginUserName"] = null;
                }                
            }            
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassProfileButton_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);
            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            GroupName = (item.FindControl("ClassNameSpan") as Label).Text;
            Session["EditGroupID"] = null;
            EditClassId = int.Parse((item.FindControl("classid") as Label).Text);
            Response.RedirectPermanent(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GroupsProfileButton_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);
            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            Session["EditClassID"] = null;
            EditGroupId = int.Parse((item.FindControl("groupid") as Label).Text);
            GroupName = (item.FindControl("GroupNameSpan") as Label).Text;
            Response.RedirectPermanent(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AllotherGroupProfileButton_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);
            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            Session["EditClassID"] = null;
            EditGroupId = int.Parse((item.FindControl("groupid") as Label).Text);
            GroupName = (item.FindControl("AllotherGroupNameSpan") as Label).Text;
            Response.RedirectPermanent(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AllotherClassProfileButton_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);
            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            Session["EditClassID"] = null;
            EditClassId = int.Parse((item.FindControl("OtherClassId") as Label).Text);
            GroupName = (item.FindControl("AllotherClassNameSpan") as Label).Text;
            Response.RedirectPermanent(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SortingButton_Click(object sender, EventArgs e)
        {
            GroupsSortExpression = "Name";
            if (SortingButton.CommandName == "Ascending")
            {
                GroupsSortDirection = SortDirection.Descending;
                SortingButton.CommandName = "Descending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
            }
            else
            {
                GroupsSortDirection = SortDirection.Ascending;
                SortingButton.CommandName = "Ascending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
            }
            SortAndBind();            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLevelButton_Click(object sender, EventArgs e)
        {
            GroupsSortExpression = "ReadingLevel";
            if (ReadingLevelButton.CommandName == "Ascending")
            {
                GroupsSortDirection = SortDirection.Descending;
                ReadingLevelButton.CommandName = "Descending";
                ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
            }
            else
            {
                GroupsSortDirection = SortDirection.Ascending;
                ReadingLevelButton.CommandName = "Ascending";
                ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
            }
            SortAndBind();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SortAndBind()
        {
            OnTextChangeSort();
            FillGroupsList(_groupList);
            FillClassList(_classList);
            AllOther = true;
            GroupLists = null;
            ClassList = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnTextChangeSort()
        {
            _groupList = GroupLists.ToList();
            _classList = ClassList.ToList();
            if (_groupList.Count == 0 && _classList.Count == 0)
            {
                GroupsMainDiv.Style.Add("Display", "none");
                MessageOuterDiv.Style.Add("Display", "block");
                Message1.Text = Constants.NOGROUPINFO;
                CreateLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATEGROUP;
            }
            else
            {
                AllUpdates.Visible = true;
                MessageOuterDiv.Style.Add("Display", "none");
            }            
            switch (GroupsSortExpression)
            {
                case "Name":
                    _groupList.Sort();
                    _classList.Sort();
                    break;
                case "ReadingLevel":
                    _groupList.Sort(_Groups.GroupsReadingLevelSorter);
                    _classList.Sort(_Groups.GroupsReadingLevelSorter);
                    break;
                case "DropDown":
                    GroupsSortExpression = "";
                    break;
                default:
                    break;
            }
            if (GroupsSortDirection == SortDirection.Descending)
            {
                _groupList.Reverse();
                _classList.Reverse();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Classes"></param>
        private void FillClassList(List<_Groups> _Classes)
        {
            try
            {
                if (_Classes != null && _Classes.Count > 0)
                {
                    string[] selectedClsIDArray = selectedClsID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    _Classes.ForEach(x => { x.Checked = false; });
                    if (selectedClsIDArray.Length > 0)
                    {
                        _Classes.Where(x => selectedClsIDArray.Contains(x.GroupId.ToString())).ToList().ForEach(x => { x.Checked = true; });
                    }
                    List<Components.Groups> classClone = _Classes;
                    classClone.ForEach(x =>
                    {
                        string teacherName = x.TeacherName;
                        if (x.GpType.Trim().Contains("1"))
                        {
                            if (teacherName.Split(',').Length != 2)
                            {
                                x.TeacherName = (teacherName.Length > 30 ? teacherName.Substring(0, 30).TrimEnd(',') + "..." : teacherName.TrimEnd(','));
                            }
                            else
                            {
                                x.TeacherName = ("You and " + teacherName.Replace("You", "").Trim(',')).Length > 30 ? ("You and " + teacherName.Replace("You" + ',', "").Trim(',')).Substring(0, 30).TrimEnd(',') + "..." : ("You and " + teacherName.Replace("You" + ',', "").Trim(','));
                            }
                        }
                        else
                        {
                            x.TeacherName = teacherName.Length > 30 ? teacherName.Substring(0, 30).TrimEnd(',') + "..." : teacherName.TrimEnd(',');
                        }
                    });


                    List<_Groups> CurrentLoginUserInGroups = classClone.Where(u => u.GpType.Trim().Contains("1")).ToList();
                    ClassRepeater.DataSource = CurrentLoginUserInGroups;
                    ClassRepeater.DataBind();
                    List<_Groups> CurrentLoginUserOutGroups = classClone.Except(CurrentLoginUserInGroups).ToList();
                    string[] selectedAllGrpIDArray = selectedAllClsID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    
                    if (selectedAllGrpIDArray.Length > 0)
                        CurrentLoginUserOutGroups.Where(x => selectedAllGrpIDArray.Contains(x.GroupId.ToString())).ToList().ForEach(x => { x.Checked = true; });
                    AllotherClassRepeater.DataSource = CurrentLoginUserOutGroups;
                    AllotherClassRepeater.DataBind();
                    if (AllOtherClassToggFlag.Value == "true")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideAllOtherClasses", "<script>$('#AllotherClassContent').attr('style','display:none;');$('#AllOtherClassButton').removeClass('AllOtherGroupBtn AllOtherGroupDnBtn').addClass('AllOtherGroupBtn');</script>", false);
                        AllOtherClassToggFlag.Value = "false";
                    }
                    if (CurrentLoginUserOutGroups.Count == 0)
                    {
                        AllOtherClassBtnDiv.Style.Add("display", "none");
                        AllotherClassRepeater.DataSource = null;
                        AllotherClassRepeater.DataBind();
                        AllotherClassRepeater.Visible = false;
                    }
                    else
                    {
                        AllOtherClassBtnDiv.Style.Add("display", "block");
                        AllotherClassRepeater.Visible = true;
                    }
                }
                else
                {
                    ClassRepeater.DataSource = null;
                    ClassRepeater.DataBind();
                    AllOtherClassBtnDiv.Style.Add("display", "none");
                    AllotherClassRepeater.Visible = false;
                }
                if (_Classes.Count == 0 || ClassRepeater.Items.Count == 0)
                    ClassContainer.Style.Add("display", "none");
                else
                    ClassContainer.Style.Add("display", "block");
            }
            catch (Exception ex) { LogFileWrite(ex); }
              
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Groups"></param>
        private void FillGroupsList(List<_Groups> _Groups)
        {
            try
            {
                if (_Groups != null && _Groups.Count > 0)
                {
                    string[] selectedGrpIDArray = selectedGrpID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                    _Groups.ForEach(x => { x.Checked = false; });
                    if (selectedGrpIDArray.Length > 0)
                    {
                        _Groups.Where(x => selectedGrpIDArray.Contains(x.GroupId.ToString())).ToList().ForEach(x => { x.Checked = true; });
                    }
                    List<Components.Groups> groupClone = _Groups;
                    groupClone.ForEach(x =>
                    {
                        string teacherName = x.TeacherName;
                        if (x.GpType.Trim().Contains("1"))
                        {
                            if (teacherName.Split(',').Length != 2)
                            {
                                x.TeacherName = (teacherName.Length > 30 ? teacherName.Substring(0, 30).TrimEnd(',') + "..." : teacherName.TrimEnd(','));
                            }
                            else
                            {
                                x.TeacherName = ("You and" + teacherName.Replace("You", "").Trim(',')).Length > 30 ? ("You and" + teacherName.Replace("You" + ',', "").Trim(',')).Substring(0, 30).TrimEnd(',') + "..." : ("You and" + teacherName.Replace("You" + ',', "").Trim(','));
                            }
                        }
                        else
                        {
                            x.TeacherName = teacherName.Length > 30 ? teacherName.Substring(0, 30).TrimEnd(',') + "..." : teacherName.TrimEnd(',');
                        }                        
                    });
                    List<_Groups> CurrentLoginUserInGroups = groupClone.Where(u => u.GpType.Trim().Contains("1")).ToList();
                    GroupsRepeater.DataSource = CurrentLoginUserInGroups;
                    GroupsRepeater.DataBind();
                    List<_Groups> CurrentLoginUserOutGroups = groupClone.Except(CurrentLoginUserInGroups).ToList();
                    string[] selectedAllGrpIDArray = selectedAllGrpID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

                    
                    if (selectedAllGrpIDArray.Length > 0)
                        CurrentLoginUserOutGroups.Where(x => selectedAllGrpIDArray.Contains(x.GroupId.ToString())).ToList().ForEach(x => { x.Checked = true; });
                    AllotherGroupRepeater.DataSource = CurrentLoginUserOutGroups;
                    AllotherGroupRepeater.DataBind();


                    if (AllOtherGroupToggFlag.Value == "true")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideAllOtherGroups", "<script>$('#AllotherGroupContent').attr('style','display:none;');$('#AllOtherGroupButton').removeClass('AllOtherGroupBtn AllOtherGroupDnBtn').addClass('AllOtherGroupBtn');</script>", false);
                        AllOtherGroupToggFlag.Value = "false";
                    }
                    if (CurrentLoginUserOutGroups.Count == 0)
                    {
                        AllOtherGroupBtnDiv.Style.Add("display", "none");
                        AllotherGroupRepeater.DataSource = null;
                        AllotherGroupRepeater.DataBind();
                        AllotherGroupRepeater.Visible = false;
                    }
                    else
                    {
                        AllOtherGroupBtnDiv.Style.Add("display", "block");
                        AllotherGroupRepeater.Visible = true;
                    }
                }
                else
                {
                    GroupsRepeater.DataSource = null;
                    GroupsRepeater.DataBind();
                    AllOtherGroupBtnDiv.Style.Add("display", "none");
                    AllotherGroupRepeater.Visible = false;
                }
                if (_Groups.Count == 0 || GroupsRepeater.Items.Count == 0)
                    GroupsContainer.Style.Add("display", "none");
                else
                    GroupsContainer.Style.Add("display", "block");
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("ClassNameSpan");
            Label teacherLabel = (Label)e.Item.FindControl("TeacherNameLabel");
            Label classID = (Label)e.Item.FindControl("classid");
            label.Text = label.Text.Length > 30 ? label.Text.Substring(0, 30) + "..." : label.Text;
            HtmlInputCheckBox checkbox = (HtmlInputCheckBox)e.Item.FindControl("ClassCheckBoxes");        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void GroupsRepeater_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("GroupNameSpan");
            Label teacherLabel = (Label)e.Item.FindControl("TeacherNameLabel");
            Label groupID = (Label)e.Item.FindControl("groupid");
            label.Text = label.Text.Length > 30 ? label.Text.Substring(0, 30) + "..." : label.Text;
            HtmlInputCheckBox checkbox = (HtmlInputCheckBox)e.Item.FindControl("GroupCheckBoxes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void AllotherGroupRepeater_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("AllotherGroupNameSpan");
            Label teacherLabel = (Label)e.Item.FindControl("TeacherNameLabel");
            Label groupID = (Label)e.Item.FindControl("groupid");
            label.Text = label.Text.Length > 30 ? label.Text.Substring(0, 30) + "..." : label.Text;
            HtmlInputCheckBox checkbox = (HtmlInputCheckBox)e.Item.FindControl("AllotherGroupCheckBoxes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void AllotherClassRepeater_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("AllotherClassNameSpan");
            Label teacherLabel = (Label)e.Item.FindControl("OCTeacherNameLabel");
            Label groupID = (Label)e.Item.FindControl("OtherClassId");
            label.Text = label.Text.Length > 30 ? label.Text.Substring(0, 30) + "..." : label.Text;
            HtmlInputCheckBox checkbox = (HtmlInputCheckBox)e.Item.FindControl("AllotherClassCheckBoxes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {

            GroupController.Instance.GetSearchGroup(SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName, int.Parse(Session["Subscription"].ToString()));
            AllOther = true;            
            SortAndBind();
            ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>",false);
        }
    }
}