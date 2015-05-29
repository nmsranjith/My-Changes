<%@ Control Language="C#" Inherits="DotNetNuke.Modules.eCollection_Groups.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<div class="MainDiv" id="MainDiv">
    <div class="TopBand" style="display: none">
        <div style="float: left; width: 100%;">
            <h1 id="PageHeader" clientidmode="Static" runat="server">
            </h1>
            <span id="ClassNameLabel" clientidmode="Static" runat="server" style="float: left;
                color: #1FB5E7; padding-left: 15px; font-size: 29pt;"></span>
            <asp:Button ID="PageHeaderButton" runat="server" ClientIDMode="Static" Visible="false"
                CssClass="RightTopCancelButton" />
            <input id="Editbtn" type="button" class="TopRightEditbtn" />
        </div>
    </div>
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
        <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server">
            <div id="overlay">
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
        <div style="width: 100%; float: left; z-index: -1000;">
            <asp:PlaceHolder ID="ContentPlaceHolder" runat="server"></asp:PlaceHolder>
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
    $(document).ready(function () {
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
        $('#eCollectionlnk').addClass('menuitemactive');
        var pagename = (window.location.href).split("=");
        var arr = ['creategroup', 'editgroup', 'addstudenttocreategroup', 'addteachertocreategroup', 'addstudenttoeditgroup', 'addteachertoeditgroup'];
        if ($.inArray(pagename[pagename.length - 1].toLowerCase(), arr) > -1) {
            $("#DashboardTabHolder").css("visibility", "hidden");
            $("#SubscriptionTabHolder").css("visibility", "hidden");
            $("#StudentsTabHolder").css("visibility", "hidden");
            $("#SessionTabHolder").css("visibility", "hidden");
            $("#BooksTabHolder").css("visibility", "hidden");
            $("#TeachersTabHolder").css("visibility", "hidden");
            $(".eCollection_Menu_Mid_hr").css("visibility", "hidden");

            $("#eBookManagementTabHolder").css("visibility", "hidden");
            $("#AppDataTabHolder").css("visibility", "hidden");
        }

        jQuery('#GroupsTabHolder').addClass('selectedTabHolder');
        jQuery('#GroupsTab').addClass('selectedTab');
    });
   
    function getInternetExplorerVersion()
    // Returns the version of Internet Explorer or a -1
    // (indicating the use of another browser).
    {
        var rv = -1; // Return value assumes failure.
        if (navigator.appName == 'Microsoft Internet Explorer') {
            var ua = navigator.userAgent;
            var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
            if (re.exec(ua) != null)
                rv = parseFloat(RegExp.$1);
        }
        return rv;
    }
    function showleftline(res) {
        if (res == 1) {
            $('#RepeaterLftConntrLine').show();
            $('#LeftConnectorLine').show().css('min-height', '17px');
        }
        else {
            $('#RepeaterLftConntrLine').hide();
            $('#LeftConnectorLine').hide();
        }
    }
</script>
