<%@ Control language="C#" Inherits="DotNetNuke.Modules.FeaturedSearch.Edit" AutoEventWireup="false"  Codebehind="Edit.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %> 
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/FeaturedSearch/Scripts/FeaturedSearch.js" ForceProvider="DnnFormBottomProvider"/> 
<div class="fsave-cont ftopdiv ftopoptions">	
	<span class="fspan">Options : </span>
    <input id="fnewrd" type="radio" value="New Search" checked="true" name="SelectItem" onclick="SelectedItem(1,'fnewrd')" class="pull-left fopspan"/> <span class="fopspan fspan" onclick="SelectedItem(1,'fnewrd')">New Search</span>
    <input id="fcurrrd" type="radio" value="Current search list" name="SelectItem" onclick="SelectedItem(2,'fcurrrd')" class="pull-left fopspan"/> <span class="fopspan fspan" onclick="SelectedItem(2,'fcurrrd')">Current search list</span>
</div>

<div id="NewSearchDiv" class="ftopdiv">
<div class="fsave-cont">
<p>
	Please enter the name of your search:
</p>
<div class="frows frowssearchname">
	<label for="SearchNameTxt">
		Name:</label>
	<span>
		<input id="SearchNameTxt" type="text" name="ftxt" class="ftxt" placeholder="i.e. wish-list, to booklist next year, save for later" maxlength="150" runat="server" clientidmode="Static"   /></span>
</div>
<div id="SaveSearchNameerror" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span class="error-text">Please enter name of your search. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)"></span></div></div>
</div>
<div id="AdvanceSearchDiv">
    <div class="fsearch-top">
        <div class="ffindTxt">
            Find results with:</div> 
<div id="SaveSearchItemserror" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span class="error-text">Please fill any one of the search category. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)"></span></div></div>			
        <div class="fsearch_nav">
            <div class="frows">
                <label for="AdvTitleTxt">
                    Title:</label>
                <span>
                    <input id="AdvTitleTxt" type="text" name="Title" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
            <div class="frows">
                <label for="AdvAuthorTxt">
                    Author:</label>
                <span>
                    <input id="AdvAuthorTxt" type="text" name="Author" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
            <div class="frows">
                <label for="AdvSubjectTxt">
                    Subject:</label>
                <span>
                    <input id="AdvSubjectTxt" type="text" name="Subject" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
			<div class="frows">
                <label for="AdvFormatTxt">
                    Format:</label>
                <span>
                    <input id="AdvFormatTxt" type="text" name="Author" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
			<div class="frows">
                <label for="AdvLibraryTypeTxt">
                    Library Type:</label>
                <span>
                    <input id="AdvLibraryTypeTxt" type="text" name="LibraryType" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
			<div class="frows">
                <label for="AdvOriginTxt">
                    Origin:</label>
                <span>
                    <input id="AdvOriginTxt" type="text" name="Origin" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
			<div class="frows">
                <label for="AdvPublishedYearTxt">
                    Published Year:</label>
                <span>
                    <input id="AdvPublishedYearTxt" type="text" name="PublishedYear" class="ftxt" maxlength="50"  runat="server" clientidmode="Static" /></span>
            </div>
			<div class="frows">
                <label for="AdvPublisherTxt">
                    Publisher:</label>
                <span>
                    <input id="AdvPublisherTxt" type="text" name="Publisher" class="ftxt" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div>
			<div class="frows">
                <label for="AdveBookPlatformTxt">
                    eBook Platform:</label>
                <span>
                    <input id="AdveBookPlatformTxt" type="text" name="eBookPlatform" class="ftxt" maxlength="50"  runat="server" clientidmode="Static" /></span>
            </div>           
            <div id="FilterSet">
                <div class="ffillterTxt">
                    Filter results:</div>                
				<div class="frows1">
                <label for="AdvAllWordsTxt">
                    All these words:</label>
                <span>
                    <input id="AdvAllWordsTxt" type="text" name="AllWords" class="ftxt1" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div> 
			<div class="frows1">
                <label for="AdvExactPhraseTxt">
                    This exact phrase:</label>
                <span>
                    <input id="AdvExactPhraseTxt" type="text" name="ExactPhrase" class="ftxt1" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div> 
			<div class="frows1">
                <label for="AdvNoWordsTxt">
                    None of these words:</label>
                <span>
                    <input id="AdvNoWordsTxt" type="text" name="Noneofthese" class="ftxt1" maxlength="50" runat="server" clientidmode="Static"  /></span>
            </div> 
            </div>
            <div class="pull-right fadvanced-search-btn">
                <button id="fsavebutton" type="button" class="btn btn-affermative upper-case" onclick="AdvanceSearch()">
                    SAVE</button>
            </div>
			<div class="pull-right fadvanced-search-btn">
                <button type="button" class="btn btn-affermative upper-case" onclick="window.location.href='/gale'">
                    CANCEL</button>
            </div>
        </div>
    </div>
    <input type="reset" id="AdvResetBtn" class="HideItems" />
</div>
</div>
<div id="CurrentSearchListDiv" class="ftopdiv flisttop HideItems">
	<span class="flistheader">Saved Searches :</span>
    <asp:Repeater ID="CurrentSearchListRptr" runat="server">
        <ItemTemplate>
            <div class="Div_FullWidth flistitems">
                <div class="flistedit">
                    <%--<img src="/Images/manage-icn.png">--%>
                    <input type="button" class="feditbtn" value="" onclick='feditsearch(<%# Eval("FEATURED_SEARCH_SK") %>)' />
                </div>
                <div class="searchname">
                    <%# Eval("SEARCH_NAME")%>
                </div>
                <div class="deletesearch">
                    <span class="ico-clearclose" onclick='fRemoveItem(<%# Eval("FEATURED_SEARCH_SK") %>,this)'></span>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
	 <div class="pull-right fadvanced-search-btn">
                <button type="button" class="btn btn-affermative upper-case" onclick="window.location.href='/gale'">
                    CANCEL</button>
            </div>
</div>
<asp:DropDownList ID="FSFacetDrpDwn" runat="server" CssClass="HideItems" DataTextField="ATTRIBUTE_TYPE" DataValueField="ATTRIBUTE_TYPE_SK" ClientIDMode="Static"></asp:DropDownList>
<asp:HiddenField ID="FModuleIdHdn" runat="server" ClientIDMode="Static" />