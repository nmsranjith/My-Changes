<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeeAllList.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.SeeAllList" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="Msg" %>
<div class="HideItems">
    <Msg:Messages ID="Messages" runat="server">
    </Msg:Messages>
</div>
<style type="text/css">
    .ProfileButton
    {
        text-align: center;
        height: 24px;
        padding-top: 9px;
        text-decoration: none;
        font-weight: normal;
        background: url('./portals/0/images/eye.png') no-repeat 22px center;
    }
    .k-animation-container
    {
        margin-left: 8px !important;
    }
    .k-widget
    {
        border: 0px solid transparent;
    }
    .k-window-action
    {
       /* background-image: url('Portals/0/images/close.png') !important;*/
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    #pager .k-state-disabled:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#F9F9F9), to(#E9E9E9)) !important;
        background: -moz-linear-gradient(to bottom,#F9F9F9, #E9E9E9);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#E9E9E9', gradientType='0') !important;
        background: -ms-linear-gradient(top, #F9F9F9 5%, #E9E9E9 130%) !important;
        color: #CCC !important;
    }
    .Searchbtndiv input[type=submit][disabled]
    {
        opacity: 0.5;
        filter: alpha(opacity=50);
        -moz-opacity: 0.5;
        cursor: default;
    }
    /* .k-loading-mask
    {
        display:none !important;
    } */
</style>
<div id="MessageOuterDiv" runat="server" style="width: 100%; position: static; display: none;">
    <div class="bubble1">
        <asp:Label ID="Message1" runat="server" Text="" />
    </div>
</div>
<div id="StudentListDiv" class="StudentsList" runat="server" style="float: left;
    width: 100%;" clientidmode="Static">
    <asp:UpdatePanel ID="StudentsListPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="StudentList_TopDiv">
                <div class="StudentDashboard_CheckAllDiv">
                    <div id="CheckAllDiv" style="float: left; height: 20px; width: 9%; cursor: pointer;"
                        onclick="javascript:CheckAll();">
                        <asp:Image ID="Image4" ClientMode="Static" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" /></div>
                    <div id="UnCheckAllDiv" style="float: left; height: 20px; width: 9%; cursor: pointer;
                        display: none;" onclick="javascript:UnCheckAll();">
                        <asp:Image ID="Image5" ClientMode="Static" runat="server" Height="21px" Width="20px"
                            ImageUrl="~/Portals/0/images/tick_student.png" /></div>
                    <div onclick="javascript:ClickSelectAll();" style="cursor: pointer; width: 35%; float: left;
                        margin: 5px 0px 0px 12px;">
                        SELECT ALL</div>
                </div>
                <div id="SrtDiv" class="StudentDashboard_SortingDiv">
                    <div style="float: left; margin-top: 4px;">
                        <span onclick="javascript:ClickPMRL();" style="float: left; cursor: pointer;">PM READING
                            LEVEL</span>
                        <asp:Button ID="ReadingLvlAscButton" runat="server" UseSubmitBehavior="false" CssClass="StudentDashboard_ReadingLevelAscSortingButtons"
                            OnClick="ReadingLvlAscButton_Click" ClientIDMode="Static" />
                        <asp:Button ID="ReadingLvlDescButton" runat="server" UseSubmitBehavior="false" CssClass="StudentDashboard_ReadingLevelDescSortingButtons"
                            Visible="false" OnClick="ReadingLvlDescButton_Click" ClientIDMode="Static" />
                        <%-- <input type="button" class="StudentDashboard_ReadingLevelAscSortingButtons" id="ReadingLvlAscButton" />
                <input type="button" class="StudentDashboard_ReadingLevelDescSortingButtons  srtbtnshide"
                    id="ReadingLvlDescButton" />--%>
                    </div>
                    <b style="margin: 4px 11px 0px 11px; float: left;">| </b>
                    <div style="float: left; margin-top: 4px;">
                        <span style="float: left; margin-right: 2px; cursor: pointer;" onclick="javascript:ClickAZ();">
                            A – Z</span>
                        <asp:Button ID="NamesAscButton" runat="server" UseSubmitBehavior="false" CssClass="StudentDashboard_NamesAscSortingButtons"
                            OnClick="NamesAscButton_Click" ClientIDMode="Static" />
                        <asp:Button ID="NamesDescButton" runat="server" UseSubmitBehavior="false" CssClass="StudentDashboard_NamesDescSortingButtons"
                            Visible="false" OnClick="NamesDescButton_Click" ClientIDMode="Static" />
                        <%--
                        <input type="button" class="StudentDashboard_NamesAscSortingButtons" id="NamesAscButton" />
                        <input type="button" class="StudentDashboard_NamesDescSortingButtons  srtbtnshide"
                            id="NamesDescButton" />--%>
                    </div>
                </div>
            </div>
            <div style="width: 87%; float: left; margin-left: 54px; border-left: 1px solid lightgray;
                min-height: 15px;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="ProgressDivClass" style="display: none" id="UpdateProgressImg">
        <div class="ProgressInnerDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                alt="Processing" />
        </div>
    </div>
    <div style="width: 647px; border-style: solid; height: 34px; margin-left: 24px; border: 1px solid #CCCCCC;
        float: left; background-color: #EEEEEE; position: relative; z-index: 50">
        <div class="eClUserSearchDiv ">
            <asp:TextBox ID="SearchTextBox" class="eClSearchbox" runat="server" placeholder="Enter your search here ..."
                ClientIDMode="Static"></asp:TextBox>
            <label id="eClSearchLabel" for="SearchTextBox">
                Enter your search here</label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClick="SearchButton_Click"
                        OnClientClick="ShowUpdate()" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--/************************************/--%>
    <div id="LMAboveClassName" style="display: none; width: 87%; float: left; margin-left: 54px;
        border-left: 1px solid lightgray; min-height: 35px;">
    </div>
    
    <div id="listView" style="display: none;">
    </div>
    <%--  /*************************************/--%>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="float: left; width: 91%; border-left: 1px solid #CCC; margin-left: 54px;
                position: relative;">
                <div id="classlist" class="Div_FullWidth" style="font-size: 12pt; color: #707070;
                    font-weight: bold; margin-top: -10px;">
                    <div style="width: 104%; float: left; min-height: 60px; margin-bottom: -10px; margin-top: 10px;">
                        <span class="HideItems">
                            <%# Eval("GroupId") %></span> <span class="HideItems">
                                <%# Eval("StudentsCount") %></span>
                        <div id='StudentsOuterDiv' class="ShowItems ClassMembersList">
                            <asp:UpdatePanel ID="StudentsPanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Repeater ID="StudentsListRepeater" runat="server">
                                        <ItemTemplate>
                                            <div style="width: 94.8%; float: left; margin-left: 54px; min-height: 80px; margin-top: -16px;">
                                                <div style="float: left; width: 92.51%; margin-top: 30px;">
                                                    <div style="width: 106.9%; float: left; margin-top: 7px; margin-left: -10px;">
                                                        <div class="List_Contents">
                                                            <div style="width: 6.9%; float: left; padding-top: 9px;">
                                                                <input type="checkbox" id="StudentsCheckBoxes" runat="server" clientidmode="Static"
                                                                    style="display: none;" value='<%# Eval("StudentId")%>' />
                                                                <span id="CustSubUserSk" class="srtbtnshide">
                                                                    <%# Eval("CustSubUserSk")%></span> <span id="SubsSKLabel" class="srtbtnshide">
                                                                        <%# Eval("SubscriptionId")%></span>
                                                                <div id='CheckDiv<%# Eval("StudentId")%>' onclick="Checked(<%# Eval("StudentId")%>)"
                                                                    class="checkdiv">
                                                                    <asp:Image ID="CheckAllImage" ClientIDMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/circle_big.png" />
                                                                </div>
                                                                <div id="UnCheckDiv<%# Eval("StudentId")%>" onclick="UnChecked(<%# Eval("StudentId")%>)"
                                                                    class="checkdiv srtbtnshide">
                                                                    <asp:Image ID="Image3" ClientMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/tick_student.png" />
                                                                </div>
                                                            </div>
                                                            <div style="width: 68.1%; float: left; padding-top: 15px; height: 30px; padding-left: 13px;"
                                                                title="<%# Eval("FirstName")%> <%# Eval("LastName")%> (<%# Eval("UserLoginName")%>)">
                                                                <span id="StudentNameLabel" class="StudentDashBoard_StudentNames">
                                                                    <%# Eval("FullName")%></span> <span id="CurrRLLabel" class="srtbtnshide">
                                                                        <%# Eval("CurrentReadingLevel")%></span> <span id="UserNameLabel" class="srtbtnshide">
                                                                            <%# Eval("UserLoginName")%></span>
                                                            </div>
                                                            <div style="width: 22.3%; float: left; padding-top: 7px;">
                                                                <%--<div class="greenBtn">
                                                                    <asp:Button ID="ProfileButton" ClientMode="Static" runat="server" CssClass="ProfileButton"
                                                                        Text="PROFILE" OnClientClick="SetSelected(this)" OnClick="ProfileButton_Click" />
                                                                </div>--%>
                                                                <div class="greenBtn">
                                                                    <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <center>
                            <div class="PagerHdr">
                                <div id='Pager<%# Eval("GroupId") %>' class="StuListPager k-pager-wrap" style="display: none;">
                                </div>
                            </div>
                        </center>
                        <span id='Load<%# Eval("GroupId") %>' class="HideItems">Empty</span>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <center>
        <div class="PagerHdr">
            <div id="pager" class="StuListPager k-pager-wrap">
            </div>
        </div>
    </center>
    <center>
        <div class="PagerHdr">
            <div id="classpager" class="StuListPager k-pager-wrap">
            </div>
        </div>
    </center>
    <center>
        <div class="PagerHdr">
            <div id="searchpager" class="StuListPager k-pager-wrap">
            </div>
        </div>
    </center>
</div>
<div id="Delete-message" title="Confirm Delete" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Confirm Delete</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="MessageLiteral" style="font-family: Raleway-regular, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">Are you sure you want
                to delete student/s?</span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
            <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
        </div>
    </div>
</div>
<asp:HiddenField ID="ClassId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ContainerId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="StudentsCount" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="SelStudid" runat="server" ClientIDMode="Static" />

<script type="text/javascript">
    function ShowUpdate() {
        checkedcount = 0;
        EnableButtons(checkedcount);
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    

    function SetStudentsListHeight() {
        $('#eCollectionContent').height($('#StudentListDiv').height() + 120);
        $('#eCollectionMenu').height($('#StudentListDiv').height() + 111);
    }
    function ClickPMRL() {
        if (!$('#ReadingLvlAscButton').is(':visible')) {
            $('#ReadingLvlDescButton').click();
        }
        else {
            $('#ReadingLvlAscButton').click();
        }

    }

    function ClickAZ() {
        if (!$('#NamesAscButton').is(':visible')) {
            $('#NamesDescButton').click();
        }
        else {
            $('#NamesAscButton').click();
        }

    }
    var currentPage = 0;
    var lastPage = 0;
    var sPgr = 1;

    var dataSource;
    var dataSource1 = null;
    var currentPage = 1;
    var count = 1;
    var Pagerlength = 1;
    var search = 0;
    var dataSource2 = null;


    jQuery(function () {
        
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
    });
    
    function PostBack() {

        if ($('#StudentsOuterDiv').children()[0].childElementCount > 0)
            $('#classlist').attr("style", "display:block");
        else
            $('#classlist').attr("style", "display:none");
        
        $('#SearchTextBox').keypress(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                e.preventDefault();
                var ex = jQuery.Event("keyup"); 
                ex.keyCode = 27; 
                $(document).trigger(ex); 
                $('#SearchButton').click();
            }

        });

        $('#SearchTextBox').keyup(function (e) {
            var code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                e.preventDefault();
                var e = jQuery.Event("keyup"); 
                e.keyCode = 27; 
                $(document).trigger(e); 
                $('#SearchButton').click();
            }

        });
        $('#SearchTextBox').focus();
        $('#SearchTextBox').val($('#SearchTextBox').val());
        if (navigator.platform.indexOf("iPad") == 0) {
            $('#SrtDiv').css('width', '38%');
        }
        if (parseInt(jQuery('#StudentsCount').val()) == 0)
            jQuery('#StudentListDiv').hide();
        else if (parseInt(jQuery('#StudentsCount').val()) <= 10)
            jQuery('#LoadAllStudentDiv').parent().hide();

        SetStudentsListHeight();

        

        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#SearchTextBox').val('Enter your search here ..');
            $("#SearchTextBox").focus(function () {
                if ($(this).val() == 'Enter your search here ..') {
                    $(this).val("");
                }

            });
            $("#SearchTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#SearchTextBox').val('Enter your search here ..');
                }

            });
        }

        $('#CreateReadingSessionButton').click(function () {
            var loginNames = '';
            $('#listView input:checked').each(function () {
                loginNames = loginNames + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().next().next().html();
                jQuery('#UserLoginNameHdn').val(loginNames);
            });
            /******************/
            var ids = 0;
            $('#classlist input:checked').each(function () {
                ids = ids + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().next().next().html();
            });
            ids = ids.split(',');
            var unique = ids.filter(function (itm, i, ids) {
                return i == ids.indexOf(itm);
            });
            ids = unique;
            ids.shift();
            jQuery('#UserLoginNameHdn').val(ids);
        });

        $('#AddStudentToGroupButton').click(function () {
            var loginNames = '';
            $('#listView input:checked').each(function () {
                loginNames = loginNames + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().next().next().html();
                jQuery('#UserLoginNameHdn').val(loginNames);
            });
            /******************/
            var ids = 0;
            $('#classlist input:checked').each(function () {
                ids = ids + ',' + jQuery(this).val() + ',' + jQuery(this).next().html() + ',' + jQuery(this).parent().next().children().next().next().html();
            });
            ids = ids.split(',');
            var unique = ids.filter(function (itm, i, ids) {
                return i == ids.indexOf(itm);
            });
            ids = unique;
            ids.shift();
            jQuery('#UserLoginNameHdn').val(ids);
        });

        $('#PrintStudentCardButton').click(function () {
            var stuids = 0;
            $('#listView input:checked').each(function () {
                stuids = stuids + ',' + jQuery(this).val();
                jQuery('#SelectedStuds').val(stuids);
            });
            /******************/
            var ids = 0;
            $('#classlist input:checked').each(function () {
                ids = ids + ',' + jQuery(this).val();
            });
            ids = ids.split(',');
            var unique = ids.filter(function (itm, i, ids) {
                return i == ids.indexOf(itm);
            });
            ids = unique;
            ids.shift();
            jQuery('#SelectedStuds').val(ids);
        });


        jQuery('#SelectedStuds').val('');

        if ($.browser.mozilla) {
            $('.StudentList_TopDiv').addClass('StudentList_TopDivFX');
        }
        else if ($.browser.msie)
            $('.StudentList_TopDiv').addClass('StudentList_TopDivIE');

    }



    function GotoTop() {
        $('html, body').animate({ scrollTop: 280 }, 'slow');
    }
    var checkedcount = 0;
    function EnableButtons(checkedcount) {
        if (checkedcount >= 1) {
            jQuery('#AddStudentToGroupButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
            jQuery('#AddStudentToGroupButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
            jQuery('#PrintStudentCardButton').removeAttr("disabled").removeClass('DbldPrintBtn').addClass('PrintBtn');
            jQuery('#PrintStudentCardButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
            jQuery('#DeleteStudentButton').removeAttr("disabled").removeClass('DbldDelBtn').addClass('CancelBtn canBtn');
            jQuery('#DeleteStudentButton').parent().removeClass('DisabledDeleteButtonHolder').addClass('ActiveDeleteButtonHolder');
            jQuery('#CreateReadingSessionButton').removeAttr("disabled").removeClass('DbldBtn').addClass('BtnStyle');
            jQuery('#CreateReadingSessionButton').parent().removeClass('DisabledAddButtonHolder').addClass('ActiveAddButtonsHolder');
        }
        else {
            jQuery('#AddStudentToGroupButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
            jQuery('#AddStudentToGroupButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
            jQuery('#PrintStudentCardButton').attr('disabled', 'disabled').removeClass('PrintBtn').addClass('DbldPrintBtn');
            jQuery('#PrintStudentCardButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
            jQuery('#DeleteStudentButton').attr('disabled', 'disabled').removeClass('CancelBtn canBtn').addClass('DbldDelBtn');
            jQuery('#DeleteStudentButton').parent().removeClass('ActiveDeleteButtonHolder').addClass('DisabledDeleteButtonHolder');
            jQuery('#CreateReadingSessionButton').attr('disabled', 'disabled').removeClass('BtnStyle').addClass('DbldBtn');
            jQuery('#CreateReadingSessionButton').parent().removeClass('ActiveAddButtonsHolder').addClass('DisabledAddButtonHolder');
        }
        if (checkedcount == $('#classlist input[type="checkbox"]').length && $('#classlist input[type="checkbox"]').length > 0) {
            jQuery("#CheckAllDiv").hide();
            jQuery("#UnCheckAllDiv").show();
        }
        else {
            jQuery("#CheckAllDiv").show();
            jQuery("#UnCheckAllDiv").hide();
        }
    }
    function Checked(studentid) {
        jQuery('#CheckDiv' + studentid).parent().children("input").attr('checked', 'checked');
        jQuery('#CheckDiv' + studentid).toggle();
        jQuery('#UnCheckDiv' + studentid).toggle();
        jQuery("#CheckDiv" + studentid).parent().parent()[0].className = "rowclick List_Contents";
        checkedcount++;
        EnableButtons(checkedcount);
    }

    function UnChecked(studentid) {
        jQuery("#CheckDiv" + studentid).parent().children("input").removeAttr('checked');
        jQuery("#CheckDiv" + studentid).toggle();
        jQuery("#UnCheckDiv" + studentid).toggle();
        jQuery("#CheckDiv" + studentid).parent().parent()[0].className = "List_Contents";
        if (checkedcount > 0)
            checkedcount--;
        EnableButtons(checkedcount);
    }
    function CheckAll() {
        jQuery('#SelectedStuds').val('');
        jQuery('#CheckAllDiv').hide();
        jQuery('#UnCheckAllDiv').show();
        $('#listView input[type="checkbox"]').attr('checked', 'true');
        jQuery('#listView input[type="checkbox"]').next().next().next().hide();
        jQuery('#listView input[type="checkbox"]').next().next().next().next().show();
        for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
            jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "rowclick List_Contents";
        }

        checkedcount = jQuery('#listView input[type="checkbox"]').length;
        SelectAll();
        EnableButtons(checkedcount);
    }

    /***********  CHECK BOX  **************/
    function SelectAll() {
        
        $('#classlist input[type="checkbox"]').attr('checked', 'true');
        jQuery('#classlist input[type="checkbox"]').next().next().next().hide();
        jQuery('#classlist input[type="checkbox"]').next().next().next().next().show();
        jQuery('#classlist input[type="checkbox"]').parent().parent().addClass('rowclick');
        checkedcount = $('#classlist input[type="checkbox"]').length;
        
    }

    function DeSelectAll() {
        
        $('#classlist input[type="checkbox"]').removeAttr('checked');
        jQuery('#classlist input[type="checkbox"]').next().next().next().next().hide();
        jQuery('#classlist input[type="checkbox"]').next().next().next().show();
        jQuery('#classlist input[type="checkbox"]').parent().parent().removeClass('rowclick');
        
    }
    /************  END - CHECK BOX  *************/


    function UnCheckAll() {
        jQuery('#SelectedStuds').val('');
        jQuery('#CheckAllDiv').show();
        jQuery('#UnCheckAllDiv').hide();
        $('#listView input[type="checkbox"]').removeAttr('checked');
        jQuery('#listView input[type="checkbox"]').next().next().next().show();
        jQuery('#listView input[type="checkbox"]').next().next().next().next().hide();
        for (var i = 0; i < jQuery('#listView input[type="checkbox"]').parent().parent().length; i++) {
            jQuery('#listView input[type="checkbox"]').parent().parent()[i].className = "List_Contents";
        }

        checkedcount = 0;
        EnableButtons(checkedcount);
        DeSelectAll();
    }
    function GetSelectedStudents() {
        jQuery('#SelectedStudents').val(jQuery('#SelectedStuds').val());
    }



    var dkwindow;
    var deleteFlag;
    $("#DeleteStudentButton").click(function () {
        if (!deleteFlag) {
            $("#Delete-message").css({ 'display': 'block' });
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            dkwindow = $("#Delete-message"); 
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
                return false;
            });
            return false;
        }
        var stuids = '';
        $('#listView input:checked').each(function () {
            stuids = stuids + ',' + jQuery(this).val();
            jQuery('#SelectedStuds').val(stuids);
        });
        /******************/
        var ids = 0;
        $('#classlist input:checked').each(function () {
            ids = ids + ',' + jQuery(this).val();
        });
        ids = ids.split(',');
        var unique = ids.filter(function (itm, i, ids) {
            return i == ids.indexOf(itm);
        });
        ids = unique;
        ids.shift();
        jQuery('#SelectedStuds').val(ids);
        return true;
    });
    $("#YesButton").click(function () {
        dkwindow.data("kendoWindow").close();
        deleteFlag = true;
        $("#DeleteStudentButton").click();
        return false;
    });
    $("#NoButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });


    function ConstructPager(start, len) {
        $('html, body').css("cursor", "wait").bind('click', function () {
            return false;
        });
        jQuery('#searchpager').html('');
        jQuery('<span class="SearchFirst" id="FirstPage" onclick="FirstPage()"><<</span><span class="SearchFirst" id="PreviousPage" onclick="PreviousPage()"><</span>').prependTo(jQuery('#searchpager'));
        for (var i = start; i <= len; i++) {
            jQuery('<span class="SearchFirst" id="' + i + '" onclick="GetPage(' + i + ')">' + i + '</span>').appendTo(jQuery('#searchpager'));
        }
        jQuery('<span class="SearchFirst" id="NextPage" onclick="NextPage()">></span><span class="SearchFirst" id="LastPage" onclick="LastPage()">>></span>').appendTo(jQuery('#searchpager'));
        setTimeout("PleseWait()", 1000);
    }
    function SetCurrentPage(pgno) {
        UnCheckAll();
        jQuery('#SelectedStuds').val('');
        currentPage = pgno;
    }

    function GetPage(pgNo) {
        if (pgNo != currentPage) {
            UnCheckAll();
            currentPage = pgNo;
            SearchResults();
        }
    }

    function FirstPage() {
        if (currentPage != 1) {
            currentPage = 1;
            if (lastPage > 10 && currentPage % 10 > 0) {
                ConstructPager(1, 10);
            }
            else
                ConstructPager(1, lastPage);
            SearchResults();
        }
    }
    function LastPage() {
        if (currentPage != lastPage) {
            currentPage = lastPage;
            if (lastPage > 10 && currentPage % 10 > 0) {
                ConstructPager(currentPage - parseInt(currentPage % 10) + 1, currentPage);
            }

            SearchResults();
        }
    }
    function PreviousPage() {
        if (currentPage != 1) {
            if (lastPage > 10 && currentPage % 10 == 1) {
                ConstructPager(currentPage - 10, currentPage - 1);
            }
            currentPage = currentPage - 1;
            SearchResults();
        }
    }
    function NextPage() {
        if (currentPage != lastPage) {
            currentPage = currentPage + 1;
            if (lastPage > 10 && currentPage % 10 == 1) {
                ConstructPager(currentPage, currentPage + Math.min(10, lastPage - currentPage));
            }
            SearchResults();
        }
    }

    function SearchResults() {
        if (search == 1)
            populateListView('paging&pageno=' + currentPage + '&pgitmscnt=' + jQuery('#listView input[type="checkbox"]').length + '&names=' + $('#searcheditems').html());
        else
            populateListView('paging&pageno=' + currentPage + '&pgitmscnt=' + jQuery('#listView input[type="checkbox"]').length);
        jQuery('#LMAboveClassName').show();
        jQuery('#searchpager span').each(function () {
            jQuery(this).removeClass('Highlight');
            if (jQuery(this).attr('id') == currentPage) {
                jQuery(this).addClass('Highlight');
            }
        });
        jQuery('#searchpager span').addClass('SearchFirst').removeClass('pagerDisabled');
        if (currentPage == lastPage) {
            jQuery('#NextPage').removeClass('SearchFirst').addClass('pagerDisabled');
            jQuery('#LastPage').removeClass('SearchFirst').addClass('pagerDisabled');
        }
        if (currentPage == 1) {
            jQuery('#FirstPage').removeClass('SearchFirst').addClass('pagerDisabled');
            jQuery('#PreviousPage').removeClass('SearchFirst').addClass('pagerDisabled');
        }
    }

    
    
    function PleseWait() {
        $('html, body').css("cursor", "auto").unbind('click'); ;
    }

    var sorttype = 'Names';
    var sortdir = 'asc';

</script>
<div id="searcheditems" class="srtbtnshide">
</div>
<div id="selecteditems" class="srtbtnshide">
</div>
