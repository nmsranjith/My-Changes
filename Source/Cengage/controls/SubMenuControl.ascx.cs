using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Instrumentation;
using Cengage.Ecommerce.Dashboard.Components.Controller;
using Cengage.Ecommerce.Dashboard.Components.Modal;
using Cengage.Ecommerce.Dashboard.Components.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class controls_SubMenuControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var tabs = TabController.GetPortalTabs(PortalSettings.Current.PortalId, -1, true, true);
            var TabNames = from t in tabs
                           select t.TabName.ToLower();
            //List<DotNetNuke.Entities.Tabs.TabInfo> parentinfo = new List<DotNetNuke.Entities.Tabs.TabInfo>();
            //int rootTab = GetTopTabID();

            ////int tabid = objinfo.TabID;
            //parentinfo = DotNetNuke.Entities.Tabs.TabController.GetTabsByParent(rootTab, PortalSettings.Current.PortalId).Where(u => (!u.IsDeleted)).ToList();
            //DnnLog.Error("Current Page :" + PortalSettings.Current.ActiveTab.TabName.ToLower());
           
                if (Session["IsAuthenticated"] == null)
                {
                    if (PortalSettings.Current.ActiveTab.TabID != PortalSettings.Current.HomeTabId)
                    {
                        if ("primary" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                        {
                            Session["Division"] = "0";
                            Primary.Attributes.Add("class", "tile_icon Primary primaryactive");
                            //if (Session["Division"] == "0" || Session["Division"] == "2") { Primary.Attributes.Add("class", "tile_icon Primary primaryactive"); }
                            // else if (Session["Division"] == "1") { Secondary.Attributes.Add("class", "tile_icon Secondary secondaryactive"); }
                            return;
                        }
                        else if ("secondary" == PortalSettings.Current.ActiveTab.TabName.Trim().ToLower())
                        {
                            Session["Division"] = "1";
                            Secondary.Attributes.Add("class", "tile_icon Secondary secondaryactive");
                            return;
                        }
                        else if ("staff-room" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                        {
                            //Session["Division"] = "2";
                            StaffRoom.Attributes.Add("class", "tile_icon StaffRoom staffroomactive");
                            //return;
                        }
                        else if ("about-us" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                        {
                            //Session["Division"] = "2";
                            AboutUs.Attributes.Add("class", "tile_icon AboutUs aboutusactive");
                            //return;
                        }
						else if (PortalSettings.Current.ActiveTab.TabName.ToLower() == "search" || PortalSettings.Current.ActiveTab.TabName.ToLower() == "list"
                        || PortalSettings.Current.ActiveTab.TabName.ToLower() == "product"
                        || PortalSettings.Current.ActiveTab.TabName.ToLower() == "checkout" || PortalSettings.Current.ActiveTab.TabName.ToLower() == "ecollection" ||
                        PortalSettings.Current.ActiveTab.TabName.ToLower() == "primary" || PortalSettings.Current.ActiveTab.TabName.ToLower() == "secondary"
                        || PortalSettings.Current.ActiveTab.TabName.ToLower() == "pmecollection" || PortalSettings.Current.ActiveTab.TabPath.Contains("Primary") ||
                        PortalSettings.Current.ActiveTab.TabPath.Contains("Secondary"))
                    {
                        HtmlGenericControl homeMenus = new HtmlGenericControl();
                        homeMenus = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("HomeMainmenu");
                        if (homeMenus != null)
                            homeMenus.Visible = false;
                        return;
                    }
                        Mainmenu.Visible = true;
                        DashBoardMenu.Visible = false;
                        EcollectionMenu.Visible = false;
                        HtmlGenericControl homeMenu = new HtmlGenericControl();
                        homeMenu = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("HomeMainmenu");
                        if (homeMenu != null)
                            homeMenu.Visible = true;
                        Mainmenu.Visible = false;
                        DashBoardMenu.Visible = false;
                        EcollectionMenu.Visible = false;
							
                    }
                    else
                    {
                        HtmlGenericControl homeMenu = new HtmlGenericControl();
                        homeMenu = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("HomeMainmenu");
                        if(homeMenu!=null)
                        homeMenu.Visible = true;
                        Mainmenu.Visible = false;
                        DashBoardMenu.Visible = false;
                        EcollectionMenu.Visible = false;
                    }

                }
                else
                {
                    HtmlGenericControl homeMenu = new HtmlGenericControl();
                    homeMenu = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("HomeMainmenu");
                    if (homeMenu != null)
                    homeMenu.Visible = false;
                    MyEnums.Visible PersonalPlaceHolderVisible = (MyEnums.Visible)DashBoardController.Instance.GetTabPermission(LoginUserSk).Where(u => u.AccountNumber == string.Empty).Select(x => x.SubTabFlag).FirstOrDefault();
                    MyEnums.Visible BusinessPlaceHolderVisible = (MyEnums.Visible)DashBoardController.Instance.GetTabPermission(LoginUserSk).Where(u => u.AccountNumber != string.Empty).Select(x => x.SubscriptionTabFlag).FirstOrDefault();
                    if (BusinessPlaceHolderVisible == MyEnums.Visible.True || PersonalPlaceHolderVisible == MyEnums.Visible.True)
                    {
                        EcollectionMenu.Visible = true;
                    }
                    else
                    {
                        EcollectionMenu.Visible = false;
                    }
                    if (Session["Division"] != null && EcollectionMenu.Visible)
                    {
                        //if (Session["Division"] == "0") { primaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolPrimary DbecolPrimaryActive"); }
                        //else if (Session["Division"] == "1") { secondaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolSecondary DbecolSecondaryActive"); }
                    }
                    else if (Session["Division"] != null)
                    {
                        //if (Session["Division"] == "0") { dashbaordprimarylnk.Attributes.Add("class", "dashboardtile_icon DbPrimary DbPrimaryActive"); }
                        //else if (Session["Division"] == "1") { dashbaordsecondarylnk.Attributes.Add("class", "dashboardtile_icon DbSecondary DbSecondaryActive"); }
                    }
                    else
                    {
                        //Dashbaord.Attributes.Add("class", "dashboardtile_icon DbDashboard DbDashboardActive");
                    }
                    if (EcollectionMenu.Visible)
                    {
                        Dashboardecoll.Attributes.Add("class", "dashboardecoll_icon DbecolDashboard");
                        primaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolPrimary");
                        staffroomecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolStaff");
                        aboutusecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolAboutUs");
                        secondaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolSecondary");
                        Mainmenu.Visible = false;
                        DashBoardMenu.Visible = false;
                        EcollectionMenu.Visible = true;
                    }
                    else
                    {
                        Dashbaord.Attributes.Add("class", "dashboardtile_icon DbDashboard");
                        dashbaordprimarylnk.Attributes.Add("class", "dashboardtile_icon DbPrimary");
                        dashbaordsecondarylnk.Attributes.Add("class", "dashboardtile_icon DbSecondary");
                        dashbaordstaffroomlnk.Attributes.Add("class", "dashboardtile_icon DbStaffRoom");
                        dashbaordaboutuslnk.Attributes.Add("class", "dashboardtile_icon DbAboutUs");
                        Mainmenu.Visible = false;
                        DashBoardMenu.Visible = true;
                        EcollectionMenu.Visible = false;
                    }
                    //foreach (string TabName in TabNames)
                    //{
                    if ("primary" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                    {
                        Session["Division"] = "0";
                        if (EcollectionMenu.Visible)
                            primaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolPrimary DbecolPrimaryActive");
                        else
                            dashbaordprimarylnk.Attributes.Add("class", "dashboardtile_icon DbPrimary DbPrimaryActive");
                        return;
                    }
                    else if ("secondary" == PortalSettings.Current.ActiveTab.TabName.Trim().ToLower())
                    {
                        Session["Division"] = "1";
                        if (EcollectionMenu.Visible)
                            secondaryecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolSecondary DbecolSecondaryActive");
                        else
                            dashbaordsecondarylnk.Attributes.Add("class", "dashboardtile_icon DbSecondary DbSecondaryActive");
                        return;
                    }
                   else if ("dashboard" == PortalSettings.Current.ActiveTab.TabName.ToLower() || "myaccount" == PortalSettings.Current.ActiveTab.TabName.ToLower() || "subscription" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                	{
                    Session["Division"] = "2";
                        if (EcollectionMenu.Visible)
                            Dashboardecoll.Attributes.Add("class", "dashboardecoll_icon DbecolDashboard DbecolDashboardActive");
                        else
                            Dashbaord.Attributes.Add("class", "dashboardtile_icon DbDashboard DbDashboardActive");
                        return;
                    }
                    else if ("staff-room" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                    {
                        Session["Division"] = "2";
                        if (EcollectionMenu.Visible)
                            staffroomecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolStaff DbecolStaffActive");
                        else
                            dashbaordstaffroomlnk.Attributes.Add("class", "dashboardtile_icon DbStaffRoom DbStaffRoomActive");
                        return;
                    }
                    else if ("about-us" == PortalSettings.Current.ActiveTab.TabName.ToLower())
                    {
                        Session["Division"] = "2";
                        if (EcollectionMenu.Visible)
                            aboutusecollnk.Attributes.Add("class", "dashboardecoll_icon DbecolAboutUs DbecolAboutUsActive");
                        else
                            dashbaordaboutuslnk.Attributes.Add("class", "dashboardtile_icon DbAboutUs DbAboutUsActive");
                        return;
                    }

                }
                //}
        }

    }
    public int LoginUserSk
    {
        get
        {
            Visitor UserInfo = new Visitor();
            int LoginUserSk = 0;
            if (Session["UserInfo"] != null)
            {
                UserInfo = (Visitor)Session["UserInfo"];
                LoginUserSk = UserInfo.UserID;
            }
            return LoginUserSk;
        }
    }
    protected void Pmecollection_Click(object sender, EventArgs e)
    {
        //Visitor UserInfo = new Visitor();
        //UserInfo = (Visitor)Session["UserInfo"];

        //string serializedClass = SerializeObject(UserInfo);
        //string guid = Guid.NewGuid().ToString();
        //int result = DashBoardController.Instance.AddUserinfo(guid, serializedClass, 'Y');
         //string url = UserInfo.CountryCode.ToLower() == "nz" ? ConfigurationManager.AppSettings["EcollectionUrlNz"].ToString() + guid : ConfigurationManager.AppSettings["EcollectionUrlAu"].ToString() + guid;
        Response.Redirect("ecollection.aspx");
        
    }

    public string SerializeObject(object o)
    {
        if (!o.GetType().IsSerializable)
        {
            return null;
        }

        using (MemoryStream stream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(stream, o);
            return Convert.ToBase64String(stream.ToArray());
        }
    }
    //protected void Dashboardlnk_Click(object sender, EventArgs e)
    //{
    //    Dashbaord.Attributes.Add("class", "dashboardtile_icon DbDashboard DbDashboardActive");
    //    Session["Division"] = "0";
    //    Response.Redirect("dashboard.aspx");
    //}

    //protected void primarylnk_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    Primary.Attributes.Add("class", "tile_icon Primary primaryactive");
    //    Response.Redirect("primary.aspx");
    //}
    //protected void secondarylnk_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "1";
    //    Secondary.Attributes.Add("class", "tile_icon Secondary secondaryactive");
    //    Response.Redirect("secondary.aspx");
    //}
    //protected void staffroomlnk_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    StaffRoom.Attributes.Add("class", "tile_icon StaffRoom staffroomactive");
    //    Response.Redirect("staff-room.aspx");
    //}
    //protected void aboutuslnk_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    AboutUs.Attributes.Add("class", "tile_icon AboutUs aboutusactive");
    //    Response.Redirect("about-us.aspx");
    //}
    //protected void primarylnk_dash_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    dashbaordprimarylnk.Attributes.Add("class", "dashboardtile_icon DbPrimary DbPrimaryActive");
    //    Response.Redirect("primary.aspx");
    //}
    //protected void secondarylnk_dash_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "1";
    //    dashbaordsecondarylnk.Attributes.Add("class", "dashboardtile_icon DbSecondary DbSecondaryActive");
    //    Response.Redirect("secondary.aspx");
    //}
    //protected void staffroomlnk_dash_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    dashbaordstaffroomlnk.Attributes.Add("class", "dashboardtile_icon DbStaffRoom DbStaffRoomActive");
    //    Response.Redirect("staff-room.aspx");
    //}
    //protected void aboutuslnk_dash_Click(object sender, EventArgs e)
    //{
    //    Session["Division"] = "0";
    //    dashbaordaboutuslnk.Attributes.Add("class", "dashboardtile_icon DbAboutUs DbAboutUsActive");
    //    Response.Redirect("about-us.aspx");
    //}
}