﻿function isiPhone(){return navigator.platform.indexOf("iPhone")!=-1||navigator.platform.indexOf("iPod")!=-1}function getParameterByName(e){var t=window.location.href;t=t.replace(/[\[]/,"\\[").replace(/[\]]/,"\\]");var n=new RegExp("[\\?&]"+e+"=([^&#]*)"),r=n.exec(location.search);return r==null?"":decodeURIComponent(r[1].replace(/\+/g," "))}function OpenWindowFromList(){var e=$("#window"),t=$("#signuplnk").bind("click",function(){e.data("kendoWindow").open()});$("#signuplnk").click(function(t){e.data("kendoWindow").open()});onClose=function(){t.show()};if(!e.data("kendoWindow")){e.kendoWindow({width:"498px",height:"874px",content:GetFile("/signup.aspx"),close:onClose,modal:true,draggable:false});e.data("kendoWindow").center();$(".k-icon.k-i-close").css("display","none");$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false});$("#ListPopuocreateuserbutton").data("kendoWindow").close()}}function OpenLoginWindowFromList(){var e=$("#loginWindow");e.kendoWindow({width:"491px",height:"317px",content:GetFile("/cengageecommerce.aspx"),modal:true,draggable:true,resizable:true});e.data("kendoWindow").center();e.data("kendoWindow").open();if($.browser.msie){$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}else if(navigator.userAgent.match(/AppleWebKit/)&&!navigator.userAgent.match(/Chrome/)){$("#loginWindow").css({height:"361px","margin-left":"-9px",width:"507px"});$("#loginWindow").parent().css("height","317px")}else{$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false});$("#ListPopuocreateuserbutton").data("kendoWindow").close()}function GetFile(e){var t=window.location.pathname;var n=t.split("/");if(n.length>0){var r="http://"+window.location.host+"/"+n[1]}else{var r="http://"+window.location.host+"/"+n[0]}var i=r+e;return i}function OpenWindow(){var e=$("#window"),t=$("#SignUp").bind("click",function(){e.data("kendoWindow").open()});$("#SignUp").click(function(t){e.data("kendoWindow").open()});onClose=function(){t.show()};if(!e.data("kendoWindow")){e.kendoWindow({width:"498px",height:"874px",content:GetFile("/signup.aspx"),close:onClose,modal:true,draggable:false});e.data("kendoWindow").center();$(".k-icon.k-i-close").css("display","none");$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false})}}function OpenLoginWindow(e){var t=$("#loginWindow");t.kendoWindow({width:"491px",height:"317px",content:GetFile("/cengageecommerce.aspx"),modal:true,draggable:true,resizable:true});t.data("kendoWindow").center();$("#loginWindow").closest(".k-window").css({position:"fixed",margin:"auto"});t.data("kendoWindow").open();if($.browser.msie){$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}else if(navigator.userAgent.match(/AppleWebKit/)&&!navigator.userAgent.match(/Chrome/)){$("#loginWindow").css({height:"361px","margin-left":"-9px",width:"507px"});$("#loginWindow").parent().css("height","317px")}else{$("#loginWindow").css({height:"362px","margin-left":"-9px",width:"509px"});$("#loginWindow").parent().css("height","317px")}$("a.k-window-action.k-link").mouseover(function(){$("a.k-window-action.k-link").parent().css("background-image","url('./Portals/0/images/close.png') !important");return false})}var domainName;if(window.location.host.indexOf("cengagelearning.com.au")>-1){domainName="cengage.com.au"}else if(window.location.host.indexOf("cengagelearning.co.nz")>-1){domainName="cengage.co.nz"}var _gaq=_gaq||[];_gaq.push(["_setAccount","UA-43278709-1"]);_gaq.push(["_setDomainName",domainName]);_gaq.push(["_setAllowLinker",true]);_gaq.push(["_trackPageview"]);(function(){var e=document.createElement("script");e.type="text/javascript";e.async=true;e.src=("https:"==document.location.protocol?"https://ssl":"http://www")+".google-analytics.com/ga.js";var t=document.getElementsByTagName("script")[0];t.parentNode.insertBefore(e,t)})();$(window).resize(function(){var e=$(".k-window-content");e.each(function(){e.data("kendoWindow").center()})});$("#msginfoclosediv").click(function(){$("#LoginMessage").hide()});if($.browser.msie){$(".advSrchbtn_Top").css("background-color","white")}if(navigator.userAgent.match(/iPad/i)!=null){$(".bannerimg").css("width","100%")}else{$(".bannerimg").css("width","1600px")}if(navigator.userAgent.indexOf("Mac")>-1){$.browser.safari=$.browser.webkit&&!/chrome/.test(navigator.userAgent.toLowerCase());if($.browser.safari){$(".footer_bottom_left").css("margin-left","-6px");$(".footer_bottom_right").css("margin-right","27px")}if(navigator.userAgent.indexOf("Chrome")!=-1){$(".footer_bottom_left").css("margin-left","-8px");$(".footer_bottom_right").css("margin-right","30px")}if($.browser.mozilla){$(".footer_bottom_right").css("margin-right","28px")}}$("#tabstrip").kendoTabStrip({animation:{open:{effects:"fadeIn"}},select:function(e){if($.browser.msie){if($(e.item).index()=="0"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="1"){$("#ie_literacydiv").removeClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="2"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").removeClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="3"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").removeClass("tabshadow");$("#ie_digitaldiv").addClass("tabshadow")}else if($(e.item).index()=="4"){$("#ie_literacydiv").addClass("tabshadow");$("#ie_numaracydiv").addClass("tabshadow");$("#ie_workbooksdiv").addClass("tabshadow");$("#ie_digitaldiv").removeClass("tabshadow")}}}});$(document).ready(function(){$("#advancedsrchTopdiv").click(function(){$("#advancedsrchdialog").css("display","block");$(".tabs").css("z-index","0");$("#advancedsrchpop").attr("src",GetFile("/AdvancedSearch.aspx"));if($("#imgadvSrc").attr("src").indexOf("av_search_sel")==-1){$("#imgadvSrc").attr("src",$("#imgadvSrc").attr("src").replace("av_search","av_search_sel"));$("#advancedsrchTopdiv").removeClass("advSrchbtn_Top");$("#advancedsrchTopdiv").addClass("advSrchbtn_TopSel")}else{$("#advancedsrchdialog").css("display","none");$(".tabs").css("z-index","5");$("#imgadvSrc").attr("src",$("#imgadvSrc").attr("src").replace("av_search_sel","av_search"));$("#advancedsrchTopdiv").removeClass("advSrchbtn_TopSel");$("#advancedsrchTopdiv").addClass("advSrchbtn_Top")}});$("body").click(function(e){if(e!=undefined&&e.target!=undefined&&$(e.target.parentElement).attr("id")!="advancedsrchTopdiv"){$("#advancedsrchdialog").css("display","none");$(".tabs").css("z-index","5");$("#imgadvSrc").attr("src",$("#imgadvSrc").attr("src").replace("av_search_sel","av_search"));$("#advancedsrchTopdiv").removeClass("advSrchbtn_TopSel");$("#advancedsrchTopdiv").addClass("advSrchbtn_Top")}})});var _totwidth=$("#Mainmenu").width();var _licount=$("#Mainmenu ul").children().length;var _splitwidth=_totwidth/_licount;var _marginwidth;var _marginwidthtbs;if(_licount<=4){_marginwidth=_licount*15;_marginwidthtbs=_marginwidth+2}else{_marginwidth=_licount*4;_marginwidthtbs=_marginwidth+8}var _liwidth=parseFloat(_splitwidth.toString()).toFixed(2);var _liwidthref=parseFloat(_splitwidth.toString()).toFixed(2)-2;if(navigator.userAgent.indexOf("Safari")!=-1&&navigator.userAgent.indexOf("Chrome")==-1){$("#tabstrip li").css("top","-12px");$("#tabstrip .k-tabstrip-items .k-first").css("top","-13px!important")}else{$("#tabstrip li").css("top","-14px")}$(".icons_staff").css("margin-left",_marginwidthtbs);$(".icons_abt").css("margin-left",_marginwidthtbs);var href=jQuery(location).attr("href");var currentpagename=href.substring(href.lastIndexOf("/")+1);if(currentpagename.toUpperCase()=="CENGAGEECOMMERCE.ASPX"){$("#loginlnk").addClass("loginlnkactive");$("#userimg").addClass("useractive")}else if(currentpagename.toUpperCase()=="SIGNUP.ASPX"){$("#SignUp").addClass("signuplnkactive")}else if(currentpagename.toUpperCase()=="HELP.ASPX"){$("[id$=lblbannertxt]").text("HELP")}if(currentpagename.toUpperCase()=="LIST.ASPX"){$("#wishlistlnk").addClass("util_rhtwishlistactive");$("#wishlistimg").css("background",'url("portals/0/images/star_blue.png") no-repeat scroll 0 0 transparent');if($("#WishList").text().length==0){$("#wishlistlnk").width("99px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent')}else{$("#wishlistlnk").width("124px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent')}}if(currentpagename.toUpperCase().indexOf("LIST.ASPX")>=0){if(currentpagename.toUpperCase()=="LIST.ASPX?ITEM=CART"){$("#cartlnk").addClass("util_rhtcartlnkactive");$("#cartimg").css("background",'url("portals/0/images/cart_blue.png") no-repeat scroll 0 0 transparent');if($("#Cart").text().length==0){$("#cartlnk").css("background",'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent');$("#cartlnk").width("100px").addClass("util_rhtcartlnkactive")}else{$("#cartlnk").css("background",'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent');$("#cartlnk").width("124px").addClass("util_rhtcartlnkactive")}}else{$("#wishlistlnk").addClass("util_rhtwishlistactive");$("#wishlistimg").css("background",'url("portals/0/images/star_blue.png") no-repeat scroll 0 0 transparent');if($("#WishList").text().length==0){$("#wishlistlnk").width("99px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("portals/0/images/cart_btn_blue.png") no-repeat scroll 0 0 transparent')}else{$("#wishlistlnk").width("124px!important").addClass("util_rhtwishlistactive");$("#wishlistlnk").css("background",'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent')}}}