<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchControl.ascx.cs"
    Inherits="controls_SearchControl" %>
   
<div id="kendodiv_schooltypetop" class="kendo_schooltypetop H5">
    <asp:DropDownList ID="ddlschooltypetop" runat="server" ClientIDMode="Static">
        <asp:ListItem Text="PRIMARY" Value="0"></asp:ListItem>
        <asp:ListItem Text="SECONDARY" Value="1"></asp:ListItem>
        <asp:ListItem Text="BOTH" Value="2"></asp:ListItem>
    </asp:DropDownList>
</div>

        <div class="Srchdiv">
            <div class="srchbox">
                <input id="TextSearch" runat="server" clientidmode="Static" type="text" class="txtsrchbox H5Light"
                    maxlength="70" style="background-color: #eeeeee !important; border: medium none;" value="Enter your search here..." onblur="if(this.value == '') { this.value='Enter your search here...';}"
                    onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:clickfocusevent(event);" />
                <%--<input id="TextSearchPrimary" runat="server" clientidmode="Static" type="text" class="txtsrchbox H5Light"
                    maxlength="70" style="background-color: #eeeeee !important; border: medium none;
                    display: none; " value="Enter your search here..." onblur="if(this.value == '') { this.value='Enter your search here...';}"
                    onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:clickfocusevent(event);" />
                <input id="TextSearchSecondary" runat="server" clientidmode="Static" type="text"
                    class="txtsrchbox H5Light" maxlength="70" style="background-color: #eeeeee !important;
                    border: medium none; display: none; " value="Enter your search here..."
                    onblur="if(this.value == '') { this.value='Enter your search here...';}" onfocus="if (this.value == 'Enter your search here...') {this.value='';};this.style.fontStyle='normal';"
                    onkeypress="javascript:clickfocusevent(event);" />--%>
            </div>
        </div>
        <div>
            <a class="Srchbtntop" id="Search" onclick="javascript:ProductClick();">
                <%--<img src='<%=Page.ResolveUrl("Portals/0/Images/search_icon.png")%>' alt=''>--%></a>
        </div>
        <%-- <input style="display: none;" type="button" id="ProductSearchButton" runat="server"
            clientidmode="Static" onserverclick="ProductSearchButton_Click" />
        <input style="display: none;" type="button" id="AutoSuggestionButton" runat="server" value=" "
            clientidmode="Static" onserverclick="AutoSuggestionButton_Click" />--%>
        <input style="display: none;" type="button" id="ProductSearchButton" runat="server" value=" "
            clientidmode="Static" />
        <input style="display: none;" type="button" id="AutoSuggestionButton" runat="server" value=" "
            clientidmode="Static" />
        <div class="Srchbtmline_top">
        </div>

<asp:HiddenField ID="StoreHidden" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="DivisionHidden" runat="server" ClientIDMode="Static" />
<script src="/Resources/Shared/scripts/Headersearch.js" type="text/javascript"></script>
