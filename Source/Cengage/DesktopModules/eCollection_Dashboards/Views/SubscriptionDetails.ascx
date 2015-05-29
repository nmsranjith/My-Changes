<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionDetails.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.SubscriptionDetails" %>
<div id="SubDetailsDiv" class="SubDetailsDiv">
    <div class="SubDetailsDiv_OuterBox1">
        <div class="SubDetailsDiv_InnerBox">
            <div class="SubDetailsDiv_InnerBox_Values1">
                AVAILABLE</div>
            <div class="SubDetailsDiv_InnerBox_Values2">
                LICENCES:</div>
            <div class="SubDetailsDiv_InnerBox_Values3">
                <span id="UsedLicenses" runat="server" clientidmode="Static" style="color: #20B3E6;">
                    0</span> OF <span id="TotalLicenses" runat="server">0</span>
            </div>
        </div>
        <div class="SubDetailsDiv_InnerBox" style="float: right;">
            <div class="SubDetailsDiv_InnerBox_Values4">
                <span id="AddedBooks" runat="server">0</span>
            </div>
            <div class="SubDetailsDiv_InnerBox_Values5">
                BOOK/S</div>
        </div>
    </div>
    <div class="SubDetailsDiv_OuterBox2" id="upgradeDivEcoll" runat="server">
        <div class="SubDetailsDiv_InnerBox">
            <div class="SubDetailsDiv_InnerBox_Values4">
                <span id="DaysLeft" runat="server">0</span></div>
            <div class="SubDetailsDiv_InnerBox_Values5">
                DAY/S LEFT</div>
        </div>
        <div id="UpgradeBox" class="SubDetailsDiv_InnerBox UpgradeBox">
            <div class="SubDetailsDiv_InnerBox_Values6" style="width: 100%;">
                <a id="UpgradeLink" class="UpgradeLink" runat="server" visible="false">UPGRADE</a>
                <div id="UpgradeTrialLink" onclick="javascript:popuprenew(1,'')" class="UpgradeTrialDiv"
                    visible="false" runat="server">
                    UPGRADE</div>
            </div>
        </div>
    </div>
	
	<div id="renewDiv" runat="server" style="float: right;border: 1px solid #BE1F16; padding: 34px 0 0; font-size: 12px; height: 83px; text-align: center; width: 316px"> 
		<asp:Label ID="expdate" runat="server" style="color: #BE1F16;font-weight: normal; font-family: raleway-bold; letter-spacing: 1px;"></asp:Label><br/>
										  
<asp:Button id="btnRenew" runat="server" CssClass="UseBtn2" OnClick="btnRenew_Click" style="width: 140px; margin-top: 10px;font-family: raleway-bold;letter-spacing: 1px;font-size: 13px;" />   
	</div>
	
	
</div>
<div id="DaysLeftDiv" runat="server" clientidmode="Static" visible="false">
    <h3 class="alertText">
        Action Required</h3>
    <a class="DaysLeftLink" href="/ecollection/books.aspx">You have <span id="GraceDaysLeft"
        runat="server" clientidmode="Static"></span> day/s to complete the book selection
        for your subscription.</a>
</div>
<div id="renewPanediv" class="censubspopupclass popupClass">
     
            <asp:PlaceHolder ID="renewPanePlaceHolder" runat="server"></asp:PlaceHolder>  

  </div>
<asp:HiddenField ID="parentsubsid" runat="server" ClientIDMode="Static" />
