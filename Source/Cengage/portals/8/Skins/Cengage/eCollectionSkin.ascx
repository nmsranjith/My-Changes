<%@ Control Language="C#" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TEXT" Src="~/Admin/Skins/Text.ascx" %>
<%@ Register TagPrefix="dnn" TagName="STYLES" Src="~/Admin/Skins/Styles.ascx" %>
<%@ Register TagPrefix="dnn" TagName="CONTROLPANEL" Src="~/Admin/Skins/controlpanel.ascx" %>
<%@ Register TagPrefix ="cengage" TagName = "CENGAGESEARCH" Src = "~/controls/SearchControl.ascx" %>
<dnn:STYLES runat="server" ID="StylesIE8" Name="IE8Minus" StyleSheet="ie8skin.css"
    Condition="LT IE 9" UseSkinPath="true" />
<script src="<%=Page.ResolveUrl("Resources/Shared/scripts/kendoui/js/kendo.web.min.js")%>"
    type="text/javascript"></script>
<link href="<%=Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.common.min.css")%>"
    rel="stylesheet" type="text/css" />
<link href="<%=Page.ResolveUrl("Resources/Shared/scripts/kendoui/styles/kendo.default.min.css")%>"
    rel="stylesheet" type="text/css" />
<style type="text/css">
    .k-widget .k-window
    {
        margin-top: 10%;
        background: transparent;
    }
    .k-window
    {
        box-shadow: none;
        background: transparent;
    }
    .k-window div.k-window-content
    {
        overflow: hidden;
        background: transparent;
    }
    div.k-window
    {
        border: 0px solid white;
    }
    .k-widget .k-window
    {
        margin-top: 10%;
        background: transparent;
    }
    .k-window
    {
        box-shadow: none;
        background: transparent;
    }
    .k-window div.k-window-content
    {
        overflow: hidden;
        background: transparent;
    }
    div.k-window
    {
        border: 0px solid white;
    }
    .k-header
    {
        background-color: white;
        border: none;
    }
    .k-block > .k-window-titlebar
    {
        border-bottom-style: none;
    }
    .k-window-titlebar
    {
        background: transparent;
    }
    #window1
    {
        background: transparent;
    }
    .k-window-action
    {
        background-image: url('./Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    .k-window-action.k-link.k-state-hover:hover
    {
        background-image: url('./Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 28px !important;
        width: 28px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    .k-window-actions
    {
        background: transparent;
    }
    .k-i-close
    {
        background-image: initial !important;
    }
    .k-window-titlebar .k-state-hover
    {
        padding: -1px !important;
    }
    .k-window-action .k-link
    {
        display: block !important;
        position: fixed !important;
    }
    .k-icon .k-i-close
    {
        background-image: none !important;
    }
    .bannertitle
    {
        padding-left: 77px;
        top: -125px;
        z-index: 4;
    }
</style>
<script type="text/javascript">
    //Google Analytics Dont edit this
    var domainName;
    if (window.location.host.indexOf("cengagelearning.com.au") > -1)
    { domainName = 'cengage.com.au'; }
    else if (window.location.host.indexOf("cengagelearning.co.nz") > -1)
    { domainName = 'cengage.co.nz'; }
    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-43278709-1']);
    _gaq.push(['_setDomainName', domainName]);
    _gaq.push(['_setAllowLinker', true]);
    _gaq.push(['_trackPageview']);
    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();
    //End Google Analytics Dont edit this
    $(document).ready(function () {


        $("#ddlcountry").kendoDropDownList();
        $("#ddlschooltypetop").kendoDropDownList();
        $("#ddlschooltypebottom").kendoDropDownList();
        if ($('#cartval').text().length == 0) {
            $('#cartlnk').css('width', '100px');
            $('#cartlnk').css('background', 'url("portals/0/images/cart_grey.png") no-repeat scroll 0 0 transparent');
        }
        else {
            $('#cartlnk').css('width', '124px');
            $('#cartlnk').css('background', 'url("portals/0/images/mylist_grey.png") no-repeat scroll 0 0 transparent')
        }
        if ($('#wishlistval').text().length == 0) {
            $('#wishlistlnk').css('width', '100px');
            $('#wishlistlnk').css('background', 'url("portals/0/images/cart_left_grey.png") no-repeat scroll 0 0 transparent');
        }
        else {
            $('#wishlistlnk').css('width', '126px');
            $('#wishlistlnk').css('background', 'url("portals/0/images/mylist_left_grey.png") no-repeat scroll 0 0 transparent');
        }
        //        $('#ie_loginarrow').hide();
        //        $('#ie_signuparrow').hide();
        //        $('#ie_wishlistarrow').hide();
        //        $('#ie_cartarrow').hide();

        if ($.browser.msie) {
            //            $("#ie_literacydiv").addClass("tabshadow");
            //            $("#ie_numaracydiv").addClass("tabshadow");
            //            $("#ie_workbooksdiv").addClass("tabshadow");
            //            $("#ie_digitaldiv").addClass("tabshadow");

        }
        //ADVANCED SEARCH
        $('#Mainmenu li').live("mouseover", function () {
            $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
            $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('adv_search_sel', 'adv_search'));
            $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
            $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
        });
        $("#advancedsrchTopdiv").click(function () {
            $("#advancedsrchdialog").css("display", "block");
            $('.tabs').css("z-index", "0");
            if ($('#imgadvSrc').attr('src').indexOf('adv_search_sel') == -1) {
                $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('adv_search', 'adv_search_sel'));
                $('#advancedsrchTopdiv').removeClass("advSrchbtn_Top");
                $('#advancedsrchTopdiv').addClass("advSrchbtn_TopSel");
            }
            else {
                $("#advancedsrchdialog").css("display", "none"); $(".tabs").css("z-index", "5");
                $('#imgadvSrc').attr('src', $('#imgadvSrc').attr('src').replace('adv_search_sel', 'adv_search'));
                $('#advancedsrchTopdiv').removeClass("advSrchbtn_TopSel");
                $('#advancedsrchTopdiv').addClass("advSrchbtn_Top");
            }

        });

        $("#msginfoclosediv").click(function () {
            $("#msginfodiv").hide();
        });
        //
        var _totwidth = $("#Mainmenu").width();
        var _licount = $("#Mainmenu ul").children().length;
        var _splitwidth = (_totwidth / _licount);
        var _marginwidth;
        var _marginwidthtbs;
        if (_licount <= 4) {
            _marginwidth = _licount * 15;
            _marginwidthtbs = _marginwidth + 2;
        }
        else {
            _marginwidth = _licount * 4;
            _marginwidthtbs = _marginwidth + 8;
        }





        var _liwidth = parseFloat(_splitwidth.toString()).toFixed(2);
        var _liwidthref = parseFloat(_splitwidth.toString()).toFixed(2) - 2;
        //        $("#Mainmenu li").css('width', _liwidth + 'px');
        //        $("#Mainmenu li a").css('width', _liwidth + 'px');
        //        $(".menuitem").css('border', "1px ridge #dedede");

        //        $("#Mainmenu li a:focus").css('border', "1px outset #FFFFFF");
        //        $(".icons").css('margin-left', "43px!important");
        //        $('.icons_staff').css("margin-left", "43px!important");
        //        $('.icons_abt').css("margin-left", "43px!important");




        var href = jQuery(location).attr('href');
        var currentpagename = href.substring(href.lastIndexOf('/') + 1);

        if (currentpagename.toUpperCase() == "CENGAGE_DASHBOARD.ASPX") {
            $('#loginlnk').addClass('util_rhtlnkactive');
            $('#userimg_dash').addClass('useractive_dash');
        }
        else if (currentpagename.toUpperCase() == "SIGNUP.ASPX") {
            $('#signuplnk').addClass('util_rhtlnkactive');


        }
        else if (currentpagename.toUpperCase() == "LIST.ASPX?ITEM=LIST") {

            $('#wishlistlnk').addClass('util_rhtwishlistactive');
            $('#wishlistimg').css('background', 'url("portals/0/images/star_blue.png") no-repeat scroll 0 0 transparent');

            if ($('#wishlistval').text().length == 0) {
                $('#wishlistlnk').css('background', 'url("portals/0/images/cart_left_blue.png") no-repeat scroll 0 0 transparent');

            }
            else {
                $('#wishlistlnk').css('background', 'url("portals/0/images/mylist_left_blue.png") no-repeat scroll 0 0 transparent');

            }

        }
        else if (currentpagename.toUpperCase() == "LIST.ASPX?ITEM=CART") {
            $('#cartlnk').addClass('util_rhtcartlnkactive');
            $('#cartimg').css('background', 'url("portals/0/images/cart_blue.png") no-repeat scroll 0 0 transparent');

            if ($('#cartval').text().length == 0) {
                $('#cartlnk').css('background', 'url("portals/0/images/cart_blue.png") no-repeat scroll 0 0 transparent');

            }
            else {
                $('#cartlnk').css('background', 'url("portals/0/images/mylist_blue.png") no-repeat scroll 0 0 transparent');

            }

        }
        else if (currentpagename.toUpperCase() == "PRIMARY.ASPX") {
            $('#primarylnk').addClass('primarydashactive');


            var ddlschooltypetopDDL = $('#ddlschooltypetop').data('kendoDropDownList'); // kendo object
            ddlschooltypetopDDL.dataSource.read(); // re-new data from Your Server - if you need this.
            ddlschooltypetopDDL.select(1);

            var ddlschooltypebottomDDL = $('#ddlschooltypebottom').data('kendoDropDownList'); // kendo object
            ddlschooltypebottomDDL.dataSource.read(); // re-new data from Your Server - if you need this.
            ddlschooltypebottomDDL.select(1);
        }
        else if (currentpagename.toUpperCase() == "SECONDARY.ASPX") {
            $('#secondarylnk').addClass('menuitemactive');
            $('#secondaryimg').css('background', 'url("portals/0/images/pen_blue.png") no-repeat scroll 0 0 transparent');

            var ddlschooltypetopDDL = $('#ddlschooltypetop').data('kendoDropDownList'); // kendo object
            ddlschooltypetopDDL.dataSource.read(); // re-new data from Your Server - if you need this.
            ddlschooltypetopDDL.select(2);

            var ddlschooltypebottomDDL = $('#ddlschooltypebottom').data('kendoDropDownList'); // kendo object
            ddlschooltypebottomDDL.dataSource.read(); // re-new data from Your Server - if you need this.
            ddlschooltypebottomDDL.select(2);
        }
        else if (currentpagename.toUpperCase() == "STAFFROOM.ASPX") {
            $('#staffroomlnk').addClass('menuitemactive');
            $('#staffroomimg').css('background', 'url("portals/0/images/StaffRoom_blue.png") no-repeat scroll 0 0 transparent');
        }
        else if (currentpagename.toUpperCase() == "ABOUTUS.ASPX") {
            $('#aboutuslnk').addClass('menuitemactive');
            $('#aboutusimg').css('background', 'url("portals/0/images/aboutus_blue.png") no-repeat scroll 0 0 transparent');
        }
        else if (currentpagename.toUpperCase() == "ECOLLECTION.ASPX") {
            $('#ecollimg_ecoll').addClass('ecollactive_ecoll');
        }
    });
    function GetFile(path) {
        var pathname = window.location.pathname;
        var temppath = pathname.split('/');
        var root = "http://" + window.location.host + '/' + temppath[1];
        var url = root + path;
        return url;
    }
    function OpenWindow() {
        var window = $("#window"),
            signup = $("#signuplnk").bind("click", function () { window.data("kendoWindow").open(); });
        $("#signuplnk").click(function (e) {
            window.data("kendoWindow").open();
        });
        onClose = function () {
            signup.show();
        }
        if (!window.data("kendoWindow")) {
            window.kendoWindow({
                width: "506px",
                height: "904px",
                content: GetFile("/signup.aspx"),
                close: onClose,
                modal: true,
                draggable: false
            });
            window.data("kendoWindow").center();
            $(".k-icon.k-i-close").css("display", "none");
            $('a.k-window-action.k-link').mouseover(function () {
                $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
                return false;
            });
        }
    }

    function OpenLoginWindow(elementId) {

        var window = $("#loginWindow");

        window.kendoWindow({
            width: "491px",
            height: "317px",
            content: GetFile("/cengageecommerce.aspx"),
            modal: true,
            draggable: true,
            resizable: true
        });
        window.data("kendoWindow").center();
        window.data("kendoWindow").open();
        $("#loginWindow").css({ 'height': '365px', 'margin-left': '-9px', 'width': '509px' });
        $("#loginWindow").parent().css('height', '317px');
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false;
        });
    }

</script>
<dnn:CONTROLPANEL runat="server" ID="cp" IsDockable="True" />
<div id="window">
</div>
<div id="window1">
</div>
<div class="utilities">
    <div class="algnCentr">
        <div id="util_aligndiv" class="util_aligndiv">
            <div id="util" class="util_rhtdiv_dashboard">
                <a id="wishlistlnk" style="width: 124px;" class="util_rhtwishlistlnk H6" href="List.aspx?item=list"
                    style="width: 120px;"><span id="wishlistimg" class="util_icon1dash"></span><span
                        id="wishlisttxt" class="util_wishlisttxt">My LISTS</span> <span id="wishlistval"
                            class="util_wishlistval">(03)</span> </a><a id="cartlnk" class="util_rhtcartlnkdash H6"
                                href="List.aspx?item=cart"><span id="cartimg" class="util_icon1"></span><span id="carttxt"
                                    class="util_carttxt">Cart</span> <span id="cartval" class="util_cartval">(03)</span></a>
            </div>
            <div class="util_lftdiv">
                <ul>
                    <li><a class="H6" href="help.aspx">Contact Us</a></li>
                    <li class="line-seprator">|</li>
                    <li><a class="H6" href="help.aspx">Help</a></li>
                    <li class="line-seprator">|</li>
                    <li>
                        <div class="H6" id="kendodiv_country">
                            <select id="ddlcountry">
                                <option>AUSTRALIA</option>
                                <option>NEW ZEALAND</option>
                                <option>OTHERS</option>
                            </select>
                        </div>
                    </li>
                    <li class="line-seprator">|</li>
                    <li><a class="logoutdiv H6" href="Primary.aspx">Logout</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
<div id="logo">
    <div class="logoouter">
        <dnn:LOGO ID="dnnLogo" runat="server" />
    </div>
</div>
<div id="masterhead">
    <div id="eColl_Mainmenu" class="algnCentr">
        <ul id="Mainmenuul">
            <li id="menuitem_user_ecoll"><a id="userlnk_ecoll" href="Cengage_dashboard.aspx"
                class="menuitem"><span id="userimg_ecoll" class="icons"></span></a></li>
            <li id="menuitem_eColl_ecoll"><a id="eColllnk_ecoll" href="eCollection.aspx" class="menuitem">
                <span id="ecollimg_ecoll" class="icons"></span></a></li>
            <li id="menuitem_primary_ecoll"><a id="primarylnk_ecoll" href="Primary.aspx" class="menuitem">
                <span id="primaryimg_ecoll" class="icons"></span></a></li>
            <li id="menuitem_secondary_ecoll"><a id="secondarylnk_ecoll" href="Secondary.aspx"
                class="menuitem"><span id="secondaryimg_ecoll" class="icons"></span></a></li>
            <li id="menuitem_staffroom_ecoll"><a id="staffroomlnk_ecoll" href="StaffRoom.aspx"
                class="menuitem"><span id="staffroomimg_ecoll" class="icons"></span></a></li>
            <li id="menuitem_aboutus_ecoll"><a id="aboutuslnk_ecoll" href="AboutUs.aspx" class="menuitem">
                <span id="aboutusimg_ecoll" class="icons"></span></a></li>
        </ul>
    </div>
    <div class="bannersececollection">
        <img src="<%=Page.ResolveUrl("portals/0/images/topband.png")%>" alt="Slide1" class="bannerimg bannereCollimg"
            width="100%" height="88%" />
        <div id="bannertitle" class="bannertitle algnCentr">
            <h1>
                <asp:Label ID="lblbannertxt" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            </h1>
        </div>
    </div>
</div>
<div class="middlecont">
    <div class="algnCentr">
        <div class="Panes">
            <div id="ContentPane" runat="server" style="clear: both; display: inline-block; z-index: 5;">
            </div>
        </div>
    </div>
</div>
<div id="Srchbottom" class="SrchBottom">
    <div class="SrchComp_Bottom">
        <div id="kendodiv_schooltypebottom" class="H5">
            <select id="ddlschooltypebottom">
                <option>ALL</option>
                <option>PRIMARY</option>
                <option>SECONDARY</option>
            </select>
        </div>
        <div class="Srchdiv">
            <div class="srchbox">
                <input type="text" class="txtsrchboxbottom H5Light" value="Enter your search here..."
                    onblur="if(this.value == '') { this.value='Enter your search here...'}" onfocus="if (this.value == 'Enter your search here...') {this.value=''}" />
            </div>
        </div>
        <div>
            <a class="Srchbtn" href="ProductResults.aspx">
                <img src="<%=Page.ResolveUrl("Portals/0/Images/search_icon.png")%>"></a>
        </div>
    </div>
    <div class="Srchbtmline">
    </div>
</div>
<div id="footer_top">
</div>
<div id="footer">
    <div id="footer_megatop">
        <div class="contCentr">
            <div class="splInfo1">
                <div>
                    <img alt="ceng" src="<%=Page.ResolveUrl("portals/0/images/ceng.png")%>" class="nelicon" />
                    <h5 class="headingtext">
                        NELSON CENGAGE</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer cenlist H6Light">Cengage corporate Home</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Primary</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Secondary</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Higher Education</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Vocational & Professional</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">English Professional Teaching</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light"><span class="galespan">Gale</span>
                            Library Reference</a></li></ul>
                </div>
                <div class="splinfo_linethin1">
                </div>
            </div>
            <div class="splInfo2">
                <div class="H5">
                    <img src="<%=Page.ResolveUrl("portals/0/images/man_icon.png")%>" class="manicon" />
                    <h5 class="headingtext">
                        CENGAGE PEOPLE</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Booksellers</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Schools</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Teachers</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Authors</a></li></ul>
                </div>
                <div class="splinfo_linethick">
                </div>
            </div>
            <div class="splInfo3">
                <div class="H5">
                    <img src="<%=Page.ResolveUrl("portals/0/images/help_icon.png")%>" class="helpicon" />
                    <h5 class="headingtext">
                        HELP & INFORMATION</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Help</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">About Us</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Permissions</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Accessibility</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Careers</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Imprints</a></li></ul>
                </div>
                <div class="splinfo_linethin_help">
                </div>
            </div>
            <div class="splInfo_contact">
                <div>
                    <img src="<%=Page.ResolveUrl("portals/0/images/shopping_icon.png")%>" class="shopicon" />
                    <h5 class="headingtextshopping">
                        SHOPPING</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer onlinelist H6Light">Online Store</a></li>
                        <li><a href="help.aspx" class="anc_class_footer H6Light">Shipping & Delivery</a></li></ul>
                </div>
                <div class="footer_contactdiv">
                    <img src="<%=Page.ResolveUrl("portals/0/images/contact_icon.png")%>" class="conticon" /><h5
                        class="headingtextcontact">
                        CONTACT US</h5>
                </div>
                <div class="footerlist1">
                    <ul>
                        <li><a href="help.aspx" class="anc_class_footer_contval H6Light">Contact Us</a></li></ul>
                </div>
            </div>
            <div class="footerlist2">
                <a href="#top">
                    <img id="footerlist2img" class="footerlist2imgdash" src="<%=Page.ResolveUrl("portals/0/images/backtotop.PNg")%>"
                        alt="backtop" /></a>
            </div>
        </div>
    </div>
    <div class="footer_megabottom">
        <div class="algnCentr">
            <div class="footer_bottom_left">
                <a href="help.aspx" class="anc_class H5Light">Sitemap</a>&nbsp;|&nbsp; <a href="help.aspx"
                    class="anc_class H5Light">Terms and Conditions</a>&nbsp;|&nbsp;<a href="help.aspx"
                        class="anc_class H5Light"> Privacy</a>&nbsp;|&nbsp; <a href="help.aspx" class="anc_class H5Light">
                            Copyright</a>&nbsp;|&nbsp;<a href="help.aspx" class="anc_class H5Light"> Disclaimer</a>
            </div>
            <div class="footer_bottom_right">
                <a href="help.aspx" class="anc_class H5Light">&#169;2013. Cengage Learning Australia
                    pty Limited</a>&nbsp;|&nbsp; <a href="help.aspx" class="anc_class H5Light">ABN: 14 058
                        280 149 / IRD 6757486748598</a>
            </div>
        </div>
    </div>
</div>
<div style="display: none;">
    <asp:Button ID="hiddenbutton" runat="server" ClientIDMode="Static" Style="display: none;" />
</div>
