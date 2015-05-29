<%@ Control Language="C#" Inherits="DotNetNuke.Modules.InformationBanner.View" AutoEventWireup="true"
    CodeBehind="View.ascx.cs" %>

<script src="<%=Page.ResolveUrl("DesktopModules/InformationBanner/scripts/InformationBannerView.js")%>" type="text/javascript"></script>
<asp:Repeater ID="AlertRepeater" runat="server">
    <ItemTemplate>
    <div id="border" class="<%#setBorderClass(Eval("ERRORTYPE").ToString())%>">
			<div id="icon" class="icon-new <%#setIconClass(Eval("ERRORTYPE").ToString())%>"></div>
            <asp:Label CssClass="message" runat="server" Text='<%# Eval("ERROR_MESSAGE")%>' />			
			<div id="color" class="icon-new <%#setColorClass(Eval("ERRORTYPE").ToString())%>" onclick="javascript:DeleteBannerAlerts(this);"></div>
            <input type="hidden" value='<%# Eval("ALERT_ID")%>' />
		</div>
    </ItemTemplate>
</asp:Repeater>
<input id="InfoBannerUserName" runat="server" type="hidden" clientidmode="Static" />
<input id="PageUrlHdn" runat="server" type="hidden" clientidmode="Static" />