<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentsProfileBulkUploadDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Dashboard.StudentsProfileBulkUploadDashboard" %>
<div class="ECollLeftModule" style="margin-top: -250px;">
    <div class="CancelButtonHolder" style="margin-bottom: 10px; width: 79%">
        <asp:Button ID="Backtocreategroupbtn" runat="server" ClientIDMode="Static" Text="CANCEL MAKING STUDENTS"
            CssClass="DbldEndBtn finishBtns" OnClick="Backtocreategroupbtn_Click" />
    </div>
    <div class="eCollection_Menu_MidHolder">
        <hr class="eCollection_Menu_Mid_hr" />
    </div>
    <center>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="BulkUploadButton" runat="server" Text="BULK UPLOAD" CssClass="DbldBtn"
                    Enabled="false" />
            </div>
        </div>
    </center>
</div>
