<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReadingHistory.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.ReadingHistory" %>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Teachers/CSS/jplayer.css")%>"
    rel="Stylesheet" type="text/css" />
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Teachers/CSS/jplayer.blue.monday.css")%>"
    rel="Stylesheet" type="text/css" />
<div id="HistoryMainDiv" class="HistoryMainDiv" runat="server" clientidmode="Static">
    <div id="TodayHistory" runat="server" clientidmode="Static" class="HistoryDiv" style="margin-top: -31px;">
        <div id="TodaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" id="TodaysHistToggle" onclick="toggle_visibility('TodaysRecordingsDiv','TodaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv His_LabelDivDown">
                    <asp:Label ID="TodayLabel" runat="server" Text="  Today" /></div>
            </div>
        </div>
        <div id="TodaysBorderDiv">
            <div id="TodaysRecordingsDiv" class="MyHistoryHolder_Content1">
                <asp:ListView ID="TodaysIndependentRecordings" runat="server" DataKeyNames="OpenedDate"
                    OnItemDataBound="IndependentGrid_RowDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl='<%# string.Format("~/Portals/0/images/{0}",Eval("SessionTypeImage"))%>'
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                    <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl='<%#Eval("FileName") %>' ClientIDMode="Static"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("Title") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# DateTime.Parse(Eval("BookOpenedTime").ToString()).ToString("HH:mm") %>
                                        <%# string.Format(" {0} the {1:dd}{2}", DateTime.Now.DayOfWeek,DateTime.Now, (DateTime.Now.Day == (1 | 21 | 31)) ? "st" : (DateTime.Now.Day == (2 | 22)) ? "nd" : (DateTime.Now.Day == (3 | 23)) ? "rd" : "th")%></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'TodayIndepRecDiv<%# Container.DataItemIndex %>')" />
                                        <asp:HiddenField ID="RecBtnHdn" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            <asp:Label ID="WordCntLbl" runat="server"></asp:Label>
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
                                            <b style="font-size: 11pt;">
                                                <%# int.Parse(Eval("Minutes").ToString()) >=60 ? string.Concat((int.Parse(Eval("Minutes").ToString()) / 60).ToString("0"), '.', (int.Parse(Eval("Minutes").ToString()) % 60).ToString("00")) : string.Concat(int.Parse(Eval("Minutes").ToString()).ToString("0"), '.', int.Parse(Eval("Seconds").ToString()).ToString("00"))%></b>
                                            <%# int.Parse(Eval("Minutes").ToString()) >=60 ? " hrs":" mins" %>
                                        </div>
                                    </div>
                                </div>
                                <div id="TodayIndepRecDiv<%# Container.DataItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 28px;">
                                            <span class="PlayAll">Play all</span><img id="TodayIndepRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                            <div id="TodayIndepRecjPlayerDiv<%# Container.DataItemIndex %>" style="display: none">
                                                <div id="TodayIndepRecjpId<%# Container.DataItemIndex %>" class="jp-jplayer">
                                                </div>
                                                <div id="TodayIndepRecjpId_container_1<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                    shuffle off</a></li>
                                                                <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                    repeat off</a></li>
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
                                                <div id="TodayIndepRecjpId_container_2<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                    <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                        repeat</a></li>
                                                                    <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                        repeat off</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
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
                                            <asp:Repeater ID="VideoGridView" runat="server">
                                                <ItemTemplate>
                                                    <div style="background-color: White;" class="Div_FullWidth">
                                                        <span id="TodayIndepRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background-color: lightgray;" class="Div_FullWidth">
                                                        <span id="TodayIndepRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>                
            </div>
        </div>
    </div>
    <div id="YesterdayHistory" runat="server" clientidmode="Static" class="HistoryDiv">
        <div id="YesterDaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="YesterdayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" id="YestHistToggle" onclick="toggle_visibility('YesterdaysRecordingsDiv','YesterdaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="YesterdayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv His_LabelDivUp">
                    <asp:Label ID="YesterdayLabel" runat="server" Text="  Yesterday" /></div>
            </div>
        </div>
        <div id="YesterdaysBorderDiv">
            <div id="YesterdaysRecordingsDiv" class="MyHistoryHolder_Content">
                <asp:ListView ID="YesterDayIndependentRecordings" runat="server" DataKeyNames="OpenedDate"
                    OnItemDataBound="IndependentGrid_RowDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl='<%# string.Format("~/Portals/0/images/{0}",Eval("SessionTypeImage"))%>'
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                    <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl='<%#Eval("FileName") %>' ClientIDMode="Static"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("Title") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# DateTime.Parse(Eval("BookOpenedTime").ToString()).ToString("HH:mm") %><%# string.Format(" {0} the {1:dd}{2}", DateTime.Now.AddDays(-1).DayOfWeek, DateTime.Now.AddDays(-1), (DateTime.Now.AddDays(-1).Day == (1 | 21 | 31)) ? "st" : (DateTime.Now.AddDays(-1).Day == (2 | 22)) ? "nd" : (DateTime.Now.AddDays(-1).Day == (3 | 23)) ? "rd" : "th")%></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'YesterdayIndepRecDiv<%# Container.DataItemIndex %>')" />
                                        <asp:HiddenField ID="RecBtnHdn" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            <asp:Label ID="WordCntLbl" runat="server"></asp:Label>
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
                                            <b style="font-size: 11pt;">
                                                <%# int.Parse(Eval("Minutes").ToString()) >=60 ? string.Concat((int.Parse(Eval("Minutes").ToString()) / 60).ToString("0"), '.', (int.Parse(Eval("Minutes").ToString()) % 60).ToString("00")) : string.Concat(int.Parse(Eval("Minutes").ToString()).ToString("0"), '.', int.Parse(Eval("Seconds").ToString()).ToString("00"))%></b>
                                            <%# int.Parse(Eval("Minutes").ToString()) >=60 ? " hrs":" mins" %>
                                        </div>
                                    </div>
                                </div>
                                <div id="YesterdayIndepRecDiv<%# Container.DataItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 28px;">
                                            <span class="PlayAll">Play all</span><img id="YesterdayIndepRecDivPlayAllButton"
                                                onclick="PlayAllAudio(this)" style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                            <div id="YesterdayIndepRecjPlayerDiv<%# Container.DataItemIndex %>" style="display: none">
                                                <div id="YesterdayIndepRecjpId<%# Container.DataItemIndex %>" class="jp-jplayer">
                                                </div>
                                                <div id="YesterdayIndepRecjpId_container_1<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                    shuffle off</a></li>
                                                                <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                    repeat off</a></li>
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
                                                <div id="YesterdayIndepRecjpId_container_2<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                    <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                        repeat</a></li>
                                                                    <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                        repeat off</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
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
                                            <asp:Repeater ID="VideoGridView" runat="server">
                                                <ItemTemplate>
                                                    <div style="background-color: White;" class="Div_FullWidth">
                                                        <span id="YesterdayIndepRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background-color: lightgray;" class="Div_FullWidth">
                                                        <span id="YesterdayIndepRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>               
            </div>
        </div>
    </div>
    <div id="Last7DaysHistory" runat="server" clientidmode="Static" class="HistoryDiv">
        <div id="LastSevenDaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" id="Last7DToggle" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv His_LabelDivUp">
                    <asp:Label ID="Last7DaysLabel" runat="server" Text="Last seven days" /></div>
            </div>
        </div>
        <div id="Last7DaysBorderDiv">
            <div id="Last7DaysRecDiv" class="MyHistoryHolder_Content">
                <asp:ListView ID="Last7DaysIndependentRecordings" DataKeyNames="OpenedDate" runat="server"
                    OnItemDataBound="IndependentGrid_RowDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl='<%# string.Format("~/Portals/0/images/{0}",Eval("SessionTypeImage"))%>'
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                    <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl='<%#Eval("FileName") %>' ClientIDMode="Static"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("Title") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# DateTime.Parse(Eval("BookOpenedTime").ToString()).ToString("HH:mm") %>
                                        <%# string.Format(" {0} the {1:dd}{2}", DateTime.Parse(Eval("BookOpenedDate").ToString()).DayOfWeek, DateTime.Parse(Eval("BookOpenedDate").ToString()), (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (1 | 21 | 31)) ? "st" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (2 | 22)) ? "nd" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (3 | 23)) ? "rd" : "th")%>
                                    </p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'Last7DaysIndepRecDiv<%# Container.DataItemIndex %>')" />
                                        <asp:HiddenField ID="RecBtnHdn" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            <asp:Label ID="WordCntLbl" runat="server"></asp:Label>
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
                                            <b style="font-size: 11pt;">
                                                <%# int.Parse(Eval("Minutes").ToString()) >=60 ? string.Concat((int.Parse(Eval("Minutes").ToString()) / 60).ToString("0"), '.', (int.Parse(Eval("Minutes").ToString()) % 60).ToString("00")) : string.Concat(int.Parse(Eval("Minutes").ToString()).ToString("0"), '.', int.Parse(Eval("Seconds").ToString()).ToString("00"))%></b>
                                            <%# int.Parse(Eval("Minutes").ToString()) >=60 ? " hrs":" mins" %>
                                        </div>
                                    </div>
                                </div>
                                <div id="Last7DaysIndepRecDiv<%# Container.DataItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 28px;">
                                            <span class="PlayAll">Play all</span><img id="Last7DaysIndepRecDivPlayAllButton"
                                                onclick="PlayAllAudio(this)" style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                            <div id="Last7DaysIndepRecjPlayerDiv<%# Container.DataItemIndex %>" style="display: none">
                                                <div id="Last7DaysIndepRecjpId<%# Container.DataItemIndex %>" class="jp-jplayer">
                                                </div>
                                                <div id="Last7DaysIndepRecjpId_container_1<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                    shuffle off</a></li>
                                                                <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                    repeat off</a></li>
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
                                                <div id="Last7DaysIndepRecjpId_container_2<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                    <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                        repeat</a></li>
                                                                    <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                        repeat off</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
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
                                            <asp:Repeater ID="VideoGridView" runat="server">
                                                <ItemTemplate>
                                                    <div style="background-color: White;" class="Div_FullWidth">
                                                        <span id="Last7DaysIndepRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background-color: lightgray;" class="Div_FullWidth">
                                                        <span id="Last7DaysIndepRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>                
            </div>
        </div>
    </div>
    <div id="RestoftheMonthHistory" runat="server" clientidmode="Static" class="HistoryDiv">
        <div id="RestoftheMonthRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="RestNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" id="RestMonthToggle" onclick="toggle_visibility('RestRecordingsDiv','RestBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="RestCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv His_LabelDivUp">
                    <asp:Label ID="RestMonthLabel" runat="server" Text="Rest of the month"></asp:Label></div>
            </div>
            <%--<%= string.Format("{0:MMMM}",DateTime.Now) %>--%>
        </div>
        <div id="RestBorderDiv">
            <div id="RestRecordingsDiv" class="MyHistoryHolder_Content">
                <asp:ListView ID="RestIndependentHistory" runat="server" DataKeyNames="OpenedDate"
                    OnItemDataBound="IndependentGrid_RowDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl='<%# string.Format("~/Portals/0/images/{0}",Eval("SessionTypeImage"))%>'
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                    <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl='<%#Eval("FileName") %>' ClientIDMode="Static"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("Title") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# DateTime.Parse(Eval("BookOpenedTime").ToString()).ToString("HH:mm") %>
                                        <%# string.Format(" {0} the {1:dd}{2}", DateTime.Parse(Eval("BookOpenedDate").ToString()).DayOfWeek, DateTime.Parse(Eval("BookOpenedDate").ToString()), (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (1 | 21 | 31)) ? "st" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (2 | 22)) ? "nd" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (3 | 23)) ? "rd" : "th")%>
                                    </p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'RestOfTheMonthIndepRecDiv<%# Container.DataItemIndex %>')" />
                                        <asp:HiddenField ID="RecBtnHdn" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            <asp:Label ID="WordCntLbl" runat="server"></asp:Label>
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
                                            <b style="font-size: 11pt;">
                                                <%# int.Parse(Eval("Minutes").ToString()) >=60 ? string.Concat((int.Parse(Eval("Minutes").ToString()) / 60).ToString("0"), '.', (int.Parse(Eval("Minutes").ToString()) % 60).ToString("00")) : string.Concat(int.Parse(Eval("Minutes").ToString()).ToString("0"), '.', int.Parse(Eval("Seconds").ToString()).ToString("00"))%></b>
                                            <%# int.Parse(Eval("Minutes").ToString()) >=60 ? " hrs":" mins" %>
                                        </div>
                                    </div>
                                </div>
                                <div id="RestOfTheMonthIndepRecDiv<%# Container.DataItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 28px;">
                                            <span class="PlayAll">Play all</span><img id="RestOfTheMonthIndepRecDivPlayAllButton"
                                                onclick="PlayAllAudio(this)" style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                            <div id="RestOfTheMonthIndepRecjPlayerDiv<%# Container.DataItemIndex %>" style="display: none">
                                                <div id="RestOfTheMonthIndepRecjpId<%# Container.DataItemIndex %>" class="jp-jplayer">
                                                </div>
                                                <div id="RestOfTheMonthIndepRecjpId_container_1<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                    shuffle off</a></li>
                                                                <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                    repeat off</a></li>
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
                                                <div id="RestOfTheMonthIndepRecjpId_container_2<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                    <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                        repeat</a></li>
                                                                    <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                        repeat off</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
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
                                            <asp:Repeater ID="VideoGridView" runat="server">
                                                <ItemTemplate>
                                                    <div style="background-color: White;" class="Div_FullWidth">
                                                        <span id="RestOfTheMonthIndepRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background-color: lightgray;" class="Div_FullWidth">
                                                        <span id="RestOfTheMonthIndepRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                            <%#Eval("RecPath")%></span>
                                                        <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                        <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                            Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>                
            </div>
        </div>
    </div>
    <div id="MonthwiseHistories" runat="server" clientidmode="Static">
        <asp:ListView ID="MonthWiseHistory" runat="server" DataKeyNames="MonthId" OnItemDataBound="MonthWiseWordLogs_BindInfo">
            <ItemTemplate>
                <div class="HistoryDiv">
                    <div id="YesterDaysRecordings" class="RecordingsHolder">
                        <div class="HistoryNodeDIv">
                            <asp:Image ID="YesterdayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                        </div>
                        <div class="TodayHistory_Name" onclick="toggle_visibility('RecordingsDiv<%# Container.DataItemIndex %>','BorderDiv<%# Container.DataItemIndex %>');">
                            <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                                <asp:Image ID="YesterdayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                    CssClass="StudentDashBoard_GuidedImage" />
                            </div>
                            <div class="His_LabelDiv His_LabelDivUp">
                                <asp:Label ID="MonthLabel" runat="server"><%# Eval("OpenedMonths")%></asp:Label></div>
                        </div>
                    </div>
                    <div id="BorderDiv<%# Container.DataItemIndex %>">
                        <div id="RecordingsDiv<%# Container.DataItemIndex %>" class="MyHistoryHolder_Content">
                            <asp:ListView ID="IndependentRecordings" runat="server" DataKeyNames="OpenedDate"
                                OnItemDataBound="IndependentGrid_RowDataBound">
                                <ItemTemplate>
                                    <div class="HistoryContentHolder">
                                        <div class="HistoryNodeImage">
                                            <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                        </div>
                                        <div class="HistoryContent">
                                            <div class="HistoryContent_SessionType">
                                                <asp:Image ID="ClassImage" runat="server" ImageUrl='<%# string.Format("~/Portals/0/images/{0}",Eval("SessionTypeImage"))%>'
                                                    Style="margin-left: 8px;" ClientMode="Static" />
                                                <asp:HiddenField ID="SessTypeVal" runat="server" ClientIDMode="Static" Value='<%# Eval("SessionType")%>' />
                                            </div>
                                            <div class="HistoryContent_BookCover">
                                                <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl='<%#Eval("FileName") %>' ClientIDMode="Static"
                                        runat="server" CssClass="History_Books" />
                                            </div>
                                            <div class="HistoryContent_BookDetails">
                                                <p class="HistoryContent_BookName">
                                                    <%# Eval("Title") %></p>
                                                <p class="HistoryContent_DateTime">
                                                    <%# DateTime.Parse(Eval("BookOpenedTime").ToString()).ToString("HH:mm") %>
                                                    <%# string.Format(" {0} the {1:dd}{2}", DateTime.Parse(Eval("BookOpenedDate").ToString()).DayOfWeek, DateTime.Parse(Eval("BookOpenedDate").ToString()), (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (1 | 21 | 31)) ? "st" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (2 | 22)) ? "nd" : (DateTime.Parse(Eval("BookOpenedDate").ToString()).Day == (3 | 23)) ? "rd" : "th")%></p>
                                                <div style="float: left; width: 100%;">
                                                    <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'IndepRecDiv<%# ((ListViewItem)Container.Parent.Parent).DataItemIndex %><%# Container.DataItemIndex  %>')" />
                                                    <asp:HiddenField ID="RecBtnHdn" runat="server" ClientIDMode="Static" />
                                                </div>
                                            </div>
                                            <div class="HistoryContent_ReadDetails">
                                                <div class="History_Book_ReadingDetails">
                                                    <div class="History_WordCountBG">
                                                        <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                                    </div>
                                                    <div class="History_WordCount">
                                                        <asp:Label ID="WordCntLbl" runat="server"></asp:Label>
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
                                                        <b style="font-size: 11pt;">
                                                            <%# int.Parse(Eval("Minutes").ToString()) >=60 ? string.Concat((int.Parse(Eval("Minutes").ToString()) / 60).ToString("0"), '.', (int.Parse(Eval("Minutes").ToString()) % 60).ToString("00")) : string.Concat(int.Parse(Eval("Minutes").ToString()).ToString("0"), '.', int.Parse(Eval("Seconds").ToString()).ToString("00"))%></b>
                                                        <%# int.Parse(Eval("Minutes").ToString()) >=60 ? " hrs":" mins" %>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="IndepRecDiv<%# ((ListViewItem)Container.Parent.Parent).DataItemIndex %><%# Container.DataItemIndex  %>"
                                                class="RecordingsTable">
                                                <div style="float: left; width: 100%; margin-left: -1px; margin-bottom: 10px;">
                                                    <div style="margin-bottom: 28px;">
                                                        <span class="PlayAll">Play all</span><img id="IndepRecDivPlayAllButton" onclick="PlayAllAudio(this)"
                                                            style="float: left; cursor: pointer;" src='<%=Page.ResolveUrl("Portals/0/Images/Play.png")%>' />
                                                        <div id="IndepRecjPlayerDiv<%# Container.DataItemIndex %>" style="display: none">
                                                            <div id="IndepRecjpId<%# Container.DataItemIndex %>" class="jp-jplayer">
                                                            </div>
                                                            <div id="IndepRecjpId_container_1<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                            <li><a href="#" class="jp-shuffle" tabindex="1" title="shuffle">shuffle</a></li>
                                                                            <li><a href="#" class="jp-shuffle-off" tabindex="1" title="shuffle off" style="display: none;">
                                                                                shuffle off</a></li>
                                                                            <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="">repeat</a></li>
                                                                            <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: none;">
                                                                                repeat off</a></li>
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
                                                            <div id="IndepRecjpId_container_2<%# Container.DataItemIndex %>" class="jp-audio"
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
                                                                                <li><a href="#" class="jp-repeat" tabindex="1" title="repeat" style="display: none;">
                                                                                    repeat</a></li>
                                                                                <li><a href="#" class="jp-repeat-off" tabindex="1" title="repeat off" style="display: block;">
                                                                                    repeat off</a></li>
                                                                            </ul>
                                                                        </div>
                                                                    </div>
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
                                                        <asp:Repeater ID="VideoGridView" runat="server">
                                                            <ItemTemplate>
                                                                <div style="background-color: White;" class="Div_FullWidth">
                                                                    <span id="IndepRecordingPath<%# Container.ItemIndex %>" style="display: none">
                                                                        <%#Eval("RecPath")%></span>
                                                                    <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                                    <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                        Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <div style="background-color: lightgray;" class="Div_FullWidth">
                                                                    <span id="IndepRecordingPathAlt<%# Container.ItemIndex %>" style="display: none">
                                                                        <%#Eval("RecPath")%></span>
                                                                    <input type="button" id="Play" onclick="PlayAudio(this);" class="AudioPlayButton" />
                                                                    <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageName") %>'
                                                                        Style="float: left; margin-top: 3px;" runat="server"></asp:Label>
                                                                </div>
                                                            </AlternatingItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>                            
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>
<div id="LastNodeDiv" runat="server" class="LastNodeDiv" clientidmode="Static">
    <asp:Image ID="LastMonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
</div>
<asp:HiddenField ID="StyleApplier" runat="server" Value="Empty" />
<asp:HiddenField ID="MonthLabel" runat="server" />
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
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 30) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 50) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function toggle_visibility(ContentDivId, BorderDivId) {
        var e = document.getElementById(ContentDivId);
        var d = document.getElementById(BorderDivId);
        if ($(e).is(':visible')) {
            $(e).fadeToggle("slow", "linear");
            $('#' + BorderDivId).parent().children().children().next().children().next().removeClass('His_LabelDivDown').addClass('His_LabelDivUp');
            setTimeout("hidetab()", 700);
        }
        else {
            $(e).fadeToggle("slow", "linear");
            $('#' + BorderDivId).parent().children().children().next().children().next().removeClass('His_LabelDivUp').addClass('His_LabelDivDown');
            showtab();
        }


    }
    jQuery(function () {
        $("input[id^='RecBtnHdn']").each(function (i) {
            if (jQuery(this).val() == 'No Recordings') {
                jQuery(this).parent().children().hide();
            }
        });
        jQuery('#level').hide();
        jQuery('#amount').hide();

    });

    function ShowRecordings(id, ContentDivId) {
        if (id.className == "RecordingsMax") {
            jQuery(id.parentNode.nextSibling).hide();
            id.parentNode.style.width = "60%";
            jQuery("#" + ContentDivId).css("display", "block");
            jQuery(id).removeClass("RecordingsMax");
            jQuery(id).addClass("RecordingsMin");
            showtab();
        }
        else {
            jQuery(id.parentNode.nextSibling).show();
            id.parentNode.style.width = "40%";
            jQuery("#" + ContentDivId).css("display", "none");
            jQuery(id).removeClass("RecordingsMin");
            jQuery(id).addClass("RecordingsMax");
            hidetab();
        }


    }
</script>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Teachers/Scripts/jquery.jplayer.min.js")%>"
    type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Teachers/Scripts/jplayer.playerlist.min.js")%>"
    type="text/javascript"></script>
