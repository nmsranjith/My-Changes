<%@ Control Language="C#" Inherits="DotNetNuke.Modules.eCollection_Students.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
    <link rel="SHORTCUT ICON" href="<%=Page.ResolveUrl("/Portals/0/Images/PM logo.ico")%>" type="image/x-icon" />
<link href="/desktopmodules/ecollection_students/module.css" type="text/css" />
<div id="MainDiv" class="MainDiv">
    <div class="TopBand">
        <div id="HeaderHdr" class="HeaderHdr">
            <h1 id="PageHeader" runat="server" clientidmode="Static">
            </h1>
        </div>
        <div id="TopBtnHdr" class="TopBtnHdr">
            <div id="HeaderBtn">
                <div id="bg" style="border: 0px solid transparent; text-decoration: initial; width: 25px;
                    float: left;">
                </div>
                <asp:HyperLink ID="PageHeaderButton" runat="server" EnableViewState="false" ClientIDMode="Static"></asp:HyperLink>
            </div>
        </div>
    </div>
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
        <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
        <div class="ShadowBox">
        </div>
        <div class="ContentPlaceHolder">
            <asp:PlaceHolder ID="ContentPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</div>
<asp:HiddenField ID="StudentsCount" runat="server" ClientIDMode="Static" />
<div id="dialog-message" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <literal style="font-family: Raleway-regular, Arial, sans-serif; font-size: 10pt;
                color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please contact your Administrator</literal>
        </div>
        <div style="width: 92%;">
            <input type="button" id="PopuOkButton" value="Ok" class="popupokbtn" />
        </div>
    </div>
</div>
<div class="HideItems">
    <asp:HiddenField ID="SubsCnt" runat="server" ClientIDMode="Static" />
    <div id="SelectedSubs">
        <div class="Div_FullWidth SubsBannerDiv">
            You are using <span id="SelectedSubscription" runat="server" clientidmode="Static">
            </span>
        </div>
    </div>
</div>
<asp:HiddenField ID="schoolname" runat="server" ClientIDMode="Static" />
<script type="text/javascript">
    jQuery(
     function SetMenuBackground() {
         jQuery('#eCollectionecollnk').addClass('DbecollectionActive'); 
         jQuery('#bannertitle').addClass('bannerMultiProfile');
        
         if (parseInt(jQuery('#SubsCnt').val()) > 1) {
             jQuery(jQuery('#SelectedSubs').html()).appendTo('#masterhead');
             jQuery('#MainDiv').addClass('MultiSubsMainDiv');
             jQuery('.bannersececollection').addClass('bannerMultiHeight');
             $('#SubscriptionTabHolder').show();
         }
         else {
             $('#SubscriptionTabHolder').hide();
         }


         SelectedMenuCss('StudentsTabHolder', 'StudentsTab');
         $('#eCollectionlnk').addClass('menuitemactive');
         var contentheight = jQuery('#eCollectionContent').height();
		 if (contentheight < 900)
			contentheight = 900;
         
     });
	 
     function setContentHeight(contentheight) {        
		if (contentheight < 900)
			contentheight = 900;
         jQuery('#eCollectionMenu').height(contentheight + 'px');
         jQuery('#eCollectionContent').height((contentheight -5) + 'px');
     }

     jQuery(function () {
         $('#ecollimg_ecoll').addClass('ecollactive_ecoll');
         
         $("#PopuOkButton").click(function () {
             kwindow.data("kendoWindow").close();
             window.location.href = "students.aspx";
             return false;
         });

         $("#OkButton").click(function () {
             kwindow.data("kendoWindow").close();
             return false;
         });

         var pagename = (window.location.href).split("=");
         var arr = ['createprofile', 'bulkupload', 'editprofile', 'addtogroup'];
         if ($.inArray(pagename[pagename.length - 1].toLowerCase(), arr) > -1) {
             $("#DashboardTabHolder").css("visibility", "hidden");
             $("#GroupsTabHolder").css("visibility", "hidden");
             $("#SessionTabHolder").css("visibility", "hidden");
             $("#BooksTabHolder").css("visibility", "hidden");
             $("#TeachersTabHolder").css("visibility", "hidden");
             $("#SubscriptionTabHolder").css("visibility", "hidden");
             $("#eCollectionMenuMid").css("visibility", "hidden");

             $("#eBookManagementTabHolder").css("visibility", "hidden");
             $("#AppDataTabHolder").css("visibility", "hidden");
         }

     });

    function HeaderButtonStyle() {
        jQuery('#PageHeaderButton').addClass('TopRightEditbtn');
    }

    function SelectedMenuCss(holder, tab) {
        jQuery('#' + holder).addClass('selectedTabHolder');
        jQuery('#' + tab).addClass('selectedTab');
    }

    function ReadingRecovery() {
        if ($("label[for=ReadingRecoveryCheck]").html() == 'Y') {
            jQuery('#RRChk').hide();
            jQuery('#RRUnChk').show();
            jQuery('#ReadingRecoveryCheck').removeAttr('checked');
            $("label[for=ReadingRecoveryCheck]").html('N');
        }
        else {
            jQuery('#RRChk').show();
            jQuery('#RRUnChk').hide();
            jQuery('#ReadingRecoveryCheck').attr('checked', 'checked');
            $("label[for=ReadingRecoveryCheck]").html('Y');
        }
    }
    function ESL() {
        if ($("label[for=ESLCheck]").html() == 'Y') {
            jQuery('#EslChk').hide();
            jQuery('#EslUnChk').show();
            jQuery('#ESLCheck').removeAttr('checked');
            $("label[for=ESLCheck]").html('N');
        }
        else {
            jQuery('#EslChk').show();
            jQuery('#EslUnChk').hide();
            jQuery('#ESLCheck').attr('checked', 'checked');
            $("label[for=ESLCheck]").html('Y');
        }
    }

    function isValidDate(inputDate) {
        if ($.browser.msie && parseInt($.browser.version, 10) < 10 && $('#DateofBirthTextBox').val() == 'Date of birth (dd/mm/yyyy)') {
            $('#DateofBirthTextBox').val('');
        }
        if (jQuery('#DateofBirthTextBox').val() == '')
            return true;
        var myRegex = /^(\d{1,2})([\-\/])(\d{1,2})\2(\d{4}|\d{2})$/;
        var match = myRegex.exec(inputDate);

        if (match != null) {
            var auxDay = match[1];
            var auxMonth = match[3] - 1;
            var auxYear = match[4];
            auxYear = auxYear.length < 3 ? (auxYear < 70 ? '20' + auxYear : '19' + auxYear) : auxYear;
            var testingDate = new Date(auxYear, auxMonth, auxDay);


            if ((auxDay == testingDate.getDate()) && (auxMonth == testingDate.getMonth()) && (auxYear == testingDate.getFullYear())) {
                return true;
            }
            else {
                GetMessage("VALIDATE_DOB1");
                jQuery('#DateofBirthTextBox').parent().parent().css('border', '1px solid #ED175B');
            }

        } else {
            GetMessage("VALIDATE_DOB1");
            jQuery('#DateofBirthTextBox').parent().parent().css('border', '1px solid #ED175B');
            return false
        };
    }

    function isValidEmailAddress(emailAddress) {
        jQuery('#CreateForm input[type=text]').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        if (jQuery('#EmailTextBox').val().trim() == '')
            return true;
        var pattern = new RegExp(/^\b[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b$/i);
        return pattern.test(emailAddress);
    }

    function checkForName(textboxid, value) {
        var pattern = new RegExp(/^[a-zA-Z .']+$/);
        jQuery('#CreateForm input[type=text]').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        if (!pattern.test(jQuery('#' + textboxid).val().trim())) {
            $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
            
            GetMessage(value);
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        return true;
    }

    function checkForUserName(textboxid, value) {
        var pattern1 = new RegExp(/^[0-9]+$/);
        var pattern2 = new RegExp(/^[0-9][0-9a-zA-Z._]+$/);
        var pattern3 = new RegExp(/^[a-zA-Z][0-9a-zA-Z._]+$/);
        jQuery('#CreateForm input[type=text]').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        if (pattern1.test(jQuery('#' + textboxid).val().trim())) {
            
            GetMessage("VALIDATE_USERNAME1");
            
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        else if (pattern2.test(jQuery('#' + textboxid).val().trim())) {
            
            GetMessage("VALIDATE_USERNAME2");
            
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        else if (!pattern3.test(jQuery('#' + textboxid).val().trim())) {
            
            GetMessage("VALIDATE_USERNAME3");
            
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        return true;
    }
    function checkPassword(textboxid, value) {
        var pattern1 = new RegExp(" ", "g");
        jQuery('#CreateForm input[type=text]').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        if (jQuery('#' + textboxid).val().trim().length <= 2) {
            GetMessage("VALIDATE_PASSWORD2");
            
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        else if (pattern1.test(jQuery('#' + textboxid).val().trim())) {
            
            GetMessage("VALIDATE_PASSWORD1");
            
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
         
        return true;
    }

    function ValidateText(textboxid) {        
        if (jQuery('#' + textboxid).val().trim().length == 0) {            
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        return true;
    }

    function ValidateDropDown(dropdownid) {
        if ($('#' + dropdownid).val() == 0) {
            $('#' + dropdownid).parent().parent().parent().addClass('MandatoryClass');
            return false;
        }
        return true;
    }
    function ValidateESL() {
        if (jQuery("label[for=ESLCheck]").html() == 'Y') {
            jQuery('#EslChk').show();
            jQuery('#EslUnChk').hide();
            jQuery('#ESLCheck').attr('checked', 'checked');
        }
        else {
            jQuery('#EslChk').hide();
            jQuery('#EslUnChk').show();
            jQuery('#ESLCheck').removeAttr('checked');
        }
    }
    function ValidateRR() {
        if (jQuery("label[for=ReadingRecoveryCheck]").html() == 'Y') {
            jQuery('#RRChk').show();
            jQuery('#RRUnChk').hide();
            jQuery('#ReadingRecoveryCheck').attr('checked', 'checked');
        }
        else {
            jQuery('#RRChk').hide();
            jQuery('#RRUnChk').show();
            jQuery('#ReadingRecoveryCheck').removeAttr('checked');
        }
    }
    function createProfile() {
        $("#dialog-message").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        kwindow = $("#dialog-message"); 
        if (!kwindow.data("kendoWindow")) {
            kwindow.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            kwindow.data("kendoWindow").center();
        }
        kwindow.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().addClass("popupClosebg");
            return false;
        });
    }

    function PlayAudio(id) {
        var path = GetFile("/Portals/0/audio/page/");
        if (id.parentNode.children[0].innerHTML.trim() != "")
            path = path + id.parentNode.children[0].innerHTML.trim();
        
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
        
        });
        showtab();
    }
    function ClickSelectAll() {
        if ($('#CheckAllDiv').css('display') == 'block') {
            $('#CheckAllDiv').click();
        }
        else {
            $('#UnCheckAllDiv').click();
        }
    }
    function GetMessage(text) {       
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=getmessages&msgcode=' + text),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (errMsg) {
                $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript").css('height', 'auto');
                $("#MsgDiv").text(errMsg).show();
            }
        });
    }
</script>
