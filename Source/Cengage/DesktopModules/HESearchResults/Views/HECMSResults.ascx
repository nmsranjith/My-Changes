<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HECMSResults.ascx.cs"
    Inherits="DotNetNuke.Modules.HESearchResults.Views.HECMSResults" %>    
<%@ Register Src="HECMSPager.ascx" TagName="HECMSPager" TagPrefix="CP" %>
<!-- With Image Div -->
<asp:PlaceHolder ID="ResultCountPlaceHldr" runat="server">
<div id="CMSResultsCountDiv" class="HideItems">
    <asp:Label ID="CurrentPgStartSizeLbl" runat="server" ClientIDMode="Static"></asp:Label>
    <asp:Label ID="CurrentPgEndSizeLbl" runat="server" ClientIDMode="Static"></asp:Label>
    <asp:Label ID="TotalResultLbl" runat="server" ClientIDMode="Static"></asp:Label>
    <asp:Label ID="DivisionLbl" CssClass="DivisionLbl" runat="server" ClientIDMode="Static"></asp:Label>
</div>
</asp:PlaceHolder>
<asp:Repeater ID="CMSSiteResultsRptr" runat="server">
    <ItemTemplate>
        <div class="cms">
            <div class="cnt">
                <div class="info">
                    <%--<asp:Image ID="PageImage" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' CssClass="avatar" />--%>
                </div>
                <h4>
                    <asp:HyperLink ID="PgTitleLink" runat="server" NavigateUrl='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TabId"),"") %>'
                        Text='<%# DataBinder.Eval(Container.DataItem, "TabName") %>' />
                </h4>

                <%# Eval("DESCRIPTION")%>
                <div class="infolink">
                    <asp:HyperLink ID="lnkLink" runat="server" NavigateUrl='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TABID"),"") %>'
                        Text='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TABID"),"") %>' />
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<CP:HECMSPager ID="HECMSPager" runat="server">
</CP:HECMSPager>
<asp:PlaceHolder ID="NoResultPlaceHldr" runat="server"></asp:PlaceHolder>

