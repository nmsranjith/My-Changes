<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupProfileDashBoard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupProfileDashBoard" %>
<div>
    <center>
        <br />
        <%--<div id="EditGroupDiv" class="ActiveAddButtonsHolder" style="float: left; margin-left: 23px;">
            <asp:Button ID="EditGroupButton" runat="server" ClientIDMode="static" Text="EDIT GROUP"
                UseSubmitBehavior="false" CssClass="BtnStyle editgroupbtn"
                OnClick="EditGroupButton_Click" />
        </div>--%>
        <div id="EditGroupDiv" class="eColNavigationLinkHdr">
            <asp:HyperLink ID="EditGroupButton" runat="server" Text="EDIT GROUP" ClientIDMode="Static"
                CssClass="BtnStyle eColNavigationLink" />
        </div>
        <br />
        <div id="PrintCredentialButtonDiv" clientidmode="Static" runat="server" class="ActiveAddButtonsHolder" style="float: left;
            margin-left: 23px;">
            <asp:Button ID="PrintCredentialButton" runat="server" ClientIDMode="static" UseSubmitBehavior="false"
                 OnClientClick="javascript:target='_blank';"  Text="PRINT STUDENT CARDS" CssClass="PrintBtn startreadingsesionbtn"
                OnClick="PrintCredentialButton_Click" />
        </div>       
    </center>
</div>
