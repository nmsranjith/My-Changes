<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookPacks.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Books.Books.BookPacks" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Books/Scripts/BookPack.js" ForceProvider="DnnFormBottomProvider" />

<div class="books-ecoll">
 <div class="books-ecoll-header"><h5>Select your book pack</h5><span class="remainingdays">You have <span id="DaysLeftLbl" runat="server"></span> left to make this selection</span></div>
<asp:Repeater ID="BookPackRptr" runat="server"  OnItemDataBound="BookPackRptr_ItemDataBound">
    <ItemTemplate>
        <div class="books-ecoll-radio"><div class="bp-ecoll-title"><input type="radio" class="bp-ecoll-radio" value='<%# Eval("BookPackSk") %>' name="bookpack"/> 
		<span class="bp-ecoll-packname" id='<%# "PackName"+Eval("BookPackSk").ToString() %>'><%# Eval("BookPackName") %></span>
		<input id="EditPack" type="button" visible="false" value="Edit" class="bp-edit-btn" onclick="OpenCustomPack()"  runat="server"/>
		</div>
		<input type="button" value="Contents" class="btn btn-general" onclick="OpenContents('<%# Eval("BookPackSk") %>','<%# Eval("BookPackName") %>')"/>
		<asp:HiddenField ID="BookTypeHdn" runat="server" Value='<%# Eval("BookPackType")%>' />
		</div>
    </ItemTemplate>
</asp:Repeater>
 <div class="custom-button">
<asp:Button ID="CustomButton" runat="server" Text="Build your own pack" CssClass="" ClientIDMode="Static"/>
</div>
 <div>
  <div class="bp-customroot" >
    <a href="#">Contect us</a> for more information about selecting your book pack
  </div>
  <div>
  	<div class="setbutton">
	<input id="SetButton" type="button" value="Set Book Pack" class="btn btn-affermative" onclick="SetBookPack()">
	</div>
  </div>
</div>
</div>


<div id="ContentsPopup" class="HideItems save-search-name">
    <div class="favpopup">
		<div class="favpopup-header custompackpopup-header">
        <h1 id="PackNameHdr"></h1> 
		<button class="btn btn-general" type="button" id="ContentsCloseButton">
                Close</button>
		</div>
        <div class="save-cont">
            <div class="save-cont-child"><div class="bp-search-title"><input id="ContentSearchTxt" type="text" placeholder="search by name, level or type" maxlength="75"/></div>
			<div class="BookSearchbtndiv">
                <input type="submit"  value="" onclick="ShowUpdate();" id="SearchButton" class="SearchButton">
            </div>
			<div class="bp-sort-title">
			<span class="bp-sort-label">Order By :</span>
            <select id="contentsOrder">
            <option value="level">Level</option>
            <option value="age">Age</option>
            <option value="text">Type</option>
            </select>
			</div>
            </div>
            <div id="BPContentsHolder" class="items-container ">                
            </div>
        </div>        
    </div>
	<input type="hidden" id="PackSk" />
</div>

<div id="CustomPackPopup" class="HideItems save-search-name">
    <div class="favpopup">
		<div class="custompackpopup-header">
        <h1 id="SelectedCntHdr"> You have selected <span id="CustomSelectedCnt"></span>
            </h1> 
			<div class="bp-custom-btns"><button class="btn btn-general" type="button" id="CustomCancelButton">
                Cancel</button>
				<button class="btn btn-affermative" type="button" id="CustomSaveButton" onclick="BPSaveBtnClick()">
                Save</button></div>
		</div>
		<div id="BPSaveError" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span id="BPErrorSpan" class="error-text">Please choose eBooks before you save your book pack. </span><span class="ico-error-close pull-right" onclick='closeAlertMsg("BPSaveError")'></span></div></div>
        <div class="save-cont">
           <div class="save-cont-child"><div class="bp-search-title"><input id="CustomSearchTxt" type="text" placeholder="search by name, level or type" maxlength="75"/></div>
		   <div class="BookSearchbtndiv">
                
                <input type="submit"  value="" onclick="ShowUpdate();" id="SearchButton" class="SearchButton">
                
            </div>
           <div class="bp-sort-title"> 
		   <span class="bp-sort-label">Order By :</span>
		   <select id="CustomOrder">
             <option value="level">Level</option>
            <option value="age">Age</option>
            <option value="text">Type</option>
            </select>
			</div>
            </div>
            <div id="BPCustomContentsHolder" class="items-container ">                
            </div>
        </div>        
    </div>
</div>

<input type="hidden" runat="server" clientidmode="static" id="pageurlhdn" />