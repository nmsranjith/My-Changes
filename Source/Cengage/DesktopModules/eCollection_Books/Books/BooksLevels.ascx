<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BooksLevels.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Books.Books.BooksLevels" %>
<script type="text/javascript">
    var popoupLevels, dkwindow1;
    var deleteFlag, deleteFlag1;
    $(document).ready(function () {       
        $('AddtosubsButton').attr('disabled', true);
        $('BottomAddToSubscription').attr('disabled', true);
        $("#AddtosubsButton,#BottomAddToSubscription").click(function () {
            $('#SelectedBookCount').val('0');

            for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                    var bookCount = $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv #BooksCollectionCount")[i].innerHTML;
                    $("#SelectedBookCount").val(parseInt($("#SelectedBookCount").val()) + parseInt(bookCount));
                }
            }



            $('#lblIsBookAlreadyAdded').val('0');
            for (var i = 0; i < $("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]").length; i++) {
                if ($("#BooksLevelDiv #BooksLevelRepeaterdiv #BooksLevelContentDiv input[type=checkbox]")[i].checked) {
                    var bookCount = $("#BooksLevelDiv #hiddenCategoriesContentdiv #hiddenCategoriesRepeaterdiv #lblProductSk")[i].innerHTML;
                    $("#lblIsBookAlreadyAdded").val(parseInt($("#lblIsBookAlreadyAdded").val()) + parseInt(bookCount));
                }
            }



            var BooksAlreadyAdded = parseInt(jQuery('#AlreadySelectedBookCnt').text());
            var booksSelected = parseInt(jQuery('#SelectedBookscountLevels').text());
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






            var booksLeft = parseInt(jQuery('#lblBooksExceeded').text());
//            var booksSelected = parseInt(jQuery('#SelectedBookscountLevels').val());
//            var BooksAlreadyAdded = parseInt(jQuery('#AlreadySelectedBookCnt').val());
            //alert('BooksLeft' + booksLeft);
            //alert('BooksSelected' + booksSelected);
            //return false;
            if (booksSelected - BooksAlreadyAdded > booksLeft) {
                if (true) {
                    $("#Delete-Levels").css({ 'display': 'block' });
                    $('.k-window-actions.k-header').css('cursor', 'pointer');
                    popoupLevels = $("#Delete-Levels"); //Give ur div id here
                    if (!popoupLevels.data("kendoWindow")) {
                        popoupLevels.kendoWindow({
                            width: "665px",
                            height: "300px",
                            modal: true,
                            draggable: false
                        });
                        popoupLevels.data("kendoWindow").center();
                    }
                    popoupLevels.data("kendoWindow").open();
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

    function doPostBack() {



    }



    function fun_levelclick() {
        popoupLevels.data("kendoWindow").close();
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




</script>
<h2 style="float: left !important; color: #707070 !important; font-family: Raleway, Arial, Sans-Serif !important;
    font-weight: 700 !important; font-size: 25pt !important">
    Level packs</h2>
<asp:Label ID="lblIsBookAlreadyAdded" ClientIDMode="Static" runat="server" Style="display: none"
    Text="100">0</asp:Label>
<asp:HiddenField ID="hiddenfieldtext" runat="server" ClientIDMode="Static" />
<asp:Label ID="SelectedBookCount" ClientIDMode="Static" runat="server" Style="display: none"
    Text="100">0</asp:Label>
<div style="float: left; width: 102.8%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
    border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
    background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
    margin-top: 6px; height: 40px; border: 1px solid lightgray;" class="LevelRepeaterClass">
    <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
        color: rgb(133, 133, 133); padding-right: 2px;" class="H5">You have selected </span><asp:Label
            ID="SelectedBookscountLevels" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
            margin-left: 3px; margin-right: 3px; float: left; margin-top: 11px; font-family: Raleway, Arial, Sans-Serif;
            font-weight: 700; font-size: 10pt; color: #707070;" ClientIDMode="Static">0</asp:Label>
    <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
        padding-left: 2px;" class="H5"> eBook/s</span>
    <asp:Label ID="lblBooksExceeded" ClientIDMode="Static" runat="server" Text="12" Style="display: none"></asp:Label>
    <asp:Button ID="AddtosubsButton" runat="server" ClientIDMode="static" Text="ADD TO SUBSCRIPTION"
        OnClick="AddToSub_click" CssClass="disabledaddtosubtn" />
    <%--  <asp:Button ID="AddtosubsButton" ClientIDMode="Static" runat="server" Text="Second" 
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" OnClick="test_click" />--%>
</div>
<div style="float: left; width: 100%; margin-top: 10px; height: 40px; border: 0px;">
    <span class="H5" style="float: left; margin-top: 11px; margin-left: 4px; color: rgb(133, 133, 133);">
        PM<br />
        colour</span> <span class="H5" style="margin-top: 11px; float: left; margin-left: 66px;
            color: rgb(133, 133, 133);">PM reading
            <br />
            level</span>
</div>
<div id="LevelContentDiv" style="float: left; width: 102.8%; margin-top: 20px;">
    <asp:Repeater ID="BooksRepeater" ClientIDMode="Static" runat="server" OnItemDataBound="BooksRepeater_ItemDataBound">
        <ItemTemplate>
            <div id="LevelRepeater" style="float: left; width: 100%; margin-bottom: 20px; border: 1px solid lightgray;
                background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
                position: relative;" class="LevelRepeaterClass">
                <div id="LevelContentDiv" clientidmode="Static" runat="server" style="float: left;
                    width: 8%; position: absolute; height: 100%; display: table-cell; text-align: center;
                    vertical-align: middle;">
                    <input type="checkbox" id="BookColourLevelCheckBox" runat="server" style="display: none" />
                    <div id="ImgDiv" style="top: 44%; float: left; position: absolute; margin-left: 18px;">
                        <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left;cursor:pointer;" src="<%=Page.ResolveUrl("Portals/0/images/unchecked.png")%>" />
                    </div>
                    <asp:Literal ID="ColorValue" runat="server" Visible="false" Text='<%# Eval("AttributeType")%>'></asp:Literal>
                    <span style="display:none;"><%# Container.ItemIndex %></span>
                </div>
                <div id="BooksLevelDiv" style="float: right; width: 92%;">
                    <asp:Repeater ID="BooksLevelRepeater" runat="server" OnItemDataBound="BooksLevelRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div id="BooksLevelRepeaterdiv" style="width: 100%; height: 102px; border: 1px solid lightgray;">
                                <div id="BooksLevelContentDiv" style="width: 100%; height: 50px; margin-top: 20px;">
                                    <input type="checkbox" id="CheckBoxes" clientidmode="Static" runat="server" style="display: none" />
                                    <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="margin-top: 24px;
                                        float: left; margin-left: 16px; position: absolute;cursor:pointer;" src="<%=Page.ResolveUrl("Portals/0/images/unchecked.png")%>" />
                                    <asp:Label ID="lblAttributeType" ClientIDMode="Static" runat="server" Text='<%# Eval("AttributeType")%>'
                                        Style="float: left; font-family: Raleway, Arial, Sans-Serif; font-size: 12pt;
                                        color: #707070; font-weight: 700; margin-top: 16px; position: absolute; margin-left: 60px;"></asp:Label>
                                    <br />
                                    <asp:Label ID="BooksCollectionCount" ClientIDMode="Static" runat="server" Style="float: left;
                                        margin-top: 25px; color: #707070; font-family: Raleway, Arial, Sans-Serif; position: absolute;
                                        margin-left: 60px; font-size: 10pt;display:none;" Text='<%# Eval("BookCount")%>'></asp:Label>
                                    <span style="float: left; margin-top: 25px; position: absolute; margin-left: 62px;
                                        font-size: 10pt; color: #707070; font-family: Raleway, Arial, Sans-Serif;"><asp:Label ID="Label1" Text='<%# Eval("BookCount")%>' runat="server"></asp:Label> eBook/s
                                        in collection</span>
                                    <asp:Button ID="SeeallcurrentLevel" ClientIDMode="Static" runat="server" Text="SEE ALL"
                                        OnClick="SeeallcurrentLevel_Click" Style="cursor: pointer; height: 25px; float: right;
                                        color: white; font-family: Raleway, Arial, sans-serif; font-size: 8pt; width: 80px;
                                        border: none; margin-top: 3px; margin-right: 18px; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                                        background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                                        background: -ms-linear-gradient(top, #0EB3D1 5%, #0F8797 130%); 
                                        
                                        -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px;
                                        text-shadow: 1px 2px 2px #557183;" class="BooksLevelContentDivBtn" CommandArgument='<%# Eval("AttributeType") %>' />
                                </div>
                                <div id="hiddenCategoriesContentdiv" style="display: none">
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
                            </div>
                            
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div style="float: left; width: 102.8%; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
    background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
    margin-top: 6px; height: 40px; border: 1px solid lightgray; margin-top: 6px;
    height: 40px; border: 1px solid lightgray;">
    <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
        color: rgb(133, 133, 133); padding-right: 2px;" class="H5">You have selected </span><asp:Label
            ID="SelectedBookscountBotLevels"  ClientIDMode="Static" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
            margin-left: 3px; font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; font-weight: 700;
            color: #707070; margin-right: 3px; float: left; margin-top: 11px;"></asp:Label>
    <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
        padding-left: 2px" class="H5"> eBook/s</span>
    <asp:Button ID="BottomAddToSubscription" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
        OnClick="botAddToSub_click" CssClass="disabledaddtosubtn" />
    <%--  <asp:Button ID="BottomAdsubButton" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" />--%>
</div>
<div id="Delete-Levels" title="Alert message!" style="display: none; background: white !important;">
    <div class="levelsuccesspopHdr">
        <span class="AfterRenewelHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="MessageLiteral" style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">You have selected more books than your subscription allows. Please refine your selection or upgrade your subscription for access to more books.</span>
        </div>
        <div style="width: 92%;">
            <%-- <input type="button" id="YesButton" style="margin-left: 192px;" value="Ok" class="popupokbtn" />--%>
            <input type="button" id="NoButton" value="Ok" class="popupokbtn"
                onclick="javascript:fun_levelclick();" />
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
            <span id="LiteralAlreadyAdded" runat=server style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">The Selected Books
                are already added !!!</span>
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
