<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupListDashBoardMenu.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupListDashBoardMenu" %>
<div>
    <center>
        <br />
        <%-- <div id="CreateGroupDiv" class="ActiveAddButtonsHolder" style="float: left; margin-left: 23px;">
            <asp:Button ID="CreateGroupButton" runat="server" Text="CREATE A GROUP" UseSubmitBehavior="false"
                CssClass="BtnStyle creategroupbtn" OnClick="CreateGroupButton_Click" Enabled="true" />
        </div>--%>
        <div id="CreateGroupDiv" class="eColNavigationLinkHdr">
            <asp:HyperLink ID="CreateGroupButton" runat="server" Text="CREATE A GROUP" ClientIDMode="Static"
                CssClass="BtnStyle eColNavigationLink" />
        </div>
        <br />
        <div id="StartReadingSessionDiv" class="DisabledAddButtonHolder" style="float: left;
            margin-left: 23px;">
            <asp:Button ID="StartReadingSessionButton" runat="server" ClientIDMode="static" Text="CREATE A READING SESSION"
                formtarget="_blank" CssClass="DbldBtn startreadingsesionbtn" Enabled="false"
                OnClick="StartReadingSessionButton_Click" />
        </div>
        <br />
        <div id="EditGroupDiv" class="DisabledAddButtonHolder" style="float: left; margin-left: 23px;">
            <asp:Button ID="EditGroupButton" runat="server" Text="EDIT GROUP" ClientIDMode="static"
                CssClass="DbldBtn editgroupbtn" Enabled="false" OnClick="EditGroupButton_Click" />
        </div>
        <br />
        <div id="MergeGroupDiv" class="DisabledAddButtonHolder" style="float: left; margin-left: 23px;">
            <asp:Button ID="MergeGroupButton" runat="server" ClientIDMode="static" Text="MERGE GROUPS"
                CssClass="DbldBtn mergegroupbtn" Enabled="false" OnClick="MergeGroupButton_Click" />
        </div>
        <br />
        <div id="DeleteGroupDiv" class="DisabledDeleteButtonHolder" style="float: left; margin-left: 23px;">
            <asp:Button ID="DeleteGroupButton" runat="server" ClientIDMode="static" Text="DELETE GROUP"
                CssClass="DbldDelBtn deletegroupbtn" Enabled="false" OnClick="DeleteGroupButton_Click" />
        </div>
        <asp:HiddenField ID="selectedGroupID" ClientIDMode="Static" runat="server" />
        <div id="CengageStagingDiv" style="float: left; margin-left: 23px; width: 86%;">
            <asp:Button ID="CengageStagingButton" runat="server" Text="Click to see CengageStaging"
                Style="float: left; background: transparent; display: none; border: none; color: #707070;
                cursor: pointer;" Enabled="true" OnClick="CengageStagingButton_Click" />
        </div>
        <br />
        <br />
        <br />
        <br />
        <a id="ClicktoAfterRenewel" style="float: left; display: none; margin-left: 30px;"
            clientidmode="Static" runat="server" href="#">Click to see After Renewel</a>
    </center>
</div>
<div id="AfterRenewelPopUp" style="display: none;" class="AfterRenewelPopUpClass">
    <div style="background-image: url('Portals/0/images/topband.png'); height: 110px;
        margin-top: -9px; width: 102%; margin-left: -7px;">
        <span class="AfterRenewelHeaderSpan" style="margin-top: 13px;">Welcome to </span>
        <asp:Label ID="AfterRenewelYear" CssClass="AfterRenewelYearLabel" Style="margin-top: 13px;
            float: left" runat="server" Text="2014"></asp:Label><br />
        <span class="AfterRenewelHeaderSpan">Thanks for renewing your subscription:</span>
    </div>
    <div style="box-shadow: 0px 0px 8px gray; -moz-box-shadow: 0 8px 8px gray; -webkit-box-shadow: 8 0px 0px gray;
        float: left; width: 103%; margin-left: -7px; height: 87%;">
        <center>
            <div class="AfterRenewelContentDiv" style="margin-top: 50px !important">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckAllTeachers" runat="server" Checked="true" />
                <span class="AfterRenewelContentSpan">Keep all teacher profiles</span>
            </div>
            <div class="AfterRenewelContentDiv">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckAllStudents" runat="server" Checked="true" />
                <span class="AfterRenewelContentSpan">Keep all student profiles</span>
                <asp:Label ID="licencesCount" Text="(you have 14 more students than licences)" CssClass="AfterRenewelContentLabel"
                    runat="server"></asp:Label>
            </div>
            <div class="AfterRenewelContentDiv">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckMoveall" runat="server" Checked="true" />
                <span class="AfterRenewelContentSpan">Move all students up one year level</span>
            </div>
            <div class="AfterRenewelContentDiv">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckStart" runat="server" Checked="true" />
                <span class="AfterRenewelContentSpan">Start with same books as last year</span>
                <asp:Label ID="DaysLeftLabel" Text="(you will have 30 days to make swap books)" CssClass="AfterRenewelContentDays"
                    runat="server"></asp:Label>
            </div>
            <asp:Button ID="StartButton" runat="server" class="ActiveAddButtonsHolder" Text="Start 2014"
                Style="font-family: Raleway,Arial; font-weight: 700; font-size: 10pt;" />
            <asp:LinkButton ID="AdvancedSetUp" runat="server" PostBackUrl="http://localhost:49784/cengageecollectionwork/eCollection/AfterRenewalAdvance.aspx"
                Text="Advanced Set Up >>"></asp:LinkButton>
        </center>
    </div>
    <div id="Delete-message" title="Alert message!" style="display: none; background: white !important;">
        <div class="deletegrphdrpp">
            <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Confirm Delete</span>
        </div>
        <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
            box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
            height: 87%;">
            <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                <span id="MessageLiteral" style="font-family: Raleway, Arial, sans-serif; font-size: 10pt;
                    color: #707070; padding: 23px; float: left;">Are you sure you want to delete group/s?</span>
            </div>
            <div style="width: 92%;">
                <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
                <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
            </div>
        </div>
    </div>
    <div id="dialogmessage" title="Alert message!" style="display: none; background: white !important;">
        <div style="background-image: url('Portals/0/images/topband.png'); background-color: White;
            height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
            <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
        </div>
        <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
            box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
            height: 87%;">
            <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                <asp:Label ID="AlertmessageLabel" runat="server" Style="font-family: Raleway, Arial, sans-serif;
                    font-size: 10pt; color: #707070; padding: 30px; float: left;">Cannot merge group, the groups having different subscription</asp:Label>
            </div>
            <div style="width: 92%;">
                <input type="button" id="MergePopuOkButton" value="Ok" class="popupokbtn" />
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    var flag;
    function SetPopUpFlag() {
        flag = true;
    }
    var kwindow;
    jQuery(document).ready(function () {
        if (flag) {
            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].click();
                }
            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].click();
                }
            }
            $("#dialogmessage").css({ 'display': 'block' });
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            kwindow = $("#dialogmessage"); //Give ur div id here
            if (!kwindow.data("kendoWindow")) {
                kwindow.kendoWindow({
                    width: "665px",
                    height: "300px",
                    modal: true,
                    draggable: false
                });
                kwindow.data("kendoWindow").center();
            }
            kwindow.data("kendoWindow").open();
            $(".k-icon.k-i-close").hide();
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                return false;
            });

        }
        $("#MergePopuOkButton").click(function () { kwindow.data("kendoWindow").close(); return false; });
        $("#EditGroupButton").click(function () {
            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value = $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," + "C";
                }
            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value = $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," + "N";
                }
            }
            for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," + "N";
                }
            }
            for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," + "C";
                }
            }

        });
        $("#MergeGroupButton").click(function () {
            document.getElementById("selectedGroupID").value = "";

            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," ;
                }
            }
        });
        $("#StartReadingSessionButton").click(function () {

            document.getElementById("selectedGroupID").value = "";

            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + "," + "C";
                }
            }
        });
        var dkwindow;
        var deleteFlag;
        $("#DeleteGroupButton").click(function () {
            if (!deleteFlag) {
                $("#Delete-message").css({ 'display': 'block' });
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                dkwindow = $("#Delete-message"); //Give ur div id here
                if (!dkwindow.data("kendoWindow")) {
                    dkwindow.kendoWindow({
                        width: "665px",
                        height: "300px",
                        modal: true,
                        draggable: false
                    });
                    dkwindow.data("kendoWindow").center();
                }
                dkwindow.data("kendoWindow").open();
                $(".k-icon.k-i-close").hide();
                $('a.k-window-action.k-link').mouseover(function () {
                    $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                    return false;
                });
                return false;
            }
            return true;
        });
        $("#YesButton").click(function () {
            dkwindow.data("kendoWindow").close();
            document.getElementById("selectedGroupID").value = "";
            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }
            for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    document.getElementById("selectedGroupID").value += $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
                }
            }

            deleteFlag = true;
            $("#DeleteGroupButton").click();
            return false;
        });
        $("#NoButton").click(function () {
            dkwindow.data("kendoWindow").close();
            return false;
        });
        $("#AfterRenewelPopUp div div.AfterRenewelPopUpClassimg").click(function () {
            this.parentNode.children[1].click();
            if (this.parentNode.children[1].checked) {
                this.parentNode.children[0].className = " "; // ("AfterRenewelPopUpClassimg");
                this.parentNode.children[0].className = "AfterRenewelPopUpClassimg";
            }
            else {
                this.parentNode.children[0].className = " ";
                this.parentNode.children[0].className = "AfterRenewelPopUpClassunimg";

            }

        });
        function onResize() {
            return 0;
        }
        $("#ClicktoAfterRenewel").click(function () {
            $("#AfterRenewelPopUp").css({ 'display': 'block' });
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            var window = $("#AfterRenewelPopUp"); //Give ur div id here
            if (!window.data("kendoWindow")) {
                window.kendoWindow({
                    width: "730px",
                    height: "610px",
                    modal: true,
                    draggable: false
                });
                window.data("kendoWindow").center();
            }
            window.data("kendoWindow").open();
            $(".k-icon.k-i-close").hide();
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                return false;
            });
            return false;
        });

    });
    
</script>
