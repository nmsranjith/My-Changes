using System;
using DotNetNuke.Modules.eCollection_Students;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Dashboards;
using System.Linq;
using System.Data;
using DotNetNuke.Common.Utilities;
namespace DotNetNuke.UI.eCollectionControls
{
    public partial class eCollectionMenu : eCollection_DashboardsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            if (Session["Subscription"] != null)
            {
                GroupsTab.NavigateUrl = Globals.NavigateURL(GetTabID(GroupsModule));
                StudentsTab.NavigateUrl = Globals.NavigateURL(GetTabID(StudentsModule));
                SessionsTab.NavigateUrl = Globals.NavigateURL(GetTabID(SessionsModule));
                TeachersTab.NavigateUrl = Globals.NavigateURL(GetTabID(TeachersModule));
                BooksTab.NavigateUrl = Globals.NavigateURL(GetTabID(BooksModule));
                DashboardTab.NavigateUrl = Globals.NavigateURL(GetTabID(DashboardsModule)) + "?pagename=" + DASHBOARD;
            }
            else
            {
                GroupsTab.Visible = false;
                StudentsTab.Visible = false;
                SessionsTab.Visible = false;
                TeachersTab.Visible = false;
                BooksTab.Visible = false;
                DashboardTab.Visible = false;
                eCollectionMenuMid.Visible = false;
            }                 
            var subsList = new List<Subscription>(from c in SubsList.AsEnumerable()
                                                  select new Subscription
                                                  {
                                                      SubsId = c.Field<int>("SubscriptionId"),
                                                      Name = c.Field<string>("SubscriptionText"),
                                                      NewName = c.Field<string>("TitleText")
                                                  });
            if (SubsList.Rows.Count > 1)
            {
                SubscriptionTabHolder.Visible = true;
                SubscriptionTab.NavigateUrl = Globals.NavigateURL(GetTabID(DashboardsModule));
            }
            else
                SubscriptionTabHolder.Visible = false;
            if (StaffDetail != null && Session["UserName"].ToString().ToLower()=="brendan.bolton@cengage.com")
            {
                if (Session["Subscription"] == null)
                {
                    eBookManagementTabHolder.Style.Add("margin-top", "-295px");
                    AppDataTabHolder.Style.Add("margin-top", "-240px");                   
                }
                RoleChkHdn.Value = "true";
                eBookManagementTabHolder.Visible = true;
                eBookManagementTab.NavigateUrl = Globals.NavigateURL(GetTabID(DashboardsModule)) + "?pagename=" + CENGAGESTAGING;

                AppDataTabHolder.Visible = true;
                AppDataCollectionTab.NavigateUrl = Globals.NavigateURL(GetTabID(DashboardsModule)) + "?pagename=" + APPDATACOLLECTION;
            }
            else
            {
                RoleChkHdn.Value = "false";
                eBookManagementTabHolder.Visible = false;
                AppDataTabHolder.Visible = false;
            }           
            
        }
    }
}