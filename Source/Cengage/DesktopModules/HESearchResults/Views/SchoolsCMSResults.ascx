<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolsCMSResults.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.SchoolsCMSResults" %>
<link href="<%=Page.ResolveUrl("DesktopModules/HESearchResults/CSS/Cms_Module.css")%>"
    rel="stylesheet" type="text/css" />
<div id="cmspagediv" class="cmsrediv">
<%@ Register Src="SchoolsCMSPager.ascx" TagName="CmsPaging" TagPrefix="CP" %>
<asp:Repeater ID="CmsPageResultRepeaterControl" runat="server">
    <HeaderTemplate>
    </HeaderTemplate>
        <ItemTemplate>
        <br />
            <div class="rowstyle">
            
                <div class="cmsrevfdiv">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass = "PageTitle"
                    NavigateUrl='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TabId"),"") %>'
                    Text='<%# DataBinder.Eval(Container.DataItem, "TabName") %>' />
                </div>
                
                
                <div class="cmsrevsdiv">
                    <asp:Label ID="DescriptionLabel" runat="server"
                    Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' />
                </div>
                
                
                <div class="cmsrevtdiv">
                    <asp:HyperLink ID="lnkLink" runat="server" CssClass = "PageLink"
                    NavigateUrl='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TabId"),"") %>'
                    Text='<%# FormatURL((int)DataBinder.Eval(Container.DataItem,"TabId"),"") %>' />
                </div>
            
            </div>
            <br />
        </ItemTemplate>
</asp:Repeater>
 <CP:CmsPaging ID="Cmspage" runat="server"></CP:CmsPaging>
<asp:HiddenField ID="Pagenum" runat="server" ClientIDMode="Static" />
<input type = "hidden" id = "hdnProdCountcms" runat = "server" clientidmode ="Static" />
<input type = "hidden" id = "hdnItemCountcms" runat = "server" clientidmode ="Static" />
</div>