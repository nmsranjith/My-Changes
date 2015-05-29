<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPaging.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Books.Books.CustomPaging" %>
<center id="PagerHolder" class="Pager" style="min-width: 90%">
    <div id="StudentPagerDiv" clientidmode="Static" style="width: 96%; float: right;
        margin-top: 18px; display: none;" runat="server">
        <div style="float: left; width: 12.8%;">
            <asp:LinkButton ID="PreviousButton" ClientIDMode="Static" CssClass="page SortText"
                runat="server" Text="<<" OnClick="PreviousButton_Click"></asp:LinkButton>
            <asp:LinkButton ID="ShowPreviousButton" ClientIDMode="Static" CssClass="page SortText"
                runat="server" Text="<" OnClick="ShowPreviousButton_Click"></asp:LinkButton>
        </div>
        <div id="PageControl" style="min-width: 5%; float: left; margin-left: -7px;">
            <%-- <asp:LinkButton ID="triggerFirstlnk" OnClientClick="javascript:triggerFirstlnkbtn(this);"
                                        ClientIDMode="Static" Style="display: none; float: left; margin-left: 4px;" CssClass="dottedlink"
                                        runat="server" Text="..." OnClick="ShowPreviousButton_Click"></asp:LinkButton>--%>
            <asp:PlaceHolder ID="plcPaging" runat="server" />
            <%--<asp:LinkButton ID="triggerEndlnk" ClientIDMode="Static" OnClientClick="javascript:TriggerEndLink(this);"
                                        Style="display: none; float: right; margin-top: -16px; margin-right: -5px;" CssClass="dottedlink"
                                        runat="server" Text="..." OnClick="ShowNextLink_Click"></asp:LinkButton>--%>
        </div>
        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="0,1" runat="server" />
        <div id="RightNavigationContainer" style="float: left; width: 12.8%; margin-left: -3px;">
            <asp:LinkButton ID="ShowNextLink" ClientIDMode="Static" CssClass="page SortText"
                runat="server" Text=">" OnClick="ShowNextLink_Click"></asp:LinkButton>
            <asp:LinkButton ID="NextLink" ClientIDMode="Static" CssClass="page SortText" runat="server"
                Text=">>" OnClick=" NextLink_Click"></asp:LinkButton>
        </div>
    </div>
</center>
<script type="text/javascript">
    function GetPageNumber(e) {
        document.getElementById("pageNumber").value = e.parentNode.children[0].innerHTML + ',' + e.innerHTML;
    }
    function TriggerEndLink(e) {
        if (e.parentNode.children.length == "7") {
            document.getElementById("pageNumber").value = e.parentNode.children[5].innerHTML;
        }
        else {
            document.getElementById("pageNumber").value = e.parentNode.children[4].innerHTML;
        }
        //$("#ShowNextLink")[0].click();
    }
    function triggerFirstlnkbtn(e) {
        document.getElementById("pageNumber").value = e.parentNode.children[1].innerHTML;
        //$("#ShowPreviousButton")[0].click();
    }
</script>

