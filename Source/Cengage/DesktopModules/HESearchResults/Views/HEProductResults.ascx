<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HEProductResults.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.HEProductResults"  EnableViewState="false" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %> 
<%@ Register Src="HEProductsPager.ascx" TagName="HEProductsPagers" TagPrefix="CP" %>
<dnn:DnnCssInclude runat="server" FilePath="Resources/Shared/scripts/Bootstrap/css/bootstrap.min.css" Priority="14" /> 
<dnn:DnnCssInclude runat="server" FilePath="DesktopModules/HESearchResults/CSS/HigherEducation.css?v=2" Priority="14" /> 
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/HESearchResults/Scripts/HESearch.js" ForceProvider="DnnFormBottomProvider"/> 
<asp:PlaceHolder ID="SearchPlaceHolder" runat="server">
<div id="SaveSearchPopUp1" class="HideItems save-search-name">
    <div class="favpopup">
        <h1>
            Save a search</h1>
        <div class="save-cont">
            <p>
                Please enter the name of your search:
            </p>
            <p>
                <label for="SearchNameTxt">
                    Name:
                </label>
                <input id="SearchNameTxt" type="text" class="txt" name="name" placeholder="i.e. wish-list, to booklist next year, save for later" />
            </p>
			<div id="SaveSearchNameerror" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span class="error-text">Please enter name of your search. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)"></span></div></div>
        </div>
		<div class="button">
            <button class="btn btn-cancel" type="button" id="CancelButtonA">
                Cancel</button>
            <button class="btn btn-affermative" type="button" id="SaveButtonA">
                Next</button>
        </div>
    </div>
</div>
<div id="SaveSearchPopUp2" class="HideItems save-search-result">
    <div class="favpopup SavePopup2">
        <h1>
            Save Search</h1>
        <div class="title">
            You are searching for:
        </div>
        <div class="cont">
            <p>
                <span class="savepopup2span">
                    Keywords:</span>
                <span id="SSKeywords"></span>
            </p>
            <p id="PgShowOnly" class="HideItems">
                <span class="savepopup2span">
                    Show Only:</span><span id="SSShowOnly"><span class="and">AND</span> </span> 
            </p>          
            <p id="PgTitle" class="HideItems">
                <span class="savepopup2span">
                    Title:</span>
                <span id="SSTitle" class="Title"></span>
            </p>
			<p id="PgAuthors" class="HideItems">
                <span class="savepopup2span">
                    Authors:</span>
                <span id="SSAuthors" class="Title"></span>
            </p>			
			<p id="PgFormat" class="HideItems">
               <span class="savepopup2span">
                    Format:</span>
                <span id="SSFormat" class="Title"><span class="and">AND</span></span>  
            </p>
			<p id="PgPublisher" class="HideItems">
                <span class="savepopup2span">
                    Publisher:</span>
                <span id="SSPublisher" class="Title"><span class="and">AND</span></span>  
            </p>
			<p id="PgPublishedYear" class="HideItems">
                <span class="savepopup2span">
                    Published Year:</span>
                <span id="SSPublishedYear" class="Title"><span class="and">AND</span></span>  
            </p>
			<p id="PgOrigin" class="HideItems">
              <span class="savepopup2span">
                    Origin:</span>
                <span id="SSOrigin" class="Title"><span class="and">AND</span></span>  
            </p>			
			<p id="PgIncluding" class="HideItems">
                <span class="savepopup2span">
                    Has all these words:</span>
                <span id="SSIncluding" class="Excluding"><span class="and">AND</span></span>  
            </p>
			<p id="PgExactWords" class="HideItems">
                <span class="savepopup2span">
                    Has these exact words:</span>
                <span id="SSExactWords" class="Excluding"><span class="and">AND</span></span>  
            </p>
            <p id="PgExcluding" class="HideItems">
                <span class="savepopup2span">
                    Has none of these words:</span>
                <span id="SSExcluding" class="Excluding"><span class="and">AND</span></span>  
            </p>			
            <p id="PgDisciplineCategory" class="HideItems">
                <span class="savepopup2span">
                    Discipline Category:</span>
                <span id="SSDisciplineCategory" class="Discipline"></span>
            </p>
            <p id="PgDiscipline" class="HideItems">
               <span class="savepopup2span">
                    Discipline:</span>
                <span id="SSDiscipline" class="Discipline"></span>
            </p>
            <p id="PgSubject" class="HideItems">
               <span class="savepopup2span">
                    Subject:</span>
                <span id="SSSubject" class="Discipline"></span>
            </p>
        </div>
        <div class="button">
            <button class="btn btn-cancel" type="button" id="CancelButtonB">Cancel</button>
            <button class="btn btn-affermative" type="button" id="SaveButtonB">Save</button>
        </div>
    </div>
</div>
<div id="SaveSearchAnonymous" class="HideItems save-search-name">
    <div class="favpopup">
        <h1>
            Save a search</h1>
        <div class="save-cont">
            <p>
                Please enter the name of your search:
            </p>
            <p class="remove-margin">
                <label for="AnonSaveSearchTxt">
                    Name:
                </label>
                <input id="AnonSaveSearchTxt" type="text" class="txt" name="name" placeholder="i.e. wish-list, to booklist next year, save for later" />
            </p>
			<div id="AnonSaveSearchNameerror" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span class="error-text">Please enter name of your search. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)"></span></div></div>        
        </div>
		<div class="fav-txt">
            <p>
                To save a search you must first be logged into your user account.</p>
            <p>
                Creating an account is free and easy.</p>
        </div>
        <div class="button">
            <button class="btn btn-affermative" type="button" id="SaveSearchLoginBtn">
                Login</button>
            <span>OR</span>
            <button class="btn btn-affermative" type="button" id="SaveSearchSignUpBtn">
                Sign up</button>
        </div>
    </div>
</div>
<div id="FavoritePopUp" class="HideItems save-search-result">
    <div class="favpopup">
        <h1>
            Mark as a favourite</h1>
        <div class="fav-txt">
            <p>
                To mark a title as favourite you must first be logged into your user account.</p>
            <p>
                Creating an account is free and easy.</p>
        </div>
        <div class="button">
            <button class="btn btn-affermative" type="button" id="FavLoginBtn">
                Login</button>
            <span>OR</span>
            <button class="btn btn-affermative" type="button" id="FavSignUpBtn">
                Sign up</button>
        </div>
    </div>
</div>
<div id="MoreFavoritePopUp" class="HideItems save-search-result">
    <div class="fav-limit-reached">
        <h1>
            Mark as a favourite</h1>
        <div class="fav-txt">
            <div class="txt">
                <p>
                    You already have marked 50 products as favourites.</p>
            </div>
        </div>
        <div class="signup">
            <button class="btn btn-affermative" type="button" id="MoreFavOkBtn">
                Ok</button>
        </div>
    </div>
</div>
<div id="MoreSaveSearch" class="HideItems save-search-result">
    <div class="fav-limit-reached">
        <h1>
            Save a Search</h1>
        <div class="fav-txt">
            <div class="txt">
                <p>
                    You already have saved 50 searches.</p>
            </div>
        </div>
        <div class="signup">
            <button class="btn btn-affermative" type="button" id="MoreSearchOkBtn">
                Ok</button>
        </div>
    </div>
</div>
<div id="overlay"  onclick="CloseMultipleIsbnSearch()"></div>
	<div id="MultipleISBNPopup" class="HideItems">
        <div class="isbn-popup">
            <div class="innerdiv1">
                <h1 class="countryLabel">
                    Search multiple ISBNs</h1>
            </div>
            <div class="innerdiv2">
                <div class="contentdiv">
                    <div class="paddingTB5 marginTB5">
                        <span class="msg">Please upload multipleISBN's via a CSV file or an Excel spreadsheet
                            where the first column (A) contains only the ISBN's</span>
                    </div>
                    <div class="padding5 margin5">
                        <input id="MultipleISBNBrowseBtn" type="button" class="btn btn-general" value="Upload file (.xls,.xlxs or .csv)" /><span id="MultipleISBNBrwsVal"></span>
                        <asp:FileUpload ID="MultipleISBNBrowse" runat="server" onchange="SetFileName(this.value)" ClientIDMode="Static"  CssClass="btn btn-general" style="display:none;" />
						<br />
                    </div>
					<div id="MultiConfirmBtnDiv" class="HideItems">
                       <input id="MultiIsbnCancelBtn" type="submit" class="btn btn-cancel" value="Cancel" />
                       <input type="button" id="MultiIsbnConfirmBtn" class="btn btn-affermative" value="Confirm"/>
                    </div>
                    <div class="paddingTB5 marginTB5">
                        <span class="lightbold">Or key in the ISBNs manually below (seperate each ISBN with a commer):</span>
						<label for="Isbntextarea" class="HideItems">Or key in the ISBNs manually below (seperate each ISBN with a commer):</label>
                    </div>                    
					<asp:TextBox ID="Isbntextarea" CssClass="msg-content" placeholder="i.e.  9876543212341, 9765456765431, 9765435211672" runat="server" Rows="100" Columns="78" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                    <div id="MultiIsbnError" class="alert alert-primary HideItems"><div><span class="ico-error pull-left"></span> <span class="error-text">Please upload excel/csv file and confirm or enter IBSNs to search. </span><span class="ico-error-close pull-right" onclick="closeAlertMsg(this)"></span></div></div>
					<div class="pop-btn">
                       <input id="MultiISBNDismiss" type="button" class="btn btn-cancel" value="Dismiss" onclick="CloseMultipleIsbnSearch()" />
                          <input type="button" id="MultiISBNSearchBtn" class="btn btn-affermative" value="Search"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
<div class="search-container">
    <div class="row">
        <div class="col-md-12">
            <!-- Search Bar-->
        </div>
        <!--Left Nav -->
        <div class="left-menu">
            <div class="accordion" id="leftMenu">
                <div class="accordion-group noborder-top">
                    <div class="accordion-heading">
                        <%--<span class="ico-caret"></span>--%>
                        <div id="ShowOnlyOptions" class="accordion-toggle" data-parent="#leftMenu" >Search Options:</div>
                    </div>
                    <div id="collapseTwo" class="accordion-body collapse in">
                        <div class="accordion-inner">
                            <ul>
                               <li>
                                    <label for="ShowOnlyNew">
                                        <span id="ShowOnlyChk1" class="ico-uncheck ItemsHide"></span>
                                        <input id="ShowOnlyNew" class="pull-left" type="checkbox" value="1" onclick="ShowOnlyFilter(1,this)"/>
                                        <span class="HideItems">NEW</span> <span class="checkbox-ellipsis">Show only New</span></label>
                                </li>
                                <li>
                                    <label for="ShowOnlyAudienceCode">
                                        <span id="ShowOnlyChk2" class="ico-uncheck ItemsHide"></span>
                                        <input id="ShowOnlyAudienceCode" class="pull-left" type="checkbox" value="2" onclick="ShowOnlyFilter(2,this)"/>
                                        <span class="HideItems">AUDIENCE</span>  <span class="checkbox-ellipsis">Show only AUS / NZ</span>
                                    </label>
                                </li>
                                <li class="spacer">
                                    <label for="ShowOnlyPublished">
                                        <span id="ShowOnlyChk3" class="ico-uncheck ItemsHide"></span>
                                        <input id="ShowOnlyPublished" class="pull-left" type="checkbox" value="3" onclick="ShowOnlyFilter(3,this)"/>
                                        <span class="HideItems">PUBLISHER</span> <span class="checkbox-ellipsis">Show only Published</span></label>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="accordion-group">
                    <div class="accordion-heading">
                        <%--<span class="ico-caret"></span>--%>
                        <div id="DisciplineHierarchyFilter" class="accordion-toggle" data-parent="#leftMenu" >
                           <span class="Narrow">Narrow by discipline:</span>
						   <a href="#" class="cleartxt" onclick='ClearHierarchy()'>clear</a>
                        </div>
                    </div>
                    <div id="collapseThree" class="accordion-body collapse in" >
                        <div class="accordion-inner">
                            <ul>
                                <asp:Repeater ID="DisciplineCategoryRptr" runat="server" OnItemDataBound="DisciplineCategoryRptr_ItemDataBound">
                                    <ItemTemplate>
                                        <li id='Lidc<%# Eval("Discipline_Category_Name_Sk") %>' class="discipline-heading discipline-accordian" title='<%# Eval("Discipline_Category_Name").ToString().Replace("'","") %>'><a id='dc<%# Eval("Discipline_Category_Name_Sk") %>' href="#collapseCategory<%# Eval("Discipline_Category_Name_Sk")%>"
                                            data-toggle="collapse" data-parent="#leftMenu" class="rjsearchtxthide dclink accordion-toggle"><span class="ico-toggle-close">a
                                                                </span></a><span class="hierarchytext" onclick='SetCategoryFilter(<%# Eval("Discipline_Category_Name_Sk") %>)'>
                                               <span class="headtitle"><%# Eval("Discipline_Category_Name")%></span>
                                               <span> (<%# Eval("Products_Count")%>)</span></span>
                                            <span id='cat<%# Eval("Discipline_Category_Name_Sk") %>' class="HideItems"><%# Eval("Discipline_Category_Name")%></span>   
                                            <asp:HiddenField ID="DisciplineCategorySkHdn" runat="server" Value='<%# Eval("Discipline_Category_Name_Sk") %>' />
                                        </li>
                                        <li id="collapseCategory<%# Eval("Discipline_Category_Name_Sk")%>" class="accordion-body collapse">
											<ul class="subdis accordion-inner">
												<asp:Repeater ID="DisciplineRptr" runat="server" OnItemDataBound="DisciplineRptr_ItemDataBound">
													<ItemTemplate>
														<li id='Lid<%# Eval("Discipline_Category_Name_Sk") %>d<%# Eval("Discipline_Sk")%>' title='<%# Eval("Discipline").ToString().Replace("'","")%>' class="discipline-accordian"><a id='dc<%# Eval("Discipline_Category_Name_Sk") %>d<%# Eval("Discipline_Sk")%>' href="#collapseDiscipline<%# Eval("Discipline_Category_Name_Sk")%><%# Eval("Discipline_Sk")%>"
															data-toggle="collapse" data-parent="#leftMenu" class="rjsearchtxthide dlink accordion-toggle"><span class="ico-toggle-close">a
															</span></a><span class="hierarchyinnertext" onclick='SetDisciplineFilter(<%# Eval("Discipline_Category_Name_Sk") %>,<%# Eval("Discipline_Sk")%>)'>
															<span class="head-subtitle">	<%# Eval("Discipline")%></span>
															<span>	(<%# Eval("Products_Count")%>)</span>  </span>                                                              
															<span id='cat<%# Eval("Discipline_Category_Name_Sk") %>d<%# Eval("Discipline_Sk")%>' class="HideItems"><%# Eval("Discipline")%></span>                                                                                    
															<asp:HiddenField ID="DisciplineSkHdn" runat="server" Value='<%# Eval("Discipline_Category_Name_Sk")+"|"+Eval("Discipline_Sk") %>' />
														</li>
														<li id="collapseDiscipline<%# Eval("Discipline_Category_Name_Sk")%><%# Eval("Discipline_Sk")%>"
															class="accordion-body collapse">
															<ul class="subdis customscrollbar accordion-inner ">
																<asp:Repeater ID="SubjectRptr" runat="server">
																	<ItemTemplate>
																		<li id='Lis<%# Eval("Discipline_Category_Name_Sk") %>d<%# Eval("Discipline_Sk")%>s<%# Eval("Subject_Sk")%>' title='<%# Eval("Subject_Name").ToString().Replace("'","")%>' class="subli discipline-accordian"  onclick='SetSubjectFilter(<%# Eval("Discipline_Category_Name_Sk") %>,<%# Eval("Discipline_Sk")%>,<%# Eval("Subject_Sk")%>)'>
																		<a href="#" class="disciplinesubjectalink"><span class="subjectellipsis">
																			<%# Eval("Subject_Name")%></span> 
																			<span  class="subjectcount" onclick='SetSubjectFilter(<%# Eval("Discipline_Category_Name_Sk") %>,<%# Eval("Discipline_Sk")%>,<%# Eval("Subject_Sk")%>)'>(<%# Eval("Products_Count")%>)</span> </a> 
																		 <span id='cat<%# Eval("Discipline_Category_Name_Sk") %>d<%# Eval("Discipline_Sk")%>s<%# Eval("Subject_Sk")%>' class="HideItems"><%# Eval("Subject_Name")%></span>   
																		 </li>
																	</ItemTemplate>
																</asp:Repeater>
															</ul>
														</li>
													</ItemTemplate>
												</asp:Repeater>
											</ul>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="FacetFilterDiv">
                    <asp:Repeater ID="FacetsRepeater" runat="server" OnItemDataBound="FacetsRepeater_ItemDataBound">
                        <ItemTemplate>
                            <div id="SearchFacetParent" runat="server" class="accordion-group">
                                <div class="accordion-heading">
                                    <a id='FacetAnchor<%# Eval("ATTRIBUTE_TYPE_SK") %>'
                                        class="accordion-toggle" data-toggle="collapse" data-parent="#leftMenu" href='#Collapse<%# Eval("ATTRIBUTE_TYPE_SK") %>'>
                                    <span class="ico-toggle-close"></span>
									
                                        <%# Eval("ATTRIBUTE_NAME") %>
                                        (<span id='FacetProductsCount<%# Eval("ATTRIBUTE_TYPE_SK") %>'><%# Eval("PROD_COUNT") %></span>)</a><a href="#" class="cleartxt" onclick='CLearFacetFilter(<%# Eval("ATTRIBUTE_TYPE_SK") %>)'>clear</a>
                                    <asp:HiddenField ID="AttrTypeSkHdn" runat="server" Value='<%# Eval("ParentFacetTitle") %>' />
                                </div>
                                <div id='Collapse<%# Eval("ATTRIBUTE_TYPE_SK") %>' class="accordion-body collapse">
                                    <div class="accordion-inner">									
                                        <ul class="customscrollbar">
                                            <asp:Repeater ID="FacetValuesRepeater" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <label for='<%# Eval("ATTRIBUTE_TYPE_SK") %><%# Eval("ATTRIBUTE_TYPE_value_SK") %>'>
                                                            <span id='FacetChk<%# Eval("ATTRIBUTE_TYPE_SK") %><%# Eval("ATTRIBUTE_TYPE_value_SK") %>' class="ico-uncheck ItemsHide"></span>
															<input id='<%# Eval("ATTRIBUTE_TYPE_SK") %><%# Eval("ATTRIBUTE_TYPE_value_SK") %>' class="pull-left"
                                                                type="checkbox" value='<%# Eval("ChildFacetTitle") %>'
                                                                onclick='SetFacetFilter(<%# Eval("ATTRIBUTE_TYPE_SK") %>,<%# Eval("ATTRIBUTE_TYPE_SK") %><%# Eval("ATTRIBUTE_TYPE_value_SK") %>,FacetChk<%# Eval("ATTRIBUTE_TYPE_SK") %><%# Eval("ATTRIBUTE_TYPE_value_SK") %>)' title='<%# Eval("ATTRIBUTE_TYPE_VALUE") %>'/>
                                                            <span class="checkbox-ellipsis"><%# Eval("ATTRIBUTE_TYPE_VALUE") %></span>
                                                            <span>(<%# Eval("PROD_COUNT")%>)</span>
															<span class="HideItems"><%# Eval("ATTRIBUTE_NAME") %></span></label></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <!-- Left Nav End-->
        <div class="right-menu">
            <div class="tabbable">
                <!-- Only required for left/right tabs -->
                <div class="nav1">
                    <ul class="nav nav-tabs">
                        <li class="active"><a id="productstab" href="#tab1" data-toggle="tab">Products</a></li>
                        <li><a id="infotab" href="#tab2" data-toggle="tab">Information</a></li>
                    </ul>
                </div>
                <div class="nav2">
                    <div class="asearch" id="tab3"><a href="#" class="adSearch"> Advanced search</a> </div>                   
					<div class="btn-group rec">						
						<span>Sort results:</span>		
						<label for="ProductsSortDpdwn" class="HideItems">Sort Results:</label>				
						<select name="ProductsSortDpdwn" id="ProductsSortDpdwn">
							<option value="R">Recommended</option>
							<option value="A">Title (A to Z)</option>
							<option value="D">Title (Z to A)</option>
							<option value="L">Latest</option>
						</select>
					</div>
                </div>
                <div class="tab-content hefullwidth">
                    <div id="pagerinfobox">
						<div id="HasResultDiv" class="floatLeft">
							<span><small>Results <strong><span id="CurrentPgStartSizeSpan"></span> - <span id="CurrentPgEndSizeSpan">
							</span></strong></small></span><span><small> of <span id="TotalResultSpan"></span> in
							</small></span><span><strong><span class="DivisionLbl"></span></strong>
							</span>
                        </div>
                        <div id="HasNoResultDiv" class="floatLeft HideItems">
							<span><small>0 Results found </small></span><span><small>in </small></span><span><strong>
								<span class="DivisionLbl"></span></strong>
							</span>
                        </div>
                        <div class="floatright">
							<span id="MulpleIsbnBtn1" class="multiisbn" onclick="OpenMultipleIsbnSearchPopUp()">Search for multiple ISBNs</span><span
								class="right-text SSBtn" id="SaveSearchBtn1"><span class="ico-savesearch"></span>
								Save Search</span>
						</div>
                    </div>
                    <div id="AdvanceSearchDiv" class="span6 HideItems">
                        <div class="search-top">
                            <div class="findTxt">
                                Find results with:</div>
                            <div class="clearTxt" id="clearAdvSearch">
                                clear & close <span class="ico-clearclose"></span></div>
                            <div class="search_nav">
                                <div class="rows">
                                    <label for="AdvTitleTxt">
                                        Title:</label>
                                    <span>
                                        <input id="AdvTitleTxt" type="text" name="Title" class="txt" maxlength="50"  /></span>
                                </div>
                                <div class="rows">
                                    <label for="AdvAuthorTxt">
                                        Author:</label>
                                    <span>
                                        <input id="AdvAuthorTxt" type="text" name="Author" class="txt" maxlength="50" /></span>
                                </div>
                                <div class="rows">
                                    <label for="AdvSubjectTxt">
                                        Subject:</label>
                                    <span>
                                        <input id="AdvSubjectTxt" type="text" name="Subject" class="txt" maxlength="50"  /></span>
                                </div>
                                <div id="AttributeSet">
                                    <div class="rows"> 
										<div class="recommand">
											<label for="AdvAttrTypesDrpDwn0" class="HideItems">Filter results:</label>		
											 <asp:DropDownList ID="AdvAttrTypesDrpDwn0" runat="server" ClientIDMode="Static" DataTextField="ATTRIBUTE_NAME" 
												DataValueField="ATTRIBUTE_TYPE_SK" CssClass="rec-dropdown" AppendDataBoundItems="true">
                                                <asp:ListItem Text="Choose" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
											<input type="text" placeholder="" class="iconinput" maxlength="50" />
										</div>
                                    </div>
                                </div>
                                <div id="FilterSet">
                                    <div class="fillterTxt">
                                        Filter results:</div>
									<label for="FilterResultsDrpDwn0" class="HideItems">Filter results:</label>			
                                    <div class="rows">
                                       <div class="recommand">
											<select id="FilterResultsDrpDwn0"  class="rec-dropdown">
												<option value="0">Choose</option>
												<option value="1">All these words</option>
												<option value="2">This exact phrase</option>
												<option value="3">None of these words</option>
											</select>
											<input type="text" placeholder="" class="iconinput" maxlength="50" />
										</div> 
                                    </div>
                                </div>
                                <div class="pull-right advanced-search-btn">
                                    <button type="button" class="btn btn-affermative upper-case" onclick="AdvanceSearch()">
                                        Search</button>
                                </div>
                            </div>
                        </div>
                        <input type="reset" id="AdvResetBtn" class="HideItems" value="Reset"/>
                    </div>
                    <div class="tab-pane active" id="tab1">
                        <!-- Product Tab -->
                        <asp:PlaceHolder ID="ResultCountPlaceHldr" runat="server">
                            <asp:Label ID="ProdCurrentPgStartSizeLbl" CssClass="HideItems" runat="server" ClientIDMode="Static"></asp:Label>
                                <asp:Label ID="ProdCurrentPgEndSizeLbl" CssClass="HideItems" runat="server" ClientIDMode="Static"></asp:Label>
                                <asp:Label ID="ProdTotalResultLbl" CssClass="HideItems" runat="server" ClientIDMode="Static"></asp:Label>
                                <asp:Label ID="ProdDivisionLbl" CssClass="HideItems" runat="server" ClientIDMode="Static"></asp:Label>
						</asp:PlaceHolder>
                        <asp:PlaceHolder ID="NoResultPlaceHldr" runat="server"></asp:PlaceHolder>
                        <asp:Repeater ID="ProductResultsRptr" runat="server">
                            <ItemTemplate>
                                <div class="span6">
                                    <div class="img">
                                        <asp:HyperLink ID="ProductImageLink" runat="server" ToolTip='<%# Eval("ToolTip")%>' CssClass="linktextnodec" NavigateUrl='<%# Eval("DetailUrl") %>'>
                                            <img alt='<%# Eval("ToolTip")%>' src='<%# Eval("IMAGE_FILE_NAME") %>' class="avatar" onError="this.onerror=null;this.src='<%# string.Concat(FormatImageURL(),"HERnoimage.png") %>';" />
											</asp:HyperLink>
                                    </div>
									<div class="product-height">
                                            <div class="product_name auto_ellipse">
                                                <asp:HyperLink ID="ProductTitleLink" ToolTip='<%# Eval("ToolTip")%>' CssClass="linktextnodec"
                                                    runat="server" NavigateUrl='<%# Eval("DetailUrl") %>'>
                                            <h2>
                                                <%# Eval("TITLE")%></h2></asp:HyperLink>                                                
                                               <%# Eval("AUDIENCE_TARGET").ToString() == "" ? string.Empty : BindHtml("AUDIENCE_TARGET", Eval("AUDIENCE_TARGET").ToString(),string.Empty)%>                                                
                                               <%# Eval("NEW_EDITION").ToString() == "N" ? string.Empty : BindHtml("NEW_EDITION", "New", string.Empty)%>
                                            </div>
                                            <div class=" publisher pull-left">
                                                <strong>Published: </strong><small>
                                                    <%# Eval("PUBLICATION_DATE")%></small> | <strong>ISBN </strong><small class="IsbnSmall">
                                                        <%# Eval("ISBN_13")%></small>                                                
                                                <%# Eval("EDITION").ToString() == "" ? string.Empty : BindHtml("EDITION", Eval("EDITION").ToString(), string.Empty)%>
                                            </div>
                                             <%# Eval("PREFERRED_NAME").ToString() == "" ? string.Empty : BindHtml("PREFERRED_NAME", Eval("PREFERRED_NAME").ToString(), string.Empty)%>
                                            <div class="supplementsblock">                                              
                                                <%# Eval("SUPPLEMENTS").ToString() == "N" ? string.Empty : BindHtml("SUPPLEMENTS", "available", string.Empty)%>                                               
                                                <%# Eval("CODE").ToString() == "" ? string.Empty : BindHtml("CODE", Eval("CODE").ToString(), Eval("CODE_NAME").ToString())%>
                                            </div>
                                        </div>
                                        
                                        <div class="pricelist">
                                            <%# Eval("PRINT_PRICE").ToString() == "" ? string.Empty : BindHtml("PRINT_PRICE", Eval("PRINT_PRICE").ToString(), Eval("PRODUCT_FORMAT").ToString())%>
                                            <%# Eval("EBOOK").ToString() == "" ? string.Empty : BindHtml("EBOOK", Eval("EBOOK").ToString(), Eval("SUBPRODUCT_TYPE").ToString()=="EBK"?"eBook From":"eBook")%>
                                            <%# Eval("ECHAPTER").ToString() == "" ? string.Empty : BindHtml("ECHAPTER", "Available", string.Empty)%>
                                            
                                            <%# Eval("PriceVal").ToString() == "" ? string.Empty : BindLoggedInHtml("PriceVal", Eval("PriceVal").ToString(), "RRP", Eval("ISBN_13").ToString(),"")%>
                                             
                                            <%# Eval("DiscountRate").ToString() == "-1"? string.Empty : BindLoggedInHtml("DiscountRate", Eval("DiscountRate").ToString(), "Discount", Eval("ISBN_13").ToString(),"")%>
                                            <%# Eval("DiscountVal").ToString() == "-1" ? string.Empty : BindLoggedInHtml("DiscountVal", Eval("DiscountVal").ToString(), "Total", Eval("ISBN_13").ToString(),"")%>
											
                                            <%# Eval("Stockqty").ToString() == "-1" ? string.Empty : BindLoggedInHtml("Stockqty", Eval("Stockqty").ToString(), "Available stock", Eval("ISBN_13").ToString(), Eval("AllowSale").ToString())%>
                                        </div>
										
                                        <%# Eval("FAVOURITE_FLAG").ToString() == "" ? string.Empty : ShowFavourite("ShowFavourite", Eval("PRODUCT_SK").ToString(), Eval("FAVOURITE_FLAG").ToString(), "FavSpan")%>
                                      
									
									
									
									
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>    										
                        <!-- Product Tab End -->
                    </div>
                    <div class="tab-pane" id="tab2">
                        <asp:PlaceHolder ID="CmsPgResPlaceHldr" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>
    </div>  
</div>
<div id="ProductsPagerDiv">
<CP:HEProductsPagers ID="HEProductsPagers" runat="server">
						</CP:HEProductsPagers>		
</div>
<div id="InformationPagerDiv" class="he-srrefdiv">
<div id="InfoPagerHolder" class="he-Pager"></div>
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="InitialNoResultpHldr" runat="server"></asp:PlaceHolder>
<input id="SearchTextHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="DivisionHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="ProductsPageNumberHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="CMSPageNumberHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="LoadCMSHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="NoOfResultsHdn" type="hidden" runat="server" clientidmode="Static" />
<input id="HierarchyFilterHdn" type="hidden" runat="server" clientidmode="Static" />
<asp:HiddenField ID="SNewHdn" runat="server" ClientIDMode="Static"/>
<asp:HiddenField ID="SAudienceHdn" runat="server" ClientIDMode="Static"/>
<asp:HiddenField ID="SPublishedHdn" runat="server" ClientIDMode="Static"/>

