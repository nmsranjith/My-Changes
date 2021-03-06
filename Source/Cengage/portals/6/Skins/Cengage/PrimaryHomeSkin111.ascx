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
<dnn:styles runat="server" id="StylesIE8" name="IE8Minus" stylesheet="ie8skin.css"
    condition="LT IE 9" useskinpath="true" />
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
	<script src="<%#Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.idTabs.min.js")%>"
    type="text/javascript"></script>
<style type="text/css" rel="stylesheet">
    
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
        margin-top: 15px !important;
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
	 #masterhead {
    background: url("./portals/0/images/bg_middle.png") no-repeat scroll center 12px transparent !important;
}
.advSrchbtn_Top
{
width:185px !important;
margin-left:20px!important;
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
            content: GetFile("/cengageecommerce.aspx"),
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
          window = $("#window1");
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
                        height: "460px",
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
                 window = $("#CountryPopup");
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
            $('#cartlnk').css('background', 'url("portals/0/images/cart_btn_grey.png") no-repeat scroll');
    }
    else 
    {
            $('#cartlnk').width('124px').addClass("util_rhtcartlnk"); 
            $('#cartlnk').css('background', 'url("portals/0/images/mylist_grey.png") no-repeat scroll');
    }
    if ($('#WishList').text().length == 0) 
    {
            $('#wishlistlnk').width('99px').addClass("util_rhtwishlistlnk");
            $('#wishlistlnk').css('background', 'url("portals/0/images/cart_btn_grey.png") no-repeat');
    }
    else 
    {
            $('#wishlistlnk').width('124px').addClass("util_rhtwishlistlnk");      
            $('#wishlistlnk').css('background', 'url("portals/0/images/mylist_grey.png") no-repeat');
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
				$('#advancedsrchpop').attr('src','/advancedsearch.aspx');
                if($('#imgadvSrc').attr('src').indexOf('adv_search_sel')== -1)
                {
                   $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('adv_search','adv_search_sel'));  
                   $('#advancedsrchTopdiv').removeClass("advSrchbtn_Top");
                   $('#advancedsrchTopdiv').addClass("advSrchbtn_TopSel"); 
                }
                else{
                   $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                   $('#imgadvSrc').attr('src',$('#imgadvSrc').attr('src').replace('adv_search_sel','adv_search'));
                   $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
                   $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
                }
              
        });     

         $("body").click(function (obj) {          
              if(obj!=undefined && obj.srcElement != undefined && $(obj.srcElement.parentElement).attr("id") != "advancedsrchTopdiv")
              {
                       $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                       $('#imgadvSrc').attr('src',$('#imgadvSrc').attr('src').replace('adv_search_sel','adv_search'));
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
<dnn:controlpanel runat="server" id="cp" isdockable="True" />
<div id="window">
</div>
<div id="window1">
</div>
<div id="window2">
</div>
<div id="window3">
</div>
<div id="loginWindow">
</div>
<div id="ListPopuocreateuserbutton" class="skin_popupClass" clientidmode="Static"
    style="display: none; margin-top: 0px !important; margin-left: -3px; overflow: hidden;">
    <div class="skin_popupbottmshade skin_popupdeleteheader">
        <h1 class="skin_fontnormal skin_marginnone">
            Shopping List</h1>
    </div>
    <br />
    <div class="skin_boxshadowstyle">
        <center>
            <div class="skin_PopUpContentDiv" style="height: 65px; margin-top: 15px !important;
                width: 93%; text-align: left;">
                <h5 class="skin_marginnone skin_l-space5 skin_PopUpContentSpan" style="margin-top: 24px;">
                    <%--To add items to the Shopping List, you must first be logged in�.
                    <br />
                    Creating an account is free and easy.--%>
                    To add items to the Shopping List, you must first be logged in...
                </h5>
                <div class="skin_clearall">
                </div>
            </div>
            <div class="skin_clearall">
            </div>
            <div style="margin-top: 30px; width: 50%; margin-left: 50%;">
                <div>
                    <input id="ListLoginButton" type="button" onclick="javascript:OpenLoginWindowFromList();"
                        class="skin_popupbutton skin_lstfavourhbtn skin_floatRight skin_floatRight10"
                        value="LOGIN" />
                </div>
                <div>
                    <input id="ListSignupButton" type="button" onclick="javascript:OpenWindowFromList();"
                        class="skin_popupbutton skin_lstfavourhbtn skin_floatRight skin_floatRight10"
                        value="SIGN UP" />
                </div>
            </div>
        </center>
    </div>
</div>
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" style="display:none;">
    <div class="msgicon">
        <img src="portals/0/images/alert_icon.png" alt="alerticon" /></div>
    <div class="msginfotxt H7">
        YOU ARE USING THIS CENGAGE SITE AS <span id="LoginUserName" runat="server" clientidmode="Static">
        </span>
    </div>
    <div id="msginfoclosediv" class="msginfoclosediv H7">
        (CLOSE WINDOW)</div>
</div>
<dnn:utilities runat="server" id="Utilities1" clientidmode="Static" />
<div id="logo">
    <div class="algnCentr">
        <dnn:logo id="dnnLogo" runat="server" />
    </div>
</div>
<div id="masterhead">
    <%--<div id="Mainmenu" runat="server" clientidmode="Static" class="algnCentr" style="display: none;">
        <ul id="Mainmenuul">
            <li id="menuitem_primary"><a id="primarylnk" href="primary.aspx"><span class="tile_icon Primary"
                clientidmode="Static" id="Primary"></span></a></li>
            <li id="menuitem_secondary"><a id="secondarylnk" href="secondary.aspx"><span class="tile_icon Secondary"
                clientidmode="Static" id="Secondary"></span></a></li>
            <li id="menuitem_staffroom"><a id="staffroomlnk" href="staffroom.aspx"><span class="tile_icon StaffRoom"
                clientidmode="Static" id="StaffRoom"></span></a></li>
            <li id="menuitem_aboutus"><a id="aboutuslnk" href="aboutus.aspx" class="menuitem"><span
                clientidmode="Static" class="tile_icon AboutUs" id="AboutUs"></span></a></li>
        </ul>
    </div>--%>
    <cengage:cengagesubmenu id="CENGAGESUBMENU" runat="server" />
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
            <cengage:cengagesearch id="SearchComponent" runat="server" />
        </div>
        <div id="advancedsrchTopdiv" class="advSrchbtn_Top H5">
            <div class="adv_icon_top">
                <img id="imgadvSrc" alt="advsrchicon" src="<%#Page.ResolveUrl("Portals/0/Images/adv_search.png")%>" /></div>
            <span class="adv_txt">Advanced Search</span>
        </div>
        <div id="advancedsrchdialog">
            <iframe id="advancedsrchpop" class="ifrmsrch"></iframe>
        </div>
    </div>
    <div class="bannersec">
        
        <div id="bannertitlePane" runat="server" class="bannertitle algnCentr">
            <h1 style="position:relative;left:-725px;top:200px;">
			    <%#PortalSettings.ActiveTab.TabName %>
                <!--<asp:Label ID="lblbannertxt" runat="server" Text="" ClientIDMode="Static"></asp:Label>-->
            </h1>
        </div>
    </div>
</div>
<!--
<div id="Hometab" class="tabs">
    <div id="tabstripcontent" class="k-content">
        <div id="tabstrip">
            <ul class="H5">
                <li id="welcomeli" class="k-state-active"><span id="welcomeimg"></span><span id="welcometxt">
                    WELCOME</span> </li>
                <li id="literacyli"><span id="literacyimg"></span><span id="literacytxt">LITERACY</span>
                    <div id="ie_literacydiv">
                    </div>
                </li>
                <li id="numeracyli"><span id="numeracyimg"></span><span id="numeracytxt">NUMERACY</span>
                    <div id="ie_numaracydiv">
                    </div>
                </li>
                <li id="workbooksli"><span id="workbooksimg"></span><span id="workbookstxt">WORK BOOKS</span>
                    <div id="ie_workbooksdiv">
                    </div>
                </li>
                <li id="digitalli"><span id="digitalimg"></span><span id="digitaltxt">DIGITAL</span>
                    <div id="ie_digitaldiv">
                    </div>
                </li>
            </ul>
            <div id="WELCOME">
                <div id="WelcomeTopPane" runat="server" class="contCentr WelcomeTopPane contdisplay"
                    style="clear: both; display: inline-block; z-index: 5;">
                </div>
                <div>
                    <div id="WelcomeTopLeftPane" class="WelcomeTopLeftPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                    <div id="WelcomeTopRightPane" class="WelcomeTopRightPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                </div>
                <div class="lftDiv">
                    <div id="WelcomeMiddleLeftPane" class="WelcomeMiddleLeftPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                    <div id="WelcomeMiddleRightPane" class="WelcomeMiddleRightPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                </div>
                <div>
                    <div id="WelcomeBottomLeftPane" class="WelcomeBottomLeftPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                    <div id="WelcomeBottomRightPane" class="WelcomeBottomRightPane contdisplay" runat="server"
                        style="clear: both; display: inline-block; z-index: 5;">
                    </div>
                </div>
            </div>
            <div id="LITERACY">
            </div>
            <div id="NUMERACY">
            </div>
            <div id="WORKBOOKS">
            </div>
            <div id="DIGITAL">
            </div>
        </div>
    </div>
</div>
-->
<div class="middlecont" style="margin-top: -46px;min-height:500px">
    <div class="contCentr">
        <div class="Panes">
            <div style="float: left; width: 100%; margin-top: 15px; margin-bottom: 15px; text-transform: uppercase;
                border-bottom: 1px solid #B7AAAA">
              <dnn:text runat="server" id="dnnTEXT" text="You are here:" resourcekey="Breadcrumb" />
                <dnn:breadcrumb runat="server" id="dnnBreadcrumb" CssClass="Breadcrumb" rootlevel="0" separator="&nbsp;/&nbsp;" />
                <div style="float: right; width: 195px; margin-top: 5px; margin-bottom: 5px;">
                    <a href="https://www.facebook.com/NelsonPrimary" target="_blank">
                        <img src="<%# Page.ResolveUrl("Portals/0/Images/facebook.png")%>" /></a>
                         <a href="https://twitter.com/nelsonprimary" style=" margin-left: 10px; 
                            target="_blank">
                            <img src="<%# Page.ResolveUrl("Portals/0/Images/twitter.png")%>" /></a>
                    <a href="https://www.youtube.com/user/NelsonPrimaryVideo" target="_blank" style=" margin-left: 10px; ">
                        <img src="<%# Page.ResolveUrl("Portals/0/Images/ecollyoutube.png")%>" /></a>
                    <a href="http://nelsonliteracydirectionsdotcom.wordpress.com/" target="_blank" style=" margin-left: 10px; ">
                        <img src="<%# Page.ResolveUrl("Portals/0/Images/rssreader.png")%>" /></a>
                </div>
            </div>
            <div id="TopPane" runat="server" style="clear: both; display: inline-block; z-index: 5;
                width: 958px; height: 100%;">
            </div>
            <div id="ContentPane" runat="server" style="clear: both; display: inline-block; z-index: 5;">
            </div>
        </div>
    </div>
</div>
<div id="Srchbottom" class="SrchBottom">
    <div class="SrchComp_Bottom">
        <cengage:footercengagesearch id="FOOTERCENGAGESEARCH" runat="server" />
    </div>
    <div class="Srchbtmline">
    </div>
</div>
<div id="footer_top">
</div>
<div id="footer">
    <div id="footer_megatop">
        <%# Session["footer_megatop"]%>
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
