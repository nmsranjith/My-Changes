<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeacherProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.TeacherProfile" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="ReadingHistory.ascx" TagName="HistoryDiv" TagPrefix="HD" %>
<%@ Register Src="MyGroups.ascx" TagName="Groups" TagPrefix="PG" %>
<link href="<%=Page.ResolveUrl("DesktopModules/eCollection_Teachers/CSS/jQuery.ui.smoothness.css")%>"
    rel="Stylesheet" type="text/css" />
<script type="text/javascript">
    jQuery(function () {
        leftStartValue = jQuery('#leftSlide').val() - 1;
        rightStartValue = jQuery('#rightSlide').val();
        pageName ='Profile';
    });
</script>
<div id="FirstDiv" style="width: 100%; float: left;">
    <asp:ListView ID="TeacherProfileRepeater" runat="server">
        <ItemTemplate>           
            <span id="TeacherIdLabel" style="display: none;">
                <%# Eval("TeacherId")%></span>
            <div id="InnerDiv" class="TeacherProfile_MainDiv">
                <div style="width: 106%; height: 100px; float: left; margin: 0px 10px 5px 0px;">
                    <div class="TeacherProfile_NameDiv">
                        <div style="float: left; width: 90%;">
                            <div style="float: left; width: 91%; margin: 20px 0px 10px 15px;">
                                <h3>
                                    <%# string.Concat(Eval("FirstName"), " ", Eval("LastName")).Length > 25 ? string.Concat(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Eval("FirstName"), " ", Eval("LastName")).Substring(0, 25).ToLower()), ".. ") : System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Eval("FirstName"), " ", Eval("LastName")).ToLower())%>:
                                </h3>
                            </div>
                            <div class="ClassDetails">
                                <div style=" float: left;">
                                    <%# Eval("ClassName")%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="TeacherProfile_ThreeBoxes_OuterBox">
                    <div class="TeacherProfile_ThreeBoxes_InnerBox1">
                        <div class="TeacherProfile_ThreeBoxes1" style="width: 100%;">
                            <div class="ReadingTypeDiv_TopDiv">
                                <asp:Label ID="NoOfBoooksOpenedLabel" CssClass="ReadingTypeDiv_CountLabel" ClientIDMode="Static" runat="server"
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
                    </div>
                    <div class="TeacherProfile_ThreeBoxes_InnerBox2">
                        <div class="TeacherProfile_ThreeBoxes2">
                            <div class="RecordingsCountDiv">
                                <asp:Label ID="NoOfRecordingsLabel" ClientIDMode="Static" runat="server" Text='<%# Eval("RecordingsCount") %>'
                                    CssClass="RecordingsCount"></asp:Label>
                            </div>
                            <div class="RecordingsLabelDiv">
                                <h5 class="RecordingsLabel">
                                    RECORDING/S MADE</h5>
                                <asp:HyperLink ID="ListenButton" ForeColor="#1FB5E7" runat="server" NavigateUrl="<%# MyRecordingsUrl() %>"
                                    CssClass="RecordingsListenLink" ClientIDMode="Static" Text="LISTEN" />
                            </div>
                        </div>
                        <div class="TeacherProfile_ThreeBoxes3">
                            <div class="MyWordsCountDiv">
                                <asp:Label ID="NoOfWordsLabel" runat="server" ClientIDMode="Static" Text='<%# Eval("WordsCount") %>'
                                    CssClass="WordsCount"></asp:Label>
                            </div>
                            <div class="MyWordsLabelDiv">
                                <h5 class="MyWordsLabel">
                                    WORD/S SAVED TO</h5>
                                <asp:HyperLink ID="MyWordsButton" ForeColor="#1FB5E7" runat="server" NavigateUrl="<%# MyWordsUrl() %>"
                                    CssClass="SeeMyWordsLink" ClientIDMode="Static" Text="MY WORDS" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="TeacherProfile_ReadingLevelSliderDiv">
                    <h5 class="Div_FullWidth ProfileSliderLabel">
                        TITLES AVAILABLE ON <font color="#20B3E6">
                            <%# Eval("FirstName").ToString().Length > 25 ? string.Concat(Eval("FirstName").ToString().Substring(0, 25).ToUpper(), ".. ") : Eval("FirstName").ToString().ToUpper() %>'S</font>
                        BOOKSHELF:
                    </h5>
                    <div class="eCollectionEditLbl">
                        <RL:RLSlider ID="ReadingLevelSlider" runat="server"></RL:RLSlider>
                        <input id="leftSlide" type="hidden" value='<%# Eval("BookLevelFrom") %>' />
                        <input id="rightSlide" type="hidden" value='<%# Eval("BookLevelUpto") %>' />
                    </div>
                </div>
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
    <div id="HistoryDiv" style="display: block">
        <HD:HistoryDiv ID="ReadingHistory" runat="server"></HD:HistoryDiv>
    </div>
    <div id="StudentDiv" style="display: none; float: left; width: 100%;">
        <PG:Groups ID="ProfileGroupsList" runat="server"></PG:Groups>
    </div>
    <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
        <div class="bubble" >
            <asp:Label ID="Message1" runat="server" Text="" />  
            <asp:Label ID="Message2" runat="server" Text="" />
        </div>
    </div> 
</div>
<asp:HiddenField ID="pointsHdn" ClientIDMode="Static" runat="server" />
<script type="text/javascript" language="javascript">

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
    function longDec() {
        var dec = parseInt(jQuery('#ReadingLevelLabel').text());
        if (dec != 1)
            jQuery('#ReadingLevelLabel').text(dec - 1);
    }
    jQuery(function () {    

        jQuery("#StudentsButton").click(function () {
            jQuery("#StudentDiv").css("display", "block");
            jQuery("#HistoryDiv").css("display", "none");
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
            jQuery("#HistoryDiv").css("display", "block");
            jQuery("#StudentDiv").css("display", "none");
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
    });  

    function SetBookReadLevel() {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=bookreadinglevel&teacherid=' + jQuery('#TeacherIdLabel').html() + '&bookreadinglevel=' + jQuery('#amount').val()),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
            }
        });
    }

</script>
