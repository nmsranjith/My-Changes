<%@ Control Language="C#" CodeBehind="~/DesktopModules/Skins/skin.cs" AutoEventWireup="false"
    Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/DesktopModules/Admin/Authentication/Login.ascx" %>
<div id="logo">
    <div class="algnCentr loginlogoalign">
        <dnn:LOGO ID="dnnLogo" runat="server" />
    </div>
</div>
<div class="middlecont logincntalign">
    <div class="contCentr">
        <div class="Panes">
            <div id="ContentPane" runat="server" >
                <span id="logintitleLabel" class="Head">Account Login</span>
                <dnn:LOGIN ID="dnnLogin" runat="server" />
            </div>
        </div>
    </div>
</div>
