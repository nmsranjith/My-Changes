<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReadingHistory.ascx.cs"
    Inherits="DotNetNuke.UI.eCollectionControls.ReadingHistory" %>
<div id="HistoryMainDiv" class="HistoryMainDiv">
    <div class="HistoryDiv" style="margin-top: -31px;">
        <div id="TodaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" onclick="toggle_visibility('TodaysRecordingsDiv','TodaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv">
                    <asp:Label ID="TodayLabel" runat="server" Text="  Today" /></div>
            </div>
        </div>
        <div id="TodaysBorderDiv">
            <div id="TodaysRecordingsDiv" class="MyHistoryHolder_Content1">
                <asp:Repeater ID="TodaysGuidedRecordings" runat="server" OnItemDataBound="Recordings_ItemDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/groups.png"
                                        ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/TheLittleBlueHorse.png"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'TodayGuidRecDiv<%# Container.ItemIndex %>')" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                                <div id="TodayGuidRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 98%; margin-left: 12px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 5px;">
                                            <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                        </div>
                                        <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                            <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                ShowHeader="False" Width="98%" BackColor="White" GridLines="None" CellPadding="3"
                                                EnableModelValidation="True" ForeColor="Black">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle Width="100%" />
                                                <AlternatingRowStyle BackColor="#EDEDED" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <input type="button" id="Play" onclick="PlayAudio(this,'TodayVideoPlayDiv<%# Container.DataItemIndex %>');"
                                                                class="PlayButton" />
                                                            <div id="TodayVideoPlayDiv<%# Container.DataItemIndex %>" style="display: none">
                                                                <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="600px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="TodaysIndependentRecordings" runat="server" OnItemDataBound="Recordings_ItemDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/1.jpg"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'TodayIndeRecDiv<%# Container.ItemIndex %>')" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                                <div id="TodayIndeRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 98%; margin-left: 12px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 5px;">
                                            <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                        </div>
                                        <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                            <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                ShowHeader="False" Width="98%" BackColor="White" GridLines="None" CellPadding="3"
                                                EnableModelValidation="True" ForeColor="Black">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle Width="100%" />
                                                <AlternatingRowStyle BackColor="#EDEDED" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayButton" />
                                                            <div id="VideoPlayDiv" style="display: none">
                                                                <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="600px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
    <div class="HistoryDiv">
        <div id="YesterDaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="YesterdayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" onclick="toggle_visibility('YesterdaysRecordingsDiv','YesterdaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="YesterdayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv">
                    <asp:Label ID="YesterdayLabel" runat="server" Text="  Yesterday" /></div>
            </div>
        </div>
        <div id="YesterdaysBorderDiv">
            <div id="YesterdaysRecordingsDiv" class="MyHistoryHolder_Content">
                <asp:Repeater ID="YesterDayGuidedRecordings" runat="server" OnItemDataBound="Recordings_ItemDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/groups.png"
                                        ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/1.jpg"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'YesterdayGuidRecDiv<%# Container.ItemIndex %>')" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                                <div id="YesterdayGuidRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 98%; margin-left: 12px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 5px;">
                                            <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                        </div>
                                        <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                            <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                ShowHeader="False" Width="98%" BackColor="White" GridLines="None" CellPadding="3"
                                                EnableModelValidation="True" ForeColor="Black">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle Width="100%" />
                                                <AlternatingRowStyle BackColor="#EDEDED" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <input type="button" id="Play" onclick="PlayAudio(this,'YesterdayVideoPlayDiv<%# Container.DataItemIndex %>');"
                                                                class="PlayButton" />
                                                            <div id="YesterdayVideoPlayDiv<%# Container.DataItemIndex %>" style="display: none">
                                                                <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="600px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="YesterDayIndependentRecordings" runat="server" OnItemDataBound="Recordings_ItemDataBound">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/Book2.png"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'YesterdayIndeRecDiv<%# Container.ItemIndex %>')" />
                                    </div>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                                <div id="YesterdayIndeRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                    <div style="float: left; width: 98%; margin-left: 12px; margin-bottom: 10px;">
                                        <div style="margin-bottom: 5px;">
                                            <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                        </div>
                                        <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                            <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                ShowHeader="False" Width="98%" BackColor="White" GridLines="None" CellPadding="3"
                                                EnableModelValidation="True" ForeColor="Black">
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle Width="100%" />
                                                <AlternatingRowStyle BackColor="#EDEDED" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <input type="button" id="Play" onclick="PlayAudio(this);" class="PlayButton" />
                                                            <div id="VideoPlayDiv" style="display: none">
                                                                <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="600px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
    <div class="HistoryDiv">
        <div id="LastSevenDaysRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv">
                    <asp:Label ID="Last7DaysLabel" runat="server" Text="Last Seven Days" /></div>
            </div>
        </div>
        <div id="Last7DaysBorderDiv">
            <div id="Last7DaysRecDiv" class="MyHistoryHolder_Content">
                <asp:Repeater ID="Last7DaysIndependentRecordings" runat="server">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="Image1" AlternateText="Cover" ImageUrl="~/Portals/0/images/book1.png"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <%-- <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'LastSevenDaysIndeRecDiv<%# Container.ItemIndex %>')" />
                                    </div>--%>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div class="HistoryDiv">
        <div id="RestoftheMonthRecordings" class="RecordingsHolder">
            <div class="HistoryNodeDIv">
                <asp:Image ID="RestNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
            </div>
            <div class="TodayHistory_Name" onclick="toggle_visibility('RestRecordingsDiv','RestBorderDiv');">
                <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                    <asp:Image ID="RestCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                        CssClass="StudentDashBoard_GuidedImage" />
                </div>
                <div class="His_LabelDiv">
                    <asp:Label ID="RestMonthLabel" runat="server" Text="November" /></div>
            </div>
        </div>
        <div id="RestBorderDiv">
            <div id="RestRecordingsDiv" class="MyHistoryHolder_Content">
                <asp:Repeater ID="RestIndependentHistory" runat="server">
                    <ItemTemplate>
                        <div class="HistoryContentHolder">
                            <div class="HistoryNodeImage">
                                <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                            </div>
                            <div class="HistoryContent">
                                <div class="HistoryContent_SessionType">
                                    <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/students.png"
                                        Style="margin-left: 8px;" ClientMode="Static" />
                                </div>
                                <div class="HistoryContent_BookCover">
                                    <asp:Image ID="Image1" AlternateText="Cover" ImageUrl="~/Portals/0/images/TheLittleBlueHorse.png"
                                        runat="server" CssClass="History_Books" />
                                </div>
                                <div class="HistoryContent_BookDetails">
                                    <p class="HistoryContent_BookName">
                                        <%# Eval("BookName") %></p>
                                    <p class="HistoryContent_DateTime">
                                        <%# Eval("DateTime") %></p>
                                    <%-- <div style="float: left; width: 100%;">
                                        <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'LastSevenDaysIndeRecDiv<%# Container.ItemIndex %>')" />
                                    </div>--%>
                                </div>
                                <div class="HistoryContent_ReadDetails">
                                    <div class="History_Book_ReadingDetails">
                                        <div class="History_WordCountBG">
                                            <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                        </div>
                                        <div class="History_WordCount">
                                            5
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
                                            <b style="font-size: 11pt;">
                                                <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="padding-top: 15px;">
                    <asp:Repeater ID="RestGuidedRecordings" runat="server" OnItemDataBound="Recordings_ItemDataBound">
                        <ItemTemplate>
                            <div class="HistoryContentHolder">
                                <div class="HistoryNodeImage">
                                    <asp:Image ID="NodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
                                </div>
                                <div class="HistoryContent">
                                    <div class="HistoryContent_SessionType">
                                        <asp:Image ID="ClassImage" runat="server" ImageUrl="~/Portals/0/images/groups.png"
                                            ClientMode="Static" />
                                    </div>
                                    <div class="HistoryContent_BookCover">
                                        <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/Book3.png"
                                            runat="server" CssClass="History_Books" />
                                    </div>
                                    <div class="HistoryContent_BookDetails">
                                        <p class="HistoryContent_BookName">
                                            <%# Eval("BookName") %></p>
                                        <p class="HistoryContent_DateTime">
                                            <%# Eval("DateTime") %></p>
                                        <div style="float: left; width: 100%;">
                                            <input id="RecordingsButton" type="button" class="RecordingsMax" onclick="ShowRecordings(this,'RestGuidRecDiv<%# Container.ItemIndex %>')" />
                                        </div>
                                    </div>
                                    <div class="HistoryContent_ReadDetails">
                                        <div class="History_Book_ReadingDetails">
                                            <div class="History_WordCountBG">
                                                <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                                            </div>
                                            <div class="History_WordCount">
                                                5
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
                                                <b style="font-size: 11pt;">
                                                    <%# Eval("BookOpenTime")+":00" %></b> mins</div>
                                        </div>
                                    </div>
                                    <div id="RestGuidRecDiv<%# Container.ItemIndex %>" class="RecordingsTable">
                                        <div style="float: left; width: 98%; margin-left: 12px; margin-bottom: 10px;">
                                            <div style="margin-bottom: 5px;">
                                                <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                    Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                            </div>
                                            <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                    ShowHeader="False" Width="98%" BackColor="White" GridLines="None" CellPadding="3"
                                                    EnableModelValidation="True" ForeColor="Black">
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle Width="100%" />
                                                    <AlternatingRowStyle BackColor="#EDEDED" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="40px">
                                                            <ItemTemplate>
                                                                <input type="button" id="Play" onclick="PlayAudio(this,'RestVideoPlayDiv<%# Container.DataItemIndex %>');"
                                                                    class="PlayButton" />
                                                                <div id="RestVideoPlayDiv<%# Container.DataItemIndex %>" style="display: none">
                                                                    <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="600px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
    </div>
</div>
<asp:Repeater ID="MonthWiseHistory" runat="server" OnItemDataBound="MonthWiseWordLog_BindInfo">
    <ItemTemplate>
        <div class="HistoryDiv">
            <div id="MonthWiseRecordings" class="RecordingsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="MonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('div<%# Container.ItemIndex %>','BorderDiv<%# Container.ItemIndex %>');">
                    <div style="float: left; width: 8%; margin-left: 13px; margin-top: 10px;">
                        <asp:Image ID="MonthCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv">
                        <asp:Label ID="MonthLabel" runat="server" Text='<%= Eval("MonthName") %>' /></div>
                </div>
            </div>
            <div id="BorderDiv<%# Container.ItemIndex %>" style="float: left; margin-top: -18px;
                min-height: 60px; margin-left: 92px; border-left: 2px; border-left-color: gray;
                border-left-style: solid;">
                <div style="width: 100%; float: left; min-height: 60px; margin-left: -83px; padding-top: 10px;">
                    <div id="div<%# Container.ItemIndex %>" style="display: none; width: 100%; float: left;">
                        <asp:GridView ID="GuidedGridView" CssClass="RecordingsGridViewstyle" runat="server"
                            AutoGenerateColumns="false" ShowHeader="false" Width="100%" GridLines="None">
                            <RowStyle Width="100px" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="12%" ItemStyle-CssClass="Guided_PaddingStyle">
                                    <ItemTemplate>
                                        <asp:Image ID="ClassImageID" runat="server" ImageUrl="~/Portals/0/images/Guided.png"
                                            Style="width: 50px;" ClientMode="Static" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-CssClass="Guided_NodePaddingStyle">
                                    <ItemTemplate>
                                        <asp:Image ID="LastMonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/mainnodecircle.png"
                                            Height="10px" Width="10px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="30%" ItemStyle-CssClass="Guided_CoverPaddingStyle">
                                    <ItemTemplate>
                                        <asp:Image ID="BookCoverImage" AlternateText="Cover" ImageUrl="~/Portals/0/images/GroupReadbook.png"
                                            runat="server" CssClass="History_Books" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="40%" ItemStyle-CssClass="Guided_PaddingStyle">
                                    <ItemTemplate>
                                        <asp:Label ID="StudentNameLabel" ForeColor="Blue" runat="server" Text='<%# Eval("Student") %>'></asp:Label><br />
                                        <asp:Label ID="BookNameLabel" ForeColor="Black" Font-Bold="true" runat="server" Text='<%# Eval("BookName") %>'></asp:Label><br />
                                        <asp:Label ID="DateTimeLabel" runat="server" Text='<%# Eval("DateTime") %>'></asp:Label><br />
                                        <div onclick="toggle_visibility('RestGuidRecDiv<%# Container.DataItemIndex %>')"
                                            style="float: left; width: 100%;">
                                            <asp:Image ID="RecordingsButton" runat="server" ImageUrl="~/Portals/0/images/RecordingsButton.png" />
                                        </div>
                                        <div id="RestGuidRecDiv<%# Container.DataItemIndex %>" style="display: none; float: left;
                                            width: 100%;">
                                            <div>
                                                <span class="PlayAll">Play all</span><asp:Image ID="PlayAllButton" runat="server"
                                                    Style="margin-top: -2px; margin-left: 3px;" ImageUrl="~/Portals/0/images/Play.png" />
                                                <asp:Label ID="VideoTimeLabel" Style="float: right; padding-right: 5px;" runat="server"
                                                    Text="2min 34Sec"></asp:Label>
                                            </div>
                                            <div style="padding: 12px; border: 1px solid lightgray; background-color: white;">
                                                <asp:GridView ID="VideoGridView" CssClass="ClassGridViewstyle" runat="server" AutoGenerateColumns="False"
                                                    ShowHeader="False" Width="100%" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                                                    BorderStyle="Solid" BorderWidth="1px" CellPadding="3" EnableModelValidation="True"
                                                    ForeColor="Black">
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle Width="100px" />
                                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Image ID="PlaYImageButton" runat="server" ImageUrl="~/Portals/0/images/Play.png" />
                                                                <div id="VideoPlayDiv" style="display: none">
                                                                    <audio controls="controls" style="width: 266px;">
                                            <source src="http://www.w3schools.com/html/horse.ogg" type="audio/ogg">
                                            <source src="horse.mp3" type="audio/mpeg">
                                            Your browser does not support the audio element.
                                            </audio>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="PageCountLabel" class="PageCountLabel" Text='<%# Eval("PageCount") %>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50%" ItemStyle-CssClass="Guided_PaddingStyle">
                                    <ItemTemplate>
                                        <span>book open</span>
                                        <asp:Label ID="BookOpenLabel" Text='<%# Eval("BookOpenTime") %>' runat="server"></asp:Label>
                                        <span>min</span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div class="LastNodeDiv">
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
        if (e.style.display == 'block') {
            e.style.display = 'none';
            hidetab();
        }
        else {
            e.style.display = 'block';
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
    function PlayAudio(id, containerID) {
        if (id.nextSibling.nextSibling.style.display == "none") {
            id.nextSibling.nextSibling.style.display = "block";
            id.src = "../Portals/0/images/DownPlay.png";
            id.parentNode.nextSibling.style.display = "none";
            showtab();
        }
        else {
            id.nextSibling.nextSibling.style.display = "none";
            id.parentNode.nextSibling.style.display = "";
            id.src = "../Portals/0/images/Play.png";
            hidetab();

        }
    }
</script>
