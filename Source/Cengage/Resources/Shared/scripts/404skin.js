﻿function isiPhone(){return navigator.platform.indexOf("iPhone")!=-1||navigator.platform.indexOf("iPod")!=-1}function getParameterByName(e){var t=window.location.href;t=t.replace(/[\[]/,"\\[").replace(/[\]]/,"\\]");var n=new RegExp("[\\?&]"+e+"=([^&#]*)"),r=n.exec(location.search);return r==null?"":decodeURIComponent(r[1].replace(/\+/g," "))}function OpenWindowFromList(){var e=$("#window"),t=$("#signuplnk").bind("click",function(){e.data("kendoWindow").open()});$("#signuplnk").click(function(t){e.data("kendoWindow").open()});onClose=function(){t.show()};if(!e.data("kendoWindow")){e.kendoWindow({width:"498px",height:"920px",content:GetFile("/signup.aspx"),close:onClose,modal:true,draggable:false,visible:false});e.data("kendoWindow").center();$(".k-icon.k-i-close").css("display","none");$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false});$("#ListPopuocreateuserbutton").data("kendoWindow").close()}}function OpenLoginWindowFromList(){var e=$("#loginWindow");e.kendoWindow({width:"491px",height:"317px",content:GetFile("/signin.aspx"),modal:true,draggable:true,resizable:true,visible:false});e.data("kendoWindow").center();e.data("kendoWindow").open();if($.browser.msie){$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}else if(navigator.userAgent.match(/AppleWebKit/)&&!navigator.userAgent.match(/Chrome/)){$("#loginWindow").css({height:"361px","margin-left":"-9px",width:"507px"});$("#loginWindow").parent().css("height","317px")}else{$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false});$("#ListPopuocreateuserbutton").data("kendoWindow").close()}function onCountryClose(){if($("#CountryName")[0].innerHTML.toLowerCase().indexOf("international")>-1){document.location.href="http://www.cengage.com/country/"}if(location.host.toLowerCase().indexOf(".co.nz")>-1){if(!($("#CountryName")[0].innerHTML.toLowerCase().indexOf("new zealand")>-1)){if($("#CountryName")[0].innerHTML.toLowerCase().indexOf("international")>-1){document.location.href="http://www.cengage.com/country/"}else{document.location.href=location.protocol+"//"+"cengage.com.au"+document.location.href.split(document.location.hostname)[1]}}}}function GetFile(e){var t=window.location.pathname;var n=t.split("/");var r=location.protocol+"//"+window.location.host+"/"+n[0];var i=r+e;return i}function CountryClose(){if(window.location.href.indexOf(".co.nz")>-1){if($("#CountryName")[0].innerHTML.trim().toUpperCase()!="NEW ZEALAND"){window.location.href=window.location.href.replace(".co.nz",".com.au")}}}function OpenWindow(){var e=$("#window");e.kendoWindow({width:"498px",height:"920px",content:GetFile("/signup.aspx"),modal:true,draggable:true,resizable:true,visible:false});e.data("kendoWindow").center().open();$(".k-icon.k-i-close").css("display","none");$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false})}function OpenLoginWindow(e){var t=$("#loginWindow");t.kendoWindow({width:"491px",height:"317px",content:GetFile("/signin.aspx"),modal:true,draggable:true,resizable:true,visible:false});t.data("kendoWindow").center();$("#loginWindow").closest(".k-window").css({position:"fixed",margin:"auto"});t.data("kendoWindow").open();if($.browser.msie){$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}else if(navigator.userAgent.match(/AppleWebKit/)&&!navigator.userAgent.match(/Chrome/)){$("#loginWindow").css({height:"361px","margin-left":"-9px",width:"507px"});$("#loginWindow").parent().css("height","317px")}else{$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false})}$(document).ready(function(){$("<li><a class='H6' tabindex='1' href='about-us.aspx'>About Us</a></li><li class='line-seprator'>|</li>").prependTo($("#util_aligndiv .util_lftdiv")[0].children[0]);$(window).resize(function(){var e=$(".k-window-content");e.each(function(){e.data("kendoWindow").center()})})});$("#msginfoclosediv").click(function(){$("#LoginMessage").hide()});if($.browser.msie){$(".advSrchbtn_Top").css("background-color","white")}if(navigator.userAgent.match(/iPad/i)!=null){$(".bannerimg").css("width","100%")}else{$(".bannerimg").css("width","1600px")}if(navigator.userAgent.indexOf("Mac")>-1){$.browser.safari=$.browser.webkit&&!/chrome/.test(navigator.userAgent.toLowerCase());if($.browser.safari){$(".footer_bottom_left").css("margin-left","-6px");$(".footer_bottom_right").css("margin-right","27px")}if(navigator.userAgent.indexOf("Chrome")!=-1){$(".footer_bottom_left").css("margin-left","-8px");$(".footer_bottom_right").css("margin-right","30px")}if($.browser.mozilla){$(".footer_bottom_right").css("margin-right","28px")}}$("#tabstrip").kendoTabStrip({animation:{open:{effects:"fadeIn"}},select:function(e){if($.browser.msie){if($(e.item).index()=="0"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="1"){$("#ie_literacydiv").removeClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="2"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").removeClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="3"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").removeClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="4"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").removeClass("tabshadow")}}}});$(document).ready(function(){$("#advancedsrchTopdiv").click(function(){$("#advancedsrchdialog").css("display","block");$(".tabs").css("z-index","0");$("#advancedsrchpop").attr("src",GetFile("/advancedsearch.aspx"));if(!$("#imgdiv").hasClass("backgrdimageopen")){$("#imgdiv").addClass("backgrdimageopen");$("#imgdiv").removeClass("backgrdimage");$("#advancedsrchTopdiv").removeClass("advSrchbtn_Top");$("#advancedsrchTopdiv").addClass("advSrchbtn_TopSel");$("#advancedsrchpop").attr("src",GetFile("/advancedsearch.aspx"))}else{$("#advancedsrchdialog").css("display","none");$(".tabs").css("z-index","5");$("#imgdiv").addClass("backgrdimage");$("#imgdiv").removeClass("backgrdimageopen");$("#advancedsrchTopdiv").removeClass("advSrchbtn_TopSel");$("#advancedsrchTopdiv").addClass("advSrchbtn_Top")}GAPushTrackEvent("Site-Search","Click","Advance-Search-Button")});$("body").click(function(e){if(e!=undefined&&e.target!=undefined&&$(e.target.parentElement).attr("id")!="advancedsrchTopdiv"){$("#advancedsrchdialog").css("display","none");$(".tabs").css("z-index","5");$("#imgdiv").addClass("backgrdimage");$("#imgdiv").removeClass("backgrdimageopen");$("#advancedsrchTopdiv").removeClass("advSrchbtn_TopSel");$("#advancedsrchTopdiv").addClass("advSrchbtn_Top")}})});if(navigator.userAgent.indexOf("Safari")!=-1&&navigator.userAgent.indexOf("Chrome")==-1){$("#tabstrip li").css("top","-12px");$("#tabstrip .k-tabstrip-items .k-first").css("top","-13px!important")}else{$("#tabstrip li").css("top","-14px")}var href=jQuery(location).attr("href");var currentpagename=href.substring(href.lastIndexOf("/")+1);if(currentpagename.toUpperCase()=="SIGNIN.ASPX"){$("#loginlnk").addClass("loginlnkactive");$("#userimg").addClass("useractive")}else if(currentpagename.toUpperCase()=="SIGNUP.ASPX"){$("#SignUp").addClass("signuplnkactive")}else if(currentpagename.toUpperCase()=="HELP.ASPX"){$("[id$=lblbannertxt]").text("HELP")}if(currentpagename.toUpperCase()=="LIST.ASPX"){$("#wishlistlnk").addClass("util_rhtwishlistactive");$("#wishlistimg").css("background",'url("<%= Page.ResolveUrl("portals/0/images/star_blue.png")%>") no-repeat scroll 0 0 transparent');if($("#WishList").text().length==0){$("#wishlistlnk").width("99px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_blue.png")%>") no-repeat scroll 0 0 transparent')}else{$("#wishlistlnk").width("124px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/mylist_blue.png")%>") no-repeat scroll 0 0 transparent')}}if(currentpagename.toUpperCase().indexOf("LIST.ASPX")>=0){if(currentpagename.toUpperCase()=="LIST.ASPX?ITEM=CART"){$("#cartlnk").addClass("util_rhtcartlnkactive");$("#cartimg").css("background",'url("<%= Page.ResolveUrl("portals/0/images/cart_blue.png")%>") no-repeat scroll 0 0 transparent');if($("#Cart").text().length==0){$("#cartlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_blue.png")%>") no-repeat scroll 0 0 transparent');$("#cartlnk").width("100px").addClass("util_rhtcartlnkactive")}else{$("#cartlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/mylist_blue.png")%>") no-repeat scroll 0 0 transparent');$("#cartlnk").width("124px").addClass("util_rhtcartlnkactive")}}else{$("#wishlistlnk").addClass("util_rhtwishlistactive");$("#wishlistimg").css("background",'url("<%= Page.ResolveUrl("portals/0/images/star_blue.png")%>") no-repeat scroll 0 0 transparent');if($("#WishList").text().length==0){$("#wishlistlnk").width("99px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/cart_btn_blue.png")%>") no-repeat scroll 0 0 transparent')}else{$("#wishlistlnk").width("124px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("<%= Page.ResolveUrl("portals/0/images/mylist_blue.png")%>") no-repeat scroll 0 0 transparent')}}}