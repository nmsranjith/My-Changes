<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="eCollectionMessages.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.eCollectionMessages" %>
<asp:Panel runat="server" ID="pl_error" Visible="false" class="dnnFormMessage dnnFormError">
 <div class="dnnMsgHolder">
    <asp:Literal ID="lit_error" runat="server" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pl_warning" Visible="false" class="dnnFormMessage dnnFormWarning">
 <div class="dnnMsgHolder">
    <asp:Literal ID="lit_warning" runat="server" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pl_success" Visible="false" class="dnnFormMessage dnnFormSuccess">
 <div class="dnnMsgHolder">
    <asp:Literal ID="lit_success" runat="server" />
    </div>
</asp:Panel>
<asp:Panel runat="server" ID="pl_info" Visible="false" class="dnnFormMessage dnnFormInfo">
 <div class="dnnMsgHolder">
    <asp:Literal ID="lit_info" runat="server" />
    </div>
</asp:Panel>
