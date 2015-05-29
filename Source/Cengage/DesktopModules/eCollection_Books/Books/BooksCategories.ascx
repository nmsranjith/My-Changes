<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BooksCategories.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Books.Books.BooksCategories" %>
<script type="text/javascript">
    var dkwindow, dkwindow1;
    var deleteFlag, deleteFlag1;
    jQuery(document).ready(function () {
        $('#AddtosubsButtonCategory').attr('disabled', true);
        $('#BottomAddToSubscriptionCategory').attr('disabled', true);
        jQuery("#<%=CategoriesDrpList.ClientID %>").kendoDropDownList();
        $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent img").click(function () {
            var LevelBookcount = 0, lev = '0';
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                    if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                        $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent img")[i].src = GetFile("/Portals/0/images/check.png");
                    }
                }
                var isGracePeriod = jQuery('#lblGracePeriod').text();
                // if (isGracePeriod == 'False') {
                jQuery("#AddtosubsButtonCategory").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                $('[id$=AddtosubsButtonCategory]').attr('disabled', false);
                jQuery("#BottomAddToSubscriptionCategory").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                $('[id$=BottomAddToSubscriptionCategory]').attr('disabled', false);
                //  }
                for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                    if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                        // var bookCount = $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent #CategoriesCollectionCount")[i].innerHTML;
                        // LevelBookcount += parseInt(bookCount)   
                        var a = $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent #CategoriesTitleCollection")[i].innerHTML;
                        
                        lev = lev + ',' + a;   //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount));                
                        
                    }
                }

                //$('#SelectedBookscountCategories').text(LevelBookcount);
                // $('#SelectedBookscountBotCategories').text(LevelBookcount);
            }
            else {
                var count = 0;
                for (var j = 0; j < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; j++) {
                    if (!$("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[j].checked) {
                        $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent img")[j].src = GetFile("/Portals/0/images/unchecked.png");
                    }
                }
                for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                    if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                        $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent img")[i].src = GetFile("/Portals/0/images/check.png");
                        count++;
                    }
                }
                if (count == 0) {
                    jQuery("#AddtosubsButtonCategory").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                    $('[id$=AddtosubsButtonCategory]').attr('disabled', true);
                    jQuery("#BottomAddToSubscriptionCategory").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                    $('[id$=BottomAddToSubscriptionCategory]').attr('disabled', true);
                }

                for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                    if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                        // var bookCount = $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent #CategoriesCollectionCount")[i].innerHTML;
                        //  LevelBookcount += parseInt(bookCount)
                        var a = $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent #CategoriesTitleCollection")[i].innerHTML;
                        lev = lev + ',' + a;   //$("#SelectedBookscount").val(parseInt($("#SelectedBookscount").val()) + parseInt(bookCount)); 
                    }
                }
                // $('#SelectedBookscountCategories').text(LevelBookcount);
                //  $('#SelectedBookscountBotCategories').text(LevelBookcount);
            }

            if (lev != "0") {                
                var categoryType = $('#CategoriesDrpList')[0].value;
				if (categoryType == 'PRIMARY SUBJECT') {
                    categoryType = '1';                     
                }
                $.ajax({
                    url: GetFile('/DesktopModules/eCollection_Books/BooksHandler.ashx?BooksStatus=bookscount&values=' + lev + '&type=' + categoryType),
                    dataType: "json",
                    success: function (value) {
                        value = value.split(',');
                        $('#SelectedBookscountCategories').text(value[0]);
                        $('#SelectedBookscountBotCategories').text(value[0]);
                        $('#AlreadySelectedBookCnt').text(value[1]);
                    }
                });
            }
            else {
                $('#SelectedBookscountCategories').text('0');
                $('#SelectedBookscountBotCategories').text('0');
                $('#AlreadySelectedBookCnt').text('0');
            }
        });


        $("#AddtosubsButtonCategory,#BottomAddToSubscriptionCategory").click(function () {
            $('#SelectedBookCountCategory').val('0');
            for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                    var bookCount = $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent #CategoriesCollectionCount")[i].innerHTML;
                    $("#SelectedBookCountCategory").val(parseInt($("#SelectedBookCountCategory").val()) + parseInt(bookCount));
                }
            }
            $('#lblIsBookAlreadyAdded').val('0');
            for (var i = 0; i < $("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]").length; i++) {
                if ($("#CategoriesContentDiv #CategoriesRepeaterdiv #CategoriesRepeaterContent input[type=checkbox]")[i].checked) {
                    var bookCount = $("#CategoriesContentDiv #hiddenCategoriesContentdiv #hiddenCategoriesRepeaterdiv #lblProductSk")[i].innerHTML;
                    $("#lblIsBookAlreadyAdded").val(parseInt($("#lblIsBookAlreadyAdded").val()) + parseInt(bookCount));
                }
            }

            var BooksAlreadyAdded = parseInt(jQuery('#AlreadySelectedBookCnt').text());
            var booksSelected = parseInt(jQuery('#SelectedBookscountCategories').text());
            if (BooksAlreadyAdded == booksSelected) {
                if (!deleteFlag1) {
                    $("#BookAlreadyAdded").css({ 'display': 'block' });
                    $($('#BookAlreadyAdded')[0].children[0].children[0])[0].innerHTML = "Alert Message!";
                    $($('#BookAlreadyAdded')[0].children[1].children[0].children[0])[0].innerHTML = "The Selected Books are already added !!!";
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    dkwindow1 = $("#BookAlreadyAdded"); //Give ur div id here
                    if (!dkwindow1.data("kendoWindow")) {
                        dkwindow1.kendoWindow({
                            width: "665px",
                            height: "300px",
                            modal: true,
                            draggable: false
                        });
                        dkwindow1.data("kendoWindow").center();
                    }
                    dkwindow1.data("kendoWindow").open();
                    $(".k-icon.k-i-close").hide();
                    $('a.k-window-action.k-link').mouseover(function () {
                        $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                        return false;
                    });
                    return false;
                }
            }

            var booksLeft = parseInt(jQuery('#lblBooksExceededCategory').text());
            //var booksSelected = parseInt(jQuery('#SelectedBookCountCategory').val());
            //            alert('booksleft' + booksLeft);
            //            alert('booksSelected' + booksSelected);
            //            return false;

            if (booksSelected - BooksAlreadyAdded > booksLeft) {
                if (!deleteFlag) {
                    $("#Delete-Categories").css({ 'display': 'block' });
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    dkwindow = $("#Delete-Categories"); //Give ur div id here
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
            }
            return true;
        });

    });


    function fun_okclick() {
        dkwindow.data("kendoWindow").close();
        return false;
    }

    function fun_Alreadyclick() {
        if ($($('#BookAlreadyAdded')[0].children[0].children[0])[0].innerHTML == "Success Message!") {
            $('#toggleEventHandler').click();
        }
        dkwindow1.data("kendoWindow").close();
        return false;

    }
    function SuccessMessage() {
        window.scroll(0, 0);
        $("#BookAlreadyAdded").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        $($('#BookAlreadyAdded')[0].children[0].children[0])[0].innerHTML = "Success Message!";
        $($('#BookAlreadyAdded')[0].children[1].children[0].children[0])[0].innerHTML = "Books Successfully Added to Your Subscription!!!";

        dkwindow1 = $("#BookAlreadyAdded"); //Give ur div id here
        if (!dkwindow1.data("kendoWindow")) {
            dkwindow1.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            dkwindow1.data("kendoWindow").center();
        }
        dkwindow1.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
            return false;
        });
        return false;

    }
    function doPostBack() {


    }
</script>


<asp:Label ID="lblIsBookAlreadyAdded" ClientIDMode="Static" runat="server" Style="display: none"
    Text="100">0</asp:Label>
<asp:Label ID="SelectedBookCountCategory" ClientIDMode="Static" runat="server" Style="display: none"
    Text="100">0</asp:Label>
<asp:Label ID="lblBooksExceededCategory" ClientIDMode="Static" runat="server" Text="12"
    Style="display: none"></asp:Label>
<div class="Div_FullWidth">
    <div style="float: left; width: 37%;">        
            <h2 style="float: left !important; color: #707070 !important;
            font-family: Raleway, Arial, Sans-Serif !important; font-weight: 700 !important; font-size: 25pt !important">Categories</h2>
    </div>
    <div style="float: left; width: 50%;">
        <asp:DropDownList ID="CategoriesDrpList" ClientIDMode="Static" runat="server" Style="height: 27px;
            float: left; position: absolute; color: #707070; font-family: Raleway, Arial, Sans-Serif;
            font-weight: 700; font-size: 10pt; width: 105px;" AutoPostBack="true" OnSelectedIndexChanged="CategoriesDrpList_SelectedIndexChanged">                        
            <asp:ListItem>TEXT TYPE</asp:ListItem>
            <asp:ListItem>PRIMARY SUBJECT</asp:ListItem>
            <asp:ListItem>GENRE</asp:ListItem>
            <asp:ListItem>CHARACTER FAMILY</asp:ListItem>
        </asp:DropDownList>
    </div>
</div>
<div style="float: left; width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
    border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
     background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
    margin-top: 6px; height: 40px; border: 1px solid lightgray;" class="LevelRepeaterClass">
    <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
        color: rgb(133, 133, 133); padding-right: 2px;" class="H5">You have selected</span><asp:Label
            ID="SelectedBookscountCategories" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
            margin-left: 3px; margin-right: 3px; float: left; margin-top: 11px; font-family: Raleway, Arial, Sans-Serif;
            font-weight: 700; font-size: 10pt; color: #707070;" ClientIDMode="Static"></asp:Label>
    <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
        padding-left: 2px;" class="H5">eBook/s</span>
    <asp:Button ID="AddtosubsButtonCategory" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
        OnClick="AddToSub_click" CssClass="disabledaddtosubtn" />
    <%--            <asp:Button ID="AddtosubsButtonCategory" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" OnClick="AddtosubsButtonCategory_Click"  />--%>
</div>
<br />
<br />
<br />
<div id="CategoriesContentDiv" style="float: right; width: 100%;margin-top:10px;">
    <asp:Repeater ID="CategoriesRepeater" runat="server" OnItemDataBound="CategoriesRepeater_ItemDataBound">
        <ItemTemplate>
            <div id="CategoriesRepeaterdiv" style="width: 100%; height: 102px; border: 1px solid lightgray;
                margin-bottom: 10px; border: 1px solid lightgray; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                 background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);" class="LevelRepeaterClass">
                <div id="CategoriesRepeaterContent" style="width: 100%; height: 50px; margin-top: 20px;">
                    <input type="checkbox" id="CategoriesCheckBoxes" runat="server" style="display: none" />
                    <img id="CategoriesCheckBoxImg" alt="" clientidmode="Static" style="margin-top: 24px;
                        float: left; margin-left: 16px; position: absolute;" src="<%=Page.ResolveUrl("Portals/0/images/unchecked.png")%>" />
                    <asp:Label ID="CategoriesTitleCollection" runat="server" ClientIDMode="Static" Text='<%# Eval("AttributeType")%>'
                        Style="float: left; font-family: Raleway, Arial, Sans-Serif; font-size: 12pt;
                        color: #707070; font-weight: 700; margin-top: 16px; position: absolute; margin-left: 60px;"></asp:Label>
                    <br />
                    <asp:Label ID="CategoriesCollectionCount" ClientIDMode="Static" runat="server" Style="float: left;
                        margin-top: 25px; color: #707070; font-family: Raleway, Arial, sans-serif; position: absolute;
                        margin-left: 60px; font-size: 10pt;display:none" Text='<%# Eval("BookCount")%>'></asp:Label>
                    <span style="float: left; margin-top: 25px; position: absolute; margin-left: 62px;
                        color: #707070; font-family: Raleway, Arial, sans-serif; font-size: 10pt;"><asp:Label Text='<%# Eval("BookCount")%>' runat=server></asp:Label> eBook/s
                        in collection</span>
                    <asp:Button ID="SeeallcurrentCategories" ClientIDMode="Static" runat="server" Text="SEE ALL"
                        OnClick="SeeallcurrentCategories_Click" Style="cursor: pointer; height: 25px;
                        float: right; color: white; font-family: Raleway, Arial, sans-serif; font-size: 8pt;
                        width: 80px; border: none; margin-top: 3px; margin-right: 18px; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                        background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                         background: -ms-linear-gradient(top, #0EB3D1 5%, #0F8797 130%);
                        -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px;
                        text-shadow: 1px 2px 2px #557183;" class="BooksLevelContentDivBtn" CommandArgument='<%# Eval("AttributeType") %>' />
                </div>
            </div>
            <div id="hiddenCategoriesContentdiv" style="display:none">
            <asp:Repeater ID="hiddencategoriesRepeater" runat="server">
                                        <ItemTemplate>
                                        <div id="hiddenCategoriesRepeaterdiv">
                                           <asp:Label ID="lblProductSk" ClientIDMode="Static" runat="server" Style="float: left;
                        margin-top: 25px; color: #707070; font-family: Raleway, Arial, sans-serif; position: absolute;
                        margin-left: 60px; font-size: 10pt;" Text='<%# Container.DataItem.ToString() %>'></asp:Label>
                        </div>
                                        </ItemTemplate>
             </asp:Repeater>         
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div style="float: left; width: 100%; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
     background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
    margin-top: 6px; height: 40px; border: 1px solid lightgray; margin-top: 0px;
    height: 40px; border: 1px solid lightgray;">
    <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
        color: rgb(133, 133, 133); padding-right: 2px;" class="H5">You have selected</span><asp:Label
            ID="SelectedBookscountBotCategories" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
            margin-left: 3px; font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; font-weight: 700;
            color: #707070; margin-right: 3px; float: left; margin-top: 11px;" ClientIDMode="Static"></asp:Label>
    <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
        padding-left: 2px" class="H5"> eBook/s</span>
    <asp:Button ID="BottomAddToSubscriptionCategory" ClientIDMode="Static" runat="server"
        Text="ADD TO SUBSCRIPTION" OnClick="botAddToSub_click" CssClass="disabledaddtosubtn" />
    <%--  <asp:Button ID="BottomAdsubButton" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" />--%>
</div>
<div id="Delete-Categories" title="Alert message!" style="display: none; background: white !important;">
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
            <span id="LiteralBooksRange" runat="server" style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">You have selected more books than your subscription allows. Please refine your selection or upgrade your subscription for access to more books.</span>
        </div>
        <div style="width: 92%;">
            <%-- <input type="button" id="YesButton" style="margin-left: 192px;" value="Ok" class="popupokbtn" />--%>
            <input type="button" id="NoButton" style="margin-left: 18px;" value="Ok" class="popupokbtn"
                onclick="javascript:fun_okclick();" />
        </div>
    </div>
</div>

<div id="BookAlreadyAdded" title="Alert message!" style="display: none; background: white !important;">
    <div class="alreadyaddedhdr">
        <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="LiteralAlreadyAdded"  runat=server style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">The Selected Books are already added !!!</span>
        </div>
        <div style="width: 92%;">
            <%-- <input type="button" id="YesButton" style="margin-left: 192px;" value="Ok" class="popupokbtn" />--%>
            <input type="button" id="NoButton1" style="margin-left: 18px;" value="Ok" class="popupokbtn"
                onclick="javascript:fun_Alreadyclick();" />
        </div>
    </div>
</div>
<asp:Button runat="server" ClientIDMode="Static" ID="toggleEventHandler" OnClick="toggleEventButton_Click" style="display:none;" />  
<asp:HiddenField runat="server" ID="AlreadySelectedBookCnt" Value="0" ClientIDMode="Static" />