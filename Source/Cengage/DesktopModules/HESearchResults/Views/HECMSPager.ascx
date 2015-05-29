<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HECMSPager.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.HECMSPager" %>
<div id="PagerHoldercms" align="center" class="he-Pager HideItems">
    <div id="StudentPagerDivcms" clientidmode="Static" style="display: none;" class="he-srrefdiv" runat="server">
         <span class="he-srresdiv">            
			<a id="PreviousButtoncms" class="" runat="server" title="First" clientidmode="Static">&laquo;</a>
				<span class="page-divider">|</span>
			<a id="ShowPreviousButtoncms" class="" runat="server" title="Previous" clientidmode="Static">&lsaquo;</a>        
        </span>

        <span id="cmsPageControlcms" class="he-srretdiv">
            <asp:PlaceHolder ID="plcPagingcms" runat="server" />
        </span>
        <asp:HiddenField ID="pageNumbercms" ClientIDMode="Static" Value="0,1" runat="server" />
        <span class="he-srrefrdiv">            
			<a id="ShowNextLinkcms" class="" runat="server" title="Next" clientidmode="Static">&rsaquo;</a>	
			<span class="page-divider">|</span>
			<a id="NextLinkcms" class="" runat="server" title="Last" clientidmode="Static">&raquo;</a>
        </span>
    </div>
</div>
<asp:HiddenField ID="hdnpagecountcms" runat="server" ClientIDMode="Static" Value = "0" />
<asp:HiddenField ID="AdvSearchOpenFlagHdn3" runat="server" ClientIDMode="Static" />
<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/CustomPage.js")%>" type="text/javascript"></script>