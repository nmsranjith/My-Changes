<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyWords.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Students.Views.MyWords" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="Msg" %>
<Msg:Messages ID="Messages" runat="server">
</Msg:Messages>
<div id="FirstDiv" class="MyRecordings_TopDiv">
    <h3 id="MyWordsHeader" class="MyWordsHeader">
        My Words</h3>
    <div id="MyWordsCount" class="MyWordsCount">
        <font id="MyWordsCnt" runat="server" face="Arial"></font> WORD/S SAVED</div>
</div>
<div style="margin: 18px 10px -23px 21px; float: left; z-index: 100000; position: relative;">
    <asp:HyperLink ID="BackButton" runat="server" ClientIDMode="Static" Style="text-decoration: none;
        color: #20B3E6; font-size: 10pt;" Text="Back to profile"></asp:HyperLink>
</div>
<div id="SecondDiv" class="Div_FullWidth" runat="server" clientidmode="Static">
    <div id="WordsLogMainDiv" class="WordsLogMainDiv">
        <div id="TodayHistory" runat="server" clientidmode="Static" class="HistoryDiv" style="margin-top: -31px;">
            <div id="TodaysMyWords" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('TodaysWordsDiv','TodaysBorderDiv');">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv His_LabelDivUp">
                        <asp:Label ID="TodayLabel" runat="server" Text="  Today" />
                        <input type="button" id="ClearTodayWords" class="ClearWords" onclick="ClearWords('ClearTodayWords','today')"
                            value="" />
                    </div>
                </div>
            </div> 
            <div id="TodaysBorderDiv">
                <ul id="TodaysWordsDiv" class="MyWordsHolder_Content1 MyWordsUl">
                    <asp:ListView ID="TodayWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("Word") %>
                                </div>
                                <div class="LogWordCount">
                                    <%# Eval("WordCount")%>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div id="YesterdayHistory" runat="server" clientidmode="Static" class="HistoryDiv">
            <div id="YesterdayWords" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="YesterdayWordsImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('YesterdayWordsDiv','YesterdayWordsBorderDiv');">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="YesterdayWordsImage1" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv His_LabelDivUp">
                        <asp:Label ID="YeaterdayWordsLabel" runat="server" Text="Yesterday" />
                        <input type="button" id="ClearYesterdayWords" class="ClearWords" onclick="ClearWords('ClearYesterdayWords','yesterday')"
                            value="" />
                    </div>
                </div>
            </div>
            <div id="YesterdayWordsBorderDiv">
                <ul id="YesterdayWordsDiv" class="MyWordsHolder_Content MyWordsUl">
                    <asp:ListView ID="YesterdayWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("Word") %>
                                </div>
                                <div class="LogWordCount">
                                    <%# Eval("WordCount")%>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div id="Last7DaysHistory" runat="server" clientidmode="Static" class="HistoryDiv">
            <div id="LastSevenDaysRecordings" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv');">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv His_LabelDivUp">
                        <asp:Label ID="Label2" runat="server" Text="Last week" />
                        <input type="button" id="ClearLast7Words" class="ClearWords" onclick="ClearWords('ClearLast7Words','lastweek')"
                            value="" /><%--
                        <input type="button" id="RemoveWords" class="RemoveWords" onclick="ClearWords('YesterdayWordLog')"
                            value="Removed from ipad" />--%>
                    </div>
                </div>
            </div>
            <div id="Last7DaysBorderDiv">
                <ul id="Last7DaysRecDiv" class="MyWordsHolder_Content MyWordsUl">
                    <asp:ListView ID="LastSevenDaysWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("Word") %>
                                </div>
                                <div class="LogWordCount">
                                    <%# Eval("WordCount")%>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div id="RestoftheMonthHistory" runat="server" clientidmode="Static" class="HistoryDiv">
            <div id="RestoftheMonthRecordings" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="RestNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('RestRecordingsDiv','RestBorderDiv');">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="RestCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv His_LabelDivUp">
                        <asp:Label ID="RestMonthLabel" runat="server" Text="Rest of the month"></asp:Label>
                        <input type="button" id="ClearRestWords" class="ClearWords" onclick="ClearWords('ClearRestWords','restofthemonth')"
                            value="" />
                    </div>
                </div>
                <%--<%= string.Format("{0:MMMM}",DateTime.Now) %>--%>
            </div>
            <div id="RestBorderDiv">
                <ul id="RestRecordingsDiv" class="MyWordsHolder_Content MyWordsUl">
                    <asp:ListView ID="RestoftheMonthWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("Word") %>
                                </div>
                                <div class="LogWordCount">
                                    <%# Eval("WordCount")%>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div id="MonthwiseHistory" runat="server" clientidmode="Static">
            <asp:ListView ID="MonthWiseWordLog" runat="server" DataKeyNames="MonthId" OnItemDataBound="MonthWiseWordLog_BindInfo">
                <ItemTemplate>
                    <div class="HistoryDiv">
                        <div id="MonthWiseRecordings" class="MyWordsHolder">
                            <div class="HistoryNodeDIv">
                                <asp:Image ID="MonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                            </div>
                            <div class="TodayHistory_Name" onclick="toggle_visibility('MonthWiseDiv<%# Container.DataItemIndex %>','BorderDiv<%# Container.DataItemIndex %>');">
                                <div class="MyHistory_Calendar">
                                    <asp:Image ID="MonthCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                        CssClass="StudentDashBoard_GuidedImage" />
                                </div>
                                <div class="His_LabelDiv His_LabelDivUp">
                                    <asp:Label ID="MonthLabel" runat="server" Text='<%# Eval("OpenedMonths") %>' />
                                    <input type="button" id="ClearMonthRestWords<%# Container.DataItemIndex %>" class="ClearWords"
                                        onclick="ClearMonthWord('ClearMonthRestWords<%# Container.DataItemIndex %>','<%# Eval("MonthId") %>')"
                                        value="" />
                                </div>
                            </div>
                        </div>
                        <div id="BorderDiv<%# Container.DataItemIndex %>">
                            <ul id="MonthWiseDiv<%# Container.DataItemIndex %>" class="MyWordsHolder_Content MyWordsUl">
                                <asp:ListView ID="MonthDataList" runat="server" GroupItemCount="4">
                                    <GroupTemplate>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                        <br />
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <li class="MyWordLog">
                                            <div class="LogWord">
                                                <%# Eval("Word") %>
                                            </div>
                                            <div class="LogWordCount">
                                                <%# Eval("WordCount")%>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
    <div class="LastNodeDiv">
        <asp:Image ID="LastMonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
    </div>
</div>
<asp:HiddenField ID="StyleApplier" runat="server" Value="Empty" />
<asp:HiddenField ID="MonthLabel" runat="server" />
<span id="studentIdLabel" runat="server" clientidmode="Static" style="display: none;">
</span>
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
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 150) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 6) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 150) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 6) + 'px');
    }
    function toggle_visibility(ContentDivId, BorderDivId) {        
        var e = document.getElementById(ContentDivId);
        var d = document.getElementById(BorderDivId);
        if ($(e).is(':visible')) {
            $(e).fadeToggle("slow", "linear");
            jQuery('#SecondDiv').height((jQuery('#SecondDiv').height() - jQuery('#' + ContentDivId).height()));
            $('#' + BorderDivId).parent().children().children().next().children().next().removeClass('His_LabelDiv His_LabelDivDown').addClass('His_LabelDiv His_LabelDivUp');
            setTimeout("hidetab()", 700);
        }
        else {
            $(e).fadeToggle("slow", "linear");
            $('#'+BorderDivId).parent().children().children().next().children().next().removeClass('His_LabelDiv His_LabelDivUp').addClass('His_LabelDiv His_LabelDivDown');
            jQuery('#SecondDiv').height((jQuery('#SecondDiv').height() + jQuery('#' + ContentDivId).height()));
            showtab();
        }

    }
    function ClearWords(id, duration) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=clearfromiPad&duration=' + duration + '&studentId=' + jQuery('#studentIdLabel').text()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                jQuery('#' + id).val('Removed from iPad').addClass('RemoveWords').removeAttr('onclick');
            }
        });
        return false;
    }

    function ClearMonthWord(id, Month) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=clearfromiPad&duration=monthwise&Month=' + Month + '&studentId=' + jQuery('#studentIdLabel').text()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                jQuery('#' + id).val('Removed from iPad').addClass('RemoveWords').removeAttr('onclick');
            }
        });
        return false;
    }

    function CheckClearFromiPad(id, duration) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=checkclearfromiPad&studentId=' + jQuery('#studentIdLabel').text() + '&duration=' + duration),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (res) {
                if (res == 0)
                    jQuery('#' + id).val('Removed from iPad').addClass('RemoveWords').removeAttr('onclick');
                else
                    jQuery('#' + id).val('Clear from iPad');
                    
            }
        });
        return false;
    }

    jQuery(function () {
        showtab();


        var obj = $('input.ClearWords').first().parent().parent();

        if (!obj.parent().next().children().is(':visible')) {
            obj.click();
        }



        $('input.ClearWords').map(function () {
            CheckClearFromiPad(this.id, this.parentElement.firstElementChild.innerText);
        });
        $('.LogWord').map(function () {
            var temp = $(this).text().trim(); var count1 = temp.match(/w/g); var count2 = temp.match(/m/g);
            if (count1 != null) {
                if (count2 != null)
                    count1 = count1.concat(count2);
            }
            else {
                if (count2 != null)
                    count1 = count2;
            }

            if (count1 != null) {
                if (temp.length > 12 && count1.length >= 4)
                    $(this).text(temp.substring(0, 10).concat(' ..'));
                else if (temp.length > 15)
                    $(this).text(temp.substring(0, 12).concat(' ..'));
            }
            else {
                if (temp.length > 15)
                    $(this).text(temp.substring(0, 12).concat(' ..'));
            }
            $(this).attr('title', temp);
        });
    });
</script>
