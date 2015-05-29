<%@ Control Language="C#" CodeBehind="~/DesktopModules/Skins/skin.cs" AutoEventWireup="false"
    Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="NAV" Src="~/Admin/Skins/Nav.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TEXT" Src="~/Admin/Skins/Text.ascx" %>
<%@ Register TagPrefix="dnn" TagName="BREADCRUMB" Src="~/Admin/Skins/BreadCrumb.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LEFTMENU" Src="~/Admin/Skins/LeftMenu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKS" Src="~/Admin/Skins/Links.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="STYLES" Src="~/Admin/Skins/Styles.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.DDRMenu.TemplateEngine" Assembly="DotNetNuke.Web.DDRMenu" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="CONTROLPANEL" Src="~/Admin/Skins/controlpanel.ascx" %>
<%@ Register TagPrefix="dnn" TagName="UTILITIES" Src="~/controls/Utilities.ascx" %>
<%@ Register TagPrefix="cengage" TagName="CENGAGESUBMENU" Src="~/controls/SubMenuControl.ascx" %>
<%@ Register TagPrefix="cengage" TagName="CENGAGESEARCH" Src="~/controls/SearchControl.ascx" %>
<%@ Register TagPrefix="cengage" TagName="FOOTERCENGAGESEARCH" Src="~/controls/FooterSearcControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>

<dnn:STYLES runat="server" ID="StylesIE8" Name="IE8Minus" StyleSheet="ie8skin.css"
    Condition="LT IE 10" UseSkinPath="true" />

<style type="text/css">
body
{
overflow-x:hidden;
}
.backgrdimage 
   {
        background-image: url('./Portals/0/Images/av_search.png');
        background-repeat: no-repeat;
		float: left;
		height: 20px;
		width: 30px;
		padding-bottom: 18px;
		background-position: 14px 10px;
    }
    .backgrdimageopen
    {
        background-image: url('./Portals/0/Images/av_search_sel.png');
        background-repeat: no-repeat;
		float: left;
		height: 20px;
		width: 30px;
		padding-bottom: 18px;
		background-position: 14px 10px;
    }
    .k-widget .k-window
    {
        margin-top: 10%;
        background: transparent;
    }
    .k-window
    {
        box-shadow: none;
        background: transparent;
    }
    .k-window div.k-window-content
    {
        overflow: hidden;
        background: transparent;
    }
    div.k-window
    {
        border: 0px solid white;
    }
    .k-widget .k-window
    {
        margin-top: 10%;
        background: transparent;
    }
    .k-window
    {
        box-shadow: none;
        background: transparent;
    }
    .k-window div.k-window-content
    {
        overflow: hidden;
        background: transparent;
    }
    div.k-window
    {
        border: 0px solid white;
    }
    .k-header
    {
        background-color: white;
        border: none;
    }
    .k-block > .k-window-titlebar
    {
        border-bottom-style: none;
    }
    .k-window-titlebar
    {
        background: transparent;
        
    }
    #window1
    {
        background: transparent;
    }
    .k-window-action.k-link
    {
        background-image: url('./Portals/0/images/close.png') ;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -19px !important;
        margin-top: 15px ;
        border: none !important;
        margin-left: -10px;
    }
    .k-window-action.k-link.k-state-hover:hover
    {
        background-image: url('./Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 28px !important;
        width: 28px !important;
        z-index: 1003 !important;
        margin-right: -19px !important;       
        border: none !important;
        margin-left: -10px;
        background-color:transparent!important;
         /* background:transparent !important;*/
    }
    
    .k-window-actions
    {
        background: transparent;
    }
    .k-i-close
    {
        background-image: initial !important;
    }
    .k-window-titlebar .k-state-hover
    {
        padding: -1px !important;
    }
    .k-window-action .k-link
    {
        display: block !important;
        position: fixed !important;
    }    
    .k-icon .k-i-close
    {
        background-image: none !important;
    }
   
.adv1_txt 
{
    font-size: 9pt !imporant;
	 letter-spacing: 1px !important;
	  float: left;
    font-size: 9pt;    
    line-height: 42px;
    margin-left: 4%;
    overflow: hidden;
    text-align: center;
    text-transform: uppercase;
	}
	
	.advSrchbtn_Top
{
    width:195px !important;
    margin-left:15px!important;
    
}
	
</style>
<script type="text/javascript">
 
function onCountryClose()
		{
		if($('#CountryName')[0].innerHTML.toLowerCase().indexOf("international")>-1){
			document.location.href="http://www.cengage.com/country/";
		}
		if(location.host.toLowerCase().indexOf('.co.nz')>-1){
		if(!($('#CountryName')[0].innerHTML.toLowerCase().indexOf("new zealand")>-1)){
		if($('#CountryName')[0].innerHTML.toLowerCase().indexOf("international")>-1){
			document.location.href="http://www.cengage.com/country/";
		}
		else{
			document.location.href=location.protocol+"//"+"cengage.com.au"+document.location.href.split(document.location.hostname)[1];
			}		
		}
		}
		}


$(window).resize(function () {

var winToCenter = $(".k-window-content");

winToCenter.each(function(){

winToCenter.data("kendoWindow").center();

});

});


$(function(){
    // PM ECOLLECTION HOME SCREEN -- START      
        $("#OtherFeatureSliderPane .nivoSlider").trigger('mouseenter');        
        $(".ui-draggable").removeClass("dnnActionMenu").addClass("eColldnnActionMenu");     
        $(".bullet first").click();
    // PM ECOLLECTION HOME SCREEN --END
});
  
 

$(document).ready(function () {
var count=0;

 $("#msginfoclosediv").click(function () {

    $('#LoginMessage').hide();
          
  });
         <% if(Session["Visitor"] == null) {%>
         $('.k-window-actions.k-header').css('cursor', 'pointer');
         var window = $("#window1");
         if (!window.data("kendoWindow")) {
             window.kendoWindow({
                 width: "723px",
                 height: "450px",
                 modal: true,
                 content: GetFile("/CountryDetection.aspx"),
                 draggable: false,
				 close:onCountryClose,
				 visible:false
             });
             window.data("kendoWindow").center();
               $(".k-icon.k-i-close").css("display", "none");
              $('a.k-window-action.k-link').mouseover(function () {
$('a.k-window-action.k-link').parent().css("background-image", 
"url('./Portals/0/images/close.png') !important");
                  return false;
              });
         }
         window.data("kendoWindow").open();
         <%}%>

        if ($.browser.msie) {

            $('.advSrchbtn_Top').css('background-color', 'white');

        }
        $('.ddlcountry').click(function () {
        
	    <% if(Session["IsAuthenticated"] == null) {%>
            count=1;
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            var window = $("#window1");
            if (!window.data("kendoWindow")) {
                window.kendoWindow({
                    width: "723px",
                    height: "460px",
                    modal: true,
                    content: GetFile("/CountryDetection?page=changeloc"),
                    draggable: false,
				 close:onCountryClose,
				 visible:false
                });
                window.data("kendoWindow").center();
                $(".k-icon.k-i-close").css("display", "none");
                $('a.k-window-action.k-link').mouseover(function () {
                    $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                    return false;
                });

            }
            window.data("kendoWindow").open();
          <%} else { %>          
                var window = $("#CountryPopup");
                if (!window.data("kendoWindow")) 
                {
                    window.kendoWindow
                    ({
                        width: "441px",
                        height: "230px",
                        modal: true,
                        draggable: false,
						visible:false
                    });
                    window.data("kendoWindow").center();
                    
                }                
                $('a.k-window-action').css("margin-top", "-25px !important");
                window.data("kendoWindow").open();
              <%}%>

        });     
     $("#ddlschooltypetop").kendoDropDownList();
        $("#ddlschooltypefooter").kendoDropDownList();


        if (navigator.userAgent.match(/iPad/i) != null) {

            $('.bannerimg').css('width', '100%');
        }
        else {
            $('.bannerimg').css('width', '1600px');
        }


       		
	if(navigator.userAgent.indexOf("Mac") > -1) 
	{	

		$.browser.safari = ($.browser.webkit && !(/chrome/.test(navigator.userAgent.toLowerCase())));
		if ($.browser.safari) {

			
			$('.footer_bottom_left').css('margin-left','-6px');
         		$('.footer_bottom_right').css('margin-right','27px');

		}

		if (navigator.userAgent.indexOf('Chrome') != -1)
		{			
  	       	$('.footer_bottom_left').css('margin-left','-8px');
         	$('.footer_bottom_right').css('margin-right','30px');
		
		}


		if ($.browser.mozilla)
		{
			$('.footer_bottom_right').css('margin-right','28px');
		}



		
	}




        if ($('#Cart').text().length == 0) {
            $('#cartlnk').width('100px').addClass("util_rhtcartlnk");
            $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
        }
        else {
            $('#cartlnk').width('124px').addClass("util_rhtcartlnk");
            $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/mylist_grey.png")%>") no-repeat scroll');
        }
        if ($('#WishList').text().length == 0) {
            $('#wishlistlnk').width('99px').addClass("util_rhtwishlistlnk");
            $('#wishlistlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
        }
        else {
            $('#wishlistlnk').width('124px').addClass("util_rhtwishlistlnk");
            $('#wishlistlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/mylist_grey.png")%>") no-repeat scroll');
        }
        $("#advancedsrchTopdiv").click(function () {
               $("#advancedsrchdialog").css("display", "block");             
               $('.tabs').css("z-index", "0");  
				$('#advancedsrchpop').attr('src',GetFile("/advancedsearch.aspx"));
                if(!$('#imgdiv').hasClass("backgrdimageopen"))
                {
                     
                   $('#imgdiv').addClass("backgrdimageopen");
                   $('#imgdiv').removeClass("backgrdimage");
                   $('#advancedsrchTopdiv').removeClass("advSrchbtn_Top");
                   $('#advancedsrchTopdiv').addClass("advSrchbtn_TopSel"); 
                   $('#advancedsrchpop').attr('src',GetFile("/advancedsearch.aspx"));
                }
                else{
                   $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                   $('#imgdiv').addClass("backgrdimage");
                   $('#imgdiv').removeClass("backgrdimageopen");
                   $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
                   $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
                }
              GAPushTrackEvent('Site-Search','Click','Advance-Search-Button');
        }); 
         $("body").click(function (obj) {          
              if(obj!=undefined && obj.target != undefined && $(obj.target.parentElement).attr("id") != "advancedsrchTopdiv")
              {
                       $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                       $('#imgdiv').addClass("backgrdimage");
                       $('#imgdiv').removeClass("backgrdimageopen");
                       $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
                       $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
               }
            });

        var _totwidth = $("#Mainmenu").width();
        var _licount = $("#Mainmenu ul").children().length;



        var _splitwidth = (_totwidth / _licount);
        var _marginwidth;
        var _marginwidthtbs;
        if (_licount <= 4) {
            _marginwidth = _licount * 15;
            _marginwidthtbs = _marginwidth + 2;
        }
        else {
            _marginwidth = _licount * 4;
            _marginwidthtbs = _marginwidth + 8;
        }

		if($("#Mainmenu")!=undefined)
		{
        $("#Mainmenuul").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenuul").css("-moz-transition", "all 0.2s ease-in-out");
        $("#Mainmenu li").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenu li").css("-moz-transition", "all 0.2s ease-in-out");
        $("#Mainmenu li a").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenu li a").css("-moz-transition", "all 0.2s ease-in-out");
        $(".menuitem").css("transition", "all 0.2s ease-in-out");
        $(".menuitem").css("-moz-transition", "all 0.2s ease-in-out");
		}
        
        var href = jQuery(location).attr('href');
        var currentpagename = href.substring(href.lastIndexOf('/') + 1);

        if (currentpagename.toUpperCase() == "CENGAGEDASHBOARD.ASPX") {
            $('#loginlnk').addClass('util_rhtlnkactive');
            $('#userimg').addClass('useractive');
        }
        else if (currentpagename.toUpperCase() == "SIGNUP.ASPX") {
            $('#SignUp').addClass('util_rhtlnkactive');
        }
        else if (currentpagename.toUpperCase() == "HELP.ASPX") {
            $('[id$=lblbannertxt]').text("HELP");
        }             
        

         if (currentpagename.toUpperCase().indexOf("LIST") >= 0)
        {
          $('#wishlistlnk').addClass('util_rhtwishlistactive');
            $('#wishlistimg').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/star_blue.png")%>") no-repeat scroll 0 0 transparent');

            if ($('#WishList').text().length == 0) {
                $('#wishlistlnk').width('99px!important').addClass("util_rhtwishlistactive");  
                $('#wishlistlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_blue.png")%>") no-repeat scroll 0 0 transparent');
               
            }
            else {
                $('#wishlistlnk').width('124px!important').addClass("util_rhtwishlistactive");
                $('#wishlistlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/mylist_blue.png")%>") no-repeat scroll 0 0 transparent');
                
            }

        }
        
        if(currentpagename.toUpperCase().indexOf("CART") >= 0) 
        {

            if (currentpagename.toUpperCase().indexOf("CART") >= 0) {
                $('#cartlnk').addClass('util_rhtcartlnkactive');
                $('#cartimg').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_blue.png")%>") no-repeat scroll 0 0 transparent');

                if ($('#Cart').text().length == 0) {

                    $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_blue.png")%>") no-repeat scroll 0 0 transparent');
                    $('#cartlnk').width('100px').addClass("util_rhtcartlnkactive");

                }
                else {
                    $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/mylist_blue.png")%>") no-repeat scroll 0 0 transparent');
                    $('#cartlnk').width('124px').addClass("util_rhtcartlnkactive");

                }

            }
          
        }

           var dropdownlist = $("#ddlschooltypetop").data("kendoDropDownList");
          var dropdownlistbottom = $("#ddlschooltypefooter").data("kendoDropDownList");

        
    });
    function GetFile(path) {
        var pathname = window.location.pathname;
        var temppath = pathname.split('/');
        var root = location.protocol+"//" + window.location.host + "/" + temppath[0];
        var url = root + path;
        return url;
    }
    function OpenWindow() {
var window = $("#window");
          window.kendoWindow({
            width: "498px",
            height: "920px",
            content: GetFile("/signup.aspx"),
            modal: true,
            draggable: true,
            resizable: true,
            visible: false
        });
        window.data("kendoWindow").center().open();
            $(".k-icon.k-i-close").css("display", "none");
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                return false;
            });
    }
    function OpenLoginWindow(elementId) {


        var href = jQuery(location).attr('href');
        var currentpagename = href.substring(href.lastIndexOf('/') + 1);

        var window = $("#loginWindow");

        window.kendoWindow({
            width: "491px",
            height: "317px",
            content: GetFile("/signin.aspx"),
            modal: true,
            draggable: true,
            resizable: true,
			visible:false
        });
        window.data("kendoWindow").center();
        $('#loginWindow').closest(".k-window").css({
            position: 'fixed',
            margin: 'auto'

        });
        window.data("kendoWindow").open();
        if (currentpagename.toUpperCase() == "CHECKOUT.ASPX") {
            $("#loginWindow").parent().css('width', '485px');

        }
        else {
            $("#loginWindow").parent().css('width', '491px');
        }
        if ($.browser.msie) {

            $("#loginWindow").css({ 'height': '363px', 'margin-left': '-16px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');

        }
        else if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
            $("#loginWindow").css({ 'height': '362px', 'margin-left': '-16px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');
        }
        else {

            $("#loginWindow").css({ 'height': '362px', 'margin-left': '-16px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');
        }


        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css({ 'background-image': 'url("./Portals/0/images/close.png") !important' });
            return false;
        });

    }
</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />
<div id="window">
</div>
<div id="window1">
</div>
<div id="loginWindow">
</div>
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" Visible="false">
    <div class="msgicon">
        <img src="portals/0/images/alert_icon.png" alt="alerticon" /></div>
    <div class="msginfotxt H7">
        YOU ARE USING THIS CENGAGE SITE AS <span id="LoginUserName"  runat="server" clientidmode="Static"></span>
    </div>
    <div id="msginfoclosediv" class="msginfoclosediv H7">
        (CLOSE WINDOW)</div>
</div>
<dnn:UTILITIES runat="server" ID="Utilities1" ClientIDMode="Static" />
<div id="logo">
    <div class="logoouter">
        <dnn:LOGO ID="dnnLogo" runat="server" />
    </div>
</div>
<div id="masterhead">
  

    <cengage:CENGAGESUBMENU ID="CENGAGESUBMENU" runat="server" />   
    
   
    <div id="SrchTop" class="SrchTop">
        <div class="SrchComp_Top">
            <cengage:CENGAGESEARCH ID="SearchComponent" runat="server" />     
        </div>
        <div id="advancedsrchTopdiv" class="H5 advSrchbtn_Top">
            <div id="imgdiv" class="backgrdimage">
            </div>
            <span style="position: relative; padding-left: 8px;padding-right: 14px;float: left;padding-top: 12px;padding-bottom: 12px;">ADVANCED SEARCH</span>
        </div>
        <div id="advancedsrchdialog">
            <iframe id="advancedsrchpop" class="ifrmsrch">
            </iframe>
        </div>
    </div>
    <div class="bannersec">
        
        <div id="bannertitle" class="bannertitle algnCentr">
             <h1 id="SecondaryPageHeader" runat="server" clientidmode="Static">
		<%#PortalSettings.ActiveTab.Title.Split('|')[0].ToString() %>
                <asp:Label Visible="false" ID="lblbannertxt" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            </h1>
        </div>
    </div>
</div>
<div class="middlecont" >
    <div class="contCentr">
        <div class="Panes">
            <div id="ContentPane" runat="server" style="clear: both; display: inline-block; z-index: 5;">
            </div>
			<div id="eCollectionSliderPane" clientidmode="Static" runat="server" style="width: 100%;
                margin-top: 15px; height: 100%; clear: both; display: inline-block; z-index: 5;
                margin-left: 15px;">
            </div>
            <div id="eCollectionDescriptionPane" runat="server" style="width: 100%; height: 100%;
                clear: both; margin-top: 54px; display: inline-block; z-index: 5;">
            </div>
            <div id="eCollectionPillarPane" clientidmode="Static" runat="server" style="width: 100%;
                height: 100%; clear: both; margin-top: 25px; display: inline-block; z-index: 5;">
            </div>
            <div id="PMeBookPane" runat="server" style="width: 100%; height: 100%; clear: both;
                display: inline-block; z-index: 5;">
            </div>
            <div id="EReaderPane" runat="server" style="width: 100%; height: 100%; clear: both;
                margin-top: 23px; display: inline-block; z-index: 5;">
            </div>
            <div id="eCollectionWebPane" runat="server" style="width: 100%; height: 100%; clear: both;
                display: inline-block; z-index: 5;">
            </div>
            <div id="OtherFeatureContentPane" runat="server" style="width: 40%; height: 100%;
                float: left; clear: both; display: inline-block; z-index: 5;">
            </div>
            <div id="OtherFeatureSliderPane" clientidmode="Static" class="sliderFrame" runat="server"
                style="width: 50%; height: 100%; padding: 41px; clear: both; display: inline-block;
                z-index: 5; padding-top: 43px; padding-left: 1px; padding-bottom: 75px; margin-top: 38px;
                margin-left: 25px;">
            </div>
            <div id="UsersPane" runat="server" style="width: 100%; height: 100%; clear: both;
                display: inline-block; z-index: 5;">
            </div>
        </div>
    </div>
</div>
<div id="Srchbottom" class="SrchBottom">
    <div class="SrchComp_Bottom">
       <cengage:FOOTERCENGAGESEARCH ID="FOOTERCENGAGESEARCH" runat="server" />   
    </div>
    <div class="Srchbtmline">
    </div>
</div>
<div id="footer_top">
</div>

<div id="footer">
    <div>
        <%# Session["footer_megatop"]%>
    </div>
    <div class="footer_megabottom">
        <div class="algnCentrfooter">
            <div class="footer_bottom_left">
                <a href="/terms-and-conditions"
                    class="anc_class H5Light">Terms and Conditions</a>&nbsp;|&nbsp;<a href='/privacy'
                        class="anc_class H5Light"> Privacy</a>&nbsp;|&nbsp; <a href='/copyright' class="anc_class H5Light">
                            Copyright</a>&nbsp;|&nbsp;<a href='/disclaimer' class="anc_class H5Light"> Disclaimer</a>
            </div>
            <div class="footer_bottom_right">
                <label class="anc_class H5Light">
                    &#169;2013. Cengage Learning Australia pty Limited</label>&nbsp;|&nbsp;
                <label class="anc_class H5Light">
                    ABN: 14 058 280 149 / IRD 6757486748598</label>
            </div>
        </div>
    </div>
</div>

<asp:HiddenField ID="Hiddenpageresult" ClientIDMode="Static" runat="server" Value="Beforeclick" />
<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" ClientIDMode="Static" Style="display: none;" />
</div>
