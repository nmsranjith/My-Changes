<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupWords.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupWords" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>
<style type="text/css" >
.HistoryDiv
{
    margin-bottom:15px!important;    
}
</style>
<div id="MessagesDiv">
    <uc1:Messages ID="Messages" runat="server" />
</div>
<div id="FirstDiv" class="MyRecordings_TopDiv">
    <asp:Label ID="GroupNameLabel" runat="server" Style="display: none"></asp:Label>
    <div id="MyWordsHeader" class="MyWordsHeader" style="width: 46%;">
        My Words</div>
    <div id="MyWordsCount" class="MyWordsCount" style="float: left; margin-left: -83px;
        margin-top: 29px;">
        <span id="WordsCount" runat="server" style="margin-right: 5px;"></span>WORD/S SAVED</div>
</div>
<div style="margin: -3px 10px 0px 21px; float: left; z-index: 100000; position: relative;">
    <asp:Button ID="BackButton" runat="server" ClientIDMode="Static" Style="width: 104%;
        font-size: 10pt; border: 0px solid white; cursor: pointer; margin-left: -5px;
        float: left; margin-top: 20px; background-color: transparent; color: #1FB5E7;
        outline: none; font-family: Raleway-regular,Raleway,Arial, Sans-Serif; font-weight: 500;"
        Text="Back to your profile" OnClick="BackButton_Click" />
</div>
<div id="SecondDiv" class="Div_FullWidth">
    <div id="WordsLogMainDiv" class="WordsLogMainDiv">
        <div class="HistoryDiv" id="TodaysHistoryDiv">
            <div id="TodaysMyWords" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="TodayNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('TodaysWordsDiv','TodaysBorderDiv',this);">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="TodayCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv_Up">
                        <asp:Label ID="TodayLabel" runat="server" Text="  Today" />
                        <asp:Button ID="TodayClearFrmIpBtn" runat="server" Text="Clear from ipad" CssClass="ClearWords"
                            OnClick="TodayClearFrmIpBtn_Click" />
                    </div>
                </div>
            </div>
            <div id="TodaysBorderDiv">
                <ul id="TodaysWordsDiv" class="MyWordsHolder_Content1" style="margin-top: 8px; float: left;display:none;
                    width: 100%; margin-left: 3px;">
                    <asp:ListView ID="TodayWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("CircledWord")%>
                                </div>
                                <div class="LogWordCount">
                                    <span style="color: white; font-family: Arial; font-weight: 700; margin-left: 1px;">
                                        <%# Eval("WordCount")%></span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div class="HistoryDiv" id="YesterdaysHistoryDiv">
            <div id="YeaterdayWords" class="MyWordsHolder" style="margin-top:-15px;">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="YeaterdayWordsImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('YeaterdayWordsDiv','YeaterdayWordsBorderDiv',this);">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="YeaterdayWordsImage1" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv_Up">
                        <asp:Label ID="YeaterdayWordsLabel" runat="server" Text="Yesterday" />
                        <asp:Button ID="YstrClearFrmBtn" runat="server" Text="Clear from ipad" CssClass="ClearWords"
                            OnClick="YstrClearFrmBtn_Click" />
                    </div>
                </div>
            </div>
            <div id="YeaterdayWordsBorderDiv">
                <ul id="YeaterdayWordsDiv" class="MyWordsHolder_Content" style="margin-top: 8px;display:none;
                    float: left; width: 100%; margin-left: 3px;">
                    <asp:ListView ID="YesterdayWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("CircledWord")%>
                                </div>
                                <div class="LogWordCount">
                                    <span style="color: white; font-family: Arial; font-weight: 700; margin-left: 1px;">
                                        <%# Eval("WordCount")%></span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div class="HistoryDiv" id="LastSevenDaysHistoryDiv">
            <div id="LastSevenDaysRecordings" class="MyWordsHolder">
                <div class="HistoryNodeDIv">
                    <asp:Image ID="Last7DaysNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                </div>
                <div class="TodayHistory_Name" onclick="toggle_visibility('Last7DaysRecDiv','Last7DaysBorderDiv',this);">
                    <div class="MyHistory_Calendar">
                        <asp:Image ID="Last7DaysCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                            CssClass="StudentDashBoard_GuidedImage" />
                    </div>
                    <div class="His_LabelDiv_Up">
                        <asp:Label ID="Label2" runat="server" Text="Last week" />
                        <asp:Button ID="LastSvndyClrFromIpad" runat="server" Text="Clear from ipad" CssClass="ClearWords"
                            OnClick="LastSvndyClrFromIpad_Click" />
                    </div>
                </div>
            </div>
            <div id="Last7DaysBorderDiv">
                <ul id="Last7DaysRecDiv" class="MyWordsHolder_Content" style="margin-top: 8px; float: left;display:none;
                    width: 100%; margin-left: 3px;">
                    <asp:ListView ID="LastSevenDaysWordLog" runat="server" GroupItemCount="4">
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            <br />
                        </GroupTemplate>
                        <ItemTemplate>
                            <li class="MyWordLog">
                                <div class="LogWord">
                                    <%# Eval("CircledWord")%>
                                </div>
                                <div class="LogWordCount">
                                    <span style="color: white; font-family: Arial; font-weight: 700; margin-left: 1px;">
                                        <%# Eval("WordCount")%></span>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
        </div>
        <div id="RestMonthDiv" runat="server" clientidmode="Static">
            <asp:Repeater ID="MonthWiseWordLog" runat="server" OnItemDataBound="MonthWiseWordLog_BindInfo">
                <ItemTemplate>
                    <div class="HistoryDiv" style="margin-bottom:15px!important;margin-top:-15px!important">
                        <div id="MonthWiseRecordings" class="MyWordsHolder">
                            <div class="HistoryNodeDIv">
                                <asp:Image ID="MonthNodeImage" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
                            </div>
                            <div class="TodayHistory_Name" onclick="toggle_visibility('MonthWiseDiv<%# Container.ItemIndex %>','BorderDiv<%# Container.ItemIndex %>',this);">
                                <div class="MyHistory_Calendar">
                                    <asp:Image ID="MonthCalImage" runat="server" ImageUrl="~/Portals/0/images/callender.png"
                                        CssClass="StudentDashBoard_GuidedImage" />
                                </div>
                                <div class="His_LabelDiv_Up">
                                    <asp:Label ID="MonthLabel" runat="server" Text='<%# Container.DataItem %>' />
                                    <asp:Button ID="RestMnthClrFromIpad" ClientIDMode="Static" runat="server" Text="Clear from ipad" CssClass="ClearWords"
                                        OnClick="RestMnthClrFromIpad_Click" />
                                </div>
                            </div>
                        </div>
                        <div id="BorderDiv<%# Container.ItemIndex %>">
                            <div id="MonthWiseDiv<%# Container.ItemIndex %>" class="MyWordsHolder_Content" style="margin-top: 8px;display:none;
                                float: left; width: 100%; margin-left: 42px;">
                                <asp:ListView ID="MonthWordLog" runat="server" GroupItemCount="4">
                                    <GroupTemplate>
                                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                        <br />
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <li class="MyWordLog">
                                            <div class="LogWord">
                                                <%# Eval("CircledWord")%>
                                            </div>
                                            <div class="LogWordCount">
                                                <span style="color: white; font-family: Arial; font-weight: 700; margin-left: 1px;">
                                                    <%# Eval("WordCount")%></span>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="LastNodeDiv">
        <asp:Image ID="LastMonthNodeImage" ClientIDMode="Static" runat="server" ImageUrl="~/Portals/0/images/circle_small.png" />
    </div>
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
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 120) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function showtab() {
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + jQuery('#SecondDiv').height() + 120) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }
    function toggle_visibility(ContentDivId, BorderDivId, contrl) {
        var e = document.getElementById(ContentDivId);
        var d = document.getElementById(BorderDivId);
        if ($(e).is(':visible')) {
            $(e).fadeToggle("slow", "linear");
            jQuery('#SecondDiv').height((jQuery('#SecondDiv').height() - jQuery('#' + ContentDivId).height()));
	        contrl.children[1].className = "His_LabelDiv_Up";
	        setTimeout("hidetab()", 700);
        }
        else {
            $(e).fadeToggle("slow", "linear");
            jQuery('#SecondDiv').height((jQuery('#SecondDiv').height() + jQuery('#' + ContentDivId).height()));
            contrl.children[1].className = "His_LabelDiv";
	        showtab();
        }

    }
    function Delete(id) {
        jQuery('#' + id).remove();
    }
    jQuery(document).ready(function () {
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

        var displayProperty = 0;
        var expandCurrentTab = 0;
        if ($("#TodaysBorderDiv ul")[0].children.length == 0) {
            $("#TodaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            $("#TodaysBorderDiv")[0].parentNode.style.marginTop = "-15px";
            if (expandCurrentTab == 0) {
                $("#TodaysBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;
            $("#TodaysHistoryDiv").attr("style", "margin-top:-30px!important;margin-bottom:15px!important");
        }
        if ($("#YeaterdayWordsBorderDiv ul")[0].children.length == 0) {
            $("#YeaterdayWordsBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            $("#YeaterdayWordsBorderDiv")[0].parentNode.style.marginTop = "-15px";
            if (expandCurrentTab == 0) {
                $("#YeaterdayWordsBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;
            $("#YesterdaysHistoryDiv").attr("style", "margin-bottom:15px!important;margin-bottom:15px!important");
        }
        if ($("#Last7DaysBorderDiv ul")[0].children.length == 0) {
            $("#Last7DaysBorderDiv")[0].parentNode.style.display = "none";
            displayProperty++;
        }
        else {
            $("#Last7DaysBorderDiv")[0].parentNode.style.marginTop = "-15px";
            if (expandCurrentTab == 0) {
                $("#Last7DaysBorderDiv")[0].parentNode.children[0].children[1].click();
            }
            expandCurrentTab++;
            $("#LastSevenDaysHistoryDiv").attr("style", "margin-top:-30px!important;margin-bottom:15px!important");
        }
        if ($("#RestMonthDiv")[0].children.length == 0) {
            $("#RestMonthDiv")[0].style.display = "none";
            displayProperty++;
        }
        else {
            $("#RestMonthDiv")[0].style.marginTop = "-15px";
            if (expandCurrentTab == 0) {
                $("#RestMonthDiv")[0].children[0].children[0].children[1].click();
            }
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
        showtab();
        jQuery('#BackButton').attr('href', jQuery('#PageHeaderButton').attr('href'));
        $('#TodaysWordsDiv').find('li').each(function () {
            // cache jquery var
            var current = $(this);
            if (current.children().size() > 0) { return true; }
            // add current text to our current phrase
            alert(current.text());
        });

        if (jQuery($("[id$=StyleApplier]")).val() != "Empty") {
            jQuery('#RestoftheMonth').css({ 'border-left': '1px', 'border-left-color': 'gray', 'border-left-style': 'solid' });
        }
    });
</script>
