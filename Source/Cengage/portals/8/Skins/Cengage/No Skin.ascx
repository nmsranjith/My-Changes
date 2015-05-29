<%@ Control Language="C#" CodeBehind="~/DesktopModules/Skins/skin.cs" AutoEventWireup="false"
    Inherits="DotNetNuke.UI.Skins.Skin" %>
<div id="ContentPane" runat="server">
</div>
<script type="text/javascript" language="javascript">
    function GetFile(path) {
        var pathname = window.location.pathname;
		var temppath;
		if(pathname.indexOf('signup')>-1)
		{
		pathname = window.location.host;
		var root = "http://" + window.location.host;
		}else{
        var temppath = pathname.split('/');
        var root = "http://" + window.location.host + "/" + temppath[0];
		}
        var url = root + path;
        return url;
    }
</script>