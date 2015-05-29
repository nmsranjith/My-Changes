<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolsProductResults.ascx.cs" Inherits="DotNetNuke.Modules.HESearchResults.Views.SchoolsProductResults" %>
<%@ Register Src="SchoolsProductsPager.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<%@ Register Src="SchoolsCMSPager.ascx" TagName="CmsPaging" TagPrefix="CP" %>
<link href="<%=Page.ResolveUrl("Resources/Shared/scripts/jquery/jquery.mCustomScrollbar.css")%>"
    rel="stylesheet" type="text/css" />
  <script src="/Resources/Shared/scripts/kendoui/latest/kendo.web.min.js"
    type="text/javascript"></script>
<link href="DesktopModules/HESearchResults/CSS/schools.css?v=6" rel="stylesheet" type="text/css" />
<asp:PlaceHolder ID="ProductPlace" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
<input type="hidden" runat="server" id="didumeanword" clientidmode="Static" />
<div id="msglbl" runat="server" clientidmode="Static" style="display: none;">
    <div id="MsgDiv" runat="server" clientidmode="Static" class="dnnFormWarning dnnFormMessage">
        <asp:Panel runat="server" ID="pl_warning" Visible="false" class="dnnFormMessage dnnFormWarning">
            <asp:Literal ID="lit_warning" runat="server" />
        </asp:Panel>
    </div>
</div>
<div class="outer" id="MainHold" clientidmode="Static" runat="server">
    <div class="lftMain">
        <asp:Repeater ID="LeftmainHeading" runat="server" OnItemDataBound="LeftmainHeading_ItemDataBound"
            EnableViewState="false">
            <ItemTemplate>
                <div class="srchfdivml dsource_cs">
                    <img src="<%=Page.ResolveUrl("Portals/0/images/SampleChapter.png")%>" alt=" " class="PrdcHPIMG" />
                    <div class="PrdChpDiv">
                        <h4 class="Hfcl">
                            <asp:Label ID="MainTitle" runat="server" EnableViewState="false"
                                ToolTip='<%# Eval("key").ToString().Split("|".ToCharArray())[0] %>' Text='<%# Eval("key").ToString().Split("|".ToCharArray())[1] %>'> 
                            </asp:Label>
                            <%--<div class="sr_clear">
                            </div>--%>
                        </h4>
                    </div>
                    <div class="scrolldiv scrolldivheight mCustomScrollBox mCS-light">
                        <div class="mCSB_container">
                            <asp:HiddenField ID="Multiselect" runat="server" EnableViewState="false" />
                            <asp:Repeater ID="leftSubHeading" runat="server" EnableViewState="false" OnItemDataBound="leftSubHeading_ItemDataBound">
                                <ItemTemplate>
                                    <div class="ThirdCl ThirdDiv_cs">
                                        <div class="LeftMenu chbox_cs">
                                            <asp:CheckBox ID="LeftMenuChk" EnableViewState="false" runat="server" ToolTip='<%# Eval("ATTRIBUTE_TYPE_VALUE_SK") %>'
                                                Text='<%# Eval("ATTRIBUTE_TYPE_VALUE") %>' Checked='<%# (Eval("IS_SELECTED").ToString() == "N") ? false : true %>'
                                                Style="display: none;" CssClass="testatt" />
                                            <input type="hidden" 
                                                name='<%# String.Format("{0}_AttributeInfo",Eval("ATTRIBUTE_TYPE_SK"))%>' value='<%# String.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}", Eval("ATTRIBUTE_TYPE_SK"), Eval("ATTRIBUTE_TYPE_VALUE_SK"),Eval("ATTRIBUTE_NAME"),Eval("ATTRIBUTE_TYPE_VALUE"),Eval("PROD_COUNT"),Eval("IS_MULTI_SELECT"),Eval("SEQNUM")) %>' />
                                                <%--<div class="sr_clear">
                                                </div>--%>
                                            <img id="unchkboxs" runat="server" onclick="FireCheckedEvent(this);"
                                                class="chkbox Chbs unchkboxs_cs" alt=" " />    
                                                <%--<div class="sr_clear">
                                                </div>--%>
                                            <%--<input type="hidden" value='<%# Eval("ATTRIBUTE_TYPE_SK") %>' id="parentsk" />--%>
                                            <asp:HiddenField ID="parentsk" runat="server" EnableViewState="false" Value='<%# Eval("ATTRIBUTE_TYPE_SK") %>' />
                                        </div>
                                        <div class="lefttext">
                                            <asp:Label ID="SubTitle" runat="server" EnableViewState="false" CssClass="textellipsislf"
                                                ToolTip='<%# String.Format("{0} ({1})", Eval("ATTRIBUTE_TYPE_VALUE"), Eval("PROD_COUNT")) %>'
                                                Text='<%# String.Format("{0} ({1})", Eval("ATTRIBUTE_TYPE_VALUE"), Eval("PROD_COUNT")) %>'>
                                            </asp:Label>
                                            <asp:HiddenField ID="Leftsubname" runat="server" EnableViewState="false" Value='<%# Eval("ATTRIBUTE_TYPE_VALUE_SK") %>' />
                                            <asp:HiddenField ID="Leftparentname" runat="server" EnableViewState="false" Value='<%# Eval("ATTRIBUTE_TYPE_SK") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="ktab">
        <div class="sepLine">
            <img id="separrow" alt="separrow" src="<%=Page.ResolveUrl("Portals/0/images/separatorarrow.PNG")%>"
                class="Imgsep" />
        </div>
        <div id="tabstrip1" class="tab_1">
            <div id="cntDiv" class="cntDiv">
                <div class="cntdivchld">
                    <div id="productstab" class="tabPR H4Light">
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnselectedvalue" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnNonOutofStockSelectedValues" />
                        <span class="tabspan"><b>(<span id="PrdCount" runat="server" clientidmode="Static"></span>)</b>
                            <span class="tabhead">PRODUCTS</span></span></div>
                    <div class="tabLtit">
                        <div id="pagestab" class="tabPG H4Light">
                            <span class="tabspan"><b>(<span id="PagesCount" runat="server" clientidmode="Static"></span>)</b><span
                                class="tabhead">&nbsp;PAGES</span></span></div>
                    </div>
                </div>
				
				
                <div class="tabRtit">
					
                    <div id="Div3" class="addtolstdiv H5 Addtolists" style="display: none;">
                        <label class="star">
                            Order by :
                        </label>
                        <asp:DropDownList ID="Selectsort" runat="server" ClientIDMode="Static" CssClass="H5 dropdwnlst">
                            <asp:ListItem Value="R">Recommended</asp:ListItem>
                            <asp:ListItem Value="A">Title (A to Z)</asp:ListItem>
                            <asp:ListItem Value="D">Title (Z to A)</asp:ListItem>
                            <asp:ListItem Value="L">Latest</asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField ID="SortOrderHidden" runat="server" />
                    </div>
					
					<div class="isbn" onclick="OpenMultipleIsbnSearchPopUp()">
                        <span id="MulpleIsbnBtn1">Search for multiple ISBNs</span>
                    </div>
                </div>
				
                <br />
                <div id="searchfilterdiv" class="updPrt">
                    <div>
                        <ul id="SelectedAttributeList" runat="server" clientidmode="Static">
                        </ul>
                    </div>
                </div>
            </div>
            <div id="tab1" class="ktab1">
                <div id="tab1subtab">
                    <div id="austra1" class="mainCont">
                        <div id="searAct" class="searAct">
                            <div class="selSer" id="selectall">
                                <input id="MainChk" runat="server" clientidmode="Static" type="checkbox" style="display: none;" />
                                <img id="ImgSelect" src="<%=Page.ResolveUrl("Portals/0/images/pagebk.png")%>" alt=""
                                    class="chkboxmain chkf" />
                                <div class="qtydiv">
                                    <span id="selqty" runat="server" clientidmode="Static" class="Seldiv">0</span><span
                                        class="Spanfsz">Selected</span>
                                </div>
                            </div>
                            <div class="prntdiv H5">
                                <span id="Span2" class="prnt" onclick="window.print();">Print</span>
                            </div>
                            <div>
                                <div id="addedLabel" runat="server" clientidmode="Static" class="Addedtolists H5">
                                    <span class="spanadd">ADDED TO LIST</span>
                                </div>
                                <div id="addtolstdiv1" runat="server" clientidmode="Static" style="display: none;"
                                    class="addtolstdiv H5 Addtolitss">
                                    <img src="<%=Page.ResolveUrl("Portals/0/images/star1.png") %>" alt="" class="start" />
                                    <asp:DropDownList ID="HeaderAddtoList" onchange="javascript:if(!Onchanged())return false"
                                        runat="server" ClientIDMode="Static" CssClass="H5 dropdwnlst">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="UserIDHiddenField"></asp:HiddenField>
                                </div>
                            </div>
                            <div class="H5 Marleft">
                                <div id="QuoteAdded" runat="server" clientidmode="Static" class="Addedtolist H5 Wauto">
                                    <span class="Spanau">ADDED TO QUOTE</span>
                                </div>
                                <div id="kendo_AddtoList">
                                    <div id="AddtoQuoteDiv" runat="server" clientidmode="Static" class="addtolstdiv H5 addqu">
                                        <img src="<%=Page.ResolveUrl("Portals/0/images/jobdetail_icon1.png") %>" alt="" class="start" />
                                        <asp:DropDownList ID="AddtoQuote" runat="server" ClientIDMode="Static" onchange="javascript:if(!Onchanged())return false"
                                            CssClass="H5 dropdwnlst" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnusersk" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnrole" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnLoginTPsk" runat="server" ClientIDMode="Static" />
                            <div id="AddedToCart" runat="server" clientidmode="Static" class="headeradded">
                                <span class="Spancart">ADDED TO CART</span>
                            </div>
                            <div id="CartAdd" runat="server" clientidmode="Static" style="display: none;" class="H5 Camarl">
							<img src="/Portals/0/images/Tray_kendo.png" class="start">
                                <a onclick="GetSelectedISBN()" id="addtocartbtn" class="H5 AdtoCartbtn Btnca">Add to
                                    cart</a>
                            </div>
                        </div>
                        <div id="searDet" class="searDet">
                            <asp:Repeater ID="rdProducts" runat="server">
                                <ItemTemplate>
                                    <div class="sdDet">
                                        <div class="chkqtydiv">
                                            <div class="col1 chbox_cs">
                                                <input id="checkbox1" runat="server" checked='<%# Eval("Chkstatus")%>'
                                                    type="checkbox" style="display: none;" />
                                                <input id='<%# Eval("Isbn13")%>' value='<%# String.Format("{0}-1-{1}-{2}-{3}-{4}",Eval("Isbn13"),Eval("AllowSale"),Eval("StockAvail"),Eval("ProductSK"),Eval("IsCachedPrice")==null?"Y":Eval("IsCachedPrice"))%>'
                                                    type="hidden" class="allisbn" />
                                                <img class="chkboxline Imgp" alt="" src="<%=Page.ResolveUrl("Portals/0/images/pagebk.png") %>" />
                                            </div>
                                            <div class="col2">
                                                <div id="Div13<%# Container.ItemIndex %>" class="divtc">
                                                    <div class="roundedtextboxdiv martop">
                                                        <input type="hidden" id="<%# String.Format("ipadtit{0}",Eval("Isbn13"))%>" value="<%# Eval("Title") %>" />
                                                        <asp:TextBox ID="TextBox1" CssClass="intxt numeronly QtyTxt" Text='<%# Eval("QtyVal") %>'
                                                            onchange=<%# String.Format("return QtyTxtchange(this,'{0}')",Eval("Isbn13")) %>
                                                            onblur=<%# String.Format("return calculateTotal(this,'{0}','{1}','{2}','{3}','{4}')",Eval("Isbn13"),Eval("AllowSale"),Eval("StockAvail"),Eval("ProductSK"),Eval("IsCachedPrice")==null?"Y":Eval("IsCachedPrice")) %>
                                                            runat="server" ClientIDMode="Static" MaxLength="3"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col3">
                                            <div class="coltdc">
                                                <div class="divmind auto_ellipse">
                                                    <h4 class="hfs " title='<%# Eval("Title")%>'>
                                                        <a href="<%# Eval("Exacturl") %>" class="acss">
                                                            <asp:Label ID="PrdSk" runat="server" Style="display: none;"
                                                                Text='<%# Eval("ProductSK") %>'></asp:Label>
                                                            <h2 id="titlelit" runat="server" class="divTitle ellipsis multiline multima">
                                                                <%# Eval("Title")%>
                                                            </h2>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div class="author">
                                                    <span class="textellipsis">&nbsp;
                                                        <asp:Label ID="autlit" runat="server" ToolTip='<%# Eval("Author")%>' Text='<%# Eval("Author").ToString().Trim()==""?"":"By " + Eval("Author")%>'> </asp:Label>
                                                    </span><span class="isbspan"><span class="isbnNo">ISBN-13: </span>
                                                        <h3 class="lblcslit" ID="isbnlit" runat="server" title='<%# Eval("Isbn13") %>'>
                                                            <%# Eval("Isbn13") %> </h3></span>
                                                    <asp:Label ID="ProductSK" runat="server" Style="display: none;"
                                                        Text='<%# Eval("productSK") %>'></asp:Label>
                                                    <span class="detailspan">MORE</span>
                                                </div>
                                            </div>
                                            <div class="divfl">
                                                <a class="adetail" href="<%# Eval("Exacturl") %>">
                                                    
													<img  class="col3img coltimgw" 
													src='<%# Eval("ImagePath")%>' alt='<%# Eval("Title").ToString()+" - "+Eval("Isbn13").ToString() %>' onError="this.onerror=null;this.src='<%# string.Concat("portals/0/images/noimage.png") %>';">
                                                    <asp:HiddenField ID="CopyYear" runat="server" Value='<%# Eval("CopyRightYear") %>' />
                                                </a>
                                            </div>
                                        </div>
                                        <div class="col4">
                                            <div class="colfcssc">
                                                <span class="H4 strcol"><strong>
                                                    <asp:Literal ID="PriceVal" runat="server" Text='<%# Eval("PriceVal")%>'></asp:Literal></strong></span>
                                                <br />
                                                <span id="Rrps" runat="server" visible='<%# Eval("RrpFlag")%>'>
                                                    RRP</span>
                                                <div id="DisD" runat="server" visible='<%# Eval("DiscountFlag")%>'
                                                    class="amountblue Amountbldiv">
                                                    <div class="bluestar">
                                                    </div>
                                                    <a class="asty">DISCOUNTED</a>
                                                </div>
                                            </div>
                                            <div class="sectoggle">
                                                <img alt="" src="<%=Page.ResolveUrl("Portals/0/images/darr2.png")%>"
                                                    style="display: none;" />
                                                <img alt="" src="<%=Page.ResolveUrl("Portals/0/images/upimgsmall.png")%>"
                                                    style="display: none;" /></div>
                                        </div>
                                        <div class="col5">
                                            <div class="inndivsc">
                                                <div class="innerdivs">
                                                    <span class="spancf">
                                                        <asp:HiddenField ID="hdnAllowSale" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "AllowSale")%>' />
                                                        <asp:HiddenField ID="hdnIscachedprice" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsCachedPrice")==null?"Y":DataBinder.Eval(Container.DataItem, "IsCachedPrice") %>' />
                                                        <asp:Label ID="StockAvail" runat="server" Text='<%# Eval("StockAvail")%>'></asp:Label></span>
                                                </div>
                                                <div id="CtctUs" runat="server" class="divnyp">
                                                    <a href="../../Contactus.aspx" id="ctan" runat="server">Contact
                                                        Us </a>
                                                </div>
                                                <div>
                                                    <div id="AddedToCartpna" runat="server" class='<%# String.Format("headeraddedline added{0}",Eval("Isbn13")) %>'>
                                                        <span class="Spancart">ADDED</span>
                                                    </div>
                                                    <a onclick=<%# String.Format("GetIndividualISBN(this,'{0}','{1}','{2}')",Eval("Isbn13"),Eval("AllowSale"),Eval("StockAvail")) %>
                                                        id="addtocartrbtn" runat="server" class='<%# String.Format("H5 repeaterCart btncrt add{0}",Eval("Isbn13")) %>'>
                                                        ADD TO CART</a>
                                                </div>
												
												<div class="available-stock-value"><%# DataBinder.Eval(Container.DataItem, "Stockavailable")%>  <span class="stock-value"><%# DataBinder.Eval(Container.DataItem, "Stockqty")%></span></div>
												
                                            </div>
                                        </div>
                                    </div>
                                    <div class="toggle_container showdes showdescription HideItems" >
                                        <div id="dishd" runat="server" visible='<%# Eval("DiscountFlag")%>' clientidmode="Static"
                                            class="divdisks divdiscountheader">
                                            <%# DataBinder.Eval(Container.DataItem, "DiscountVal")%>
                                        </div>
                                        <div class="psty">
                                            <b>STUDENTS:</b><br />
                                            <input type="hidden" id="<%# String.Format("ismoreopened{0}",Eval("ProductSK"))%>"
                                                value="N" />
                                            <div id="<%# String.Format("StudentAttributes{0}",Eval("ProductSK"))%>" class="studentattrc">
                                                <asp:PlaceHolder ID="StudentPlace" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
                                            </div>
                                            <br />
                                            <br />
                                            <b class="prdattrc">PRODUCT:</b><br />
                                            <div id="<%# String.Format("ProductAttributes{0}",Eval("ProductSK"))%>" class="prdattrskc">
                                                <asp:PlaceHolder ID="ProductPlace" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
                                            </div>
                                            <a href="<%# Eval("Exacturl") %>" class="seeincata cataloguebtn">SEE
                                                IN CATALOGUE</a>
                                            <img alt="moreimg" class="moreimgclass" src='<%=Page.ResolveUrl("portals/0/Images/Moreloading.gif")%>'
                                                style="display: none;" />
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnRRP" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "RRPPrice")%>' />
                                    <asp:HiddenField ID="hdnDiscount" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DiscountPrice")%>' />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <CP:CustomPaging ID="Custompage" runat="server">
                        </CP:CustomPaging>
                    </div>
                </div>
            </div>
        </div>
        <div id="tab2">
            <asp:PlaceHolder ID="Cmsplace" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
            <div id="cmsview" class="cmsviewlf">
            <img id="Loaderimg" alt="moreimg" class="moreimgclassa" style="display: none;" src='<%=Page.ResolveUrl("portals/0/Images/ajax-loader1.gif")%>' />
            </div>
            <CP:CmsPaging ID="Cmspageview" runat="server"></CP:CmsPaging>
            <div id="btndsearch1" class="tab2searchdiv">
            </div>
            <div id="btndsubject1" class="tab2searchdiv">
            </div>
            <div id="btndProductType1" class="tab2searchdiv">
            </div>
            <input type="hidden" id="hdval1" />
            <div class="bkserDet">
            </div>
        </div>
    </div>
</div>
<input type="hidden" runat="server" id="StoreAttributeNames" clientidmode="Static" />
<div id="OrderRestrictPopup" class="popupClass srtechalldivff" style="display: none;">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Technical Issue</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="PopUpContentDivRes">
            <div class="marginnone l-space5 PopUpContentSpan">
                We are currently we are experiencing technical issues with our website. Please try
                again later.....
                <br />
                <br />
                <ul id="Spanisbn" class="scrolldivtech scrolldivp">
                </ul>
            </div>
        </div>
        <div class="divok srtechalldivok">
            <asp:Button ID="Ok" CssClass="lstfavourhbtn oktoptechi" ClientIDMode="Static" runat="server"
                Text="OK" />
        </div>
    </div>
</div>
<div id="OrderRestrictPopupQuote" class="popupClass srtechalldivf" style="display: none;">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Technical Issue</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="PopUpContentDiv">
            <h5 class="marginnone l-space5 PopUpContentSpan">
                We are currently we are experiencing technical issues with our website. Please try
                again later.....
                <br />
                <br />
                <asp:Label ID="Spanisbnquote" runat="server" ClientIDMode="Static" CssClass="lbltech">
                </asp:Label>
            </h5>
        </div>
        <div class="divok srtechalldivok">
            <asp:Button ID="OKQuote" CssClass="lstfavourhbtn" ClientIDMode="Static" runat="server"
                Text="OK" />
        </div>
    </div>
</div>
<div id="OutofstockPopup" class="popupClass" style="display: none;">
    <div class="popupbottmshade popupdeleteheader sroutstkalldivf">
        <h1 class="fontnormal marginnone sroutstkalldivmar">
            Out Of Stock</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="PopUpContentDivoutstock">
            <div class="marginnone l-space5 PopUpContentSpan srcout">
                One or more items you have selected are currently out of stock and will need to
                be back ordered. Out of stock items include:
                <br />
                <br />
                <ul id="OutOfStockIsbn" class="scrolldivp Outstockall">
                </ul>
            </div>
        </div>
        <div class="divok sroutstkalldivok">
            <asp:Button ID="OkStock" CssClass="lstfavourhbtn Oktop" ClientIDMode="Static" runat="server"
                Text="OK" />
        </div>
    </div>
</div>
<div id="SoutofStockPopup" class="popupClass sroutstkinddiv" style="display: none;">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Out Of Stock</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="PopUpContentDivout">
            <h5 class="marginnone l-space5 PopUpContentSpan srcouti">
                This item is currently out of stock, and will be back ordered..
            </h5>
        </div>
        <div class="divok">
            <asp:Button ID="SitemcOk" CssClass="lstfavourhbtn" ClientIDMode="Static" runat="server"
                Text="CONFIRM" />
        </div>
    </div>
</div>
<div id="AllowsalePopups" class="popupClass srtechisdivf" style="display: none;">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Technical Issue</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="PopUpContentDivsale">
            <h5 class="marginnone l-space5 PopUpContentSpan srtechisdivmar">
                We are currently we are experiencing technical issues with our website. Please try
                again later.....
            </h5>
        </div>
        <div class="divok">
            <asp:Button ID="SitemcOks" CssClass="lstfavourhbtn" ClientIDMode="Static" runat="server"
                Text="OK" />
        </div>
    </div>
</div>
<div id="Popuocreateuserbutton" class="popupClass shoppinglistfdiv">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Shopping List</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
            <div class="PopUpContentDivlogin">
                <h5 class="marginnone l-space5 PopUpContentSpan PopUpContentSpanh">
                    To add items to the Shopping List, you must be logged in.
                </h5>
                <div class="clearall">
                </div>
            </div>
            <div class="clearall">
            </div>
            <div class="shoppingdiv">
                <input id="LoginButton" type="button" onclick="javascript:OpenLoginWindowFromList();"
                    class="popupbutton lstfavourhbtn floatRight10" value="LOGIN" />
                <div class="orbtn">
                    OR</div>
                <input id="SignupButton" type="button" onclick="javascript:OpenWindowFromList();"
                    class="popupbutton lstfavourhbtn floatRight floatRight10 listgrch" value="SIGN UP" />
            </div>
    </div>
</div>
<div id="PopuocreatelistAnonbutton" class="popupClass srcrlist" style="display: none;">
    <div class="popupbottmshade popupdeleteheader" id="Div5" runat="server">
        <h1 class="fontnormal marginnone">
            Create a List</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
            <div class="PopUpContentDiv srcrlistdiv">
                <h5 class="marginnone l-space5 PopUpContentSpan srcrh srchannlsmar">
                    Please enter the name of your new list:</h5>
                <div class="lstSrchdiv srcrldiv">
                    <div class="lstSrchbtn IE_Filter_grad srcrlisdiv">
                        <label for='CreateListAnonTextBox' class="srcrlilbl">
                            List Name:</label>
                    </div>
                    <div class="srchbox1 createlistbuttoninputdiv createlistbuttoninputdivlw">
                        <asp:TextBox runat="server" ID="CreateListAnonTextBox" type="text" class="lsttxtsrchbox H5 srcrtxtb"
                            ClientIDMode="Static" value="i.e. wish-list, to booklist next year, save for later"
                            onblur="if(this.value == '') { this.value='i.e. wish-list, to booklist next year, save for later'}"
                            onfocus="if (this.value == 'i.e. wish-list, to booklist next year, save for later') {this.value=''}" />
                    </div>
                </div>
                <div class="clearall">
                </div>
                <div id="errorDiv4" class="dnnFormWarning popurerrordiv srcrerrdiv" style="display: none;">
                </div>
                <div class="clearall">
                </div>
            </div>
            <div class="PopUpContentDiv srcrlitdiv">
                <h5 class="marginnone l-space5 PopUpContentSpan srcrlih">
                    <asp:Literal ID="messagelabel" runat="server" Text="To create and save a list you must first be logged into your user acccount<br />Creating an accout is free and easy."></asp:Literal>
                </h5>
                <div class="clearall">
                </div>
            </div>
            <div class="srcrlasdiv">
                <asp:Button ClientIDMode="Static" ID="CreateListLoginButton" CssClass="popupbutton lstfavourhbtn floatRight10 srchannlsbtmar"
                    runat="server" Text="LOGIN" />
                <div class="srcrordiv">
                    OR</div>
                <asp:Button ID="CreateListSignUpButton" CssClass="popupbutton lstfavourhbtn floatRight floatRight10 srchannlsbtmars"
                    ClientIDMode="Static" runat="server" Text="SIGN UP" />
            </div>
    </div>
</div>
<!--

pop ups
for list quote and and createlist
-->
<div id="Popuocreatelistbutton" class="popupClass srchcrlisdiv" style="display: none;">
    <div class="popupbottmshade popupdeleteheader">
        <h1 class="fontnormal marginnone">
            Create a List</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
            <div class="PopUpContentDiv srchcrlisdivif">
                <h5 class="marginnone l-space5 PopUpContentSpan srchcrlisdivis">
                    Please enter the name of your new list:</h5>
                <div class="lstSrchdiv srchcrlisdivith">
                    <div class="lstSrchbtn IE_Filter_grad srchcrlisdivifor">
                        <a href="#" class="srchcrlisaif">List Name:</a>
                    </div>
                    <div class="srchboxf createlistbuttoninputdiv createlistbuttoninputdivw">
                        <asp:TextBox runat="server" ID="txtcreatelistbox" ClientIDMode="Static" type="text"
                            class="lsttxtsrchbox H5 srchtxtf" value="i.e. wish-list, to booklist next year, save for later"
                            onblur="if(this.value == '') 
                            { this.value='i.e. wish-list, to booklist next year, save for later'}" onfocus="if (this.value == 'i.e. wish-list, to booklist next year, save for later') {this.value=''}" />
                        <span class="Starblue">* </span>
                    </div>
                </div>
                <div class="clearall">
                </div>
                <div id="Lsdiv" class="dnnFormWarning1 popurerrordiv Lsdivf">
                </div>
                <div class="clearall">
                </div>
            </div>
            <div class="srcrlsdiv">
                <asp:Button ClientIDMode="Static" ID="CancelCreateListButton" CssClass="favoritegreycancelbutton popupbutton floatLeft floatLeft10 IE_Filter_gray srcrlsbt"
                    runat="server" Text="CANCEL" />
                <asp:Button ID="CreateListButton" ClientIDMode="Static" CssClass="popupbutton lstfavourhbtn floatRight floatRight10"
                    runat="server" Text="CREATE LIST" />
            </div>
    </div>
</div>
<!--

pop ups
for list quote and and createlist
-->
<div id="QuotePopup" class="popupClass srquotedivh" style="display: none;">
    <div class="popupbottmshade srquotedivf">
        <h1>
            Create a Quote</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
            <div class="PopUpContentDiv srquotedivsec">
                <h5 class="marginnone l-space5 PopUpContentSpan srquoteh5">
                    Please enter the name of your new quote:</h5>
                <div class="srquotedivthd">
                    <div class="lstSrchdiv srquotedivfrt">
                        <div class="lstSrchbtn IE_Filter_grad srquotedivfve">
                            <a href="#" class="srquoteaf">Custom quote reference: </a>
                        </div>
                        <div class="srchboxf createlistbuttoninputdiv">
                            <input type="text" class="lsttxtsrchbox H5 srquoteinf" id="txtQuoteName" clientidmode="Static"
                                runat="server" />
                            <span class="srquotespf">*</span>
                        </div>
                    </div>
                    <div id="OuterWarninCreate" class="errormessageclass srquotedivt" style="display: none;">
                        <div class="srquotedivfrtt">
                            <div id="WarninCreate" class="errorStyle srquotedivsix">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="srquotedivsev">
                    <div class="lstSrchdivs srquotedivet">
                        <div class="lstSrchbtn IE_Filter_grad srquotedivnin">
                            <a href="#" class="srquoteaff">Account name or number: </a>
                        </div>
                        <div class="srchboxf createlistbuttoninputdiv">
                            <input id="txtAccountName" clientidmode="Static" class="srquoteinls" runat="server" />
                            <asp:HiddenField ID="hdnAccountSK" runat="server" ClientIDMode="Static" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="srlstdiv">
                <asp:Button ID="btnCreate" ClientIDMode="Static" CssClass="popupbutton lstfavourhbtn floatRight floatRight10"
                    runat="server" Text="CREATE QUOTE" /><asp:HiddenField ID="hdnProductDetails" runat="server" />
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
<input type="hidden" id="hdnProdCount" runat="server" clientidmode="Static" />
<input type="hidden" id="hdnItemCount" runat="server" clientidmode="Static" />
<asp:HiddenField ID="Hidpagecnt" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ProductPrice" runat="server" />
<asp:HiddenField ID="SelectedCount" ClientIDMode="Static" Value="0" runat="server" />
<asp:HiddenField ID="PrskHd" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="Noresult" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdncmsclick" runat="server" ClientIDMode="Static" Value="0" />
<input type="hidden" id="cmsPagenum" runat="server" clientidmode="Static" />
<script src="<%=Page.ResolveUrl("Resources/Shared/scripts/ellipsis.js")%>" type="text/javascript"></script>
<script src="<%=Page.ResolveUrl("DesktopModules/HESearchResults/Scripts/search.js")%>" type="text/javascript"></script>