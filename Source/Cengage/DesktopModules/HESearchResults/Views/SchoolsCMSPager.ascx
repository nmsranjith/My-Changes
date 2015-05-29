<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolsCMSPager.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.SchoolsCMSPager" %>
<link href="<%=Page.ResolveUrl("DesktopModules/HESearchResults/CSS/Cms_Module.css")%>"
    rel="stylesheet" type="text/css" />
<div id="PagerHoldercms" class="Pagercms">
    <div id="StudentPagerDivcms" clientidmode="Static" class="cmsrefdiv" style="display: none;" runat="server">
        <div class="cmsresdiv">
            <asp:HyperLink ID="PreviousButtoncms" ClientIDMode="Static" rel="prev" CssClass="whoverllcms SortTextcms"
                runat="server"></asp:HyperLink>
            <asp:HyperLink ID="ShowPreviousButtoncms" ClientIDMode="Static" rel="prev" CssClass="whoverlcms SortTextcms"
                runat="server"></asp:HyperLink>
        </div>
        <div id="cmsPageControlcms" class="cmsretdiv">
            <asp:PlaceHolder ID="plcPagingcms" runat="server" />
        </div>
        <asp:HiddenField ID="pageNumbercms" ClientIDMode="Static" Value="0,1" runat="server" />
        <div class="cmsrefrdiv">
            <asp:HyperLink ID="ShowNextLinkcms" ClientIDMode="Static" rel="next"
                CssClass="whoverrcms SortTextcms" runat="server"></asp:HyperLink>
            <asp:HyperLink ID="NextLinkcms" ClientIDMode="Static" rel="next"
                CssClass="SortTextcms whovercms" runat="server"></asp:HyperLink>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnpagecountcms" runat="server" ClientIDMode="Static" Value="0" />
<%--<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/CmsSearch.js")%>" type="text/javascript"></script>--%>
<script type="text/javascript">
    function GetQueryStringParams(i) { var h = window.location.search.substring(1); var g = h.split("&"); for (var j = 0; j < g.length; j++) { var f = g[j].split("="); if (f[0] == i) { return f[1] } } } $(document).ready(function () { if (GetQueryStringParams("cp") != undefined) { var a = Math.ceil(($("#hdnProdCountcms").val()) / ($("#hdnItemCountcms").val())); $("#cmsPageControlcms").children("a").each(function () { if ($(this).text() == GetQueryStringParams("cp")) { $(this).addClass("Highlightcms"); if (GetQueryStringParams("cp") == a) { $("#ShowNextLinkcms").attr("disabled", "disabled"); $("#NextLinkcms").attr("disabled", "disabled"); $("#NextLinkcms").removeClass("whovercms").addClass("Rightovercodcms"); $("#ShowNextLinkcms").removeClass("whoverrcms").addClass("Rightrovercodcms") } else { if (1 == GetQueryStringParams("cp")) { $("#PreviousButtoncms").attr("disabled", "disabled"); $("#ShowPreviousButtoncms").attr("disabled", "disabled"); $("#PreviousButtoncms").removeClass("whoverllcms").addClass("Leftlovercodcms"); $("#ShowPreviousButtoncms").removeClass("whoverlcms").addClass("Leftovercodcms") } } } else { $(this).removeClass("Highlightcms"); if (GetQueryStringParams("cp") != a) { $("#ShowNextLinkcms").removeAttr("disabled"); $("#NextLinkcms").removeAttr("disabled"); $("#NextLinkcms").removeClass("Rightovercodcms").addClass("whovercms"); $("#ShowNextLinkcms").removeClass("Rightrovercodcms").addClass("whoverrcms") } if (1 != GetQueryStringParams("cp")) { $("#PreviousButtoncms").removeAttr("disabled"); $("#ShowPreviousButtoncms").removeAttr("disabled"); $("#PreviousButtoncms").removeClass("Leftlovercodcms").addClass("whoverllcms"); $("#ShowPreviousButtoncms").removeClass("Leftovercodcms").addClass("whoverlcms") } } }) } });
</script>