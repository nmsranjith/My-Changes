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
using _IDCollection = DotNetNuke.Modules.eCollection_Groups.Components.Common.IDCollection;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    public partial class EditGroup : eCollection_GroupsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    GroupController groupcontroller = GroupController.Instance;
            //    int selectedGroupId = 0;
            //    if (EditGroupId != -1)
            //    {
            //        TeachersList = GroupLists.Where(u => u.GroupId == EditGroupId).FirstOrDefault().TeachersList;
            //        StudentList = GroupLists.Where(u => u.GroupId == EditGroupId).FirstOrDefault().StudentList;
            //    }
            //    if (EditClassId != -1)
            //    {
            //        TeachersList = ClassList.Where(u => u.GroupId == EditClassId).FirstOrDefault().TeachersList;

            //        StudentList = ClassList.Where(u => u.GroupId == EditClassId).FirstOrDefault().StudentList;
            //    }
            //    TeacherDetailsRepeater.DataSource = TeachersList;
            //    TeacherDetailsRepeater.DataBind();
            //    StudentDetailsRepeater.DataSource = StudentList;
            //    StudentDetailsRepeater.DataBind();
            //    SubscriptionDrpList.DataSource = GroupController.Instance.GetSubscription(LoginName);
            //    SubscriptionDrpList.DataValueField = "Id";
            //    SubscriptionDrpList.DataTextField = "Text";
            //    SubscriptionDrpList.DataBind();
            //    List<Components.Groups> grouplist = groupcontroller.GetGroupNameByGroupId(selectedGroupId, "Teacher");
            //    string groupname = null;
            //    char classorgroup = new char();
            //    foreach (Components.Groups group in grouplist)
            //    {
            //        groupname = group.Name;
            //        classorgroup = group.GroupType;
            //    }
            //    GroupNameTextBox.Value = groupname;
            //    if (classorgroup == 'C')
            //    {
            //        ClassCheckBox.Checked = true;
            //    }
            //    else
            //    {
            //        ClassCheckBox.Checked = false;
            //    }
            //    Session["EditClassID"] = null;
            //    Session["EditGroupID"] = null;
            //}
        }

        protected void CancelCreateGroup_Click(object sender, EventArgs e)
        {

            //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }

        protected void AddTeachersButton_Click(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Cookies["SubscriptionId"].Value = "ECOLL-100";//SubscriptionDrpList.SelectedItem.Text;
            //GroupList();
            //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDTEACHERTOEDITGROUP);
        }


        private void GroupList()
        {
            //StudentList = null;
            //TeachersList = null;
            ////Session["studentList"] = null;
            ////Session["teacherList"] = null;
            //List<IDCollection> selectedStudentList = new List<IDCollection>();
            //RepeaterItemCollection myItemStudentCollection = StudentDetailsRepeater.Items;
            //for (int index = 0; index < myItemStudentCollection.Count; index++)
            //{
            //    IDCollection studentIDCollection = new IDCollection();
            //    studentIDCollection.Text = (myItemStudentCollection[index].Controls[2] as Label).Text;
            //    studentIDCollection.Id = int.Parse((myItemStudentCollection[index].Controls[1] as Literal).Text);
            //    selectedStudentList.Add(studentIDCollection);
            //}
            //StudentList = selectedStudentList;
            //List<IDCollection> selectedTeacherList = new List<IDCollection>();
            //RepeaterItemCollection myItemTeacherCollection = TeacherDetailsRepeater.Items;
            //for (int index = 0; index < myItemTeacherCollection.Count; index++)
            //{
            //    IDCollection teacherIDCollection = new IDCollection();
            //    teacherIDCollection.Text = (myItemTeacherCollection[index].Controls[2] as Label).Text;
            //    teacherIDCollection.Id = int.Parse((myItemTeacherCollection[index].Controls[1] as Literal).Text);
            //    selectedTeacherList.Add(teacherIDCollection);
            //}
            //TeachersList = selectedTeacherList;
        }

        protected void AddStudentButton_Click(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.Cookies["SubscriptionId"].Value = "ECOLL-100";//SubscriptionDrpList.SelectedItem.Text;
            //GroupList();
            //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDSTUDENTTOEDITGROUP);
        }
        protected void StudentDeleteImgButton__Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            //Button button = (sender as Button);

            ////Get the Repeater Item reference
            //RepeaterItem item = button.NamingContainer as RepeaterItem;
            //int studentID = int.Parse((item.FindControl("studentid") as Literal).Text);
            //List<_IDCollection> SelectedstudentID = new List<_IDCollection>();
            //RepeaterItemCollection myItemCollection;
            //myItemCollection = StudentDetailsRepeater.Items;
            //for (int Stindex = 0; Stindex < myItemCollection.Count; Stindex++)
            //{
            //    if ((int.Parse((myItemCollection[Stindex].Controls[1] as Literal).Text)) != studentID)
            //    {
            //        IDCollection studentIDCollection = new IDCollection();
            //        studentIDCollection.Id = (int.Parse((myItemCollection[Stindex].Controls[1] as Literal).Text));
            //        studentIDCollection.Text = (myItemCollection[Stindex].Controls[2] as Label).Text;
            //        SelectedstudentID.Add(studentIDCollection);
            //    }
            //}
            //StudentList = SelectedstudentID;
            //StudentDetailsRepeater.DataSource = StudentList;
            //StudentDetailsRepeater.DataBind();
            //if (StudentList.Count == 0)
            //{
            //    StudentDetailsDiv.Style.Add("display", "none");
            //}
            //StudentDetailsUpdatePanel.Update();
            //MessageUpdatePanel.Update();
        }


        protected void TeacherDeleteImgButton__Click(object sender, EventArgs e)
        {

            ////Get the reference of the clicked button.
            //Button button = (sender as Button);

            ////Get the Repeater Item reference
            //RepeaterItem item = button.NamingContainer as RepeaterItem;
            //int teacherID = int.Parse((item.FindControl("teacherid") as Literal).Text);
            //List<_IDCollection> SelectedTeacherID = new List<_IDCollection>();
            //RepeaterItemCollection myItemCollection;
            //myItemCollection = TeacherDetailsRepeater.Items;
            //for (int Teindex = 0; Teindex < myItemCollection.Count; Teindex++)
            //{
            //    if ((int.Parse((myItemCollection[Teindex].Controls[1] as Literal).Text)) != teacherID)
            //    {
            //        IDCollection teacherIDCollection = new IDCollection();
            //        teacherIDCollection.Id = (int.Parse((myItemCollection[Teindex].Controls[1] as Literal).Text));
            //        teacherIDCollection.Text = (myItemCollection[Teindex].Controls[2] as Label).Text;
            //        SelectedTeacherID.Add(teacherIDCollection);
            //    }
            //}
            //TeachersList = SelectedTeacherID;
            //TeacherDetailsRepeater.DataSource = TeachersList;
            //TeacherDetailsRepeater.DataBind();
            //if (TeachersList.Count == 0)
            //{
            //    TeacherDetailsDiv.Style.Add("display", "none");
            //}
            //TeacherDetailsUpdatePanel.Update();
            //MessageUpdatePanel.Update();
        }

        protected void Backtocreategroupbtn_Click(object sender, EventArgs e)
        {
            ////Session["studentList"] = null;
            //StudentList = null;
            ////Session["teacherList"] = null;
            //TeachersList = null;
            ////Session["deletedStudent"] = null;
            ////DeletedStudentList = null;
            ////Session["deletedTeacher"] = null;
            //if (Request.QueryString["parentpage"] == "profile")
            //{
            //    string profileurl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID).ToString();
            //    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + GROUPPROFILE);
            //}
            //else
            //{
            //    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            //}

        }


        protected void CreateGroupButton_Click(object sender, EventArgs e)
        {
            //Components.Groups groups = new Components.Groups();
            //HttpCookie cookie = HttpContext.Current.Request.Cookies["SelectedGroupId"];
            //groups.CreatedByUserId = 5;
            //groups.GroupId = int.Parse(cookie.Value);
            //groups.CustomerSubId = int.Parse(SubscriptionDrpList.SelectedValue);
            //groups.Name = GroupNameTextBox.Value;
            //if (ClassCheckBox.Checked)
            //{
            //    groups.GroupType = 'C';//char.Parse(MyEnums.GroupType.Class.ToString());
            //}
            //else
            //{
            //    groups.GroupType = 'N';//char.Parse(MyEnums.GroupType.Group.ToString());
            //}
            ////if (!GroupController.Instance.ValidateGroupName(GroupNameTextBox.Value.Trim(), groups.GroupType, int.Parse(SubscriptionDrpList.SelectedValue)))
            ////{


            //groups.UserCreated = "ADMIN";
            //groups.ActiveFlag = 'Y';
            //groups.DateCreated = DateTime.Now;
            //List<IDCollection> groupMemberList = new List<IDCollection>();
            //GroupList();
            //foreach (IDCollection teacherCollection in TeachersList)
            //{
            //    groupMemberList.Add(teacherCollection);
            //}
            //foreach (IDCollection studentCollection in StudentList)
            //{
            //    groupMemberList.Add(studentCollection);
            //}
            ////foreach (IDCollection deletedStudentCollection in DeletedStudentList)
            ////{
            ////    groupMemberList.Add(deletedStudentCollection);
            ////}
            ////foreach (IDCollection deletedTeacherCollection in DeletedTeachersList)
            ////{
            ////    groupMemberList.Add(deletedTeacherCollection);
            ////}
            ////Session["studentList"] = null;
            //StudentList = null;
            ////Session["teacherList"] = null;
            //TeachersList = null;
            ////Session["deletedStudent"] = null;
            ////DeletedStudentList = null;
            ////Session["deletedTeacher"] = null;

            //GroupController.Instance.UpdateMembers(groups, groupMemberList);
            //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            //}
            //else
            //{
            //    Messages.ShowWarning("Group name already exists. Please specify a unique group name");
            //    GroupNameTextBox.Value = string.Empty;
            //}
        }



    }
}