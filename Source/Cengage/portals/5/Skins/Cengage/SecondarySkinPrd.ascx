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
	<link href="/Resources/Shared/stylesheets/Secondaryprd.css" rel="stylesheet" type="text/css" />
<dnn:STYLES runat="server" ID="StylesIE8" Name="IE8Minus" StyleSheet="ie8skin.css"
    Condition="LT IE 10" UseSkinPath="true" />
<script type="text/javascript" src="/Resources/Shared/scripts/Secondaryskinprd.js"> </script>
<script type="text/javascript">
    $(document).ready(function () {
var count=0;
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
           var dropdownlist = $("#ddlschooltypetop").data("kendoDropDownList");
          var dropdownlistbottom = $("#ddlschooltypefooter").data("kendoDropDownList");
    });
</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />
<div id="window">
</div>
<div id="window1">
</div>
<div id="loginWindow">
</div>
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" visible="false">
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
            <h1>
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
</div>

<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" ClientIDMode="Static" Style="display: none;" />
</div>
