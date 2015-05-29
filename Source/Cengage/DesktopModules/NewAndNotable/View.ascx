<%@ Control language="C#" Inherits="DotNetNuke.Modules.NewAndNotable.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<script src="/Resources/Shared/scripts/kendoui/latest/kendo.web.min.js"
    type="text/javascript"></script>
<script type="text/javascript">
function NNGetQueryStringParams(d) {
	var b = window.location.search.substring(1);
	var c = b.split("&");
	for (var a = 0; a < c.length; a++) {
		var f = c[a].split("=");
		if (f[0] == d) {
			return f[1]
		}
	}
}
function jScript() {

$(function () {   

    /* Product Search Result - Availability check for Authors, Audience code, Training package code, qualification code and competence code, if not hide */
    $('.AvailabilityCheck').each(function () {
        if ($(this).text().toLowerCase().trim() != '') {
            $(this).parent().addClass('ShowItems');
        }
        else {
            $(this).parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for Authors, Audience code, Training package code, qualification code and competence code -end */

    /* Product Search Result - Availability check for Print Price and eBook Price, if not hide*/
    $('.AvailabilityCheck1').each(function () {
        if ($(this).text().toLowerCase().trim() != '') {
            $(this).parent().parent().addClass('ShowItems');
        }
        else {
            $(this).parent().parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for Print Price and eBook Price -end */

    /* Product Search Result - Availability check for New flag, if not hide else set text as 'New' */
    $('.NewFlag').each(function () {
        if ($(this).text().toLowerCase().trim() == 'y'||$(this).text().toLowerCase().trim() == 'new') {
            $(this).text('New');
        }
        else {
            $(this).parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for New flag -end */

    /* Product Search Result - Availability check for eChapter flag, if not hide else set text as 'Available' */
    $('.eChapterFlag').each(function () {
        if ($(this).text().toLowerCase().trim() == 'y') {
            $(this).text('Available');
        }
        else {
            $(this).parent().parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for New flag -end */

    /* Product Search Result - Sub product Type check, if it is 'EBK' then chnage text as 'eBook from' else 'eBook' */
    $('.eBookCheck').each(function () {
        if ($(this).text().toLowerCase().trim() == 'ebk') {
            $(this).text('eBook From');
        }
        else {
            $(this).text('eBook');
        }
    });
    /* Product Search Result - Sub product Type check, if it is 'EBK' then chnage text as 'eBook from' else 'eBook' -end */

    /* Product Search Result - Availability check for Supplements, if not hide else set text as 'Available' */
    $('.SuppAvl').each(function () {
        if ($(this).text().toLowerCase().trim() == 'y' || $(this).text().toLowerCase().trim() == 'available') {
            $(this).text('available');
        }
        else {
            $(this).parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for New flag -end */

    /* Product Search Result - Check product for favourites, if not OFF state else ON state */

    $('div.favorite').on("mouseover", function () {
        if ($(this).children().next().attr('class') == 'btn btn-onoff btn-on')
            $(this).children().next().val('UNFAVOURITE')
        else { 
		$(this).children().next().val("FAVOURITE")
		}
    });
    $('div.favorite').on("mouseout", function () {
        if ($(this).children().next().attr('class') == 'btn btn-onoff btn-on')
            $(this).children().next().val('FAVOURITE')
        else { 
		//$(this).children().next().val('UNFAVOURITE')
		}
    });

    $('.favspan.HideItems').each(function () {
        if ($(this).text().toLowerCase().trim() == 'y') {
            $(this).parent().children().next().addClass("btn-on");
            $(this).parent().children().first().removeClass('HideItems').addClass("ico-favour-right");
        }
        else {
            $(this).parent().children().first().removeClass("btn-on");
            $(this).parent().children().first().addClass('HideItems').removeClass("ico-favour-right");
        }
    });
    /* Product Search Result - Check product for favourites -end */

ndkwindow2 = $("#NewNotableFavoritePopUp");
ndkwindow4 = $("#NewNotableMoreFavoritePopUp");
	$("#FavLoginBtn1").click(function () {
		ndkwindow2.data("kendoWindow").close();
		$("#loginlnk").click();
		return false
	});
	$("#FavSignUpBtn1").click(function () {
		ndkwindow2.data("kendoWindow").close();
		window.location.href = "/signup";
		return false
	});
    
	$('#NNMoreFavOkBtn').click(function(){
		ndkwindow4.data("kendoWindow").close();
		return false;
	});
});

}

var ndkwindow2, ndkwindow4;
function NNSaveFavorite(b, a) {
ndkwindow2 = $("#NewNotableFavoritePopUp");
ndkwindow4 = $("#NewNotableMoreFavoritePopUp");
var div='';
if(location.href.indexOf('vpg')!=-1){
div="vpg"
}
if(location.href.indexOf('gale')!=-1){
div="gale"
}
if(location.href.indexOf('higher')!=-1){
div="higher"
}

	if ($(a).attr("class") == "btn btn-onoff" || $(a).attr("class") == "btn ipad-btn-onoff") {		
		$.ajax({
			url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=" + b + "&division="+div),
			dataType: "json",
			async: false,
			success: function (c) {
				if (c == -1) {
					$("#NewNotableFavoritePopUp").css({
						display: "block"
					});
					$(".k-window-actions.k-header").css("cursor", "pointer");
					if (!ndkwindow2.data("kendoWindow")) {
						ndkwindow2.kendoWindow({
							modal: true,
							draggable: false
						});
						ndkwindow2.data("kendoWindow").center()
					}
					ndkwindow2.data("kendoWindow").open();
					$(".k-icon.k-i-close").hide();
					$("a.k-window-action.k-link").mouseover(function () {
						return false
					});
					return false
				} else {
					if (c == 50) {
						$("#NewNotableMoreFavoritePopUp").css({
							display: "block"
						});
						$(".k-window-actions.k-header").css("cursor", "pointer");
						if (!ndkwindow4.data("kendoWindow")) {
							ndkwindow4.kendoWindow({
								modal: true,
								draggable: false
							});
							ndkwindow4.data("kendoWindow").center()
						}
						ndkwindow4.data("kendoWindow").open();
						$(".k-icon.k-i-close").hide();
						$("a.k-window-action.k-link").mouseover(function () {
							return false
						});
						return false
					} else {												
						if(navigator.userAgent.match(/iPad/i) != null)
						{							
							$("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
							$(a).addClass("btn-onoff").addClass("ipad-btn-on").val("FAVOURITE");
						}
						else
						{
							$("#FavSpan" + b).removeClass("HideItems").addClass("ico-favour-right");
							$(a).addClass("btn-on").val("UNFAVOURITE")						
						}
					}
				}
			}
		})
	} else {
		$.ajax({
			url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=" + b + "&division=" + NNGetQueryStringParams("division")),
			dataType: "json",
			async: false,
			success: function (c) {				
				if(navigator.userAgent.match(/iPad/i) != null)
				{					
					$(a).addClass('ipad-btn-onoff').removeClass("btn-onoff").removeClass("btn-on").removeClass("ipad-btn-on").val("FAVOURITE");	
					$("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right-ipad").removeClass("ico-favour-right");
				}
				else
				{
					$(a).removeClass("btn-on").val("FAVOURITE");
					$(a).blur();
					$("#FavSpan" + b).addClass("HideItems").removeClass("ico-favour-right");					
				}
			}
		})
	}
}


</script>
<asp:UpdatePanel ID ="newUpdatePanel" runat ="server">
<ContentTemplate>
<script type="text/javascript" language="javascript">
    Sys.Application.add_load(jScript);
 </script>
    <div class="new_fav" runat ="server" id ="divNew">
	<div class="new-notable-nav">
		<div class="title">
			<span class="he_module_title" runat="server" id="spanTitle">New and Notable</span>
			<div class="see-btn" id ="divNewSeeAll" runat ="server">
				<input type="button" id ="btnNewSeeAll" class="seeAllBtn" value = "See all" runat ="server" onserverclick ="btnNewSeeAll_Click"/>
			</div>
            <div class="see-btn" id ="divNewHideAll" runat ="server">
				<input type="button" id ="btnHideAll" class="seeAllBtn" value = "Hide all" runat ="server" onserverclick ="btnNewHideAll_Click"/>
			</div>
		</div>    
		<asp:Repeater ID="newProductResultsRptr" runat="server" >
			<ItemTemplate>
				<div class="fav-product">
					
					<div class="product_name">
						<asp:HyperLink ID="ProductTitleLink" CssClass="linktextnodec" runat="server" NavigateUrl='<%# Eval("DetailUrl") %>'>
							<h2 title='<%# Eval("ToolTip")%>'>
							<%# Eval("TITLE")%></h2></asp:HyperLink>
					</div>
					<div class="img">
						<asp:HyperLink ID="ProductImageLink" runat="server" ToolTip='<%# Eval("ToolTip")%>' CssClass="linktextnodec" NavigateUrl='<%# Eval("DetailUrl") %>'>
                                            <img alt='<%# Eval("ToolTip")%>' src='<%# Eval("IMAGE_FILE_NAME") %>' class="avatar" onError="this.onerror=null;this.src='<%# string.Concat(FormatImageURL(),"noimage.png") %>';" />
						</asp:HyperLink>
					</div>
			
					<div class="favcnt">
						<div class="cont">
							<div class="country">
								<span class="AvailabilityCheck">
									<%# Eval("AUDIENCE_TARGET")%></span>
							</div>
							<div class="version">
								<span class="NewFlag">
									<%# Eval("NEW_EDITION")%></span>
							</div>
							<div class=" publisher">
								<strong>Published: </strong>
								   <small> <%# Eval("PUBLICATION_DATE")%></small>
									| <strong>ISBN </strong>
								  <small>  <%# Eval("ISBN_13")%></small>
									| <strong>Edition </strong>
									<small><%# Eval("EDITION")%></small></div>
							<div class="author">
								<strong>Author/s:</strong><span class="AvailabilityCheck"><%# Eval("PREFERRED_NAME")%></span></div>
							<div class="supplements ">
								<span class="ico-supplements"></span><strong>Supplements &nbsp;</strong> <span class="SuppAvl">
									<%# Eval("SUPPLEMENTS")%></span>
							</div>
						</div>
						<div class="favorite">
						 <span id='FavSpan<%# Eval("PRODUCT_SK") %>' class='HideItems'></span> 
							<input type="button" class="btn btn-onoff" value="FAVOURITE" onclick="NNSaveFavorite('<%# Eval("PRODUCT_SK") %>',this)" />
							<span class="favspan HideItems">
								<%# Eval("FAVOURITE_FLAG") %></span> <span class="HideItems">
									<%# Eval("PRODUCT_SK")%></span>
						</div>
					</div>
				</div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<div id="NewNotableFavoritePopUp" class="HideItems save-search-result">
    <div class="favpopup">
        <h1>
            Mark as a favourite</h1>
        <div class="fav-txt">
            <p>
                To mark a title as favourite you must first be logged into your user account.</p>
            <p>
                Creating an account is free and easy.</p>
        </div>
        <div class="button">
            <button class="btn btn-affermative" type="button" id="FavLoginBtn1">
                Login</button>
            <span>OR</span>
            <button class="btn btn-affermative" type="button" id="FavSignUpBtn1">
                Sign up</button>
        </div>
    </div>
</div>
<div id="NewNotableMoreFavoritePopUp" class="HideItems save-search-result">
    <div class="fav-limit-reached">
        <h1>
            Mark as a favourite</h1>
        <div class="fav-txt">
            <div class="txt">
                <p>
                    You already have marked 50 products as favourites.</p>
            </div>
        </div>
        <div class="signup">
            <button class="btn btn-affermative" type="button" id="NNMoreFavOkBtn">
                Ok</button>
        </div>
    </div>
</div>
<script src="DesktopModules/NewAndNotable/Scripts/NewAndNotable.js" type="text/javascript"></script>
