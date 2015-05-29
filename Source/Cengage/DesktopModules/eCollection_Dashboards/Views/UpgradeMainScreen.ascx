<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpgradeMainScreen.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.UpgradeMainScreen" %>
<div class="row">
    <div id="thankyou">
    </div>
    <div id="formcontent">
        <p class="thankyouhdr">
            Would you like to keep some (or all) of your teacher and student information
            from last year?</p>
        <div class="note">
            <p class="notemsg">
                <strong class="noteheader">NOTE:</strong> This is a permanent action. Please be careful, By selecting
                'no, start fresh'  you are deleting all student and teacher information. This can't
                be undone. If you have any questions please contact <a href="/contact-us" style="cursor:pointer;text-decoration:none;color:#01b4d8">customer service.</a></p>
        </div>
        <div class="buttons" style="margin-top: 30px;">
            <asp:Button runat="server" Text="NO, START FRESH" CssClass="pinkBtnUpgrade" ClientIDMode="Static"
                OnClick="FreshStartBtn_Click" />
            <asp:Button runat="server" Text="YES, BEGIN WIZARD" CssClass="greenBtnUpgrade" ClientIDMode="Static"
                OnClick="UpgradeWizardBtn_Click" />
        </div>
    </div>
</div>
<asp:HiddenField runat="server" ID="jscheck" ClientIDMode="Static" Value="main" />
<script src="desktopmodules/ecollection_dashboards/Scripts/UpgradeStepOne.js" type="text/javascript"></script>
