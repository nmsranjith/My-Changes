<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls"%>
<div class="dnnForm" id="prakash">
	<div class="dnnFormItem">
		<dnn:label runat="server" Text="Name" HelpText="Please enter your name in the space provided" Suffix=":" />
<asp:textbox id="Name" runat="server" CssClass="dnnFormRequired" />
<asp:RequiredFieldValidator ID="reqName" ControlToValidate="Name" cssclass="dnnFormMessage dnnFormError" runat="server" Text="Name is required" />
		
	</div>
	<div class="dnnFormItem">
		<dnn:label runat="server" Text="Email" HelpText="Please enter your email address in the space provided" Suffix=":" />
		<asp:textbox id="Email" runat="server" CssClass="dnnFormRequired" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="Email" cssclass="dnnFormMessage dnnFormError" runat="server" Text="Email Address is required" />
	</div>
	<div class="dnnFormItem">
		<dnn:label runat="server" Text="Message" HelpText="Please send us your comments" Suffix=":" />

		<textarea id="Message" rows="8" runat="server"></textarea>
	</div>
	<div class="dnnFormItem">
		<dnn:label runat="server" Text="Human Test" HelpText="We want to make sure you aren't a robot." Suffix=":" />
		<dnn:captchacontrol  id="ctlCaptcha" captchawidth="130" captchaheight="40" runat="server" errorstyle-cssclass="NormalRed"  />
	</div>
<div style="margin-left:30%;">
	<asp:placeholder id="plhButton" runat="server" />
</div>
</div>














