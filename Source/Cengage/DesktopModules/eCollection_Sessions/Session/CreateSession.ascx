<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateSession.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.CreateSession" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Students/CSS/jQuery.ui.smoothness.css")%>"
    rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    var kwindow;
    function removeFormField(id) {
        $(id).remove();
        return false;
    }
    $(document).ready(function () {
	$('.CalendarDiv img').attr('alt','');
	$('CalendarDiv img').attr('title','date');
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-245px');
        }

        jQuery('#DateofBirthTextBox').val(jQuery('#DOBHdFld').val());
        jQuery('#DateofBirthTextBox').change(
        function () {
            jQuery('#DOBHdFld').val(jQuery('#DateofBirthTextBox').val());
        });


        $('#guided').click(function () {
            if ($(this).hasClass('guided')) {
                $(this).addClass('independent').removeClass('guided');
            }
            else {
                $(this).addClass('guided').removeClass('independent');
            }
        });
        //        $("#SessionTabHolder").css("position", "relative");
        //        $("#SessionTabHolder").css("z-index", "100000");
        //        document.getElementById('overlay').style.visibility = 'visible';
        jQuery('#HeaderBtn').addClass('FinishCreateProfile');
        jQuery('#bg').addClass('CancelBtn');
        jQuery('#PageHeaderButton').css({ 'padding-top': '2px', 'float': 'left', 'color': 'white', 'text-decoration': 'none' });


        jQuery("#HistoryButton").click(function () {
            jQuery('#Switch').removeClass('independant_on');
            $('#<%= lblSessiontype.ClientID %>').val("1");

            return false;
        });
        jQuery("#StudentsButton").click(function () {
            jQuery('#Switch').addClass('independant_on');
            $('#<%= lblSessiontype.ClientID %>').val("2");

            return false;
        });
        jQuery("#DrpSubscription").kendoDropDownList({ animation: false });;

        $('#' + '<%= txtSessionName.ClientID %>').change(function () {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            if ($(this).val().length > 0) {
                $('#SessionNameHdn').val($('#' + '<%= txtSessionName.ClientID %>').val());
            }
        });
        //        if (jQuery('#' + '<%= txtSessionName.ClientID %>').val().length == 0 || jQuery('#' + '<%= txtSessionName.ClientID %>').val() == '  Enter session name')
        //            jQuery('#' + '<%= txtSessionName.ClientID %>').val('  Enter session name').css('color', 'lightgray');
        //        jQuery('#' + '<%= txtSessionName.ClientID %>').click(function () {
        //            jQuery('#' + '<%= txtSessionName.ClientID %>').val("");
        //        });

        //        jQuery('#' + '<%= txtSessionName.ClientID %>').change(function () {
        //            if (jQuery('#' + '<%= txtSessionName.ClientID %>').val().length == 0 || jQuery('#' + '<%= txtSessionName.ClientID %>').val() == '  Enter session name')
        //                jQuery('#' + '<%= txtSessionName.ClientID %>').val('  Enter session name').css('color', 'lightgray');

        //        });

        $("#CancelButton").click(function () {

            $("#DrpSubscription option:contains(" + document.getElementById("RisePopUp").value.trim() + ")").attr('selected', 'selected');
            $("#DrpSubscriptiondiv .k-input")[0].innerHTML = document.getElementById("RisePopUp").value.trim();
            //            for (var t = 0; t < $("#DrpSubscription-list ul li").length; t++) {
            //                if ($("#DrpSubscription-list ul li")[t].innerHTML == document.getElementById("RisePopUp").value.trim()) {
            //                    //alert('Test');
            //                    //$("#DrpSubscription-list ul li")[t].click();
            //                }
            //            }
            kwindow.data("kendoWindow").close(); return false;
        });
        $("#PopuOkButton").click(function () {
            kwindow.data("kendoWindow").close();
            document.getElementById("RisePopUp").value = '';
            $("#DrpSubscription").change();
        });
    });
    function onCrossClose() {
        $("#DrpSubscription option:contains(" + document.getElementById("RisePopUp").value.trim() + ")").attr('selected', 'selected');
        $("#DrpSubscriptiondiv .k-input")[0].innerHTML = document.getElementById("RisePopUp").value.trim();
    }
    function Remove(obj) {

        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTextBox").value = "";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);

    }

    function RemoveGroup(obj) {

        var SelectedValues = $("#SelectedValueGroupTextBox").val().trim().split(',');
        document.getElementById("SelectedValueGroupTextBox").value = "";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueGroupTextBox").val($("#SelectedValueGroupTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);

    }

    function RemoveProduct(obj) {
        var SelectedValues = $("#SelectedValueProductTextBox").val().trim().split(',');
        document.getElementById("SelectedValueProductTextBox").value = "";
        //alert(obj.parentNode.parentNode.children[0].children[1].innerHTML);
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.parentNode.children[0].children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueProductTextBox").val($("#SelectedValueProductTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        var bookid = obj.parentNode.parentNode.children[0].children[1].innerHTML;
        obj.parentNode.parentNode.parentNode.parentNode.removeChild(obj.parentNode.parentNode.parentNode);
        return false;
    }



    function onchanged() {
        if (document.getElementById("RisePopUp").value != $("#DrpSubscription option:selected").text() && document.getElementById("RisePopUp").value != '') {
            if ($('#SelectedStudentList')[0].children.length != 0) {
                $("#MessageLiteral")[0].innerHTML = 'The Current Session have members based on subscription ' + document.getElementById("RisePopUp").value + '. If you select some other subscription, the selected members in the subscription ' + document.getElementById("RisePopUp").value + ' will be removed.';
                $("#dialog-message").css({ 'display': 'block' });
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                kwindow = $("#dialog-message"); //Give ur div id here
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
                    $('a.k-window-action.k-link').parent().css("background-image", "url('../Portals/0/images/close.png') !important");
                    return false;
                });
                return true;
            }
            else {
                return true;
            }
        }
        else {
            if ($('#SelectedStudentList')[0].children.length > 0) {
                return false;
            }
            else {
                return true;
            }
        }
    }
    function GetSessionEndDate() {
        if ($('#DateofBirthTextBox').val() != "") {

            var date = $('#DateofBirthTextBox').val();
            $("#DOBHdFld").val(date.replace(/\-/g,'/'));
        }
    }
   

</script>
<style type="text/css">
.ui-datepicker-trigger
{
	float: right;
	width: 8%;
}
.ui-datepicker
{
background-color: inherit;
}

.ui-widget-header .ui-icon 
{
	background-image: url('/portals/0/images/sprite.png');	
	width: 10px;
}
.ui-icon-circle-triangle-e
{
	background-position: 0px -15px;
}
.ui-icon-circle-triangle-w
{
	background-position: -2px -47px;
}
    h5
    {
        font-size: 10.5pt !important;
    }
    /**popup****/
    .k-window-action
    {
        background-image: url('../Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    
    #SessionDropDownDiv .k-dropdown-wrap
    {
        background-color: white !important;
        background-image: url('../Portals/0/images/Levelbg.png') !important;
        background-repeat: repeat !important;
    }
    #SessionDropDownDiv .k-dropdown-wrap .k-input
    {
        height: 1.3em !important;
        margin-top: 2px !important;
        text-indent: 0px !important;
        margin-left: 10px !important;
    }
    #SessionDropDownDiv .k-dropdown-wrap .k-select
    {
        margin-top: 2px !important;
    }
    #SessionDropDownDiv .k-dropdown-wrap .k-state-hover
    {
        background-color: white !important;
    }
    
    #SessionDropDownDiv .k-select
    {
        margin-top: 4px;
    }
    
    #SessionDropDownDiv .k-i-arrow-s
    {
        background-position: -129px -17px !important;
    }
    #SessionDropDownDiv .k-numerictextbox .k-i-arrow-n
    {
        background-position: 0px -17px !important;
    }
    #SessionDropDownDiv .k-numerictextbox .k-i-arrow-s
    {
        background-position: -65px -18px !important;
    }
    
    .switch
    {
        border: 0px solid lightGrey;
        float: right;
        margin-right: 249px;
        border-radius: 0px;
        margin-top: 7px;
        width: 244px;
        height: 48px;
        background-image: url('Portals/0/images/session_switch.png'); /* Other styles */
    }
    
    .guided
    {
        background-image: url("../Portals/0/images/guided_on.png");
    }
    
    .independent
    {
        background-image: url("../Portals/0/images/independant_on.png");
    }
    
    
    .SelectedGroupItem
    {
        background-color: #67A024;
    }
    
    .SelectedStudentItem
    {
        background-color: #BB078A;
    }
    
    .SelectedTeacherItem
    {
        background-color: #21B4E6;
    }
    
    
    .SelectedGroupItem
    {
        float: left;
        width: 20%;
        background-image: none;
        border-radius: 2px;
        font-weight: bold;
        height: 33px;
        margin: 0px 5px 5px 0px;
        list-style-type: none;
    }
    
    .SelectedStudentItem
    {
        float: left;
        width: 20%;
        background-image: none;
        border-radius: 2px;
        font-weight: bold;
        height: 33px;
        margin: 0px 5px 5px 0px;
        list-style-type: none;
    }
    .SelectedStudentItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 8px;
        margin-left: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    .SelectedStudentItem a
    {
        float: right;
        margin-top: 9px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
        font-size: 10pt;
    }
    
    
    .SelectedTeacherItem
    {
        float: left;
        width: 20%;
        background-image: none;
        border-radius: 2px;
        font-weight: bold;
        height: 33px;
        margin: 0px 5px 5px 0px;
        list-style-type: none;
    }
    
    .SelectedTeacherItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 8px;
        margin-left: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    
    .SelectedTeacherItem a
    {
        float: right;
        margin-top: 9px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
        font-size: 10pt;
    }
    
    .SelectedGroupItem span
    {
        float: left;
        display: inline;
        margin: 0px;
        margin-top: 8px;
        margin-left: 20px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        font-size: 9.4pt;
        font-weight: bold;
    }
    
    .SelectedGroupItem a
    {
        float: right;
        margin-top: 9px;
        margin-right: 11px;
        color: white;
        font-family: Raleway-regular,Raleway, Arial, sans-serif;
        text-decoration: none;
        cursor: pointer;
        font-size: 10pt;
    }
    
    .SelectedGroupItem a:hover
    {
        color: Red;
    }
    
    .SelectedStudentItem a:hover
    {
        color: Red;
    }
    .SelectedTeacherItem a:hover
    {
        color: Red;
    }
    
    .GroupAddtoinnersnddiv
    {
        float: left;
        width: 99%;
        height: auto;
        margin: 10px 0px 10px 10px;
        border: 0px solid lightGray;
    }
    
    #SelectedStudentList
    {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }
    #SelectedStudentList li
    {
        list-style-type: none;
        display: inline;
    }
    #SelectedStudentList ul li
    {
        list-style-type: none;
        display: inline;
    }
    #CancelSessionButton
    {
        text-shadow: none;
    }
</style>
<uc1:Messages ID="Messages" runat="server" />
<div id="dialog-message" title="Alert message!" style="display: none; background: white !important;">
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
            <span id="MessageLiteral" style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;"></span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="PopuOkButton" style="margin-left: 192px;" value="Continue"
                class="popupokbtn" />
            <input type="button" id="CancelButton" style="margin-left: 18px;" value="Cancel"
                class="popupokbtn" />
        </div>
    </div>
</div>
<div style="width: 96%; margin-top: 19px; margin-left: 2%">
    <asp:HiddenField ID="RisePopUp" runat="server" ClientIDMode="Static" />
    <%-- <div style="width: 100%; margin-top: 15px; float: left; height: auto; border: 1px solid lightgrey;">
        <div style="float: left; height: 25px; margin: 25px 0px 10px 10px;">
            
            <h5>
                SUBSCRIPTION:</h5>
        </div>
        <div id="DrpSubscriptiondiv" style="float: left; margin: 22px 5px 0px 5px;">
            <asp:DropDownList ID="DrpSubscription" ClientIDMode="Static" runat="server" Style="width: 100px;
                height: 27px;" AutoPostBack="True" OnSelectedIndexChanged="itemSelected" TabIndex="0">
            </asp:DropDownList>
            
        </div>
    </div>--%>
    <div style="width: 100%; margin-top: 15px; float: left; height: auto; border: 1px solid lightgrey;">
        <div style="float: left; width: 30px; height: 0px; margin: 25px 0px 10px 10px;">
            <%-- <span class="Session">TO:</span>--%>
            <h5>
                TO:</h5>
        </div>
        <div style="float: left; margin: 13px 0px 0px 5px; height: 36px;">
            <asp:Button ID="AddStudentButton" Enabled="true" runat="server" Text="ADD STUDENTS"
                OnClick="AddStudentButton_Click" CssClass="SessionAddStudentButton" />
        </div>
        <div style="color: #392C29; float: left; margin: 13px 0px 0px 5px; height: 36px;">
            <asp:Button ID="AddGroupsButton" Enabled="true" runat="server" Text="ADD GROUPS"
                OnClick="AddGroupsButton_Click" CssClass="SessionAddStudentButton" />
        </div>
        <div style="color: #392C29; float: left; margin: 13px 0px 0px 5px; height: 36px;">
            <asp:Button ID="AddTeachersButton" Enabled="true" runat="server" Text="ADD TEACHERS"
                OnClick="AddTeachersButton_Click" CssClass="SessionAddStudentButton" />
        </div>
        <div class="GroupAddtoinnersnddiv">
            <div style="float: left; width: 97%; height: 0.1px; margin: 0px 0px 5px 5px; border: 1px solid lightgrey;
                border-color: #D4D4D4">
            </div>
            <ul id="SelectedStudentList" runat="server" clientidmode="Static">
            </ul>
            <div id="dialog" title="Alert Message" style="display: none;">
                <p>
                    <span id="StudentNamespan"></span>is Already added</p>
            </div>
            <asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueGroupTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueProductTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
        </div>
    </div>
    <div style="width: 100%; margin-top: 15px; float: left; height: auto; border: 1px solid lightgrey;">
        <div style="float: left; width: 159px; height: 25px; margin: 25px 0px 10px 10px;">
            <%--<span class="Session">BOOKS SELECTED:</span>--%>
            <h5>
                BOOKS SELECTED:</h5>
        </div>
        <div style="float: left; width: 130px; margin: 13px 0px 0px 5px; height: 36px;">
            <asp:Button ID="Button1" Enabled="true" runat="server" Text="ATTACH BOOKS" OnClick="AttachBooks_Click1"
                CssClass="SessionAddStudentButton" />
        </div>
        <div style="float: left; width: 96%; height: 0.1px; margin: 0px 0px 0px 15px; border: 1px solid lightgrey;
            border-color: #D4D4D4">
        </div>
        <div id="BooksAddedDiv" class="BooksAddedDiv" style="border: 0px !important; margin-top: 0px !important">
            <asp:ListView ID="BookAddedRepeater" runat="server" GroupItemCount="6" ItemPlaceholderID="itemsGoHere" OnItemDataBound="BookAddedRepeater_ItemDataBound" 
                GroupPlaceholderID="groupsGoHere">
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="groupsGoHere"></asp:PlaceHolder>
                </LayoutTemplate>
                <GroupTemplate>
                    <asp:PlaceHolder runat="server" ID="itemsGoHere"></asp:PlaceHolder>
                </GroupTemplate>
                <ItemTemplate>
                    <div id="BookContentDiv" style="margin-left: -22px">
                        <div class="DashBoard_Items_books_Holder" style="width: 25%;">
                            <div>
                                <div class="DashBoard_Items_books">
                                    <%--<asp:Image ID="Book1" CssClass="DashBoard_Items_books_images" runat="server" ImageUrl='<%#Page.ResolveUrl(string.Format("Portals/0/images/{0}",Eval("ImageFileName"))) %>' />--%>
                                    <asp:Image  ID="AddedBooks" runat="server" ClientIDMode="Static" CssClass="DashBoard_Items_books_images" ImageUrl='<%# Eval("ImageFileName") %>' />
                                    <asp:Label ID="lblCUSTSUBSITEM_SK" runat="server" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'
                                        Style="display: none;"></asp:Label>
                                </div>
                                <div style="margin-left: 129px;">
                                    <img id="RemoveBooks" onclick='javascript:RemoveProduct(this);' src="<%=Page.ResolveUrl("Portals/0/images/close.png") %>"
                                        style="border-width: 0px; width: 20px;" />
                                </div>
                            </div>
                            <%--    <div class="DashBoard_Items_books" style="text-align: right; margin-top: 23px;">
                                <a href="/dotnetnuke/cengage/eCollection/books.aspx" style="color: Black;">See all ></a>
                            </div>--%>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div style="float: left; width: 100%; height: 60px; margin-top: 15px; border: 1px solid lightGrey;
        background-color: #F4F0F1; background: -moz-linear-gradient(center top , white 0%, white 55%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#E7E7E7));
        background: -ms-linear-gradient(top, #FDFDFD 5%, #E7E7E7 130%) !important;" class="SessionHeadClass">
        <div style="color: #6B5D63; float: left; width: 145px; margin: 25px 0px 10px 10px;">
            <%--<span class="Session">SESSION TYPE:</span>--%>
            <h5>
                SESSION TYPE:</h5>
        </div>
        <div id="Switch" class="CreateSwitch" runat="server" clientidmode="Static">
            <asp:HiddenField ID="lblSessiontype" runat="server" Value="1" />
            <div id="HistoryButton" style="width: 92px; height: 30px; border: 0px solid white;
                cursor: pointer; background-color: transparent; float: left;">
            </div>
            <div id="StudentsButton" style="border: 0px solid white; width: 144px; height: 30px;
                cursor: pointer; background-color: transparent; float: left;">
            </div>
        </div>
    </div>
    <div style="width: 100%; margin-top: 15px; float: left; height: auto; border: 1px solid lightgrey;">
        <div style="float: left; width: 120px; margin: 0px 0px 10px 10px; padding-top: 4px;
            padding-left: 3px; position: absolute;">
            <%--<span style="float: left; position: absolute; padding-top: 4px; padding-left: 3px;
                margin: 0px 0px 10px 10px;" class="Session"><h5>NOTES:</h5></span>--%>
            <h5>
                NOTES:</h5>
        </div>
        <div style="float: left; width: 96%; height: 0.1px; margin: 25px 0px 0px 15px; border: 1px solid lightgrey;
            border-color: #D4D4D4">
        </div>
        <div style="float: left; width: 96%; height: 108px; margin: 15px 0px 0px 15px; border: 0px solid lightGrey;">
            <textarea class="Notes" runat="server" id="txtNotes" cols="20" name="S1" rows="2"
                style="overflow: hidden;" maxlength='2000'></textarea>
        </div>
    </div>
    <div style="height: 50px; float: left; width: 100%; border-bottom: 1px solid black;
        border: 1px solid lightGrey; background-color: #F4F0F1; margin-top: 15px; background-image: url('../Portals/0/images/Tbrowimg.png');
        background-color: #F4F0F1; background: -moz-linear-gradient(center top , white 0%, white 55%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#E7E7E7));
        background: -ms-linear-gradient(top, #FDFDFD 5%, #E7E7E7 130%) !important;" class="SessionHeadClass">
        <div style="color: #6B5D63; float: left; width: 127px; margin: 18px 0px 10px 10px;">
            <%--<span class="Session" style="width: 135px;">SESSION NAME:</span>--%>
            <h5>
                SESSION NAME:</h5>
        </div>
        <div class="EditSessionProfile_Div" style="margin-left: 21px !important; width: 73% !important">
            <div style="float: left; width: 95%;">
                <asp:TextBox ID="txtSessionName" runat="server" CssClass="EditSessionProfile_TextBox"
                    MaxLength="60">
                </asp:TextBox></div>
            <div class="EditSessionProfile_Span">
                <span>*</span></div>
        </div>
    </div>
    <div style="margin-top: 15px; float: left; width: 100%; height: 45px; border-bottom: 1px solid black;
        border: 1px solid lightGrey; background-color: #F4F0F1; background-image: url('../Portals/0/images/Tbrowimg.png');
        background-color: #F4F0F1; background: -moz-linear-gradient(center top , white 0%, white 55%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#E7E7E7));
        background: -ms-linear-gradient(top, #FDFDFD 5%, #E7E7E7 130%) !important;" class="SessionHeadClass">
        <div style="float: left; width: 187px; margin-top: 12px; margin-left: 1px; margin-right: 0px;
            margin-bottom: 10px; margin: 15px 0px 10px 10px;">
            <%--<span id="ReadingLevelLabel" style="color: #6B5D63;" class="Session">THIS SESSION ENDS:</span>--%>
            <h5>
                THIS SESSION ENDS:</h5>
        </div>
        <div class="CalendarDiv" id="SessionDropDownDiv" style="float: left; width: 257px !important;
            margin: 10px 0px 10px -8px;">
            <%-- <select id="SessionDropDownList" runat="server" style="height: 27px; float: left;
                margin-left: 5px; position: absolute; width: 470px;">
                <option value="Next Week">Next Week</option>
                <option value="Today">Today</option>
            </select>--%>
            <%--  <asp:DropDownList ID="SessionDropDownList" ClientIDMode="Static" runat="server" Style="height: 27px;
                float: left; margin-left: 5px; position: absolute; width: 470px;">
                <asp:ListItem Value="Next Week">Next Week</asp:ListItem>
                <asp:ListItem Value="Today">Today</asp:ListItem>
            </asp:DropDownList>--%>
            <asp:HiddenField ID="DOBHdFld" runat="server" ClientIDMode="Static" />
			<input type="text" id="DateofBirthTextBox" name="IpadDate" style="font-style: italic; width: 88%; height: 21px;"  />
            <%-- <input type="text" id="DateofBirthTextBox" readonly="readonly" placeholder="Session End Date"
                            autocomplete="off" runat="server" clientidmode="Static" class="" style="font-style: italic;
                            width: 91%; height: 21px;" />--%>
        </div>
    </div>
    <div style="float: left; width: 100%; height: 0.1px; margin: 1px 0px 0px 1px; border: 1px solid lightgrey;
        border-color: #D4D4D4; margin-top: 15px">
    </div>
    <div class="Div_FullWidth">
        <div style="width: 34%; float: left; margin-top: 21px; margin-left: 14px;" class="Session">
            <%-- <h5>FINISHED CREATING ?</h5>--%>
        </div>
        <div style="width: 60%; float: right; height: 50px;">
            <div style="width: 35%; float: left; margin-left: 96px; margin-right: -17px;">
                <div class="CancelSessionBtnHolder">
                    <asp:Button ID="CancelSessionButton" CssClass="CancelBtn" Text="CANCEL" ClientIDMode="Static"
                        OnClick="CancelCreateSession_Click" runat="server" />
                </div>
            </div>
            <div style="width: 50%; float: right; margin-right: -20px;">
                <div class="ActiveAddButtonsHolder">
                    <asp:Button ID="CreateSessionButton" CssClass="AddButton" Text="CREATE SESSION" OnClick="CreateSessionButton_Click"  OnClientClick="javascript:GetSessionEndDate();" 
                        ClientIDMode="Static" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="SessionNameHdn" runat="server" ClientIDMode="Static" />
