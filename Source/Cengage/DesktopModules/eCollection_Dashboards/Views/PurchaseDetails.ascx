<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PurchaseDetails.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.PurchaseDetails" %>
    
<div id="TotalPurchase" class="Div_FullWidth" style="position:relative;">
    <asp:Repeater ID="PurchaseDetailrepeater" runat="server">
        <ItemTemplate>
            <div id="eCollectionPurchasedDiv" class="eCollectionPurchasedDiv">
                <div class="Div_FullWidth">
                    <div class="StarImg">
                    </div>
                     <h5 class="eCollectionPurchasedDiv_Label">
                        PM ECOLLECTION PURCHASE
                    </h5>
                    <div class="DateDiv">    
                        <i><%# string.Format("{0:dd}{1} {0:MMMM yyyy}", DateTime.Parse(Eval("StartedDate").ToString()), (DateTime.Parse(Eval("StartedDate").ToString()).Day == (1)) ? "st" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (21)) ? "st" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (2)) ? "nd" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (22)) ? "nd" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (3)) ? "rd" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (23)) ? "rd" : (DateTime.Parse(Eval("StartedDate").ToString()).Day == (31)) ? "st" : "th")%></i>
                    </div>
                    <div class="Div_FullWidth">
                        <hr class="Dashboard_hr3" />
                    </div>
                    <div class="eCollectionPurchasedDiv_AdminName">
                        By: <i>
                            <%# Eval("AdminName")%></i>
                    </div>
                </div>
                <div class="Div_FullWidth">
                    <div class="SubDetailsDiv_OuterBox1">
                        <div class="SubDetailsDiv_InnerBox">
                            <div class="SubDetailsDiv_InnerBox_Values1">
                                AVAILABLE</div>
                            <div class="SubDetailsDiv_InnerBox_Values2">
                                LICENCES:</div>
                            <div class="SubDetailsDiv_InnerBox_Values3">
                                <span id="UsedLicenses" runat="server" style="color: #20B3E6;">
                                    <%# Eval("AvailableLicenses")%></span> OF <span id="TotalLicenses" runat="server">
                                        <%# Eval("TotalLicenses")%></span></div>
                        </div>
                        <div class="SubDetailsDiv_InnerBox" style="float: right;">
                            <div class="SubDetailsDiv_InnerBox_Values4">
                                <span id="AddedBooks" runat="server">
                                    <%# Eval("BooksAdded")%></span></div>
                            <div class="SubDetailsDiv_InnerBox_Values5">
                                BOOK/S</div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

