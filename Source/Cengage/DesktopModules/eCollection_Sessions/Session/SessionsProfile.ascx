<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SessionsProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.SessionsProfile" %>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Sessions/Scripts/jquery.jplayer.min.js")%>"
    type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Sessions/jplayer.css")%>"
    rel="Stylesheet" type="text/css" />
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Sessions/jplayer.blue.monday.css")%>"
    rel="Stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Sessions/Scripts/jplayer.playerlist.min.js")%>"
    type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Students/CSS/jQuery.ui.smoothness.css")%>"
    rel="Stylesheet" type="text/css" />
<%@ Register Src="~/Controls/eCollectionControls/ReadingHistory.ascx" TagName="HistoryDiv"
    TagPrefix="HD" %>

    <%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="MS" %>
    <Ms:Messages ID="Messages" runat="server"></Ms:Messages>
<style type="text/css">

.ui-datepicker
{
background-color: inherit;
}
.bubble
{
    top:13px !important;
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
.ui-datepicker-trigger
{
	margin-top: 2px;
	float: right;
}
    .BooksImgClass
    {
        margin-top: 0px; 
        margin-left: 15px;                
    }   
    .k-window-action
    {
        /*background-image: url('Portals/0/images/close.png') !important;*/
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    #StudentDiv table tr
    {
        -moz-border-radius: 10px;
        -webkit-border-radius: 10px;
        border-radius: 10px;
        -khtml-border-radius: 10px;
        background-image: url('Portals/0/images/Tbrowimg.png');
        background-repeat: repeat;
    }
    .tdimg
    {
        background-image: url('Portals/0/images/Tbrowlongimg.png');
        background-repeat: repeat;
    }
    .divcircle
    {
        width: 60px;
        height: 60px;
        -moz-border-radius: 5em;
        -webkit-border-radius: 5em;
        border-radius: 5em;
        -khtml-border-radius: 5em;
    }
    #HistoryAccordion a
    {
        background-color: Silver !important;
    }
    #HistoryAccordion
    {
        background-color: Silver;
    }
    .RecordingMax
    {
        background-image: url('Portals/0/images/RecordingsButton.png');
        background-position: 126px 27px;
        width: 125px;
        height: 27px;
        border: 0px;
        cursor: pointer;
    }
    .RecordingMin
    {
        background-image: url('Portals/0/images/minimizeButton.png');
        background-position: 126px 27px;
        width: 125px;
        height: 27px;
        border: 0px;
        cursor: pointer;
    }
    .k-dropdown-wrap
    {
        background-color: white !important;
        background-image: url('Portals/0/images/Levelbg.png') !important;
        background-repeat: repeat !important;
        box-shadow: 1px 2px lightgray;
    }
    #DropdownDiv .k-dropdown-wrap .k-input
    {
        height: 1.3em !important;
        margin-top: 2px !important;
        text-indent: 0px !important;
        margin-left: 15px;
    }
    .k-dropdown-wrap .k-select
    {
        margin-top: 2px !important;
    }
    .k-dropdown-wrap .k-state-hover
    {
        background-color: white !important;
    }
    
    #DropdownDiv .k-i-arrow-s
    {
        background-position: -129px -17px !important;
    }
    
    #DropdownDiv .k-icon
    {
        background-image: url('Portals/0/images/ui-icons_cccccc_256x240.png') !important;
    }
    
    .AudioPlayBack
    {
        box-shadow: 0px -1px 1px gray;
        padding: 12px;
        border: 1px solid lightgray;
        background-color: white;
        float: left;
        width: 96%;
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
    }
    
</style>
<div id="FirstDiv" class="StudentProfile_MainDiv">
    <div style="width: 100%; height: 126px; float: left; margin: 0px 10px 10px 0px; border: 1px solid lightgrey;
        background: -moz-linear-gradient(center top , white 1%, #FAFDFD 5%, #E5F6FE 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FAFDFD), to(#E5F6FE));
        background: -ms-linear-gradient(top, #FDFDFD 5%, #E5F6FE 130%) !important; " class="StudentProfile_Sub_Div" >
        <div style="float: left; width: 100%">
            <div style="float: left; height: 127px; width: 37%; margin: 15px 0px 5px 15px;">
                <h2 id="SessionName" runat="server" clientidmode="static" style="font-family: Raleway-regular,Raleway,Arial;
                    font-weight: 500; color: #707070; font-size: 17.5pt; margin-top: 10px !important;">
                </h2>
            </div>
            <div style="float: left; width: 29%; margin: 36px 0px 5px 14px;">
                <div class="H6Heading">
                    SENT:
                    <asp:Label ID="lblSessionCreatedDate" runat="server"></asp:Label>
                </div>
            </div>
            <div style="float: left; width: 7%; margin: 30px 0px 5px 92px;">
                <%--<img id="editSession" src="../Portals/0/images/editsession.png" height="30px" width="30px">--%>
                <asp:Button ID="imgEditSession" Visible="false" runat="server" CssClass="EditSessionImage"
                    OnClick="imgEditSession_Click" />
            </div>
            <div style="float: left; width: 7%; margin: 30px 0px 5px 0px;">
                <asp:Button ID="imgEndSession" Visible="false" ClientIDMode="static" runat="server"
                    CssClass="EndSessionImage" OnClick="imgEndSession_Click" />
            </div>
        </div>
        <div style="float: left; width: 96%; height: 0.1px; margin: -75px 0px 0px 15px; border: 1px solid lightgrey;
            border-color: #D4D4D4">
        </div>
        <div style="width: 96%; float: left; margin: -56px 0px 5px 15px;" class="H6Heading">
            <div style="float: left">
                <%--<span id="ReadingLevelLabel" style="font-weight: bolder; font-size: 9pt !important;">
                    This session will end:</span>--%>
                <h4 style="font-size: 10pt !important;">
                    This session will end:</h4>
            </div>
            <div style="float: left; margin-top: -6px; margin-left: 15px" class="H6Heading">
                <div class="CalendarDiv">
                    <%--  <select id="SessionDropDownList" runat="server" style="height: 40px; float: left;
                        margin-left: 134px; position: absolute; margin-top: -36px; width: 130px;">
                        <option value="Next Week">Next week</option>
                        <option value="Today">Today</option>
                    </select>--%>
                    <%-- <asp:DropDownList ID="SessionDropDownList" ClientIDMode="Static" runat="server" style="height: 30px; float: left;
                        margin-left: 134px; position: absolute; margin-top: -36px; width: 130px;" AutoPostBack="True"  OnSelectedIndexChanged="itemSelected">
                    <asp:ListItem Value="Next Week">Next Week</asp:ListItem>                    
                    <asp:ListItem Value="Today">Today</asp:ListItem>                    
                    </asp:DropDownList>--%>
                    <asp:HiddenField ID="DOBHdFld" runat="server" ClientIDMode="Static" />
                    <input type="text" id="DateofBirthTextBox" name="IpadDate" style="font-style: italic;
                            width: 86%; height: 21px;" />
                   <%-- <input type="text" id="DateofBirthTextBox" readonly="readonly" placeholder="Session End Date"
                        autocomplete="off" runat="server" clientidmode="Static" class="" style="font-style: italic;
                        width: 91%; height: 21px;" />--%>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 100%; min-height: 73px; float: left; margin: 0px 10px 10px 0px;">
        <div style="width: 48%; float: left; margin-top: 15px; border: 1px solid lightgrey;">
            <div style="margin: 15px 0px 0px 15px;">
                <h5>
                    OPENED</h5>
            </div>
            <div style="float: left; width: 90%; height: 0.1px; margin: 0px 0px 0px 15px; border: 1px solid lightgrey;
                border-color: #D4D4D4">
            </div>
            <div style="float: left; width: 91%; padding: 15px">
                <asp:Repeater ID="repOpenedStudents" runat="server">
                    <HeaderTemplate>
                        <table border="0" width="100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <h4 style="color: #2BACF2; font-weight: bold;">
                                    <%#(Eval("Value").ToString().Length>=42 ? Eval("Value").ToString().Substring(0,42) :  Eval("Value").ToString()) %></h4>
                            </td>
                            <td>
                                <h4>
                                    <i>
                                        <%#Convert.ToDateTime(Eval("Key")).ToString("dd/MM/yyyy")%></i></h4>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div style="width: 48%; float: right; margin-top: 15px; border: 1px solid lightgrey;
            margin-left: 22px; margin-right: -2px;">
            <div style="margin: 15px 0px 0px 15px;">
                <h5>
                    UNOPENED</h5>
            </div>
            <div style="float: left; width: 90%; height: 0.1px; margin: 0px 0px 0px 15px; border: 1px solid lightgrey;
                border-color: #D4D4D4">
            </div>
            <div style="float: left; width: 91%;padding: 15px">
                <asp:Repeater ID="repUnOpenedStudents" runat="server">
                    <HeaderTemplate>
                        <table border="0" width="100%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <h4 style="color: #2BACF2; font-weight: bold;">
                                    <%# (Container.DataItem.ToString().Length >= 42 ? Container.DataItem.ToString().Substring(0, 42) : Container.DataItem.ToString())%></h4>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div style="width: 100%; margin-top: 15px; float: left; min-height: 72px; border: 1px solid lightgrey;
        margin-left: 0px; background: -moz-linear-gradient(center top , white 0%, #FEFCF7 1%, #F9EFD1 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FEFCF7), to(#F9EFD1));
        background: -ms-linear-gradient(top, #FEFCF7 5%, #F9EFD1 130%) !important;" class="StudentProfile_Sub_Div2" filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#FEFCF7', endColorstr='#F9EFD1', gradientType='0');">
        <div style="float: left; width: 96%; margin: 13px 0px 0px 15px; border: 0px solid lightGrey;
            overflow: hidden;">
            <asp:Label runat="server" ID="txtNotes"> </asp:Label>
        </div>
    </div>
    <div style="width: 100%; float: left; margin: 3px 0px 0px 0px; margin-top: 15px">
        <asp:Repeater ID="BooksRepeater" runat="server" OnItemDataBound ="BooksRepeater_ItemDataBound" >
            <ItemTemplate>
                <div id="SelectedBookRepdiv" style="height: 143px; width: 100%; float: left; border: 1px solid lightgray;
                    margin-bottom: 20px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
                    background: -ms-linear-gradient(top, #FDFDFD 5%, #EAE9E9 130%) !important; filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
                    box-shadow: 1px 1px 5px lightgray;">
                    <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 58px;">
                        <%-- <input type="checkbox" id="BooksCheckBox" runat="server" style="float: left; margin-left: 24px;
                                    display: none;" />--%>
                        <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Visible="false" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                        <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("ImageFileName") %>'></asp:Label>
                        <%-- <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left; margin-left: 24px;
                                    width: 20px;" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />--%>
                    </div>
                    <div class="DashBoard_Items_books">
                        <asp:Image  CssClass="DashBoard_Items_books_images BooksImgClass" ID="BookCoverImage" runat="server" ClientIDMode="Static"  
                        ImageUrl='<%# Eval("ImageFileName") %>' />
                    </div>
                    <div style="width: 68%; float: right; margin-top: 20px;">
                        <div style="width: 100%; float: left">
                            <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text='<%# Eval("Title") %>'></asp:Label>
                            <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                font-weight: 700;"></span>
                            <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; padding: 5px;
                                font-family: Raleway-regular,Raleway, arial, sans-serif; font-weight: 700;" runat="server"></asp:Label>
                            <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                font-weight: 700;"></span>
                            <asp:Label ID="ToYearLabel" runat="server" Style="padding: 5px; color: #707070; font-size: 12pt;
                                font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;"></asp:Label>
                            <br />
                            <asp:Label ID="AuthorNameLabel" runat="server" Style="font-family: Arial-Regular, Sans-Serif;
                                font-size: 10pt; color: #707070; margin-top: 5px; float: left;" Text='<%# Eval("PreferredName") %>'></asp:Label>
                        </div>
                        <div style="font-family: Arial-Regular, Sans-Serif; width: 100%; float: left; margin-top: 5px;
                            font-size: 10pt; color: #707070;">
                            <asp:Label ID="ColourLabel" runat="server" Text='<%# Eval("ColourLevel") %>'></asp:Label>,
                            <asp:Label ID="ReadingLevelLabel" runat="server" Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,                            
                            <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label><br />
                            <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>' Style="float:left;margin-top: 5px;"></asp:Label>
                            <asp:Label ID="YearLabel" runat="server" Style="float:left;margin-top:5px ;width:100%" Text='<%#  string.Format("Copyright: {0}", Eval("CopyRightYear")) %>'></asp:Label>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%-- <asp:Repeater ID="CollectionRepeater" runat="server">
            <ItemTemplate>
                <div id="CollectionRepdiv" style="height: 130px; width: 100%; float: left; border: 1px solid lightgray;
                    margin-bottom: 20px; background: -moz-linear-gradient(center top , white 0%, #fdfdfd 1%, #DBD9D9 130%) repeat scroll 0 0 transparent;
                    background: -webkit-gradient(linear, left top, left bottom, from(#fdfdfd), to(#EAE9E9));
					background:-ms-linear-gradient(top, #FDFDFD 5%, #EAE9E9 130%) !important;
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
                    box-shadow: 1px 1px 5px lightgray;">
                    <div style="float: left; margin-top: 10px; margin-left: 30px;">
                        <img id="BookCoverImage" src="<%=Page.ResolveUrl("Portals/0/images/TheLittleBlueHorse.png")%>" />
                    </div>
                    <div style="width: 68%; float: right; margin-top: 20px;">
                        <div style="width: 100%; float: left">
                            <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                font-family: Raleway, Arial, Sans-Serif; font-weight: 600;" Text="Big Book of Word Problems:"></asp:Label>
                            <span style="color: #707070; font-size: 12pt; font-family: Raleway, Arial, Sans-Serif;">
                                Years</span>
                            <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; font-family: Raleway, Arial, Sans-Serif;"
                                runat="server" Text="5"></asp:Label>
                            <span style="color: #707070; font-size: 12pt; font-family: Raleway, Arial, Sans-Serif;">
                                and</span>
                            <asp:Label ID="ToYearLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                font-family: Raleway, Arial, Sans-Serif;" Text="6"></asp:Label>
                            <br />
                            <asp:Label ID="AuthorNameLabel" runat="server" Style="color: #707070; margin-top: 5px;
                                font-family: Raleway, Arial, Sans-Serif; float: left;" Text="Author"></asp:Label>
                        </div>
                        <div style="font-family: Raleway, Arial, Sans-Serif; width: 100%; float: left; margin-top: 5px;
                            font-size: 10pt; color: #707070;">
                            <asp:Label ID="StudentLabel" runat="server" Text="Student" Style="font-style: italic"></asp:Label>
                            <asp:Label ID="ReadingLevelLabel" runat="server" Text="12"></asp:Label>,
                            <asp:Label ID="ColourLabel" runat="server" Text="yellow"></asp:Label>,
                            <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: 5"></asp:Label><br />
                            <asp:Label ID="BookLabel" runat="server" Style="margin-top: 5px; float: left; font-style: italic">Book: </asp:Label>
                            <asp:Label ID="PagesCountLabel" runat="server" Style="margin-top: 5px; float: left;margin-left:5px;"
                                Text=" 16 pages, "></asp:Label>
                            <asp:Label ID="YearLabel" runat="server" Style="margin-top: 5px; float: left;" Text="2011"></asp:Label>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
        <%--      <asp:GridView ID="SessionGridView" CssClass="RecordingsGridViewstyle" runat="server"
                            AutoGenerateColumns="false" ShowHeader="false" Width="100%" GridLines="None"  >
                            <RowStyle Width="100px" />
                            <Columns>               
                                <asp:TemplateField ItemStyle-Width="30%" ItemStyle-CssClass="Guided_CoverPaddingStyle" ItemStyle-BackColor="#F4F0F1">
                                    <ItemTemplate>
                                        <img ID="BookCoverImage" src="../Portals/0/images/TheLittleBlueHorse.png"
                                             Height="100px" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>                                                         
                                <asp:TemplateField ItemStyle-Width="40%" ItemStyle-CssClass="Guided_PaddingStyle" ItemStyle-BackColor="#F4F0F1">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" ForeColor="#4A79AD" Font-Bold="true" runat="server" Text='Big Book of Word Problems : Years 5 and 6'></asp:Label><br />
                                        <asp:Label ID="Label2" ForeColor="#4A79AD" Font-Bold="false" runat="server" Text='Author'></asp:Label><br />
                                        <asp:Label ID="Label3" ForeColor="Black" Font-Bold="false" runat="server" Text='Student:[Reading Level],[Color],[Reading Age:5]'></asp:Label><br />
                                        <asp:Label ID="Label5" ForeColor="Black" Font-Bold="false" runat="server" Text='Book:[16 Pages],[2012]'></asp:Label><br />        
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                
                            </Columns>                                                       
                        </asp:GridView>--%>
    </div>
    <asp:HiddenField ID="selectedSessionID" ClientIDMode="Static" runat="server" />
</div>
<div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none">
    <div class="bubble" >
        <asp:Label ID="Message1" runat="server" Text="" />            
    </div>
</div> 
<div id="SecondDiv" style="width: 100%; float: left;" runat="server" clientidmode="Static">
    <div id="HistoryDiv" style="display: block; margin-top: 15px">
        <div id="HistoryMainDiv" class="HistoryMainDiv">
            <div class="HistoryDiv" style="margin-top: -31px;" id="TodaysDiv" runat="server">
                <div id="TodaysRecordings" class="RecordingsHolder" >
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" id="TodaysHistToggle" onclick="toggle_visibility('TodaysRecordingsDiv','TodaysBorderDiv');">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv His_LabelDivDown">
                            <asp:Label ID="TodayLabel" runat="server" Text="  Today" />
                        </div>
                    </div>
                </div>
                <div id="TodaysBorderDiv">
                    <div id="TodaysRecordingsDiv" class="MyHistoryHolder_Content1" style="padding-top:0px!important ">
                        <asp:Repeater ID="TodaysIndependentRecordings" runat="server" OnItemDataBound="TodaysIndependentRecordings_ItemDataBound">
                            <ItemTemplate>
                                <div class="HistoryContentHolder" style="margin-top:15px">
                                    <div class="HistoryNodeImage">
                                        <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                    </div>
                                    <div class="HistoryContent">
                                        <div class="HistoryContent_SessionType">
                                            <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                                Style="margin-left: 8px;" ClientMode="Static" />
                                            <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                        </div>
                                        <div class="HistoryContent_BookCover">
                                            <asp:Image ID="BookCoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static" 
                                                CssClass="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="TodaysIndMemberID" runat="server" Text='<%#Eval("UserID")%>' Visible="false"></asp:Literal>
                                            <asp:Literal ID="TodaysSessionHistoryID" runat="server" Text='<%#Eval("SessionHistoryId")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'TodayIndeRecDiv<%# Container.ItemIndex%>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordBtnImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -30px;">
                                                    <%# Eval("WordCount")%>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book</div>
                                                <div class="Div_FullWidth">
                                                    opened:</div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 11pt; font-family: Raleway-regular,Raleway, Arial,Sans-Serif;
                                                        font-weight: 700;">
                                                        <%# Eval("BooksOpenedMin") %></b>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="TodayIndeRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                            <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                <div style="margin-bottom: 28px;">
                                                    <span class="PlayAll">Play all</span><img id="TodayGuidRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                        style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                    <div id="TodayIndRecjPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                        <div id="TodayIndeRecjpId<%# Container.ItemIndex %>" class="jp-jplayer">
                                                        </div>
                                                        <div id="TodayIndeRecjpId_container_1<%# Container.ItemIndex %>" class="jp-audio"
                                                            style="display: none; margin-left: 127px;">
                                                            <div class="jp-type-playlist">
                                                                <div class="jp-gui jp-interface" style="">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-previous" tabindex="1">previous</a></li>
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-next" tabindex="1">next</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="display: block;">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                    </div>
                                                                    <ul class="jp-toggles">
                                                                        <%--   <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                        <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                            shuffle off</a></li>
                                                                        <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                        <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                            repeat off</a></li>--%>
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-playlist" style="display: none">
                                                                    <ul style="display: block;">
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="TodayIndeRecjpId_container_2<%# Container.ItemIndex %>" class="jp-audio"
                                                            style="display: none; margin-left: 127px;">
                                                            <div class="jp-type-single">
                                                                <div class="jp-gui jp-interface" style="">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                        <ul class="jp-toggles">
                                                                            <%--   <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                                repeat</a></li>
                                                                            <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                                repeat off</a></li>--%>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="jp-title">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <%#Eval("RecordFilePath")%></li>
                                                                                    </ul>
                                                                                </div>--%>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="AudioPlayBack">
                                                    <asp:Repeater ID="TodayIndeVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;float:left;width:100%;">
                                                                <span id="IndeRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn" />
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;float:left;width:100%;">
                                                                <span id="IndeRecordingPathalt<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn" />
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv" id="YesterDayGuidedRecordingsDiv" runat="server">
                <div id="YesterDaysRecordings" class="RecordingsHolder">
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="YesterdayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" id="YestHistToggle" onclick="toggle_visibility('YesterdaysRecordingsDiv','YesterdaysBorderDiv');">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="YesterdayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv His_LabelDivUp">
                            <asp:Label ID="YesterdayLabel" runat="server" Text="Yesterday" /></div>
                    </div>
                </div>
                <div id="YesterdaysBorderDiv">
                    <div id="YesterdaysRecordingsDiv" class="MyHistoryHolder_Content">
                        <asp:Repeater ID="YesterDayIndependentRecordings" runat="server" OnItemDataBound="YesterDayIndependentRecordings_ItemDataBound">
                            <ItemTemplate>
                                <div class="HistoryContentHolder">
                                    <div class="HistoryNodeImage">
                                        <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                    </div>
                                    <div class="HistoryContent">
                                        <div class="HistoryContent_SessionType">
                                            <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                                Style="margin-left: 8px;" ClientMode="Static" />
                                            <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                        </div>
                                        <div class="HistoryContent_BookCover">
                                            <asp:Image  ID="BookCoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static" 
                                                CssClass="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="YesterdaysIndMemberID" runat="server" Text='<%#Eval("UserID")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Literal ID="TodaysSessionHistoryID" runat="server" Text='<%#Eval("SessionHistoryId")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'YesterdayIndeRecDiv<%# Container.ItemIndex%>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordBtnImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_big.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -30px;">
                                                    <%# Eval("WordCount")%>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book</div>
                                                <div class="Div_FullWidth">
                                                    opened:</div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 11pt; font-family: Raleway-regular,Raleway, Arial,Sans-Serif;
                                                        font-weight: 700;">
                                                        <%# Eval("BooksOpenedMin") %></b>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="YesterdayIndeRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                            <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                <div style="margin-bottom: 28px;">
                                                    <span class="PlayAll">Play all</span><img id="YesterdayIndRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                        style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                    <div id="YesterdayIndejPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                        <div id="YesterdayIndejplayer<%# Container.ItemIndex %>" class="jp-jplayer">
                                                        </div>
                                                        <div id="YesterdayIndejplayer_container1<%# Container.ItemIndex %>" class="jp-audio"
                                                            style="display: none; margin-left: 127px;">
                                                            <div class="jp-type-playlist">
                                                                <div class="jp-gui jp-interface">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-previous" tabindex="1">previous</a></li>
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-next" tabindex="1">next</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="display: block;">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                    </div>
                                                                    <ul class="jp-toggles">
                                                                        <%--  <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                        <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                            shuffle off</a></li>
                                                                        <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                        <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                            repeat off</a></li>--%>
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-playlist" style="display: none">
                                                                    <ul style="display: block;">
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="YesterdayIndejplayer_container2<%# Container.ItemIndex %>" class="jp-audio"
                                                            style="display: none; margin-left: 127px;">
                                                            <div class="jp-type-single">
                                                                <div class="jp-gui jp-interface" style="">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                        <ul class="jp-toggles">
                                                                            <%--<li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                                repeat</a></li>
                                                                            <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                                repeat off</a></li>--%>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="jp-title">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <%#Eval("RecordFilePath")%></li>
                                                                                    </ul>
                                                                                </div>--%>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="AudioPlayBack">
                                                    <asp:Repeater ID="YesterdayIndVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;float:left;width:100%;">
                                                                <span id="YesterdayIndRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn"/>
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;float:left;width:100%;">
                                                                <span id="YesterdayIndRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn" />
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv" id="Last7DaysGuidedRecordingsDiv" runat="server">
                <div id="LastSevenDaysRecordings" class="RecordingsHolder">
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" id="Last7DToggle" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv');">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv His_LabelDivUp">
                            <asp:Label ID="Last7DaysLabel" runat="server" Text="Last Seven Days" /></div>
                    </div>
                </div>
                <div id="Last7DaysBorderDiv">
                    <div id="Last7DaysRecDiv" class="MyHistoryHolder_Content">
                        <asp:Repeater ID="Last7DaysIndependentRecordings" runat="server" OnItemDataBound="Last7DaysIndependentRecordings_ItemDataBound">
                            <ItemTemplate>
                                <div class="HistoryContentHolder">
                                    <div class="HistoryNodeImage">
                                        <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                    </div>
                                    <div class="HistoryContent">
                                        <div class="HistoryContent_SessionType">
                                            <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                                Style="margin-left: 8px;" ClientMode="Static" />
                                            <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                        </div>
                                        <div class="HistoryContent_BookCover">
                                            <asp:Image ID="BookCoverImage" ImageUrl ='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"
                                                CssClass="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <%-- <p class="HistoryContent_DateTime">
                                                <%# Eval("SessionCreatedDate")%></p>--%>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="LastSevendaysIndMemberID" runat="server" Text='<%#Eval("UserID")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Literal ID="TodaysSessionHistoryID" runat="server" Text='<%#Eval("SessionHistoryId")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'LastSevenDaysIndependentRecDiv<%#Container.ItemIndex %>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordBtnImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_big.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -29px; margin-left: -1px;">
                                                    <%# Eval("WordCount")%>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book</div>
                                                <div class="Div_FullWidth">
                                                    opened:</div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 11pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                                        font-weight: 700;">
                                                        <%# Eval("BooksOpenedMin") %></b>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="LastSevenDaysIndependentRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                            <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                <div style="margin-bottom: 28px;">
                                                    <span class="PlayAll">Play all</span><img id="LastSevenDaysIndRecDivPlayAllButton"
                                                        onclick="PlayAllAudio(this)" style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                    <div id="LastSevenDaysIndjPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                        <div id="LastSevenDaysIndjplayer" class="jp-jplayer">
                                                        </div>
                                                        <div id="LastSevenDaysjplayer_container1<%# Container.ItemIndex %>" class="jp-audio"
                                                            style="display: none; margin-left: 127px;">
                                                            <div class="jp-type-playlist">
                                                                <div class="jp-gui jp-interface">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-previous" tabindex="1">previous</a></li>
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-next" tabindex="1">next</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="display: block;">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                    </div>
                                                                    <ul class="jp-toggles">
                                                                        <%-- <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                        <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                            shuffle off</a></li>
                                                                        <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                        <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                            repeat off</a></li>--%>
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-playlist" style="display: none">
                                                                    <ul style="display: block;">
                                                                    </ul>
                                                                </div>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="LastSevenDaysjplayer_container2" class="jp-audio" style="display: none;
                                                            margin-left: 127px;">
                                                            <div class="jp-type-single">
                                                                <div class="jp-gui jp-interface" style="">
                                                                    <ul class="jp-controls">
                                                                        <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                        <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                        <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                        <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="">mute</a></li>
                                                                        <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                            unmute</a></li>
                                                                        <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                    </ul>
                                                                    <div class="jp-progress">
                                                                        <div class="jp-seek-bar">
                                                                            <div class="jp-play-bar">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-volume-bar">
                                                                        <div class="jp-volume-bar-value">
                                                                        </div>
                                                                    </div>
                                                                    <div class="jp-time-holder">
                                                                        <div class="jp-current-time">
                                                                        </div>
                                                                        <div class="jp-duration">
                                                                        </div>
                                                                        <ul class="jp-toggles">
                                                                            <%-- <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                                repeat</a></li>
                                                                            <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                                repeat off</a></li>--%>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <%-- <div class="jp-title">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <%#Eval("RecordFilePath")%></li>
                                                                                    </ul>
                                                                                </div>--%>
                                                                <div class="jp-no-solution" style="display: none;">
                                                                    <span>Update Required</span> To play the media you will need to either update your
                                                                    browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                        target="_blank">Flash plugin</a>.
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="AudioPlayBack">
                                                    <asp:Repeater ID="LastSevenDaysIndVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;float:left;width:100%;">
                                                                <span id="LastSevenDaysIndRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudios(this);" class="AudPlayBtn" />
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;float:left;width:100%;">
                                                                <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudios(this);" class="AudPlayBtn"/>
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>'
                                                                    runat="server"></asp:Label>
                                                            </div>
                                                        </AlternatingItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv" id="RestMonthDiv" clientidmode="Static" runat="server">
                <asp:Repeater ID="RestMonthRepeater" runat="server" OnItemDataBound="RestMonthRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div id="RestoftheMonthRecordings" class="RecordingsHolder" style="margin-bottom:15px">
                            <div class="HistoryNodeDIv">
                                <asp:Image ID="RestNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                            </div>
                            <div class="TodayHistory_Name" id="RestMonth" onclick="toggle_visibility('RestRecordingsDiv<%# Container.ItemIndex %>','RestBorderDiv<%# Container.ItemIndex %>');">
                                <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                                    <asp:Image ID="RestCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                        CssClass="StudentDashBoard_GuidedImage" />
                                </div>
                                <div class="TodayHistory_LabelDiv His_LabelDivUp">
                                    <asp:Label ID="RestMonthLabel" runat="server" Text='<%# Container.DataItem %>' /></div>
                            </div>
                        </div>
                        <div id="RestBorderDiv<%# Container.ItemIndex %>">
                            <div id="RestRecordingsDiv<%# Container.ItemIndex %>" class="MyHistoryHolder_Content" style="padding-top:0px!important">
                                <asp:Repeater ID="RestIndependentHistory" runat="server" OnItemDataBound="RestIndependentHistory_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="HistoryContentHolder" style="margin-bottom:10px;">
                                            <div class="HistoryNodeImage">
                                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                            </div>
                                            <div class="HistoryContent">
                                                <div class="HistoryContent_SessionType">
                                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                                        Style="margin-left: 8px;" ClientMode="Static" />
                                                    <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                                </div>
                                                <div class="HistoryContent_BookCover">
                                                    <asp:Image  ID="BookCoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"  
                                                        CssClass="History_Books" />
                                                </div>
                                                <div class="HistoryContent_BookDetails">
                                                    <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                        <%# Eval("StudentNames")%></p>
                                                    <p class="HistoryContent_BookName">
                                                        <%# Eval("Title")%></p>
                                                    <%-- <p class="HistoryContent_DateTime">
                                                        <%# Eval("SessionCreatedDate")%></p>--%>
                                                    <p class="HistoryContent_DateTime">
                                                        <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                                    <asp:Literal ID="RestIndMemberID" runat="server" Text='<%#Eval("UserID")%>' Visible="false"></asp:Literal>
                                                    <asp:Literal ID="TodaysSessionHistoryID" runat="server" Text='<%#Eval("SessionHistoryId")%>'
                                                        Visible="false"></asp:Literal>
                                                        <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                    <div class="RecordingButtonGradient">
                                                        <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                            onclick="ShowRecordings(this,'RestIndenpRecDiv<%# ((RepeaterItem)Container.Parent.Parent).ItemIndex %><%#Container.ItemIndex %>')" />
                                                        <div class="RecordButtonRevBackground" onclick="javascript:RecordBtnImg(this)">
                                                        </div>
                                                    </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="HistoryContent_ReadDetails">
                                                    <div class="History_Book_ReadingDetails">
                                                        <div class="History_WordCountBG">
                                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' style="width: 28px;
                                                                margin-top: 10px;" alt="" class="WordCountBG" />
                                                        </div>
                                                        <div class="History_WordCount" style="margin-top: -30px;">
                                                            <%# Eval("WordCount")%>
                                                        </div>
                                                        <div class="History_MyWordsLabel">
                                                            My Words
                                                        </div>
                                                    </div>
                                                    <div class="History_ReadingTime">
                                                        <div class="Div_FullWidth">
                                                            Book</div>
                                                        <div class="Div_FullWidth">
                                                            opened:</div>
                                                        <div class="Div_FullWidth">
                                                            <b style="color: #707070; font-size: 11pt; font-family: Raleway-regular,Raleway, Arial,Sans-Serif;
                                                                font-weight: 700;">
                                                                <%# Eval("BooksOpenedMin") %></b>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="RestIndenpRecDiv<%# ((RepeaterItem)Container.Parent.Parent).ItemIndex %><%# Container.ItemIndex %>" class="RecordingsTable">
                                                    <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                        <div style="margin-bottom: 28px;">
                                                            <span class="PlayAll">Play all</span><img id="RestIndenpDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                                style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                            <div id="RestIndenpjPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                                <div id="RestIndenpjplayer<%# Container.ItemIndex %>" class="jp-jplayer">
                                                                </div>
                                                                <div id="RestIndenpjplayer_container1<%# Container.ItemIndex %>" class="jp-audio"
                                                                    style="display: none; margin-left: 127px;">
                                                                    <div class="jp-type-playlist">
                                                                        <div class="jp-gui jp-interface">
                                                                            <ul class="jp-controls">
                                                                                <li><a href="#" class="jp-previous" tabindex="1">previous</a></li>
                                                                                <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                                <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                                <li><a href="#" class="jp-next" tabindex="1">next</a></li>
                                                                                <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                                <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="display: block;">mute</a></li>
                                                                                <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                                    unmute</a></li>
                                                                                <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                            </ul>
                                                                            <div class="jp-progress">
                                                                                <div class="jp-seek-bar">
                                                                                    <div class="jp-play-bar">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="jp-volume-bar">
                                                                                <div class="jp-volume-bar-value">
                                                                                </div>
                                                                            </div>
                                                                            <div class="jp-time-holder">
                                                                                <div class="jp-current-time">
                                                                                </div>
                                                                                <div class="jp-duration">
                                                                                </div>
                                                                            </div>
                                                                            <ul class="jp-toggles">
                                                                                <%--<li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                                    <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                                        shuffle off</a></li>
                                                                                    <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                                    <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                                        repeat off</a></li>--%>
                                                                            </ul>
                                                                        </div>
                                                                        <div class="jp-playlist" style="display: none">
                                                                            <ul style="display: block;">
                                                                            </ul>
                                                                        </div>
                                                                        <div class="jp-no-solution" style="display: none;">
                                                                            <span>Update Required</span> To play the media you will need to either update your
                                                                            browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                                target="_blank">Flash plugin</a>.
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div id="RestIndenpjplayer_container2<%# Container.ItemIndex %>" class="jp-audio"
                                                                    style="display: none; margin-left: 127px;">
                                                                    <div class="jp-type-single">
                                                                        <div class="jp-gui jp-interface" style="">
                                                                            <ul class="jp-controls">
                                                                                <li><a href="#" class="jp-play" tabindex="1" style="display: block;">play</a></li>
                                                                                <li><a href="#" class="jp-pause" tabindex="1" style="display: none;">pause</a></li>
                                                                                <li><a href="#" class="jp-stop" tabindex="1">stop</a></li>
                                                                                <li><a href="#" class="jp-mute" tabindex="1" title="mute" style="">mute</a></li>
                                                                                <li><a href="#" class="jp-unmute" tabindex="1" title="unmute" style="display: none;">
                                                                                    unmute</a></li>
                                                                                <li><a href="#" class="jp-volume-max" tabindex="1" title="max volume" style="">max volume</a></li>
                                                                            </ul>
                                                                            <div class="jp-progress">
                                                                                <div class="jp-seek-bar">
                                                                                    <div class="jp-play-bar">
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="jp-volume-bar">
                                                                                <div class="jp-volume-bar-value">
                                                                                </div>
                                                                            </div>
                                                                            <div class="jp-time-holder">
                                                                                <div class="jp-current-time">
                                                                                </div>
                                                                                <div class="jp-duration">
                                                                                </div>
                                                                                <ul class="jp-toggles">
                                                                                    <%-- <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                                            repeat</a></li>
                                                                                        <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                                            repeat off</a></li>--%>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                        <%-- <div class="jp-title">
                                                                                    <ul>
                                                                                        <li>
                                                                                            <%#Eval("RecordFilePath")%></li>
                                                                                    </ul>
                                                                                </div>--%>
                                                                        <div class="jp-no-solution" style="display: none;">
                                                                            <span>Update Required</span> To play the media you will need to either update your
                                                                            browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/"
                                                                                target="_blank">Flash plugin</a>.
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="AudioPlayBack">
                                                            <asp:Repeater ID="RestIndenpVideoRepeater" runat="server">
                                                                <ItemTemplate>
                                                                    <div style="background-color: White;float:left;width:100%;">
                                                                        <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                            <%#Eval("RecordFilePath")%></span>
                                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn" />
                                                                        <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                            class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <div style="background-color: lightgray;float:left;width:100%;">
                                                                        <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                            <%#Eval("RecordFilePath")%></span>
                                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudPlayBtn" />
                                                                        <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                            class="PageCountLabel pgNameAdj" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                                    </div>
                                                                </AlternatingItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>                               
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="LastNodeDiv">
        <asp:Image ID="LastMonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
    </div>
</div>
<%--<div id="Delete-message" title="Alert message!" style="display: none; background: white !important;">
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
            <span id="MessageLiteralEnd" runat="server" style="font-family: Raleway-regular,Raleway, Arial, sans-serif;
                font-size: 10pt; color: #707070; padding: 23px; float: left;">Do you want to end?</span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
            <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
        </div>
    </div>
</div>--%>
<script type="text/javascript" language="javascript">
    function changeGroupsStyle() {
        jQuery("#GroupsTab").css("background-color", "#03394D");
    }
    function RecordBtnImg(e) {
        e.parentNode.children[0].click();
    }
    function increment() {
        var inc = parseInt(jQuery('#ReadingLevelLabel').text());
        if (inc != 30)
            jQuery('#ReadingLevelLabel').text(inc + 1);
    }
    function decrement() {
        var dec = parseInt(jQuery('#ReadingLevelLabel').text());
        if (dec != 1)
            jQuery('#ReadingLevelLabel').text(dec - 1);
    }

    jQuery(function () {
        //        $(".RecordButtonRevBackground").click(function () { $("#RecordingsButton").click(); });

        $('#DateofBirthTextBox').val($('#DOBHdFld').val());

    });

    var dkwindow;
    var deleteFlag;
    $("#imgEndSession").click(function () {
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
        //document.getElementById("selectedSessionID").value = "";

        //        for (var i = 0; i < $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#ClassRepeaterDiv #ClassRepeaterContentDiv #RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }
        //        for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
        //            if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
        //                document.getElementById("selectedSessionID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
        //            }
        //        }
        deleteFlag = true;
        $("#imgEndSession").click();
        return false;
    });
    $("#NoButton").click(function () {
        dkwindow.data("kendoWindow").close();
        return false;
    });


</script>
<script type="text/javascript">
    function RecordButtonImg(e) {
        e.parentNode.children[0].click();
    }
    function increment() {
        var inc = parseInt(jQuery('#ReadingLevelLabel').text());
        if (inc != 30)
            jQuery('#ReadingLevelLabel').text(inc + 1);
    }
    function decrement() {
        var dec = parseInt(jQuery('#ReadingLevelLabel').text());
        if (dec != 1)
            jQuery('#ReadingLevelLabel').text(dec - 1);
    }

    function hidetab() {

        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() +150) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 150) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function toggle_visibility(ContentDivId, BorderDivId) {
        var e = document.getElementById(ContentDivId);
        var d = document.getElementById(BorderDivId);
        if ($(e).is(':visible')) {
            $(e).fadeToggle("slow", "linear");
            $('#' + BorderDivId).prev().children().next().children().next().removeClass('His_LabelDivDown').addClass('His_LabelDivUp');
            setTimeout("hidetab()", 700);
        }
        else {
            $(e).fadeToggle("slow", "linear");
            $('#' + BorderDivId).prev().children().next().children().next().removeClass('His_LabelDivUp').addClass('His_LabelDivDown');
            showtab();
        }


    }
    jQuery(function () {
      
        jQuery('#DateofBirthTextBox').change(
        function () {
            jQuery('#DOBHdFld').val(jQuery('#DateofBirthTextBox').val());
        });
        if (jQuery($("[id$=StyleApplier]")).val() != "Empty") {

        }
        jQuery('#level').hide();
        jQuery('#amount').hide();

    });

    function ShowRecordings(id, ContentDivId) {
        if (id.value == "RECORDINGS") {
            jQuery(id.parentNode.nextSibling).hide();
            //id.parentNode.style.width = "60%";
            jQuery("#" + ContentDivId).css("display", "block");
            id.value = "MINIMIZE";
            jQuery(id.parentNode.children[1]).addClass("sortimage");
            showtab();
        }
        else {
            jQuery(id.parentNode.nextSibling).show();
            jQuery(id.parentNode.children[1]).removeClass("sortimage");
            jQuery("#" + ContentDivId).css("display", "none");
            id.value = "RECORDINGS";
            hidetab();
        }


    }
    function PlayAudio(id) {
        var path = GetFile("/Portals/0/audio/page/");
        if (id.parentNode.children[0].innerHTML.trim() != "")
            path = path + id.parentNode.children[0].innerHTML.trim();
        //        if ($.browser.msie) {
        //            window.open(path);
        //            return;
        //        }

        id.parentNode.parentNode.parentNode.children[0].children[2].style.display = "block";
        id.parentNode.parentNode.parentNode.children[0].children[2].children[1].style.display = "none";
        id.parentNode.parentNode.parentNode.children[0].children[2].children[2].style.display = "block";
        var jid = id.parentNode.parentNode.parentNode.children[0].children[2].children[2].id;
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer("stop");
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer("destroy");
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer({
            ready: function () {
                $(this).jPlayer("setMedia", {
                    m4a: path
                }).jPlayer("play");
            },
            swfPath: GetFile("/Portals/0/Jplayer.swf"), wmode: "window", solution: "html,flash", supplied: 'm4a', preload: 'metadata', volume: 0.8, muted: false, backgroundColor: '#000000', cssSelectorAncestor: '#' + jid, cssSelector: { videoPlay: '.jp-video-play', play: '.jp-play', pause: '.jp-pause', stop: '.jp-stop', seekBar: '.jp-seek-bar', playBar: '.jp-play-bar', mute: '.jp-mute', unmute: '.jp-unmute', volumeBar: '.jp-volume-bar', volumeBarValue: '.jp-volume-bar-value', volumeMax: '.jp-volume-max', currentTime: '.jp-current-time', duration: '.jp-duration', fullScreen: '.jp-full-screen', restoreScreen: '.jp-restore-screen', repeat: '.jp-repeat', repeatOff: '.jp-repeat-off', gui: '.jp-gui', noSolution: '.jp-no-solution'
            },
            errorAlerts: false,
            warningAlerts: false
        }).bind($.jPlayer.event.play, function () { // pause other instances of player when current one play
            $(this).jPlayer("pauseOthers");
        });

        if (id.parentNode.parentNode.parentNode.children[0].children[2].style.display == "none")
            id.parentNode.parentNode.parentNode.children[0].children[2].style.display = "block";
        showtab();
    }
    function PlayAudios(id) {
        var path = GetFile("/Portals/0/audio/page/");
        if (id.parentNode.children[0].innerHTML.trim() != "")
            path = path + id.parentNode.children[0].innerHTML.trim();
        //        if ($.browser.msie) {
        //            window.open(path);
        //            return;
        //        }

        id.parentNode.parentNode.parentNode.children[0].children[2].style.display = "block";
        id.parentNode.parentNode.parentNode.children[0].children[2].children[1].style.display = "none";
        id.parentNode.parentNode.parentNode.children[0].children[2].children[2].style.display = "block";
        var jid = id.parentNode.parentNode.parentNode.children[0].children[2].children[2].id;
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer("stop");
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer("destroy");
        $("#" + id.parentNode.parentNode.parentNode.children[0].children[2].children[0].id).jPlayer({
            ready: function () {
                $(this).jPlayer("setMedia", {
                    m4a: path
                }).jPlayer("play");
            },
            swfPath: GetFile("/Portals/0/Jplayer.swf"), wmode: "window", solution: "html,flash", supplied: 'm4a', preload: 'metadata', volume: 0.8, muted: false, backgroundColor: '#000000', cssSelectorAncestor: '#' + jid, cssSelector: { videoPlay: '.jp-video-play', play: '.jp-play', pause: '.jp-pause', stop: '.jp-stop', seekBar: '.jp-seek-bar', playBar: '.jp-play-bar', mute: '.jp-mute', unmute: '.jp-unmute', volumeBar: '.jp-volume-bar', volumeBarValue: '.jp-volume-bar-value', volumeMax: '.jp-volume-max', currentTime: '.jp-current-time', duration: '.jp-duration', fullScreen: '.jp-full-screen', restoreScreen: '.jp-restore-screen', repeat: '.jp-repeat', repeatOff: '.jp-repeat-off', gui: '.jp-gui', noSolution: '.jp-no-solution'
            },
            errorAlerts: false,
            warningAlerts: false
        }).bind($.jPlayer.event.play, function () { // pause other instances of player when current one play
            $(this).jPlayer("pauseOthers");
        });

        if (id.parentNode.parentNode.parentNode.children[0].children[2].style.display == "none")
            id.parentNode.parentNode.parentNode.children[0].children[2].style.display = "block";
        showtab();
    }

    function PlayAllAudio(id) {
        id.parentNode.children[2].style.display = "block";
        id.parentNode.children[2].children[1].style.display = "block";
        id.parentNode.children[2].children[2].style.display = "none";
        var path = GetFile("/Portals/0/audio/page/");
        var jsonObj = [];
        var len = id.parentNode.parentNode.parentNode.children[0].children[1].children.length;
        var l = id.parentNode.parentNode.parentNode.children[0].children[1].children;
        var jplayerid = id.parentNode.children[2].children[0].id;
        var jplayerCont = id.parentNode.children[2].children[1].id;
        $("#" + jplayerid).jPlayer("stop");
        $("#" + jplayerid).jPlayer("destroy");
        var m4at = "m4a";
        for (var t = 0; t < len; t++) {
            var id = path + l[t].children[0].innerHTML.trim();
            item = {}
            item[m4at] = id;
            jsonObj.push(item);
        }
        var jsonString = JSON.stringify(jsonObj);
        new jPlayerPlaylist({
            jPlayer: "#" + jplayerid,
            cssSelectorAncestor: "#" + jplayerCont

        }, jsonObj, {
            swfPath: "../desktopmodules/ecollection_Sessions/scripts/Jplayer.swf",
            supplied: "m4a",
            wmode: "window",
            errorAlerts: false,
            playlistOptions: {
                autoPlay: true,
                enableRemoveControls: true
            }
        });
        showtab();
    }
    $("#DateofBirthTextBox").on("change", function () {

        jQuery.ajax(
                {
                    url: GetFile('/DesktopModules/eCollection_Sessions/SessionHandler.ashx?SessionStatus=EndSession&EndDate=' + jQuery('#DateofBirthTextBox').val()),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    serverFiltering: true,
                    serverPaging: true,
                    pageSize: 20
                });

    });

       

</script>
