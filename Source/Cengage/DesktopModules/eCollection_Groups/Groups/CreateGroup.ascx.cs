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
using _IDCollection = DotNetNuke.Modules.eCollection_Groups.Components.Common.IDCollection;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using DotNetNuke.Modules.eCollection_Groups.Components.ExceptionHandling;
using DotNetNuke.Modules.eCollection_Students.Components.Controllers;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateGroup" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   This screen is used for creating Classes/Groups
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public partial class CreateGroup : eCollection_GroupsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataCache.RemoveCache(string.Format("GetStudentsBySubcription{0}", int.Parse(Session["Subscription"].ToString())));
                    DataCache.RemoveCache(string.Format("GetTeachersbySubscription{0}", int.Parse(Session["Subscription"].ToString())));
                    DataCache.RemoveCache(string.Format("GetStudentByGroup{0}", LoginName));
                    if (SelectedSubscription != null && SelectedSubscription != string.Empty)
                    {
                        string selectedIndex = "1";                       
                    }
                    if (EditGroupId != -1 || EditClassId != -1)
                    {
                        if (StudentList == null || TeachersList == null)
                            EditGroup();
                        CreateGroupButton.Text = "SAVE GROUP";
                    }
                    else
                    {
                        GroupName = string.Empty;
                        EditClassId = null;
                        EditGroupId = null;
                        if (TeachersList == null)
                        {
                            List<IDCollection> currentUserCollection = new List<IDCollection>();
                            IDCollection currentUser = new IDCollection() { Id = userID, Text = "You" };
                            currentUserCollection.Add(currentUser);
                            TeachersList = currentUserCollection;
                        }
                            if (MergeClassId != null && SelectedID == null)
                            {
                                // clicked from merge group
                                if (StudentList == null || TeachersList == null)
                                    MergeGroups();
                            }
                        
                        CreateGroupButton.Text = "CREATE GROUP";
                    }
                    GroupNameTextBox.Text = GroupName;
                    ChangeCheckBoxImg();
                    if (TeachersList != null && TeachersList.Count != 0)
                    {
                        CreateGroupContentDiv.Style.Add("display", "block");
                        TeachersList.ForEach(u => { if (u.Id.ToString() == userID.ToString()) { u.Text = "You"; } });
                        TeacherDetailsRepeater.DataSource = TeachersList;//.Where(u => u.Id != userID);
                        TeacherDetailsRepeater.DataBind();
                        CreateGroupBtnHolder.Attributes.Add("class", "ActiveAddButtonsHolder CreateButtonGradient");
                        CreateGroupButton.Enabled = true;
                        CreateGroupButton.CssClass = "BtnStyle";
                    }
                    else
                    {
                        if (StudentList != null && StudentList.Count != 0)
                        {
                            CreateGroupBtnHolder.Attributes.Add("class", "DisabledAddButtonHolder CreateButtonGradient");
                            CreateGroupButton.Enabled = false;
                            CreateGroupButton.CssClass = "BtnStyle DbldBtn";
                        }
                        TeacherDetailsDiv.Style.Add("display", "none");
                    }

                    if (StudentList != null && StudentList.Count != 0)
                    {
                        CreateGroupBtnHolder.Attributes.Add("class", "ActiveAddButtonsHolder CreateButtonGradient");
                        CreateGroupButton.Enabled = true;
                        CreateGroupButton.CssClass = "BtnStyle";
                        CreateGroupContentDiv.Style.Add("display", "block");
                        StudentDetailsRepeater.DataSource = StudentList;
                        StudentDetailsRepeater.DataBind();
                        StudentDetailsDiv.Style.Add("display", "block");
                    }
                    else
                    {
                        StudentDetailsDiv.Style.Add("display", "none");
                    }

                    if (StudentList != null || TeachersList != null)
                    {
                        RisePopUp.Value = SelectedSubscription;
                    }
                }
                catch (Exception ex)
                {
                    this.Messages.ShowError(ex.Message);
                    LogFileWrite(ex);
                }
            }
            Messages.ClearMessages();

        }

        /// <summary>
        /// 
        /// </summary>
        private void ChangeCheckBoxImg()
        {
            if (!Type)
            {
                CheckBoxDiv.Attributes.Add("class", "groupuncheckboximg groupcheckboximg");
                ClassCheckHiddenField.Value = "false";
            }
            else
            {
                CheckBoxDiv.Attributes.Add("class", "groupcheckboximg");
                ClassCheckHiddenField.Value = "true"; 
            }
            ClassCheckBox.Checked = Type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TeacherDetailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("TeacherNameLabel");
            label.Text = label.Text.Length > 35 ? label.Text.Substring(0, 35) + "..." : label.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StudentDetailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("StudentNameLabel");
            label.Text = label.Text.Length > 35 ? label.Text.Substring(0, 35) + "..." : label.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelCreateGroup_Click(object sender, EventArgs e)
        {
            StudentList = null;
            TeachersList = null;
            EditClassId = null;
            EditGroupId = null;
            SelectedSubscription = null;
            GroupName = null;
            SelectedID = null;
            MergeClassId = null;
            Session["ClassType"] = null;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Students> SelectedstudentList = new List<Students>();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentDetailsRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    Students studentIDCollection = new Students();
                    studentIDCollection.UserID = (int.Parse((myItemCollection[Stindex].FindControl("studentid") as Literal).Text));
                    studentIDCollection.StudentNames = (myItemCollection[Stindex].FindControl("StudentNameLabel") as Label).Text;
                    SelectedstudentList.Add(studentIDCollection);
                }
                StudentList = SelectedstudentList;
                if (SelectedstudentList.Count > 0)
                    RisePopUp.Value = SelectedSubscription;
                GroupName = GroupNameTextBox.Text;
                Type = ClassCheckBox.Checked;
                ChangeCheckBoxImg();
                if (EditGroupId != -1 || EditClassId != -1)
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDSTUDENTTOEDITGROUP);
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDSTUDENTTOCREATEGROUP);
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
        protected void AddTeachersButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<_IDCollection> SelectedTeacherID = new List<_IDCollection>();
                RepeaterItemCollection myItemCollection;
                myItemCollection = TeacherDetailsRepeater.Items;
                for (int Teindex = 0; Teindex < myItemCollection.Count; Teindex++)
                {
                    _IDCollection teacherIDCollection = new _IDCollection();
                    teacherIDCollection.Id = (int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text));
                    teacherIDCollection.Text = (myItemCollection[Teindex].FindControl("TeacherNameLabel") as Label).Text;
                    SelectedTeacherID.Add(teacherIDCollection);
                }
                TeachersList = SelectedTeacherID;
              
                if (SelectedTeacherID.Count > 0)
                    RisePopUp.Value = SelectedSubscription;
                GroupName = GroupNameTextBox.Text;
                Type = ClassCheckBox.Checked;
                ChangeCheckBoxImg();
                if (EditGroupId != -1 || EditClassId != -1)
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDTEACHERTOEDITGROUP);
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDTEACHERTOCREATEGROUP);
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
        protected void ClickTeacherDelete(object sender, EventArgs e)
        {
           TeacherDelete.Text = "delete";
           TeacherDeleteImgButton__Click(Session["obj"] as object,e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TeacherDeleteImgButton__Click(object sender, EventArgs e)
        {
            try
            {
                Session["obj"] = sender;
                //Get the reference of the clicked button.
                Button button = (sender as Button);

                //Get the Repeater Item reference
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                int teacherID = int.Parse((item.FindControl("teacherid") as Literal).Text);
                //if (teacherID != userID)
                //{
                List<_IDCollection> SelectedTeacherID = new List<_IDCollection>();
                RepeaterItemCollection myItemCollection;
                myItemCollection = TeacherDetailsRepeater.Items;

                for (int Teindex = 0; Teindex < myItemCollection.Count; Teindex++)
                {
                    if (CreateGroupButton.Text == "CREATE GROUP")
                    {
                        if (TeacherDelete.Text == string.Empty && (int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text)) == teacherID && ((myItemCollection[Teindex].FindControl("TeacherNameLabel") as Label).Text.ToLower()) == "you")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "DeleteOwnerMessage", "<script>DeleteOwnerMessage()</script>", false);
                            return;
                        }
                    }
                    else
                    {
                        if (EditGroupId != -1)
                        {
                            if (TeacherDelete.Text == string.Empty && (int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text)) == teacherID && GroupController.Instance.GetGroupOwnerID(EditGroupId.Value) == teacherID)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "DeleteOwnerMessage", "<script>DeleteOwnerMessage()</script>", false);
                                return;
                            }
                        }
                        if (EditClassId != -1)
                        {
                            if (TeacherDelete.Text == string.Empty && (int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text)) == teacherID && GroupController.Instance.GetGroupOwnerID(EditClassId.Value) == teacherID)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "DeleteOwnerMessage", "<script>DeleteOwnerMessage()</script>", false);
                                return;
                            }
                        }
                    }
                    if ((int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text)) != teacherID)
                    {
                        _IDCollection teacherIDCollection = new _IDCollection();
                        teacherIDCollection.Id = (int.Parse((myItemCollection[Teindex].FindControl("teacherid") as Literal).Text));
                        teacherIDCollection.Text = (myItemCollection[Teindex].FindControl("TeacherNameLabel") as Label).Text;
                        SelectedTeacherID.Add(teacherIDCollection);
                    }

                }
                TeachersList = SelectedTeacherID;

                TeacherDetailsRepeater.DataSource = TeachersList;
                TeacherDetailsRepeater.DataBind();


                CreateGroupBtnHolder.Attributes.Add("class", "ActiveAddButtonsHolder CreateButtonGradient");
                CreateGroupButton.Enabled = true;
                CreateGroupButton.CssClass = "BtnStyle";

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
        protected void StudentDeleteImgButton__Click(object sender, EventArgs e)
        {
            try
            {
                //Get the reference of the clicked button.
                Button button = (sender as Button);

                //Get the Repeater Item reference
                RepeaterItem item = button.NamingContainer as RepeaterItem;
                int studentID = int.Parse((item.FindControl("studentid") as Literal).Text);
                List<Students> SelectedstudentID = new List<Students>();
                RepeaterItemCollection myItemCollection;
                myItemCollection = StudentDetailsRepeater.Items;
                for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
                {
                    if ((int.Parse((myItemCollection[Stindex].FindControl("studentid") as Literal).Text)) != studentID)
                    {
                        Students studentIDCollection = new Students();
                        studentIDCollection.UserID = (int.Parse((myItemCollection[Stindex].FindControl("studentid") as Literal).Text));
                        studentIDCollection.StudentNames = (myItemCollection[Stindex].FindControl("StudentNameLabel") as Label).Text;
                        SelectedstudentID.Add(studentIDCollection);
                    }
                }
                StudentList = SelectedstudentID;
                StudentDetailsRepeater.DataSource = StudentList;
                StudentDetailsRepeater.DataBind();
                //StudentDetailsUpdatePanel.Update();
                //MessageUpdatePanel.Update();
                if (StudentList.Count == 0 && TeacherDetailsRepeater.Items.Count == 0)
                {
                    CreateGroupBtnHolder.Attributes.Add("class", "DisabledAddButtonHolder CreateButtonGradient");
                    CreateGroupButton.Enabled = false;
                    CreateGroupButton.CssClass = "BtnStyle DbldBtn";
                    StudentDetailsDiv.Style.Add("display", "none");
                }
                else
                {
                    CreateGroupBtnHolder.Attributes.Add("class", "ActiveAddButtonsHolder CreateButtonGradient");
                    CreateGroupButton.Enabled = true;
                    CreateGroupButton.CssClass = "BtnStyle";
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
        protected void CreateGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (GroupNameTextBox.Text.Trim() == string.Empty)
                {
                    Messages.ShowWarning(GetMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_GRPNAMEVALIDATION));
                    return;
                }
               
                Components.Groups groups = new Components.Groups();
                groups.CustomerSubId = int.Parse(Session["Subscription"].ToString());
                groups.Name = GroupNameTextBox.Text;
                groups.LoginName = LoginName;
                if (ClassCheckBox.Checked)
                {
                    groups.GroupType = 'C';
                }
                else
                {
                    groups.GroupType = 'N';
                }
                groups.CreatedOnDate = DateTime.Now;
                groups.ActiveFlag = 'Y';
                groups.DateCreated = DateTime.Now;
                if (TeachersList != null && TeachersList.Count != 0)
                {
                   groups.TeachersList = TeachersList;                    
                }
                else if (TeacherDetailsRepeater.Items.Count > 0)
                {                 
                    List<IDCollection> newTeachersList = new List<_IDCollection>();
                    foreach (RepeaterItem a in TeacherDetailsRepeater.Items)
                    {
                        string id=(a.Controls[3] as Literal).Text;
                        string name=(a.Controls[5] as Label).Text;
                        newTeachersList.Add(new IDCollection() { Id=Null.SetNullInteger(id),Text=name });
                    }
                    groups.TeachersList = newTeachersList;
                }

                if (StudentList != null && StudentList.Count != 0)
                {
                    groups.StudentList = StudentList;
                }
                else if (StudentDetailsRepeater.Items.Count > 0)
                {
                    List<Students> newStudentList = new List<Students>();
                    foreach (RepeaterItem a in StudentDetailsRepeater.Items)
                    {
                        string id = (a.Controls[3] as Literal).Text;
                        string name = (a.Controls[5] as Label).Text;
                        Students studentIDCollection = new Students();                       
                        newStudentList.Add(new Students() { UserID = Null.SetNullInteger(id), StudentNames = name });                        
                    }
                    groups.StudentList = newStudentList;
                }

                Components.GroupController Gc = GroupController.Instance;
                if (EditGroupId != -1 || EditClassId != -1)
                {
                    List<_IDCollection> TeacherListIDCollection = null;
                    List<Students> StudentListIDCollection = null;
                    List<Students> DeletedStudentIDCollection = null;
                    List<_IDCollection> DeletedTeacherIDCollection = null;


                    AllOther = true;
                    int customerSubSK = 0;
                    if (EditGroupId != -1)
                    {
                        TeacherListIDCollection = GroupLists.Where(u => u.GroupId == EditGroupId).ToList().FirstOrDefault().TeachersList;
                        List<string> teacherIDCol = TeacherListIDCollection.Select(u => u.Id.ToString()).ToList<string>();
                        groups.TeachersList = TeachersList.Where(u => (!teacherIDCol.Contains(u.Id.ToString()))).ToList();
                        StudentListIDCollection = GroupLists.Where(u => u.GroupId == EditGroupId).FirstOrDefault().StudentList;
                        List<string> studentIDCol = StudentListIDCollection.Select(u => u.UserID.ToString()).ToList<string>();
                        groups.StudentList = StudentList.Where(u => (!studentIDCol.Contains(u.UserID.ToString()))).ToList();
                        customerSubSK = GroupLists.Where(u => u.GroupId == EditGroupId).FirstOrDefault().CustomerSubId.Value;
                        groups.GroupId = EditGroupId.Value;
                    }
                    if (EditClassId != -1)
                    {
                        TeacherListIDCollection = ClassList.Where(u => u.GroupId.Equals(EditClassId)).FirstOrDefault().TeachersList;
                        List<string> teacherIDCol = TeacherListIDCollection.Select(u => u.Id.ToString()).ToList<string>();
                        groups.TeachersList = TeachersList.Where(u => (!teacherIDCol.Contains(u.Id.ToString()))).ToList();
                        StudentListIDCollection = ClassList.Where(u => u.GroupId.Equals(EditClassId)).FirstOrDefault().StudentList;
                        List<string> studentIDCol = StudentListIDCollection.Select(u => u.UserID.ToString()).ToList<string>();
                        groups.StudentList = StudentList.Where(u => (!studentIDCol.Contains(u.UserID.ToString()))).ToList();

                        customerSubSK = ClassList.Where(u => u.GroupId.Equals(EditClassId)).FirstOrDefault().CustomerSubId.Value;
                        groups.GroupId = EditClassId.Value;
                    }                   
                    
                    if (StudentList != null)
                    {
                        DeletedStudentIDCollection = (from InactiveStudent in StudentListIDCollection
                                                      where !(from activeStudent in StudentList
                                                              select activeStudent.UserID).Contains(InactiveStudent.UserID)
                                                      select InactiveStudent).ToList();
                    }
                    if (TeachersList != null)
                    {
                        DeletedTeacherIDCollection = (from InactiveTeacher in TeacherListIDCollection
                                                      where !(from activeTeacher in TeachersList
                                                              select activeTeacher.Id).Contains(InactiveTeacher.Id)
                                                      select InactiveTeacher).ToList();                       
                    }

                    if (TeacherDetailsRepeater.Items.Count == 0)
                    {
                        Messages.ShowWarning("A teacher is mandatory. Please enter a teacher, then click Create Group.");
                        return;
                    }
                    if (StudentDetailsRepeater.Items.Count == 0 && TeacherDetailsRepeater.Items.Count == 0)
                    {
                        Messages.ShowWarning(GetMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_MEMBERVALIDATION));
                        return;
                    }
                    if (GroupName.ToLower() != GroupNameTextBox.Text.Trim().ToLower() && GroupNameTextBox.Text.Trim() != string.Empty)
                    {
                        if (GroupController.Instance.ValidateGroupName(GroupNameTextBox.Text.Trim(), groups.GroupType, int.Parse(Session["Subscription"].ToString())))
                        {
                            Messages.ShowWarning(GetMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_GRPNAMEEXIST));
                            GroupNameTextBox.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            GroupController.Instance.UpdateMembers(groups, DeletedTeacherIDCollection, DeletedStudentIDCollection, customerSubSK);
                            Messages.ClearMessages();
                        }
                    }
                    else
                    {
                        GroupController.Instance.UpdateMembers(groups, DeletedTeacherIDCollection, DeletedStudentIDCollection, customerSubSK);
                        Messages.ClearMessages();
                    }
                }
                else
                {                   
                    if (!GroupController.Instance.ValidateGroupName(GroupNameTextBox.Text.Trim(), groups.GroupType, int.Parse(Session["Subscription"].ToString())))
                    {
                        
                        if (TeacherDetailsRepeater.Items.Count == 0)
                        {
                            Messages.ShowWarning("A teacher is mandatory. Please enter a teacher, then click Create Group.");
                            return;
                        }
                        else if (!(StudentDetailsRepeater.Items.Count == 0 && TeacherDetailsRepeater.Items.Count == 0))
                        {
                            int succesfull = int.Parse(Gc.Add(groups).ToString());
                        }                         
                        else
                        {
                            Messages.ShowWarning(GetMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_MEMBERVALIDATION));
                            return;
                        }

                        StudentList = null;
                        TeachersList = null;
                    }
                    else
                    {
                        Messages.ShowWarning(GetMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_GRPNAMEEXIST));
                        GroupNameTextBox.Text = string.Empty;
                        return;
                    }
                }
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                StudentsController.Instance.ClearAllCache();
                DataCache.RemoveCache(string.Format("GetGroupOwnerID{0}", EditGroupId.Value));
                DataCache.RemoveCache(string.Format("GetGroupOwnerID{0}", EditClassId.Value));
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
            catch (GroupValidationException exc)
            {
                if (exc.getErrorState() == MyEnums.CrudState.Insert)
                {
                    Messages.ShowWarning(exc.getErrorMessage());
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
        private void EditGroup()
        {
            try
            {
                GroupController groupcontroller = GroupController.Instance;
                int? selectedGroupId = null;
                if (TeachersList == null || StudentList == null)
                {
                    int? selectedSubs = 0;
                    string groupname = null;
                    char classorgroup = new char();
                    AllOther = true;
                    if (EditGroupId != -1)
                    {
                        selectedGroupId = EditGroupId;
                        TeachersList = groupcontroller.GetMembersByGroup(Null.SetNullInteger(EditGroupId), "Teacher");
                        StudentList = groupcontroller.GetStudentByGroup(Null.SetNullInteger(EditGroupId));
                        selectedSubs = GroupLists.Where(u => u.GroupId.Equals(EditGroupId)).FirstOrDefault().CustomerSubId;
                        groupname = GroupLists.Where(u => u.GroupId.Equals(EditGroupId)).FirstOrDefault().Name;
                        classorgroup = GroupType;
                    }
                    if (EditClassId != -1)
                    {
                        selectedGroupId = EditClassId;
                        TeachersList = groupcontroller.GetMembersByGroup(Null.SetNullInteger(EditClassId), "Teacher");
                        StudentList = groupcontroller.GetStudentByGroup(Null.SetNullInteger(EditClassId));
                        selectedSubs = ClassList.Where(u => u.GroupId.Equals(EditClassId)).FirstOrDefault().CustomerSubId;
                        groupname = ClassList.Where(u => u.GroupId.Equals(EditClassId)).FirstOrDefault().Name;
                        classorgroup = ClassType;
                    }

                    GroupNameTextBox.Text = groupname;
                    GroupName = groupname;
                    if (classorgroup == 'C')
                    {
                        Type = true;
                    }
                    else
                    {
                        Type = false;
                    }
                    ChangeCheckBoxImg();
                    AllOther = false;
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
        private void MergeGroups()
        {
            try
            {
               // DnnLog.Error("Merge ecoll start");
                
                List<int> GroupsIds = new List<int>();
                foreach (string id in MergeClassId)
                {
                    GroupsIds.Add(int.Parse(id));
                }
                AllOther = true;
                GroupController _groupController = GroupController.Instance;
                List<Components.Groups> MergedGroups = new List<Components.Groups>();
                MergedGroups = _groupController.GetGroupList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()));
                GroupType = 'C';
                List<Components.Groups> MergedGroupClass = new List<Components.Groups>();
                MergedGroupClass = MergedGroups.Concat(_groupController.GetClassList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()))).ToList<Components.Groups>(); // do urself
                List<_IDCollection> TeacherListIDCollection = new List<_IDCollection>();
                List<Students> StudentListIDCollection = new List<Students>();
                int selectedSubs = 0;
                //DnnLog.Error("Merge ecoll Count"+ MergedGroupClass.Count);
                foreach (Components.Groups groups in MergedGroupClass)
                {
                   // DnnLog.Error("Merge " + groups.GroupId + "," + groups.MemberCount + "teacher group=" + groups.TeachersList.Count + "Student group=" + groups.StudentList.Count);
                    if (GroupsIds.Contains(groups.GroupId))
                    {
                        groups.TeachersList = _groupController.GetMembersByGroup(Null.SetNullInteger(groups.GroupId), "Teacher");
                        groups.StudentList = _groupController.GetStudentByGroup(Null.SetNullInteger(groups.GroupId));
                        foreach (_IDCollection idc in groups.TeachersList)
                        {
                            if (!TeacherListIDCollection.Contains(new _IDCollection(idc.Id, idc.Text)))
                            {
                                TeacherListIDCollection.Add(new _IDCollection(idc.Id, idc.Text));
                            }
                        }
                        foreach (Students idc in groups.StudentList)
                        {
                            if (!StudentListIDCollection.Contains(new Students(idc.UserID, idc.StudentNames)))
                            {
                                StudentListIDCollection.Add(new Students(idc.UserID, idc.StudentNames));
                            }
                        }
                        selectedSubs = groups.CustomerSubId.Value;
                    }
                }
                AllOther = false;
               // DnnLog.Error("TeacherListIDCollection=" + TeacherListIDCollection.Count);
               // DnnLog.Error("StudentListIDCollection=" + StudentListIDCollection.Count);
                TeachersList = TeacherListIDCollection;
                StudentList = StudentListIDCollection;
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }    

    }
}
