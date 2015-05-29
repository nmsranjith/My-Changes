<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftDashboard.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Common.LeftDashboard" %>
<div style="width: 92%; float: left; margin-right: 1%;">
    <div class="LinksHolder HideItems" runat="server" clientidmode="Static" id="RenewalButton" visible="false">
        <a href="#" class="DashboardLinks1" onclick="Open()">After renewal</a>
    </div>
    <div class="LinksHolder" runat="server" clientidmode="Static" id="BackButton">
        <a class="DashboardLinks1" runat="server" id="BackToDashboard">BACK TO DASHBOARD</a>
    </div>
</div>
<style type="text/css">
    .k-window
    {
        top: 90px !important;
    }
    .k-overlay
    {
        opacity: 0.7 !important;
    }
    
    #AfterRenewelPopUp h1
    {
        font-size: 27pt !important;
        margin-top: 5px !important;
        float: left !important;
        margin-left: 34px !important;
        margin-bottom: auto !important;
        font-family: Raleway-Regular,Arial, Sans-Serif !important;
        color: #707070 !important;
    }
    
    #AfterRenewelPopUp h5
    {
        font-weight: 700 !important;
        font-size: 11pt !important;
        margin-bottom: 10px !important;
        float: left;
        margin-top: 34px;
        margin-left: 34px;
        margin-bottom: auto;
        font-family: Raleway-Regular,Arial,sans-serif;
        color: #707070;
    }
    
    .k-window-action
    {
        display: block !important;
        visibility: visible !important;
    }
    .k-window-action
    {
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 1px !important;
        border: none !important;
        margin-left: -10px;
    }
</style>
<div id="AfterRenewelPopUp" style="display: none;" class="AfterRenewelPopUpClass">
    <div style="background-image: url('./Portals/0/images/topband.png'); height: 150px;
        width: 100%; margin-left: -7px; margin-top: -5px;">
        <%--<span class="AfterRenewelHeaderSpan" style="margin-top: 13px;">--%>
        <h1>
            Welcome to
            <%--</span>--%>
            <asp:Label ID="AfterRenewelYear" runat="server"></asp:Label></h1>
        <br />
        <%--<span class="AfterRenewelHeaderSpan">--%>
        <h1>
            Thanks for renewing your subscription:
            <asp:Label ID="lblSubscriptionName" runat="server"></asp:Label></h1>
        <%--</span>  --%>
    </div>
    <div style="box-shadow: 0px 0px 8px gray; -moz-box-shadow: 0 8px 8px gray; -webkit-box-shadow: 8 0px 0px gray;
        float: left; width: 103%; margin-left: -7px; height: 87%;">
        <center>
            <div class="AfterRenewelContentDiv" style="margin-top: 50px !important">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckAllTeachers" runat="server" Checked="true" />
                <%--<span class="AfterRenewelContentSpan">--%>
                <h5>
                    Keep all teacher profiles</h5>
                <%--</span>--%>
            </div>
            <div class="AfterRenewelContentDiv">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckAllStudents" runat="server" Checked="true" />
                <%--<span class="AfterRenewelContentSpan">--%>
                <h5>
                    Keep all student profiles
                    <%--</span>--%>
                    <asp:Label ID="licencesCount" runat="server" Style="color: brown"></asp:Label>
                </h5>
            </div>
            <div class="AfterRenewelContentDiv">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckMoveall" runat="server" Checked="true" />
                <h5>
                    Move all students up one year level</h5>
            </div>
            <div class="AfterRenewelContentDiv" id="BooksRenewel" runat="server">
                <div class="AfterRenewelPopUpClassimg">
                </div>
                <asp:CheckBox ID="CheckStart" runat="server" Checked="true" />
                <h5>
                    Start with same books as last year
                    <asp:Label ID="DaysLeftLabel" Text="(you will have 30 days to swap books)" Style="color: Green;"
                        runat="server"></asp:Label>
                </h5>
            </div>
            <asp:Button ID="StartButton" ClientIDMode="Static" class="ActiveAddButtonsHolder"
                runat="server" Style="font-family: Raleway,Arial; font-weight: 700; font-size: 10pt;
                margin-top: 10px" OnClientClick="javascript:btnAccept_onclick();" OnClick="StartButton_Click" />
            <%--<asp:LinkButton ID="lnkStartButton" runat="server" Text="Start 2014" Style="font-family: Raleway,Arial;
                font-weight: 700; font-size: 10pt;" ClientIDMode="Static" class="ActiveAddButtonsHolder" />--%>
            <a id="AdvancedSetupLink" runat="server" clientidmode="Static" visible="false" style="margin-top: 20px">Advanced set up&nbsp;&nbsp;>></a>
            <%--<asp:LinkButton ID="AdvancedSetUp" runat="server" Text="Advanced set up&nbsp;&nbsp;>>" PostBackUrl="http://s2.cengagelearning.com.au/subscription.aspx?pagename=managelicense" style="margin-top:20px"></asp:LinkButton>--%>
        </center>
    </div>
</div>
<script type="text/javascript">
    var flag;
    function setPopupFlag() {
        flag = true;
    }
    function Open() {
        $("#AfterRenewelPopUp").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        var window = $("#AfterRenewelPopUp"); //Give ur div id here
        if (!window.data("kendoWindow")) {
            window.scroll(0, 0);
            window.kendoWindow({
                width: "730px",
                height: "610px",
                modal: true,
                draggable: false
            });
            window.data("kendoWindow").center();
        }
        window.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().css("background-image", "url('./Portals/0/images/close.png') !important");
            return false;
        });
        return false;
    }
    jQuery(document).ready(function () {
        if (flag) {
            Open();
        }
        $("#StartButton").click(function () {
            // $("#AfterRenewelPopUp").data("kendoWindow").close();
            // return true;
        });
        $("#AfterRenewelPopUp div div.AfterRenewelPopUpClassimg").click(function () {
            this.parentNode.children[1].click();
            if (this.parentNode.children[1].checked) {
                this.parentNode.children[0].className = " "; // ("AfterRenewelPopUpClassimg");
                this.parentNode.children[0].className = "AfterRenewelPopUpClassimg";
            }
            else {
                this.parentNode.children[0].className = " ";
                this.parentNode.children[0].className = "AfterRenewelPopUpClassunimg";

            }

        });
        function onResize() {
            return 0;
        }




    });

    function btnAccept_onclick() {
        $("#AfterRenewelPopUp").parent().appendTo("form");
    }

</script>
