<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sessions.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.Sessions" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%--<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Sessions/Scripts/SessionList.js"
    ForceProvider="DnnFormBottomProvider" />
<div id="MessageOuterDiv" runat="server" class="ess-messageouterDiv">
    <div class="bubble1">
        <asp:Label ID="Message1" runat="server" Text="" />
        <asp:HyperLink ID="CreateLink" ClientIDMode="Static" NavigateUrl="" Text="Create the first one now?"
            CssClass="CreateLinkStyle" runat="server" />
    </div>
</div>
<div id="SessionListDiv" runat="server" clientidmode="Static">
    <div class="GroupsDivtest">
        <div id="dialog-message">
        </div>
        <h2>
            Sessions</h2>
        <div class="ProgressDivClass HideItems" id="UpdateProgressImg" style="display:none;">
            <div class="ProgressInnerDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                    alt="Processing" />
            </div>
        </div>
        <div class="search-bar">
            <div class="eClUserSearchDiv">
                <label id="eClSearchLabel" for="SearchTextBox">
                    Search:</label>
                <input type="text" id="SearchTextBox" class="SearchTextCss" runat="server" title="session name"
                    clientidmode="Static" />
            </div>
            <div class="Searchbtndiv">
                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
                <asp:Button ID="SearchButton" runat="server" CssClass="SearchButton" ClientIDMode="Static"
                    OnClick="SearchButton_Click" OnClientClick="ShowUpdate()" />
                <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <div class="selectors">
        <div class="select-all">
            <div id="SelectAllDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBox"
                    id="SelectallCheckBox" alt="Image cannot be displayed" />
                <span id="SelectAllChkbx" class="ico-uncheck"></span>
            </div>
            <span class="SelectAllSpan" onclick="javascript:ClickSelectAll();">Select All</span>
        </div>
        <%-- <div class="GroupsSortReaddiv">
            <asp:Button ID="SortingButton"  CssClass="SortRead" runat="server"  Text="A to Z" OnClick="NamesAscButton_Click"/>
            <b style="float:right">|</b>
            <asp:Button ID="ReadingLevelButton" runat="server" class="ess-ReadinglevelButton"  CssClass="SortRead"  Text="DATE CREATED" />
        </div>--%>
        <div class="GroupsSortReaddiv">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" class="ess-UpdatePanel1" >
                        <ContentTemplate>--%>
            <asp:Button ID="DateCreatedAscButton" class="ess-DateCreatedAscButton" Text="DATE CREATED"
                runat="server" OnClick="DateCreatedAscButton_Click" OnClientClick="checkOnPageBack()"
                CssClass="SortRead" />
            <asp:Button ID="DateCreatedDescButton" class="ess-DateCreatedDescButton" Text="DATE CREATED"
                runat="server" OnClick="DateCreatedDescButton_Click" OnClientClick="checkOnPageBack()"
                CssClass="SortReadDesc" Visible="False" />
            <%--     </ContentTemplate>
                    </asp:UpdatePanel>--%>
            <b class="ess-b">| </b>
            <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server" class="ess-UpdatePanel5" >
                        <ContentTemplate>--%>
            <asp:Button ID="NamesAscButton" class="ess-NamesAscButton" runat="server" Text="A – Z"
                OnClick="NamesAscButton_Click" OnClientClick="checkOnPageBack()" CssClass="SortRead" />
            <asp:Button ID="NamesDescButton" class="ess-NamesDescButton" runat="server" Text="A – Z"
                OnClick="NamesDescButton_Click" OnClientClick="checkOnPageBack()" CssClass="SortReadDesc"
                Visible="False" />
            <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
        </div>
        <div class="categorybysort">
            <label for="" class="">
                Sort Results:</label>
            <select name="" id="sessionssortdpn">
                <option value="">Recommended</option>
                <option value="">Title (A to Z)</option>
                <option value="">Title (Z to A)</option>
                <option value="">Latest</option>
            </select>
        </div>
    </div>
    <div class="ClassSearchDiv">
        <div class="KenduoComboBoxClassDiv">
            <div class="counts">
                <div class="History_WordCountBG">
                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                </div>
                <div class="History_WordCount">
                    <asp:Label ID="lblActiveSessionCount" runat="server"></asp:Label>
                </div>
            </div>
            <%--<div style="width: 27px;height:28px; padding-left:43px;margin-left: -30px; margin-top: -29px;">
            <img id="Img1" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" style="width:27px;height:28px">
            <div style="margin-left:8px;margin-top:-25px"> 
            
            </div>
        </div>--%>
            <div class="hide-show">
                <%-- <asp:TextBox ID="classSearchTextBox" AutoPostBack="true" class="KenduoSearchTextBox"
                ClientIDMode="Static" runat="server" OnTextChanged="classSearchTextBox_TextChanged"></asp:TextBox>--%>
                <asp:Button ID="ActiveSessionsButton" runat="server" CssClass="ActiveSessionsButton"
                    ClientIDMode="Static" OnClientClick="return false;" Text="Active sessions" Visible="false" />
                <span class="student-class">Active sessions</span> <span class="student-line"></span>
                <span class="btn btn-general">Hide</span>
            </div>
        </div>
    </div>
    <div>
        <div id="ActiveSessionOuterDiv">
            <asp:Repeater ID="ActiveSessionList" runat="server" ClientIDMode="Static">
                <ItemTemplate>
                    <div id="ClassRepeaterDiv" class="Div_FullWidth">
                        <div id="RepeaterFstCol" class="gchkbx">
                            <input id="ClassCheckBoxes" clientidmode="Static" type="checkbox" class="HideItems"
                                runat="server" />
                            <img id="ClassCheckBoxImg" class="HideItems" alt="image cannot be displayed" clientidmode="Static"
                                src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                            <span class="ico-uncheck "></span>
                            <asp:Label ID="SessionId" runat="server" ClientIDMode="Static" Text='<%# Eval("SessionId") %>'
                                class="HideItems"></asp:Label>
                        </div>
                        <div id="ClassRepeaterContentDiv" class="ClassRepeaterSubDiv">
                            <div class="contents-list">
                                <div class="contents-msg">
                                    <asp:Label ID="MembersLabel" CssClass="H4Heading" runat="server" Text='<%# (Eval("Name").ToString().Length>=38 ? Eval("Name").ToString().Substring(0,38) :  Eval("Name").ToString()) %>'></asp:Label>
                                    <span class="H6Heading PaddingL5">(<%# Eval("UnOpened")%></span> <span class="H6Heading">
                                        unopened)</span>
                                    <asp:Label ID="TeacherNameLabel" runat="server" Visible="false" Text='<%# Eval("Name") %>'></asp:Label></div>
                                <div class="session-date">
                                    <span>
                                        <%# Convert.ToDateTime(Eval("SessionCreatedDate")).ToString("dd/MM/yyyy")%></span></div>
                            </div>
                            <div class="repeater-btn-ctrls">
                                <a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=seconds">
                                    Edit</a>
                                <asp:Button ID="ClassProfileButton" Text="VIEW" runat="server" CssClass="btn btn-affermative"
                                    OnClick="ClassProfileButton_Click" CommandArgument='<%# Eval("SessionId") %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="ClassSearchDiv">
        <div class="KenduoComboBoxClassDiv">
            <div class="counts">
                <div class="History_WordCountBG">
                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                </div>
                <div class="History_WordCount">
                    <asp:Label ID="lblFinishedSessionCount" runat="server"></asp:Label>
                </div>
            </div>
            <%-- <div style="width: 27px;height:28px; padding-left:43px;margin-left: -30px; margin-top: -29px;">
            <img id="Img2" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" style="width:27px;height:28px">
            <div style="margin-left:8px;margin-top:-25px">
            <asp:Label ID="lblFinishedSessionCount" runat="server" CssClass="H5Heading"></asp:Label>
            </div>
        </div>--%>
            <div class="hide-show">
                <%--<asp:Button ID="FinishedSessionsButton" runat="server" Text="Finished Sessions" 
                CssClass="AllOtherGroupBtn"
                onclick="FinishedButton_Click" style="margin-left:0px;" />--%>
                <%--<div id="divfinish" >--%>
                <%-- <asp:TextBox ID="FinishedSessionTextBox" AutoPostBack="true" class="KenduoSearchTextBox"
                    ClientIDMode="Static" runat="server" OnTextChanged="FinishedSessionTextBox_TextChanged"
                    Height="33px" Style="box-shadow: none !important; border: 0px"></asp:TextBox>--%>
                <%-- </div>--%>
                <asp:Button ID="FinishedSessionsButton" runat="server" CssClass="FinishedSessionsButton"
                    ClientIDMode="Static" OnClientClick="return false;" Text="Finished sessions" />
                <span class="student-line"></span><span class="btn btn-general">Hide</span>
            </div>
        </div>
    </div>
    <div>
        <div id="FinishedSessionOuterDiv">
            <asp:Repeater ID="FinishedSessionList" runat="server" ClientIDMode="Static">
                <ItemTemplate>
                    <div class="Div_FullWidth">
                        <div id="GroupRepeaterContentDiv" class="gchkbx">
                            <input id="GroupCheckBoxes" clientidmode="Static" class="HideItems" type="checkbox"
                                runat="server" />
                            <img id="ClassCheckBoxImg" class="HideItems" alt="image cannot be displayed" clientidmode="Static"
                                src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                            <span class="ico-uncheck "></span>
                            <asp:Label ID="SessionId" runat="server" ClientIDMode="Static" Text='<%# Eval("SessionId") %>'
                                class="HideItems"></asp:Label>
                        </div>
                        <div id="GroupsRepeaterDiv">
                            <div class="contents-list">
                                <div class="contents-msg">
                                    <asp:Label ID="MembersLabel" CssClass="H4Heading" runat="server" Text='<%# (Eval("Name").ToString().Length>=38 ? Eval("Name").ToString().Substring(0,38) :  Eval("Name").ToString()) %>'></asp:Label>
                                    <span class="H6Heading PaddingL5">(<%# Eval("UnOpened")%>
                                        unopened)</span>
                                    <asp:Label ID="TeacherNameLabel" Visible="false" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </div>
                                <div>
                                    <span class="session-date">
                                        <%# Convert.ToDateTime(Eval("SessionCreatedDate")).ToString("dd/MM/yyyy")%></span>
                                </div>
                            </div>
                            <div class="repeater-btn-ctrls">
                                <a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=seconds">
                                    Edit</a>
                                <asp:Button ID="GroupsProfileButton" runat="server" Text="VIEW" CssClass="btn btn-affermative"
                                    OnClick="ClassProfileButton2_Click" CommandArgument='<%# Eval("SessionId") %>'
                                    UseSubmitBehavior="false" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="AllOtherGroupsDiv" class="AllOtherGroupsDiv" runat="server">
        <div id="AllotherGroupDivBtn" class="AllotherGroupDivBtn" runat="server">
            <%--  <div class="Session_WordCountBG" style="width: 10%; height: 50px;">
         <b class="History_WordCount" style="width: 40%; margin-left: 8px; margin-top: 18px;">
            <img id="Img2" style="width: 27px; margin: -5px 0 0 10px;" alt="image cannot be displayed"
                    clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_white.png")%>" />
                <asp:Label ID="lblArchivedSessionsCount" runat="server" CssClass="H6Heading" ></asp:Label></b>            
        </div>--%>
            <div class="counts">
                <div class="History_WordCountBG">
                    <img src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>' alt="" class="WordCountBG" />
                </div>
                <div class="History_WordCount">
                    <asp:Label ID="lblArchivedSessionsCount" runat="server"></asp:Label>
                </div>
            </div>
            <%--        <div style="width: 27px;height:28px; padding-left:43px;margin-left: -25px; margin-top: 8px;">
            <img id="Img3" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" style="width:27px;height:28px">
            <div style="margin-left:8px;margin-top:-25px">
            <asp:Label ID="lblArchivedSessionsCount" runat="server" CssClass="H5Heading"></asp:Label>
            </div>
        </div>--%>
            <div class="hide-show">
                <asp:Button ID="AllOtherGroupButton" ClientIDMode="Static" runat="server" Text="Archived sessions"
                    OnClientClick="return false;" CssClass="FinishedSessionsButton" />
                <span class="student-line"></span><span class="btn btn-general">Hide</span>
            </div>
        </div>
    </div>
    <div id="ArchivedSessionOuterDiv" class="ArchivedSessionOuterDiv">
        <asp:Repeater ID="ArchivedSessionList" runat="server" ClientIDMode="Static">
            <ItemTemplate>
                <div class="Div_FullWidth">
                    <div id="GroupRepeaterContentDiv" class="gchkbx">
                        <input id="GroupCheckBoxes" clientidmode="Static" type="checkbox" class="HideItems"
                            runat="server" />
                        <img id="ClassCheckBoxImg" class="HideItems" alt="image cannot be displayed" clientidmode="Static"
                            src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                        <span class="ico-uncheck "></span>
                        <asp:Label ID="SessionId" runat="server" ClientIDMode="Static" Text='<%# Eval("SessionId") %>'
                            class="HideItems"></asp:Label>
                    </div>
                    <div id="GroupsRepeaterDiv">
                        <div class="contents-list">
                            <div class="contents-msg">
                                <asp:Label ID="MembersLabel" CssClass="H4Heading" runat="server" Text='<%# (Eval("Name").ToString().Length>=38 ? Eval("Name").ToString().Substring(0,38) :  Eval("Name").ToString()) %>'></asp:Label>
                                <span class="H6Heading PaddingL5">(<%# Eval("UnOpened")%>
                                    unopened)</span>
                                <asp:Label ID="TeacherNameLabel" Visible="false" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </div>
                            <div>
                                <span class="session-date">
                                    <%# Convert.ToDateTime(Eval("SessionCreatedDate")).ToString("dd/MM/yyyy")%></span>
                            </div>
                        </div>
                        <div class="repeater-btn-ctrls">
                            <a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=seconds">
                                Edit</a>
                            <asp:Button ID="GroupsProfileButton" runat="server" Text="VIEW" CssClass="btn btn-affermative"
                                OnClick="ClassProfileButton2_Click" CommandArgument='<%# Eval("SessionId") %>'
                                UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</div>
<%--<div id="AllOtherGroupsDiv1" class="AllOtherGroupsDiv" runat="server">
    <div class="AllOtherGroupsImgDiv">
        <img src="../Portals/0/images/circle_big.png" alt="" />
    </div>
</div>--%>
