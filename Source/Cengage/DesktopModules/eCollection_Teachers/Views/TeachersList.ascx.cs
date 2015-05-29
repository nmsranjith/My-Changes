using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Services;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="TeachersList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the teachers.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class TeachersList : eCollection_TeachersModuleBase
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
                if (!IsPostBack)
                {
                    SelTeachersCnt.Value = AllTeachers.Count.ToString();                   
                    FillTeachersList();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProfileButton_Click(object sender, EventArgs e)
        {
            Session["SelTeacherId"] = SelectedTeacherId.Value;
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + TEACHERPROFILE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReSendInviteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button resendBtn = sender as Button;
                string[] cmdArg = resendBtn.CommandArgument.Split('|');
                var UserId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = Null.SetNullInteger(Session["Subscription"]) }).UserId;
                var uri = new Uri(Request.Url.AbsoluteUri);
                var url = string.Concat(uri.Scheme, "://", uri.Host, "/signup?inviteesk=", encrypt(cmdArg[0]), "&subsid=", encrypt(Session["Subscription"].ToString()), "&usersk=", encrypt(UserId.ToString()), "&app=ecollection");
                teacherController.MailbodyBuilder(cmdArg[1], url);
                teacherController.SendMail(cmdArg[0], teacherController.MailbodyBuilder(cmdArg[1], url), "Invitation for PM eCollection:");
            }
            catch (Exception ex) { LogFileWrite(ex); }            
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
                if (SearchTextBox.Text.Trim() != string.Empty)
                {
                    var mainList = AllTeachers;
                    string[] name = SearchTextBox.Text.Trim().Split(',');// Request.QueryString["names"].Split(',');
                    name = name.Where(s => s != string.Empty).ToArray();
                    List<Teacher> teacherList = new List<Teacher>();                    
                    if (name.Length > 0)
                    {
                        foreach (string s in name)
                        {
                            var text = s.Trim().ToLower();
                            teacherList.AddRange(mainList.Where(x => x.DisplayName.ToLower().Contains(text)));
                            teacherList = teacherList.Distinct().ToList();
                        }
                    }
                    else
                        teacherList = mainList;

                    TeachersListView.DataSource = teacherList;
                    TeachersListView.DataBind();
                }
                else
                    FillTeachersList();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            finally
            {
              ScriptManager.RegisterStartupScript(Page, GetType(), "EndUpdateProgress", "<script>EndUpdateProgress()</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillTeachersList()
        {
            try
            {
                TeachersListView.DataSource = AllTeachers;
                TeachersListView.DataBind();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
             
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SortingButton_Click(object sender, EventArgs e)
        {
            try
            {
                var sortTeachers = new List<Teacher>();
                string teacherId = string.Empty;
                foreach (ListViewDataItem item in TeachersListView.Items)
                {
                    teacherId = string.Concat(teacherId, ',', (item.Controls[1] as HtmlInputCheckBox).Value);
                }
                string[] teacherIds = teacherId.Split(',');
               
                var stu = AllTeachers; // teacherController.GetAll(teachers);
                teacherIds = teacherIds.Where(s => s != string.Empty).ToArray();
                foreach (string s in teacherIds)
                    sortTeachers.AddRange(stu.Where(x => s != string.Empty && x.TeacherId == int.Parse(s)).ToList());
                sortTeachers.Sort(teacher => teacher.FullName);

                NamesAscButton.Visible = true;
                NamesDescButton.Visible = false;
                
                if ((sender as Button).CommandArgument == "desc")
                {
                    sortTeachers.Reverse(); 
                    NamesAscButton.Visible = false;
                    NamesDescButton.Visible = true;
                }

                TeachersListView.DataSource = sortTeachers;
                TeachersListView.DataBind();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [WebMethod]
        protected string ProfileLink(string username)
        {
            return Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?username=" + username + "&pagename=" + TEACHERPROFILE;
        }
    }
}