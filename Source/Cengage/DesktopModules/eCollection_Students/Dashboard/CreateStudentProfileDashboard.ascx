<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateStudentProfileDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Dashboard.CreateStudentProfileDashboard" %>
<div id="ECollLeftModule" class="ECollLeftModule" style="margin-top: -245px;">
    <div id="CreatePageFinishBtn" class="CancelButtonHolder  srtbtnshide" style="margin-bottom: 10px;
        width: 79%">
        <asp:Button ID="Backtocreategroupbtn" runat="server" ClientIDMode="Static" Text="CANCEL MAKING STUDENT"
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
        <div id="CreatePageBtn" class="ButtonsDiv srtbtnshide">
            <%--<div class="ActiveAddButtonsHolder">
                <asp:Button ID="BulkUploadButton" runat="server" Text="BULK UPLOAD" CssClass="BtnStyle"
                    OnClick="BulkUploadButton_Click" ClientIDMode="Static" />
            </div>--%>
             <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="BulkUploadButton" runat="server" Text="BULK UPLOAD" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>  
        </div>
        <div id="ProfilePageBtn" class="ButtonsDiv srtbtnshide">
           <%-- <div class="ActiveAddButtonsHolder">
                <asp:Button ID="EditProfileButton" runat="server" Text="EDIT PROFILE" CssClass="BtnStyle"
                    OnClick="EditProfileButton_Click" ClientIDMode="Static" />
            </div>--%>
             <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="EditProfileButton" runat="server" Text="EDIT PROFILE" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>    
        </div>
        <div id="BackToProfileBtn" class="ButtonsDiv srtbtnshide">
            <%--<div class="ActiveAddButtonsHolder">
                <asp:Button ID="BackToProfileButton" runat="server" Text="BACK TO PROFILE" CssClass="BtnStyle"
                    OnClick="BackToProfileButton_Click" ClientIDMode="Static" />
            </div>--%>
             <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="BackToProfileButton" runat="server" Text="BACK TO PROFILE" ClientIDMode="Static"
                    CssClass="BtnStyle eColNavigationLink"/>
            </div> 
        </div>
    </center>
</div>
