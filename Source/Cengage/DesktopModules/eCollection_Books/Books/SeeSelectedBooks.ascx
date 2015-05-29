<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeeSelectedBooks.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Books.Books.SeeSelectedBooks" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
    <%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<script type="text/javascript">
    function SetBookReadLevel() {
        var readLevel = $("#amount").val().split('-');
        var fromReadLvl = parseInt(readLevel[0]) + 1;
        var toReadLvl = readLevel[1];
        document.getElementById("SliderRange").value = $("#amount").val();
        $("#hiddenbtn").click();
    }
    function GetFwdPageNumber() {
        var pageno = 0;
        document.getElementById("SliderRange").value = $("#amount").val();
        pageno = parseInt(document.getElementById("pageNumber").value.trim());
        pageno++;
        document.getElementById("pageNumber").value = pageno;
    }
    function GetBckPageNumber() {
        var pageno = 0;
        document.getElementById("SliderRange").value = $("#amount").val();
        pageno = parseInt(document.getElementById("pageNumber").value.trim());
        pageno--;
        document.getElementById("pageNumber").value = pageno;
    }
    function CheckBoxImgClick(e) {
        
        e.parentNode.children[0].click();
        var selectedID = document.getElementById("custItmSKHidden").value.split(',');
        if (e.parentNode.children[0].checked) {
            
            document.getElementById("custItmSKHidden").value += e.parentNode.children[1].innerHTML.trim() + ",";
            e.parentNode.parentNode.className = "bookRowStyle rowclick";
            e.src = GetFile("/Portals/0/images/tick_student.png");
            var isGracePeriod = jQuery('#lblGracePeriod').text();

            //if (isGracePeriod == 'False') {
            jQuery("#RemovetosubsButtontop").removeClass("disabledremovetosubbtn").addClass("removetosubbtn");
            $('[id$=RemovetosubsButtontop]').attr('disabled', false);
            jQuery("#RemoveSubButtonbot").removeClass("disabledremovetosubbtn").addClass("removetosubbtn");
                $('[id$=RemoveSubButtonbot]').attr('disabled', false);
            //}
        }
        else {
            document.getElementById("custItmSKHidden").value = "";
            e.parentNode.parentNode.className = "bookRowStyle";
            e.src = GetFile("/Portals/0/images/circle_big.png");
            var index = selectedID.indexOf(e.parentNode.children[1].innerHTML.trim());
            if (index > -1)
                selectedID.splice(index, 1);

            var blkstr = $.map(selectedID, function (val, index) {
                var str = val;
                return str;
            }).join(",");
            document.getElementById("custItmSKHidden").value = blkstr;   
            if(document.getElementById("custItmSKHidden").value == "")
            {         
                jQuery("#RemovetosubsButtontop").removeClass("removetosubbtn").addClass("disabledremovetosubbtn");
                $('[id$=RemovetosubsButtontop]').attr('disabled', true);
                jQuery("#RemoveSubButtonbot").removeClass("removetosubbtn").addClass("disabledremovetosubbtn");
                $('[id$=RemoveSubButtonbot]').attr('disabled', true);
            }
        }
    }
    $(document).ready(function () {       
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    function PostBack() {
        pageName = 'Profile';
        //  $('#booksListDiv').height('2425px');
      //  $('#eCollectionMenu').height($('#eCollectionContent').height());

        $("#eCollectionContent").height($("#eCollectionContent").children()[0].offsetHeight);
        $('#eCollectionMenu').height((jQuery('#eCollectionContent').height()) + 'px');
        $('[id$=RemovetosubsButtontop]').attr('disabled', true);
        $('[id$=RemoveSubButtonbot]').attr('disabled', true);

        jQuery("#level").css({ "color": "#1FB5E7" });
        //jQuery("#slider-range").css({ "display": "inline-block", "margin-left": "12px", "margin-top": "20px" });
        jQuery("#<%=RemovetosubsButtontop.ClientID %>").click(function () {
            //alert("Functionality under construction.......");
            //return false;
        });
        jQuery("#<%=RemoveSubButtonbot.ClientID %>").click(function () {
            //alert("Functionality under construction.......");
            // return false;
        });
        //jQuery("#booksliderDiv").addClass("divgradient");
      

    }
    
</script>
<style type="text/css">
    .eCollectionContentStyle ,.eCollectionMenuStyle
    {
        padding-bottom:40px;
    }
     .ui-widget-header
    {
        border: 0px solid transparent !important;
    }
    .Sliderholder
    {
        width: 97% !important;
        margin-left: 0px !important;
    }
     .sliderDivProfSaf
    {
        width: 94.8% !important;
    }
     .sliderDivProfIE
    {
        width: 94% !important;
         margin-left: 10px !important;
    }
    .sliderDivProfMoz
    {
        width: 97.1% !important;
    }
    .BooksImgClass
    {
        margin-top: 4px; 
        margin-left: 5px;                
    }    
         .bookRowStyle
        {
            height: 143px; width: 100%; float: left; border: 1px solid lightgray;
                            margin-bottom: 20px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
							background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
                            box-shadow: 1px 1px 5px lightgray;
        }
     #SeeSelectedBokksdiv:hover
     {
         background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#537A37), color-stop(32%,#59833A), color-stop(43%,#557D38), color-stop(95%,#436A32)) !important;
        background: -webkit-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -o-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: -ms-linear-gradient(top, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        background: linear-gradient(to bottom, #537A37 1%,#59833A 32%,#557D38 43%,#436A32 95%) !important;
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#537A37', endColorstr='#436A32',GradientType=0 ) !important;
     }  
#BookSearchdiv .k-dropdown-wrap
{
   
background-color: white !important;
color:#707070 !important;
box-shadow: 0px 2px 5px 0px lightgray !important;
width:105px !important;
height: 26px !important;
 background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent !important;
            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9)) !important;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0') !important;
            background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
}
#CategoriesDrpList-list
{
    width:125px !important;
    color:#707070 !important;
}
#BookSearchdiv .k-dropdown-wrap .k-input
{
    height: 1.3em !important;
    margin-top: 3px !important;
    text-indent:18px !important;
    color:#707070 !important;
}
#BookSearchdiv .k-dropdown-wrap .k-select
{
    margin-top: 4px !important;
    cursor:pointer;
    color:#707070 !important;
}
    /*Custom Paging*/
    .page:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#707070), to(#707070));
        background: -moz-linear-gradient(to bottom,#707070, #707070);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0');*/
        background: -ms-linear-gradient(top, #707070 5%, #707070 130%);
        background-color: #707070;
        color: White !important;
    }
    .Pager a[disabled]:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#F9F9F9), to(#E9E9E9));
        background: -moz-linear-gradient(to bottom,#F9F9F9, #E9E9E9);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#E9E9E9', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#F9F9F9', gradientType='0');*/
        background: -ms-linear-gradient(top, #F9F9F9 5%, #E9E9E9 130%);
        background-color: #E9E9E9;
    }
    #PageControl
    {
        margin-left: 0px !important;
    }
    #PagerHolder
    {
        margin-bottom: 36px;
    }
    </style>
<div id="booksListDiv" style="width: 100%; float: left; margin-top: 20px; height: auto; margin-bottom: 39px;">
    <asp:Label ID="lblGracePeriod" ClientIDMode="Static" runat="server" Text="12" Style="display: none"></asp:Label>
    <div class="GpNamediv">
        <span>Book selection wizard</span><br />
        <br />
        <br />
        <p>
            Make the most of your subscription by selecting the eBooks you want. You still have
            time to finalise your book selection. If you haven't filled your subscription when
            the grace period expires the eCollection system will automatically fill your subscription
            with eBooks.
        </p>
    </div>
    <div class="GpMemberCountdiv" style="margin-left: 25px;">
        <img src="<%=Page.ResolveUrl("Portals/0/images/BookSelected.png")%>" alt="BookSelected"
            style="float: left; margin-left: 16px; margin-top: 25px; margin-bottom: 25px;
            margin-right: 6px;" />
        <asp:Label ID="lblBooksSelected" runat="server" CssClass="GPMemCountlbl" Text=""></asp:Label>
        <span class="H5 GPMemSpan" style="padding-right: 15px">EBOOK/S SELECTED</span>
    </div>
    <div class="GpMemberCountdiv">
        <img src="<%=Page.ResolveUrl("Portals/0/images/BookLeft.png")%>" alt="BookSelected"
            style="float: left; margin-left: 20px; margin-top: 15px; margin-bottom: 15px;
            margin-right: 20px;" />
        <asp:Label ID="lblBooksLeft" runat="server" CssClass="GPMemCountlbl" Text=""></asp:Label>
        <span class="H5 GPMemSpan">EBOOK/S LEFT</span>
    </div>
    <div class="GpMemberCountdiv" style="margin-right: 1px;">
        <img src="<%=Page.ResolveUrl("Portals/0/images/DaysLeft.png")%>" alt="BookSelected"
            style="float: left; margin-left: 20px; margin-top: 25px; margin-bottom: 25px;
            margin-right: 20px;" />
        <asp:Label ID="lblDaysLeft" runat="server" CssClass="GPMemCountlbl" Text=""></asp:Label>
        <span class="H5 GPMemSpan">DAY/S LEFT</span>
    </div>
    <div id="SeeSelectedBokksdiv" style="-moz-border-radius: 3px; -webkit-border-radius: 3px;
        border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
        background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
        background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );
        width: 30.4%; float: left; margin-left: 25px; margin-top: 10px; height: 35px;">
        <asp:Button ID="SeeSelectedBokks" ClientIDMode="Static" runat="server" Text="SEE SELECTED EBOOK/S"
            CssClass="SeeSelectedDisabled SeeSelectedBooksImage" OnClick="SeeSelectedBokks_Click" />
    </div>
    <div style="float: left; width: 92.8%; margin-top: 33px; margin-left: 25px;">
        <span id="headSpan" style="float: left; font-weight: bold; font-weight: 700; font-family: Raleway, Arial,Sans-Serif;
            color: #707070; font-size: 25pt"> eBook/s in subscription</span>
        <div style="float: left; margin-top: 10px; margin-left: 22px;">
            <asp:Button ID="BacktoCollectionButton" runat="server" Text="Back to Book selection wizard"
                Style="cursor: pointer; color: #22B3E2; float: left; background: none; width: 196px;
                height: 23px; border: 0px;display:none;" OnClick="BacktoCollectionButton_Click" />
        </div>
        <div style="float: left; width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
            border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
            background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
            margin-top: 6px; height: 40px; border: 1px solid lightgray; box-shadow: 1px 1px 5px lightgray;">
            <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
                font-family: Raleway, Arial, Sans-Serif; font-weight: bold; font-size: 10pt;
                color: rgb(133, 133, 133);">You have selected </span><asp:Label ID="SelectedBookscount"
                    runat="server" Text="58" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
                    margin-left: 3px; margin-right: 3px; float: left; margin-top: 11px; font-weight: 700;
                    font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; color: #707070;"></asp:Label>
            <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; font-family: Raleway, Arial, Sans-Serif;
                font-size: 10pt; color: rgb(133, 133, 133); font-weight: bold;"> eBook/s</span>
            <asp:Button ID="RemovetosubsButtontop" ClientIDMode="Static" runat="server" Text="REMOVE FROM SUBSCRIPTION"
                OnClick="RemoveFromSubscription_Click" CssClass="disabledremovetosubbtn" />
            <%-- <asp:Button ID="RemovetosubsButtontop" ClientIDMode="Static" runat="server" Text="REMOVE FROM SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #D308AF 5%, #A8066A 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#D308AF), to(#A8066A));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#D308AF', endColorstr='#A8066A', gradientType='0');
                margin-top: 6px; height: 25px; width: 195px; margin-right: 18px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #A8066A; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #722993;"  OnClick="RemoveFromSubscription_Click" />--%>
        </div>
        <div style="width: 100%; float: left;">
            <%--            <div id="BookSearchdiv" style="float: left; width: 100%; border: 1px solid lightgray;
                margin-top: 15px; height: 40px;">
                <asp:TextBox ID="BookSearchTextBox" runat="server" Style="border: 0px solid white;
                    width: 320px; height: 30px; margin-top: 3px; margin-left: 2px; position: absolute;"></asp:TextBox>
                <asp:Button ID="BookSearchButton" ClientIDMode="Static" runat="server" Text="Search"
                    Style="color: White; float: right; width: 88px; font-family: Raleway, Arial, Sans-Serif;
                    text-shadow: 1px 2px 2px #4D7B18; font-size: 10pt; height: 33px; margin-top: 3px;
                    margin-right: 3px; box-shadow: 0px 2px 5px 0px lightgray !important; border: 1px solid #5E872E;
                    -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; khtml-border-radius: 3px;
                    background: -moz-linear-gradient(top, #689A2E 1%, #74A738 32%, #74A738 43%, #5E872E 95%);
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(1%,#689A2E), color-stop(32%,#74A738), color-stop(43%,#74A738), color-stop(95%,#5E872E));
                    background: -webkit-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
                    background: -o-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
                    background: -ms-linear-gradient(top, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
                    background: linear-gradient(to bottom, #689A2E 1%,#74A738 32%,#74A738 43%,#5E872E 95%);
                    filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#689A2E', endColorstr='#5E872E',GradientType=0 );" />

                <select id="CategoriesDrpList" clientidmode="Static" runat="server" style="float: right;
                    margin-top: 4px; margin-right: -20px; text-shadow: 1px 2px 2px #F6F6F6; font-family: Raleway, Arial, Sans-Serif;
                    font-weight: 700; font-size: 10pt; color: #707070;">
                    <option>[PACK NAME]</option>
                </select>
            </div>--%>
            <%-- <div style="float: left; width: 100%; border: 1px solid lightgray; margin-top: 14px;
                height: 135px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');">
                <span id="TitleSpan" style="font-size: 12pt; font-weight: bold; color: #707070; margin-top: 13px;
                    float: left; margin-left: 15px;">TITLES AVAILABLE ON
                    <label id="level" for="level">
                        <%--MARY'S
                    </label>
                    BOOK SHELF: </span>
                <hr style="float: left; width: 94%; margin-left: 15px; border-left: 0px;" />
                <RL:RLSlider ID="ReadingLevelSlider" runat="server">
                </RL:RLSlider>
            </div>--%>
            <div style="width: 98.3%; margin-top: 10px; padding: 15px 0px 65px 11px; float: left; border: 1px solid #CCC;">
                <h5>
                    TITLES AVALABLE ON THE BOOKSHELF:
                </h5>
                <div class="eCollectionEditLbl">
                    <%-- style="margin-top: 30px">--%>
                    <RL:RLSlider ID="ReadingLevelSlider" runat="server">
                    </RL:RLSlider>
                    <asp:HiddenField ID="SliderValue" ClientIDMode="Static" runat="server" />
                </div>
            </div>
            <asp:HiddenField ID="custItmSKHidden" ClientIDMode="Static" runat="server" />
            <asp:HiddenField ID="SliderRange" ClientIDMode="Static" runat="server" />
            <div id="SelectedBookdiv" style="width: 100%; float: left; margin-top: 15px;">
                <asp:UpdatePanel ID="BooksUpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="hiddenbtn" ClientIDMode="Static" Style="display: none" runat="server"
                            OnClick="hiddenbtn_Click" />
                        <asp:Repeater ID="CollectionRepeater" runat="server"  OnItemDataBound="CollectionRepeater_ItemDataBound" >
                            <ItemTemplate>
                                <div id="SelectedBookRepdiv" class='<%# Eval("ClassName") %>'>
                                    <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 64px;min-width: 57px;">
                                        <input type="checkbox" id="BooksCheckBox" runat="server" checked='<%# Eval("Checked") %>'
                                            style="float: left; margin-left: 24px; display: none;" />
                                        <asp:Label ID="lblPRODUCT_SK" runat="server" Style="display: none" Text='<%# Eval("PRODUCT_SK") %>'></asp:Label>
                                        <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Style="display: none" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                                        <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("IMAGE_FILE_NAME") %>'></asp:Label>
                                        <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left; margin-left: 24px;
                                            cursor: pointer; width: 20px;" onclick="javascript:CheckBoxImgClick(this);" src='<%# Page.ResolveUrl(Eval("CheckImgPathName").ToString()) %>' />
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
                                            font-size: 10pt; color: #707070;" >
                                            <%--<asp:Label ID="StudentLabel" runat="server" Text="Student: " Style="font-style: italic"></asp:Label>--%>
                                            <asp:Label ID="ColourLabel" runat="server" Text='<%# Eval("ColourLevel") %>'></asp:Label>,
                                            <asp:Label ID="ReadingLevelLabel" runat="server" Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,
                                            <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label><br />
                                            <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>' Style="float:left;margin-top: 5px;"></asp:Label>
                                            <asp:Label ID="YearLabel" runat="server" Style="float:left;margin-top:5px ;width:100%" Text='<%#  string.Format("Copyright: {0}", Eval("COPYRIGHT_YEAR")) %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                          <CP:CustomPaging ID="CustomPaging" runat="server">
                        </CP:CustomPaging><%--
                     <center id="PagerHolder" class="Pager" style="min-width: 90%">
                            <div id="StudentPagerDiv" clientidmode="Static" style="width: 96%; float: right;
                                display: block; margin-top: 18px;" runat="server">
                                <div style="float: left; margin-left: 227px; width: 23.8%;">
                                    <asp:Button ID="PreviousButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                        border: 0px solid white; float: left; background: transparent;" OnClientClick="javascript:GetBckPageNumber();"
                                        ClientIDMode="Static" runat="server" Text="Previous" OnClick="PreviousButton_Click">
                                    </asp:Button>
                                    <asp:Button ID="ShowNextButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                        margin-left: 7px; border: 0px solid white; float: left; background: transparent;"
                                        OnClientClick="javascript:GetFwdPageNumber();" ClientIDMode="Static" runat="server"
                                        Text="Next>>" OnClick="ShowNextButton_Click"></asp:Button>
                                </div>
                                <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="1" runat="server" />
                            </div>
                        </center>  --%> 
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div style="float: left; width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px;
            border-radius: 3px; -khtml-border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
            background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
           background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%); 
            margin-top: 6px; height: 40px; border: 1px solid lightgray; box-shadow: 1px 1px 5px lightgray;">
            <span style="text-shadow: 1px 2px 2px #F6F6F6; float: left; margin-top: 11px; margin-left: 20px;
                font-family: Raleway, Arial, Sans-Serif; font-weight: bold; font-size: 10pt;
                color: rgb(133, 133, 133);">You have selected </span><asp:Label ID="SelectedBookscountBot"
                    runat="server" Text="58" Font-Bold="true" Style="text-shadow: 1px 2px 2px #F6F6F6;
                    margin-left: 3px; margin-right: 3px; float: left; margin-top: 11px; font-weight: 700;
                    font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; color: #707070"></asp:Label>
            <span style="text-shadow: 1px 2px 2px #F6F6F6; margin-top: 11px; float: left; font-weight: 700;
                font-family: Raleway, Arial, Sans-Serif; font-size: 10pt; color: rgb(133, 133, 133);
                font-weight: bold;"> eBook/s</span>
            <asp:Button ID="RemoveSubButtonbot" ClientIDMode="Static" runat="server" Text="REMOVE FROM SUBSCRIPTION"
                OnClick="RemoveFromSubscriptionBottom_Click" CssClass="disabledremovetosubbtn" />
            <%--<asp:Button ID="RemoveSubButtonbot" ClientIDMode="Static" runat="server" Text="REMOVE FROM SUBSCRIPTION"
                Style="padding-bottom: 0px; padding-top: 0px; padding-left: 18px; padding-right: 18px;
                cursor: pointer; float: right; background: -moz-linear-gradient(center top , white 0%, #D308AF 5%, #A8066A 130%) repeat scroll 0 0 transparent;
                background: -webkit-gradient(linear, left top, left bottom, from(#D308AF), to(#A8066A));
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#D308AF', endColorstr='#A8066A', gradientType='0');
                margin-top: 6px; height: 25px; width: 195px; margin-right: 18px; font-family: Raleway, Arial, sans-serif;
                font-size: 8pt; border: 1px solid #A8066A; color: White; -moz-border-radius: 3px;
                -webkit-border-radius: 3px; border-radius: 3px; -khtml-border-radius: 3px; text-shadow: 1px 2px 2px #722993;"  OnClick="RemoveFromSubscriptionBottom_Click" />--%>
        </div>
    </div>
</div>
