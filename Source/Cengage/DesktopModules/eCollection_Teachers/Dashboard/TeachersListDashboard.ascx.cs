using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Linq;
using System.Web.UI;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Teachers.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="TeachersListDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //     Left panel for teachers list screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class TeachersListDashboard :eCollection_TeachersModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string roleId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) }).UserRole;
                if (roleId.ToUpper() != "SUBS ADMIN" && roleId.ToUpper() != "CEN ADMIN")
                {
                    CreateProfileHdr.Visible = false;
                    DeleteProfileHdr.Visible = false;
                    List_MidHdr.Visible = false;
                }
                else
                {
                    CreateTeacherProfileButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATETEACHERPROFILE;
                }
                var teacher = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = 0 });

                if (teacher != null && teacher.UserRole.ToUpper() == "CEN ADMIN")
                {
                    CreateProfileHdr.Visible = true;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateTeacherProfileButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename="+CREATETEACHERPROFILE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateReadingSessionButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Teacher> selectedTeachers = GetSelectedTeachers();
                if (selectedTeachers != null)
                {
                    List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers> sessionMemList = new List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers>();
                    Session["SelectedGroups"] = null;
                    Session["SelectedProducts"] = null;
                    Session["EditSelectedId"] = null;
                    Session["SessionNotes"] = string.Empty;
                    Session["SessionType"] = string.Empty;
                    Session["SessionExpiryDate"] = null;
                    foreach (Teacher teacher in selectedTeachers)
                    {
                        DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers sessionMember = new DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers()
                        {
                            //GRP_MEM_SK=int.Parse(str[i]),
                            CUST_SUBS_USER_SK = teacher.CustSubUserSk,
                            TeacherName = teacher.UserLoginName,
                            MemberType = "USER",
                            Added_date = DateTime.Now,
                            Active = "Y"
                        };
                        sessionMemList.Add(sessionMember);
                    }
                    Session["SelectedGroups"] = sessionMemList;
                    Session["SelectedSubscriptionId"] = int.Parse(Session["Subscription"].ToString());// selectedTeachers[0].SubscriptionList[0].Id;
                    Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + CREATEREADINGSESSION + "&returnurl=teachers");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "SubscriptionMismatch", "<script>SubscriptionMismatch()</script>", false);
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTeacherToGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Teacher> selectedTeachers = GetSelectedTeachers();
                if (selectedTeachers != null)
                {
                    Session["SelectedTeachers"] = selectedTeachers;
                    Session["TeacherNames"] = selectedTeachers[0].UserLoginName;// string.Concat("|", string.Join("|", selectedTeachers.Select(teacher => teacher.UserLoginName).ToArray()), "|");

                    Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDTOGROUP);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "SubscriptionMismatch", "<script>SubscriptionMismatch()</script>", false);
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteTeacherButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Teacher> selectedTeachers = new List<Teacher>();
                var str = UserLoginNameHdn.Value.Substring(1, UserLoginNameHdn.Value.Length - 1).Split(',');

                for (int i = 0; i < str.Length; i++)
                {
                    selectedTeachers.Add(AllTeachers.Find(x => x.TeacherId == int.Parse(str[i])));
                }
                teacherController.DeleteTeachers(new Teacher()
                {
                    UserModified = TeacherLoginName,
                    DateModified = DateTime.Now,
                    SelectedTeachersList = selectedTeachers,
                    SubscriptionId = Null.SetNullInteger(Session["Subscription"])
                });

                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<Teacher> GetSelectedTeachers()
        {
            try
            {
                List<Teacher> selectedTeachers = new List<Teacher>();
                var str = UserLoginNameHdn.Value.Substring(1, UserLoginNameHdn.Value.Length - 1).Split(',');
                int[] cnt = new int[str.Length];
                UserLoginNameHdn.Value = string.Empty;
                for (int i = 0; i < str.Length; i++)
                {
                    selectedTeachers.Add(AllTeachers.Find(x => x.TeacherId == int.Parse(str[i])));
                   // cnt[i] = AllTeachers.Find(x => x.TeacherId == int.Parse(str[i])).SubscriptionList.Count;
                }

                //int a = Array.IndexOf(cnt, cnt.Min());
                //List<Teacher> newList = new List<Teacher>();
                //selectedTeachers[0].CustSubUserSk = int.Parse(selectedTeachers[0].CustSubUserSkList.Find(x => x.Id == int.Parse(Session["Subscription"].ToString())).Text);
                //newList.Add(selectedTeachers[a]);
                //selectedTeachers.RemoveAt(a);
                //foreach (Teacher teacher in selectedTeachers)
                //{
                //    int subNotExists = 0;
                //    for (int i = 0; i < newList[0].SubscriptionList.Count; i++)
                //    {
                //        if (teacher.SubscriptionList.Exists(delegate(IDCollection t) { return t.Id == newList[0].SubscriptionList[i].Id; }))
                //        {
                //            teacher.CustSubUserSk = int.Parse(teacher.CustSubUserSkList.Find(x => x.Id == newList[0].SubscriptionList[0].Id).Text);
                //        }
                //        else
                //            subNotExists++;
                //    }
                //    if (subNotExists == newList[0].SubscriptionList.Count)
                //        return null;
                //    else
                //        newList.Add(teacher);
                //}
                return selectedTeachers;
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return null;
        }
    }
}