<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateGroup.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.CreateGroup" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
<style type="text/css">
    .CreateGroupDiv .k-dropdown-wrap
    {
        background-color: white !important;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0') !important;
        box-shadow: 0px 2px 5px 0px lightgray !important;
    }
    .k-window-action
    {
        background-image: url('Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: -32px !important;
        border: none !important;
        margin-left: -10px;
    }   
    .k-window-action.k-link.k-state-hover:hover
    {
        background-color:transparent !important;
        margin-top: -25px !important; 
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    // $(document).ready(function () {
    var kswindow;
    function DeleteOwnerMessage() {
        $("#dialogmessage").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        kswindow = $("#dialogmessage"); //Give ur div id here
        if (!kswindow.data("kendoWindow")) {
            kswindow.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            kswindow.data("kendoWindow").center();
        }
        kswindow.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
    }

    var kwindow;
    function PostBack() {

        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-295px');
        }

        if ($("#StudentDetailsDiv")[0].children.length == 0) {
            $("#StudentDetailsDiv").css("display", "none");
        }
        if ($("#TeacherDetailsDiv")[0].children.length == 0) {
            $("#TeacherDetailsDiv").css("display", "none");
        }
        jQuery('#eCollectionContent').height((jQuery('.CreateEditGroupDivSet').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 9) + 'px');


        $(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                for (var i = 0; i < $(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                    }
                }

            }
            else {
                for (var i = 0; i < $(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    }
                }

            }
        });

        $(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                for (var i = 0; i < $(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                    }
                }

            }
            else {
                for (var i = 0; i < $(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    }
                }
            }
        });
        $("#TeacherDetailsRepeaterDiv input[type=image]").click(function () {
            $(this.parentNode.parentNode.parentNode).remove();
        });
        $("#StudentDetailsRepeaterDiv input[type=image]").click(function () {
            $(this.parentNode.parentNode.parentNode).remove();
        });
        $("#SubscriptionDrpList").kendoDropDownList({ animation: false });
        $('#<%=GroupNameTextBox.ClientID%>').keydown(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                return false;
            }
        });
        $("#<%=GroupNameTextBox.ClientID%>").focus(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                return false;
            }
        });
        $("#<%=GroupNameTextBox.ClientID%>").blur(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                return false;
            }
        });
        if ($('#ClassCheckHiddenField').val() == "true") {
            $('#<%=ClassCheckBox.ClientID %>')[0].checked = true;
            if ($('#CheckBoxDiv').hasClass("groupuncheckboximg")) {
                $('#CheckBoxDiv').removeClass("groupuncheckboximg");
            }
        }
        else {
            $('#<%=ClassCheckBox.ClientID %>')[0].checked = false;
            if (!$('#CheckBoxDiv').hasClass("groupuncheckboximg")) {
                $('#CheckBoxDiv').addClass("groupuncheckboximg");
            }
        }

        if ($('#<%=ClassCheckBox.ClientID %>')[0].checked) {
            this.className = "groupcheckboximg";            
        }
        else {
            this.className = "groupcheckboximg groupuncheckboximg";            
        }
        $('#CheckBoxDiv').click(function () {
            $('#<%=ClassCheckBox.ClientID %>').click();
            if ($('#<%=ClassCheckBox.ClientID %>')[0].checked) {
                this.className = "groupcheckboximg";
                $('#ClassCheckHiddenField').val("true");
            }
            else {
                this.className = "groupcheckboximg groupuncheckboximg";
                $('#ClassCheckHiddenField').val("false");
            }
        });

        $("#CancelButton").click(function () {
            $("#SubscriptionDrpList option:contains(" + document.getElementById("RisePopUp").value.trim() + ")").attr('selected', 'selected');
            //$("#SubscriptionDrpList-list ul li:contains(" + document.getElementById("RisePopUp").value.trim() + ")").attr('selected', 'selected');
            for (var t = 0; t < $("#SubscriptionDrpList-list ul li").length; t++) {
                if ($("#SubscriptionDrpList-list ul li")[t].innerHTML == document.getElementById("RisePopUp").value.trim()) {
                    $("#SubscriptionDrpList-list ul li")[t].click();
                }
            }
            //$("#SubscriptionDrpListdiv .k-input")[0].innerHTML = document.getElementById("RisePopUp").value.trim();
            kwindow.data("kendoWindow").close(); return false;
        });
        $("#PopuOkButton").click(function () {
            kwindow.data("kendoWindow").close();
            document.getElementById("RisePopUp").value = '';
            $("#SubscriptionDrpList").change();
        });
    }
    function onClose() {
        $("#SubscriptionDrpList option:contains(" + document.getElementById("RisePopUp").value.trim() + ")").attr('selected', 'selected');
        for (var t = 0; t < $("#SubscriptionDrpList-list ul li").length; t++) {
            if ($("#SubscriptionDrpList-list ul li")[t].innerHTML == document.getElementById("RisePopUp").value.trim()) {
                $("#SubscriptionDrpList-list ul li")[t].click();
            }
        }
    }
    function okclick() {
        kswindow.data("kendoWindow").close();
        $('#TeacherDelete').val('delete');
        $('#TeacherDelete').click();
    }
    function cancelclick() {
        $('#TeacherDelete').val('');
        kswindow.data("kendoWindow").close(); return false;
    }
</script>
<div class="CreateEditGroupDivSet">
    <asp:UpdatePanel ID="CreateGroupUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:HiddenField ID="RisePopUp" runat="server" ClientIDMode="Static" />
            <div id="dialog-message" title="Alert message!" class="eBkcntmgtdigmsgdiv">
                <div class="CrGrpPPMsgdiv">
                    <span class="AfterRenewelHeaderSpan eBkcntmgthdrspan">Alert Message!</span>
                </div>
                <div class="eBkcntmgtPPMsgdiv">
                    <div class="eBkcntmgtPPMsginnerdiv;">
                        <span id="MessageLiteral" class="CrGrpalertmsg"></span>
                    </div>
                    <div class="CrGrpbtnsdiv">
                        <input type="button" id="PopuOkButton eBkcntmgtPPokbtn" value="Continue"
                            class="popupokbtn" />
                        <input type="button" id="CancelButton eBkcntmgtPPcnclbtn" value="Cancel"
                            class="popupokbtn" />
                    </div>
                </div>
            </div>
            <div id="MessagesDiv">
                <uc1:Messages ID="Messages" runat="server" />
            </div>
            <div class="CreateGroupDiv">
                <h5>
                    Group name:</h5>
                <br />              
                <div id="GroupNameTextDiv">
                    <asp:TextBox ID="GroupNameTextBox" ClientIDMode="Static" MaxLength="60" Style="width: 60%"
                        runat="server" autocomplete="off"></asp:TextBox>
                    <div id="CheckBoxDiv" runat="server" clientidmode="Static" class="groupuncheckboximg groupcheckboximg">
                    </div>
                    <span class="CreateditGrpNmSpan CrGrpchkbxspan">Class</span>
                    <asp:CheckBox ID="ClassCheckBox" runat="server" ClientIDMode="Static" Checked="false"
                        Style="display: none" />
                </div>
                <div class="CancelButtonHolder CrGrpAddstubtndiv">
                    <asp:Button ID="AddStudentButton" ClientIDMode="Static" runat="server" CssClass="DbldEndBtn AddButtonBackground"
                        Text=" ADD STUDENTS" OnClick="AddStudentButton_Click" /></div>
                <div id="AddBtndiv" class="CancelButtonHolder CrGrpAddtchbtndiv">
                    <asp:Button ID="AddTeachersButton" CssClass="DbldEndBtn AddButtonBackground" ClientIDMode="Static"
                        runat="server" Text=" ADD TEACHERS" OnClick="AddTeachersButton_Click" /></div>
            </div>
            <div id="CreateGroupContentDiv" runat="server" style="display: none;">
                <div class="TeacherDetailsDiv" clientidmode="Static" id="TeacherDetailsDiv" runat="server">                 
                    <asp:Repeater ID="TeacherDetailsRepeater" runat="server" OnItemDataBound="TeacherDetailsRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div id="TeacherDetailsRepeaterDiv" class="TeacherDtlssRptrContDiv">
                                <div id="TeacherDetailsRepeaterContentDiv" class="TeacherDtlsFirst">
                                    <div class="TeacherDtlsFirstDiv">
                                        <input id="TeacherDetailsCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            style="display: none" />
                                        <asp:Literal ID="teacherid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Literal>
                                    </div>
                                    <div class="TeacherDtlsSecondDiv CrGrpTchNameLbl">
                                        <asp:Label ID="TeacherNameLabel" CssClass="TeacherDtlslbl" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                    </div>
                                    <div class="TeacherDtlsThrdDiv">
                                        <asp:Button ID="TeacherDeleteImgButton" CssClass="DeleteImg" runat="server" OnClick="TeacherDeleteImgButton__Click" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="StudentDetailsDiv" id="StudentDetailsDiv" clientidmode="Static" runat="server">
                   <asp:Repeater ID="StudentDetailsRepeater" runat="server" OnItemDataBound="StudentDetailsRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div id="StudentDetailsRepeaterDiv" class="TeacherDtlssRptrContDiv">
                                <div id="StudentDetailsRepeaterContentDiv" class="TeacherDtlsFirst">
                                    <div class="TeacherDtlsFirstDiv">
                                        <input id="StudentDetailsCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            style="display: none" />
                                        <asp:Literal ID="studentid" runat="server" Text='<%# Eval("UserID") %>' Visible="false"></asp:Literal>
                                    </div>
                                    <div class="TeacherDtlsSecondDiv CrGrpsStuNameLbl">
                                        <asp:Label ID="StudentNameLabel" CssClass="TeacherDtlslbl" runat="server" Text='<%# Eval("StudentNames") %>'></asp:Label>
                                    </div>
                                    <div class="TeacherDtlsThrdDiv">
                                        <asp:Button ID="StudentDeleteImgButton" CssClass="DeleteImg" runat="server" OnClick="StudentDeleteImgButton__Click" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div id="CreateEditGroupFooterDiv" class="Div_FullWidth">                
                <div class="ActiveAddButtonsHolder CreateButtonGradient" clientidmode="Static" id="CreateGroupBtnHolder"
                    runat="server">
                    <asp:Button ID="CreateGroupButton" runat="server" CssClass="BtnStyle CrGrpCreteBtn"
                        Enabled="true" EnableViewState="true" OnClick="CreateGroupButton_Click" />
                </div>               
            </div>
            <div id="dialogmessage" class="eBkcntmgtdigmsgdiv">
                <div class="eBkcntmgthdrbg">
                    <span class="AfterRenewelHeaderSpan eBkcntmgthdrspan">Alert Message!</span>
                </div>
                <div class="eBkcntmgtPPMsgdiv">
                    <div class="eBkcntmgtPPMsginnerdiv">
                        <span id="Span1" class="CrGrpalertmsg">Are you sure you want to delete owner
                            of the group/class?</span>
                    </div>
                    <div class="CrGrpbtnsdiv">
                        <input type="button" id="okButton" value="Yes" class="popupokbtn eBkcntmgtPPokbtn"
                            onclick="okclick()" />
                        <input type="button" id="cancelButton" onclick="cancelclick()"
                            value="No" class="popupokbtn eBkcntmgtPPcnclbtn" />
                    </div>
                </div>
            </div>
            <asp:Button ID="TeacherDelete" runat="server" ClientIDMode="Static" OnClick="ClickTeacherDelete"
                CssClass="HideItems" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<asp:HiddenField ID="ClassCheckHiddenField" runat="server" ClientIDMode="Static" Value="false" />
