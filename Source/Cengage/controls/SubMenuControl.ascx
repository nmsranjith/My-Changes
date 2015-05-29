<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubMenuControl.ascx.cs" Inherits="controls_SubMenuControl" %>
<%--<div id="Mainmenu" runat="server" clientidmode="Static" class="algnCentr">
        <ul id="Mainmenuul">
            
            <li id="menuitem_primary"><a id="primarylnk" runat = "server" clientidmode="Static" onserverclick = "primarylnk_Click"><span class="tile_icon Primary"
                clientidmode="Static" id="Primary" runat = "server"></span></a></li>
            <li id="menuitem_secondary"><a id="secondarylnk" runat = "server" clientidmode="Static" onserverclick = "secondarylnk_Click"><span class="tile_icon Secondary"
                clientidmode="Static" id="Secondary" runat = "server"></span></a></li>
            <li id="menuitem_staffroom"><a id="staffroomlnk" runat = "server" clientidmode="Static" onserverclick ="staffroomlnk_Click"><span class="tile_icon StaffRoom"
                clientidmode="Static" id="StaffRoom" runat = "server"></span></a></li>
            <li id="menuitem_aboutus"><a id="aboutuslnk" runat = "server" clientidmode="Static" onserverclick = "aboutuslnk_Click" class="menuitem"><span
                clientidmode="Static" class="tile_icon AboutUs" id="AboutUs" runat = "server"></span></a></li>
        </ul>
    </div>
    <div id="DashBoardMenu" runat="server" clientidmode="Static" class="algnCentr" style="display: none;">
        <ul id="Ul1">
            <li id="menuitem_user_dash"><a id = "Dashboardlnk" runat = "server" clientidmode="Static" onserverclick = "Dashboardlnk_Click"><span class="dashboardtile_icon DbDashboard" id="Dashbaord"
                clientidmode="Static" runat = "server"></span></a></li>
            <li id="menuitem_primary_dash"><a id="primarylnk_dash" runat = "server" clientidmode="Static" onserverclick = "primarylnk_dash_Click"><span class="dashboardtile_icon DbPrimary" id="dashbaordprimarylnk" clientidmode="Static" runat = "server"></span></a></li>
            <li id="menuitem_secondary_dash"><a id="secondarylnk_dash" runat = "server" clientidmode="Static" onserverclick = "secondarylnk_dash_Click"><span class="dashboardtile_icon DbSecondary" id="dashbaordsecondarylnk"  clientidmode="Static"  runat = "server"></span></a></li>
            <li id="menuitem_staffroom_dash"><a id="staffroomlnk_dash" runat = "server" clientidmode="Static" onserverclick ="staffroomlnk_dash_Click"><span class="dashboardtile_icon DbStaffRoom"  id="dashbaordstaffroomlnk"   clientidmode="Static"  runat = "server"></span></a></li>
            <li id="menuitem_aboutus_dash"><a id="aboutuslnk_dash" runat = "server" clientidmode="Static" onserverclick = "aboutuslnk_dash_Click"><span class="dashboardtile_icon DbAboutUs"  id="dashbaordaboutuslnk" clientidmode="Static" runat = "server"></span></a>
            </li>
        </ul>
    </div>--%>
    <div id="Mainmenu" runat="server" clientidmode="Static" class="algnCentr" visible="true">
        <ul id="Mainmenuul">
            <li id="menuitem_primary"><a id="primarylnk" href = '<%#Page.ResolveUrl("~/primary")%>'><span class="tile_icon Primary"
                clientidmode="Static" id="Primary" runat = "server"></span></a></li>
            <li id="menuitem_secondary"><a id="secondarylnk" href = '<%#Page.ResolveUrl("~/secondary")%>'><span class="tile_icon Secondary"
                clientidmode="Static" id="Secondary" runat = "server"></span></a></li>
            <li id="menuitem_staffroom"><a id="staffroomlnk" href = '<%#Page.ResolveUrl("~/staff-room")%>'><span class="tile_icon StaffRoom"
                clientidmode="Static" id="StaffRoom" runat = "server"></span></a></li>
            <li id="menuitem_aboutus"><a id="aboutuslnk" href = '<%#Page.ResolveUrl("~/about-us")%>' class="menuitem"><span
                clientidmode="Static" class="tile_icon AboutUs" id="AboutUs" runat = "server"></span></a></li>
        </ul>
    </div>
    <div id="DashBoardMenu" runat="server" clientidmode="Static" class="algnCentr" visible="false">
        <ul id="Ul1">
            <li id="menuitem_user_dash"><a id = "Dashboardlnk" href = "/dashboard.aspx"><span class="dashboardtile_icon DbDashboard" id="Dashbaord"
                clientidmode="Static" runat = "server"></span></a></li>
            <li id="menuitem_primary_dash"><a id="primarylnk_dash" href = '<%#Page.ResolveUrl("~/primary")%>'><span class="dashboardtile_icon DbPrimary" id="dashbaordprimarylnk" clientidmode="Static" runat = "server"></span></a></li>
            <li id="menuitem_secondary_dash"><a id="secondarylnk_dash" href = '<%#Page.ResolveUrl("~/secondary")%>'><span class="dashboardtile_icon DbSecondary" id="dashbaordsecondarylnk"  clientidmode="Static"  runat = "server"></span></a></li>
            <li id="menuitem_staffroom_dash"><a id="staffroomlnk_dash" href = '<%#Page.ResolveUrl("~/staff-room")%>'><span class="dashboardtile_icon DbStaffRoom"  id="dashbaordstaffroomlnk"   clientidmode="Static"  runat = "server"></span></a></li>
            <li id="menuitem_aboutus_dash"><a id="aboutuslnk_dash" href = '<%#Page.ResolveUrl("~/about-us")%>'><span class="dashboardtile_icon DbAboutUs"  id="dashbaordaboutuslnk" clientidmode="Static" runat = "server"></span></a>
            </li>
        </ul>
    </div>
    <div id="EcollectionMenu" runat="server" clientidmode="Static" class="algnCentr" visible="false">
        <ul id="Ul2">
            <li id="Li1"><a id = "Dashboardecollnk" runat = "server" clientidmode="Static"  href = "/dashboard.aspx"><span class="dashboardecoll_icon DbecolDashboard" id="Dashboardecoll"
                clientidmode="Static" runat = "server"></span></a></li>
                <li id="Li2"><a id = "eCollectionecollnk_dash" runat = "server" clientidmode="Static" href = '<%#Page.ResolveUrl("~/ecollection")%>' ><span class="dashboardecoll_icon Dbecollection" id="eCollectionecollnk"
                clientidmode="Static" runat = "server"></span></a></li>
            <li id="Li3"><a id="primaryecollnk_dash" runat = "server" clientidmode="Static" href = '<%#Page.ResolveUrl("~/primary")%>'><span class="dashboardecoll_icon DbecolPrimary" id="primaryecollnk" clientidmode="Static" runat = "server"></span></a></li>
            <li id="Li4"><a id="secondaryecollnk_dash" runat = "server" clientidmode="Static" href = '<%#Page.ResolveUrl("~/secondary")%>'><span class="dashboardecoll_icon DbecolSecondary" id="secondaryecollnk"  clientidmode="Static"  runat = "server"></span></a></li>
            <li id="Li5"><a id="staffroomecollnk_dash" runat = "server" clientidmode="Static" href = '<%#Page.ResolveUrl("~/staff-room")%>'><span class="dashboardecoll_icon DbecolStaff"  id="staffroomecollnk"   clientidmode="Static"  runat = "server"></span></a></li>
            <li id="Li6"><a id="aboutusecollnk_dash" runat = "server" clientidmode="Static" href = '<%#Page.ResolveUrl("~/about-us")%>'><span class="dashboardecoll_icon DbecolAboutUs"  id="aboutusecollnk" clientidmode="Static" runat = "server"></span></a>
            </li>
        </ul>
    </div>
