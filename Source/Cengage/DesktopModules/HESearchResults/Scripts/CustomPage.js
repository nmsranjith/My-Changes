﻿function GetQueryStringParams(e){var t=window.location.search.substring(1);var n=t.split("&");for(var r=0;r<n.length;r++){var i=n[r].split("=");if(i[0]==e){return i[1]}}}$(document).ready(function(){if(GetQueryStringParams("p")!=undefined){var e=Math.ceil($("#hdnProdCount").val()/$("#hdnItemCount").val());$("#PageControl").children("a").each(function(){if($(this).text()==GetQueryStringParams("p")){$(this).addClass("Highlight");if(GetQueryStringParams("p")==e){$("#ShowNextLink").attr("disabled","disabled");$("#NextLink").attr("disabled","disabled");$("#NextLink").removeClass("whover").addClass("Rightovercod");$("#ShowNextLink").removeClass("whoverr").addClass("Rightrovercod")}else{if(1==GetQueryStringParams("p")){$("#PreviousButton").attr("disabled","disabled");$("#ShowPreviousButton").attr("disabled","disabled");$("#PreviousButton").removeClass("whoverll").addClass("Leftlovercod");$("#ShowPreviousButton").removeClass("whoverl").addClass("Leftovercod")}}}else{$(this).removeClass("Highlight");if(GetQueryStringParams("p")!=e){$("#ShowNextLink").removeAttr("disabled");$("#NextLink").removeAttr("disabled");$("#NextLink").removeClass("Rightovercod").addClass("whover");$("#ShowNextLink").removeClass("Rightrovercod").addClass("whoverr")}if(1!=GetQueryStringParams("p")){$("#PreviousButton").removeAttr("disabled");$("#ShowPreviousButton").removeAttr("disabled");$("#PreviousButton").removeClass("Leftlovercod").addClass("whoverll");$("#ShowPreviousButton").removeClass("Leftovercod").addClass("whoverl")}}})}})