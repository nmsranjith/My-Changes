<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterSearcControl.ascx.cs" Inherits="controls_FooterSearcControl" %>
     <script src="/Resources/Shared/scripts/Footersearch.js" type="text/javascript"></script>

<div id="kendodiv_schooltypeFooter" class="kendo_schooltypetop H5">
    <asp:DropDownList ID="ddlschooltypefooter" runat="server" ClientIDMode="Static"  Style="width: 144px !important;">
        <asp:ListItem Text="PRIMARY" Value="0"></asp:ListItem>
        <asp:ListItem Text="SECONDARY" Value="1"></asp:ListItem>
        <asp:ListItem Text="BOTH" Value="2"></asp:ListItem>
    </asp:DropDownList>
</div>

  
        <div class="Srchdiv">
            <div class="srchbox1">
               <input id="TextSearchFooter" runat="server" clientidmode="Static"  type="text" class="txtsrchbox H5Light txtsrchfooter"
                    maxlength="70" style="background-color: transparent !important; border: medium none;
                    height: 16px !important; position: absolute; width: 700px !important;" value="Enter your search here..."
                    onblur="if(this.value == '') { this.value='Enter your search here...';}" onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:AnchorFocus(event);" />
                     <%--<input id="TextSearchFooterPrimary" runat="server" clientidmode="Static" type="text" class="txtsrchbox H5Light txtsrchfooter"
                    style="background-color: transparent !important; border: medium none; display: block;
                    height: 16px; position: absolute; width: 532px;" value="Enter your search here..."
                    onblur="if(this.value == '') { this.value='Enter your search here...';}" onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:AnchorFocus(event);" />
                     <input id="TextSearchFooterSecondary" runat="server" clientidmode="Static" type="text" class="txtsrchbox H5Light txtsrchfooter"
                    style="background-color: transparent !important; border: medium none; display: none;
                    height: 16px; position: absolute; width: 532px;" value="Enter your search here..."
                    onblur="if(this.value == '') { this.value='Enter your search here...';}" onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:AnchorFocus(event);" />--%>
            </div>
        </div>
        <div>
            <a class="Srchbtntop" id="footerSearch" onclick="javascript:ProductFooterClick();">
                <%-- <img src='<%=Page.ResolveUrl("Portals/0/Images/search_icon.png")%>' alt=''>--%></a>
        </div>
            
        <div class="Srchbtmline_top">
        </div>
        <%--<div style="float: left; color: #707070; font-size: 12pt; margin-top: 20px; margin-left: 190px; width: 103%;display:none;">
            <div id="Didyoumeanlabel" runat="server" style="float: left; width: 73%; visibility: hidden">
                <span id="DidUmeanSpan" runat="server">Did You Mean : </span><a id="DidYouMean" runat="server" style="text-decoration: underline; color: blue; cursor: pointer;"></a>
            </div>
            <div id="DidyoumeanDiv" runat="server" style="float: left; color: #707070; font-size: 12pt; margin-top: 3px; margin-left: 9px;">
                <input type="checkbox" name="turnOnDidUMean" id="turnOnDidUMean" clientidmode="Static" onserverchange="turnOnDidUMean_Change" runat="server" checked="checked" />
                <label for="turnOnDidUMean">
                    Enable 'Did you mean'</label>
            </div>
        </div>--%>
        <asp:HiddenField ID="HiddenFooterSearchText" runat="server" ClientIDMode="Static" />
 
