<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderListViewControl.ascx.cs"
    Inherits="controls_OrderListViewControl" %>
<style type="text/css">
    .cen_pur_imgstyle
    {
        float: left;
        margin-top: -4px;
    }
    .summary
    {
        border-top: 2px solid #E4E4E4;
        border-left: 1px solid #E4E4E4;
        border-right: 2px solid #E4E4E4;
        border-bottom: 1px solid #E4E4E4;
        height: 240px;
        padding: 15px 22.5px 5px 22.5px !important; /*width: 97.4%;*/
        margin-left: -12px;
        margin-top: 10px;
        border-radius: 2px;
    }
    .ohist
    {
        border: 2px solid #E4E4E4;
        <%--height: 285px;--%>
        padding: 22px 22px 29px 22px;
        margin-top: 10px;
        border-radius: 2px; /*width: 97.4%;*/
        margin-left: -12px;
    }
    .yourorder1
    {
        border: 2px solid #E4E4E4;
        padding: 15px 22.5px 5px 22.5px; /*width: 97.4%;*/
        margin-left: -12px;
        margin-top: 10px;
        border-radius: 2px;
    }
    .summ
    {
        margin-top: 10px;
        font-weight: 600;
    }
    .heading
    {
        float: left;
        padding-bottom: 10px;
        text-align: center;
        font-weight: 600;
    }
    .heading1
    {
        padding-right: 21px;
        text-align: left;
    }
    .heading2
    {
        padding-right: 21px;
    }
    .heading3
    {
        padding-right: 21px;
    }
    .heading4
    {
        padding-right: 21px;
    }
    .heading5
    {
        padding-right: 21px;
    }
    .totvalue
    {
        float: left;
        padding-bottom: 10px;
    }
    .totvalue1
    {
        color: #828282;
        float: left;
        width: 78px;
        padding-bottom: 10px;
    }
    
    .headingvalue1
    {
        color: #ABABAB;
        float: left;
        width: 125px;
        padding-bottom: 10px;
    }
    
    .headingvalue2
    {
        color: #ABABAB;
        float: left;
        width: 162px;
        padding-bottom: 10px;
    }
    
    .headingvalue3
    {
        color: #ABABAB;
        float: left;
        width: 100px;
        padding-bottom: 10px;
    }
    
    .headingvalue4
    {
        color: #ABABAB;
        float: left;
        width: 128px;
        padding-bottom: 10px;
    }
    
    .headingvalue5
    {
        color: #ABABAB;
        float: left;
        width: 115px;
        padding-bottom: 10px;
    }
    
    .heading111
    {
        margin-top: 20px;
    }
    .headingval
    {
        height: 48px;
    }
    
    .dispatched
    {
        color: #FFFFFF;
        float: right;
        font-size: 10pt;
        height: 20px;
        margin-top: -5px;
        padding-top: 5px;
        text-align: center;
        width: 115px;
    }
    #orderheaderdiv, #purchasedby
    {
        height: 30px;
    }
    #orderheaderdiv
    {
        width: 102%;
    }
    #orderheadervaluediv, #purchasedbyvalue
    {
        height: 35px;
    }
    .pby
    {
        color: #828282;
        float: left;
        padding-right: 98px;
        padding-bottom: 10px;
    }
    .sto
    {
        color: #828282;
        float: left;
        padding-bottom: 10px;
    }
    
    .purchaseship
    {
        border-bottom: 2px solid #E4E4E4;
        height: 25px;
        margin-top: 61px;
        width: 725px;
        font-weight: 600;
    }
    .purchasevalue
    {
        color: #ABABAB;
        float: left;
        padding-right: 51px;
        padding-bottom: 10px;
        margin-top: 10px;
        font-weight: normal;
        width: 130px;
    }
    .shipptovalule
    {
        color: #ABABAB;
        float: left;
        margin-left: 11px;
        padding-bottom: 10px;
        margin-top: 10px;
        font-weight: normal;
    }
    .yourorderheading
    {
        color: #929292;
        float: left;
        width: 150px;
        padding-bottom: 10px;
        font-weight: 600;
    }
    .price
    {
        color: #929292;
        float: left; /*	padding-right: 85px;*/
        padding-bottom: 10px;
        font-weight: 600;
    }
    
    .yourorderheading1
    {
        color: #929292;
        float: left; /*	padding-right: 249px;*/
        width: 120px;
        padding-bottom: 10px;
    }
    .status
    {
        color: #929292;
        padding-bottom: 10px;
        float: right;
        padding-right: 24px;
        font-weight: 600;
    }
    
    .yourordheading
    {
        height: 35px;
        border-bottom: 2px solid #E4E4E4;
        margin-top: 20px;
    }
    
    .prdvalue
    {
        color: #ABABAB;
        float: left;
        font-weight: normal;
        text-align: left;
        padding-top: 19px;
        font-family: Arial;
    }
    
    
    
    .prdvalue1
    {
        padding-right: 64px;
        width: 75px;
    }
    .prdvalue2
    {
        padding-right: 45px;
        min-width: 200px;
    }
    
    .prdvalue3
    {
        padding-right: 70px;
    }
    
    .prdvalue4
    {
        padding-left: 10px;
    }
    .prdvalue5
    {
        padding-right: 63px;
        width: 101px;
    }
    .statusbutton
    {
        border-radius: 2px 2px 2px 2px;
        color: #FFFFFF;
        float: right;
        height: 20px;
        margin-top: 18px;
        padding-top: 5px;
        text-align: center; /*width: 115px;*/
    }
    .gradcancelled
    {
        background: -moz-linear-gradient(center top , #f15c5b 0%, #f15c5b 1%, #ed2731 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#f15c5b), to(#ed2731)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f15c5b', endColorstr='#ed2731', gradientType='0') !important;
        border: 2px solid #d86568;
    }
    
    .gradbackorder
    {
        background: -moz-linear-gradient(center top , #f8b358 0%, #f8b358 1%, #f78530 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#f8b358), to(#f78530)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f8b358', endColorstr='#f78530', gradientType='0') !important;
        border: 2px solid #e9b874;
    }
    
    .gradprogress
    {
        background: -moz-linear-gradient(center top , #fdcb4c 0%, #fdcb4c 1%, #fbb515 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#fdcb4c), to(#fbb515)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdcb4c', endColorstr='#fbb515', gradientType='0') !important;
        border: 2px solid #eed086;
    }
    
    .graddispatched
    {
        background: -moz-linear-gradient(center top , #a6c684 0%, #a6c684 1%, #79a55a 130%) repeat scroll 0 0 transparent !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#a6c684), to(#79a55a)) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#a6c684', endColorstr='#79a55a', gradientType='0') !important;
        border: 2px solid #97af82;
    }
    
    .ordhist
    {
        height: 35px;
        border-bottom: 2px solid #E4E4E4;
        margin-top: 20px;
    }
    
    .ordhistheading1
    {
        color: #929292;
        float: left;
        min-width: 380px; /*41%;*/
        padding-bottom: 10px;
    }
    .ordhistheading
    {
        color: #929292;
        float: left;
        min-width: 158px; /*16%;*/
        padding-bottom: 10px;
        font-weight: 600;
    }
    .ordhiststatus
    {
        color: #929292;
        padding-bottom: 10px;
        float: left;
        margin-left: -36px;
        font-weight: 600;
    }
    
    .full
    {
        color: #ABABAB !important; /*font-family: Arial,Sans-Serif;*/
        font-size: 11pt;
        float: right;
        text-decoration: none;
    }
    
    .ordhist
    {
        height: 27px;
    }
    
    .ordhistval
    {
        height: 46px;
        font-weight: normal;
        font-family: Arial,Sans-Serif;
    }
    .ordhistvalue
    {
        color: #ABABAB;
        float: left;
        padding-top: 19px;
    }
    
    .ordhistvalue1
    {
        padding-right: 56px;
    }
    .ordhistvalue2
    {
        padding-right: 160px;
    }
    
    .ordhistvalue4
    {
        padding-right: 12px;
        margin-left:190px;
    }
    .ordhistvalue5
    {
        padding-right: 0px;
        width: 165px;
    }
    .ordhistvaluestatus
    {
        border-radius: 2px 2px 2px 2px;
        color: #FFFFFF;
        float: right;
        height: 25px;
        margin-top: 13px;
        padding-top: 5px;
        text-align: center; /* width: 115px;*/
    }
    .sall
    {
        color: #848484 !important;
        text-decoration: none;
    }
    .seall
    {
        color: #ABABAB !important;
    }
    
    .seeall
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#B8B8B8), to(#ADADAD)) !important;
        background: -moz-linear-gradient(to bottom,#B8B8B8, #ADADAD) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#B8B8B8', endColorstr='#ADADAD', gradientType='0') !important;
        padding: 8px 22px 5px 22px;
        text-align: center;
        border-radius: 2px;
        font-weight: 500;
        margin-top: 5px;
        font-size: 8pt;
    }
    .seeall:hover
    {
        background: -webkit-gradient(linear, left top, left bottom, from(#adadac), to(#adadad)) !important;
        background: -moz-linear-gradient(to bottom,#adadac, #adadad) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#adadac', endColorstr='#adadad', gradientType='0') !important;
    }   
    .textord
    {
        /*font-family: Arial,Sans-Serif;*/
        font-size: 9pt;
        color: #707070;
    }
    .textordvalue
    {
        /*font-family: Arial,Sans-Serif;*/
        font-size: 9pt; /* color: #ABABAB !important;*/
        color: #707070 !important;
        
    }
    
    .textstatus
    {
        /*font-family: Arial,Sans-Serif;*/
        font-size: 9pt;
        font-weight: bold;
    }
    
    .line
    {
        border-bottom: 2px solid #E4E4E4;
        margin-top: 25px;
        position: absolute;
        width: 725px;
    }
    .quan
    {
        margin-left: 90px;
    }
    .ord
    {
        margin-left: 184px;
    }
    #Dispacted_Dispatched
    {
        font-family: Raleway;
        color: #91B76D;
    }
    #Dispacted_Cancelled
    {
        font-family: Raleway;
        color: #F1555B;
    }
    #Dispacted_Inprogress
    {
        font-family: Raleway;
        color: #F49944;
    }
    .ord_summary_style
    {
        font-weight: normal !important;
        font-family: Arial;
    }
    .purchasinghistory_outer
    {
        float: left !important;
        width: 865px;
        margin-left: -14px !important;
        margin-top: 20px;
    }
</style>
<div id="listview">
    <asp:Repeater ID="OrderSummary" runat="server">
        <ItemTemplate>
            <div id="Ordsummary" class="summary H4">
                <div class="h4 summ">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/ordersummary.png")%>" alt="" class="cen_pur_imgstyle" />
                    &nbsp;&nbsp; ORDER SUMMARY
                </div>
                <div id="orderheaderdiv">
                    <div class="heading111">
                        <div class="line">
                        </div>
                        <div class="heading heading1 textordvalue">
                            DATE PURCHASED
                            <br />
                            <br />
                            <br />
                            <span class="ord_summary_style">
                                <%# DataBinder.Eval(Container.DataItem,"purchaseddate")%></span></div>
                        <div class="heading heading2 textordvalue">
                            MY REFERENCE NUMBER
                            <br />
                            <br />
                            <br />
                            <span class="ord_summary_style">
                                <%# DataBinder.Eval(Container.DataItem,"referencenumber")%></span></div>
                        <div class="heading heading3 textordvalue">
                            ORDER TOTAL
                            <br />
                            <br />
                            <br />
                            <span class="ord_summary_style">
                                <%# DataBinder.Eval(Container.DataItem,"ordtotal")%></span></div>
                        <div class="heading heading4 textordvalue" style="margin-left: -11px;">
                            DISCOUNT TOTAL
                            <br />
                            <br />
                            <br />
                            <span class="ord_summary_style">
                                <%# DataBinder.Eval(Container.DataItem,"discount")%></span></div>
                        <div class="heading heading5 textordvalue">
                            SHIPPING CHARGE
                            <br />
                            <br />
                            <br />
                            <span class="ord_summary_style">
                                <%# DataBinder.Eval(Container.DataItem,"shippingcharge")%></span></div>
                        <div class="totvalue textord">
                            TOTAL PRICE<br />
                            <br />
                            <br />
                            <div style="text-align: center; font-weight: 800; font-family: Arial !important;">
                                <span class="ord_summary_style">
                                    <%# DataBinder.Eval(Container.DataItem, "total")%></span></div>
                        </div>
                    </div>
                    <div id="Dispacted_Dispatched" class="dispatched textstatus">
                        <%# DataBinder.Eval(Container.DataItem,"status")%></div>
                </div>
                <div id="purchasedby">
                    <div class="purchaseship">
                        <div class="pby textord">
                            PURCHASED BY
                        </div>
                        <div class="sto textord">
                            SHIPPED TO
                        </div>
                    </div>
                </div>
                <div id="purchasedbyvalue">
                    <div>
                        <div class="purchasevalue textordvalue">
                            <%# DataBinder.Eval(Container.DataItem,"username")%><br />
                            <%# DataBinder.Eval(Container.DataItem,"email")%></div>
                        <div class="shipptovalule textordvalue">
                            <%# DataBinder.Eval(Container.DataItem,"shippingaddress")%></div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div id="Yourorder" class="yourorder1 H4 ">
        <div class="h4 summ">
            <img src="<%=Page.ResolveUrl("Portals/0/Images/your_order.png")%>" class="cen_pur_imgstyle" />
            &nbsp;&nbsp; DETAILS
        </div>
        <div class="yourordheading">
            <div class="yourorderheading textord" style="padding-left: 0px; padding-right: 13px;">
                DATE PURCHASED
            </div>
            <div class="yourorderheading textord">
                ORDER NUMBER</div>
            <div class="yourorderheading textord" style="margin-left: -10px;">
                PRODUCT
            </div>
            <div class="price textord" style="margin-left: 160px;">
                QUANTITY</div>
            <div class="price textord" style="padding-left: 55px;">
                PRICE
            </div>
            <div class="status textord">
                STATUS</div>
        </div>
        <asp:Repeater ID="OrderDetails" runat="server" OnItemDataBound="OrderDetails_DataBound">
            <ItemTemplate>
                <div class="headingval">
                    <div class="prdvalue prdvalue5 text textordvalue" style="padding-left: 0px;">
                        <%# DataBinder.Eval(Container.DataItem, "purchaseddate1")%></div>
                    <div class="prdvalue prdvalue1 text textordvalue">
                        <%# DataBinder.Eval(Container.DataItem, "ordnum")%></div>
                    <div title='<%#Eval("productTitle")%>' class="prdvalue prdvalue2 text textordvalue">
                        <%# DataBinder.Eval(Container.DataItem, "product")%></div>
                    <div class="prdvalue prdvalue3 text textordvalue quan">
                        <%# DataBinder.Eval(Container.DataItem, "quantity")%></div>
                    <div class="prdvalue prdvalue4 text textordvalue">
                        <%# DataBinder.Eval(Container.DataItem, "price")%></div>
                    <div id="status1" runat="server" class="statusbutton textstatus">
                        <%# DataBinder.Eval(Container.DataItem, "status1")%></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    
    </div>
    <div id="orderhistory" class="ohist H4 ">
        <div class="h4 summ">
            <img src="<%=Page.ResolveUrl("Portals/0/Images/your_order.png")%>" class="cen_pur_imgstyle" />
            &nbsp;&nbsp;OTHER ORDERS <a href="dashboard.aspx?pagename=AllOrderDetails" class="full">
                See a full order history ></a>
        </div>
        <div class="ordhist">
            <div class="ordhistheading textord">
                DATE PURCHASED
            </div>
            <div class="ordhistheading textord" style="padding-right: 215px;">
                ORDER NUMBER</div>
            <div class="ordhistheading textord ord">
                ORDER VALUE</div>
            <div class="ordhiststatus textord">
                STATUS</div>
        </div>
        <asp:Repeater ID="OtherOrders" runat="server" OnItemDataBound="OtherOrders_DataBound">
            <ItemTemplate>
                <div class="ordhistval">
                    <div class="ordhistvalue ordhistvalue5 textordvalue" style="padding-left: 0px;">
                        <%# DataBinder.Eval(Container.DataItem, "purchaseddate")%></div>
                    <div class="ordhistvalue ordhistvalue1 textordvalue">
                        <%# DataBinder.Eval(Container.DataItem, "ordernumber")%></div>
                    <div class="ordhistvalue ordhistvalue2 textordvalue">
                         <i><a href="dashboard.aspx?pagename=SingleOrder&orderid=<%# DataBinder.Eval(Container.DataItem, "ordernumber")%>" class="sall">See
                    Details</a></i></div>
                    <div class="ordhistvalue ordhistvalue4 textordvalue">
                        <%# DataBinder.Eval(Container.DataItem, "ordtotal")%></div>
                    <div id="status" runat="server" class="ordhistvaluestatus textstatus">
                        <%# DataBinder.Eval(Container.DataItem, "status")%></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
       
        <a href="dashboard.aspx?pagename=AllOrders" style="margin-top: -2px; margin-left: 420px;
            float: left; text-decoration: none; color: white !important;" class="seall textordvalue">
            <div class="seeall">
                SEE ALL</div>
        </a>
    </div>
    &nbsp;
    <br />
    &nbsp;
    <br />
    &nbsp;
</div>
<style type="text/css">
    .ordersattr
    {
        border-bottom: 2px solid #E4E4E4;
        margin-top: 10px;
        padding-left: 10px;
        width: 175px;
    }
    
    .shipaddr, .userdet
    {
        border: 2px solid #E4E4E4;
        margin-top: 10px;
        padding-left: 10px;
    }
    
    .topalign1
    {
        margin-top: 10px;
    }
    .purship
    {
        width: 196px;
        margin-top: 10px;
    }
    
    .shipaddr
    {
        padding-bottom: 20px;
    }
    .userdet
    {
        padding-bottom: 10px;
    }
    
    .textstyle1
    {
        color: #717171; /*font-family: Arial,Sans-Serif;*/
        font-size: 10pt;
        font-weight: bold;
        padding-bottom: 10px;
    }
    
    .textstyle2
    {
        color: #6C6C6C; /*font-family: Arial,Sans-Serif !important;*/
        font-size: 10pt !important;
        font-weight: bold !important;
        padding-bottom: 5px;
    }
    
    .textstyle3
    {
        color: #939393 !important; /*font-family: Arial,Sans-Serif;*/
        font-size: 10pt;
    }
    
    .padlef
    {
        padding-left: 10px;
    }
    
    .rightprd
    {
        border-left: 1px solid #E4E4E4;
        float: left;
        height: 765px;
        margin: 10px 15px 20px;
        width: auto;
    }
    
    #left
    {
        float: left;
    }
    .prd1
    {
        float: left;
        margin-left: 10px;
        margin-top: -14px;
    }
    
    .prdrow1
    {
        padding-bottom: 10px;
        width: 730px;
        height: 765px;
    }
    
    .prdrow2
    {
        padding-bottom: 10px;
        width: auto;
    }
    
    
    .quantitycircle
    {
        color: #FFFFFF;
        padding-top: 5px;
        position: relative;
        background: transparent;
    }
    
    .eachprd
    {
        float: left;
        margin-left: 20px;
        margin-top: 10px;
        margin-right: 15px;
    }
    
    #gridview
    {
        margin-top: 10px;
    }
    .gridsat
    {
        float: none;
        height: 25px;
        margin: 165px 10px 10px 20px;
        width: auto !important;
    }
    .prdimgage
    {
        width: 130px;
    }
</style>
<div id="gridview">
    <div id="left">
        <div class="ordersattr">
            <div class="textstyle1">
                ORDER NUMBER</div>
            <br />
            <div class="textstyle2">
                Date Purchased</div>
            <div class="textstyle3">
                21st July2015</div>
            &nbsp;
            <div class="textstyle2">
                My reference number</div>
            <div class="textstyle3">
                [Custom Label]</div>
            &nbsp;
            <div class="textstyle2">
                Order total</div>
            <div class="textstyle3">
                $444.11 AU (inc GST)
            </div>
            &nbsp;
            <div class="textstyle2">
                Discount total
            </div>
            <div class="textstyle3">
                $40 AU
            </div>
            &nbsp;
            <div class="textstyle2">
                Shipping charge</div>
            <div class="textstyle3">
                $10.00 AU</div>
            &nbsp;
            <div class="textstyle2">
                Total purchase price</div>
            <div class="textstyle3">
                $444.11 AU (inc GST)
            </div>
            &nbsp;
        </div>
        <div class="purship">
            <div class="textstyle2 padlef">
                Shipped to
            </div>
            <div class="shipaddr textstyle3">
                [Shipping Address]
            </div>
            <div class="textstyle2 topalign1 padlef">
                Purchased by</div>
            <div class="userdet textstyle3">
                [Users full name]<br />
                [Users email address]
            </div>
            &nbsp;
        </div>
    </div>
    <div class="rightprd">
        <div class="prdrow1">
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/book1.png")%>" /></div>
                <div class="statusbutton gradcancelled textstatus gridsat">
                    CANCELLED</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/book2.png")%>" /></div>
                <div class=" statusbutton gradbackorder textstatus gridsat">
                    ON BACKORDER</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/space_book.png")%>" /></div>
                <div class=" statusbutton gradbackorder textstatus gridsat">
                    ON BACKORDER</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/TheLittleBlueHorse.png")%>" /></div>
                <div class=" statusbutton gradbackorder textstatus gridsat">
                    ON BACKORDER</div>
            </div>
            <br />
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/britain_book.png")%>" /></div>
                <div class="statusbutton gradcancelled textstatus gridsat">
                    CANCELLED</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/hist2.png")%>" /></div>
                <div class="statusbutton gradprogress textstatus gridsat">
                    IN PROGRESS</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/rock_book.png")%>" /></div>
                <div class="statusbutton gradprogress textstatus gridsat">
                    IN PROGRESS</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/music_book.png")%>" /></div>
                <div class="statusbutton gradprogress textstatus gridsat">
                    IN PROGRESS</div>
            </div>
            <br />
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/aqa_book.png")%>" /></div>
                <div class="statusbutton graddispatched textstatus gridsat">
                    DISPATCHED</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/eng_book.png")%>" /></div>
                <div class="statusbutton graddispatched textstatus gridsat">
                    DISPATCHED</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/jobdetail_book2.png")%>" /></div>
                <div class="statusbutton graddispatched textstatus gridsat">
                    DISPATCHED</div>
            </div>
            <div class="eachprd">
                <div class="quantitycircle">
                    <img src="<%=Page.ResolveUrl("Portals/0/Images/quantitycircle.png")%>" /></div>
                <div class="prd1">
                    <img class="prdimgage" src="<%=Page.ResolveUrl("Portals/0/Images/lang_book.png")%>" /></div>
                <div class="statusbutton graddispatched textstatus gridsat">
                    DISPATCHED</div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#dashboardouter').addClass('purchasinghistory_outer');
    });
    if ($.browser.mozilla) {
    $('.seeall').css('margin-top', '3px');
        $('.line').css('width', '730px');
        $('.purchaseship').css('width', '730px');
    }
    if ($.browser.msie) {
        $('.line').css('width', '718px');
        $('.purchaseship').css('width', '718px');
    }
    if (parseInt($.browser.version) > 9) { $('.line').css('width', '718px'); $('.purchaseship').css('width', '718px'); }
</script>
