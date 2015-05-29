<%@ Control Language="C#" Inherits="DotNetNuke.Modules.SampleRequestForm.View" AutoEventWireup="false"
    CodeBehind="View.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap.min.css"
    Priority="14" />
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/SampleRequestForm/Scripts/SampleRequestForm.js"
    ForceProvider="DnnFormBottomProvider" />
<asp:PlaceHolder ID="SRFPlaceHldr" runat="server"></asp:PlaceHolder>
