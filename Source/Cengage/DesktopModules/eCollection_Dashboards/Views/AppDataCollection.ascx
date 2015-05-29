<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AppDataCollection.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.AppDataCollection" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<style type="text/css">
    .AccLbl
    {
        width: 495px !important;
        height: 36px;
        float: left;
        border: 1px solid #CCC;
        margin-bottom: 15px;
    }
    .AccLbl .k-dropdown-wrap
    {
        border: 0px solid transparent !important;
        border-left: 1px solid #CCC !important;
    }
    .k-picker-wrap, .k-input, .k-dropdown-wrap
    {
        height: 91% !important;
    }
    .Div_FullWidth .k-dropdown-wrap .k-input
    {
        margin-top: 5px !important;
    }
    
    h6
    {
        color: #0080C8 !important;
        font-size: 10pt !important;
        font-weight: bold;
        margin-left: 19px !important;
        float: left;
        width: 100px !important;
    }
    h3
    {
        font-size: 11pt !important;
        font-weight: 900 !important;
        float: left;
        width: 100%;
        margin: 10px 0px 10px 0px;
    }
    .labelledtxtbox
    {
        width: 353px !important;
    }
    td label
    {
        margin-left: 11pt !important;
        font-size: 11pt !important;
    }
    td
    {
        font-family: Times New Roman !important;
        font-size: 12pt;
    }
    #ckblInfoDiv td
    {
        font-family: Raleway !important;
    }
    .masterdiv
    {
        float: left;
        width: 100%;
    }
    .AccLbl .k-icon
    {
        background-image: url('./portals/0/images/ui-icons_cccccc_256x240.png') !important;
        background-position: -128px -13px !important;
    }
    .mainDiv
    {
        font-size: 11pt !important;
        font-family: Times New Roman !important;
    }
    .CalendarDiv .k-input
    {
        width: 97% !important;
        text-indent: 15px !important;
    }
    .CalendarDiv .k-i-calendar
    {
        margin-top: 5px !important;
        margin-right: 3px !important;
    }
    
    .AccLbl .k-input
    {
        font-size: 10pt !important;
        margin-top: 5px;
        text-transform: uppercase !important;
    }
    .k-state-default
    {
        border-radius: 0px !important;
    }
    #ckblInfoDiv legend
    {
        color: #0080C8 !important;
        margin: 0px 25px 0px 15px;
        font-size: 10pt !important;
    }
    .k-calendar .k-content .k-link
    {
        font-family: Arial !important;
        font-size: 9pt !important;
        font-style: normal !important;
        font-weight: bold !important;
    }
    .k-nav-prev, .k-nav-prev .k-state-hover
    {
        background-image: url('./portals/0/images/sprite.png') !important;
        background-position: 1px -44px !important;
    }
    .k-nav-prev .k-i-arrow-w
    {
        background: none !important;
    }
    .k-nav-next, .k-nav-next .k-state-hover
    {
        background-image: url('./portals/0/images/sprite.png') !important;
        background-position: 3px -13px !important;
    }
    #ddlAccount .k-link:hover, #ddlAccount-list .k-state-selected, #SearchddlAccountTextBox-list .k-list > .k-state-selected, #ddlAccount-list .k-list > .k-state-focused, #ddlAccount-list .k-panelbar > .k-state-selected, #ddlAccount-list .k-panel > .k-state-selected, #ddlAccount-list .k-state-hover
    {
        background: #707070 !important;
        border: 1px solid #707070 !important;
        color: White !important;
    }
    
    #ddlAccount-list .k-state-hover
    {
        background: #CCC !important;
        border: 1px solid #CCC !important;
        color: Black !important;
    }
    
    
    #ddlAccount-list ul
    {
        background: -moz-linear-gradient(center top , white 0%, white 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
        font-style: normal !important;
        text-transform: uppercase !important;
    }
    
    #ddlAccount-list
    {
        margin-top: 3px !important;
    }
    .AppData
    {
        margin-left: 20px;
    }
    
    .CalendarDiv .k-icon
    {
        background-image: url('./portals/0/images/sprite.png') !important;
        background-position: -32px -176px !important;
    }
    .popupHdrBG
    {
        background-image: url('./Portals/0/images/topband.png');
        background-color: White;
        height: 110px;
        margin-top: -9px;
        width: 102%;
        margin-left: -7px;
    }
    
    .popupClosebg
    {
        background-image: url('./Portals/0/images/close.png') !important;
    }
    .popupokbtn
    {
        font-family: Raleway-regular,Raleway,Arial, sans-serif;
        font-weight: 700;
        font-size: 10pt;
        border-radius: 3px;
        color: White;
        width: 135px;
        height: 35px;
        margin-left: 250px;
        float: left;
        border: 1px solid #6EB8C6;
        background: -webkit-gradient(linear, left top, left bottom, from(#01B3D7), to(#008FAC));
        background: -moz-linear-gradient(center top , white 0%, #01B3D7 5%, #008FAC 130%) repeat scroll 0 0 transparent;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#01B3D7', endColorstr='#008FAC', gradientType='0');
        background: -ms-linear-gradient(top, #01B3D7 5%, #008FAC 130%) !important;
        padding: 5px;
        cursor: pointer;
    }
    
    .popupokbtn:hover
    {
        background: -moz-linear-gradient(center top , #0B82A5 5%, #0B82A5 1%, #15617B 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#0B82A5), to(#15617B)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0B82A5', endColorstr='#15617B', gradientType='0') !important;
        background: -ms-linear-gradient(top, #0B82A5 5%, #15617B 130%) !important;
    }
    
    .PopupHeaderSpan
    {
        font-size: 28pt;
        float: left;
        margin-left: 34px;
        margin-bottom: auto;
        font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
        color: #707070;
    }
    .lblStyle
    {
        border: 0px solid transparent !important;
    }
    .k-window-action
    {
        margin-top: 6px !important;
    }
    .DatePickers, .inputStyle .k-widget
    {
        font-style: italic !important;
        width: 209px;
        border: 0px solid !important;
        border-left: 1px Solid #ccc !important;
    }
    .DatePickers
    {
        height: 26px !important;
    }
    .inputStyle .k-widget
    {
        height: 36px !important;
    }
    .inputStyle .k-picker-wrap
    {
        border: 0px solid !important;
        border-left: 1px Solid #ccc !important;
    }
    .inputStyle
    {
        height: 36px !important;
    }
    .AccountInfo
    {
        color: black;
        background-color: Orange;
        font-weight: bold;
    }
    
    .tdinfo
    {
        border: 1px solid black;
        vertical-align: top;
    }
</style>
<div id="MsgDiv" style="margin: 0px 35px 35px;">
    <Msg:Message ID="Messages" runat="server"></Msg:Message>
</div>
<div class="masterdiv AppData">
    <div class="CalendarDiv">
        <h1>
            Generate a report</h1>
        <hr style="border: 0px solid #707070; width: 88%; float: left; background-color: #707070;
            color: #707070; height: 1px; margin-bottom: 20px;" />
        <br />
        <h3>
            Specify a time period:</h3>
        <div class="labelledtxtbox">
            <div class="lblStyle" style="" id="spnFirstName">
                <h6>
                    FROM DATE</h6>
            </div>
            <div class="inputStyle">
                <input type="date" id="FromTextBox" name="IpadDate1" class="DatePickers" value='<%= DateTime.Now.ToString("dd/MM/yyyy") %>' /></div>
        </div>
        <br />
        <br />
        <asp:HiddenField ID="hdnFrom" runat="server" ClientIDMode="Static" />
        <br />
        <div class="labelledtxtbox">
            <div class="lblStyle" style="" id="Div1">
                <h6>
                    TO DATE</h6>
            </div>
            <div class="inputStyle" style="">
                <input type="date" id="ToTextBox" name="IpadDate" class="DatePickers" value='<%= DateTime.Now.ToString("dd/MM/yyyy") %>' /></div>
        </div>
        <br />
        <asp:HiddenField ID="hdnTo" runat="server" ClientIDMode="Static" />
    </div>
    <div class="Div_FullWidth">
        <h3>
            Specify a account:</h3>
        <div class="AccLbl">
            <div class="lblStyle" id="Div2">
                <h6>
                    ACCOUNT</h6>
            </div>
            <div class="inputStyle" style="">
                <asp:DropDownList ID="ddlAccount" DataTextField="AccountName" ClientIDMode="Static"
                    AppendDataBoundItems="true" DataValueField="ANo" runat="server" Height="38px"
                    Width="351px">
                    <asp:ListItem Value="-2" Selected="True">Select an account</asp:ListItem>
                    <asp:ListItem Value="-1">All</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
</div>
<div id="ckblInfoDiv" class="Div_FullWidth">
    <h3 style="margin-left: 20px;">
        Specify a report category:</h3>
    <h6 style="width: 250px !important; color: #707070 !important;">
        <asp:Panel ID="Panel1" runat="server" GroupingText="CATEGORY" Style="float: left;
            width: 358px; margin-top: 5px;">
            <asp:CheckBoxList ID="ckblInfo" runat="server" Style="margin: 20px;">
                <asp:ListItem>Account Info</asp:ListItem>
                <asp:ListItem>User</asp:ListItem>
                <asp:ListItem>Books</asp:ListItem>
                <asp:ListItem>Reading Session</asp:ListItem>
                <asp:ListItem>Error Log</asp:ListItem>
            </asp:CheckBoxList>
        </asp:Panel>
    </h6>
</div>
<div class="Div_FullWidth" style="margin-top: 15px; margin-left: 40px;">
    <asp:Button ID="btnSubmit" runat="server" class="ActiveAddButtonsHolder" ClientIDMode="Static"
        Style="cursor: pointer; width: 20% !important; margin: 0px !important; height: 35px;
        color: White; border: 0px solid #CCC !important;" Text="GENERATE" OnClick="btnSubmit_Click" />
</div>
<br />
<div id="maindiv" style="width: 100%; float: left" class="HideItems" runat="server">
    <style type="text/css">
        .AccountInfo
        {
            color: black;
            font-weight: bold;
        }
        .thAccinfo
        {
            background-color: #ffcc99;
            text-align: left;
        }
        .thUserinfo
        {
            background-color: #66ccff;
            text-align: left;
        }
        .thBooksinfo
        {
            background-color: orange;
            text-align: left;
        }
        .spaces
        {
            width: 100px;
        }
        .tdinfo
        {
            border: 0.5pt solid gray;
            vertical-align: top;
        }
        .thSessionsinfo
        {
            background-color: #99cc00;
            text-align: left;
        }
        .thErrorsinfo
        {
            background-color: red;
            text-align: left;
        }
        .thleftalign
        {
            text-align: left;
        }
    </style>
    <div id="SingleAccountSection" runat="server" clientidmode="Static">
     <table>
        <tr>
            <td>
                <div id="SingleAccountSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="7">
                                ACCOUNT SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thAccinfo" colspan="4">
                                Account Information
                            </th>
                            <th class="thAccinfo" colspan="3">
                                Subscription Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thAccinfo">
                                Account Name
                            </th>
                            <th class="thAccinfo">
                                Account ID
                            </th>
                            <th class="thAccinfo">
                                Total Number of Licenses
                            </th>
                            <th class="thAccinfo">
                                Total Number of Books
                            </th>
                            <th class="thAccinfo">
                                Number of Subscriptions
                            </th>
                            <th class="thAccinfo">
                                Subscription ID
                            </th>
                            <th class="thAccinfo">
                                Number of Books Per Subscription
                            </th>
                        </tr>
                        <asp:ListView ID="SingleAccountSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="AccountsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("Total No Of Licenses") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("Total Number Of Books")%>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="lblNoOfSubscripions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SubscriptionsListView1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Subscription")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SubscriptionsListView2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number Of Books")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="SingleUserSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="8">
                                USERS SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thUserinfo" colspan="4">
                                Teachers Information
                            </th>
                            <th class="thUserinfo" colspan="4">
                                Students Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">                            
                            <th id="Th9" class="thUserinfo">
                                No of Teachers
                            </th>
                            <th id="Th10" class="thUserinfo">
                                No of times Teacher logged in
                            </th>
                            <th id="Th11" class="thUserinfo">
                                Teacher Name
                            </th>
                            <th id="Th12" class="thUserinfo">
                                No of times logged in
                            </th>
                            <th id="Th13" class="thUserinfo">
                                No of Students
                            </th>
                            <th id="Th14" class="thUserinfo">
                                No of times Student logged in
                            </th>
                            <th id="Th15" class="thUserinfo">
                                Student Name
                            </th>
                            <th id="Th16" class="thUserinfo">
                                No of times logged in
                            </th>
                        </tr>
                        <asp:ListView ID="SingleUserSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="UserSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>                                            
                                            <td id="u11" class="tdinfo">
                                                <asp:Label ID="NoOfTeachers" runat="server"></asp:Label>
                                            </td>
                                            <td id="u12" class="tdinfo">
                                                <asp:Label ID="NooftimesTeacherloggedin" runat="server"></asp:Label>
                                            </td>
                                            <td id="u13" class="tdinfo">
                                                <asp:ListView ID="TeachersList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("User Name") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u14" class="tdinfo">
                                                <asp:ListView ID="TeachersList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("No of times logged in")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u15" class="tdinfo">
                                                <asp:Label ID="NoOfStudents" runat="server"></asp:Label>
                                            </td>
                                            <td id="u16" class="tdinfo">
                                                <asp:Label ID="NooftimesStudentloggedin" runat="server"></asp:Label>
                                            </td>
                                            <td id="u17" class="tdinfo">
                                                <asp:ListView ID="StudentList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("UserLoginName")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u18" class="tdinfo">
                                                <asp:ListView ID="StudentList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("No of times logged in")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="SingleBooksSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="5">
                                BOOKS SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thBooksinfo" colspan="3">
                                Books Information
                            </th>
                            <th class="thBooksinfo">
                                Favourite Books
                            </th>
                            <th class="thBooksinfo">
                                Most Used PM Reading Level
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">                            
                            <th class="thBooksinfo">
                                Number of books read
                            </th>
                            <th class="thBooksinfo">
                                Number of Guided Profile books
                            </th>
                            <th class="thBooksinfo">
                                Number of Independent Profile books
                            </th>
                            <th class="thBooksinfo">
                                Title
                            </th>
                            <th class="thBooksinfo">
                                Reading Level
                            </th>
                        </tr>
                        <asp:ListView ID="SingleBooksSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="BooksSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>                                           
                                            <td class="tdinfo">
                                                <asp:Label ID="NoOfbooksread" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofGuidedProfilebooks" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofIndependentProfilebooks" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="FavouriteBooksList" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("TITLE")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="ReadingLevelList" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Reading Level")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="SingleSessionsSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="9">
                                READING SESSION SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thSessionsinfo" colspan="9">
                                Reading Session Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">                            
                            <th class="thSessionsinfo">
                                Number of Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Guided Profile reading sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Independent Profile reading sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Completed Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Notes for all Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Students under all Reading Session
                            </th>
                            <th class="thSessionsinfo">
                                Session Name
                            </th>
                            <th class="thSessionsinfo">
                                Number of Students under the Reading Session
                            </th>
                            <th class="thSessionsinfo">
                                Number of Books assigned per reading Session
                            </th>
                        </tr>
                        <asp:ListView ID="SingleSessionsSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="SessionsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>                                           
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofGuidedProfilereadingsessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofIndependentProfilereadingsessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofCompletedReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofNotesforallReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofStudentsunderallReadingSession" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("SESSION_NAME")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number of Students under Reading Session")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList3" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number of Books assigned per reading Session")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="SingleErrorsSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>                           
                            <th class="thleftalign">
                                ERROR LOG SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thErrorsinfo">
                                Error Log
                            </th>
                        </tr>
                        <tr class="AccountInfo">                            
                            <th class="thErrorsinfo">
                                Number Of Errors
                            </th>
                        </tr>
                        <asp:ListView ID="SingleErrorsSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="ErrorsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>                                           
                                            <td class="tdinfo">
                                                <asp:Label ID="ErrorLogCountLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </div>
    <div  id="MultipleAccountsSection" runat="server" clientidmode="Static">
    <table>
        <tr>
            <td>
                <div id="AccountSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="7">
                                ACCOUNT SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thAccinfo" colspan="4">
                                Account Information
                            </th>
                            <th class="thAccinfo" colspan="3">
                                Subscription Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thAccinfo">
                                Account Name
                            </th>
                            <th class="thAccinfo">
                                Account ID
                            </th>
                            <th class="thAccinfo">
                                Total Number of Licenses
                            </th>
                            <th class="thAccinfo">
                                Total Number of Books
                            </th>
                            <th class="thAccinfo">
                                Number of Subscriptions
                            </th>
                            <th class="thAccinfo">
                                Subscription ID
                            </th>
                            <th class="thAccinfo">
                                Number of Books Per Subscription
                            </th>
                        </tr>
                        <asp:ListView ID="AccountSectionMainList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="AccountsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("Total No Of Licenses") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("Total Number Of Books")%>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="lblNoOfSubscripions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SubscriptionsListView1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Subscription")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SubscriptionsListView2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number Of Books")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="UserSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="10">
                                USERS SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thUserinfo" colspan="6">
                                Teachers Information
                            </th>
                            <th class="thUserinfo" colspan="4">
                                Students Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thUserinfo">
                                Account Name
                            </th>
                            <th class="thUserinfo">
                                Account ID
                            </th>
                            <th id="Th1" class="thUserinfo">
                                No of Teachers
                            </th>
                            <th id="Th2" class="thUserinfo">
                                No of times Teacher logged in
                            </th>
                            <th id="Th3" class="thUserinfo">
                                Teacher Name
                            </th>
                            <th id="Th4" class="thUserinfo">
                                No of times logged in
                            </th>
                            <th id="Th5" class="thUserinfo">
                                No of Students
                            </th>
                            <th id="Th6" class="thUserinfo">
                                No of times Student logged in
                            </th>
                            <th id="Th7" class="thUserinfo">
                                Student Name
                            </th>
                            <th id="Th8" class="thUserinfo">
                                No of times logged in
                            </th>
                        </tr>
                        <asp:ListView ID="UserSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="UserSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td id="u11" class="tdinfo">
                                                <asp:Label ID="NoOfTeachers" runat="server"></asp:Label>
                                            </td>
                                            <td id="u12" class="tdinfo">
                                                <asp:Label ID="NooftimesTeacherloggedin" runat="server"></asp:Label>
                                            </td>
                                            <td id="u13" class="tdinfo">
                                                <asp:ListView ID="TeachersList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("User Name") %>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u14" class="tdinfo">
                                                <asp:ListView ID="TeachersList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("No of times logged in")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u15" class="tdinfo">
                                                <asp:Label ID="NoOfStudents" runat="server"></asp:Label>
                                            </td>
                                            <td id="u16" class="tdinfo">
                                                <asp:Label ID="NooftimesStudentloggedin" runat="server"></asp:Label>
                                            </td>
                                            <td id="u17" class="tdinfo">
                                                <asp:ListView ID="StudentList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("UserLoginName")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td id="u18" class="tdinfo">
                                                <asp:ListView ID="StudentList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("No of times logged in")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="BooksSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="7">
                                BOOKS SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thBooksinfo" colspan="5">
                                Books Information
                            </th>
                            <th class="thBooksinfo">
                                Favourite Books
                            </th>
                            <th class="thBooksinfo">
                                Most Used PM Reading Level
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thBooksinfo">
                                Account Name
                            </th>
                            <th class="thBooksinfo">
                                Account ID
                            </th>
                            <th class="thBooksinfo">
                                Number of books read
                            </th>
                            <th class="thBooksinfo">
                                Number of Guided Profile books
                            </th>
                            <th class="thBooksinfo">
                                Number of Independent Profile books
                            </th>
                            <th class="thBooksinfo">
                                Title
                            </th>
                            <th class="thBooksinfo">
                                Reading Level
                            </th>
                        </tr>
                        <asp:ListView ID="BooksSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="BooksSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NoOfbooksread" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofGuidedProfilebooks" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofIndependentProfilebooks" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="FavouriteBooksList" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("TITLE")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="ReadingLevelList" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Reading Level")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="SessionsSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>
                            <th class="thleftalign" colspan="11">
                                READING SESSION SECTION
                            </th>
                        </tr>
                        <tr>
                            <th class="thSessionsinfo" colspan="11">
                                Reading Session Information
                            </th>
                            <th class="spaces">
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thSessionsinfo">
                                Account Name
                            </th>
                            <th class="thSessionsinfo">
                                Account ID
                            </th>
                            <th class="thSessionsinfo">
                                Number of Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Guided Profile reading sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Independent Profile reading sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Completed Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Notes for all Reading Sessions
                            </th>
                            <th class="thSessionsinfo">
                                Number of Students under all Reading Session
                            </th>
                            <th class="thSessionsinfo">
                                Session Name
                            </th>
                            <th class="thSessionsinfo">
                                Number of Students under the Reading Session
                            </th>
                            <th class="thSessionsinfo">
                                Number of Books assigned per reading Session
                            </th>
                        </tr>
                        <asp:ListView ID="SessionsSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="SessionsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofGuidedProfilereadingsessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofIndependentProfilereadingsessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofCompletedReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofNotesforallReadingSessions" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="NumberofStudentsunderallReadingSession" runat="server"></asp:Label>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList1" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("SESSION_NAME")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList2" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number of Students under Reading Session")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:ListView ID="SessionsList3" runat="server">
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td class="tdinfo">
                                                                    <%#Eval("Number of Books assigned per reading Session")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
            <td>
                <div id="ErrorsSectionTable" runat="server" clientidmode="Static">
                    <table>
                        <tr>                           
                            <th class="thleftalign">
                                ERROR LOG SECTION
                            </th>
                        </tr>
                        <tr>
                            <th colspan="3" class="thErrorsinfo">
                                Error Log
                            </th>
                        </tr>
                        <tr class="AccountInfo">
                            <th class="thErrorsinfo">
                                Account Name
                            </th>
                            <th class="thErrorsinfo">
                                Account ID
                            </th>
                            <th class="thErrorsinfo">
                                Number Of Errors
                            </th>
                        </tr>
                        <asp:ListView ID="ErrorsSectionList" runat="server" DataKeyNames="ANo" OnItemDataBound="MainListView_ItemDataBound">
                            <ItemTemplate>
                                <asp:ListView ID="AccountsListView" DataKeyNames="AccountSk" runat="server" OnItemDataBound="ErrorsSection_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdinfo">
                                                <%#Eval("AccountName") %>
                                            </td>
                                            <td class="tdinfo">
                                                <%#Eval("AccountID")%>
                                            </td>
                                            <td class="tdinfo">
                                                <asp:Label ID="ErrorLogCountLabel" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </div>
</div>
<div id="Select-message" title="Select Category" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan">Alert Message</span>
    </div>
    <div class="appdatapopupindiv">
        <div class="appdatapopupinndiv">
            <span id="MessageLiteral" class="appdatappspan">Please select atleast
                one category.</span>
        </div>
        <div class="appdataokbtndiv">
            <input type="button" id="YesButton" value="OK" class="popupokbtn appdataokbtn" />
        </div>
    </div>
</div>
<div id="SelectDate-message" title="Select Date" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message</span>
    </div>
    <div class="appdatapopupindiv">
        <div class="appdatapopupinndiv">
            <span id="Span1" class="appdatappspan">
                <asp:Label ID="DateValidation" runat="server"></asp:Label>
            </span>
        </div>
        <div class="appdataokbtndiv">
            <input type="button" id="DateButton" value="OK" class="popupokbtn appdataokbtn" />
        </div>
    </div>
</div>
<script src="desktopmodules/ecollection_dashboards/Scripts/AppDataCollection.js" type="text/javascript"></script>
