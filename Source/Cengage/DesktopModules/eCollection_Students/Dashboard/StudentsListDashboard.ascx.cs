using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Dashboard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsListDashboard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel controller class for student list screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class StudentsListDashboard : eCollection_StudentsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                CreateStudentProfileButton.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + CREATESTUDENTPROFILE;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentToGroupButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Student> SelectedStudents = new List<Student>();
                var str = UserLoginNameHdn.Value.Substring(1, UserLoginNameHdn.Value.Length - 1).Split(',');
                for (int i = 0; i < str.Length; i += 3)
                {
                    Student sessionMembers = new Student()
                    {
                        StudentId = int.Parse(str[i].Trim()),
                        CustSubUserSk = int.Parse(str[i + 1].Trim()),
                        FullName = str[i + 2].Trim(),
                    };
                    SelectedStudents.Add(sessionMembers);
                }
                Session["SelectedStudents"] = SelectedStudents;
                //Session["Subscription"] = SubscriptionId.Value;
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + ADDTOGROUP);
            }
            catch (Exception ex) { LogFileWrite(ex); }      
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
                List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers> SelectedGroups = new List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers>();
                var str = UserLoginNameHdn.Value.Substring(1, UserLoginNameHdn.Value.Length - 1).Split(',');
                Session["SelectedGroups"] = null;
                Session["SelectedProducts"] = null;
                Session["EditSelectedId"] = null;
                Session["SessionNotes"] = string.Empty;
                Session["SessionType"] = string.Empty;
                Session["SessionExpiryDate"] = null;
                for (int i = 0; i < str.Length; i += 3)
                {
                    DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers sessionMembers = new DotNetNuke.Modules.eCollection_Sessions.Components.Modal.SessionMembers()
                    {
                        //GRP_MEM_SK=int.Parse(str[i]),
                        CUST_SUBS_USER_SK = int.Parse(str[i + 1].Trim()),
                        StudentName = str[i + 2].Trim(),
                        MemberType = "USER",
                        Added_date = DateTime.Now,
                        Active = "Y"
                    };
                    SelectedGroups.Add(sessionMembers);
                }
                Session["SelectedGroups"] = SelectedGroups;
                Session["SelectedSubscriptionId"] = int.Parse(Session["Subscription"].ToString());
                Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + CREATEREADINGSESSION + "&returnurl=students");
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PrintStudentCardButton_Click(object sender, EventArgs e)
        {     
            List<Student> StudentsList = GetSeletedStudents();
            PrintStudentCard(studentController.GetStudentsDetails(CreateDocument(StudentsList)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                studentController.DeleteStudents(new Student()
                {
                    UserModified = TeacherLoginName,
                    DateModified = DateTime.Now,
                    SelectedStudentsList = GetSeletedStudents(),
                    SubscriptionId = Null.SetNullInteger(Session["Subscription"])
                });

                List<Student> addedStudentList = Session["AddedStudentList"] as List<Student>;
                List<Student> selectedStudList= GetSeletedStudents();
                if (addedStudentList != null)
                {
                    foreach (Student a in selectedStudList)
                    {
                        //addedStudentList.Remove(a);
                        string indexRcrd = "NA";
                        foreach (Student b in addedStudentList)
                        {
                            if (b.StudentId == a.StudentId)
                            {
                                indexRcrd = addedStudentList.IndexOf(b).ToString();
                            }
                        }
                        if (indexRcrd != "NA")
                        {
                            addedStudentList.RemoveAt(Convert.ToInt32(indexRcrd));
                        }
                    }
                    Session["AddedStudentList"] = addedStudentList;
                }
                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<Student> GetSeletedStudents()
        {
            List<Student> studentList = new List<Student>();
            try
            {                
                var str = SelectedStuds.Value.Split(',');
                foreach (var ids in str)
                {
                    studentList.Add(new Student() { StudentId = int.Parse(ids) });
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return studentList;
        }
    }
}