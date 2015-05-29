<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateTeacherProfileDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Dashboard.CreateTeacherProfileDashboard" %>
<div class="ECollLeftModule" style="margin-top: -40px;">
    <div id="BcktoCreateGrpBtn" class="CancelButtonHolder" style="margin-bottom: 10px; width: 79%">
        <asp:Button ID="Backtocreategroupbtn" runat="server" ClientIDMode="Static" Text="FINISH CREATING PROFILE"
            CssClass="DbldEndBtn finishBtns" OnClick="Backtocreategroupbtn_Click" />
    </div>
    <div id="FinishAddtoGrpBtn" class="ButtonsDiv srtbtnshide">
        <div class="CancelButtonHolder" style="margin-bottom: 10px; width: 79%">
            <asp:Button ID="FinishButton" runat="server" ClientIDMode="Static" Text="FINISH ADDING TO GROUP"
                CssClass="DbldEndBtn finishBtns" OnClick="Backtocreategroupbtn_Click" />
        </div>
    </div>
    <div id="eCollection_Menu_MidHolder" class="eCollection_Menu_MidHolder">
        <hr class="eCollection_Menu_Mid_hr" />
    </div>
    <center>
        <div id="BlkUpldBtn" class="ButtonsDiv">
          <%--  <div class="ActiveAddButtonsHolder">
                <asp:Button ID="BulkUploadButton" runat="server" Text="BULK UPLOAD" CssClass="BtnStyle"
                    OnClick="BulkUploadButton_Click" />
            </div>--%>
             <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="BulkUploadButton" runat="server" Text="BULK UPLOAD" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div> 
        </div>     
    </center>
</div>
