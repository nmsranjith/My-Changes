<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupRecordings.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupRecordings" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
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
        margin: 0;
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
    .ReadingSessionBookWraper
    {
        width: 216px;
        list-style: none;
        overflow: scroll;
        height: 120px;
    }
    .ReadingSessionBookWraper li
    {
        display: inline;
    }
    .GpYstrDyReptcontentFortscndDiv img
    {
        height: 28px;
        width: 27px;
        margin-left: 47px;
        float: left;
        margin-top: 10px;
    }
</style>
<div id="MessagesDiv">
    <uc1:Messages ID="Messages" runat="server" />
</div>
<div id="FirstDiv" class="MyRecordings_TopDiv">
    <asp:Label ID="GroupNameLabel" runat="server" Style="display: none"></asp:Label>
    <div id="MyWordsHeader" class="MyWordsHeader" style="width: 46%;">
        Recordings</div>
    <div id="MyWordsCount" class="MyWordsCount" style="float: left; margin-left: -83px;
        margin-top: 29px;">
        <span id="RecordingsCount" style="margin-right: 5px;" runat="server"></span>RECORDING/S
        SAVED</div>
</div>
<div id="HistoryDiv" style="display: block; float: left; width: 100%;">
    <asp:Button ID="BackToProfile" runat="server" Text="Back to your profile" Style="width: 26%;
        font-size: 10pt; border: 0px solid white; cursor: pointer; margin-left: -5px;
        float: left; margin-top: 20px; background-color: transparent; color: #1FB5E7;
        outline: none; font-family: Raleway-regular, Raleway,Arial, Sans-Serif; font-weight: 500;"
        OnClick="BackToProfile_Click" />
    <div id="HistoryMainDiv" style="width: 97.1% !important; float: left; border-left: 1px solid lightgray !important;
        margin: 38px 0px 0px 24px !important; padding-bottom: 1px;">
        <div class="HistoryDiv" >
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
                <div id="TodaysRecordingsDiv" class="MyHistoryHolder_Content1" style="display:none">
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
                                                <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this);">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div class="HistoryContent_ReadDetails" style="height: 144px">
                                        <div class="History_ReadingTime" style="margin-top: 45px;">
                                        <asp:Label ID="WordCount" runat="server" Visible="false" Text='<%# Eval("WordCount")%>'></asp:Label>
                                            <div class="Div_FullWidth">
                                                Book</div>
                                            <div class="Div_FullWidth">
                                                opened</div>
                                            <div class="Div_FullWidth">
                                                <b style="color: #707070; font-size: 12pt; font-family: Raleway-ExtraBold,Raleway,Arial, Sans-Serif;">
                                                    <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                </b>
                                                <asp:Label ID="hourspan" runat="server"></asp:Label></div>
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
                                                <asp:Repeater ID="TodayIndeVideoRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <div style="background-color: White;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background-color: lightgray;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
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
                    <div class="TodayHistory_LabelDiv_Up">
                        <asp:Label ID="YesterdayLabel" class="H4" runat="server" Text="  Yesterday" /></div>
                </div>
            </div>
            <div id="YesterdaysBorderDiv">
                <div id="YesterdaysRecordingsDiv" class="MyHistoryHolder_Content"  style="display:none">
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
                                                <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this);">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div class="HistoryContent_ReadDetails" style="height: 144px;">
                                        <div class="History_ReadingTime" style="margin-top: 45px;">
                                        <asp:Label ID="WordCount" runat="server" Visible="false" Text='<%# Eval("WordCount")%>'></asp:Label>
                                            <div class="Div_FullWidth">
                                                Book</div>
                                            <div class="Div_FullWidth">
                                                opened</div>
                                            <div class="Div_FullWidth">
                                                <b style="color: #707070; font-size: 12pt; font-family: Raleway-ExtraBold,Raleway,Arial, Sans-Serif;">
                                                    <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                </b>
                                                <asp:Label ID="hourspan" runat="server"></asp:Label></div>
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
                                            <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                <asp:Repeater ID="YesterdayIndVideoRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <div style="background-color: White;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background-color: lightgray;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
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
                <div id="Last7DaysRecDiv" class="MyHistoryHolder_Content"  style="display:none">
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
                                                    onclick="ShowRecordings(this,'LastSevenDaysIndeRecDiv<%# Container.ItemIndex %>')" />
                                                <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this);">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div class="HistoryContent_ReadDetails" style="height: 144px;">
                                        <div class="History_ReadingTime" style="margin-top: 45px">
                                        <asp:Label ID="WordCount" runat="server" Visible="false" Text='<%# Eval("WordCount")%>'></asp:Label>
                                            <div class="Div_FullWidth">
                                                Book</div>
                                            <div class="Div_FullWidth">
                                                opened</div>
                                            <div class="Div_FullWidth">
                                                <b style="color: #707070; font-size: 12pt; font-family: Raleway-ExtraBold,Raleway,Arial, Sans-Serif;">
                                                    <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                </b>
                                                <asp:Label ID="hourspan" runat="server"></asp:Label></div>
                                        </div>
                                    </div>
                                    <div id="LastSevenDaysIndeRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                        <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                            <div style="margin-bottom: 28px;">
                                                <span class="PlayAll">Play all</span><img id="LastSevenDaysRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                    style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                <div id="LastSevenDaysjPlayerDiv<%# Container.ItemIndex %>" style="display: none">
                                                    <div id="LastSevenDaysjplayer<%# Container.ItemIndex %>" class="jp-jplayer">
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
                                                <asp:Repeater ID="LastSevenDaysVideoRepeater" runat="server">
                                                    <ItemTemplate>
                                                        <div style="background-color: White;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <div style="background-color: lightgray;">
                                                            <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                <%#Eval("RecordFilePath")%></span>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
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
        <div class="HistoryDiv" style="margin-top: 0px">
            <div id="RestMonthDiv">
                <asp:Repeater ID="RestMonthRepeater" runat="server" OnItemDataBound="RestMonthRepeater_ItemDataBound">
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
                                    <asp:Label ID="RestMonthLabel" class="H4" runat="server" Text='<%# Container.DataItem %>' /></div>
                            </div>
                        </div>
                        <div id="RestBorderDiv<%# Container.ItemIndex %>" style="min-height:60px;">
                            <div id="RestRecordingsDiv<%# Container.ItemIndex %>" class="MyHistoryHolder_Content"  style="display:none">
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
                                                                onclick="ShowRecordings(this,'RestIndenpRecDiv<%# Container.ItemIndex %>')" />
                                                            <div class="RecordButtonRevBackground" onclick="javascript:RecordButtonImg(this);">
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Label ID="BookOpenedMinsLabel" Style="font-style: italic;" CssClass="HistoryContent_DateTime"
                                                        runat="server"></asp:Label>
                                                </div>
                                                <div class="HistoryContent_ReadDetails" style="height: 144px;">
                                                    <div class="History_ReadingTime" style="margin-top: 45px">
                                                    <asp:Label ID="WordCount" runat="server" Visible="false" Text='<%# Eval("WordCount")%>'></asp:Label>
                                                        <div class="Div_FullWidth">
                                                            Book</div>
                                                        <div class="Div_FullWidth">
                                                            opened</div>
                                                        <div class="Div_FullWidth">
                                                            <b style="color: #707070; font-size: 12pt; font-family: Raleway-ExtraBold,Raleway,Arial, Sans-Serif;">
                                                                <asp:Label ID="BookOpenedMin" runat="server" Text='<%# Eval("BooksOpenedMin")%>'></asp:Label>
                                                            </b>
                                                            <asp:Label ID="hourspan" runat="server"></asp:Label></div>
                                                    </div>
                                                </div>
                                                <div id="RestIndenpRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
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
                                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                            runat="server"></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <div style="background-color: lightgray;">
                                                                        <span id="RecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                            <%#Eval("RecordFilePath")%></span>
                                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayAudBtn" />
                                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
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
                                
                             <%--   <div class="GpRptcntdiv" style="padding-bottom: 215px;">
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
                                                                        <asp:Label ID="ReadingSessionSentTime" runat="server" Text='<%# Eval("SessionCreatedDate") %>'></asp:Label>
                                                                    </div>
                                                                    <div class="GpYstrDyReptcontentfstsndDiv">
                                                                        <asp:Button ID="ReviewButton" CssClass="GpReviewbtn" Text="REVIEW" runat="server"
                                                                            CommandArgument='<%# Eval("SessionID") %>' OnClick="ReviewButton_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="GpYstrDyReptcontentscndtDiv">
                                                                    <img id="GuidedImg" style="" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/unopen.png")%>" />
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
                                                                            padding-left: 55px; margin-top: 30px; font-family: Raleway-regular,Raleway,Arial,Sans-Serif;
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
                                </div>--%>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div style="width: 100%; float: left; margin-left: 17px;">
        <asp:Image ID="LastMonthNodeImage" ClientIDMode="Static" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
    </div>
</div>
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
        jQuery('#eCollectionContent').height((jQuery('.GroupprofileContentDiv').height() + jQuery('#SecondDiv').height() + 50) + 'px');
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
    function RecordButtonImg(e) {
        e.parentNode.children[0].click();
    }
    jQuery(function () {
        var displayProperty = 0;
        var expandCurrentTab = 0;
        if ($("#TodaysBorderDiv div")[0].children.length == 0) {
            $("#TodaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#TodaysBorderDiv")[0].parentNode.children[0].children[1].click();
                showtab();
            }
            expandCurrentTab++;

        }
        if ($("#YesterdaysBorderDiv div")[0].children.length == 0) {
            $("#YesterdaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#YesterdaysBorderDiv")[0].parentNode.children[0].children[1].click();
                showtab();
            }
            expandCurrentTab++;
            $("#YesterdaysHistoryDiv").attr("style", "margin-top:-30px!important");
        }
        if ($("#Last7DaysBorderDiv div")[0].children.length == 0) {
            $("#Last7DaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            if (expandCurrentTab == 0) {
                $("#Last7DaysBorderDiv")[0].parentNode.children[0].children[1].click();
                showtab();
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
                showtab();
            }
            //$("#RestMonthDiv")[0].parentNode.style.marginTop = "-15px";
        }
        if (displayProperty == 4) {
            $("#LastMonthNodeImage").css("display", "none");
        }
//        if ($("#<%=GroupNameLabel.ClientID %>")[0].innerHTML.length > 20) {
//            $("#lblbannertxt")[0].innerHTML = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + $("#<%=GroupNameLabel.ClientID %>")[0].innerHTML.substring(0, 20) + "...</font>";
//        }
//        else {
//            $("#lblbannertxt")[0].innerHTML = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + $("#<%=GroupNameLabel.ClientID %>")[0].innerHTML + "</font>";
//        }
        if (jQuery($("[id$=StyleApplier]")).val() != "Empty") {

        }
        showtab();
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
    function hidetab() {
        jQuery('#eCollectionContent').height((jQuery('#HistoryDiv').height() + jQuery('#FirstDiv').height() + 120) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('#HistoryDiv').height() + jQuery('#FirstDiv').height() + 120) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
</script>
