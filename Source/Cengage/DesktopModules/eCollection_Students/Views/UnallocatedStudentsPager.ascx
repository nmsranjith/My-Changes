<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnallocatedStudentsPager.ascx.cs"
Inherits="DotNetNuke.Modules.eCollection_Students.Views.UnallocatedStudentsPager" %>
<div id="PagerHolder" class="he-Pager">
    <div id="StudentPagerDiv" clientidmode="Static" style="display: none;" class="he-srrefdiv" runat="server">
        <span class="he-srresdiv">           
			<a id="PreviousButton" class="" runat="server" title="First" clientidmode="Static">&laquo;</a>
					<span class="page-divider">|</span>
			<a id="ShowPreviousButton" class="" runat="server"  title="Previous" clientidmode="Static">&lsaquo;</a>
        </span>
		
				
        <span id="PageControl" class="he-srretdiv">
            <asp:PlaceHolder ID="plcPaging" runat="server" />
        </span>
        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="0,1" runat="server" />
		
	
        <span class="he-srrefrdiv">
			<a id="ShowNextLink" class="" runat="server" title="Next" clientidmode="Static">&rsaquo;</a>	
						<span class="page-divider">|</span>
			<a id="NextLink" class="" runat="server" title="Last" clientidmode="Static">&raquo;</a>	
        </span>
    </div>
</div>
<asp:HiddenField ID="Hdnpagecount" runat="server" ClientIDMode="Static" Value = "0" />
<asp:HiddenField ID="AdvSearchOpenFlagHdn2" runat="server" ClientIDMode="Static" />
<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/CustomPage.js")%>" type="text/javascript"></script>


