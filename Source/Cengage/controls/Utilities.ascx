<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Utilities.ascx.cs" Inherits="controls_Utilities" %>
<div class="utilities">
    <div class="algnCentr">
        <div id="util_aligndiv" class="util_aligndiv">
            <div id="util" class="util_rhtdiv">
                <a runat="server" clientidmode="Static" id="loginlnk" class="loginlnk H6" tabindex="4" onclick="javascript:OpenLoginWindow(this);">
                    Login</a>          
                    <a runat="server" clientidmode="Static" ID="SignUp" class="signuplnk H6" tabindex="5" 
                        onclick="javascript:OpenWindow();">Sign Up</a> 
                         <div id="logoutsepreator" runat="server" clientidmode="Static" visible="false" class="logoutlnksepreator line-seprator">|</div>
                        <a id="LogOut" runat="server" visible="false"
                    class="logoutlnk H6" clientidmode="Static" onserverclick="lnkLogout_Click">LOGOUT</a>                        
                        <a runat="server" clientidmode="Static" id="wishlistlnk" tabindex="6"  class="util_rhtwishlistlnk H6" href="~/list.aspx">
                            <span id="wishlistimg" class="util_icon1"></span>
                            <span id="wishlisttxt" class="util_wishlisttxt">
                                My LISTS</span> 
                                <span id="WishList" runat="server" clientidmode="Static" class="util_wishlistval"></span>
                         </a>                        
                        <a runat="server" clientidmode="Static" id="cartlnk" class="util_rhtcartlnk H6" tabindex="7" href="~/list.aspx?item=cart">
                            <span id="cartimg" class="util_icon1"></span><span id="carttxt" class="util_carttxt">
                                Cart</span> <span id="Cart"  runat="server" clientidmode="Static"  class="util_cartval"></span></a>
            </div>
            <div class="util_lftdiv">
                <ul>
                    <li>
                        <a tabindex="1" class="H6" href="/contactus.aspx">Contact Us</a>
                    </li>
                    <li class="line-seprator">|</li>
                    <li>
                    <a tabindex="2" class="H6" href="/Help.aspx">Help</a>
                    </li>
                    <li id="CountrySeprator" runat="server" class="line-seprator">|</li>
                    <li>
                        <a id="CountryName" runat="server" clientidmode="Static"  TabIndex="3" class="ddlcountry H6">
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</div>
<div id="CountryPopup" style="display:none;" class="Countrypopup">
    <div class="CountryPopupHead">
        <h1 class="countryhead1">
           Logout</h1>
    </div>
    <br />
    <div class="boxshadowstyle">
        <div class="CountryPopUpContentDiv">
            <h5 class="CountryPopUpContent">
               You must first logout before you can change your location setting. Do you wish to logout? </h5>                
        </div>
        <div class="Countryconfirm">
          <div class="countryconfimleft">
                 <asp:LinkButton ID="CountryCancel" runat="server" CssClass="Countrylogin" Text="CANCEL"
                        ClientIDMode="Static">
                </asp:LinkButton>    
            </div>  
            <div class="countryconfimright">
                <asp:LinkButton ID="CountryLogout" runat="server" CssClass="Countrylogin" Text="CONFIRM"
                        ClientIDMode="Static" onclick="CountryLogout_Click">
                </asp:LinkButton>          
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#CountryCancel').click(function () {
            window.data("kendoWindow").close();
        });
        $("#loginlnk").attr("href", "#");
        $("#SignUp").attr("href", "#");
        $("#CountryName").attr("href", "#"); 
		 $('#Cancel').click(function () {
            var window = self.parent.$('#RemoveCart');
            window.data("kendoWindow").close();
        });
    });
</script>