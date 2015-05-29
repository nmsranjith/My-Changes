<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BPBooksList.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Books.Books.BPBooksList" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Books/Scripts/BookPack.js"
    ForceProvider="DnnFormBottomProvider" />
<div id="BPHeader1" class="books-ecoll">
        <div class="bp-header-txt"><h5>
            Book pack name</h5>
        <span class="remainingdays1">You have <span id="DaysLeftLbl1" runat="server"></span> left
            to make this selection</span></div>
			<div><input id="BPChangeBtn" type="button" value="Change" class="btn btn-general" onclick="ChangeBtnClick()"/></div>
</div>
<div id="BPHeader2" class="books-ecoll HideItems">
    <div class="books-ecoll-header">
        <h5>
            Select your book pack</h5>
        <span class="remainingdays">You have <span id="DaysLeftLbl" runat="server"></span> left
            to make this selection</span></div>
    <asp:Repeater ID="BookPackRptr" runat="server">
        <ItemTemplate>
            <div class="books-ecoll-radio">
                <div class="bp-ecoll-title"><input type="radio" value='<%# Eval("BookPackSk") %>' name="bookpack" />
                <span id='<%# "PackName"+Eval("BookPackSk").ToString() %>'>
                    <%# Eval("BookPackName") %></span>
					<input type="button" value="Edit" class="bp-edit-btn HideItems" onclick="OpenCustomPack()"/>
					</div>
                <input type="button" value="Contents" class="btn btn-general" onclick="OpenContents('<%# Eval("BookPackSk") %>','<%# Eval("BookPackName") %>')" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="custom-button">
        <asp:Button ID="CustomButton" runat="server" Text="Build Custom" CssClass="" ClientIDMode="Static" />
    </div>
	<div>
	  <div class="bp-customroot" >
		<a href="#">Contect us</a> for more information about selecting your book pack
	  </div>
    <div>
    <div class="setbutton">
		<button class="btn btn-general" type="button" id="BPCancelButton" onclick="CancelBtnClick()">
            Cancel</button>
		<input id="SetButton" type="button" value="Set Book Pack" class="btn btn-affermative" onclick="SetBookPack()"/>
    </div>
	</div>
</div>
</div>
<div id<div id="ContentsPopup" class="HideItems save-search-name">
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
<div class="BooksDivset">
    <asp:Button ID="Duringgraceperiod" runat="server" ClientIDMode="Static" Style="display: none"
        OnClick="Duringgraceperiod_Click" />
    <div class="ProgressDivClass" style="display: none" id="UpdateProgressImg">
        <div class="ProgressInnerDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                alt="Processing" />
        </div>
    </div>
    <div id="BookSearchdiv" class="BooksSearchdiv">
        <asp:UpdatePanel ID="BookUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="SearchTextBox" ClientIDMode="Static" runat="server" CssClass="Booksearchtxt"></asp:TextBox>
                <asp:DropDownList ID="CategoriesDrpList" ClientIDMode="Static" runat="server" Style="float: left;
                    font-family: Raleway, Arial, Sans-Serif; font-size: 9pt; width: 200px; margin-left: 25px;
                    color: #707070; margin-top: 4px;" OnSelectedIndexChanged="itemSelected" AutoPostBack="true"
                    TabIndex="0">
                    <asp:ListItem Text="TEXT TYPE"></asp:ListItem>
                    <asp:ListItem Text="GUIDED READING LEVEL"></asp:ListItem>
                    <asp:ListItem Text="READING AGE"></asp:ListItem>
                    <asp:ListItem Text="CHARACTER FAMILY"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="SearchButton" runat="server" OnClick="SearchButton_Click" Text="SEARCH"
                    OnClientClick="ShowUpdate()" Style="color: White; float: right; width: 88px;
                    font-family: Raleway ExtraBold, Arial, Sans-Serif; text-shadow: 1px 2px 2px #4D7B18;
                    font-size: 10pt; height: 33px; margin-top: 3px; margin-right: 3px; cursor: pointer;
                    box-shadow: 1px 2px lightgray; border-radius: 3px; background: -moz-linear-gradient(center top , white 0%, #8EBC5A 5%, #609624 130%) repeat scroll 0 0 transparent;
                    background: -webkit-gradient(linear, left top, left bottom, from(#8EBC5A), to(#609624));
                    background: -ms-linear-gradient(top, #8EBC5A 5%, #609624 130%); filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#8EBC5A', endColorstr='#609624', gradientType='0');" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="CategoriesDrpList" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="width: 91%; margin-top: 10px; margin-left: 25px; padding: 15px 0px 65px 15px;
        float: left; border: 1px solid #CCC;">
        <h5>
            TITLES AVAILABLE ON THE BOOKSHELF :
        </h5>
        <div class="eCollectionEditLbl">
            <%-- style="margin-top: 30px">--%>
            <RL:RLSlider ID="ReadingLevelSlider" runat="server">
            </RL:RLSlider>
            <asp:HiddenField ID="SliderValue" ClientIDMode="Static" runat="server" />
        </div>
    </div>
    <asp:HiddenField ID="custItmSKHidden" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SliderRange" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel ID="MessageDivUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="MessageOuterDiv" runat="server" style="width: 100%; position: static; display: none;">
                <div class="bubble">
                    <asp:Label ID="Message1" runat="server" Text="" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="BooksContentDiv" id="BooksDiv">
        <asp:UpdatePanel ID="BooksUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="hiddenbtn" ClientIDMode="Static" Style="display: none" runat="server"
                    OnClick="hiddenbtn_Click" />
                <asp:Repeater ID="CollectionRepeater" runat="server" OnItemDataBound="CollectionRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div id="SelectedBookRepdiv" class='<%# Eval("ClassName") %>'>
                            <div id="SelectedBookContentFrstdiv" style="float: left; margin-top: 68px; min-width: 57px;">
                                <input type="checkbox" id="BooksCheckBox" runat="server" checked='<%# Eval("Checked") %>'
                                    style="float: left; margin-left: 24px; display: none;" />
                                <asp:Label ID="CUST_SUBS_ITEM_SK" runat="server" Style="display: none" Text='<%# Eval("CUST_SUBS_ITEM_SK") %>'></asp:Label>
                                <asp:Label ID="lblIMAGE_FILE_NAME" runat="server" Visible="false" Text='<%# Eval("IMAGE_FILE_NAME") %>'></asp:Label>
                                <img id="ClassCheckBoxImg" alt="" clientidmode="Static" style="float: left; margin-left: 24px;
                                    width: 20px; cursor: pointer;" onclick="javascript:CheckBoxImgClick(this);" src='<%# Page.ResolveUrl(Eval("CheckImgPathName").ToString()) %>' />
                            </div>
                            <div class="DashBoard_Items_books">
                                <asp:Image runat="server" ID="BookCoverImage" ClientIDMode="Static" CssClass="DashBoard_Items_books_images BooksImgClass"
                                    ImageUrl='<%# Eval("IMAGE_FILE_NAME")%>' />
                            </div>
                            <div style="width: 61%; float: right; margin-top: 25px;">
                                <div style="width: 100%; float: left">
                                    <asp:Label ID="BookNameLabel" runat="server" Style="color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text='<%# Eval("Title") %>'></asp:Label>
                                    <%--  <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">Years</span>
                                    <asp:Label ID="FromYearLabel" Style="color: #707070; font-size: 12pt; padding: 5px;
                                        font-family: Raleway-regular,Raleway, arial, sans-serif; font-weight: 700;" runat="server"
                                        Text="5"></asp:Label>
                                    <span style="color: #707070; font-size: 12pt; font-family: Raleway-regular,Raleway, Arial, Sans-Serif;
                                        font-weight: 700;">and</span>
                                    <asp:Label ID="ToYearLabel" runat="server" Style="padding: 5px; color: #707070; font-size: 12pt;
                                        font-family: Raleway-regular,Raleway, Arial, Sans-Serif; font-weight: 700;" Text="6"></asp:Label>--%>
                                    <br />
                                    <asp:Label ID="AuthorNameLabel" runat="server" Style="font-family: Arial-Regular, Sans-Serif;
                                        font-size: 10pt; color: #707070; margin-top: 5px; float: left;" Text='<%# Eval("Author") %>'></asp:Label>
                                </div>
                                <div style="font-family: Arial-Regular, Sans-Serif; width: 100%; float: left; margin-top: 5px;
                                    font-size: 10pt; color: #707070;">
                                    <asp:Label ID="ColourLabel" runat="server" Text='<%# Eval("ColourLevel") %>'></asp:Label>,
                                    <asp:Label ID="ReadingLevelLabel" runat="server" Text='<%#  string.Format("PM level {0}", Eval("ReadingLevel")) %>'></asp:Label>,
                                    <asp:Label ID="ReadingAgeLabel" runat="server" Text="Reading age: "><%# Eval("ReadingAge") %></asp:Label></br>
                                    <asp:Label ID="AttributeTypeLabel" runat="server" Text='<%# Eval("TEXTTYPE") %>'
                                        Style="float: left; margin-top: 5px;"></asp:Label>
                                    <asp:Label ID="YearLabel" runat="server" Style="float: left; margin-top: 5px; width: 100%"
                                        Text='<%#  string.Format("Copyright: {0}", Eval("COPYRIGHT_YEAR")) %>'></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <CP:CustomPaging ID="CustomPaging" runat="server">
                </CP:CustomPaging>
                <%--<center id="PagerHolder" class="Pager" style="min-width: 90%">
                    <div id="StudentPagerDiv" clientidmode="Static" style="width: 96%; float: right;
                        display: block; margin-top: 18px;" runat="server">
                        <div style="float: left; margin-left: 227px; width: 23.8%;">
                            <asp:Button ID="PreviousButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                border: 0px solid white; float: left; background: transparent;" OnClientClick="javascript:GetBckPageNumber();"
                                ClientIDMode="Static" runat="server" Text="Previous" OnClick="PreviousButton_Click">
                            </asp:Button>
                            <asp:Button ID="ShowNextButton" Style="cursor: pointer; font-family: Raleway; color: rgb(31, 181, 231);
                                margin-left: 7px; border: 0px solid white; float: left; background: transparent;"
                                OnClientClick="javascript:GetFwdPageNumber();" ClientIDMode="Static" runat="server"
                                Text="Next>>" OnClick="ShowNextButton_Click"></asp:Button>
                        </div>
                        <asp:HiddenField ID="pageNumber" ClientIDMode="Static" Value="1" runat="server" />
                    </div>
                </center>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

<input type="hidden" runat="server" clientidmode="static" id="pageurlhdn" />