<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeachersListDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Dashboard.TeachersListDashboard" %>
<div class="ECollLeftModule">
    <center>
        <div class="ButtonsDiv">
           <%-- <div class="ActiveAddButtonsHolder">
                <asp:Button ID="CreateTeacherProfileButton" runat="server" Text="CREATE TEACHER PROFILE"
                    CssClass="BtnStyle" OnClick="CreateTeacherProfileButton_Click" ClientIDMode="Static" />
            </div>--%>
             <div id="CreateProfileHdr" runat="server" clientidmode="Static" class="eColNavigationLinkHdr">
                <asp:HyperLink ID="CreateTeacherProfileButton" runat="server" Text="CREATE TEACHER PROFILE" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>  
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="CreateReadingSessionButton" runat="server" Text="CREATE A READING SESSION"
                    Enabled="false" CssClass="DbldBtn" ClientIDMode="Static" OnClick="CreateReadingSessionButton_Click" />
            </div>
        </div>
        <div id="List_MidHdr" runat="server" class="eCollection_Menu_MidHolder">
            <hr class="eCollection_Menu_Mid_hr" />
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="AddTeacherToGroupButton" runat="server" Text="ADD TEACHER/S TO A GROUP"
                    CssClass="DbldBtn" Enabled="false" ClientIDMode="Static" OnClick="AddTeacherToGroupButton_Click" />
            </div>
        </div>
        <div class="ButtonsDiv">
            <div id="DeleteProfileHdr" runat="server" clientidmode="Static" class="DisabledDeleteButtonHolder">
                <asp:Button ID="DeleteTeacherButton" runat="server" Text="DELETE TEACHER PROFILE/S"
                    CssClass="DbldDelBtn" Enabled="false" ClientIDMode="Static" OnClick="DeleteTeacherButton_Click" />
            </div>
        </div>
        <div class="eCollection_Menu_MidHolder">
            <hr class="eCollection_Menu_Mid_hr" />
        </div>
    </center>
</div>
<asp:HiddenField ID="SelectedStuds" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="UserLoginNameHdn" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="SubscriptionId" runat="server" ClientIDMode="Static" />
