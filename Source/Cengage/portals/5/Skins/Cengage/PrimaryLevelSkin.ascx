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
    Condition="LT IE 9" UseSkinPath="true" />
 <link href="/Resources/Shared/stylesheets/Primarylevel.css" rel="stylesheet" type="text/css" />
	
 <script type="text/javascript" src="/Resources/Shared/scripts/Primarylevelskin.js"> </script>
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
		        $("#window1").remove();
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                var window2 = $("#window2");
                if (!window2.data("kendoWindow")) {
                    window2.kendoWindow({
                        width: "723px",
                        height: "440px",
                        modal: true,
                        content: GetFile("/CountryDetection.aspx?page=changeloc"),
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
       $("#ddlschooltypetop").kendoDropDownList();
        $("#ddlschooltypefooter").kendoDropDownList();
    if ($('#Cart').text().length == 0) 
    {
            $('#cartlnk').width('100px').addClass("util_rhtcartlnk"); 
            $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
    }
    else 
    {
            $('#cartlnk').width('124px').addClass("util_rhtcartlnk");
            $('#cartlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/mylist_grey.png")%>") no-repeat scroll');
    }
    if ($('#WishList').text().length == 0) 
    {
            $('#wishlistlnk').width('99px').addClass("util_rhtwishlistlnk");
            $('#wishlistlnk').css('background', 'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_grey.png")%>") no-repeat scroll');
           
    }
    else 
    {
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
        <center>
            <div class="skin_PopUpContentDiv" style="height: 65px; margin-top: 15px !important;
                width: 93%; text-align: left;">
                <h5 class="skin_marginnone skin_l-space5 skin_PopUpContentSpan" style="margin-top: 24px;">
                    <%--To add items to the Shopping List, you must first be logged in….
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
<div id="LoginMessage" runat="server" clientidmode="Static" class="msginfodiv" visible="false">
    <div class="msgicon">
        <img src="<%#Page.ResolveUrl("portals/0/images/alert_icon.png")%>" alt="alerticon" /></div>
    <div class="msginfotxt H7">
        YOU ARE USING THIS CENGAGE SITE AS <span id="LoginUserName" runat="server" clientidmode="Static">
        </span>
    </div>
    <div id="msginfoclosediv" class="msginfoclosediv H7">
        (CLOSE WINDOW)</div>
</div>
<dnn:UTILITIES runat="server" ID="Utilities1" ClientIDMode="Static" />
<div id="logo">
    <div class="algnCentr">
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
            <iframe id="advancedsrchpop" class="ifrmsrch"></iframe>
        </div>
    </div>
    <div class="bannersec">
        <div id="bannertitlePane" runat="server" class="bannertitle algnCentr">
           <h1>
		<%#PortalSettings.ActiveTab.Title.Split('|')[0].ToString() %>
                <asp:Label ID="lblbannertxt" runat="server" Visible="false" Text="" ClientIDMode="Static"></asp:Label>
            </h1>
        </div>
    </div>
</div>

<div class="middlecont" style="margin-top:-25px;min-height:580px;">
    <div class="contCentr">
        <div class="Panes">
          <div style="float: left; width: 100%; margin-top: 15px; margin-bottom: 15px; text-transform: uppercase; border-bottom: 1px solid #B7AAAA">
                <dnn:TEXT runat="server" ID="dnnTEXT" Text="You are here:" ResourceKey="Breadcrumb" />
                <dnn:BREADCRUMB runat="server" ID="dnnBreadcrumb" RootLevel="0" Separator="&nbsp;/&nbsp;" />
              <div style="float: right; width: 195px; margin-top: 5px; margin-bottom: 5px;">
                    <a href="https://www.facebook.com/NelsonPrimary">
                        <img alt="facebook" src="<%# Page.ResolveUrl("Portals/0/Images/facebook.png")%>" /></a>
                         <a href="https://twitter.com/nelsonprimary" style=" margin-left: 10px;">
                            <img alt="twitter" src="<%# Page.ResolveUrl("Portals/0/Images/twitter.png")%>" /></a>
                    <a href="https://www.youtube.com/user/NelsonPrimaryVideo"  style=" margin-left: 10px; ">
                        <img alt="youtube" src="<%# Page.ResolveUrl("Portals/0/Images/ecollyoutube.png")%>" /></a>
                    <a href="http://nelsonliteracydirectionsdotcom.wordpress.com/"  style=" margin-left: 10px; ">
                        <img alt="rssreader" src="<%# Page.ResolveUrl("Portals/0/Images/rssreader.png")%>" /></a>
                </div>
            </div>
               <div id="TopPane" runat="server" style="clear: both; display: inline-block; z-index: 5;
                width: 940px;"></div>
            
            
                 <div id="LeftPane" runat="server" style="float:left; display:block; z-index: 5;
                width: 210px; margin: -22px 10px 10px 0; height: 100%;"></div>


                 <div style="width: 10px; float: left;"> 				 
                 <div style="position: absolute; height: 100%;display: block; margin: 30px 0 0 0; z-index:30">
                  <img alt="" id="imgsepline" src="/Portals/0/images/separatorarrow.Png"></div></div>

          
				  <div style="position:relative; width: 715px ; position:relative; display: block; z-index: 12;
                  min-height:400px; float:left; height:auto; border-left: 1px #ccc solid; margin-bottom:20px; ">
                  <div id="ContentPane" runat="server" style="margin:-22px 0 0 20px; ">   </div>
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
