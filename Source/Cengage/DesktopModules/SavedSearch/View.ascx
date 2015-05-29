<%@ Control Language="C#" Inherits="DotNetNuke.Modules.SavedSearch.View" AutoEventWireup="true"
    CodeBehind="View.ascx.cs" %>
<script src="<%=Page.ResolveUrl("DesktopModules/SavedSearch/scripts/SavedSearchView.js")%>" type="text/javascript"></script>
<div class="container">
<asp:UpdatePanel id="SavedSearchUpdatePanel" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
    <div class="home_search" id="SShomesearchdiv" runat="server" clientidmode="Static">
        <div class="row">
            <div class="home-search-nav">
                <div id="a1" runat="server" clientidmode="Static" class="title-nav">
                    <h3>
                        <asp:Label ID="lblTitle" runat="server" /></h3>
                    <div >
                        <input type="button" class="see-btn" id="cmdSeeAll" value="See all" runat="server" clientidmode="Static" onserverclick="cmdSeeAll_Click" />
						<input type="button" runat="server" id="SavedSearchDeleteButton"    clientidmode="Static" value="Ok" class="shownull"  onserverclick="YesButton_Click"/>
                    </div>
                </div>
                
                
                <asp:Repeater ID="SearchRepeater" runat="server">
                    <ItemTemplate>
                        <div id='b1' class="rows">
                            <asp:Label ID="lblSEARCH_NAME" runat="server" CssClass="keyword" Text='<%# Eval("SEARCH_NAME") %>'></asp:Label>
                            <label class="quotesLabel">
                                - "</label><asp:Label ID="lblKEYWORDS_STORED" runat="server" Text='<%# Eval("KEYWORDS_STORED").ToString().PadRight(140).Substring(0,140).TrimEnd() %>'
                                    CssClass="keyword1"></asp:Label><label class="quotesLabel">"</label>
                            <div class="search-nav">
								
								<a id="cmdSearch" class="txt" href='<%# Page.ResolveUrl(Eval("URL_SEARCH").ToString()) %>'>Search</a>
								<span class="ico-savedsearch" onclick="javascript:Navigateurl(this);">&nbsp;</span>
								 <asp:HiddenField ID="urlHidden" runat="server" Value='<%# Eval("URL_SEARCH") %>' />
								<span id="level0" class="cross-icon" >
                                            <div ID="cmdDelete" class="icon-new icon-close-black" onclick="javascript:DeleteSavedSearch(this);"></div>
                                            <asp:HiddenField ID="idHidden" clientidmode="Static" runat="server" Value='<%# Eval("PROD_SAV_FAV_SK") %>' />
                                    </span>
                            </div>
                           
                            
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
           <div id="newHdn" style="display:none;" ></div>
            </div>
        </div>
    </div>
	<asp:HiddenField ID="hndToggle" ClientIDMode="static" runat="server" Value="1" />
	<asp:Label id="tempHdn" style="display:none;" runat="server" ClientIDMode="static"></asp:Label>
	<asp:HiddenField ID="hndTotalSeeAll" ClientIDMode="static" runat="server" Value="0" />
	<div id="overlay"></div>
	    <div id="Delete-message" >
    <div class="popupHdrBG">
        <h2 class="PopupHeaderSpan" >Confirm Delete</h2>
    </div>
    <div class="pop-content">
        <div class="p-cnt" >
            <span id="MessageLiteral" >Are you sure you want to delete this saved search?</span>
        </div>
        <div class="pull-right">
            <input type="button" id="NoButton" value="Cancel" class="btn btn-cancel" onclick="javascript:DeleteSavedSearchNo();"/>
			<input type="button" id="YesButton"  value="Ok" class="popupokbtn btn btn-affermative"  onclick="javascript:DeleteSavedSearchYes();"/>
            
        </div>
    </div>
	<asp:HiddenField ID="hndSavedSearchID" ClientIDMode="static" runat="server" Value="0" />
	
	
</div>
 
    </ContentTemplate>
                </asp:UpdatePanel>

    <asp:HiddenField ID="hndCount" ClientIDMode="static" runat="server" Value="0" />
	
</div>
