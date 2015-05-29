<%@ Control Language="C#" CodeBehind="Settings.ascx.cs" Inherits="DotNetNuke.Professional.FolderProviders.AzureFolderProvider.Settings, DotNetNuke.Professional.FolderProviders" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnFormItem">
    <dnn:Label ID="plAccountName" runat="server" controlname="tbAccessKeyId" />
    <asp:TextBox ID="tbAccountName" runat="server" CssClass="dnnFormRequired" />
    <asp:RequiredFieldValidator id="valAccountName" runat="server" ControlToValidate="tbAccountName" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" resourcekey="valAccountName.ErrorMessage" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plAccountKey" runat="server" />
    <asp:TextBox ID="tbAccountKey" runat="server" CssClass="dnnFormRequired" />
    <asp:RequiredFieldValidator ID="valAccountKey" runat="server" ControlToValidate="tbAccountKey" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" resourcekey="valAccountKey.ErrorMessage" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plContainerName" runat="server" />
    <asp:Panel ID="SelectContainerPanel" runat="server" CssClass="dnnLeft">
        <asp:DropDownList ID="ddlContainers" runat="server" AutoPostBack="true" onselectedindexchanged="ddlContainers_SelectedIndexChanged" CausesValidation="false">
            <asp:ListItem value="SelectContainer" resourcekey="SelectContainer" />
            <asp:ListItem value="Refresh" resourcekey="RefreshContainerList" />
        </asp:DropDownList>
        <div class="dnnFormItem"><asp:LinkButton ID="btnNewContainer" runat="server" Text="New Container" resourcekey="NewContainer" onclick="btnNewContainer_Click" CausesValidation="false" /></div>
    </asp:Panel>
    <asp:Panel ID="CreateContainerPanel" runat="server" Visible="false" CssClass="dnnLeft">
        <asp:TextBox ID="tbContainerName" runat="server" CssClass="dnnFormRequired" />
        <div class="dnnFormItem"><asp:LinkButton ID="btnSelectExistingContainer" runat="server" resourcekey="SelectExistingContainer" OnClick="btnSelectExistingContainer_Click" CausesValidation="false" /></div>
    </asp:Panel>
    <asp:CustomValidator ID="valContainerName" runat="server" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" onservervalidate="valContainerName_ServerValidate" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plUseHttps" runat="server" />
    <asp:CheckBox ID="chkUseHttps" runat="server" />
</div>