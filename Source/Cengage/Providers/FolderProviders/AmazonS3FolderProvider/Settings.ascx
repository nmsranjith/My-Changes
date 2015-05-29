<%@ Control Language="C#" CodeBehind="Settings.ascx.cs" Inherits="DotNetNuke.Professional.FolderProviders.AmazonS3FolderProvider.Settings, DotNetNuke.Professional.FolderProviders" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnFormItem">
    <dnn:Label ID="plAccessKeyId" runat="server" controlname="tbAccessKeyId" />
    <asp:TextBox ID="tbAccessKeyId" runat="server" CssClass="dnnFormRequired" />
    <asp:RequiredFieldValidator id="valAccessKeyId" runat="server" ControlToValidate="tbAccessKeyId" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" resourcekey="valAccessKeyId.ErrorMessage" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plSecretAccessKey" runat="server" />
    <asp:TextBox ID="tbSecretAccessKey" runat="server" CssClass="dnnFormRequired" />
    <asp:RequiredFieldValidator ID="valSecretAccessKey" runat="server" ControlToValidate="tbSecretAccessKey" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" resourcekey="valSecretAccessKey.ErrorMessage" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plBucketName" runat="server" />
    <asp:Panel ID="SelectBucketPanel" runat="server" CssClass="dnnLeft">
        <asp:DropDownList ID="ddlBuckets" runat="server" AutoPostBack="true" onselectedindexchanged="ddlBuckets_SelectedIndexChanged" CausesValidation="false">
            <asp:ListItem value="SelectBucket" resourcekey="SelectBucket" />
            <asp:ListItem value="Refresh" resourcekey="RefreshBucketList" />
        </asp:DropDownList>
        <div class="dnnFormItem"><asp:LinkButton ID="btnNewBucket" runat="server" resourcekey="NewBucket" onclick="btnNewBucket_Click" CausesValidation="false" /></div>
    </asp:Panel>
    <asp:Panel ID="CreateBucketPanel" runat="server" Visible="false" CssClass="dnnLeft">
        <asp:TextBox ID="tbBucketName" runat="server" CssClass="dnnFormRequired" />
        <div class="dnnFormItem"><asp:LinkButton ID="btnSelectExistingBucket" runat="server" resourcekey="SelectExistingBucket" OnClick="btnSelectExistingBucket_Click" CausesValidation="false" /></div>
    </asp:Panel>
    <asp:CustomValidator ID="valBucketName" runat="server" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" onservervalidate="valBucketName_ServerValidate" />
</div>
<div class="dnnFormItem">
    <dnn:Label ID="plProtocol" runat="server" />
    <asp:RadioButtonList ID="rblProtocols" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="dnnFormRadioButtons">
        <asp:ListItem Text="HTTPS" Selected="True" />
        <asp:ListItem Text="HTTP" />
    </asp:RadioButtonList>
</div>