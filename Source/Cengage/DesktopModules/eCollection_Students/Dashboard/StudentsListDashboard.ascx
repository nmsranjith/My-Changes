<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentsListDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Dashboard.StudentsListDashboard" %>
<div class="ECollLeftModule">
    <center>
        <asp:Label ID="TestName" runat="server"></asp:Label>
        <div class="ButtonsDiv">
            <%--<div class="ActiveAddButtonsHolder">
                <asp:Button ID="CreateStudentProfileButton1" runat="server" Text="CREATE STUDENT PROFILE" ClientIDMode="Static" formtarget="_blank" 
                    CssClass="BtnStyle" OnClick="CreateStudentProfileButton_Click" UseSubmitBehavior="false"/>             
            </div> --%>            
            <div class="eColNavigationLinkHdr">
                <asp:HyperLink ID="CreateStudentProfileButton" runat="server" Text="CREATE STUDENT PROFILE" ClientIDMode="Static" formtarget="_blank" 
                    CssClass="BtnStyle eColNavigationLink"/>
            </div>            
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="CreateReadingSessionButton" runat="server" Text="CREATE A READING SESSION" formtarget="_blank" 
                    Enabled="false" CssClass="DbldBtn" ClientIDMode="Static" OnClick="CreateReadingSessionButton_Click"/>          
            </div>
        </div>
         <div class="eCollection_Menu_MidHolder">
            <hr class="eCollection_Menu_Mid_hr" />
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="AddStudentToGroupButton" runat="server" Text="ADD STUDENT/S TO A GROUP"
                    CssClass="DbldBtn" Enabled="false" ClientIDMode="Static" OnClick="AddStudentToGroupButton_Click" OnClientClick="GetSelectedStudents()" />
                          </div>
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledAddButtonHolder">
                <asp:Button ID="PrintStudentCardButton" runat="server" Text="PRINT STUDENT CARDS" 
                    CssClass="DbldPrintBtn" Enabled="false" ClientIDMode="Static" 
                    OnClick="PrintStudentCardButton_Click" 
                    formtarget="_blank"  />             
            </div>
        </div>
        <div class="ButtonsDiv">
            <div class="DisabledDeleteButtonHolder">
                <asp:Button ID="DeleteStudentButton" runat="server" Text="DELETE STUDENT PROFILE/S" 
                    CssClass="DbldDelBtn" Enabled="false" ClientIDMode="Static" OnClick="DeleteStudentButton_Click" />
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

