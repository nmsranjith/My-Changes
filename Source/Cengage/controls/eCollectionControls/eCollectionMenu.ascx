<%@ Control Language="C#" AutoEventWireup="true" CodeFile="eCollectionMenu.ascx.cs"
    Inherits="DotNetNuke.UI.eCollectionControls.eCollectionMenu" %>
<ul class="eCollection_Menu_Holder">
    <li id="SubscriptionTabHolder" class="MenuHolder" runat="server" clientidmode="Static">
        <asp:HyperLink ID="SubscriptionTab" CssClass="eCollection_Menu4" Text="SUBSCRIPTIONS"
            runat="server" ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="DashboardTabHolder" class="MenuHolder">
        <asp:HyperLink ID="DashboardTab" CssClass="eCollection_Menu1" Text="DASHBOARD" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="StudentsTabHolder" class="MenuHolder">
        <asp:HyperLink ID="StudentsTab" CssClass="eCollection_Menu2" Text="STUDENTS" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="GroupsTabHolder" class="MenuHolder">
        <asp:HyperLink ID="GroupsTab" CssClass="eCollection_Menu3" Text="GROUPS" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="SessionTabHolder" class="MenuHolder">
        <asp:HyperLink ID="SessionsTab" CssClass="eCollection_Menu4" Text="SESSIONS" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="BooksTabHolder" class="MenuHolder">
        <asp:HyperLink ID="BooksTab" CssClass="eCollection_Menu5" Text="BOOKS" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="TeachersTabHolder" class="MenuHolder">
        <asp:HyperLink ID="TeachersTab" CssClass="eCollection_Menu6" Text="TEACHERS" runat="server"
            ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="eBookManagementTabHolder" class="MenuHolder" runat="server" clientidmode="Static">
        <asp:HyperLink ID="eBookManagementTab" CssClass="eCollection_Menu5" Style="background-color: white;
            width: 78.6%;  height: 13px; border: 1px solid Lightgray;text-align: center;
            margin-bottom: 10px;  margin-left: 24px; cursor: pointer; font-weight: bold;
            font-size: 9pt; color: #707070; text-decoration: none; float: left; padding: 11px 0px 11px 0px;"
            Text="eBOOK MANAGEMENT" runat="server" ClientIDMode="Static"></asp:HyperLink>
    </li>
    <li id="AppDataTabHolder" class="MenuHolder" runat="server" clientidmode="Static">
        <asp:HyperLink ID="AppDataCollectionTab" CssClass="eCollection_Menu5" Style=" background-color: white;
            width: 78.6%;  height: 13px; border: 1px solid Lightgray;text-align: center;
            margin-bottom: 10px;  margin-left: 24px; cursor: pointer; font-weight: bold;
            font-size: 9pt; color: #707070; text-decoration: none; float: left; padding: 11px 0px 11px 0px;"
            Text="eCOLLECTION REPORT" runat="server" ClientIDMode="Static"></asp:HyperLink>
    </li>
</ul>
<div id="eCollectionMenuMid" class="eCollection_Menu_MidHolder" runat="server" clientidmode="Static">
    <hr class="eCollection_Menu_Mid_hr" />
</div>
<asp:HiddenField ID="RoleChkHdn" runat="server" ClientIDMode="Static" />