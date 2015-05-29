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
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.DDRMenu.TemplateEngine" Assembly="DotNetNuke.Web.DDRMenu" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" TagName="CONTROLPANEL" Src="~/Admin/Skins/controlpanel.ascx" %>
<%@ Register TagPrefix="dnn" TagName="UTILITIES" Src="~/controls/Utilities.ascx" %>
<%@ Register TagPrefix="cengage" TagName="CENGAGESUBMENU" Src="~/controls/SubMenuControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FIRSTLEVELCONTROL" Src="~/controls/FirstLevelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/stylesheets/Homeskincorp.css" Priority="14"  />
	<dnn:DnnCssInclude  runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap.min.css" />
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/custom.css" />
	<!-- <script type="text/javascript" src="/Resources/Shared/scripts/Homeskincorp.js"> </script>
     HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="/Resources/Shared/scripts/html5shiv.js"></script>
      <script src="/Resources/Shared/scripts/respond.min.js"></script>
    <![endif]-->

<script type="text/javascript">
$(document).ready(function () 
{   
var e;
if(window!=undefined){
e = window.location.pathname;
}
if (e == "/")
{
$("#breadcrump").show();
}



function onCountryClose() {
    if ($("#CountryName")[0].innerHTML.toLowerCase().indexOf("international") > -1) {
        document.location.href = "http://www.cengage.com/country/"
    }
    if (location.host.toLowerCase().indexOf(".co.nz") > -1) {
        if (!($("#CountryName")[0].innerHTML.toLowerCase().indexOf("new zealand") > -1)) {
            if ($("#CountryName")[0].innerHTML.toLowerCase().indexOf("international") > -1) {
                document.location.href = "http://www.cengage.com/country/"
            } else {
                document.location.href = location.protocol + "//" + "cengage.com.au" + document.location.href.split(document.location.hostname)[1]
            }
        }
    }
}




 var count=0;
 var count1=0;
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
                                draggable: false,
								visible:false
                            });                                                  
                  }                     
                  window3.data("kendoWindow").open(); 
                  window3.data("kendoWindow").center();   
              <% } else { %>                       
                location.href= "List.aspx";
              <% } %>
            });
     <% if(Request.QueryString["inviteesk"]!=null){ %>
	 count1++;
             var subsid=getParameterByName("subsid");
             var inviteesk=getParameterByName("inviteesk");
             var accountsk=getParameterByName("accountsk");
             var usersk=getParameterByName("usersk");
             $('.k-window-actions.k-header').css('cursor', 'pointer');
             var window = $("#window1");
             if (!window.data("kendoWindow")) {
                 window.kendoWindow({
                      width: "498px",
                      height: "920px",
                     modal: true,
                     content: 
    GetFile("/signup.aspx?inviteesk="+inviteesk+"&subsid="+subsid+"&accountsk="+accountsk+"&usersk="+usersk),
                     draggable: false,
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
        //SignUp
         <% if(Session["Visitor"] == null) {%>
         $('.k-window-actions.k-header').css('cursor', 'pointer');
         var window = $("#window1");
         if (!window.data("kendoWindow")) {
             window.kendoWindow({
                 width: "714px",
                 height: "470px",
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
         //window.data("kendoWindow").open();
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
                        width: "714px",
                        height: "465px",
                        modal: true,
                        content: GetFile("/CountryDetection?page=changeloc"),
                        draggable: false,
				 close:onCountryClose,
				 visible:false
                    });
                    window2.data("kendoWindow").center();
                    $(".k-icon.k-i-close").css("display", "none");
                    $('a.k-window-action.k-link').mouseover(function () {
                        $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                        return false;
                    });
                }
                window2.data("kendoWindow").open();
				$("#window2").parent().appendTo("form");
               <%} else { %>                
                var window = $("#CountryPopup");
                if (!window.data("kendoWindow")) 
                {
                    window.kendoWindow
                    ({
                        width: "441px",
                        modal: true,
                        draggable: false,
						visible:false
                    });
                    window.data("kendoWindow").center();                    
                     $('.k-window-action').css("margin-top", "-15px!important");
                }
                window.data("kendoWindow").open();
              <%}%>
        });  
		
    
    });

</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />
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
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" style="display: none;">
    <div class="msgicon">
        <img src="<%#Page.ResolveUrl("portals/0/images/alert_icon.png")%>" alt="alerticon" /></div>
    <div class="msginfotxt H7">
        YOU ARE USING THIS CENGAGE SITE AS <span id="LoginUserName" runat="server" clientidmode="Static">
        </span>
    </div>
    <a href="#" id="msginfoclosediv" class="msginfoclosediv H7">
        (CLOSE WINDOW)</a>
</div>
<header>
 <div class="header_wrapper container">
<dnn:utilities runat="server" id="Utilities1" clientidmode="Static" />



    <cengage:CENGAGESUBMENU ID="CENGAGESUBMENU" runat="server" /> 

</div>
</header>
<!-- Side Panel Start-->
<div id="RightSidePane" class="sidefloatright" runat="server">
</div>
<!-- Side Panel Ends-->
<div class="content_wrapper">
    <div class="container">
	<dnn:FIRSTLEVELCONTROL runat="server" id="LevelOneMenu" clientidmode="Static" />
        <div class="Panes">
		 <% if(PortalSettings.Current.ActiveTab.ParentId!=-1 && PortalSettings.Current.HomeTabId!=PortalSettings.Current.ActiveTab.TabID) {%>
           <div class="b-crumb">
                <dnn:BREADCRUMB runat="server" ID="dnnBreadcrumb" CssClass="cengage-breadcrumb" RootLevel="0" Separator="&nbsp;/&nbsp;" />
            </div>
			<% }%>
            <div id="ContentPane" runat="server" class="corporatContentPane">
            </div>
        </div>
    </div>
</div>
<div class="he-wrapper">
   <div class="scrolltotop">	
	<a href="#"><span class="ico-backtotop"></span>Back To Top</a>
	</div>
</div>
<%# Session["footer_megatop"]%>
<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" Text=" " ClientIDMode="Static" Style="display: none;" />
</div>
<script type="text/javascript">
function OpenLoginWindow(e) {
    var t = jQuery(location).attr("href");
    var n = t.substring(t.lastIndexOf("/") + 1);
    var r = $("#loginWindow");
    r.kendoWindow({
        width: "491px",
        height: "317px",
        content: GetFile("/signin.aspx"),
        modal: true,
        draggable: true,
        resizable: true,
        visible: false
    });
    r.data("kendoWindow").center();
    $("#loginWindow").closest(".k-window").css({
        position: "fixed",
        margin: "auto"
    });
    r.data("kendoWindow").open();
    if (n.toUpperCase() == "CHECKOUT.ASPX") {
        $("#loginWindow").parent().css("width", "485px")
    } else {
        $("#loginWindow").parent().css("width", "491px")
    }
    if ($.browser.msie) {
        $("#loginWindow").css({
            height: "300px",
            "margin-left": "-16px",
            width: "509px"
        });
        $("#loginWindow").parent().css("height", "317px")
    } else if (navigator.userAgent.match(/AppleWebKit/) && !navigator.userAgent.match(/Chrome/)) {
        $("#loginWindow").css({
            height: "300px",
            "margin-left": "-16px",
            width: "509px"
        });
        $("#loginWindow").parent().css("height", "317px")
    } else {
        $("#loginWindow").css({
            height: "300px",
            "margin-left": "-16px",
            width: "509px"
        });
        $("#loginWindow").parent().css("height", "317px")
    }
    $("a.k-window-action.k-link").mouseover(function() {
        $("a.k-window-action.k-link").parent().css({
            "background-image": 'url("./Portals/0/images/close.png") !important'
        });
        return false
    });
	event.preventDefault();
}
</script>
