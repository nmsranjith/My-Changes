<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeeAllCollection.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Books.Books.SeeAllCollection" %>
<style  type="text/css">
.bookRowStyle
{
    height: 143px; width: 100%; float: left; border: 1px solid lightgray;
                            margin-bottom: 20px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
							background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
                            box-shadow: 1px 1px 5px lightgray;
}
.BooksImgClass
    {
        margin-top: 0px; 
        margin-left: 5px;                
    }
</style>
<script type="text/javascript">
    function btnAccept_onclick() {
       // $("#SeeAlldiv").parent().parent().appendTo("form");
    }
    var dkseeAll;
    var deleteFlag;

    jQuery(document).ready(function () {
        $('#AddtosubsButtonSeeAll').attr('disabled', true);
        $('#BottomAddToSeeAll').attr('disabled', true);
        doPostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(doPostBack);

      
      
    });

    function doPostBack() {

        $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img").click(function () {
            var LevelBookcount = 0;
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                this.parentNode.parentNode.className = "bookRowStyle rowclick";
                this.src = GetFile("/Portals/0/images/tick_student.png");
                //                for (var i = 0; i < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; i++) {
                //                    if ($("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[i].checked) {
                //                        $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                //			            $("#SelectedBookdiv #SelectedBookRepdiv")[i].className = "bookRowStyle rowclick";
                //                    }
                //                }
                jQuery("#AddtosubsButtonSeeAll").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                $('[id$=AddtosubsButtonSeeAll]').attr('disabled', false);
                jQuery("#BottomAddToSeeAll").removeClass("disabledaddtosubtn").addClass("addtosubtn");
                $('[id$=BottomAddToSeeAll]').attr('disabled', false);

                for (var i = 0; i < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; i++) {
                    if ($("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[i].checked) {
                        //$("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        LevelBookcount++;
                    }
                }

                $('#SelectedBookscountSeeAll').text(LevelBookcount);
                $('#SelectedBookscountBotSeeAll').text(LevelBookcount);
            }
            else {
                var count = 0;
                this.parentNode.parentNode.className = "bookRowStyle";
                this.src = GetFile("/Portals/0/images/circle_big.png");
                //                for (var j = 0; j < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; j++) {
                //                    if (!$("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[j].checked) {
                //                        $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img")[j].src = GetFile("/Portals/0/images/circle_big.png");
                //                        $("#SelectedBookdiv #SelectedBookRepdiv")[j].className = "bookRowStyle";
                //                    }
                //                }
                for (var i = 0; i < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; i++) {
                    if ($("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[i].checked) {
                        //$("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        count++;
                    }
                }
                if (count == 0) {
                    jQuery("#AddtosubsButtonSeeAll").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                    $('[id$=AddtosubsButtonSeeAll]').attr('disabled', true);
                    jQuery("#BottomAddToSeeAll").removeClass("addtosubtn").addClass("disabledaddtosubtn");
                    $('[id$=BottomAddToSeeAll]').attr('disabled', true);
                }

                for (var i = 0; i < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; i++) {
                    if ($("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[i].checked) {
                        //$("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        LevelBookcount++;
                    }
                }

                $('#SelectedBookscountSeeAll').text(LevelBookcount);
                $('#SelectedBookscountBotSeeAll').text(LevelBookcount);
            }
        });


        $("#AddtosubsButtonSeeAll").click(function () {
            if (AddToSub()) return true;
            else
                return false;
        });

        $("#BottomAddToSeeAll").click(function () {
            if (AddToSub()) return true;
            else
                return false;
        });




    }

    function AddToSub() {

        $('#SelectedBookCountSeeAll').val('0');
        var count = 0;
        for (var i = 0; i < $("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]").length; i++) {
            if ($("#SelectedBookdiv #SelectedBookRepdiv #SelectedBookContentFrstdiv input[type=checkbox]")[i].checked) {
                count++;
            }
        }

        $("#SelectedBookCountSeeAll").val(count);

        var booksLeft = parseInt(jQuery('#lblBooksExceededSeeAll').text());
        var booksSelected = parseInt(jQuery('#SelectedBookCountSeeAll').val());
        //                        alert('booksleft' + booksLeft);
        //                        alert('booksSelected' + booksSelected);
        //                        return false;

        if (booksSelected > booksLeft) {        
            if (!deleteFlag) {
                $("#Delete-SeeAll").css({ 'display': 'block' });
                $($('#Delete-SeeAll')[0].children[0].children[0])[0].innerHTML = "Alert Message!";
                $($('#Delete-SeeAll')[0].children[1].children[0].children[0])[0].innerHTML = "You have selected more books than your subscription allows. Please refine your selection or upgrade your subscription for access to more books.";
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                dkseeAll = $("#Delete-SeeAll"); //Give ur div id here
                if (!dkseeAll.data("kendoWindow")) {
                    dkseeAll.kendoWindow({
                        width: "665px",
                        height: "300px",
                        modal: true,
                        draggable: false
                    });
                    dkseeAll.data("kendoWindow").center();
                }
                dkseeAll.data("kendoWindow").open();
                $(".k-icon.k-i-close").hide();
                $('a.k-window-action.k-link').mouseover(function () {
                    $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                    return false;
                });
                return false;
            }
        }

        return true;

    }

    function fun_SeeAllclick() {
        if ($($('#Delete-SeeAll')[0].children[0].children[0])[0].innerHTML == "Success Message!") {
            $('#toggleEventHandler').click();
        }
        dkseeAll.data("kendoWindow").close();
        return false;
    }

    function SuccessMessage() {
        window.scroll(0, 0);
        $("#Delete-SeeAll").css({ 'display': 'block' });
        $($('#Delete-SeeAll')[0].children[0].children[0])[0].innerHTML = "Success Message!";
        $($('#Delete-SeeAll')[0].children[1].children[0].children[0])[0].innerHTML = "Books Successfully Added to Your Subscription!!!";
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        dkseeAll = $("#Delete-SeeAll"); //Give ur div id here
        if (!dkseeAll.data("kendoWindow")) {
            dkseeAll.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            dkseeAll.data("kendoWindow").center();
        }
        dkseeAll.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
            return false;
        });
        return false;
    }
</script>
        
        <div id="LevelCollection" clientidmode="Static" runat="server" style="float: left; width: 100%; margin-top: 5px;">           
        <span id="headSpan" runat="server" clientidmode="Static" style="float: left; color: #707070;
            font-family: Raleway, Arial, Sans-Serif; font-weight: 700; font-size: 25pt">Reading Age</span>
            <asp:HiddenField id="hiddenfieldtext" runat="server" clientidmode="Static" />            
              <asp:Label ID="SelectedBookCountSeeAll" ClientIDMode="Static" runat="server" Style="display:none" Text="100">0</asp:Label>
            <asp:Label ID="lblBooksExceededSeeAll" ClientIDMode="Static" runat="server" Text="12" Style="display:none"></asp:Label>
            <asp:Button ID="BacktoCollection" runat="server" Text="Back to collections"
            Style="cursor: pointer; color: #22B3E2; float: left; background: none; background: transparent;
            width: 138px; height: 23px; border: 0px; margin-top: 12px; margin-left: 2px;"    OnClick="BacktoCollection_Click"/>


        <div style="float: left; width: 102.8%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
            border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
            background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
            margin-top: 6px; height: 40px; border: 1px solid lightgray;" class="LevelRepeaterClass">
            <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
                color: rgb(133, 133, 133); padding-right: 2px;" class="H5">You have selected </span><asp:Label
                    ID="SelectedBookscountSeeAll" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
                    margin-left: 3px; margin-right: 3px; float: left; margin-top: 11px; font-family: Raleway, Arial, Sans-Serif;
                    font-weight: 700; font-size: 10pt; color: #707070;" ClientIDMode="Static"></asp:Label>
            <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
                padding-left: 2px;" class="H5"> eBook/s</span>
                <asp:Button ID="AddtosubsButtonSeeAll" ClientIDMode=Static  runat="server" Text="ADD TO SUBSCRIPTION" OnClick="AddToSub_click"  CssClass="disabledaddtosubtn"  />
          <%--  <asp:Button ID="AddtosubsButtonSeeAll" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" />--%>
        </div>
           
            <br />
            <div id="SelectedBookdiv" style="width: 102.8%; float: left; margin-top: 15px;">
                <asp:Repeater ID="RepSeeAllBooks" runat="server" OnItemDataBound="RepSeeAllBooks_ItemDataBound"  >
                    <ItemTemplate>
                        <div id="SelectedBookRepdiv" class="bookRowStyle">
                            <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 64px;min-width: 57px;">
                                <input type="checkbox" id="BooksCheckBox" runat="server" checked='<%# Eval("Checked") %>' style="float: left; margin-left: 24px;
                                    display: none;"  visible= '<%# Eval("AlreadyAvailable") %>'/>
                                <asp:Label ID="lblPRODUCT_SK" runat="server" Visible ="false"   Text='<%# Eval("PRODUCT_SK") %>'></asp:Label>
                                <%--<asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Visible="false" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>--%>
                                <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("IMAGE_FILE_NAME") %>'></asp:Label>
                               <%-- <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left; margin-left: 24px;
                                    width: 20px;" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />--%>
                                <asp:Image ID="checkboximg" runat="server" style="float: left;cursor:pointer; margin-left: 24px;
                                    width: 20px;" ImageUrl="~/Portals/0/images/circle_big.png" Visible='<%# Eval("AlreadyAvailable") %>' />    
                            </div>
                            <div class="DashBoard_Items_books">
                                <asp:Image runat="server" ID="BookCoverImage" ClientIDMode="Static" CssClass ="DashBoard_Items_books_images BooksImgClass" ImageUrl='<%# Eval("IMAGE_FILE_NAME")%>'/>
                            </div>
                            <div style="width: 61%; float: right; margin-top: 25px;">
                                <div style="width: 100%; float: left">
                                    <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text='<%# Eval("Title") %>'></asp:Label>
                                 <%--   <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">Years</span>
                                    <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; padding: 5px;
                                        font-family:Raleway-regular,Raleway, arial, sans-serif; font-weight: 700;" runat="server" Text="5"></asp:Label>
                                    <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">and</span>
                                    <asp:Label ID="ToYearLabel" runat="server" Style="padding: 5px; color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text="6"></asp:Label>--%>
                                    <br />
                                    <asp:Label ID="AuthorNameLabel" runat="server" Style="font-family: Arial-Regular, Sans-Serif;
                                        font-size: 10pt; color: #707070; margin-top: 5px; float: left;" Text='<%# Eval("Author") %>'></asp:Label>
                                </div>
                               <div style="font-family: Arial-Regular, Sans-Serif; width: 100%; float: left; margin-top: 5px;
                                    font-size: 10pt; color: #707070;">
                                    <asp:Label ID="ColourLabel" runat="server" Text='<%# Eval("ColourLevel") %>'></asp:Label>,
                                    <asp:Label ID="ReadingLevelLabel" runat="server"  Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,                                    
                                    <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label><br />
                                    <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>' Style="float:left;margin-top: 5px;"></asp:Label>
                                    <asp:Label ID="YearLabel" runat="server" Style="float:left;margin-top:5px ;width:100%" Text='<%#  string.Format("Copyright: {0}", Eval("COPYRIGHT_YEAR")) %>'></asp:Label>
                                </div>
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
                    ID="SelectedBookscountBotSeeAll" runat="server" Text="0" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
                    margin-left: 3px; font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; font-weight: 700;
                    color: #707070; margin-right: 3px; float: left; margin-top: 11px;" ClientIDMode="Static"></asp:Label>
            <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; color: rgb(133, 133, 133);
                padding-left: 2px" class="H5"> eBook/s</span>
                <asp:Button ID="BottomAddToSeeAll" ClientIDMode=Static  runat="server" Text="ADD TO SUBSCRIPTION" OnClick="botAddToSub_click"  CssClass="disabledaddtosubtn" />
            <%--<asp:Button ID="BottomAdsubButton" ClientIDMode="Static" runat="server" Text="ADD TO SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #0EB3D1 5%, #0F8797 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#0EB3D1), to(#0F8797));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0EB3D1', endColorstr='#0F8797', gradientType='0');
                margin-top: 7.3px; height: 25px; width: 166px; margin-right: 17px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #0F8797; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #557183;" />--%>
        </div>
        </div>
                      <div id="Delete-SeeAll" title="Alert message!" style="display: none; background: white !important;">
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
                font-size: 10pt; color: #707070; padding: 23px; float: left;">You have selected more books than your subscription allows. Please refine your selection or upgrade your subscription for access to more books.</span>                
        </div>
        <div style="width: 92%;">
           <%-- <input type="button" id="YesButton" style="margin-left: 192px;" value="Ok" class="popupokbtn" />--%>
            <input type="button" id="SeeAllNoButton" style="margin-left: 18px;" value="Ok" class="popupokbtn" onclick="javascript:fun_SeeAllclick()" />
        </div>
    </div>
</div>
<asp:Button runat="server" ClientIDMode="Static" ID="toggleEventHandler" OnClick="toggleEventButton_Click" style="display:none;" />          

           
            
