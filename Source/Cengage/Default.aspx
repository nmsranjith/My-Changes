<%@ Page Language="C#" AutoEventWireup="false"  EnableEventValidation="false" Inherits="DotNetNuke.Framework.DefaultPage" CodeFile="Default.aspx.cs" %>
<%@ Register TagPrefix="dnncrm" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Common.Controls" Assembly="DotNetNuke" %>
<asp:literal id="skinDocType" runat="server"></asp:literal>
<html <asp:literal id="attributeList" runat="server"></asp:literal>>
<head id="Head" runat="server">

    <title />	
	<!--<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />-->
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <!--<meta content="text/javascript" http-equiv="Content-Script-Type" />
    <meta content="text/css" http-equiv="Content-Style-Type" /> -->
    <meta id="MetaRefresh" runat="Server" http-equiv="Refresh" name="Refresh" />
    <meta id="MetaDescription" runat="Server" name="DESCRIPTION" />
    <meta id="MetaKeywords" runat="Server" name="KEYWORDS" />
	<meta name="dcterms.dateCopyrighted" id="MetaCopyright" runat="Server" content="2012">
    <meta id="MetaGenerator" runat="Server" name="GENERATOR" />
    <meta id="MetaAuthor" runat="Server" name="AUTHOR" />
    <!--<meta name="RESOURCE-TYPE" content="DOCUMENT" />-->
    <meta name="dcterms.audience" content="GLOBAL" />
    <meta id="MetaRobots" runat="server" name="ROBOTS" />
    <meta id="RevisitMetadata" name="REVISIT-AFTER" content="1 DAYS" />
    <meta name="RATING" content="GENERAL" />
<meta name = "viewport" content = "width = 1080" />
<meta runat="Server" id="OpenGraphTitle" property="og:title" content="SAME AS PAGE TITLE" />

<meta runat="Server" id="OpenGraphSiteName" property="og:site_name" content="Cengage Learning [Australia or New Zealand]"/>
<meta runat="Server" id="OpenGraphDescription" property="og:description" content="SAME AS META DESCRIPTION"/>
<meta runat="Server" id="OpenGraphURL" property="og:url" content="SAME AS META DESCRIPTION" />
    <style type="text/css" id="StylePlaceholder" runat="server"></style>
    <asp:PlaceHolder runat="server" ID="ClientDependencyHeadCss"></asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="ClientDependencyHeadJs"></asp:PlaceHolder>
    <asp:placeholder id="CSS" runat="server" />
    <asp:placeholder id="SCRIPTS" runat="server" />
	<!--[if IE 8 ]>  <link href='http://fonts.googleapis.com/css?family=Raleway:400,500,600,700,800' rel='stylesheet' type='text/css'>   <![endif]-->
	<link rel="apple-touch-icon" href="touch-icon-iphone.png" />
	<link rel="apple-touch-icon" sizes="76x76" href="touch-icon-ipad.png" />
	<link rel="apple-touch-icon" sizes="120x120" href="touch-icon-iphone-retina.png" />
	<link rel="apple-touch-icon" sizes="152x152" href="touch-icon-ipad-retina.png" />
    <link href="Resources/Shared/scripts/jquery/bx_styles.css"  rel="stylesheet" type="text/css" />   
    <link href="Portals/0/skins/Cengage/index.css" rel="stylesheet" type="text/css"/>  
	<link href="/Resources/Shared/stylesheets/Utilities.css" rel="stylesheet" type="text/css" />
	 <!--<script src="/Resources/Shared/scripts/Headersearch.js" type="text/javascript"></script>-->
    <link href="/Resources/Shared/stylesheets/Headersearchstyle.css" rel="stylesheet" type="text/css" />
	<!--<script src="Resources/Shared/scripts/Footersearch.js" type="text/javascript"></script>-->
    <link href="Resources/Shared/stylesheets/Footersearchstyle.css" rel="stylesheet" type="text/css" />
	<script src="/Resources/Shared/scripts/kendoui/latest/kendo.web.min.js"
    type="text/javascript"></script>
    <script src="/Resources/Shared/scripts/kendoui/latest/kendo.touch.min.js"
    type="text/javascript"></script>
<link href="/Resources/Shared/scripts/kendoui/styles/kendo.common.min.css"
    rel="stylesheet" type="text/css" />
<link href="/Resources/Shared/scripts/kendoui/styles/kendo.default.min.css"
    rel="stylesheet" type="text/css" />
<script src="/Resources/Shared/scripts/jquery/jquery.bxSlider.min.js"
    type="text/javascript"></script>
	<link href="/Portals/0/Skins/Cengage/ie8skin.css" rel="stylesheet"
    type="text/css" />
	<script type="text/javascript" src="/Resources/Shared/scripts/css_browser_selector.js"> </script>
	<script type="text/javascript" src="/js/Default.js">
</script>
</head>
<body id="Body" runat="server" onbeforeunload="doUnload()" onmousedown="somefunction()">
     <dnn:Form ID="Form" runat="server" ENCTYPE="multipart/form-data">
        <asp:PlaceHolder ID="BodySCRIPTS" runat="server" />
        <asp:Label ID="SkinError" runat="server" CssClass="NormalRed" Visible="False"></asp:Label>
        <asp:PlaceHolder ID="SkinPlaceHolder" runat="server" />
        <input id="ScrollTop" runat="server" name="ScrollTop" type="hidden" />
        <input id="__dnnVariable" runat="server" name="__dnnVariable" type="hidden" />
		<input id="_userDomainForGA" clientidmode="Static" runat="server" name="_userDomainForGA" type="hidden" />
		<input id="_successSignForGA" clientidmode="Static" runat="server" name="_successSignForGA" type="hidden" />
        <asp:placeholder runat="server" ID="ClientResourcesFormBottom" />        
    </dnn:Form>
    <asp:placeholder runat="server" id="ClientResourceIncludes" />
    <dnncrm:ClientResourceLoader runat="server" id="ClientResourceLoader">
        <Paths>
            <dnncrm:ClientResourcePath Name="SkinPath" Path="<%# CurrentSkinPath %>" />
            <dnncrm:ClientResourcePath Name="SharedScripts" Path="~/Resources/Shared/Scripts/" />
        </Paths>
    </dnncrm:ClientResourceLoader>    
</body>
</html>
