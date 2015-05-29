<%@ Control Language="C#" CodeBehind="Settings.ascx.cs" Inherits="DotNetNuke.Professional.FolderProviders.UNCFolderProvider.Settings, DotNetNuke.Professional.FolderProviders" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnFormItem">
    <asp:Panel ID="pnlError" runat="server" 
               CssClass="dnnFormMessage dnnFormValidationSummary" />
    <dnn:Label ID="plUNCPath" runat="server" controlname="tbAccessKeyId" />
    <asp:TextBox ID="tbUNCPath" runat="server" CssClass="dnnFormRequired" />
    <asp:RequiredFieldValidator id="valUNCPath" runat="server" ControlToValidate="tbUNCPath" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" />
</div>