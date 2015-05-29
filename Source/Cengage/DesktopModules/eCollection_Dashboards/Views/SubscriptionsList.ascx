<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionsList.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.SubscriptionsList" %>
<style type="text/css">
    .k-window-action
    {
        display: block !important;
        visibility: visible !important;
    }
    .CreateLinkStyle
    {
        float: left;
        color: #47C5DC !important;
        margin-left: 31%;
    }
</style>
<div id="MessageOuterDiv" runat="server" style="width: 98%; margin-left: 4%; position: static;
    display: none;">
    <div class="bubble1">
        <asp:Label ID="Message1" runat="server" Text="" />
        <asp:HyperLink ID="CreateLink" ClientIDMode="Static" NavigateUrl="" Text="Purchase the first one now?"
            CssClass="CreateLinkStyle HideItems" runat="server" />
        <a href="/pmecollection" class="CreateLinkStyle">Purchase the first one now?</a>
    </div>
</div>
<div id="SubstopDiv" class="Div_FullWidth SubstopDiv" runat="server" clientidmode="Static">
    <h4 id="SubsHeadingid" runat="server" clientidmode="Static" class="SubsHeading">
        You have multiple PM eCollection subscriptions
    </h4>
    <h6 class="SubsSelectHeading">
        Please select the subscription you would like to work with:
    </h6>
    <div class="Div_FullWidth">
        <asp:Repeater ID="SubscriptionTypeRptr" runat="server" OnItemDataBound="SubscriptionTypeRptr_ItemDataBound" >
            <ItemTemplate>
                <div id="SubsListHdr" class="Div_FullWidth SubsHeadings">
                    <h6 class="SubsSelectHeading">
                        Your <span id="SubscriptionType" runat="server">
                            <%# Eval("SubscriptionType") %></span> :
                    </h6>
                    <div id="SubsListContainer" class="SubsListDiv">
                        <asp:Repeater ID="SubsListRptr" runat="server" onitemcommand="SubsListRptr_ItemCommand">
                            <ItemTemplate>
                            <asp:HiddenField ID="custsubsskhdn" runat="server" ClientIDMode="Static" Value='<%# Eval("SubscriptionId")%>' />
                               <%-- <div class="SubsContent">
                                    <div class="SubsImage">
                                    </div>
                                    <div id='SubsText<%# Eval("SubsId")%>' class="SubsTitle">
                                        <%# Eval("Name")%></div>
                                    <div id='<%# Eval("SubsId")%>1a' class="SubsTitleLbl" title='<%# Eval("TitleText")%>'>
                                        <%# Eval("NewName")%></div>
                                    <div class="SubsManageDiv">
                                        <input id='Addbtn1<%# Eval("SubsId")%>' type="button" class='AddLblBtn <%# Eval("ClassName")%>'
                                            value="<%# Eval("AddEditBtnText")%>" onclick="AddLabel('<%# Eval("AddEditBtnText")%>','<%# Eval("SubsId")%>')" />
                                        <asp:Button ID="SubsUseButton1" ClientMode="Static" runat="server" CssClass="UseBtn"
                                            Text="OPEN" CommandArgument='<%# Eval("SubsId")%>' OnClick="UseButton_Click" />
                                    </div>
                                </div>--%>
                                <div class='SubsContent <%#Eval("OpacityClsName")%>'>
                                    <div class="Div_FullWidth">
                                        <div class="SubsTitleLbl">
                                            <span id='SubsText<%# Eval("SubscriptionId")%>' class="SubsTitleLbl HideItems" title='<%# Eval("TitleText")%>'><%# Eval("SubscriptionText")%></span>
                                            <span id='<%# Eval("SubscriptionId")%>' class="SubsTitleLbl" title='<%# Eval("TitleText")%>'><%# Eval("SubscriptionName")%></span>
                                            <span class="floatleft EditLblspan <%# Eval("EdtClsName")%>">(</span><a id='Addbtn<%# Eval("SubscriptionId")%>' href="#" class='floatleft AddLblBtn <%# Eval("EdtClsName")%>'
                                                onclick="AddLabel('editlabel','<%# Eval("SubscriptionId")%>')">edit label)</a>
                                        </div>
                                    </div>
                                    <div class="Div_FullWidth">
                                        <div style="width: 50%; float: left;">
                                            <div class="Div_FullWidth licBooksendDiv">
                                                licences / students: <%# Eval("TotalLicenses")%>
                                            </div>
                                            <div class="Div_FullWidth licBooksendDiv">
                                                book pack: <%# Eval("TotalBooks")%>
                                            </div>
                                            <div class="Div_FullWidth licBooksendDiv">
                                                expires: <%# Eval("EndDate")%>
                                            </div>
                                        </div>
                                        <div style="width: 45%; float: right;">
                                            <div class='SubsManageDiv <%# Eval("SrtDateClsName")%>'>
                                                <span class='startdatespan <%# Eval("SrtDateClsName")%>'>Available from <%# Eval("StartDate")%></span>
                                            </div>
											 <div class='SubsManageDiv <%# Eval("ExpiredClsName")%>'>
                                                <span class='startdatespan <%# Eval("ExpiredClsName")%>'>EXPIRED</span>
                                            </div>
                                            <div class='SubsManageDiv <%#Eval("UseBtnClsName")%>'>
                                                <asp:Button ID="SubsUseButton" ClientMode="Static" runat="server" CssClass="UseBtn" Text="USE"
                                                    CommandArgument='<%# Eval("SubscriptionId")%>' OnClick="UseButton_Click" />
                                            </div>
                                           
                                        </div>
										
										<div class="<%# Eval("remiderdate")%>" style="clear:both;line-height: 50px; float: left;border: 1px solid #BE1F16;margin-top: 10px; padding:0 12px;"> 
										  <span style="color: #BE1F16;font-weight: normal;margin:0 20px 0 6px; font-family: raleway-bold;">Expires on: <%#Eval("EndDate")%></span>
										  <%--<input type="submit" class="UseBtn2" value="Renew for 2015" clientmode="Static" style="
											width: 128px;">--%>

        <asp:Button ID="btnRenew" CssClass="UseBtn2" runat="server" Text='<%# String.Concat("Renew for ",Eval("endyear"))%>' ClientIDMode="Static" CommandName="RENEW"  style="
											width: 128px;"  />      
										</div>
                                    </div>
                                </div>
								
								 <div class='button-opacity <%#Eval("ReActivateBtnClsName")%>'>
                                           <input type="button" id="btnReactivate" class="UseBtn" value="RE-ACTIVATE"
                                                  onclick="javascript:setcustsubsk('<%# Eval("SubscriptionId")%>','<%#Eval("SUBS_ID")%>','<%# Eval("TotalLicenses")%>','<%# Eval("SUB_QTY_ALL")%>')"/>
                                                
                                            </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField ID="SelectedSubsHdn" runat="server" ClientIDMode="Static" />
</div>
<div id="AddNameLabel" class="PopUpHolder">
    <div class="PopUpHeader">
        <span id="popupHeader" class="PopupHeaderSpan"></span>
    </div>
    <div class="PopUpMsgHolder">
        <div class="PopUpMsgInnerHolder">
            <literal class="PopUpMsg">Subscription Name :</literal>
            <input id="SubsNameTxt" type="text" class="PopTxtBx" maxlength="30" />
        </div>
        <div class="PopUpBtnHolder">            
            <input type="button" id="CancelButton" value="Cancel" class="popupokbtn" />
            <input type="button" id="OkButton" value="Ok" class="popupokbtn" />
        </div>
    </div>
</div>
  <div id="renewPanediv" class="censubspopupclass popupClass">
     
            <asp:PlaceHolder ID="renewPanePlaceHolder" runat="server"></asp:PlaceHolder>  

  </div> 
<script src="desktopmodules/ecollection_dashboards/Scripts/SubscriptionList.js" type="text/javascript"></script>
<input type="hidden" runat="server" id="UserRoleHdn" clientidmode="Static" />
<asp:HiddenField ID="parentsubsid" runat="server" ClientIDMode="Static" />