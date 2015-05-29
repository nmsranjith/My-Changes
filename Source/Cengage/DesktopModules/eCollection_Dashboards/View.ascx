<%@ Control Language="C#" Inherits="DotNetNuke.Modules.eCollection_Dashboards.View"
    AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<%@ Register Src="Views/eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<style type="text/css">
    .MulSubsBtn
    {
        margin-top: 6px !important;
    }
</style>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<div id="MainDiv" class="MainDiv">
    <div id="eCollectionMenu" class="eCollectionMenuStyle">
        <asp:PlaceHolder ID="eCollectionMenuPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="FunctionalityPlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    <div id="eCollectionContent" class="eCollectionContentStyle">
        <asp:PlaceHolder ID="ContentPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="SubsDetailsPlaceHdr" runat="server" ClientIDMode="Static"></asp:PlaceHolder>
        <asp:PlaceHolder ID="VideoPlaceHdr" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="DailyActivitiesPlaceHdr" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="PurchaseDetailsPlaceHdr" runat="server"></asp:PlaceHolder>
        <div class="HideItems"><asp:PlaceHolder ID="PurchasePlaceHolder" runat="server"></asp:PlaceHolder></div>
    </div>
</div>
<div class="HideItems">
    <asp:HiddenField ID="SubsCnt" runat="server" ClientIDMode="Static" />
    <div id="SelectedSubs">
        <div class="Div_FullWidth SubsBannerDiv">
            You are using <span id="SelectedSubscription" runat="server" clientidmode="Static">
            </span>
        </div>
    </div>
</div>
<asp:HiddenField ID="schoolname" runat="server" ClientIDMode="Static" />
<script src="desktopmodules/ecollection_dashboards/Scripts/Common.js" type="text/javascript"></script>
