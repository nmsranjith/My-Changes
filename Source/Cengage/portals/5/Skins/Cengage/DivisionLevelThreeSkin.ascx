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
<%@ Register TagPrefix="cengage" TagName="UTILITIES" Src="~/controls/Utilities.ascx" %>
<%@ Register TagPrefix="cengage" TagName="BROWSESEARCH" Src="~/controls/BrowseAndSearchControl.ascx" %>
<%@ Register TagPrefix="cengage" TagName="CENGAGESUBMENU" Src="~/controls/SubMenuControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="FIRSTLEVELCONTROL" Src="~/controls/FirstLevelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/stylesheets/Primarylevelskin.css" Priority="14"  />
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/stylesheets/Primaryhomeskin.css"  />
	<dnn:DnnCssInclude  runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap.min.css" />
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/custom.css" />
	<script type="text/javascript" src="/Resources/Shared/scripts/Secondarylevelskin.js"> </script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="/Resources/Shared/scripts/html5shiv.js"></script>
      <script src="/Resources/Shared/scripts/respond.min.js"></script>
    <![endif]-->
<script type="text/javascript">
 $(document).ready(function () 
 {
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
    "url('/Portals/0/images/close.png') !important");
                      return false;
                  });
             }
             window.data("kendoWindow").open();
          <%}%>	 
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
                    "url('/Portals/0/images/close.png') !important");
                                      return false;
                                  });
         }
         window.data("kendoWindow").open();
         <%}%>
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
				 visible: false
                    });
                    window2.data("kendoWindow").center();
                    $(".k-icon.k-i-close").css("display", "none");
                    $('a.k-window-action.k-link').mouseover(function () {
                        $('a.k-window-action.k-link').parent().css("background-image", "url('/Portals/0/images/close.png') !important");
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
                        draggable: false,
						visible: false
                    });
                    window.data("kendoWindow").center();                    
                     $('.k-window-action').css("margin-top", "-15px!important");
                }
                window.data("kendoWindow").open();
              <%}%>
        });  
        });  
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
<div id="ListPopuocreateuserbutton" class="skin_popupClass"
    style="display: none; margin-top: 0px !important; margin-left: -3px; overflow: hidden;">
    <div class="skin_popupbottmshade skin_popupdeleteheader">
        <h1 class="skin_fontnormal skin_marginnone">
            Shopping List</h1>
    </div>
    <br />
    <div class="skin_boxshadowstyle">
            <div class="skin_PopUpContentDiv" style="height: 65px; margin-top: 15px !important;
                width: 93%; text-align: left;">
                <span class="shoppinglistalertmsg skin_marginnone skin_l-space5 skin_PopUpContentSpan" style="margin-top: 24px;">
                    <%--To add items to the Shopping List, you must first be logged in….
                    <br />
                    Creating an account is free and easy.--%>
                    To add items to the Shopping List, you must first be logged in...
                </span>
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
    </div>
</div>
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" style="display:none;">
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
<cengage:UTILITIES runat="server" id="Utilities1" clientidmode="Static" />

    <cengage:CENGAGESUBMENU ID="CENGAGESUBMENU" runat="server" /> 
   
    
</div>
</header>

<!-- Side Panel Start-->
<div id="RightSidePane" class="sidefloatright" runat="server">
</div>
<!-- Side Panel Ends-->
<div class="content_wrapper">
    <div class="container">
    <cengage:BROWSESEARCH runat="server" id="BrowseSearch" clientidmode="Static" />
	<dnn:FIRSTLEVELCONTROL runat="server" id="LevelOneMenu" clientidmode="Static" />
        <div class="">
		 <% if(PortalSettings.Current.ActiveTab.ParentId!=-1 && PortalSettings.Current.HomeTabId!=PortalSettings.Current.ActiveTab.TabID) {%>
            <div class="LevelThree-bcrumb">
                <dnn:breadcrumb runat="server" id="dnnBreadcrumb" CssClass="cengage-breadcrumb" rootlevel="0" separator="&nbsp;/&nbsp;" />
            </div>
			<% }%>
			 <div id="TopPane" runat="server" class="corpLevelTopPane">
            </div>
			<div id="LeftPane" class="LevelThreeLeft" runat="server">
</div>
			<div id="RightPane" class="LevelThreeRight" runat="server">
</div>

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
