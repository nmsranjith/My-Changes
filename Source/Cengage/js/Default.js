$('.menu-dropdownmenu').live("mouseover",function(){
$(this).addClass('open');
if($('#dropdown_submenu')[0]!=undefined){
$('#dropdown_submenu').show();
}
});

$('.menu-dropdownmenu').live("mouseout",function(){
$(this).removeClass('open');
if($('#dropdown_submenu')[0]!=undefined){
$('#dropdown_submenu').hide();
}
});

 var count=0;
 var count1=0;
function OpenUpdateProgeress(){if($("#dropdown_submenu")[0]!=undefined){if($("#dropdown_submenu")[0].children[0].children[0].innerHTML=="MY DASHBOARD"){$("#updateProgress1").show()}}}function checkKeycode(e){var t;if(window.event){t=window.event.keyCode}else{if(e){t=e.which}}if(t==116){isClose=true}}function somefunction(){isClose=true}function doUnload(){if(!isClose){httpGet(GetFile("/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=userdetail"))}}function httpGet(e){var t=null;t=new XMLHttpRequest;t.open("GET",e,false);t.send(null);return t.responseText}function GetQueryStringParamsls(e){var t=window.location.search.substring(1);var n=t.split("&");for(var r=0;r<n.length;r++){var i=n[r].split("=");if(i[0]==e){return i[1]}}}function SetFooter(){if($("body").height()<$(window).height()){var e=$(window).height()-$("body").height();var t=$(".content_wrapper:eq(0)").height()+e;$(".content_wrapper:eq(0)").css("3",t)}}function onTextSearchtopAllSelect(e){var t=this.dataItem(e.item.index());$("#TextSearch").val(t)}function clickfocusevent(e){var t=e.which?e.which:e.keyCode;if(t==13){e.preventDefault();searchHandlerCall("");return true}}function SearchBtn_Click(){searchHandlerCall("")}function searchHandlerCall(e){var t=$("#TextSearch").val(),n="",r=$("#DivisionHiddenField").val().toLowerCase();$("#TextSearch").val(t);if(t!=""&&t!="Enter your search here..."){n="/search?q="+encodeURIComponent(t)+"&division="+r.toLowerCase()}else{n="/search?q=&division="+r.toLowerCase()}if(e=="advance"){n=n+"&ao=t"}else{}window.location.href=n}function _setCookie(e,t){document.cookie=e+"="+t+"; path=/"}function _getCookie(e){var t=e+"=";var n=document.cookie.split(";");for(var r=0;r<n.length;r++){var i=n[r];while(i.charAt(0)==" ")i=i.substring(1,i.length);if(i.indexOf(t)==0)return i.substring(t.length,i.length)}return null}function GetFile(e){var t=window.location.pathname;var n=t.split("/");var r=location.protocol+"//"+window.location.host+"/"+n[0];var i=r+e;return i}function OpenAsianCountryWindow(){$(".ddlcountry").click()}var drpmenuisclicked=true;var clickedObject;var isClose=false;document.onkeydown=checkKeycode;$(document).ready(function(){$("#msginfoclosediv").click(function(){$("#LoginMessage").hide()});$("#dnn_cp_RibbonBar_ControlPanel").prepend('<ul class="dnnadminmega dnnRight" style="padding-left: 20px"><li class="root"><a href="/Logoff.aspx">Log Out</a></li></ul>')});var ua=navigator.userAgent.toLowerCase();var isWinXP=ua.indexOf("windows nt 5.1")>0;if(isWinXP){$("html").addClass("win-xp")}$(window).bind("beforeunload",function(e){if(window.event.clientX<0||window.event.clientY<0){httpGet(GetFile("/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=userdetail"))}});var os;(function(e){var t="Unbekannt";var n="";if(screen.width){width=screen.width?screen.width:"";height=screen.height?screen.height:"";n+=""+width+" x "+height}var r=navigator.appVersion;var i=navigator.userAgent;var s=navigator.appName;var o=""+parseFloat(navigator.appVersion);var u=parseInt(navigator.appVersion,10);var a,f,l;if((f=i.indexOf("Opera"))!=-1){s="Opera";o=i.substring(f+6);if((f=i.indexOf("Version"))!=-1){o=i.substring(f+8)}}else{if((f=i.indexOf("MSIE"))!=-1){s="Microsoft Internet Explorer";o=i.substring(f+5)}else{if((f=i.indexOf("Chrome"))!=-1){s="Chrome";o=i.substring(f+7)}else{if((f=i.indexOf("Safari"))!=-1){s="Safari";o=i.substring(f+7);if((f=i.indexOf("Version"))!=-1){o=i.substring(f+8)}}else{if((f=i.indexOf("Firefox"))!=-1){s="Firefox";o=i.substring(f+8)}else{if((a=i.lastIndexOf(" ")+1)<(f=i.lastIndexOf("/"))){s=i.substring(a,f);o=i.substring(f+1);if(s.toLowerCase()==s.toUpperCase()){s=navigator.appName}}}}}}}if((l=o.indexOf(";"))!=-1){o=o.substring(0,l)}if((l=o.indexOf(" "))!=-1){o=o.substring(0,l)}u=parseInt(""+o,10);if(isNaN(u)){o=""+parseFloat(navigator.appVersion);u=parseInt(navigator.appVersion,10)}var c=/Mobile|mini|Fennec|Android|iP(ad|od|hone)/.test(r);var h=navigator.cookieEnabled?true:false;if(typeof navigator.cookieEnabled=="undefined"&&!h){document.cookie="testcookie";h=document.cookie.indexOf("testcookie")!=-1?true:false}var p=t;var d=[{s:"Windows 3.11",r:/Win16/},{s:"Windows 95",r:/(Windows 95|Win95|Windows_95)/},{s:"Windows ME",r:/(Win 9x 4.90|Windows ME)/},{s:"Windows 98",r:/(Windows 98|Win98)/},{s:"Windows CE",r:/Windows CE/},{s:"Windows 2000",r:/(Windows NT 5.0|Windows 2000)/},{s:"Windows XP",r:/(Windows NT 5.1|Windows XP)/},{s:"Windows Server 2003",r:/Windows NT 5.2/},{s:"Windows Vista",r:/Windows NT 6.0/},{s:"Windows 7",r:/(Windows 7|Windows NT 6.1)/},{s:"Windows 8.1",r:/(Windows 8.1|Windows NT 6.3)/},{s:"Windows 8",r:/(Windows 8|Windows NT 6.2)/},{s:"Windows NT 4.0",r:/(Windows NT 4.0|WinNT4.0|WinNT|Windows NT)/},{s:"Windows ME",r:/Windows ME/},{s:"Android",r:/Android/},{s:"Open BSD",r:/OpenBSD/},{s:"Sun OS",r:/SunOS/},{s:"Linux",r:/(Linux|X11)/},{s:"iOS",r:/(iPhone|iPad|iPod)/},{s:"Mac OS X",r:/Mac OS X/},{s:"Mac OS",r:/(MacPPC|MacIntel|Mac_PowerPC|Macintosh)/},{s:"QNX",r:/QNX/},{s:"UNIX",r:/UNIX/},{s:"BeOS",r:/BeOS/},{s:"OS/2",r:/OS\/2/},{s:"Search Bot",r:/(nuhk|Googlebot|Yammybot|Openbot|Slurp|MSNBot|Ask Jeeves\/Teoma|ia_archiver)/}];for(var v in d){var m=d[v];if(m.r.test(i)){p=m.s;break}}var g=t;if(/Windows/.test(p)){g=/Windows (.*)/.exec(p)[1];p="Windows"}switch(p){case"Mac OS X":g=/Mac OS X (10[\.\_\d]+)/.exec(i)[1];break;case"Android":g=/Android ([\.\_\d]+)/.exec(i)[1];break;case"iOS":g=/OS (\d+)_(\d+)_?(\d+)?/.exec(r);g=g[1]+"."+g[2]+"."+(g[3]|0);break}e.jscd={screen:n,browser:s,browserVersion:o,mobile:c,os:p,osVersion:g,cookies:h};if(Math.floor(jscd.browserVersion)==6){$("html").addClass("safari mobile ipad ios6")}})(this);$(function(){$(".scrolltotop").click(function(e){e.preventDefault();var t=$(window).scrollTop();$("html, body").animate({scrollTop:"0px"},500)});SetFooter();if($.fn.placeholder!==undefined){$("input,textarea").placeholder()}$(".customscrollbar").mCustomScrollbar({theme:"dark-thick",scrollButtons:{enable:false,advanced:{updateOnBrowserResize:true,updateOnContentResize:true}}});$(".accordion-toggle").on("click",function(){var e=window.navigator.userAgent;var t=e.indexOf("MSIE ");if(t>0||!!navigator.userAgent.match(/Trident.*rv\:11\./)){var n=false;if($(this).parent().next().css("display")=="block"){$(this).parent().next().css("display","none")}else{n=true;$(this).parent().next().css("display","block")}}var r=$(this);if($(this).parent().next().css("display")=="none"||n){if($(this).parents().eq(2).attr("id")=="FacetFilterDiv"){if(r.parent().next().find(".mCSB_container").children().length>4){setTimeout(function(){$(".customscrollbar").each(function(){$(this).mCustomScrollbar("update")})},100)}}else{setTimeout(function(){$(".customscrollbar").each(function(){$(this).mCustomScrollbar("update")})},100)}}})});var isAdv=false;$(document).ready(function(){if($("#TextSearch")[0]!=undefined){var e=$("#TextSearch");$.ui.autocomplete.prototype._renderItem=function(e,t){var n=new RegExp($.trim(this.term.toLowerCase())),r="";if(t.label!="Advanced Search"){r=t.label.replace(n,"<span class='ui-autocomplete-term'>"+$.trim(this.term.toLowerCase())+"</span>")}else{r='<span class="ico-advancesearch "></span>Advanced Search'}return $("<li></li>").data("item.autocomplete",t).append("<a class='listItems'>"+r+"</a>").appendTo(e)};e.autocomplete({source:function(e,t){$.ajax({type:"GET",contentType:"application/json; charset=utf-8",url:"./controls/SearchHandlerModified/SearchHandlerModified.ashx?ddlDivisionValue="+$("#DivisionHiddenField").val()+"&searchtxt="+encodeURIComponent($("#TextSearch").val()),data:"{}",dataType:"json",success:function(e){t(e)},error:function(e){}})},select:function(e,t){if(t.item.value=="Advanced Search"){$(this).val($("#TextSearch").val());searchHandlerCall("advance");return false}else{$(this).val(t.item.value);searchHandlerCall("")}},focus:function(e,t){if(t.item.value=="Advanced Search"){return false}else{}},open:function(e,t){$(".ui-autocomplete").children().last().find("*").addClass("lastItemList")}})}var t=navigator.cookieEnabled?true:false;if(typeof navigator.cookieEnabled=="undefined"&&!t){document.cookie="testcookie";t=document.cookie.indexOf("testcookie")!=-1?true:false}if(t){var n;if($("#DivisionHiddenField")[0]!=undefined){if($("#DivisionHiddenField").val().indexOf("primary")==0)n="Recently Browsed Series";else if($("#DivisionHiddenField").val().indexOf("secondary")==0)n="Recently Browsed Subjects";else if($("#DivisionHiddenField").val().indexOf("gale")==0)n="Recently Browsed Product Types";else n="Recently Browsed Disciplines";var r=$("#DivisionHiddenField").val();var i=_getCookie(r);if(i!=null){i=decodeURIComponent(i);i=i.trim().split("|");if(i.length>0){$("#DropDownBrowseMenu").append('<li class="head">'+n+"</li>");for(var s=0;s<i.length;s++){var o=i[s].trim().split("~");if(o.length>1){var u=o[0];var a=o[1];$("#DropDownBrowseMenu").append("<li><a href="+a+">"+u+"</a> </li>")}}}}}}$("#BrowseDropDown ul li a").click(function(){var e=$("#DivisionHiddenField").val();var n=new String(this.innerHTML);n=String(n);var r=this.href;if(t){var i=[];i=_getCookie(e);var s=[];var o,u,a="";if(i==null){o=a.concat(n.trim(),"~",r.trim(),"|");a="";_setCookie(e,encodeURIComponent(o.trim()))}else{i=decodeURIComponent(i);var f=i;i=i.trim().split("|");o=a.concat(n.trim(),"~",r.trim(),"|");a="";u=a.concat(n.trim(),"~",r.trim());a="";var l=false;try{l=i.indexOf(u)==-1}catch(c){l=true;for(var h=0;h<i.length;h++){if(i[h]==u){l=false}}}if(l){if(i.length>3){s.push(o);s.push(i[0]);s.push(i[1]);s.push(i[2]);s.pop();o=a.concat(s[0],s[1],"|",s[2],"|")}else{o=a.concat(o.trim(),f.trim())}}else{return}_setCookie(e,encodeURIComponent(o.trim()))}}});$("#SelectedMenu").attr("href","#"); if($("#SelectedMenu").length>0){ if($("#SelectedMenu").next().children().length ==0 && $("#SelectedMenu").next().hasClass('dropdown-menu')){$("#SelectedMenu").next().css('border','none'); }} $("#cartlnk").hover(function(){$("span#Cart").addClass("hvrHighLight")},function(){$("span#Cart").removeClass("hvrHighLight")});$("#CountryCancel").click(function(){window.data("kendoWindow").close()});$("#loginlnk").attr("href","#");$("#CountryName").attr("href","#");$("#Cancel").click(function(){var e=self.parent.$("#RemoveCart");e.data("kendoWindow").close()});var f=$("#DivisionHiddenField").val();if(f!=undefined){switch(f.toLowerCase()){case"primary":$("#dnn_CENGAGESUBMENU_PrimaryLink").addClass("current-menu-parent");break;case"secondary":$("#dnn_CENGAGESUBMENU_SecondaryLink").addClass("current-menu-parent");break;case"vocational":$("#dnn_CENGAGESUBMENU_VocationalLink").addClass("current-menu-parent");break;case"gale":$("#dnn_CENGAGESUBMENU_GaleLink").addClass("current-menu-parent");break;default:$("#dnn_CENGAGESUBMENU_HigherEducation").addClass("current-menu-parent")}}});$(window).load(function(){(function(e){if(typeof define==="function"&&define.amd){define(["jquery"],e)}else{e(jQuery)}})(function(e){function t(t,s){function o(){f.text=f.$cont.text();f.opts.ellipLineClass=f.opts.ellipClass+"-line";f.$el=e('<span class="'+f.opts.ellipClass+'" />');f.$el.text(f.text);f.$cont.empty().append(f.$el);u()}function u(){if(typeof f.opts.lines==="number"&&f.opts.lines<2){f.$el.addClass(f.opts.ellipLineClass);return}g=f.$cont.height();if(f.opts.lines==="auto"&&f.$el.prop("scrollHeight")<=g){return}if(!h){return}y=e.trim(f.text).split(/\s+/);f.$el.html(r+y.join("</span> "+r)+"</span>");f.$el.find("span").each(h);if(p!=null){a(p)}}function a(e){y[e]='<span class="'+f.opts.ellipLineClass+'">'+y[e];y.push("</span>");f.$el.html(y.join(" "))}var f=this,l=0,c=[],h,p,d,v,m,g,y;f.$cont=e(t);f.opts=e.extend({},i,s);if(f.opts.lines==="auto"){var b=function(t,n){var r=e(n),i=r.position().top;m=m||r.height();if(i===v){c[l].push(r)}else{v=i;l+=1;c[l]=[r]}if(i+m>g){p=t-c[l-1].length;return false}};h=b}if(typeof f.opts.lines==="number"&&f.opts.lines>1){var w=function(t,n){var r=e(n),i=r.position().top;if(i!==v){v=i;l+=1}if(l===f.opts.lines){p=t;return false}};h=w}if(f.opts.responsive){var E=function(){c=[];l=0;v=null;p=null;f.$el.html(f.text);clearTimeout(d);d=setTimeout(u,100)};e(window).on("resize."+n,E)}o()}var n="ellipsis",r='<span style="white-space: nowrap;">',i={lines:"auto",ellipClass:"ellip",responsive:false};e.fn[n]=function(r){return this.each(function(){try{e(this).data(n,new t(this,r))}catch(i){if(window.console){console.error(n+": "+i)}}})}});$(".auto_ellipse span.divTitle").ellipsis({lines:2});$(".auto_ellipse h2").ellipsis({lines:2})});$(document).ready(function(){function e(){var e=$("#PartnerRequestPopUp");$(".k-window-actions.k-header").css("cursor","pointer");if(!e.data("kendoWindow")){e.kendoWindow({modal:true,draggable:false});e.data("kendoWindow").center()}e.data("kendoWindow").open();$(".k-icon.k-i-close").hide();$(".k-window-action.k-link")[0].style.cssText="margin-right: -29px!important;margin-top: -29px !important;margin-left: 2px !important;vertical-align: top !important;";$("a.k-window-action.k-link").mouseover(function(){return false})}var t=window.devicePixelRatio>1||$(window).width()>=1300||navigator.userAgent.match(/iPad/i)!=null||window.matchMedia&&window.matchMedia("(-webkit-min-device-pixel-ratio: 1.5),(-moz-min-device-pixel-ratio: 1.5),(min-device-pixel-ratio: 1.5)").matches;if(t&&$("#dnn_dnnLogo_imgLogo").length>0){var n=$("#dnn_dnnLogo_imgLogo").attr("src");if(n.match(/VPG/g)!=undefined){$("#dnn_dnnLogo_imgLogo").attr("src",$("#dnn_dnnLogo_imgLogo").attr("src").replace($("#dnn_dnnLogo_imgLogo").attr("src").split("/")[3],"VPG-HD.png"))}else if(n.match(/Corp/g)!=undefined){$("#dnn_dnnLogo_imgLogo").attr("src",$("#dnn_dnnLogo_imgLogo").attr("src").replace($("#dnn_dnnLogo_imgLogo").attr("src").split("/")[3],"Corp-HD.png"))}else if(n.match(/Gale/g)!=undefined){$("#dnn_dnnLogo_imgLogo").attr("src",$("#dnn_dnnLogo_imgLogo").attr("src").replace($("#dnn_dnnLogo_imgLogo").attr("src").split("/")[3],"Gale-HD.png"))}else if(n.match(/Nelson/g)!=undefined){$("#dnn_dnnLogo_imgLogo").attr("src",$("#dnn_dnnLogo_imgLogo").attr("src").replace($("#dnn_dnnLogo_imgLogo").attr("src").split("/")[3],"Nelson-HD.png"))}}$(".partnerregistration_CMS").live("click",function(){var t;var n=$(this)[0].href;var r=httpGet(GetFile("/DesktopModules/CengageRegistration/Components/Handlers/GetErrorMessage.ashx?request=checkpartner"));if(r==2){e();return false}if(r==0){if($("#loginWindow").parent()[0].style.display=="none"||$("#loginWindow").parent()[0].style.display=="")$("#loginlnk").click();$("#loginWindow")[0].children[0].src=$("#loginWindow")[0].children[0].src+"?p=cms&"+$(this)[0].href.split("?")[1];return false}if(r==1){t=true}if(t){this.href="/signup?e=booksellerpartnersignup"}});$("#partnersignupcancel").click(function(){var e=self.parent.$("#PartnerRequestPopUp");e.data("kendoWindow").close()});var r,i,s=false;var o=false;$("#DropDownBrowseMenu li ul").mouseleave(function(){if($("#DivisionHiddenField").val()=="primary"||$("#DivisionHiddenField").val()=="secondary"){$(this).find(".dropdown-menu").hide();return true}if(o){if(i!=undefined&&$(i).length>0){$(i).addClass("submenuparentactive");$(i).show()}if(r!=undefined&&$(r).length>0)$(r).show()}else{$("#BrowseDropDown").removeClass("open");if(i!=undefined&&$(i).length>0){$(i).removeClass("submenuparentactive");i=undefined}if(r!=undefined&&$(r).length>0){if($(r).attr("style")!=undefined)$(r).attr("style","");r=undefined}}});$("#DropDownBrowseMenu>li").mouseleave(function(){if($("#DivisionHiddenField").val()=="primary"||$("#DivisionHiddenField").val()=="secondary"){if(r!=undefined&&$(r).length>0)$(r).show();return true}else{if(clickedObject!=undefined&&$(this).is($(clickedObject))){if(i!=undefined&&$(i).length>0){$(i).addClass("submenuparentactive");$(i).show()}}if(r!=undefined&&$(r).length>0)$(r).show();return false}});$(".dropdown-submenu").bind("hoverintent",function(e){if($("#DivisionHiddenField").val()=="primary"||$("#DivisionHiddenField").val()=="secondary"){if($(this).is($("a"))){if(r!=undefined&&$(r).length>0)$(r).hide();if($(this).parent().parent().attr("id")==undefined)r=$(this).parent().parent().show();else r=$(this).parent().find(".dropdown-menu").show()}drpmenuisclicked=true}else{if($(this).parent().attr("id")=="DropDownBrowseMenu"){clickedObject=undefined;if(r!=undefined&&$(r).length>0)$(r).hide();r=$(this).find(".dropdown-menu").show()}}});$("#DropDownBrowseMenu>li").click(function(e){drpmenuisclicked=false;if($("#DivisionHiddenField").val()=="primary"||$("#DivisionHiddenField").val()=="secondary")return true;if(s)return true;e.stopPropagation();o=true;clickedObject=$(this);if(i!=undefined&&$(i).length>0)$(i).removeClass("submenuparentactive");i=$(this).addClass("submenuparentactive").show();if(r!=undefined&&$(r).length>0){if($(r).attr("style")!=undefined)$(r).attr("style","")}r=$(this).find(".dropdown-menu").show();return false});if($(".ProdErrorMsg").length>0)$(".he-adsearch input[type=text]")[0].style.marginLeft="-92px";$("#DropDownBrowseMenu li ul li a").click(function(){s=true});$("#DropDownBrowseMenu li a").click(function(){s=true});var u=false;$("#BrowseDropDown").click(function(e){drpmenuisclicked=true;e.stopPropagation();u=true;$("#BrowseDropDown").addClass("open");$("#DropDownBrowseMenu").show()});$("#BrowseDropDown").live("mouseenter",function(){$("#DropDownBrowseMenu").show();$("#BrowseDropDown").addClass("open")});$("#DropDownBrowseMenu").live("mouseleave",function(){drpmenuisclicked=true;if(o){if(i!=undefined&&$(i).length>0){$(i).addClass("submenuparentactive");$(i).show();if(r!=undefined&&$(r).length>0)$(r).show()}o=false;return false}else{if(i!=undefined&&$(i).length>0){$(i).removeClass("submenuparentactive");i=undefined}if(r!=undefined&&$(r).length>0){if($(r).attr("style")!=undefined)$(r).attr("style","");r=undefined}}});$("#BrowseDropDown").live("mouseleave",function(){drpmenuisclicked=true;if(!u){if($("#DropDownBrowseMenu").is(":visible"))$("#DropDownBrowseMenu").show();if(o){if(i!=undefined&&$(i).length>0){$(i).addClass("submenuparentactive");$(i).show();if(r!=undefined&&$(r).length>0)$(r).show()}o=false;return false}else{$("#BrowseDropDown").removeClass("open");$("#DropDownBrowseMenu").hide();if(i!=undefined&&$(i).length>0){$(i).removeClass("submenuparentactive");i=undefined}if(r!=undefined&&$(r).length>0){if($(r).attr("style")!=undefined)$(r).attr("style","");r=undefined}}}else{$("#DropDownBrowseMenu").show();$("#BrowseDropDown").addClass("open")}});$("html").click(function(e){drpmenuisclicked=true;u=false;o=false;$("#BrowseDropDown").removeClass("open");$("#DropDownBrowseMenu").hide();if(i!=undefined&&$(i).length>0){$(i).removeClass("submenuparentactive");i=undefined}if(r!=undefined&&$(r).length>0){if($(r).attr("style")!=undefined)$(r).attr("style","");r=undefined}});$(".dclink").focus(function(){$(this).parent().addClass("sidemenuFormatTabBGcolor")}).focusout(function(){$(this).parent().removeClass("sidemenuFormatTabBGcolor")});$("#FacetFilterDiv .accordion-toggle").focus(function(){$(this).addClass("underlinefocus")}).focusout(function(){$(this).removeClass("underlinefocus")});$("#FacetFilterDiv .cleartxt").focus(function(){$(this).addClass("clearfocus")}).focusout(function(){$(this).removeClass("clearfocus")});$("#collapseTwo ul li input").focus(function(){$(this).parent().parent().addClass("sidemenuFormatTabBGcolor")}).focusout(function(){$(this).parent().parent().removeClass("sidemenuFormatTabBGcolor")});$(".nav1 ul li a").focus(function(){$(".nav1 ul li").removeClass("active");$(this).parent().addClass("active")}).focusout(function(){$(".nav1 ul li").removeClass("active")});$(".adSearch").focus(function(){$(this).parent().addClass("tab3active")}).focusout(function(){$(this).parent().removeClass("tab3active")});$(".product_name .linktextnodec").focus(function(){$(this).find("span").addClass("producttitle-focus")}).focusout(function(){$(this).find("span").removeClass("producttitle-focus")});$(".img .linktextnodec").focus(function(){$(this).find("img").addClass("borderImg")}).focusout(function(){$(this).find("img").removeClass("borderImg")})});var domainName;var _gaq=_gaq||[];if(window.location.host.indexOf("com.au")>-1){domainName="cengagelearning.com.au";_gaq.push(["_setAccount","UA-46157015-1"])}else{if(window.location.host.indexOf("co.nz")>-1){domainName="cengagelearning.co.nz";_gaq.push(["_setAccount","UA-46157015-2"])}}_gaq.push(["_setDomainName",domainName]);_gaq.push(["_setAllowLinker",true]);$(window).bind("load",function(){if($("#PriceCurrencyMetatag")[0]==undefined){_gaq.push(["_trackPageview"])}});(function(){var e=document.createElement("script");e.type="text/javascript";e.async=true;e.src=("https:"==document.location.protocol?"https://":"http://")+"stats.g.doubleclick.net/dc.js";var t=document.getElementsByTagName("script")[0];t.parentNode.insertBefore(e,t)})();$(document).ready(function(){ if($(".nivoSlider").length>0){if($(".nivoSlider").css('width')=='940px')$(".nivoSlider").css('margin-left','10px')} $("a").mouseup(function(){href=$(this).attr("href");if(href!=undefined&&href!==null){href_lower=href.toLowerCase();if(href_lower.substr(-3)=="pdf"||href_lower.substr(-3)=="xls"||href_lower.substr(-3)=="doc"||href_lower.substr(-3)=="mp3"||href_lower.substr(-3)=="mp4"||href_lower.substr(-3)=="flv"||href_lower.substr(-3)=="txt"||href_lower.substr(-3)=="csv"||href_lower.substr(-3)=="zip"){_gaq.push(["_trackEvent","Downloads",href_lower.substr(-3),href])}if(href_lower.substr(-4)=="xlsx"||href_lower.substr(-4)=="docx"||href_lower.substr(-4)=="pptx"){_gaq.push(["_trackEvent","Downloads",href_lower.substr(-4),href])}if(href_lower.substr(0,4)=="http"){var e=document.domain.replace("www.","");if(href_lower.indexOf(e)==-1){href=href.replace("http://","");href=href.replace("https://","");_gaq.push(["_trackEvent","Outbound Traffic","Click",href])}}else{if(href_lower.indexOf("mailto:")!==-1){$(this).click(function(){var e=href.replace(/^mailto\:/i,"");_gaq.push(["_trackEvent","Email","Click",e])})}}}});if($("#_userDomainForGA")[0]!=undefined){if($("#_userDomainForGA").val()!=""){_gaq.push(["_setCustomVar",1,"LoggedIn",$("#_userDomainForGA").val(),2])}}var e=document.location.toString().toLowerCase();if(e.indexOf("type=auto")>-1&&$("#TrialUserSkHiddenField").val()==0){OpenLoginWindow("")}});jQuery.event.special.hoverintent={pxDelta:10,pxRate:200,bindType:"mouseover",delegateType:"mouseover",handle:function(e){function t(){i.off("mousemove",n).off("mouseout",t);clearTimeout(l)}function n(e){o=e.pageX;u=e.pageY}var r=Array.prototype.slice.call(arguments,0),i=jQuery(e.target),s=jQuery.event.special.hoverintent,o,u,a,f,l;i.on("mousemove",n).on("mouseout",t);(function c(){if(Math.abs(a-o)+Math.abs(f-u)<s.pxDelta){if(!drpmenuisclicked)l=setTimeout(c,s.pxRate);t();drpmenuisclicked=true;clickedObject==undefined;e.type="hoverintent";return e.handleObj.handler.apply(e.target,r)}a=o;f=u;l=setTimeout(c,s.pxRate)})()}}

 $('.k-overlay').live('click', function () { 
$(".k-window").each(function(e){
  var shoppopup = $(this)[0].children[1].id;
   var window = $('#' + shoppopup).data("kendoWindow"); if (window) window.close(); 

});});

function AddTransaction(e, t, n, r, i, s) {
    _gaq.push(["_addTrans", e, t, n, r, i, s])
}

function AddItem(e, t, n, r, i, s, o) {
    _gaq.push(["_addItem", e, t, n, r, i, s]);
    if (o){ GAPushTrackPageView("/Goal/purchase-complete/"); 
	_gaq.push(["_trackTrans"]); }
}

function FillOrderDetails() {
    
    if ($("#cartorderdiv").length==0) {
        var e = $(".SubsproductTable table tr").length;
        var t = false;
        var n;
		try {
				n = $("#SubsOrderNumber")[0].innerHTML.split(":")[1].trim();
		}
		catch(e)
		{
			try
			{
			 n = $("#SubsOrderNumber")[0].innerHTML.replace(/[^a-z\d\s]+/gi, "");
			 }
			 catch(e)
			 {
			 n="";
			 }
		}
        var r;
			try
			{
				r  = $("#SubsAllTotal")[0].innerHTML.trim();
			}
			catch(e)
			{
				r=0;
			}
		
        var i;
		try
		{
				i = $("#SubTax")[0].innerHTML.trim();
		}
		catch(e)
		{
				i=0;
		}
        var s = "Free Shipping";
		var o;
		try
		{
				o= $("#CountryName").text();
		}
        catch(e)
		 {
			o="";
		 }
        var u;
		try
		{
				u= $("#_storeName").val();
		}
		catch(e)
		{
			u="";
		}
        var a = 0;
        AddTransaction(n, u, r, i, s, o);
        $(".SubsproductTable table tr").each(function(r) {
            if (a != 0) {
                var i;
				try
				{
					i = $(this).find(".qtycls")[0].innerHTML.trim();
				}
				catch(e)
				{
					i="";
				}
                var s;
				try
				{
					s = $(this).find(".titlcls")[0].innerHTML.trim();
				}
				catch(e)
				{
					s="";
				}
				
                var o;
				try
				{
					o = $(this).find(".isbn").val().trim();
				}
				catch(e)
				{
					o="";
				}
                var u;
				try
				{
					u = $(this).find(".unitRrp").val().trim();
				}
				catch(e)
				{
					u="";
				}
                var f = "Subscription";
                if (r == e - 1) t = true;
                AddItem(n, o, s, f, u, i, t)
            }
            a = 1
        });
        GAPushTrackPageView("/goal/step6/complete/")
    } else {
        var e = $(".CartproductTable table tr").length;
        var t = false;
        var n;
		try
		{
			n = $("#OrderNumber")[0].innerHTML.trim();
		}
		catch(e)
		{
			n="";
		}
        var r;
		try
		{
			r = $("#Total")[0].innerHTML.trim();
		}
		catch(e)
		{
			r="";
		}
        var i;
		try
		{
		   i	= $("#Tax")[0].innerHTML.trim();
		}
		catch(e)
		{
			i="";
		}
        var s;
		try
		{
			s = $("#Shipping")[0].innerHTML.trim();
		}
		catch(e)
		{
		    s ="";
		}
        var o;
		try
		{
			o = $("#CountryName")[0].innerText.trim();
		}
		catch(e)
		{
			o="";
		}
        var u;
		try
		{
			u = $("#_storeName").val();
		}
		catch(e)
		{
		 u="";
		}
        var a = 0;
        AddTransaction(n, u, r, i, s, o);
        $(".CartproductTable table tr").each(function(r) {
            if (a != 0) {
                var i;
				try
				{
					i = $(this).find(".qtycls")[0].innerHTML.trim();
				}
				catch(e)
				{
					i="";
				}
                var s;
				try
				{
					s = $(this).find(".titlecls")[0].innerHTML.trim();
				}
				catch(e)
				{
					s="";
				}
                var o;
				try
				{
				 o = $(this).find(".isbncls")[0].innerHTML.trim();
				}
				catch(e)
				{
					o="";
				}
				try
				{
					if ($(this).find(".isbncls").find("a").length > 0) 
						o = $(this).find(".isbncls").find("a")[0].innerHTML.trim();
				}
				catch(e)
				{
				  o="";
				}
                var u;
				try
				{
					u = $(this).find(".discountRrp").val().trim();
				}
				catch(e)
				{
					u=0;
				}
				try
				{
                if (parseInt(u) == 0) u = $(this).find(".unitRrp").val().trim();
				}
				catch(e)
				{
					u="";
				}
                var f;
				try
				{
					f = $(this).find(".prodauthor").val().trim();
				}
				catch(e)
				{
					f="";
				}
                if (r == e - 1) t = true;
                AddItem(n, o, s, f, u, i, t)
            }
            a = 1
        });
    }
}

function GAPushTrackEvent(e, t, n) {
    _gaq.push(["_trackEvent", e, t, n])
}

function GAPushTrackPageView(e) {
    _gaq.push(["_trackPageview", e])
}

/*

$(window).click("resize",function(){
var footer = $(".footer-all");
    if(footer.length>0)
    {
    var pos = footer.position();
    var height = window.outerHeight;
    height = height - pos.top;
    height = height - footer.height();
    if (height > 0) {
        footer.css({
            'margin-top':(height+20) + 'px'
        });
    }
    }
});
 


$(window).click("live",function(){
var footer = $(".footer-all");
    if(footer.length>0)
    {
    var pos = footer.position();
    var height = window.outerHeight;
    height = height - pos.top;
    height = height - footer.height();
    if (height > 0) {
        footer.css({
            'margin-top':(height-85) + 'px'
        });
    }
    }
});
 
 
$(window).bind("load", function () {
$(".footer-all").removeClass( "footer-fix" );
    var footer = $(".footer-all");
    if(footer.length>0)
    {
    var pos = footer.position();
    var height = window.outerHeight;
    height = height - pos.top;
    height = height - footer.height();
    if (height > 0) {
        footer.css({
            'margin-top':(height-15) + 'px'
        });
    }
    }
});
*/


$(document).ready(function(){
if(navigator.platform == "iPad")
{
var footer = $(".footer-all");
	if(footer.length>0)
	{
    var pos = footer.position();
    var height = $(window).height();
    height = height - pos.top;
    height = height - footer.height();
    if (height > 0) {
        footer.css({
            'margin-top':(height+70) + 'px'
        });
    }
	}
}
});


$(window).bind("orientationchange", function () {
    var footer = $(".footer-all");
	if(footer.length>0)
	{
	
    var pos = footer.position();
    var height = $(window).height();
    height = height - pos.top;
    height = height - footer.height();
    if (height > 0) {
        footer.css({
            'margin-top':(height+70) + 'px'
        });
    }
	}
});




function OpenLoginWindow(e) {

if($('#loginWindow')[0]==undefined){
$('#Form').append("<div id='loginWindow'></div>");
}
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




/*placeholder.js*/
/*! http://mths.be/placeholder v2.0.8 by @mathias */
;(function(window, document, $) {

	// Opera Mini v7 doesn’t support placeholder although its DOM seems to indicate so
	var isOperaMini = Object.prototype.toString.call(window.operamini) == '[object OperaMini]';
	var isInputSupported = 'placeholder' in document.createElement('input') && !isOperaMini;
	var isTextareaSupported = 'placeholder' in document.createElement('textarea') && !isOperaMini;
	var prototype = $.fn;
	var valHooks = $.valHooks;
	var propHooks = $.propHooks;
	var hooks;
	var placeholder;

	if (isInputSupported && isTextareaSupported) {

		placeholder = prototype.placeholder = function() {
			return this;
		};

		placeholder.input = placeholder.textarea = true;

	} else {

		placeholder = prototype.placeholder = function() {
			var $this = this;
			$this
				.filter((isInputSupported ? 'textarea' : ':input') + '[placeholder]')
				.not('.placeholder')
				.bind({
					'focus.placeholder': clearPlaceholder,
					'blur.placeholder': setPlaceholder
				})
				.data('placeholder-enabled', true)
				.trigger('blur.placeholder');
			return $this;
		};

		placeholder.input = isInputSupported;
		placeholder.textarea = isTextareaSupported;

		hooks = {
			'get': function(element) {
				var $element = $(element);

				var $passwordInput = $element.data('placeholder-password');
				if ($passwordInput) {
					return $passwordInput[0].value;
				}

				return $element.data('placeholder-enabled') && $element.hasClass('placeholder') ? '' : element.value;
			},
			'set': function(element, value) {
				var $element = $(element);

				var $passwordInput = $element.data('placeholder-password');
				if ($passwordInput) {
					return $passwordInput[0].value = value;
				}

				if (!$element.data('placeholder-enabled')) {
					return element.value = value;
				}
				if (value == '') {
					element.value = value;
					// Issue #56: Setting the placeholder causes problems if the element continues to have focus.
					if (element != safeActiveElement()) {
						// We can't use `triggerHandler` here because of dummy text/password inputs :(
						setPlaceholder.call(element);
					}
				} else if ($element.hasClass('placeholder')) {
					clearPlaceholder.call(element, true, value) || (element.value = value);
				} else {
					element.value = value;
				}
				// `set` can not return `undefined`; see http://jsapi.info/jquery/1.7.1/val#L2363
				return $element;
			}
		};

		if (!isInputSupported) {
			valHooks.input = hooks;
			propHooks.value = hooks;
		}
		if (!isTextareaSupported) {
			valHooks.textarea = hooks;
			propHooks.value = hooks;
		}

		$(function() {
			// Look for forms
			$(document).delegate('form', 'submit.placeholder', function() {
				// Clear the placeholder values so they don't get submitted
				var $inputs = $('.placeholder', this).each(clearPlaceholder);
				setTimeout(function() {
					$inputs.each(setPlaceholder);
				}, 10);
			});
		});

		// Clear placeholder values upon page reload
		$(window).bind('beforeunload.placeholder', function() {
			$('.placeholder').each(function() {
				this.value = '';
			});
		});

	}

	function args(elem) {
		// Return an object of element attributes
		var newAttrs = {};
		var rinlinejQuery = /^jQuery\d+$/;
		$.each(elem.attributes, function(i, attr) {
			if (attr.specified && !rinlinejQuery.test(attr.name)) {
				newAttrs[attr.name] = attr.value;
			}
		});
		return newAttrs;
	}

	function clearPlaceholder(event, value) {
		var input = this;
		var $input = $(input);
		if (input.value == $input.attr('placeholder') && $input.hasClass('placeholder')) {
			if ($input.data('placeholder-password')) {
				$input = $input.hide().next().show().attr('id', $input.removeAttr('id').data('placeholder-id'));
				// If `clearPlaceholder` was called from `$.valHooks.input.set`
				if (event === true) {
					return $input[0].value = value;
				}
				$input.focus();
			} else {
				input.value = '';
				$input.removeClass('placeholder');
				input == safeActiveElement() && input.select();
			}
		}
	}

	function setPlaceholder() {
		var $replacement;
		var input = this;
		var $input = $(input);
		var id = this.id;
		if (input.value == '') {
			if (input.type == 'password') {
				if (!$input.data('placeholder-textinput')) {
					try {
						$replacement = $input.clone().attr({ 'type': 'text' });
					} catch(e) {
						$replacement = $('<input>').attr($.extend(args(this), { 'type': 'text' }));
					}
					$replacement
						.removeAttr('name')
						.data({
							'placeholder-password': $input,
							'placeholder-id': id
						})
						.bind('focus.placeholder', clearPlaceholder);
					$input
						.data({
							'placeholder-textinput': $replacement,
							'placeholder-id': id
						})
						.before($replacement);
				}
				$input = $input.removeAttr('id').hide().prev().attr('id', id).show();
				// Note: `$input[0] != input` now!
			}
			$input.addClass('placeholder');
			$input[0].value = $input.attr('placeholder');
		} else {
			$input.removeClass('placeholder');
		}
	}

	function safeActiveElement() {
		// Avoid IE9 `document.activeElement` of death
		// https://github.com/mathiasbynens/jquery-placeholder/pull/99
		try {
			return document.activeElement;
		} catch (exception) {}
	}

}(this, document, jQuery));


/*placeholder.js*/
