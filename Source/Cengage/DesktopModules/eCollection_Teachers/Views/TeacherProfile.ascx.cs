using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using System.Web.Services;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="TeacherProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To display the respective teacher's profile
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class TeacherProfile :eCollection_TeachersModuleBase
    {
        Teacher teacherProfile = null;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                teacherProfile = selectedTeacher();
                teacherProfile.Active = (char)MyEnums.Active.Yes;
                teacherProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);
                if (!IsPostBack)
                {
                    TeacherProfileRepeater.DataSource = teacherController.GetProfileDetails(teacherProfile).Tables[0];
                    TeacherProfileRepeater.DataBind();
                    Session["RecordingsCount"] = (TeacherProfileRepeater.Items[0].FindControl("NoOfRecordingsLabel") as Label).Text;
                    Session["WordsCount"] = (TeacherProfileRepeater.Items[0].FindControl("NoOfWordsLabel") as Label).Text;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_PreRender runs when the control is before the page renders
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ReadingHistory.IsNoReadingHistory)
            {
                Message1.Text = Constants.NOREADHISTORYINFO;
                Message2.Style.Add("Display", "none");
                MessageOuterDiv.Style.Add("Display", "block");
            }
            if (ProfileGroupsList.IsNoGroupExist)
            {
                Message2.Text = Constants.NOGROUPINFO;
            }
            if (Message1.Text == "" && Message2.Text == "")
            {
                MessageOuterDiv.Style.Add("Display", "none");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        protected string MyRecordingsUrl()
        {
            return Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?username=" + Request.QueryString["username"] + "&pagename=" + MYRECORDINGS;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        protected string MyWordsUrl()
        {
            return Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?username=" + Request.QueryString["username"] + "&pagename=" + MYWORDS;
        }
       
    }
}