using System;
using System.Web.UI;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    public partial class StartCreateStudents : eCollection_StudentsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Subscription"] != null)
            {
                if (Null.SetNullInteger(DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses) <= 0)
                {
                    //LogValues("ZERO");
                    string roleId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) }).UserRole;
                    if (roleId.ToUpper() != "SUBS ADMIN" && roleId.ToUpper() != "CEN ADMIN")
                        TeacherTxt.Visible = true;
                    else
                        AdminText.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>createProfile()</script>", false);
                }
                else { }
                   // LogValues("NON ZERO" + Null.SetNullInteger(DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses.ToString()));

                StudCreateLnk.HRef = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", CREATENEW); ;
                StudBulkUploadLnk.HRef = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", BULKUPLOAD); ;
                StudUnallocatedSearchLnk.HRef = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", UNALLOCATED); ;
            }
        }
    }
}