<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPaging.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.CustomPaging" %>
<style type="text/css">
    .page:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#707070), to(#707070));
        background: -moz-linear-gradient(to bottom,#707070, #707070);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#707070', endColorstr='#707070', gradientType='0');*/
        background-color: #707070;
        color: White !important;
    }
    .Pager a[disabled]:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#F9F9F9), to(#E9E9E9));
        background: -moz-linear-gradient(to bottom,#F9F9F9, #E9E9E9);
        background: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#E9E9E9', gradientType='0'); /*filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9F9F9', endColorstr='#F9F9F9', gradientType='0');*/
        background-color: #E9E9E9;
    }
</style>
<center id="PagerHolder" class="Pager CusPagingPgrHldr">
    <div id="StudentPagerDiv" clientidmode="Static" class="CusPagingpgrinnerdiv" runat="server">
        <div class="CusPagingthirddiv">
            <asp:LinkButton ID="PreviousButton" ClientIDMode="Static" CssClass="page SortText"
                runat="server" Text="<<" OnClick="PreviousButton_Click"></asp:LinkButton>
            <asp:LinkButton ID="ShowPreviousButton" ClientIDMode="Static" CssClass="page SortText"
                runat="server" Text="<" OnClick="ShowPreviousButton_Click"></asp:LinkButton>
        </div>
        <div id="PageControl" class="CusPagingPgrplacehdrdiv">
            <asp:PlaceHolder ID="plcPaging" runat="server" />
        </div>
        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="0,1" runat="server" />
        <div id="RightNavigationContainer" class="CusPagingRightNavdiv">
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
