<%@ Control language="C#" Inherits="DotNetNuke.Modules.eCollection_Books.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>

<style type="text/css">
#overlay {
z-index:1000;
position:absolute;
top:0;
bottom:0;
left:0;
width:100%;
background:#F5F5F5;
opacity:0.45;
-moz-opacity:0.45;
filter:alpha(opacity=45);
visibility:hidden;
}
</style>
<div class="MainDiv" id="MainDiv">
    <div class="TopBand">
        <asp:Label ID="PageHeaderLabel" runat="server" style="float:left;font-size: 29pt;"></asp:Label>
        <asp:Button ID="PageHeaderButton" runat="server" Text="Cancel"
            Visible="false" CssClass="RightTopCancelButton" />
        <input id="Editbtn" type="button" class="TopRightEditbtn" />
    </div>
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
        <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server">
        <div id="overlay"></div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
       
        <div style="width: 100%; float: left; z-index: -1000; background-color: white;">
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
<asp:HiddenField ID="PageName" runat="server" ClientIDMode="Static" />

<asp:HiddenField ID="schoolname" runat="server" ClientIDMode="Static" />
<script type="text/javascript">
    jQuery(document).ready(function () {
        pageName = 'Profile';
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
        var contentheight = jQuery('#eCollectionContent').height();
        jQuery('#eCollectionMenu').height(contentheight + 'px');
        jQuery('#eCollectionContent').height((contentheight + 5) + 'px');
        jQuery('#BooksTabHolder').addClass('selectedTabHolder');
        jQuery('#BooksTab').addClass('selectedTab');
    });

//    function GetFile(path) {
//        var pathname = window.location.pathname;
//        var temppath = pathname.split('/');
//        var root;
//        root = "http://" + window.location.host; 
//        var url = root + path;
//        return url;
//    }
</script>

