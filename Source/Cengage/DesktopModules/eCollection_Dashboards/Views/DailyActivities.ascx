<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyActivities.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.DailyActivities" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<div id="MsgDiv" style="float: left; width: 100%;">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<asp:UpdatePanel ID="LoadMorepanel" runat="server" ClientIDMode="Static" >
    <ContentTemplate>
        <div class="DashboardDiv">
            <asp:Repeater ID="ActivityRepeater" runat="server" OnItemDataBound="ActivityRepeater_ItemDataBound">
                <ItemTemplate>
                    <div id="ContentHdr<%# Container.ItemIndex %>" class="Div_FullWidth ActivitiesContent">
                        <div id="GroupsCreatedDiv<%# Container.ItemIndex %>" class="GroupsCreatedDiv">
                            <div class="Div_FullWidth">
                                <div class="StarImg">
                                </div>
                                <h5 class="UpgradedDetailsDiv_Label">
                                    <asp:Label ID="Activity" runat="server" Text='<%# Eval("ActivityType")%>'></asp:Label>
                                </h5>
                                <div class="DateDiv">
                                    <i id="GrpCrtDate<%# Container.ItemIndex %>" class="DataLine">
                                        <asp:Label ID="Date" runat="server" Text='<%# Eval("DateCreated")%>'></asp:Label>
                                    </i>
                                </div>
                            </div>
                            <div class="Div_FullWidth">
                                <hr class="Dashboard_hr3" />
                            </div>
                            <div class="GroupsCreatedDiv_Content">
                                <div class="GroupsCreatedDiv_AdminName">
                                    <i class="DataLine">
                                        <asp:Label ID="User" runat="server" Text='<%# Eval("AddedBy")%>'></asp:Label>
                                    </i>&nbsp;<%# Eval("Prefix")%>:
                                </div>
                                <div id="GrpContentDiv" class="GroupsCreatedDiv_Content_Item">
                                    <asp:Repeater ID="ContentRepeater" runat="server">
                                        <ItemTemplate>
                                            <div class='<%# Eval("ClassName")%>' title='<%# Eval("TitleString")%>'>
                                                <%# Eval("Name")%>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:UpdatePanel ID="BookRprPanel" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <div class="Div_FullWidth">
                                                <asp:Repeater ID="BookRepeater" runat="server" OnItemDataBound="BookRepeater_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="DashBoard_Items_books">
                                                            <asp:Image ID="BookCoverImg" CssClass="DashBoard_Items_books_images" runat="server"
                                                                ImageUrl='<%# Eval("TitleString")%>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:Repeater ID="Upgraded" runat="server">
                                        <ItemTemplate>
                                            <%# Eval("TitleString")%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="seeallHdr">
                                            <asp:LinkButton ID="SeeAllBtn" Class="SeeAllBtn" runat="server" Text="See all" Visible="false"
                                                OnClick="SeeAllBtn_Click"></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="Div_FullWidth">
        <asp:Image ID="LoadMoreImg" ImageUrl="~/portals/0/images/progress.gif" runat="server" AlternateText="Loading.." ClientIDMode="Static" CssClass="HideItems" Style="margin: 20px 20px 32px 200px;" />
        <asp:Button ID="LoadMoreButton" runat="server" OnClick="LoadMore_Click" CssClass="HideItems" CommandArgument="50" ClientIDMode="Static"/>        
        <asp:HiddenField ID="BoolCount" runat="server" ClientIDMode="Static" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="ItemCount" runat="server" Value="0" />
<asp:UpdatePanel ID="SelectedrprPanel" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="SelectedBkRpr" runat="server" ClientIDMode="Static" />
    </ContentTemplate>
</asp:UpdatePanel>
<div class="HideItems">
</div>
<script src="desktopmodules/ecollection_dashboards/Scripts/DailyActivities.js" type="text/javascript"></script>
