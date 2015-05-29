<%@ Control language="C#" Inherits="DotNetNuke.Modules.FavouriteAndRecentlyViewed.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %> 
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/FavouriteAndRecentlyViewed/Scripts/FavouriteItems.js" ForceProvider="DnnFormBottomProvider"/> 

<script type="text/javascript">
var fdkwindow2, fdkwindow4;
function jScript() {
fdkwindow2 = $("#favFavoritePopUp");
fdkwindow4 = $("#favMoreFavoritePopUp");
$(function () {   
	$("#FFavLoginBtn").click(function () {
		fdkwindow2.data("kendoWindow").close();
		$("#loginlnk").click();
		return false
	});
	$("#FFavSignUpBtn").click(function () {
		fdkwindow2.data("kendoWindow").close();
		window.location.href = "/signup";
		return false
	});
	
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
    $('.fav-SuppAvl').each(function () {
        if ($(this).text().toLowerCase().trim() == 'y' || $(this).text().toLowerCase().trim() == 'available') {
            $(this).text('available');
        }
        else {
            $(this).parent().addClass('HideItems');
        }
    });
    /* Product Search Result - Availability check for New flag -end */

    /* Product Search Result - Check product for favourites, if not OFF state else ON state */

    $('div.favorite1').on("mouseover", function () {
        if ($(this).children().next().attr('class') == 'btn btn-onoff btn-on')
            $(this).children().next().val('UNFAVOURITE')
        else { 
		$(this).children().next().val("FAVOURITE")
		}
    });
    $('div.favorite1').on("mouseout", function () {
        if ($(this).children().next().attr('class') == 'btn btn-onoff btn-on')
            $(this).children().next().val('FAVOURITE')
        else { 
		//$(this).children().next().val('UNFAVOURITE')
		}
    });

    $('.favspan1.HideItems').each(function () {
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

    
    
});

}

function FRSaveFavorite(b, a) {
	
	if ($(a).attr("class") == "btn btn-onoff" || $(a).attr("class") == "btn ipad-btn-onoff") {		
		$.ajax({
			url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=favorite&prodId=" + b + "&division=higher"),
			dataType: "json",
			async: false,
			success: function (c) {
				if (c == -1) {
					//$("#favFavoritePopUp").css({
					//	display: "block"
					//});
					$(".k-window-actions.k-header").css("cursor", "pointer");
					if (!fdkwindow2.data("kendoWindow")) {
						fdkwindow2.kendoWindow({
							modal: true,
							draggable: false
						});
						fdkwindow2.data("kendoWindow").center()
					}
					fdkwindow2.data("kendoWindow").open();
					$(".k-icon.k-i-close").hide();
					$("a.k-window-action.k-link").mouseover(function () {
						return false
					});
					return false
				} else {
					if (c == 50) {
						//$("#favMoreFavoritePopUp").css({
						//	display: "block"
						//});
						$(".k-window-actions.k-header").css("cursor", "pointer");
						if (!fdkwindow4.data("kendoWindow")) {
							fdkwindow4.kendoWindow({
								modal: true,
								draggable: false
							});
							fdkwindow4.data("kendoWindow").center()
						}
						fdkwindow4.data("kendoWindow").open();
						$(".k-icon.k-i-close").hide();
						$("a.k-window-action.k-link").mouseover(function () {
							return false
						});
						return false
					} else {
						
						if(navigator.userAgent.match(/iPad/i) != null)
						{							
							$("#FavSpan1" + b).removeClass("HideItems").addClass("ico-favour-right-ipad");
							$(a).addClass("btn-onoff").addClass("ipad-btn-on").val("FAVOURITE");
						}
						else
						{
							$("#FavSpan1" + b).removeClass("HideItems").addClass("ico-favour-right");
							$(a).addClass("btn-on").val("UNFAVOURITE")						
						}
					}
				}
			}
		})
	} else {
		$.ajax({
			url: GetFile("desktopmodules/hesearchresults/handlers/hesearchhandler.ashx?section=removefavorite&prodId=" + b + "&division=higher"),
			dataType: "json",
			async: false,
			success: function (c) {				
				if(navigator.userAgent.match(/iPad/i) != null)
				{					
					$(a).addClass('ipad-btn-onoff').removeClass("btn-onoff").removeClass("btn-on").removeClass("ipad-btn-on").val("FAVOURITE");	
					$("#FavSpan1" + b).addClass("HideItems").removeClass("ico-favour-right-ipad").removeClass("ico-favour-right");
				}
				else
				{
					$(a).removeClass("btn-on").val("FAVOURITE");
					$(a).blur();
					$("#FavSpan1" + b).addClass("HideItems").removeClass("ico-favour-right");					
				}
			}
		})
	}
}

</script>

<asp:UpdatePanel ID ="favUpdatePanel" runat ="server" UpdateMode="Conditional">
<ContentTemplate>
<script type="text/javascript" language="javascript">
    Sys.Application.add_load(jScript);
 </script>
<div class="home_fav" runat ="server" id ="divFavourite">
	<div class="home-favorite-nav">
		<div class="title">
			<span runat="server" id="spanTitle">Favourite & Recently View</span>
			<div class="see-btn" id ="divSeeAll" runat ="server">
				<input type="button" id ="btnFavSeeAll" class="seeAllBtn"  value = "See all" runat ="server" onserverclick ="btnFavSeeAll_Click"/>
			</div>
            <div class="see-btn" id ="divHideAll" runat ="server">
				<input type="button" id ="btnFavHideALL" class="seeAllBtn"  value = "Hide all" runat ="server" onserverclick ="btnFavHideAll_Click"/>
			</div>
		</div>
		<asp:Repeater ID="ProductResultsRptr" runat="server" >
			<ItemTemplate>
			<div class="fav-product">
				
				<div class="product_name">
					<asp:HyperLink ID="ProductTitleLink" CssClass="linktextnodec" runat="server" NavigateUrl='<%# FormatURL(Eval("TITLE").ToString(),Eval("ISBN_13").ToString()) %>'>
						<h2 title='<%# Eval("TITLE")%>'>
						<%# Eval("TITLE")%></h2></asp:HyperLink>
				</div>
				
				<div class="img">
                                        <asp:HyperLink ID="ProductImageLink" runat="server" ToolTip='<%# Eval("TITLE")%>' CssClass="linktextnodec" NavigateUrl='<%# FormatURL(Eval("TITLE").ToString(),Eval("ISBN_13").ToString()) %>'>
                                            <img alt='<%# Eval("TITLE")%>' src='<%# string.Concat(FormatImageURL(),Eval("IMAGE_FILE_NAME")) %>' class="avatar" onError="this.onerror=null;this.src='<%# string.Concat(FormatImageURL(),"noimage.png") %>';" />
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
							<span class="ico-supplements"></span><strong>Supplements&nbsp</strong> <span class="fav-SuppAvl">
								<%# Eval("SUPPLEMENTS")%></span>
						</div>
					</div>
					<div class="favorite1">
					 <span id='FavSpan1<%# Eval("PRODUCT_SK") %>' class='HideItems'></span> 
						<input type="button" class="btn btn-onoff" value="FAVOURITE" onclick="FRSaveFavorite('<%# Eval("PRODUCT_SK") %>',this)" />
						<span class="favspan1 HideItems">
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
<input type = "hidden" runat = "server" id ="hdnDivision" />
<input type = "hidden" runat = "server" id ="hdnDisplayCount"  />
<div id="favFavoritePopUp" class="HideItems save-search-result">
    <div class="favpopup">
        <h1>
            Mark as a favorite</h1>
        <div class="fav-txt">
            <p>
                To mark a title as favorite you must first be logged into your user account.</p>
            <p>
                Creating an account is free and easy.</p>
        </div>
        <div class="button">
            <button class="btn btn-affermative" type="button" id="FFavLoginBtn">
                Login</button>
            <span>OR</span>
            <button class="btn btn-affermative" type="button" id="FFavSignUpBtn">
                Sign up</button>
        </div>
    </div>
</div>
<div id="favMoreFavoritePopUp" class="HideItems save-search-result">
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
            <button class="btn btn-affermative" type="button" id="MoreFavOkBtn">
                Ok</button>
        </div>
    </div>
</div>

