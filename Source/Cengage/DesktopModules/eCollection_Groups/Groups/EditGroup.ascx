<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditGroup.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.EditGroup" %>
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
</style>
<script type="text/javascript">
    jQuery(document).ready(function () {
        function GetFile(path) {
            var pathname = window.location.pathname;
            var temppath = pathname.split('/');
            var root = "http://" + window.location.host + "/" + temppath[1];
            var url = root + path;
            return url;
        }
        $("#<%=SubscriptionDrpList.ClientID %>").kendoDropDownList();
        jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                for (var i = 0; i < jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                    }
                }

            }
            else {
                for (var i = 0; i < jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        jQuery(".TeacherDetailsDiv #TeacherDetailsRepeaterDiv #TeacherDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    }
                }

            }
        });

        jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                for (var i = 0; i < jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                    }
                }

            }
            else {
                for (var i = 0; i < jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        jQuery(".StudentDetailsDiv #StudentDetailsRepeaterDiv #StudentDetailsRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    }
                }
            }
        });


        $('#<%=GroupNameTextBox.ClientID%>').keypress(function () {
            if ($(this).val().length != 0 && $(this).val() != this.title) {
                $('#<%=AddTeachersButton.ClientID%>').removeAttr("disabled");
                $('#<%=AddStudentButton.ClientID%>').removeAttr("disabled");
            }
            else {
                $('#<%=AddTeachersButton.ClientID%>').attr("disabled", "disabled");
                $('#<%=AddStudentButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        $("#<%=GroupNameTextBox.ClientID%>").focus(function () {
            if ($(this).val() == this.title) {
                $(this).val("");
                $(this).removeClass("classSearchwater");
            }
            if ($(this).val().length != 0 && $(this).val() != this.title) {
                $('#<%=AddTeachersButton.ClientID%>').removeAttr("disabled");
                $('#<%=AddStudentButton.ClientID%>').removeAttr("disabled");
            }
            else {
                $('#<%=AddTeachersButton.ClientID%>').attr("disabled", "disabled");
                $('#<%=AddStudentButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        $("#<%=GroupNameTextBox.ClientID%>").blur(function () {
            if ($(this).val().trim() == "") {
                $(this).val(this.title);
                $(this).addClass("classSearchwater");
            }
            if ($(this).val().length != 0 && $(this).val() != this.title) {
                $('#<%=AddTeachersButton.ClientID%>').removeAttr("disabled");
                $('#<%=AddStudentButton.ClientID%>').removeAttr("disabled");
            }
            else {
                $('#<%=AddTeachersButton.ClientID%>').attr("disabled", "disabled");
                $('#<%=AddStudentButton.ClientID%>').attr("disabled", "disabled");
            }
        });
        var checkboxchecked = true;
        if ($("#ClassCheckBox")[0].checked) {
            checkboxchecked = false;
        }
        else {
            $('#CheckBoxDiv').addClass('groupuncheckboximg');
            checkboxchecked = true;
        }

        $('#CheckBoxDiv').click(function () {
            if (checkboxchecked) {
                $('#CheckBoxDiv').addClass("groupuncheckboximg");
                $('#<%=ClassCheckBox.ClientID %>').click();
                checkboxchecked = false;
            }
            else {
                $('#CheckBoxDiv').removeClass('groupuncheckboximg');
                $('#<%=ClassCheckBox.ClientID %>').click();
                checkboxchecked = true;
            }
        });
        $("#<%=CreateGroupButton.ClientID %>").click(function () {
            if ($("#SubscriptionDrpList").val() == 0) {
                $("#MessagesDiv").addClass("dnnFormMessage dnnFormWarning").text('subscription is mandatory for group. Please specify the details and  then click Create group button').show();
                return false;
            }
            else if ($("#<%=GroupNameTextBox.ClientID%>").val().trim() == "") {
                $("#MessagesDiv").addClass("dnnFormMessage dnnFormWarning").text('Group name is mandatory for group. Please specify the details and  then click Create a group button').show();
                return false;
            }
            else if (!$("#<%=CreateGroupContentDiv.ClientID %>").is(":visible")) {
                $("#MessagesDiv").addClass("dnnFormMessage dnnFormWarning").text('Either a teacher or student is mandatory for group. Please specify the details and  then click Create a group button').show();
                return false;
            }
            else {
                return true;
            }
        });

    });

</script>
<div class="CreateEditGroupDivSet">
    <div class="FinishCreateProfile CancelButtonGradient" style="margin-top: -76px;">
        <asp:Button ID="Backtocreategroupbtn" runat="server" ClientIDMode="Static" Text="CANCEL MAKING GROUP"
            CssClass="CancelBtn CancelButtonBackground" OnClick="Backtocreategroupbtn_Click" /></div>
    <asp:UpdatePanel ID="MessageUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="MessagesDiv">
                <uc1:Messages ID="Messages" runat="server" />
            </div>
            <div class="CreateGroupDiv">
                <span class="H4">Group name:</span><br />
                <br />
                <div style="float: left; border: 0px solid white; width: 113px;">
                    <asp:DropDownList ID="SubscriptionDrpList" runat="server" ClientIDMode="Static" DataTextField="Text"
                        DataValueField="Id" AppendDataBoundItems="true" class="SubscriptionDrpList">
                    </asp:DropDownList>
                </div>
                <div style="width: 35.1%; margin-left: 10px;">
                    <input type="text" id="GroupNameTextBox" style="width: 60%" runat="server" enableviewstate="true" />
                    <div id="CheckBoxDiv" class="groupcheckboximg">
                    </div>
                    <span style="margin-right: 19px;" class="CreateditGrpNmSpan">Class</span>
                    <input id="ClassCheckBox" runat="server" clientidmode="Static" type="checkbox" style="display: none" />
                </div>
                <div id="AddBtndiv" class="box AddButtonGradient" style="margin-right: -4px;">
                    <asp:Button ID="AddTeachersButton" CssClass="AddButtonBackground" runat="server"
                        Text=" ADD TEACHERS" OnClick="AddTeachersButton_Click" /></div>
                <div class="box AddButtonGradient" style="margin-right: 4px;">
                    <asp:Button ID="AddStudentButton" CssClass="AddButtonBackground" runat="server" Text=" ADD STUDENTS"
                        OnClick="AddStudentButton_Click" /></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="CreateGroupContentDiv" runat="server" style="display: block;">
        <div class="TeacherDetailsDiv" id="TeacherDetailsDiv" runat="server">
            <asp:UpdatePanel ID="TeacherDetailsUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="TeacherDetailsRepeater" runat="server">
                        <ItemTemplate>
                            <div id="TeacherDetailsRepeaterDiv" class="TeacherDtlssRptrContDiv">
                                <div id="TeacherDetailsRepeaterContentDiv" class="TeacherDtlsFirst">
                                    <div class="TeacherDtlsFirstDiv">
                                        <input id="TeacherDetailsCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            style="display: none" />
                                        <asp:Literal ID="teacherid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Literal>
                                        <img id="TeacherDetailsCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                    </div>
                                    <div class="TeacherDtlsSecondDiv">
                                        <asp:Label ID="Label1" CssClass="TeacherDtlslbl" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                    </div>
                                    <div class="TeacherDtlsThrdDiv">
                                        <asp:Button ID="TeacherDeleteImgButton" CssClass="DeleteImg" runat="server" OnClick="TeacherDeleteImgButton__Click" />
                                        <%--<input type="image" id="TeacherDeleteImgButton" src="<%=Page.ResolveUrl("Portals/0/images/close.jpg")%>" />--%>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="StudentDetailsDiv" id="StudentDetailsDiv" runat="server">
            <asp:UpdatePanel ID="StudentDetailsUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="StudentDetailsRepeater" runat="server">
                        <ItemTemplate>
                            <div id="StudentDetailsRepeaterDiv" class="TeacherDtlssRptrContDiv">
                                <div id="StudentDetailsRepeaterContentDiv" class="TeacherDtlsFirst">
                                    <div class="TeacherDtlsFirstDiv">
                                        <input id="StudentDetailsCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            style="display: none" />
                                        <asp:Literal ID="studentid" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Literal>
                                        <img id="StudentDetailsCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                    </div>
                                    <div class="TeacherDtlsSecondDiv">
                                        <asp:Label ID="StudentNameLabel" CssClass="TeacherDtlslbl" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                                    </div>
                                    <div class="TeacherDtlsThrdDiv">
                                        <asp:Button ID="StudentDeleteImgButton" CssClass="DeleteImg" runat="server" OnClick="StudentDeleteImgButton__Click" />
                                        <%--<input type="submit" id="StudentDeleteImgButton"  runat="server" onclick="StudentDeleteImgButton__Click" />--%>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="CreateEditGroupFooterDiv">
        <div class="FinishCreateProfile CancelButtonGradient">
            <asp:Button ID="CancelCreateGroup" runat="server" Text="CANCEL MAKING GROUP" CssClass="CancelBtn CancelButtonBackground"
                OnClick="CancelCreateGroup_Click"></asp:Button>
        </div>
        <div class="ActiveAddButtonsHolder CreateButtonGradient">
            <asp:Button ID="CreateGroupButton" runat="server" CssClass="BtnStyle" Style="height: 35px"
                Text="SAVE GROUP" OnClick="CreateGroupButton_Click" />
        </div>
    </div>
</div>
