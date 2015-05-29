<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeStepOne.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.UpgradeStepOne" %>
<div class="row">
    <div id="formbanner">
    </div>
    <div id="formcontent">
        <p class="steponehdr">
            Please select the information you would like to keep from the options below:</p>
        <div class="lists " id="TeachersCheckDiv" style="color: #000">
            <div class="Div_FullWidth upgradedivmargintop">
                <asp:CheckBox runat="server" ClientIDMode="Static" ID="TeachersCheck" Checked="true" /><label><span
                    id="TchChkSpan"></span>Keep all teachers attached to this subscription.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    Teacher access is free and does not count against your purchased</p>
                <p>
                    licenses.</p>
                <span class="hypenspan upgradeMargintop">- </span>
                <p class="upgradeMargintop">
                    You can remove individual teacher access from your accounts</p>
                <p>
                    dashboard at any time.</p>
            </div>
        </div>
        <div class="lists " id="StudentsCheckDiv" style="color: #000">
            <div class="Div_FullWidth upgradedivmargintop">
                <asp:CheckBox runat="server" ClientIDMode="Static" ID="StudCheck" Checked="true" /><label><span
                    id="StdChkSpan"></span>Keep all student information.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    This includes profile information, reading history, PM reading level, reading age,
                </p>
                <p>
                    student recordings &nbsp;and My Words.</p>
            </div>
        </div>
       <%-- <div class="lists " clientidmode="Static" id="RenewUpgradeToogle">--%>
        
        <div class="lists " id="GrpsCheckDiv" style="color: #000" clientidmode="Static" runat="server">
            <div class="Div_FullWidth upgradedivmargintop">
                <asp:CheckBox  runat="server" ClientIDMode="Static" ID="GroupsCheck" Checked="true" /><label><span
                    id="GrpChkSpan"></span>Keep all groups and classes.</label></div>
        </div>
     <%--   </div>--%>
        <div class="lists " clientidmode="Static" id="MOveAllStudentDiv" runat="server" style="color: #000;display:none;">
            <div class="Div_FullWidth upgradedivmargintop">
                <asp:CheckBox runat="server" ClientIDMode="Static" ID="MOveAllStudent" Checked="true" /><label><span
                    id="MoveStdChkSpan"></span>Move all students up one year level.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan"> </span>
                <p>
                    
                </p>
                <p>
                </p>
            </div>
        </div>
        <div class="lists " id="BoksCheckDiv" style="color: #000; padding-top: 5px">
            <div class="Div_FullWidth upgradedivmargintop">
                <asp:CheckBox runat="server" ClientIDMode="Static" ID="BooksCheck" Checked="true" /><label><span
                    id="BkChkSpan"></span>Use the same books as last year.</label></div>
            <div class="Div_FullWidth">
                <span class="hypenspan">- </span>
                <p>
                    You are able to modify your book selection for the first 30 days of your
                </p>
                <p>
                    subscription.</p>
            </div>
        </div>
        <div class="warning" id="WarningDiv" style="display: none">
            <h6 style="color: #000; font-weight: bold">
                WARNING:</h6>
            <p style="font-size: 9pt;">
                Unfortunately we will not be able to automatically migrate the following information
                to your upgraded subscription.</p>
            <div class="red">
            </div>
            <p style="font-size: 9pt;">
                For more information and help please contact <a href="/contact-us" style="cursor: pointer;
                    text-decoration: none; color: #01b4d8">customer service.</a></p>
        </div>
        <div class="buttons">
            <asp:Button Text="<< Back" ID="BackBtn" CssClass="greybtnUpgrade" runat="server"
                ClientIDMode="Static" OnClick="BackBtn_Click" />
            <asp:Button ID="ContinueBtn" Text="Continue" CssClass="greenBtnUpgrade" runat="server"
                ClientIDMode="Static" OnClick="ContinueStepsBtn_Click" />
            <%--<span runat="server" id="ContinueToStep2" visible="false" onclick="ContinueStepsBtn_Click" clientidmode="Static" />--%>
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="Upgradeflaghdn" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" ID="UpgradeStepCnt" ClientIDMode="Static" Value="1" />
<asp:HiddenField runat="server" ID="IsLicLesshdn" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" ID="IsQtyLesshdn" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" ID="BackClickhdn" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" ID="jscheck" ClientIDMode="Static" Value="one" />
<asp:HiddenField runat="server" ID="renewupgradehdn" ClientIDMode="Static" Value="0" />
<script src="desktopmodules/ecollection_dashboards/Scripts/UpgradeStepOne.js" type="text/javascript"></script>
