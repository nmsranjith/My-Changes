<%@ Control language="C#" Inherits="DotNetNuke.Modules.eCollection_Teachers.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>

<div id="MainDiv" class="MainDiv"> 
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
       <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server">          
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
        <div class="ShadowBox">
        </div>
        <div style="width: 100%; float: left;z-index: 900; background-color: white;">
            <asp:PlaceHolder ID="ContentPlaceHolder" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</div>
<asp:HiddenField ID="PageName" runat="server" ClientIDMode="Static" />
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
         //jQuery('#bannertitle h1').html(jQuery('#lblbannertxt').html()).show();
         if (parseInt(jQuery('#SubsCnt').val()) > 1) {
             jQuery(jQuery('#SelectedSubs').html()).appendTo('#masterhead');            
             jQuery('#MainDiv').addClass('MultiSubsMainDiv');
             jQuery('.bannersececollection').addClass('bannerMultiHeight');
             $('#SubscriptionTabHolder').show();
         }
         else {
             $('#SubscriptionTabHolder').hide();
         }

         $('#ecollimg_ecoll').addClass('ecollactive_ecoll');        
         SelectedMenuCss('TeachersTabHolder', 'TeachersTab');

         var contentheight = jQuery('#eCollectionContent').height();
         jQuery('#eCollectionMenu').height(contentheight + 'px');
         jQuery('#eCollectionContent').height((contentheight + 5) + 'px');

         $('#eCollectionlnk').addClass('menuitemactive');

         var pagename = (window.location.href).split("=");
         var arr = ['createprofile', 'bulkupload', 'addtogroup'];
         if ($.inArray(pagename[pagename.length - 1].toLowerCase(), arr) > -1) {
             $("#DashboardTabHolder").css("visibility", "hidden");
             $("#StudentsTabHolder").css("visibility", "hidden");
             $("#SessionTabHolder").css("visibility", "hidden");
             $("#BooksTabHolder").css("visibility", "hidden");
             $('#GroupsTabHolder').css("visibility", "hidden");
             $("#SubscriptionTabHolder").css("visibility", "hidden");
             $("#eCollectionMenuMid").css("visibility", "hidden");

             $("#eBookManagementTabHolder").css("visibility", "hidden");
             $("#AppDataTabHolder").css("visibility", "hidden");
         }
     }
    );
     function ClickSelectAll() {
         if ($('#CheckAllDiv').css('display') == 'block') {
             $('#CheckAllDiv').click();
         }
         else {
             $('#UnCheckAllDiv').click();
         }
     }
    function SelectedMenuCss(holder, tab) {
        jQuery('#' + holder).addClass('selectedTabHolder');
        jQuery('#' + tab).addClass('selectedTab');
    }

    function disp_clear() {
        jQuery('#CreateForm input').val('');
        jQuery('select').find('option:first-child').attr('selected', true);
        window.scroll(0, 0);
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
       // }).bind($.jPlayer.event.play, function () { // pause other instances of player when current one play
       //     $(this).jPlayer("pauseOthers");
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
        }); //.bind($.jPlayer.event.play, function () { // pause other instances of player when current one play
            //$(this).jPlayer("pauseOthers");
        //});
        showtab();
    }
    function GetMessage(text) {
        $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript").css('height', 'auto');
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=getmessages&msgcode=' + text),
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (errMsg) {
                $("#MsgDiv").text(errMsg).show();
            }
        });
    }
</script>
