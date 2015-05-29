<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupProfile" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="~/Controls/eCollectionControls/ReadingHistory.ascx" TagName="HistoryDiv"
    TagPrefix="HD" %>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/Scripts/jquery.corner.js")%>"
    type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/Scripts/easyjquery.js")%>"
    type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/Scripts/jquery.jplayer.min.js")%>"
    type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/jplayer.css")%>"
    rel="Stylesheet" type="text/css" />
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/jplayer.blue.monday.css")%>"
    rel="Stylesheet" type="text/css" />
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Groups/Scripts/jplayer.playerlist.min.js")%>"
    type="text/javascript"></script>
<style type="text/css">
    .KenduoComboBoxClassDiv .k-combobox
    {
        width: 94% !important;
        float: left;
        margin-left: 1px !important;
    }
    #HistoryDiv .ui-state-active, .ui-state-hover, ui-state-focus, .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-content .ui-state-active, .ui-widget-header .ui-state-default, .ui-widget-header .ui-state-active, .ui-widget-content
    {
        border: 0px solid white !important;
        background: none !important;
        zoom: 0;
    }
    .GpYstrDyReptcontentThrdtDiv ul, .GpYstrDyReptcontentThrdtDiv li
    {
        margin: 0;
        padding: 0;
        list-style: none;
    }
    .GpYstrDyReptcontentThrdtDiv li
    {
        width: 210px;
        height: 90px;
    }
    
    p#controls
    {
        display: none;
    }
    
    .GpYstrDyReptcontentThrdtDiv:hover + p#controls
    {
        display: block;
    }
    
    p#controls
    {
        margin: 6px 0px 0px 0px;
        position: relative;
    }
    #prevBtn, #nextBtn
    {
        display: block;
        margin: 0;
        overflow: hidden;
        text-indent: -8000px;
        width: 30px;
        height: 39px;
        position: absolute;
        left: -30px;
        top: 82px;
    }
    #content
    {
        position: relative;
    }
    #nextBtn
    {
        left: 230px;
    }
    #prevBtn
    {
        left: 40px;
    }
    .dnnFormSuccess, .dnnFormWarning
    {
        margin-bottom: 20px !important;
    }
    .dnnFormMessage
    {
        width: 85.8%;
        margin: 1px 0px 0px 2px;
        padding: 10px 10px 10px 75px;
    }
    .dnnMsgHolder
    {
        width: 94%;
        margin-left: 3px;
    }
    #SearchTextBox-list
    {
        width: 587px !important;
        margin-left: -1px !important;
        margin-top: 6px !important;
        box-shadow: none !important;
        -webkit-box-shadow: none !important;
    }
    #SearchTextBox-list ul
    {
        background: -moz-linear-gradient(center top , white 0%, white 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }
    
    #SearchTextBox-list .k-state-hover, #SearchTextBox-list .k-state-focused, .k-state-hover:hover
    {
        background: #707070 !important;
        border: 1px solid #707070 !important;
        color: White !important;
    }
    .classSearchwater:hover
    {
        border: none !important;
        padding-left: 8px !important;
        padding-top: 1px !important;
        background: transparent !important;
        width: 607px !important;
        box-shadow: none !important;
    }
    .classSearchwater
    {
        width: 607px !important;
        height: 29px !important;
        background: transparent !important;
        padding-left: 8px !important;
        padding-top: 1px !important;
        box-shadow: none !important;
        border: none !important;
        font-size: 10pt !important;
    }
    .ReadingSessionBookWraper
    {
        width: 216px;
        list-style: none;
        overflow: auto;
        height: 140px;
    }
    .ReadingSessionBookWraper li
    {
        display: inline;
    }
   
</style>
<div class="GroupprofileContentDiv">
    <%-- <span id="ClassNameForProfile" runat="server" style="float: left; color: #707070;
        font-size: 10pt; margin-top: -50px; margin-left: 56px; font-style: italic;">Class
        Name</span>--%>
    <div id="MessagesDiv">
        <uc1:Messages ID="Messages" runat="server" />
    </div>
    <div class="Gpnamememdiv">
        <div class="GpNamediv">
            <h3 id="GroupNameLabel" class="Gpnamelbl" runat="server">
            </h3>
            <br />
          <%--  <div style="float: left; margin-left: 16px; margin-top: 2px;">
                <asp:Button ID="EditGroupButton" runat="server" ClientIDMode="static" Text="Edit Group"
                    UseSubmitBehavior="false" OnClientClick="javascript:target='_parent';" Style="border: 0px solid white;
                    float: left; background: transparent; color: #1FB5E7; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                    font-weight: bold; font-size: 10pt; outline: none; cursor: pointer" OnClick="EditGroupButton_Click" />
                <b id="Boldline" style="float: left; color: #1FB5E7; margin-top: 3px;">|</b>
                <asp:Button ID="PrintCredentialButton" runat="server" ClientIDMode="static" UseSubmitBehavior="false"
                    OnClientClick="javascript:target='_blank';" Text="Print student usernames and passwords" Style="border: 0px solid white;
                    float: left; background: transparent; color: #1FB5E7; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                    font-weight: bold; font-size: 10pt; outline: none; cursor: pointer" OnClick="PrintCredentialButton_Click" />
            </div>--%>
        </div>
        <div class="GpMemberCountdiv">
            <asp:Label ID="MemebersCountLabel" runat="server" CssClass="GPMemCountlbl"></asp:Label>
            <span class="GPMemSpan">MEMBER/S</span>
        </div>
    </div>
    <div class="GpYourclassdiv" id="GpYourClass" runat="server">
        <h3 class="GPYourclassspan">
            This is your class</h3>
    </div>
    <div class="GpReadingdiv">
        <div style="float: left; border: 0px; width: 38%; margin-left: 16px; margin-top: 15px;">
            <h3 style="float: left; font-weight: 700 !important; font-size: 13.5pt !important;
                color: #707070; margin-bottom: 3px;">
                Reading span level</h3>
        </div>
        <div class="GpReadingContentdiv">
            <div id="divcircle" runat="server" clientidmode="Static" class="divcircleleft" style="background-color: #707070;">
                <span id="MinReadLevel" runat="server" style="float: left; text-align: center; width: 100px;
                    padding-top: 3px; position: absolute; font-family: Raleway-regular,Raleway, Arial, sans-serif;
                    font-size: 50pt; color: white;">0</span>
            </div>
            <span style="float: left; margin-top: 37px; margin-left: 9px; margin-right: 11px;
                color: #707070; font-family: Raleway-regular,Raleway, Arial, sans-serif; font-size: 10pt;">
                to</span>
            <div id="divcircletwo" runat="server" clientidmode="Static" class="divcircleright"
                style="background-color: #707070;">
                <span id="MaxReadLevel" runat="server" style="text-align: center; width: 100px; float: left;
                    padding-top: 3px; font-family: Raleway-regular,Raleway, Arial, sans-serif; font-size: 50pt;
                    color: white;">0</span>
            </div>
        </div>
    </div>
    <br />
    <div class="GpBookLstnRcrddiv">
        <div class="GpBooksdiv">
            <div class="GPBooksopendiv">
                <asp:Label ID="NoOfBoooksOpenedLabel" runat="server" CssClass="GpNoofBooksopenlbl"></asp:Label>
                <span class="GpNoofbooksspan" id="BookOpenedText" runat="server">BOOK/S OPENED</span><br />
            </div>
            <div class="GPbooksindepentdiv">
                <asp:Label ID="IndependentCountLabel" runat="server" Text="0%" CssClass="Gpindepentcntlbl"></asp:Label>
                <span class="Gpindenpentcntspan">INDEPENDENT</span>
            </div>
            <div class="Gpdepentdiv">
                <asp:Label ID="GuidedCountLabel" runat="server" Text="0%" CssClass="Gpdenpentcntlbl"></asp:Label>
                <span class="Gpdependcntspn">GUIDED</span>
            </div>
        </div>
        <div class="GpWordsdiv">
            <div class="Gpnoofwordsdiv">
                <asp:Label ID="NoOfWordsLabel" runat="server" CssClass="GpnoofWordslbl"></asp:Label>
            </div>
            <div class="Gpwrddiv">
                <h5 class="Gpwrdsavespn">
                    WORD/S SAVED</h5>
                <span class="Gpwrdsavetospn">TO</span>
                <asp:Button ID="WordLogButton" Text="MY WORDS" UseSubmitBehavior="false" OnClientClick="javascript:target='_parent';"
                    CssClass="Gpwrdlogbtn" runat="server" OnClick="WordLogButton_Click" />
            </div>
        </div>
        <div class="GpListendiv">
            <div class="GpListencntdiv">
                <asp:Label ID="NoOfRecordingsLabel" runat="server" CssClass="Gplstncntlbl"></asp:Label>
            </div>
            <div class="Gplstncontdiv">
                <h5 class="Gplstnrcrdspn">
                    RECORDING/S MADE</h5>
                <asp:Button ID="ListenButton" runat="server" UseSubmitBehavior="false" OnClientClick="javascript:target='_parent';"
                    Text="LISTEN" CssClass="Gplstnlogbtn" OnClick="ListenButton_Click" />
            </div>
        </div>
    </div>
    <div class="GpGrapdiv">
        <div style="width: 100%; margin: 3px 3px 0px 0px; float: left; border: 1px solid lightgray;">
            <div style="float: left; margin-left: 6.5%; width: 97%;">
                <asp:ListView ID="graph" runat="server" Style="width: 100%; float: left; border: 1px solid lightgray;
                    min-height: 200px;">
                    <ItemTemplate>
                        <div id="Graph1" class="GraphLineHdr">
                            <div id="LineDiv<%# Container.DataItemIndex %>" class="GraphLine">
                                <div id="Div<%# Container.DataItemIndex %>24" class="GraphPoint GraphPt2324">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>23" class="GraphPoint GraphPt2324">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>22" class="GraphPoint GraphPt2122">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>21" class="GraphPoint GraphPt2122">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>20" class="GraphPoint GraphPt1920">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>19" class="GraphPoint GraphPt1920">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>18" class="GraphPoint GraphPt1718">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>17" class="GraphPoint GraphPt1718">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>16" class="GraphPoint GraphPt1516">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>15" class="GraphPoint GraphPt1516">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>14" class="GraphPoint GraphPt121314">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>13" class="GraphPoint GraphPt121314">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>12" class="GraphPoint GraphPt121314">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>11" class="GraphPoint GraphPt91011">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>10" class="GraphPoint GraphPt91011">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>09" class="GraphPoint GraphPt91011">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>08" class="GraphPoint GraphPt678">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>07" class="GraphPoint GraphPt678">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>06" class="GraphPoint GraphPt678">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>05" class="GraphPoint GraphPt345">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>04" class="GraphPoint GraphPt345">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>03" class="GraphPoint GraphPt345">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>02" class="GraphPoint GraphPt12">
                                </div>
                                <div id="Div<%# Container.DataItemIndex %>01" class="GraphPoint GraphPt12 GraphPt1">
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
        <div style="float: left; margin-left: 12.18%; margin-top: -3px; background-color: white;
            z-index: 1000; width: 10px; height: 4px;">
            <div style="-webkit-transform: rotate(45deg); -moz-transform: rotate(45deg); transform: rotate(45deg);
                -ms-filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865474,
            M12=-0.7071067811865477,
            M21=0.7071067811865477,
            M22=0.7071067811865474,SizingMethod='auto expand'); filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865474,
            M12=-0.7071067811865477,
            M21=0.7071067811865477,
            M22=0.7071067811865474,sizingMethod='auto expand'); float: left; border-left: 1px solid #CCC;
                height: 8px; width: 4px; margin-top: -3px; background-color: white;">
            </div>
            <div style="-webkit-transform: rotate(-48deg); -moz-transform: rotate(-48deg); transform: rotate(-48deg);
                -ms-filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865483,
            M12=0.7071067811865467,
            M21=-0.7071067811865467,
            M22=0.7071067811865483,SizingMethod='auto expand'); filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865483,
            M12=0.7071067811865467,
            M21=-0.7071067811865467,
            M22=0.7071067811865483,sizingMethod='auto expand'); float: left; float: left; border-left: 1px solid #CCC;
                height: 9px; width: 4px; margin-left: 6px; margin-top: -11px;">
            </div>
        </div>
        <div style="float: right; margin-right: 11.6%; margin-top: -3px; background-color: white;
            z-index: 1000; width: 10px; height: 4px;">
            <div style="-webkit-transform: rotate(45deg); -moz-transform: rotate(45deg); transform: rotate(45deg);
                -ms-filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865474,
            M12=-0.7071067811865477,
            M21=0.7071067811865477,
            M22=0.7071067811865474,SizingMethod='auto expand'); filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865474,
            M12=-0.7071067811865477,
            M21=0.7071067811865477,
            M22=0.7071067811865474,sizingMethod='auto expand'); float: left; border-left: 1px solid #CCC;
                height: 8px; width: 4px; margin-top: -3px; background-color: white;">
            </div>
            <div style="-webkit-transform: rotate(-48deg); -moz-transform: rotate(-48deg); transform: rotate(-48deg);
                -ms-filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865483,
            M12=0.7071067811865467,
            M21=-0.7071067811865467,
            M22=0.7071067811865483,SizingMethod='auto expand'); filter: progid:DXImageTransform.Microsoft.Matrix(M11=0.7071067811865483,
            M12=0.7071067811865467,
            M21=-0.7071067811865467,
            M22=0.7071067811865483,sizingMethod='auto expand'); float: left; float: left; border-left: 1px solid #CCC;
                height: 9px; width: 4px; margin-left: 6px; margin-top: -11px;">
            </div>
        </div>
    </div>
    <div class="GpGraphrangediv">
        <div style="float: left; width: 100%; height: 73px;">
            <div class="GpGraphfrmrangdiv">
                <div style="margin-top: 20px; margin-bottom: 22px;">
                    <i class="GpGrphfrmspn"><span id="sixmonthBefore" runat="server" style="font-weight: bold">
                    </span></i>
                    <p class="Gpgrphlvlpr">
                        LEVEL <span id="MinStartReadFrom" runat="server" style="font-weight: bold; margin-right: 0px;">
                            0 </span>/<span id="MaxStartReadUpto" runat="server" style="font-weight: bold">0</span></p>
                </div>
            </div>
            <div class="Gpgrpmidiv">
                <center>
                    <span id="TotalMonth" style="margin-right: 3px" runat="server">0 MONTH/S</span>
                </center>
            </div>
            <div class="GpGrphtorangdiv">
                <div style="margin-top: 20px; margin-bottom: 22px;">
                    <i class="GpGrphtospn"><span id="TodayDate" runat="server" style="font-weight: bold">
                    </span></i>
                    <p class="GpGrphtolvl">
                        LEVEL <span id="MinCurrReadFrom" runat="server" style="font-weight: bold; margin-right: 0px;">
                            0</span>/<span id="MaxCurrReadUpto" runat="server" style="font-weight: bold">0</span></p>
                </div>
            </div>
        </div>
        <asp:Repeater ID="ReadingLevelRepeater" runat="server">
            <ItemTemplate>
                <div class="Percentage_ReadingLevel">
                    <div class="LevelBooksRead">
                        <asp:Label ID="PercentageLevelLabel" Style="color: #20B3E6" runat="server" Text='<%# Eval("ReadingPercentage") %>'></asp:Label>
                        OF BOOKS READ WERE LEVEL
                        <asp:Label ID="CurrentReadinLevel" runat="server" Text='<%# Eval("ReadingLevel") %>'></asp:Label>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="Switch" class="GPSwitchdiv">
        <div id="HistoryButton" class="GPHistoerybtndiv">
        </div>
        <div id="StudentsButton" class="GpStudentbtndiv">
        </div>
    </div>
    <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
        <div  class="bubble" >
            <asp:Label ID="Message1" runat="server" Text="" />            
            <asp:Label ID="Message2" runat="server"  Text="" />            
        </div>
    </div>
    <div id="HistoryDiv" style="display: block; float: left; width: 100%;">
        <div id="HistoryMainDiv" class="HistoryMainDiv">
            <div class="HistoryDiv">
                <div id="TodaysRecordings" class="RecordingsHolder" style="margin-top:0px;">
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" onclick="toggle_visibility('TodaysRecordingsDiv','TodaysBorderDiv',this);">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv">
                            <asp:Label ID="TodayLabel" class="H4" runat="server" Text="  Today" />
                        </div>
                    </div>
                </div>
                <div id="TodaysBorderDiv">
                    <div id="TodaysRecordingsDiv" class="MyHistoryHolder_Content1" style="display:none;">
                         <asp:Repeater ID="TodaysIndependentRecordings" runat="server" OnItemDataBound="TodaysIndependentRecordings_ItemDataBound">
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
                                            <asp:Image ID="BookCoverImg" AlternateText="CoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"
                                                CssClass ="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="TodaysIndMemberID" runat="server" Text='<%# Eval("UserID")%>' Visible="false"></asp:Literal>
                                            <asp:Literal ID="ProductId" runat="server" Text='<%# Eval("ProductId")%>' Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'TodayIndeRecDiv<%# Container.ItemIndex %>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -32px;">
                                                    <asp:Label ID="WordCount" runat="server" Text='<%# Eval("WordCount")%>'></asp:Label>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book</div>
                                                <div class="Div_FullWidth">
                                                    opened</div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                                        font-weight: 700;">
                                                        <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                    </b>
                                                    <asp:Label ID="hourspan" runat="server"></asp:Label>
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
                                                <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                    <asp:Repeater ID="TodayIndeVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;">
                                                                <span id="IndeRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;">
                                                                <span id="IndeRecordingPathalt<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
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
                       
                        <div class="GpRptcntdiv" style="padding-bottom: 15px;">
                            <div class="Gpguideddiv" id="TSessionDiv">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater ID="TodaysReadingSessionRepeater" runat="server" OnItemDataBound="TodaysReadingSessionRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <div id="TodaysGpSessionCircleImg" class="GpYesterDayRepeaterDiv">
                                                    <div class="GpYesterDayRepeaterimgDiv">
                                                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                    </div>
                                                    <div id="TodaysGpSessionRepeater" class="GpYesterDayRepeatercontentDiv">
                                                        <div class="GpYstrDyReptcontentfstDiv">
                                                            <div class="GpYstrDyReptcontentfstinDiv">
                                                                <span class="GpYstrDyReptcontentfstinspn">
                                                                    <%# Eval("SessionName")%></span>
                                                                <asp:Label ID="TodaysReadingSessionSentTime" runat="server" style="float:left;" Text='<%# Eval("SessionCreatedDate") %>'></asp:Label>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentfstsndDiv">
                                                                <asp:Button ID="TodaysReviewButton" CssClass="GpReviewbtn" Text="REVIEW" runat="server"
                                                                    CommandArgument='<%# Eval("SessionID") %>' OnClick="ReviewButton_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentscndtDiv">
                                                            <img id="TGuidedImg" style="" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/unopen.png")%>" />
                                                        </div>
                                                        <asp:Label ID="TodaysGpId" runat="server" Text='<%# Eval("GroupId")%>' Visible="false"></asp:Label>
                                                        <div class="GpYstrDyReptcontentThrdtDiv">
                                                            <ul id="TWrapList" runat="server" clientidmode="Static" class="ReadingSessionBookWraper">
                                                            </ul>
                                                        </div>
                                                        <span></span>
                                                        <div class="GpYstrDyReptcontentFortDiv">
                                                            <div class="GpYstrDyReptcontentFortfstDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="TodaysUnReadCountLabel" runat="server" Text='<%# Eval("BookUnOpened") %>'
                                                                    Style="float: left; margin-left: -18px; margin-top: 14px; font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortfstspn">UNREAD</span>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentFortscndDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="TodaysReadCountLable" runat="server" Text='<%# Eval("BookOpened") %>' Style="float: left;
                                                                    padding-left: 49px; margin-top: 28px; font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070; position: absolute;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortscndspn">OPENED</span>
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentFithDiv">
                                                            <span>Note:</span>
                                                            <p>
                                                                <%# Eval("SessionNote")%>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv" id="YesterdaysHistoryDiv">
                <div id="YesterDaysRecordings" class="RecordingsHolder">
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="YesterdayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" onclick="toggle_visibility('YesterdaysRecordingsDiv','YesterdaysBorderDiv',this);">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="YesterdayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv_Up" style="display:block;">
                            <asp:Label ID="YesterdayLabel" class="H4" runat="server" Text="  Yesterday" />
                        </div>
                    </div>
                </div>
                <div id="YesterdaysBorderDiv">
                    <div id="YesterdaysRecordingsDiv" class="MyHistoryHolder_Content" style="display:none;">
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
                                            <asp:Image ID="BookCoverImg" AlternateText="CoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"
                                                CssClass ="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="YesterdaysIndMemberID" runat="server" Text='<%# Eval("UserID")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Literal ID="ProductId" runat="server" Text='<%# Eval("ProductId")%>' Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'YesterdayIndeRecDiv<%# Container.ItemIndex %>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_big.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -32px;">
                                                    <asp:Label ID="WordCount" runat="server" Text='<%# Eval("WordCount")%>'></asp:Label>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book
                                                </div>
                                                <div class="Div_FullWidth">
                                                    opened
                                                </div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                                        font-weight: 700;">
                                                        <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                    </b>
                                                    <asp:Label ID="hourspan" runat="server"></asp:Label>
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
                                                                            <%--  <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
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
                                                <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                    <asp:Repeater ID="YesterdayIndVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;">
                                                                <span id="YesterdayIndRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;">
                                                                <span id="YesterdayIndRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn"/>
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
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
                       
                        <div class="GpRptcntdiv" style="padding-bottom: 15px;">
                            <div class="Gpguideddiv" id="YSessionDiv">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater ID="YesterdaysReadingSessionRepeater" runat="server" OnItemDataBound="YesterdaysReadingSessionRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <div id="YesterdaysGpSessionCircleImg" class="GpYesterDayRepeaterDiv">
                                                    <div class="GpYesterDayRepeaterimgDiv">
                                                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                    </div>
                                                    <div id="YesterdaysGpSessionRepeater" class="GpYesterDayRepeatercontentDiv">
                                                        <div class="GpYstrDyReptcontentfstDiv">
                                                            <div class="GpYstrDyReptcontentfstinDiv">
                                                                <span class="GpYstrDyReptcontentfstinspn">
                                                                    <%# Eval("SessionName")%></span>
                                                                <asp:Label ID="Last7DaysReadingSessionSentTime" runat="server" style="float:left;" Text='<%# Eval("SessionCreatedDate") %>'></asp:Label>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentfstsndDiv">
                                                                <asp:Button ID="YesterdaysReviewButton" CssClass="GpReviewbtn" Text="REVIEW" runat="server"
                                                                    CommandArgument='<%# Eval("SessionID") %>' OnClick="ReviewButton_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentscndtDiv">
                                                            <img id="YGuidedImg" style="" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/unopen.png")%>" />
                                                        </div>
                                                        <asp:Label ID="YesterdaysGpId" runat="server" Text='<%# Eval("GroupId")%>' Visible="false"></asp:Label>
                                                        <div class="GpYstrDyReptcontentThrdtDiv">
                                                            <ul id="YWrapList" runat="server" clientidmode="Static" class="ReadingSessionBookWraper">
                                                            </ul>
                                                        </div>
                                                        <span></span>
                                                        <div class="GpYstrDyReptcontentFortDiv">
                                                            <div class="GpYstrDyReptcontentFortfstDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="YesterdaysUnReadCountLabel" runat="server" Text='<%# Eval("BookUnOpened") %>'
                                                                    Style="float: left; margin-left: -18px; margin-top: 14px; font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortfstspn">UNREAD</span>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentFortscndDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="YesterdaysReadCountLable" runat="server" Text='<%# Eval("BookOpened") %>' Style="float: left;
                                                                    padding-left: 49px; margin-top: 28px; font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070; position: absolute;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortscndspn">OPENED</span>
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentFithDiv">
                                                            <span>Note:</span>
                                                            <p>
                                                                <%# Eval("SessionNote")%>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv" id="LastSevenDaysHistoryDiv">
                <div id="LastSevenDaysRecordings" class="RecordingsHolder">
                    <div class="HistoryNodeDIv">
                        <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                    </div>
                    <div class="TodayHistory_Name" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv',this);">
                        <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                            <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                CssClass="StudentDashBoard_GuidedImage" />
                        </div>
                        <div class="TodayHistory_LabelDiv_Up">
                            <asp:Label ID="Last7DaysLabel" class="H4" runat="server" Text="Last Seven Days" /></div>
                    </div>
                </div>
                <div id="Last7DaysBorderDiv">
                    <div id="Last7DaysRecDiv" class="MyHistoryHolder_Content" style="display:none;">
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
                                            <asp:Image ID="BookCoverImg" AlternateText="CoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"
                                                CssClass ="History_Books" />
                                        </div>
                                        <div class="HistoryContent_BookDetails">
                                            <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                <%# Eval("StudentNames")%></p>
                                            <p class="HistoryContent_BookName">
                                                <%# Eval("Title")%></p>
                                            <p class="HistoryContent_DateTime">
                                                <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                            <asp:Literal ID="LastSevendaysIndMemberID" runat="server" Text='<%# Eval("UserID")%>'
                                                Visible="false"></asp:Literal>
                                            <asp:Literal ID="ProductId" runat="server" Text='<%# Eval("ProductId")%>' Visible="false"></asp:Literal>
                                            <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                <div class="RecordingButtonGradient">
                                                    <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                        onclick="ShowRecordings(this,'LastSevenDaysRecDiv<%# Container.ItemIndex %>')" />
                                                    <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this)">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="HistoryContent_ReadDetails">
                                            <div class="History_Book_ReadingDetails">
                                                <div class="History_WordCountBG">
                                                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_big.png")%>' style="width: 28px;
                                                        margin-top: 10px;" alt="" class="WordCountBG" />
                                                </div>
                                                <div class="History_WordCount" style="margin-top: -32px;">
                                                    <asp:Label ID="WordCount" runat="server" Text='<%# Eval("WordCount")%>'></asp:Label>
                                                </div>
                                                <div class="History_MyWordsLabel">
                                                    My Words
                                                </div>
                                            </div>
                                            <div class="History_ReadingTime">
                                                <div class="Div_FullWidth">
                                                    Book</div>
                                                <div class="Div_FullWidth">
                                                    opened</div>
                                                <div class="Div_FullWidth">
                                                    <b style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                                        font-weight: 700;">
                                                        <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                    </b>
                                                    <asp:Label ID="hourspan" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="LastSevenDaysRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                            <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                <div style="margin-bottom: 28px;">
                                                    <span class="PlayAll">Play all</span><img id="LastSevenDaysRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                        style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                    <div id="LastSevenDaysjPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                        <div id="LastSevenDaysjplayer<%# Container.ItemIndex %>" class="jp-jplayer">
                                                        </div>
                                                        <%--<div id="LastSevenDaysjplayerList" class="jp-jplayer"></div>--%>
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
                                                        <div id="LastSevenDaysjplayer_container2<%# Container.ItemIndex %>" class="jp-audio"
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
                                                <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                    <asp:Repeater ID="LastSevenDaysVideoRepeater" runat="server">
                                                        <ItemTemplate>
                                                            <div style="background-color: White;">
                                                                <span id="LastSevenDaysRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn"/>
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                        <AlternatingItemTemplate>
                                                            <div style="background-color: lightgray;">
                                                                <span id="LastSevenDaysRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                                    <%#Eval("RecordFilePath")%></span>
                                                                <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                    class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
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
                        <div class="GpRptcntdiv" style="padding-bottom: 15px;">
                            <div class="Gpguideddiv" id="L7SessionDiv">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater ID="Last7DaysReadingSessionRepeater" runat="server" OnItemDataBound="Last7DaysReadingSessionRepeater_ItemDataBound">
                                            <ItemTemplate>
                                                <div id="Last7DaysGpSessionCircleImg" class="GpYesterDayRepeaterDiv">
                                                    <div class="GpYesterDayRepeaterimgDiv">
                                                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                    </div>
                                                    <div id="Last7DaysGpSessionRepeater" class="GpYesterDayRepeatercontentDiv">
                                                        <div class="GpYstrDyReptcontentfstDiv">
                                                            <div class="GpYstrDyReptcontentfstinDiv">
                                                                <span class="GpYstrDyReptcontentfstinspn">
                                                                    <%# Eval("SessionName")%></span>
                                                                <asp:Label ID="Last7DaysReadingSessionSentTime" runat="server" style="float:left;" Text='<%# Eval("SessionCreatedDate") %>'></asp:Label>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentfstsndDiv">
                                                                <asp:Button ID="Last7DayReviewButton" CssClass="GpReviewbtn" Text="REVIEW" runat="server"
                                                                    CommandArgument='<%# Eval("SessionID") %>' OnClick="ReviewButton_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentscndtDiv">
                                                            <img id="L7GuidedImg" style="" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/unopen.png")%>" />
                                                        </div>
                                                        <asp:Label ID="Last7DaysGpId" runat="server" Text='<%# Eval("GroupId")%>' Visible="false"></asp:Label>
                                                        <div class="GpYstrDyReptcontentThrdtDiv">
                                                            <ul id="L7WrapList" runat="server" clientidmode="Static" class="ReadingSessionBookWraper">
                                                            </ul>
                                                        </div>
                                                        <span></span>
                                                        <div class="GpYstrDyReptcontentFortDiv">
                                                            <div class="GpYstrDyReptcontentFortfstDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="Last7DaysUnReadCountLabel" runat="server" Text='<%# Eval("BookUnOpened") %>'
                                                                    Style="float: left; margin-left: -18px; margin-top: 14px; font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortfstspn">UNREAD</span>
                                                            </div>
                                                            <div class="GpYstrDyReptcontentFortscndDiv">
                                                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                <asp:Label ID="Last7DaysReadCountLable" runat="server" Text='<%# Eval("BookOpened") %>' Style="float: left;
                                                                    padding-left: 49px; margin-top: 28px; font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
                                                                    font-weight: 700; font-size: 12pt; color: #707070; position: absolute;"></asp:Label>
                                                                <span class="GpYstrDyReptcontentFortscndspn">OPENED</span>
                                                            </div>
                                                        </div>
                                                        <div class="GpYstrDyReptcontentFithDiv">
                                                            <span>Note:</span>
                                                            <p>
                                                                <%# Eval("SessionNote")%>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="HistoryDiv">
                <div id="RestMonthDiv">
                    <asp:Repeater ID="RestMonthRepeater" ClientIDMode="Static" runat="server" OnItemDataBound="RestMonthRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div id="RestoftheMonthRecordings" class="RecordingsHolder">
                                <div class="HistoryNodeDIv">
                                    <asp:Image ID="RestNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                                </div>
                                <div class="TodayHistory_Name" onclick="toggle_visibility('RestRecordingsDiv<%# Container.ItemIndex %>','RestBorderDiv<%# Container.ItemIndex %>',this);">
                                    <div style="float: left; width: 8%; margin-left: 16px; margin-top: 10px;">
                                        <asp:Image ID="RestCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                            CssClass="StudentDashBoard_GuidedImage" />
                                    </div>
                                    <div class="TodayHistory_LabelDiv_Up">
                                        <asp:Label ID="RestMonthLabel" class="H4" runat="server" Text='<%# Container.DataItem %>' />
                                    </div>
                                </div>
                            </div>
                            <div id="RestBorderDiv<%# Container.ItemIndex %>" style=" min-height: 60px;display:block ">
                                <div id="RestRecordingsDiv<%# Container.ItemIndex %>" class="MyHistoryHolder_Content" style="display:none;">
                                    <asp:Repeater ID="RestIndependentHistory" runat="server" OnItemDataBound="RestIndependentHistory_ItemDataBound">
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
                                                        <asp:Image ID="BookCoverImg" AlternateText="CoverImage" ImageUrl='<%# Eval("BookImgName") %>' runat="server" ClientIDMode="Static"
                                                            CssClass ="History_Books" />
                                                    </div>
                                                    <div class="HistoryContent_BookDetails">
                                                        <p class="HistoryContent_BookName" style="color: #9D9D9D;">
                                                            <%# Eval("StudentNames")%></p>
                                                        <p class="HistoryContent_BookName">
                                                            <%# Eval("Title")%></p>
                                                        <p class="HistoryContent_DateTime">
                                                            <asp:Label ID="BookOpenTime" runat="server" Text='<%# Eval("BookOpenAt")%>'></asp:Label></p>
                                                        <asp:Literal ID="RestIndMemberID" runat="server" Text='<%# Eval("UserID")%>' Visible="false"></asp:Literal>
                                                        <asp:Literal ID="ProductId" runat="server" Text='<%# Eval("ProductId")%>' Visible="false"></asp:Literal>
                                                        <asp:Panel ID="RecordingButtonPanel" runat="server">
                                                            <div class="RecordingButtonGradient">
                                                                <input id="RecordingsButton" type="button" value="RECORDINGS" class="RecordButtonBackground"
                                                                    onclick="ShowRecordings(this,'RestIndenpRecDiv<%# ((RepeaterItem)Container.Parent.Parent).ItemIndex %><%# Container.ItemIndex %>')" />
                                                                <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this)">
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Label ID="BookOpenedMinsLabel" runat="server" Style="font-style: italic;" CssClass="HistoryContent_DateTime"></asp:Label>
                                                    </div>
                                                    <div class="HistoryContent_ReadDetails">
                                                        <div class="History_Book_ReadingDetails">
                                                            <div class="History_WordCountBG">
                                                                <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' style="width: 28px;
                                                                    margin-top: 10px;" alt="" class="WordCountBG" />
                                                            </div>
                                                            <div class="History_WordCount" style="margin-top: -32px;">
                                                                <asp:Label ID="WordCount" runat="server" Text='<%# Eval("WordCount")%>'></asp:Label>
                                                            </div>
                                                            <div class="History_MyWordsLabel">
                                                                My Words
                                                            </div>
                                                        </div>
                                                        <div class="History_ReadingTime">
                                                            <div class="Div_FullWidth">
                                                                Book
                                                            </div>
                                                            <div class="Div_FullWidth">
                                                                opened
                                                            </div>
                                                            <div class="Div_FullWidth">
                                                                <b style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                                                    font-weight: 700;">
                                                                    <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                                </b>
                                                                <asp:Label ID="hourspan" runat="server"></asp:Label>
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
                                                            <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                                <asp:Repeater ID="RestIndenpVideoRepeater" runat="server">
                                                                    <ItemTemplate>
                                                                        <div style="background-color: White;">
                                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                                <%#Eval("RecordFilePath")%></span>
                                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                            <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                                class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <AlternatingItemTemplate>
                                                                        <div style="background-color: lightgray;">
                                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                                <%#Eval("RecordFilePath")%></span>
                                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                            <asp:Label ID="PageCountLabel" Style="position: absolute; margin-top: 6px; margin-left: 3px;"
                                                                                class="PageCountLabel" Text='<%# Eval("PageName") %>' runat="server"></asp:Label>
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
                                    <div id="RMSession<%# Container.ItemIndex %>"class="GpRptcntdiv" style="padding-bottom: 15px;">
                                        <div class="Gpguideddiv">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="ReadingSessionRepeater" runat="server" OnItemDataBound="ReadingSessionRepeater_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div id="YesterDayRepeaterDiv" class="GpYesterDayRepeaterDiv">
                                                                <div class="GpYesterDayRepeaterimgDiv">
                                                                    <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                </div>
                                                                <div id="YesterDayContentDiv" class="GpYesterDayRepeatercontentDiv">
                                                                    <div class="GpYstrDyReptcontentfstDiv">
                                                                        <div class="GpYstrDyReptcontentfstinDiv">
                                                                            <span class="GpYstrDyReptcontentfstinspn">
                                                                                <%# Eval("SessionName")%></span>
                                                                            <asp:Label ID="ReadingSessionSentTime" runat="server" style="float:left;" Text='<%# Eval("SessionCreatedDate") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="GpYstrDyReptcontentfstsndDiv">
                                                                            <asp:Button ID="ReviewButton" CssClass="GpReviewbtn" Text="REVIEW" runat="server"
                                                                                CommandArgument='<%# Eval("SessionID") %>' OnClick="ReviewButton_Click" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="GpYstrDyReptcontentscndtDiv">
                                                                        <img id="RMGuidedImg" style="" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/unopen.png")%>" />
                                                                    </div>
                                                                    <asp:Label ID="GroupId" runat="server" Text='<%# Eval("GroupId")%>' Visible="false"></asp:Label>
                                                                    <div class="GpYstrDyReptcontentThrdtDiv">
                                                                        <ul id="WrpList" runat="server" clientidmode="Static" class="ReadingSessionBookWraper">
                                                                        </ul>
                                                                    </div>
                                                                    <span></span>
                                                                    <div class="GpYstrDyReptcontentFortDiv">
                                                                        <div class="GpYstrDyReptcontentFortfstDiv">
                                                                            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                            <asp:Label ID="UnReadCountLabel" runat="server" Text='<%# Eval("BookUnOpened") %>'
                                                                                Style="float: left; margin-left: -18px; margin-top: 14px; font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
                                                                                font-weight: 700; font-size: 12pt; color: #707070;"></asp:Label>
                                                                            <span class="GpYstrDyReptcontentFortfstspn">UNREAD</span>
                                                                        </div>
                                                                        <div class="GpYstrDyReptcontentFortscndDiv">
                                                                            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
                                                                            <asp:Label ID="ReadCountLable" runat="server" Text='<%# Eval("BookOpened") %>' Style="float: left;
                                                                                padding-left: 49px; margin-top: 28px; font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
                                                                                font-weight: 700; font-size: 12pt; color: #707070; position: absolute;"></asp:Label>
                                                                            <span class="GpYstrDyReptcontentFortscndspn">OPENED</span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="GpYstrDyReptcontentFithDiv">
                                                                        <span>Note:</span>
                                                                        <p>
                                                                            <%# Eval("SessionNote")%>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="LastNodeDivGroupProfile">
            <asp:Image ID="LastMonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
        </div>
    </div>
    <div id="StudentDiv"  runat="server" style="display: none;" class="GpStudentdiv">
        <asp:Panel ID="StudentContentPanel" runat="server">
            <asp:UpdatePanel ID="CheckALLUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="GroupTopDiv">
                        <div class="GroupSelectAll">
                            <div id="SelectAllDiv" style="float: left">
                                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
                                    id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal></div>
                            <span class="SelectAllSpan"  onclick="javascript:document.getElementById('SelectallCheckBox').click()">SELECT ALL</span>
                        </div>
                        <div class="GroupsSortReaddiv" style="width: auto; margin-top: 3px;">
                            <span style="float: left; font-size: 10pt; cursor: pointer; font-family: Raleway-regular,Raleway, Arial, sans-serif;
                                color: #707070;" onclick="javascript:document.getElementById('ReadingLevelButton').click();">
                                PM READING LEVEL</span>
                            <asp:Button ID="ReadingLevelButton" CommandName="Ascending" ClientIDMode="Static"
                                UseSubmitBehavior="false" Style="width: 1px  !important; background-position: 99% 55%;
                                float: left; margin-top: -1px;" runat="server" CssClass="SortRead Reading" OnClick="ReadingLevelButton_Click" />
                            <b style="float: left; margin-top: 0px; margin-left: 3px; font-family: Raleway-regular,Raleway, Arial, sans-serif;
                                color: #707070;">|</b> <span style="float: left; cursor: pointer; font-size: 10pt;
                                    margin-left: 10px;" onclick="javascript:document.getElementById('SortingButton').click();">
                                    A – Z</span>
                            <asp:Button ID="SortingButton" CssClass="SortRead Sort" runat="server" ClientIDMode="Static"
                                UseSubmitBehavior="false" CommandName="Ascending" Style="width: 1px !important;
                                margin-right: 15px !important;" OnClick="SortingButton_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="margin-top: -1px" class="GpStudentleftlnglne">
            </div>
            <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
                <div class="ProgressInnerDiv">
                    <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"  alt="Processing" /> 
                </div>
            </div>
            <div class="SearchDiv" style="width: 627px">
                <div class="SearchInnerDiv" style="width: 627px">
                    <asp:UpdatePanel ID="SearchUpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <input type="text" id="SearchTextBox" clientidmode="Static" class="classSearchwater"
                                autocomplete="off" style="width: 540px !important;" runat="server" title="Enter your search here ..." />
                            <div class="Searchbtndiv">
                                <asp:Button ID="SearchButton" runat="server" CssClass="SearchButton" OnClick="SearchButton_Click" OnClientClick="ShowUpdate()"  />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="GpStudentleftlnglne" style="min-height: 40px;" id="LeftConnectorLine">
            </div>
            <div class="GptStudentRptdiv" style="padding-bottom: 0px;" id="RepeaterLftConntrLine">
                <div id="RepeaterStudentDiv" class="GpRptitmcontentdiv" style="margin-bottom: -22px;
                    margin-top: 17px;">
                    <asp:UpdatePanel ID="StudentRepeaterUpdatePanel" ClientIDMode="Static" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Repeater ID="StudentRepeater" runat="server" OnItemDataBound="StudentRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <div id="StudentRepeaterDiv" class="GpStudentRptcontentdiv">
                                        <div id="StudentRepeaterContentDiv" class="GpStudentRptcontentinnerdiv" style="width: 94.2%">
                                            <div class="GpStudentRptcontentinrfstdiv">
                                                <input id="StudentCheckBoxes" clientidmode="Static" type="checkbox" style="display: none" />
                                                <asp:Literal ID="StudentId" runat="server" Text='<%# Eval("UserID") %>' Visible="false"></asp:Literal>
                                                <img id="StudentCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                            </div>
                                            <div class="GpStudentRptcontentinsnddiv">
                                                <asp:Label ID="StudentNameLabel" CssClass="CreateStTchlbl" runat="server" Text='<%# Eval("StudentNames") %>'></asp:Label>
                                                <asp:Label ID="StudentLoginName" runat="server" Text='<%# string.Format("({0})",Eval("StudentLoginName"))%>'
                                                    Style="color: #707070; font-weight: bold; font-family: Raleway-regular,Raleway,Arial,Sans-serif;
                                                    font-size: 11pt; padding-left: 3px; padding-right: 3px; float: left;"></asp:Label>
                                            </div>
                                            <div class="GpStudentRptcontentinthrdiv">
                                                <asp:Button ID="ClassProfileButton" runat="server" CssClass="GpStudentRptcontentinthrbtn" formtarget="_blank"
                                                    Text="PROFILE" OnClick="ClassProfileButton_Click" CommandArgument='<%# Eval("StudentLoginName").ToString().ToLower() %>' />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%-- <div class="AllOtherGroupsImgDiv" style="margin-top: -40px;">
                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
            </div>--%>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="pointsHdn" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="grfHdnLine" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="leftcolorreading" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="rightcolorreading" runat="server" ClientIDMode="Static" />
</div>
<script type="text/javascript" language="javascript">
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    jQuery(document).ready(function () {

        var searchAutoComplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutoComplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx?Search=groupStudentAutoComplete'),
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
        var ver = getInternetExplorerVersion();
        if (ver > -1) {
            if (ver != 8.0) {
                $("#divcircle").corner("50px");
                $("#divcircletwo").corner("50px");
            }
        }
        var displayProperty = 0;
        var expandCurrentTab = 0;



        if ($("#TodaysRecordingsDiv")[0].childElementCount == 1 && $("#TSessionDiv").children()[0].childElementCount == 0) {
            $("#TodaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#TodaysBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;

        }

        if ($("#YesterdaysRecordingsDiv")[0].childElementCount == 1 && $("#YSessionDiv").children()[0].childElementCount==0) {
            $("#YesterdaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#YesterdaysBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;
            $("#YesterdaysHistoryDiv").attr("style", "margin-top:-30px!important");
        }
        if ($("#Last7DaysRecDiv")[0].childElementCount == 1 && $("#L7SessionDiv").children()[0].childElementCount == 0) {
            $("#Last7DaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#Last7DaysBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;
            $("#LastSevenDaysHistoryDiv").attr("style", "margin-top:-30px!important");

        }
        if ($("#RestMonthDiv")[0].children.length == 0) {
            $("#RestMonthDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#RestMonthDiv")[0].children[0].children[1].click();
            }
            //$("#RestMonthDiv")[0].parentNode.style.marginTop = "-15px";
        }
        if (displayProperty == 4) {
            $(".LastNodeDivGroupProfile").css("display", "none");
        }

        var ids = $('#pointsHdn').val().split('-');
        var ids2 = $('#grfHdnLine').val();
        for (var i = 0; i < (12 - ids2) + 1; i++) {
            $('#Graph1 #LineDiv' + i + ' div').addClass('NoBackground');
            //            $('#Graph1 #LineDiv' + i).hide();
        }
        for (var i = 12 - ids2 + 1; i < 12; i++) {
            $('#Graph1 #LineDiv' + i + ' div').addClass('NoBackground');
            $('#Graph1 #LineDiv' + i).hide();
        }
        for (var i = 0; i < ids.length; i++) {
            $('#Div' + ids[i]).removeClass('NoBackground');
            $('#Div' + ids[i]).parent().show();
            if (ids[i] != "") {
                if (ver != 8.0) {
                    $('#Div' + ids[i]).corner("90px");
                }
            }
        }
//        if ($("#<%=GroupNameLabel.ClientID %>")[0].innerHTML.length > 20) {
//            $("#lblbannertxt")[0].innerHTML = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + $("#<%=GroupNameLabel.ClientID %>")[0].innerHTML.substring(0, 20) + "...</font>";
//        }
//        else {
//            $("#lblbannertxt")[0].innerHTML = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + $("#<%=GroupNameLabel.ClientID %>")[0].innerHTML + "</font>";
        //        }

        //        $(".GpYstrDyReptcontentThrdtDiv").easySlider({
        //            controlsBefore: '<p id="controls">',
        //            controlsAfter: '</p>',
        //            auto: false,
        //            continuous: false
        //        });

        $("#level").text(" ");
        $("#slider-range").css({ "display": "inline-block", "margin-left": "16px", "margin-top": "8px" });

        var contentheit = $('#eCollectionContent').height();

        $('#eCollectionMenu').height(contentheit + 'px');
        if ($.browser.msie || $.browser.mozilla) {
            $("#Boldline").css("margin-top", "1px");
        }
        if ($.browser.msie || $.browser.mozilla) {
        }
        else {
            if (contentheit != 1617)
                $('#eCollectionContent').height((contentheit + 300) + 'px');
        }
        var constantheight = $('#eCollectionContent').height();
        jQuery("#YesterDayContentDiv input[type=button]").click(function () {
            return false;
        });


        jQuery('#eCollectionMenu').height(jQuery('#eCollectionContent').height() + 'px');
        jQuery('#eCollectionContent').height((jQuery('#eCollectionContent').height() + 9) + 'px');
        jQuery("#ClassRepeaterDiv input[type=button]").click(function () {
            return false;
        });
        //jQuery('#Switch').css({ 'background-image': 'url("../../Portals/0/images/history on.png")' });

        jQuery("#StudentsButton").click(function () {
            $('#HistoryDiv').stop().fadeTo(300, 0);
            $('#<%=StudentDiv.ClientID%>').stop().fadeTo(300, 1);
            jQuery("#HistoryDiv").css("display", "none");
            jQuery("#<%=StudentDiv.ClientID%>").css({ "display": "block" });
            showtab();
            var msg2 = $('#<%=Message2.ClientID%>').text();
            jQuery('#Switch').addClass('student_ON');
            if (msg2 == "") {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "none");
            }
            else {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "block");
                jQuery("#<%=Message1.ClientID%>").css("display", "none");
                jQuery("#<%=Message2.ClientID%>").css("display", "block");
                $('#<%=StudentDiv.ClientID%>').stop().fadeTo(300, 0);
                jQuery("#<%=StudentDiv.ClientID%>").css("display", "none");
            }
            return false;
        });
        jQuery("#HistoryButton").click(function () {
            $('#<%=StudentDiv.ClientID%>').stop().fadeTo(300, 0);
            $('#HistoryDiv').stop().fadeTo(300, 1);
            jQuery("#<%=StudentDiv.ClientID%>").css("display", "none");
            jQuery("#HistoryDiv").css({ "display": "block" });
            var msg1 = $('#<%=Message1.ClientID%>').text();
            jQuery('#Switch').removeClass('student_ON');
            if (msg1 == "") {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "none");
            }
            else {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "block");
                jQuery("#<%=Message2.ClientID%>").css("display", "none");
                jQuery("#<%=Message1.ClientID%>").css("display", "block");
            }
            showtab();
            return false;
        });
        jQuery("#RecordingsDiv div input[type=image]").click(function () {
            var contentht;
            var element = this.nextSibling.nextSibling.nextSibling.nextSibling;
            if (element.style.display == "none") {
                contentht = $('#PlayRecordDiv').height();
                element.style.display = "block";
                var contentheight = $('#eCollectionContent').height();
                $('#eCollectionMenu').height(contentheight + 'px');
                $('#eCollectionContent').height((contentheight + contentht) + 'px');
            }
            else {
                element.style.display = "none";
                contentht = $('#PlayRecordDiv').height();
                var contentheight = $('#eCollectionContent').height();
                $('#eCollectionMenu').height(contentheight + 'px');
                contentht = contentht + 10;
                $('#eCollectionContent').height(contentheight + 'px');
            }
            return false;
        });
        jQuery("#IndependentContentDiv div input[type=button]").click(function () {
            var contentht;
            if (this.className == "RecordingsMax") {
                contentht = $('#RecordingsDiv').height();
                jQuery(this.parentNode.nextSibling).hide();
                jQuery("#RecordingsDiv").css("display", "block");
                jQuery(this).removeClass("RecordingsMax");
                jQuery(this).addClass("RecordingsMin");
                var contentheight = $('#eCollectionContent').height();
                $('#eCollectionMenu').height(contentheight + 'px');
                $('#eCollectionContent').height((contentheight + contentht) + 'px');
            }
            else {
                jQuery(this.parentNode.nextSibling).show();
                jQuery("#RecordingsDiv").css("display", "none");
                jQuery(this).removeClass("RecordingsMin");
                jQuery(this).addClass("RecordingsMax");
                contentht = $('#RecordingsDiv').height();
                var contentheight = $('#eCollectionContent').height();
                $('#eCollectionMenu').height((constantheight + 8) + 'px');

                $('#eCollectionContent').height((constantheight + 8) + 'px');
            }

            return false;
        });



        $("#TodaySearchTextBox").kendoComboBox({
            placeholder: "Your Class",
            dataTextField: "Name",
            dataValueField: "Id",
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx'),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverFiltering: true,
                        serverPaging: true,
                        pageSize: 20
                    }
                }
            }
        });
        $("#YesterdayTextBox").kendoComboBox({
            placeholder: "Your Class",
            dataTextField: "Name",
            dataValueField: "Id",
            filter: "contains",
            dataSource: {
                transport: {
                    read: {
                        url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx'),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverFiltering: true,
                        serverPaging: true,
                        pageSize: 20
                    }
                }
            }
        });
        if ($.browser.msie) {
            // $('#<%=ReadingLevelButton.ClientID %>').removeClass('Reading').css({ "padding-right": "15px", "width": "155px" });
        }
        //         jQuery('#HistoryAccordion').accordion({ active: false, alwaysOpen: false, event: "mouseover", autoHeight: false });
        if ($.browser.msie) {
            $("#divcircle").removeClass("divcircleleft");
            var leftcolord = document.getElementById("leftcolorreading").value;
            $("#divcircle").css({ "width": "100px", "height": "100px", "background-color": leftcolord, "float": "left" });
            var rightcolord = document.getElementById("rightcolorreading").value;
            $("#divcircletwo").removeClass("divcircleright");
            $("#divcircletwo").css({ "width": "100px", "height": "100px", "background-color": rightcolord, "float": "left" });
        }
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
    });
    function RecordButtonImg(e) {
        e.parentNode.children[0].click();
    }
    function hidetab() {
        jQuery('#eCollectionContent').height((jQuery('.GroupprofileContentDiv').height() + jQuery('#SecondDiv').height() + 30) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('.GroupprofileContentDiv').height() + jQuery('#SecondDiv').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function PostBack() {
        var version = getInternetExplorerVersion();
        if ($.browser.msie) {
            var textvalue = $("#SearchTextBox").val();
            $("#SearchTextBox").val('Enter your search here ...');
            if (version > -1) {
                if (version != 9.0 && version != 10.0) {
                    if (textvalue.trim().length != 0) {
                        $("#SearchTextBox").val(textvalue);
                    }
                    $("#SearchTextBox").css("line-height", "27.5px");
                }
                else {
                    $("#SearchTextBox").val(textvalue);
                }

            }
        }

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;     
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

                if (code == 13) {
                    e.preventDefault();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
            }

        });
        $('#SearchTextBox').keyup(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;     
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }

            }
        });
        $('#SearchTextBox').keyup(function (e) {
            $('#SearchTextBox').focus();
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                if (code == 13) {
                    e.preventDefault();
                    $('#<%=SearchButton.ClientID%>').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
            }
        });
        $("#SearchTextBox").focus(function () {
            if (jQuery(this).val() == this.title) {
                jQuery(this).val("");
            }
        });
        $("#SearchTextBox").blur(function () {
            if (jQuery(this).val().trim() == "") {
                //jQuery(this).val(this.title);
            }
        });

        var checkallFlag = true;
        $("#SelectallCheckBox").click(function () {
            if (checkallFlag) {
                checkallFlag = false;
                for (var i = 0; i < $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv")[i].className = "rowclick GpStudentRptcontentinnerdiv";
                    }


                }
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");

            }
            else {
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                checkallFlag = true;
                for (var i = 0; i < $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv")[i].className = "GpStudentRptcontentinnerdiv";
                    }

                }
            }

        });
        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv")[i].className = "rowclick GpStudentRptcontentinnerdiv";
                        count++;
                    }
                    else {

                    }
                }

                if ($("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length == count) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }

            }
            else {
                var count = 0;
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                //checkallFlag = true;
                for (var i = 0; i < $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterStudentDiv #StudentRepeaterDiv #StudentRepeaterContentDiv")[i].className = "GpStudentRptcontentinnerdiv";
                    }
                }
                if (count != 0)
                    checkallFlag = true;

            }
        });
    }
</script>
<script type="text/javascript">
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
        jQuery('#eCollectionContent').height((jQuery('.GroupprofileContentDiv').height() + jQuery('#SecondDiv').height() + 30) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('.GroupprofileContentDiv').height() + jQuery('#SecondDiv').height() + 80) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function toggle_visibility(ContentDivId, BorderDivId, contrl) {
        var e = document.getElementById(ContentDivId);
        var d = document.getElementById(BorderDivId);
        if ($(e).is(':visible')) {
            $(e).fadeToggle("slow", "linear");
            contrl.children[1].className = "TodayHistory_LabelDiv_Up";
            setTimeout("hidetab()", 700);
        }
        else {
            $(e).fadeToggle("slow", "linear");
            contrl.children[1].className = "TodayHistory_LabelDiv";
            showtab();
        }


    }
    jQuery(function () {
        if (jQuery($("[id$=StyleApplier]")).val() != "Empty") {

        }
        jQuery('#level').hide();
        jQuery('#amount').hide();

    });

    function ShowRecordings(id, ContentDivId) {

        if (id.value == "RECORDINGS") {
            jQuery(id.parentNode.nextSibling).hide();
            //id.parentNode.style.width = "60%";
            $("#" + ContentDivId).stop().fadeTo(300, 1);
            jQuery("#" + ContentDivId).css("display", "block");
            id.value = "MINIMIZE";
            jQuery(id.parentNode.children[1]).addClass("sortimage");
            showtab();
        }
        else {
            jQuery(id.parentNode.nextSibling).show();
            jQuery(id.parentNode.children[1]).removeClass("sortimage");
            $("#" + ContentDivId).stop().fadeTo(300, 0);
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
            swfPath: GetFile("/Portals/0/Jplayer.swf"),
            supplied: "m4a",
            wmode: "window",
            errorAlerts: false,
            playlistOptions: {
                autoPlay: true,
                enableRemoveControls: true
            }
        }).bind($.jPlayer.event.play, function () { // pause other instances of player when current one play
            $(this).jPlayer("pauseOthers");
        });
    }
    function Hide(obj) {
        $(obj).css("display","none");
        
    }
</script>
