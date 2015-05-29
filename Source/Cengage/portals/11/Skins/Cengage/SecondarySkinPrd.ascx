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
<link href="<%#Page.ResolveUrl("Portals/0/Skins/Cengage/ie8skin.css")%>" rel="stylesheet"
    type="text/css" />
<dnn:STYLES runat="server" ID="StylesIE8" Name="IE8Minus" StyleSheet="ie8skin.css"
    Condition="LT IE 10" UseSkinPath="true" />
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.web.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.dropdownlist.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.window.min.js")%>"
    type="text/javascript"></script>
<link href="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.common.min.css")%>"
    rel="stylesheet" type="text/css" />
<link href="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.default.min.css")%>"
    rel="stylesheet" type="text/css" />
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.bxSlider.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.idTabs.min.js")%>"
    type="text/javascript"></script>
<style type="text/css">
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
	width:185px !important;
	margin-left:20px !important;
	}
	
</style>
<script type="text/javascript">
 //Google Analytics Dont edit this
var domainName;
if(window.location.host.indexOf("cengagelearning.com.au")>-1)
{ domainName='cengage.com.au'; }
else if(window.location.host.indexOf("cengagelearning.co.nz")>-1)
{ domainName='cengage.co.nz'; }
var _gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-43278709-1']);
_gaq.push(['_setDomainName', domainName]);
_gaq.push(['_setAllowLinker', true]);
_gaq.push(['_trackPageview']);
(function() {
var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
//End Google Analytics Dont edit this

$(window).resize(function () {

var winToCenter = $(".k-window-content");

winToCenter.each(function(){

winToCenter.data("kendoWindow").center();

});

});


 
 
 

    $(document).ready(function () {
var count=0;
//$('#window1').parent().parent().click(function () {
//count++;
//         if ($(document.activeElement).closest(".k-window").length == 0) {
//		 if(count==3){
//             $("#window1").data("kendoWindow").close();
//			 }
//         }
//     });

 $("#msginfoclosediv").click(function () {

    $('#LoginMessage').hide();
          
  });
 //SignUp
         <% if(Session["Visitor"] == null) {%>
         $('.k-window-actions.k-header').css('cursor', 'pointer');
         var window = $("#window1");
         if (!window.data("kendoWindow")) {
             window.kendoWindow({
                 width: "723px",
                 height: "450px",
                 modal: true,
                 content: GetFile("/CountryDetection.aspx"),
                 draggable: false
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
                    content: GetFile("/CountryDetection.aspx?page=changeloc"),
                    draggable: false
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
                        draggable: false
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
			$('#advancedsrchpop').attr('src','/AdvancedSearch.aspx');
            if ($('#imgadvSrc').attr('src').indexOf('av_search_sel') == -1) {
                $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('av_search', 'av_search_sel'));
                $('#advancedsrchTopdiv').removeClass("advSrchbtn_Top");
                $('#advancedsrchTopdiv').addClass("advSrchbtn_TopSel");
            }
            else {
                $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('av_search_sel', 'av_search'));
                $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
                $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
            }

        });

         $("body").click(function (obj) {          
              if(obj!=undefined && obj.target != undefined && $(obj.target.parentElement).attr("id") != "advancedsrchTopdiv")
              {
                       $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                       $('#imgadvSrc').attr('src',$('#imgadvSrc').attr('src').replace('av_search_sel','av_search'));
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



        $("#Mainmenuul").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenuul").css("-moz-transition", "all 0.2s ease-in-out");
        $("#Mainmenu li").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenu li").css("-moz-transition", "all 0.2s ease-in-out");
        $("#Mainmenu li a").css("transition", "all 0.2s ease-in-out");
        $("#Mainmenu li a").css("-moz-transition", "all 0.2s ease-in-out");
        $(".menuitem").css("transition", "all 0.2s ease-in-out");
        $(".menuitem").css("-moz-transition", "all 0.2s ease-in-out");

        var _liwidth = parseFloat(_splitwidth.toString()).toFixed(2);
        var _liwidthref = parseFloat(_splitwidth.toString()).toFixed(2) - 2;
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
        

         if (currentpagename.toUpperCase() == "LIST.ASPX")
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
        
        if(currentpagename.toUpperCase().indexOf("LIST.ASPX") >= 0) 
        {

            if (currentpagename.toUpperCase() == "LIST.ASPX?ITEM=CART") {
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
            else 
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
        }

           var dropdownlist = $("#ddlschooltypetop").data("kendoDropDownList");
          var dropdownlistbottom = $("#ddlschooltypefooter").data("kendoDropDownList");

         //   dropdownlist.select(function(dataItem) 
         //   {
         //       return dataItem.text === "ALL";
         //   });
         //   dropdownlistbottom.select(function(dataItem) 
         //   {
         //       return dataItem.text === "ALL";
         //   });
        
        <% if(Session["IsAuthenticated"] == null) {%>

         $('#Mainmenu').css("display", "block");
         $('#DashBoardMenu').css("display", "none");

       
        
        if (currentpagename.toUpperCase() == "PRIMARY.ASPX") {
            $('#Primary').addClass('primaryactive');
            dropdownlist.select(function(dataItem) {
                return dataItem.text === "PRIMARY";
            });
            dropdownlistbottom.select(function(dataItem) {
                return dataItem.text === "PRIMARY";
            });
        }
        if (currentpagename.toUpperCase() == "SECONDARY.ASPX") {
            $('#Secondary').addClass('secondaryactive');
               dropdownlist.select(function(dataItem) {
                return dataItem.text === "SECONDARY";
            });
            dropdownlistbottom.select(function(dataItem) {
                return dataItem.text === "SECONDARY";
            });
        }
        if (currentpagename.toUpperCase() == "STAFF-ROOM.ASPX") {
            $('#StaffRoom').addClass('staffroomactive');
             $('#Primary').removeClass('primaryactive');  
        }
        if (currentpagename.toUpperCase() == "ABOUT-US.ASPX") {
            $('#AboutUs').addClass('aboutusactive');
             $('#Primary').removeClass('primaryactive');  
        }
         <%} else { %>         
         $('#Mainmenu').css("display", "none");
        // $('#DashBoardMenu').css("display", "block");
        if (currentpagename.toUpperCase() == "DASHBOARD.ASPX" || currentpagename.toUpperCase().indexOf("DASHBOARD.ASPX") >= 0) 
        {            
             $('#dashboard').addClass('DbDashboardActive');
			 $('#dashbaordprimarylnk').removeClass('DbPrimaryActive');
			 $('#dashbaordsecondarylnk').removeClass('DbSecondaryActive'); 	
        }
        if (currentpagename.toUpperCase() == "PRIMARY.ASPX" || currentpagename.toUpperCase().indexOf("PRIMARY.ASPX") >= 0) 
        {
            $('#dashbaordprimarylnk').addClass('DbPrimaryActive'); 

               dropdownlist.select(function(dataItem) {
                return dataItem.text === "PRIMARY";
            });
              dropdownlistbottom.select(function(dataItem) {
                return dataItem.text === "PRIMARY";
            });

        }
        else if (currentpagename.toUpperCase() == "SECONDARY.ASPX"  || currentpagename.toUpperCase().indexOf("SECONDARY.ASPX") >= 0)  
        {  
           
            $('#dashbaordsecondarylnk').addClass('DbSecondaryActive');    

         
            dropdownlist.select(function(dataItem) {
                return dataItem.text === "SECONDARY";
            });
            dropdownlistbottom.select(function(dataItem) {
                return dataItem.text === "SECONDARY";
            });

        }
        else if (currentpagename.toUpperCase() == "STAFF-ROOM.ASPX"  || currentpagename.toUpperCase().indexOf("STAFF-ROOM.ASPX") >= 0)  
        {
              $('#dashbaordstaffroomlnk').addClass('DbStaffRoomActive');
              $('#dashbaordprimarylnk').removeClass('DbPrimaryActive'); 
        }
        else if (currentpagename.toUpperCase() == "ABOUT-US.ASPX" || currentpagename.toUpperCase().indexOf("ABOUT-US.ASPX") >= 0) 
        {
             $('#dashbaordaboutuslnk').addClass('DbAboutUsActive');
             $('#dashbaordprimarylnk').removeClass('DbPrimaryActive'); 
        }
         <% } %>

    });
    function GetFile(path) {
        var pathname = window.location.pathname;
        var temppath = pathname.split('/');
        var root = "http://" + window.location.host + "/" + temppath[0];
        var url = root + path;
        return url;
    }
    function OpenWindow() {


        var window = $("#window"),
                signup = $("#SignUp").bind("click", function () { window.data("kendoWindow").open(); });
        $("#SignUp").click(function (e) {
            window.data("kendoWindow").open();
        });
        onClose = function () {
            signup.show();
        }
        if (!window.data("kendoWindow")) {
            window.kendoWindow({
                width: "498px",
                height: "874px",
                content: GetFile("/signup.aspx"),
                close: onClose,
                modal: true,
                draggable: false
            });
            window.data("kendoWindow").center();
            $(".k-icon.k-i-close").css("display", "none");
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                return false;
            });

        }

    }
    function OpenLoginWindow(elementId) {


        var href = jQuery(location).attr('href');
        var currentpagename = href.substring(href.lastIndexOf('/') + 1);

        var window = $("#loginWindow");

        window.kendoWindow({
            width: "491px",
            height: "317px",
            content: GetFile("/cengageecommerce.aspx"),
            modal: true,
            draggable: true,
            resizable: true
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
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" style="display:none;">
    <div class="msgicon">
        <%--<img src="portals/0/images/alert_icon.png" alt="alerticon" />--%>
        </div>
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
   <%--<div id="Mainmenu" runat="server" clientidmode="Static" class="algnCentr" style="display: none;">
        <ul id="Mainmenuul">
            <li id="menuitem_primary"><a id="primarylnk" href="primary.aspx"><span class="tile_icon Primary"
                clientidmode="Static" id="Primary"></span></a></li>
            <li id="menuitem_secondary"><a id="secondarylnk" href="secondary.aspx"><span class="tile_icon Secondary"
                clientidmode="Static" id="Secondary"></span></a></li>
            <li id="menuitem_staffroom"><a id="staffroomlnk" href="staff-room.aspx"><span class="tile_icon StaffRoom"
                clientidmode="Static" id="StaffRoom"></span></a></li>
            <li id="menuitem_aboutus"><a id="aboutuslnk" href="about-us.aspx" class="menuitem"><span
                clientidmode="Static" class="tile_icon AboutUs" id="AboutUs"></span></a></li>
        </ul>
    </div>--%>

    <cengage:CENGAGESUBMENU ID="CENGAGESUBMENU" runat="server" />   
    
    <%--<div id="DashBoardMenu" runat="server" clientidmode="Static" class="algnCentr" style="display: none;">
        <ul id="Ul1">
            <li id="menuitem_user_dash"><a  href="dashboard.aspx"><span class="dashboardtile_icon DbDashboard" id="dashbaordlnk"
                clientidmode="Static"></span></a></li>
            <li id="Li1"><a href="primary.aspx"><span class="dashboardtile_icon DbPrimary" id="dashbaordprimarylnk" 
                clientidmode="Static"></span></a></li>
            <li id="Li2"><a  href="secondary.aspx"><span class="dashboardtile_icon DbSecondary" id="dashbaordsecondarylnk"
                clientidmode="Static"></span></a></li>
            <li id="Li3"><a href="staff-room.aspx"><span class="dashboardtile_icon DbStaffRoom"  id="dashbaordstaffroomlnk"
                clientidmode="Static" id="Span3"></span></a></li>
            <li id="Li4"><a href="about-us.aspx" class="menuitem"><span
                clientidmode="Static" class="dashboardtile_icon DbAboutUs"  id="dashbaordaboutuslnk"></span></a>
            </li>
        </ul>
    </div>--%>
    <div id="SrchTop" class="SrchTop">
        <div class="SrchComp_Top">
            <cengage:CENGAGESEARCH ID="SearchComponent" runat="server" />     
        </div>
        <div id="advancedsrchTopdiv" class="advSrchbtn_Top H5">
            <div class="adv_icon_top">
                <img id="imgadvSrc" alt="advsrchicon" src="<%#Page.ResolveUrl("Portals/0/Images/av_search.png")%>" /></div>
            <span class="adv_txt">Advanced Search</span>
        </div>
        <div id="advancedsrchdialog">
            <iframe id="advancedsrchpop" class="ifrmsrch" frameborder="0">
            </iframe>
        </div>
    </div>
    <div class="bannersec">
        
        <div id="bannertitle" class="bannertitle algnCentr">
            <h1>
			<%#PortalSettings.ActiveTab.Title %>
                <asp:Label style="display:none;" ID="lblbannertxt" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            </h1>
        </div>
    </div>
</div>
<div class="middlecont" >
    <div class="contCentr">
        <div class="Panes">
            <div id="ContentPane" runat="server" style="clear: both; display: inline-block; z-index: 5;">
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
    <div id="footer_megatop">
        <div class="contCentr">
            <div class="splInfo1">
                <div>
                    <img alt="ceng" src="<%#Page.ResolveUrl("portals/0/images/ceng.png")%>" class="nelicon" />
                    <h5 class="headingtext">
                        NELSON CENGAGE</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="http://www.cengage.com.au/" class="anc_class_footer cenlist H6Light"
                            target="_blank">Cengage corporate Home</a></li>
                        <li><a href="http://www.nelsonprimary.com.au/" class="anc_class_footer H6Light" target="_blank">
                            Primary</a></li>
                        <li><a href="http://www.nelsonsecondary.com/" class="anc_class_footer H6Light" target="_blank">
                            Secondary</a></li>
                        <li><a href="http://www.higher.cengage.com.au/" class="anc_class_footer H6Light"
                            target="_blank">Higher Education</a></li>
                        <li><a href="http://www.vpg.cengage.com.au" class="anc_class_footer H6Light" target="_blank">
                            Vocational & Professional</a></li>
                        <li><a href="http://www.vpg.cengage.com.au/1/250/15/elt.pm" class="anc_class_footer H6Light"
                            target="_blank">English Professional Teaching</a></li>
                        <li><a href="http://www.gale.cengage.com.au/" class="anc_class_footer H6Light" target="_blank">
                            <span class="galespan">Gale</span> Library Reference</a></li></ul>
                </div>
            </div>
            <div class="splInfo2">
                <div class="H5">
                    <img src="<%#Page.ResolveUrl("portals/0/images/man_icon.png")%>" alt="manicon" class="manicon" />
                    <h5 class="headingtext">
                        CENGAGE PEOPLE</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Booksellers</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Schools</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Teachers</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Authors</a></li></ul>
                </div>
            </div>
            <div class="splInfo3">
                <div class="H5">
                    <img src="<%#Page.ResolveUrl("portals/0/images/help_icon.png")%>" alt="helpicon"
                        class="helpicon" />
                    <h5 class="headingtext">
                        HELP & INFORMATION</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="http://www.cengage.com.au/2/75/1/using_this_site.pm" class="anc_class_footer H6Light"
                            target="_blank">Help</a></li>
                        <li><a href="http://www.cengage.com.au/1/2/1/about_us.pm" class="anc_class_footer H6Light"
                            target="_blank">About Us</a></li>
                        <li><a href="http://www.cengage.com.au/1/207/1/permissions.pm" class="anc_class_footer H6Light"
                            target="_blank">Permissions</a></li>
                        <li><a href="http://www.cengage.com.au/1/206/1/accessiblity_requests.pm" class="anc_class_footer H6Light"
                            target="_blank">Accessibility</a></li>
                        <li><a href="http://www.cengage.com.au/1/64/1/careers.pm" class="anc_class_footer H6Light"
                            target="_blank">Careers</a></li>
                        <li><a href="http://www.nelsonprimary.com.au/1/279/14/nelson_primary_imprints.pm"
                            class="anc_class_footer H6Light" target="_blank">Imprints</a></li></ul>
                </div>
            </div>
            <div class="splInfo_contact">
                <div>
                    <img src="<%#Page.ResolveUrl("portals/0/images/shopping_icon.png")%>" alt="shopicon"
                        class="shopicon" />
                    <h5 class="headingtextshopping">
                        SHOPPING</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="https://ecommerce.cengage.com.au/cl/" class="anc_class_footer onlinelist H6Light"
                            target="_blank">Online Store</a></li>
                        <li><a href="http://www.cengage.com.au/2/77/1/terms_and_conditions/DY" class="anc_class_footer H6Light"
                            target="_blank">Shipping & Delivery</a></li></ul>
                </div>
                <div class="footer_contactdiv">
                    <img src="<%#Page.ResolveUrl("portals/0/images/contact_icon.png")%>" alt="conticon"
                        class="conticon" /><h5 class="headingtextcontact">
                            CONTACT US</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="http://www.nelsonprimary.com/au/1/40/14/contact_us.pm" class="anc_class_footer_contval H6Light"
                            target="_blank">Contact Us</a></li></ul>
                </div>
            </div>
            <div class="footerlist2">
                <a href="#top">
                    <img id="footerlist2img" class="footerlist2img" src="<%#Page.ResolveUrl("portals/0/images/backtotop.PNg")%>"
                        alt="backtop" /></a>
            </div>
        </div>
    </div>
    <div class="footer_megabottom">
        <div class="algnCentrfooter">
            <div class="footer_bottom_left">
                <a href="help.aspx" class="anc_class H5Light">Sitemap</a>&nbsp;|&nbsp; <a href="help.aspx"
                    class="anc_class H5Light">Terms and Conditions</a>&nbsp;|&nbsp;<a href="help.aspx"
                        class="anc_class H5Light"> Privacy</a>&nbsp;|&nbsp; <a href="help.aspx" class="anc_class H5Light">
                            Copyright</a>&nbsp;|&nbsp;<a href="help.aspx" class="anc_class H5Light"> Disclaimer</a>
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
<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" ClientIDMode="Static" Style="display: none;" />
</div>
