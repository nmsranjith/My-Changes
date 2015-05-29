<%@ Control Language="C#" CodeBehind="~/DesktopModules/Skins/skin.cs" AutoEventWireup="false"
    Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="NAV" Src="~/Admin/Skins/Nav.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TEXT" Src="~/Admin/Skins/Text.ascx" %>
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
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/stylesheets/Secondary.css" Priority="14"  />
	<dnn:DnnCssInclude  runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap.min.css" />
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/custom.css" />
	<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap-select.css" />
	<script type="text/javascript" src="/Resources/Shared/scripts/Secondaryskin.js"> </script>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
     <script src="/Resources/Shared/scripts/html5shiv.js"></script>
      <script src="/Resources/Shared/scripts/respond.min.js"></script>
    <![endif]-->
<script type="text/javascript">
$(document).ready(function () {
var count=0;
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
         window.data("kendoWindow").open();
         <%}%>
        $('.ddlcountry').click(function () {       
	    <% if(Session["IsAuthenticated"] == null) {%>
            count=1;
            $('.k-window-actions.k-header').css('cursor', 'pointer');
            var window = $("#window1");
            if (!window.data("kendoWindow")) {
                window.kendoWindow({
                    width: "714px",
                    height: "465px",
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
    });
</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />
<div id="window">
</div>
<div id="window1">
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
 <div id="masterhead" class="header_wrapper container">
<dnn:utilities runat="server" id="Utilities1" clientidmode="Static" />
    <cengage:CENGAGESUBMENU ID="CENGAGESUBMENU" runat="server" /> 
    <h1 id="SecondaryPageHeader" runat="server" clientidmode="Static">
 <asp:Label Visible="false" ID="lblbannertxt" runat="server" Text="" ClientIDMode="Static"></asp:Label>
 </h1>
</div>
 
</header>
<!-- Side Panel Start-->
<div id="RightSidePane" class="sidefloatright" runat="server">
</div>
<!-- Side Panel Ends-->
<div class="content_wrapper">
    <div class="container">
        <div class="Panes">
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
<asp:HiddenField ID="Hiddenpageresult" ClientIDMode="Static" runat="server" Value="Beforeclick" />
<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" Text=" " ClientIDMode="Static" Style="display: none;" />
</div>
