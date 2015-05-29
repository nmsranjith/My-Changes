<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookList.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Books.Books.BookList" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<style type="text/css">
    .BooksSearchdiv .k-dropdown-wrap
    {
        background: #707070 !important;
        box-shadow: 1px 1px 7px -1px #707070;
        height: 26px !important;
        border: 1px solid #707070 !important;
    }
    .BooksImgClass
    {
        margin-top: 4px; 
        margin-left: 5px;                
    }    
    .BooksSearchdiv .k-dropdown-wrap .k-input
    {
        color: white !important;
    }
    .bookRowStyle
    {
        height: 150px;
        width: 100%;
        float: left;
        border: 1px solid lightgray;
        margin-bottom: 20px;
        background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, #fdfdfd 5%, #EAE9E9 130%);
        box-shadow: 1px 1px 5px lightgray;
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
        width: 94.4% !important;
    }
    .sliderDivProfIE
    {
        width: 96.3% !important;
        margin-left: 10px !important;
    }
    .sliderDivProfMoz
    {
        width: 97.1% !important;
    }
    .Booksearchtxt:hover
    {
        height: 30px;
        border: 0px solid transparent !important;
        padding-left: 7.5px !important;
        padding-top: 3px !important;
        background: transparent !important;
        width: 320px !important;
        box-shadow: none !important;
        font-size: 10pt !important;
    }
    .Booksearchtxt
    {
        border: 0px solid transparent !important;
        width: 320px !important;
        height: 30px !important;
        float: left;
        padding-left: 7.5px !important;
        padding-top: 3px !important;
        background: transparent !important;
        box-shadow: none !important;
        font-size: 10pt !important;
    }
    .BooksSearchdiv ::-webkit-input-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
        font-size: 10pt;
        color: #707070;
    }
    .BooksSearchdiv ::-moz-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
        opacity: 1;
        font-size: 10pt;
        color: #707070;
    }
    .BooksSearchdiv ::-ms-input-placeholder
    {
        font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
        font-size: 10pt;
        color: #707070;
    }
    
    .BooksSearchdiv .k-icon
    {
        background: url('Portals/0/images/arrow_prim.png') no-repeat 0px 0px !important;
        margin-top: 3px !important;
        background-position: 0px 0px !important;
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
        margin-left:0px !important;
    }
    #RightNavigationContainer
    {
        width:12.8% !important;
    }
    .eCollectionContentStyle ,.eCollectionMenuStyle
    {
        padding-bottom:40px;
    }
</style>
<script type="text/javascript">
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
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
            if (isGracePeriod == 'False') {
                $('[id$=RemovetosubsButtontop]').attr('disabled', false);
                $('[id$=RemoveSubButtonbot]').attr('disabled', false);
            }
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
            $('[id$=RemovetosubsButtontop]').attr('disabled', true);
            $('[id$=RemoveSubButtonbot]').attr('disabled', true);
        }
    }
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    function PostBack() {

        var searchAutocomplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutocomplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Books/BooksHandler.ashx?BooksStatus=booksAutoComplete'),
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        }
                    }
                },
                filter: "contains",
                separator: ", ",
                minLength: 1,
                placeholder: "Enter your search here ..."
            });
        }

        pageName = 'Profile';       

        $('#eCollectionContent')[0].style.height = "";
        $("#eCollectionContent").height($("#eCollectionContent").children()[0].offsetHeight);
        $('#eCollectionMenu').height((jQuery('#eCollectionContent').height()) + 'px');
        $("#<%=CategoriesDrpList.ClientID %>").kendoDropDownList();
        $("#<%=CategoriesDrpList.ClientID %>").parent().show();
        //        var displayKendo = $("#CategoriesDrpList").data("kendoDropDownList");
        //        displayKendo.show();
        $("#level").css({ "color": "#1FB5E7" });
        // $("#slider-range").css({ "display": "inline-block", "margin-left": "13px", "margin-top": "20px" });
        $("#bookslistsliderDiv").addClass("divgradient");
        //$("#dialog-message").css({ 'display': 'block' });

        $("#dialog-message").dialog({
            modal: true,
            autoOpen: true,
            show: "blind",
            hide: "explode",
            buttons: {
                "Continue": function () {
                    if ($("#duringradio").is(":checked")) {

                    }
                    else {
                        $(this).dialog("close");
                    }
                }
            }
        });

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            
        });
    }
    
   
</script>
<div class="BooksDivset">
    <asp:Button ID="Duringgraceperiod" runat="server" ClientIDMode="Static" Style="display: none"
        OnClick="Duringgraceperiod_Click" />
        <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
            <div class="ProgressInnerDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg" alt="Processing" /> 
            </div>
        </div>
    <div id="BookSearchdiv" class="BooksSearchdiv">
        <asp:UpdatePanel ID="BookUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="SearchTextBox" ClientIDMode="Static" runat="server" CssClass="Booksearchtxt"></asp:TextBox>
                <asp:DropDownList ID="CategoriesDrpList" ClientIDMode="Static" runat="server" Style="float: left;
                             font-family: Raleway, Arial, Sans-Serif; font-size: 9pt;width:200px; margin-left:25px;
                            color: #707070; margin-top: 4px;" OnSelectedIndexChanged="itemSelected" AutoPostBack="true"
                    TabIndex="0">
                    <asp:ListItem Text="TEXT TYPE"></asp:ListItem>
                    <asp:ListItem Text="GUIDED READING LEVEL"></asp:ListItem>
                    <asp:ListItem Text="READING AGE"></asp:ListItem>
                    <asp:ListItem Text="CHARACTER FAMILY"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="SEARCH" OnClientClick="ShowUpdate()" 
                    Style="color: White; float: right; width: 88px; font-family: Raleway ExtraBold, Arial, Sans-Serif;
                    text-shadow: 1px 2px 2px #4D7B18; font-size: 10pt; height: 33px; margin-top: 3px;
                    margin-right: 3px; cursor: pointer; box-shadow: 1px 2px lightgray; border-radius: 3px;
                    background: -moz-linear-gradient(center top , white 0%, #8EBC5A 5%, #609624 130%) repeat scroll 0 0 transparent;
                    background: -webkit-gradient(linear, left top, left bottom, from(#8EBC5A), to(#609624));
                    background: -ms-linear-gradient(top, #8EBC5A 5%, #609624 130%); filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#8EBC5A', endColorstr='#609624', gradientType='0');" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoriesDrpList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>   
    <div style="width: 91%; margin-top: 10px; margin-left: 25px; padding: 15px 0px 65px 15px;
        float: left; border: 1px solid #CCC;">
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
    <asp:UpdatePanel ID="MessageDivUpdatePanel" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
                <div  class="bubble" >
                    <asp:Label ID="Message1" runat="server" Text="" />                        
                </div>
            </div> 
        </ContentTemplate> 
    </asp:UpdatePanel> 
    <div class="BooksContentDiv" id="BooksDiv">
        <asp:UpdatePanel ID="BooksUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                   <asp:Button ID="hiddenbtn" ClientIDMode="Static" Style="display: none" runat="server"
                    OnClick="hiddenbtn_Click" />
                <asp:Repeater ID="CollectionRepeater" runat="server"  OnItemDataBound="CollectionRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div id="SelectedBookRepdiv" class='<%# Eval("ClassName") %>'>
                            <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 68px;min-width: 57px;">
                                <input type="checkbox" id="BooksCheckBox" runat="server" checked='<%# Eval("Checked") %>'
                                    style="float: left; margin-left: 24px; display: none;" />
                                <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Style="display: none" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                                <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("IMAGE_FILE_NAME") %>'></asp:Label>
                                <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left; margin-left: 24px;
                                    width: 20px; cursor: pointer;" onclick="javascript:CheckBoxImgClick(this);" src='<%# Page.ResolveUrl(Eval("CheckImgPathName").ToString()) %>' />
                            </div>
                            <div class="DashBoard_Items_books">
                                <asp:Image runat="server" ID="BookCoverImage" ClientIDMode="Static" CssClass ="DashBoard_Items_books_images BooksImgClass" ImageUrl='<%# Eval("IMAGE_FILE_NAME")%>'/>
                            </div>
                            <div style="width: 61%; float: right; margin-top: 25px;">
                                <div style="width: 100%; float: left">
                                    <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text='<%# Eval("Title") %>'></asp:Label>
                                    <%--  <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">Years</span>
                                    <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; padding: 5px;
                                        font-family: Raleway-regular,Raleway, arial, sans-serif; font-weight: 700;" runat="server"
                                        Text="5"></asp:Label>
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
                                    <asp:Label ID="ReadingLevelLabel" runat="server" Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,
                                    <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label></br>
                                    <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>' Style="float:left;margin-top: 5px;"></asp:Label>
                                    <asp:Label ID="YearLabel" runat="server" Style="float:left;margin-top:5px ;width:100%" Text='<%#  string.Format("Copyright: {0}", Eval("COPYRIGHT_YEAR")) %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <CP:CustomPaging ID="CustomPaging" runat="server">
                </CP:CustomPaging>
                <%--<center id="PagerHolder" class="Pager" style="min-width: 90%">
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
                </center>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<script type="text/javascript" >
    $('#<%=SearchButton.ClientID %>').removeAttr('disabled');
</script>
