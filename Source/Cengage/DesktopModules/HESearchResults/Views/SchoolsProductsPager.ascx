<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolsProductsPager.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.SchoolsProductsPager" %>
<link href="<%=Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.mCustomScrollbar.css")%>"
    rel="stylesheet" type="text/css" />
<link href="<%=Page.ResolveUrl("DesktopModules/HESearchResults/CSS/schools.css")%>" rel="stylesheet"
    type="text/css" />
<div id="PagerHolder" class="Pager">
    <div id="StudentPagerDiv" clientidmode="Static" style="display: none;" class="srrefdiv"
        runat="server">
        <div class="srresdiv">
            <asp:HyperLink ID="PreviousButton" ClientIDMode="Static" rel="prev" CssClass="whoverll SortText"
                runat="server"></asp:HyperLink>
            <asp:HyperLink ID="ShowPreviousButton" ClientIDMode="Static" rel="prev" CssClass="whoverl SortText"
                runat="server"></asp:HyperLink>
        </div>
        <div id="PageControl" class="srretdiv">
            <asp:PlaceHolder ID="plcPaging" runat="server" />
        </div>
        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="0,1" runat="server" />
        <div class="srrefrdiv">
            <asp:HyperLink ID="ShowNextLink" ClientIDMode="Static" rel="next" CssClass="whoverr SortText"
                runat="server"></asp:HyperLink>
            <asp:HyperLink ID="NextLink" ClientIDMode="Static" CssClass="SortText whover" rel="next" runat="server"></asp:HyperLink>
        </div>
    </div>
</div>
<asp:HiddenField ID="Hdnpagecount" runat="server" ClientIDMode="Static" Value="0" />
<%--<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/CustomPage.js")%>" type="text/javascript"></script>--%>
<script type="text/javascript">
    function GetQueryStringParams(i) {
        var h = window.location.search.substring(1);
        var g = h.split("&");
        for (var j = 0; j < g.length; j++) {
            var f = g[j].split("="); if (f[0] == i) {
                return f[1]
            }
        }
    }
    $(document).ready(function () {
        if (GetQueryStringParams("p") != undefined) {
            var a = Math.ceil(($("#hdnProdCount").val()) / ($("#hdnItemCount").val()));
            $("#PageControl").children("a").each(function () {
                if ($(this).text() == GetQueryStringParams("p")) {
                    $(this).addClass("Highlight");
                    if (GetQueryStringParams("p") == a) {
                        $("#ShowNextLink").attr("disabled", "disabled");
                        $("#NextLink").attr("disabled", "disabled");
                        $("#NextLink").removeClass("whover").addClass("Rightovercod");
                        $("#ShowNextLink").removeClass("whoverr").addClass("Rightrovercod")
                    }
                    else {
                        if (1 == GetQueryStringParams("p")) {
                            $("#PreviousButton").attr("disabled", "disabled");
                            $("#ShowPreviousButton").attr("disabled", "disabled");
                            $("#PreviousButton").removeClass("whoverll").addClass("Leftlovercod");
                            $("#ShowPreviousButton").removeClass("whoverl").addClass("Leftovercod")
                        }
                    }
                }
                else {
                    $(this).removeClass("Highlight");
                    if (GetQueryStringParams("p") != a) {
                        $("#ShowNextLink").removeAttr("disabled");
                        $("#NextLink").removeAttr("disabled");
                        $("#NextLink").removeClass("Rightovercod").addClass("whover");
                        $("#ShowNextLink").removeClass("Rightrovercod").addClass("whoverr")
                    }
                    if (1 != GetQueryStringParams("p")) {
                        $("#PreviousButton").removeAttr("disabled");
                        $("#ShowPreviousButton").removeAttr("disabled");
                        $("#PreviousButton").removeClass("Leftlovercod").addClass("whoverll");
                        $("#ShowPreviousButton").removeClass("Leftovercod").addClass("whoverl")
                    }
                }

            })
        }
    });
</script>
