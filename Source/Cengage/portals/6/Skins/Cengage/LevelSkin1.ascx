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
    Condition="LT IE 9" UseSkinPath="true" />
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.web.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.dropdownlist.min.js")%>"
    type="text/javascript"></script>
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.tabstrip.min.js")%>"
    type="text/javascript"></script>
<link href="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.common.min.css")%>"
    rel="stylesheet" type="text/css" />
<link href="<%#Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.default.min.css")%>"
    rel="stylesheet" type="text/css" />
<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.bxSlider.min.js")%>"
    type="text/javascript"></script>
<style type="text/css" rel="stylesheet">
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
        background-image: url('./Portals/0/images/close.png');
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -19px !important;
        margin-top: 15px;
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
        margin-top: 15px;
        border: none !important;
        margin-left: -10px;
        background-color: transparent !important; /* background:transparent !important;*/
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
    ul li
    {
        list-style: none;
    }
  #megaStore .k-widget.k-menu-horizontal > .k-last
    {
        border: 0;
    }
   #dnn_dnnBreadcrumb_lblBreadCrumb
    {
        font-weight:400;color:#B7AAAA;
        letter-spacing:1px !important;margin-top:25px;float:left;
    }
    #dnn_dnnBreadcrumb_lblBreadCrumb a
    {
    color:#B7AAAA;text-decoration:none;
    }
.advSrchbtn_Top
{
    width:185px !important;
    margin-left:15px!important;
    
}
</style>
<script type="text/javascript">
 

$(window).resize(function () {

var winToCenter = $(".k-window-content");

winToCenter.each(function(){

winToCenter.data("kendoWindow").center();

});

});

function isiPhone(){
    return (
        //Detect iPhone
        (navigator.platform.indexOf("iPhone") != -1) ||
        //Detect iPod
        (navigator.platform.indexOf("iPod") != -1)
    );
}


function getParameterByName(val) {
var name=window.location.href;
     name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
     var regex = new RegExp("[\\?&]" + val + "=([^&#]*)"),
         results = regex.exec(location.search);
     return results == null ? "" : 
decodeURIComponent(results[1].replace(/\+/g, " "));
}

    function OpenWindowFromList() {
        var window = $("#window"),
            signup = $("#signuplnk").bind("click", function () { window.data("kendoWindow").open(); });
        $("#signuplnk").click(function (e) {
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
            $("#ListPopuocreateuserbutton").data("kendoWindow").close();
        }
    }
    function OpenLoginWindowFromList() {
        var window = $("#loginWindow");
        window.kendoWindow({
            width: "491px",
            height: "317px",
            content: GetFile("/signin.aspx"),
            modal: true,
            draggable: true,
            resizable: true
        });
        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
        if ($.browser.msie) {
            $("#loginWindow").css({ 'height': '362px', 'margin-left': '-9px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');
        }
        else if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
            $("#loginWindow").css({ 'height': '361px', 'margin-left': '-9px', 'width': '507px' });
            $("#loginWindow").parent().css('height', '317px');
        }
        else {
            $("#loginWindow").css({ 'height': '362px', 'margin-left': '-9px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');
        }
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false;
        });
        $("#ListPopuocreateuserbutton").data("kendoWindow").close();
    }
    
 $(document).ready(function () 
 {

 var count=0;
 var count1=0;


 
  $("#msginfoclosediv").click(function () {

    $('#LoginMessage').hide();
          
  });
          $('#wishlistlnk').click(function () 
          {
              <% if(Session["IsAuthenticated"] == null) {%>                            
                 $("#ListPopuocreateuserbutton").css("display","block");
                  var window3 = $("#ListPopuocreateuserbutton");        
                  if (!window3.data("kendoWindow")) 
                  {
                            window3.kendoWindow({
                                width: "600px",
                                height: "270px",
                                modal: true,
                                draggable: false
                            });                                                  
                  }                     
                  window3.data("kendoWindow").open(); 
                  window3.data("kendoWindow").center();   
              <% } else { %>           
                location.href= "List.aspx";
              <% } %>
            });
     <% if(Request.QueryString["inviteeid"]!=null){ %>
	 count1++;
             var subsid=getParameterByName("subsid");
             var inviteeid=getParameterByName("inviteeid");
             $('.k-window-actions.k-header').css('cursor', 'pointer');
             var window = $("#window1");
             if (!window.data("kendoWindow")) {
                 window.kendoWindow({
                      width: "498px",
                      height: "874px",
                     modal: true,
                     content: 
    GetFile("/signup.aspx?inviteeid="+inviteeid+"&subsid="+subsid),
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
		        $("#window1").remove();
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                var window2 = $("#window2");
                if (!window2.data("kendoWindow")) {
                    window2.kendoWindow({
                        width: "723px",
                        height: "440px",
                        modal: true,
                        content: GetFile("/CountryDetection.aspx?page=changeloc"),
                        draggable: false
                    });
                    window2.data("kendoWindow").center();
                    $(".k-icon.k-i-close").css("display", "none");
                    $('a.k-window-action.k-link').mouseover(function () {
                        $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                        return false;
                    });

                }
                window2.data("kendoWindow").open();
               <%} else { %>                
                var window = $("#CountryPopup");
                if (!window.data("kendoWindow")) 
                {
                    window.kendoWindow
                    ({
                        width: "441px",
                        modal: true,
                        draggable: false
                    });
                    window.data("kendoWindow").center();                    
                     $('.k-window-action').css("margin-top", "-15px!important");
                }
                window.data("kendoWindow").open();
              <%}%>
        });  
       $("#ddlschooltypetop").kendoDropDownList();
        $("#ddlschooltypefooter").kendoDropDownList();

        if(navigator.userAgent.match(/iPad/i) != null)
        {
           
             $('.bannerimg').css('width', '100%');
        }
        else 
        {
            $('.bannerimg').css('width', '1600px');
        }

      
			
	if(navigator.userAgent.indexOf("Mac") > -1) 
	{	

		$.browser.safari = ($.browser.webkit && !(/chrome/.test(navigator.userAgent.toLowerCase())));
		if ($.browser.safari) 
        {			
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
    if ($('#Cart').text().length == 0) 
    {
            $('#cartlnk').width('100px').addClass("util_rhtcartlnk"); 
                        $('#cartlnk').css('background', 'url("<%#Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
    }
    else 
    {
            $('#cartlnk').width('124px').addClass("util_rhtcartlnk"); 
            
            $('#cartlnk').css('background', 'url("<%#Page.ResolveUrl("portals/0/images/mylist_grey.png")%>") no-repeat scroll');
    }
    if ($('#WishList').text().length == 0) 
    {
            $('#wishlistlnk').width('99px').addClass("util_rhtwishlistlnk");
            $('#wishlistlnk').css('background', 'url("<%#Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
           
    }
    else 
    {
            $('#wishlistlnk').width('124px').addClass("util_rhtwishlistlnk");      
           
            $('#wishlistlnk').css('background', 'url("<%#Page.ResolveUrl("portals/0/images/mylist_grey.png")%>") no-repeat scroll');
    }
    $("#tabstrip").kendoTabStrip({
            animation: {            
                open: {
                    effects: "fadeIn"
                }
            },
            select: function (e) {
                if ($.browser.msie) {

                    if ($(e.item).index() == "0") {

                        $("#ie_literacydiv").addClass("tabshadow");
                        $("#ie_numaracydiv").addClass("tabshadow");
                        $("#ie_workbooksdiv").addClass("tabshadow");
                        $("#ie_digitaldiv").addClass("tabshadow");
                    }
                    else if ($(e.item).index() == "1") {
                        $("#ie_literacydiv").removeClass("tabshadow");
                        $("#ie_numaracydiv").addClass("tabshadow");
                        $("#ie_workbooksdiv").addClass("tabshadow");
                        $("#ie_digitaldiv").addClass("tabshadow");
                    }
                    else if ($(e.item).index() == "2") {


                        $("#ie_literacydiv").addClass("tabshadow");
                        $("#ie_numaracydiv").removeClass("tabshadow");
                        $("#ie_workbooksdiv").addClass("tabshadow");
                        $("#ie_digitaldiv").addClass("tabshadow");
                    }
                    else if ($(e.item).index() == "3") {

                        $("#ie_literacydiv").addClass("tabshadow");
                        $("#ie_numaracydiv").addClass("tabshadow");
                        $("#ie_workbooksdiv").removeClass("tabshadow");
                        $("#ie_digitaldiv").addClass("tabshadow");
                    }
                    else if ($(e.item).index() == "4") {

                        $("#ie_literacydiv").addClass("tabshadow");
                        $("#ie_numaracydiv").addClass("tabshadow");
                        $("#ie_workbooksdiv").addClass("tabshadow");
                        $("#ie_digitaldiv").removeClass("tabshadow");
                    }
                }
            }

        });
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

        var _liwidth = parseFloat(_splitwidth.toString()).toFixed(2);
        var _liwidthref = parseFloat(_splitwidth.toString()).toFixed(2) - 2;    


        if (navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1) {
            $('#tabstrip li').css('top', "-12px");
            $('#tabstrip .k-tabstrip-items .k-first').css('top', "-13px!important");
        }
        else {
            $('#tabstrip li').css('top', "-14px");
        }

        $('.icons_staff').css('margin-left', _marginwidthtbs);
        $('.icons_abt').css('margin-left', _marginwidthtbs);


        var href = jQuery(location).attr('href');
        var currentpagename = href.substring(href.lastIndexOf('/') + 1);

        if (currentpagename.toUpperCase() == "CENGAGEECOMMERCE.ASPX") {
            $('#loginlnk').addClass('loginlnkactive');
            $('#userimg').addClass('useractive');         
        }
        else if (currentpagename.toUpperCase() == "SIGNUP.ASPX") {
            $('#SignUp').addClass('signuplnkactive');

        }
        else if (currentpagename.toUpperCase() == "HELP.ASPX") 
        {
           $('[id$=lblbannertxt]').text("HELP");
        }
        
        if (currentpagename.toUpperCase() == "LIST.ASPX")
        {
          $('#wishlistlnk').addClass('util_rhtwishlistactive');
            $('#wishlistimg').css('background', 'url("portals/0/images/star_blue.png") no-repeat scroll 0 0 transparent');

            if ($('#WishList').text().length == 0) {
                $('#wishlistlnk').width('99px!important').addClass("util_rhtwishlistactive");  
                $('#wishlistlnk').css('background', 'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent');
               
            }
            else {
                $('#wishlistlnk').width('124px!important').addClass("util_rhtwishlistactive");
                $('#wishlistlnk').css('background', 'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent');
                
            }

        }
        
        if(currentpagename.toUpperCase().indexOf("LIST.ASPX") >= 0) 
        {

            if (currentpagename.toUpperCase() == "LIST.ASPX?ITEM=CART") {
                $('#cartlnk').addClass('util_rhtcartlnkactive');
                $('#cartimg').css('background', 'url("portals/0/images/cart_blue.png") no-repeat scroll 0 0 transparent');

                if ($('#Cart').text().length == 0) {

                    $('#cartlnk').css('background', 'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent');
                    $('#cartlnk').width('100px').addClass("util_rhtcartlnkactive");

                }
                else {
                    $('#cartlnk').css('background', 'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent');
                    $('#cartlnk').width('124px').addClass("util_rhtcartlnkactive");

                }

            }
            else 
            {
                $('#wishlistlnk').addClass('util_rhtwishlistactive');
                $('#wishlistimg').css('background', 'url("portals/0/images/star_blue.png") no-repeat scroll 0 0 transparent');

                if ($('#WishList').text().length == 0) {
                    $('#wishlistlnk').width('99px!important').addClass("util_rhtwishlistactive");  
                    $('#wishlistlnk').css('background', 'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent');
               
                }
                else {
                    $('#wishlistlnk').width('124px!important').addClass("util_rhtwishlistactive");
                    $('#wishlistlnk').css('background', 'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent');
                
                }
            }
        }
        
    
      
        var dropdownlist = $("#ddlschooltypetop").data("kendoDropDownList");
         var dropdownlistbottom = $("#ddlschooltypefooter").data("kendoDropDownList");
          //dropdownlist.select(function(dataItem) {
         //       return dataItem.text === "ALL";
          //  });
         //     dropdownlistbottom.select(function(dataItem) {
          //      return dataItem.text === "ALL";
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
         $('#DashBoardMenu').css("display", "block");
        if (currentpagename.toUpperCase() == "DASHBOARD.ASPX" || currentpagename.toUpperCase().indexOf("DASHBOARD.ASPX") >= 0) 
        {            
             $('#dashboardlnk').addClass('DbDashboardActive');
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

function OpenWindow() 
{   
        var window = $("#window"),
            signup = $("#SignUp").bind("click", function () { window.data("kendoWindow").open();  });
        $("#SignUp").click(function (e) {
          window.data("kendoWindow").open();
        });
        onClose = function () {
           signup.show();
        }
        if (!window.data("kendoWindow")) 
        {
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
function OpenLoginWindow(elementId) 
{ 
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
        if ($.browser.msie) {
        $("#loginWindow").css({ 'height': '362px', 'margin-left': '-9px', 'width': '509px' });
        $("#loginWindow").parent().css('height', '317px');
        }
        else if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
                $("#loginWindow").css({ 'height': '361px', 'margin-left': '-9px', 'width': '507px' });
                $("#loginWindow").parent().css('height', '317px');
            }
        else
        {
            $("#loginWindow").css({ 'height': '362px', 'margin-left': '-9px', 'width': '509px' });
            $("#loginWindow").parent().css('height', '317px');
        }
        $('a.k-window-action.k-link').mouseover(function () 
        {
            $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false;
        });  
}
</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />

<div class="middlecont" style="margin-top:-46px;min-height:580px;">
    <div class="contCentr">
        <div class="Panes" style="margin-top:123px">         
        <div id="TopPane" runat="server" style="clear: both; display: inline-block; z-index: 5;
                width: 958px; height: 100%;">
            </div>
            
            <div id="LeftPane" runat="server" style="float:left; clear: both; display: inline-block; z-index: 5;
                width: 241px; margin-bottom:10px;" class="leftPaneBorder">
            </div>
            <div id="ContentPane" runat="server" style="clear: both; display: inline-block; z-index: 5;
                width: 690px ;min-height:500px;">
            </div>
          </div>
    </div>
</div>
