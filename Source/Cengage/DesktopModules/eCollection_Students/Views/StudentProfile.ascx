<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.StudentProfile" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="ReadingHistory.ascx" TagName="HistoryDiv" TagPrefix="HD" %>
<%@ Register Src="MyGroups.ascx" TagName="Groups" TagPrefix="PG" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="Messages" %>
<script type="text/javascript">
    jQuery(function () {
        leftStartValue = jQuery('#leftSlide').val() - 1;
        rightStartValue = jQuery('#rightSlide').val();
        pageName = 'Profile';
    });
</script>
<style type="text/css">
    .Sliderholder
    {
        width: 97.3% !important;
        margin-left: 0px !important;
    }
     .sliderDivProfSaf
    {
        width: 93.6% !important;
        margin-left: 11px !important;
    }
     .sliderDivProfIE
    {
        width: 94.6% !important;
        margin-left: 10px !important;
    }
    .sliderDivProfMoz
    {
        width: 97.1% !important;
    }
</style>
<div id="FirstDiv" class="Div_FullWidth">
    <Messages:Messages ID="Messages" runat="server">
    </Messages:Messages>
    <asp:ListView ID="StudentProfileRepeater" runat="server" OnItemDataBound="StudentProfileRepeater_OnDataBound">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>            
            <span id="studentIdLabel" style="display: none;">
                <%# Eval("StudentId") %></span>
            <div id="InnerDiv" class="StudentProfile_MainDiv">
                <div class="stuprfinnerdiv">
                    <div class="StudentProfile_NameDiv">
                        <div class="Div_FullWidth">
                            <div class="stuprfNameDiv">
                                <h3 title="<%# string.Concat(Eval("FirstName"), " ", Eval("LastName")) %>">
                                    <%# string.Concat(Eval("FirstName"), " ", Eval("LastName")).Length > 15 ? string.Concat(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Eval("FirstName"), " ", Eval("LastName")).Substring(0, 15).ToLower()), ".. ") : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Eval("FirstName"), " ", Eval("LastName")).ToLower())%>:</h3>
                            </div>
                               <div style="float: right; width:30% ;margin: 20px 22px 0px 0px;">
                                <div id="ESL" class="ESL" onclick="ESLUpdate();" style="width: 100%;">
                                    <div style="float: left; width: 16%;">
                                        <img id="EslChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>' alt=""
                                            style="display: none;" />
                                        <img id="EslUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                                            alt="" />
                                    </div>
                                    <div style="float: right; width: 81%; margin-top: 6px;">
                                        ESL</div>
                                </div>
                                <asp:CheckBox ID="ESLCheck" runat="server" ClientIDMode="Static" Text='<%# Eval("ESL") %>'
                                    Style="display: none;" />
                            </div>
                        </div>
                        <div style="float: left; width: 100%;">
                            <div class="ClassDetails">
                                <div style="float: left;">
                                    <%# Eval("ClassName").ToString().Length > 13 ? string.Concat(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Eval("ClassName").ToString().Substring(0, 11), " .."))) : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("ClassName").ToString())%><%# Eval("ClassName").ToString().Length>0?":":"" %></h3>
                                </div>
                                <div style="float: left;">
                                    (LEVEL <span id="Rdlvlfrom">
                                        <%# Eval("CurrentReadinglevelFrom") %></span> TO LEVEL <span id="Rdlvlto">
                                            <%# Eval("CurrentReadinglevelUpto")%></span>)
                                </div>
                            </div>
                            <div style="float: right;width:30%; margin: -10px 22px 0px 0px;">
                                <div id="ReadingRecovery" class="ReadingRecovery" onclick="ReadingRecoveryUpdate();" style="width: 100%;">
                                    <div style="float: left; width: 16%;">
                                        <img id="RRChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>' alt=""
                                            style="display: none;" />
                                        <img id="RRUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                                            alt="" />
                                    </div>
                                    <div style="float: right; width: 81%; margin-top: 6px;">
                                        READING RECOVERY</div>
                                </div>
                                <asp:CheckBox ID="ReadingRecoveryCheck" runat="server" ClientIDMode="Static" Text='<%# Eval("ReadingRecovery") %>'
                                    Style="display: none;" />
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="StudentProfile_ReadingLevelDiv">
                                <div style="float: left; width: 100%;">
                                    <div class="StudentProfile_DecrementButtonDiv">
                                        <input type="button" id="Button1" class="StudentProfile_DecrementButton" onclick="decrement()" />
                                    </div>
                                    <div class="StudentProfile_ReadingLevelValueDiv" style="float: left;">
                                        <div>
                                            <asp:Label ID="PMReadingLevelLabel" CssClass="StudentProfile_ReadingLevelLabel" runat="server"
                                                ClientIDMode="Static" Text='<%# Eval("CurrentReadingLevel") %>'> </asp:Label>
                                        </div>
                                    </div>
                                    <div class="StudentProfile_IncrementButtonDiv">
                                        <input type="button" id="Button2" class="StudentProfile_IncrementButton" onclick="increment()" />
                                    </div>
                                </div>
                                <h5 id="ReadLvllbl">
                                    PM READING LEVEL
                                </h5>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--  <div class="StudentProfile_ReadingLevelSliderDiv">
                    <h5 class="ProfileSliderLabel">
                        TITLES AVAILABLE ON <font color="#20B3E6">
                            <%# Eval("FirstName").ToString().Length > 25 ? string.Concat(Eval("FirstName").ToString().Substring(0, 25).ToUpper(), ".. ") : Eval("FirstName").ToString().ToUpper() %>'S</font>
                        BOOKSHELF:
                    </h5>
                    <div class="eCollectionEditLbl" style=" width: 100%; float: left; height: 80px; ">
                        <RL:RLSlider ID="ReadingLevelSlider" runat="server">
                        </RL:RLSlider>
                        <input id="leftSlide" type="hidden" value='<%# Eval("CurrentReadinglevelFrom") %>' />
                        <input id="rightSlide" type="hidden" value='<%# Eval("CurrentReadinglevelUpto") %>' />
                    </div>
                </div>--%>
                <div class="StudentProfile_ReadingLevelSliderDiv">
                    <h5 class="ProfileSliderLabel">
                        TITLES AVAILABLE ON <font color="#20B3E6">
                            <%# Eval("FirstName").ToString().Length > 25 ? string.Concat(Eval("FirstName").ToString().Substring(0, 25).ToUpper(), ".. ") : Eval("FirstName").ToString().ToUpper() %>'S</font>
                        BOOKSHELF:
                    </h5>
                    <div class="eCollectionEditLbl">
                        <%-- style="margin-top: 30px">--%>
                        <RL:RLSlider ID="ReadingLevelSlider" runat="server">
                        </RL:RLSlider>
                         <input id="leftSlide" type="hidden" value='<%# Eval("CurrentReadinglevelFrom") %>' />
                        <input id="rightSlide" type="hidden" value='<%# Eval("CurrentReadinglevelUpto") %>' />
                    </div>
                </div>
                <div class="StudentProfile_FourBoxes_OuterBox">
                    <div class="StudentProfile_FourBoxes_InnerBox">
                        <div class="StudentProfile_FourBoxes">
                            <div class="ReadingTypeDiv_TopDiv">
                                <asp:Label ID="NoOfBoooksOpenedLabel" CssClass="ReadingTypeDiv_CountLabel" runat="server"
                                    Text='<%# Eval("BooksOpened") %>'></asp:Label>
                                <span class="ReadingTypeDiv_Label">BOOK/S OPENED</span><br />
                            </div>
                            <div class="ReadingType_Indept">
                                <asp:Label ID="IndependentCountLabel" runat="server" CssClass="ReadingTypeDiv_PercentageLabel"
                                    Text='<%# Eval("IndependentPercentage")+"%" %>'></asp:Label>
                                <span class="ReadingTypeDiv_TypeLabel">INDEPENDENT</span>
                            </div>
                            <div class="ReadingType_Dept">
                                <asp:Label ID="GuidedCountLabel" runat="server" Text='<%# Eval("GuidedPercentage")+"%" %>'
                                    CssClass="ReadingTypeDiv_PercentageLabel" Style="margin-left: 23px;"></asp:Label>
                                <span class="ReadingTypeDiv_TypeLabel">GUIDED</span>
                            </div>
                        </div>
                        <div class="StudentProfile_FourBoxes" style="float: right;">
                            <div class="ReadingAgeCountDiv">
                                <asp:Label ID="ReadingAgeLabel" runat="server" Text='<%# Eval("ReadingAge") %>' CssClass="ReadingAgeCount"></asp:Label>
                            </div>
                            <div class="ReadingAgeLabelDiv">
                                <h6 class="RecordingsLabel">
                                    READING AGE</h6>
                            </div>
                        </div>
                    </div>
                    <div class="StudentProfile_FourBoxes_InnerBox" style="float: right;">
                        <div class="StudentProfile_FourBoxes">
                            <div class="RecordingsCountDiv">
                                <asp:Label ID="NoOfRecordingsLabel" runat="server" Text='<%# Eval("RecordingsCount") %>'
                                    CssClass="RecordingsCount" ClientIDMode="Static"></asp:Label>
                            </div>
                            <div class="RecordingsLabelDiv">
                                <h6 class="RecordingsLabel">
                                    RECORDING/S MADE</h6>
                                <asp:HyperLink ID="ListenButton" ForeColor="#1FB5E7" runat="server" NavigateUrl="<%# MyRecordingsUrl() %>"
                                    CssClass="RecordingsListenLink" ClientIDMode="Static" Text="LISTEN" />
                            </div>
                        </div>
                        <div class="StudentProfile_FourBoxes" style="float: right;">
                            <div class="MyWordsCountDiv">
                                <asp:Label ID="NoOfWordsLabel" runat="server" ClientIDMode="Static" Text='<%# Eval("WordsCount") %>'
                                    CssClass="WordsCount"></asp:Label>
                            </div>
                            <div class="MyWordsLabelDiv">
                                <h6 class="RecordingsLabel">
                                    WORD/S SAVED TO</h6>
                                <asp:HyperLink ID="MyWordsButton" ForeColor="#1FB5E7" runat="server" NavigateUrl="<%# MyWordsUrl() %>"
                                    CssClass="SeeMyWordsLink" ClientIDMode="Static" Text="MY WORDS" />
                            </div>
                        </div>
                    </div>
                </div>
                <div style="width: 99.8%; margin: 3px 3px 0px 0px; float: left; border: 1px solid lightgray;
                    min-height: 200px;">
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
                <div class="graphArrowhdr1">
                    <div class="grpahArrow1">
                    </div>
                    <div class="grpahArrow2">
                    </div>
                </div>
                <div class="graphArrowhdr2">
                    <div class="grpahArrow1">
                    </div>
                    <div class="grpahArrow2">
                    </div>
                </div>
                <div class="DurationDiv">
                    <div style="width: 21%; float: left; height: 50px; padding-top: 20px; border-right: 1px solid #CCC;">
                        <i class="TodayDateItalics">
                            <asp:Label ID="sixmonthBefore" runat="server" ></asp:Label></i>
                        <p style="font-weight: bold; margin-top: 1px; text-align: center; height: 10px; font-family: Arial;">
                            LEVEL
                            <%# Eval("StartingReadingLevel") %></p>
                    </div>
                    <div style="width: 55%; font-weight: bold; float: left; padding-top: 20px; margin-top: 11px;
                        font-family: Arial;">
                        <center>              
                            <asp:Label ID="TotalMonth" style="margin-right: 3px"  runat="server" >0 MONTH/S</asp:Label>                                         
                        </center>
                    </div>
                    <div style="width: 22%; float: right; margin-left: 4px; height: 50px; padding-top: 20px;
                        margin-right: -10px; border-left: 1px solid #CCC;">
                        <i class="TodayDateItalics">
                            <asp:Label ID="TodayDate" runat="server" Text='<%# string.Format("{0:dd}{1} {0:MMM yyyy}",DateTime.Now,(DateTime.Now.Day == 1)? "st":(DateTime.Now.Day == 21)? "st":(DateTime.Now.Day == 31)? "st":(DateTime.Now.Day == 2)? "nd":(DateTime.Now.Day ==22)? "nd":(DateTime.Now.Day == 3)? "rd":(DateTime.Now.Day == 23)? "rd":"th") %>'></asp:Label></i>
                        <p style="font-weight: bold; margin-top: 1px; text-align: center; height: 10px; font-family: Arial;">
                            LEVEL <span id="GraphCurrRL">
                                <%# Eval("CurrentReadingLevel")%></span></p>
                    </div>
                </div>
                <asp:Repeater ID="ReadingLevelRepeater" runat="server">
                    <ItemTemplate>
                        <div class="Percentage_ReadingLevel">  
                            <div class="LevelBooksRead">
                                <asp:Label ID="PercentageLevelLabel" Style="color: #20B3E6" runat="server"><%# string.Concat(Eval("PercentRead").ToString(),"% ") %></asp:Label>
                                OF BOOKS READ WERE LEVEL
                                <asp:Label ID="CurrentReadinLevel" runat="server" Text='<%# Eval("CurrentReadingLevel") %>'></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <input type="hidden" value='<%# Eval("UserLoginName") %>' id="usernamehdn" />
        </ItemTemplate>
    </asp:ListView>
</div>
<div id="SecondDiv" style="width: 100%; float: left;">
    <div id="Switch" class="ProfileSwitchON">
        <div id="HistoryButton" style="width: 144px; height: 51px; border: 0px solid white;
            cursor: pointer; background-color: transparent; float: left;">
        </div>
        <div id="StudentsButton" style="border: 0px solid white; width: 144px; height: 51px;
            cursor: pointer; background-color: transparent; float: left;">
        </div>
    </div>
    <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
        <div  class="bubble" >
            <asp:Label ID="Message1" runat="server" Text="" />            
            <asp:Label ID="Message2" runat="server"  Text="" />            
        </div>
    </div> 
    <div id="HistoryDiv" class="Div_FullWidth">
        <HD:HistoryDiv ID="ReadingHistory" runat="server">
        </HD:HistoryDiv>
    </div>
    <div id="StudentDiv" class="ButtonsDiv srtbtnshide">
        <PG:Groups ID="ProfileGroupsList" runat="server">
        </PG:Groups>
    </div>
</div>
<input id="PageName" type="hidden" value="Profile" />
<asp:HiddenField ID="pointsHdn" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="grfHdnLine" ClientIDMode="Static" runat="server" />
<script type="text/javascript">
    function increment() {
        var inc = parseInt(jQuery('#PMReadingLevelLabel').text());
        if (isNaN(inc))
            inc = 0;
        if (inc != 24)
            SetReadingLevel(inc + 1);
        return false;
    }
    function decrement() {
        var dec = parseInt(jQuery('#PMReadingLevelLabel').text());
        if (dec != 1)
            SetReadingLevel(dec - 1);
        return false;
    }
    function SetReadingLevel(Rlevel) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=readinglevel&studentid=' + jQuery('#studentIdLabel').html() + '&readinglevel=' + Rlevel),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                jQuery('#PMReadingLevelLabel').text(Rlevel);
                jQuery('#GraphCurrRL').text(Rlevel);
            }
        });
    }

    function SetBookReadLevel() {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=bookreadinglevel&studentid=' + jQuery('#studentIdLabel').html() + '&bookreadinglevel=' + jQuery('#amount').val()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                var bkRdlvl = jQuery('#amount').val().split('-');
                jQuery('#Rdlvlfrom').text(parseInt(bkRdlvl[0]) + 1);
                jQuery('#Rdlvlto').text(bkRdlvl[1]);
            }
        });
    }

    jQuery(function () {
        showtab();
               

       /* $('.LevelBooksRead').map(function () {
            if ($(this).children().first().text().trim() == '0%') {
                $(this).parent().addClass('HideItems');
            }
        });

        if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
        $('#slider-range').addClass('sliderDivProfSaf');
        $('#24').addClass('rdLvl24');
        $('h6').removeClass('RecordingsLabel').addClass('RecordingsLabelSaf');
        $('#ReadLvllbl').addClass('ReadLvllbl');
        }
        if ($.browser.msie) {
        $('#SliderContent').css('width', '100.3%');
        }*/
        $('#eCollection_Menu_MidHolder').addClass('srtbtnshide');
        $('#ProfilePageBtn').removeClass('srtbtnshide');
        $('#ECollLeftModule').css('margin-top', '10px');

        jQuery("#StudentsButton").click(function () {
            jQuery("#StudentDiv").removeClass("srtbtnshide");
            jQuery("#HistoryDiv").addClass("srtbtnshide");
            jQuery('#Switch').removeClass('ProfileSwitchON');
            jQuery('#Switch').addClass('ProfileSwitchOFF');
            var msg2 = $('#<%=Message2.ClientID%>').text();
            if (msg2 == "") {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "none");
            }
            else {
                jQuery("#<%=MessageOuterDiv.ClientID%>").css("display", "block");
                jQuery("#<%=Message1.ClientID%>").css("display", "none");
                jQuery("#<%=Message2.ClientID%>").css("display", "block");
            }
            showtab();
            return false;
        });
        jQuery("#HistoryButton").click(function () {
            jQuery("#HistoryDiv").removeClass("srtbtnshide");
            jQuery("#StudentDiv").addClass("srtbtnshide");
            jQuery('#Switch').removeClass('ProfileSwitchOFF');
            jQuery('#Switch').addClass('ProfileSwitchON');
            var msg1 = $('#<%=Message1.ClientID%>').text();
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
        ValidateRR();
        ValidateESL();
        var ids = $('#pointsHdn').val().split('-');
        var ids2 = $('#grfHdnLine').val();
        for (var i = 0; i < (12 - ids2) + 1; i++) {
            $('#Graph1 #LineDiv' + i + ' div').addClass('NoBackground');
            
        }
        for (var i = 12 - ids2 + 1; i < 12; i++) {
            $('#Graph1 #LineDiv' + i + ' div').addClass('NoBackground');
            $('#Graph1 #LineDiv' + i).hide();
        }
        for (var i = 0; i < ids.length; i++) {
            $('#Div' + ids[i]).removeClass('NoBackground');
            $('#Div' + ids[i]).parent().show();
            $('#Div' + ids[i]).corner('90px');
        }
    });
    function ReadingRecoveryUpdate() {
        ReadingRecovery();
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=readingrecovery&studentid=' + jQuery('#studentIdLabel').html() + '&readingrecovery=' + jQuery("label[for=ReadingRecoveryCheck]").html()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
            }
        });
    }

    function ESLUpdate() {
        ESL();
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=esl&studentid=' + jQuery('#studentIdLabel').html() + '&esl=' + jQuery("label[for=ESLCheck]").html()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
            }
        });
    }
</script>
<script src="<%=Page.ResolveUrl("DesktopModules/eCollection_Students/Scripts/jquery.corner.js")%>"
    type="text/javascript"></script>
