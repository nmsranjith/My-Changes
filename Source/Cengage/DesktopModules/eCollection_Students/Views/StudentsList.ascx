<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentsList.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.StudentsList" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Messages" TagPrefix="Msg" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Students/Scripts/StudentsList.js"
    ForceProvider="DnnFormBottomProvider" />
<div class="HideItems">
    <Msg:Messages ID="Messages" runat="server">
    </Msg:Messages>
</div>
<span id="MovedMessageSpan" runat="server" visible="false" class="dnnFormSuccess dnnMsgScript movedmsg">
</span>
<div id="MessageOuterDiv" runat="server" class="ecstulistmsg">
    <div class="bubble1">
        <asp:Label ID="Message1" runat="server" Text="" />
        <asp:HyperLink ID="CreateLink" ClientIDMode="Static" NavigateUrl="" Text="Create the first one now?"
            CssClass="CreateLinkStyle" runat="server" />
    </div>
</div>
<div id="StudentListDiv" class="StudentsList" runat="server" clientidmode="Static">
    <div class="ProgressDivClass" style="display: none;" id="UpdateProgressImg">
        <div class="ProgressInnerDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                alt="Processing" />
        </div>
    </div>
    <h2>
        Students</h2>
    <div class="ecstulistsrch">
        <div class="eClUserSearchDiv ">
            <label id="eClSearchLabel" for="SearchTextBox">
                Search:</label>
            <asp:TextBox ID="SearchTextBox" class="eClSearchbox" runat="server" placeholder="student name"
                ClientIDMode="Static"></asp:TextBox>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClick="SearchButton_Click"
                        OnClientClick="ShowUpdate()" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="StudentsListPanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="StudentList_TopDiv">
                <div class="StudentDashboard_CheckAllDiv">
                    <div id="CheckAllDiv" class="ecstulistcka" onclick="javascript:CheckAll();">
                        <span class="ico-uncheck "></span>
                    </div>
                    <div id="UnCheckAllDiv" class="ecstulistucka" onclick="javascript:UnCheckAll();">
                        <span class="ico-check"></span>
                    </div>
                    <div onclick="javascript:ClickSelectAll();" class="ecstulistsadiv">
                        Select All</div>
                </div>                
                <div class="categorybysort">
                    <label for="" class="">
                        Sort Results:</label>
						
					<asp:UpdatePanel ID="SortUpdatePanel" runat="server">
					<ContentTemplate>
					<asp:DropDownList ID="studentssortdpn" runat="server" ClientIDMode="Static"  AutoPostBack="true" onselectedindexchanged="studentssortdpn_SelectedIndexChanged">
							<asp:ListItem Text="Reading Level" Value="1"></asp:ListItem>
							<asp:ListItem Text="Name (A to Z)" Value="2"></asp:ListItem>
							<asp:ListItem Text="Name (Z to A)" Value="3"></asp:ListItem>
						</asp:DropDownList>
					</ContentTemplate>
					<Triggers>
					<asp:AsyncPostbackTrigger ControlID="studentssortdpn" EventName="SelectedIndexChanged" />
					</Triggers>
					</asp:UpdatePanel>     
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="ecstulistfirstdiv">
                <div id="classlist" class="Div_FullWidth">
                    <asp:ListView ID="ClassListView" runat="server" DataKeyNames="GroupId" OnItemDataBound="ClassMembers_DataBound">
                        <ItemTemplate>
                            <div class="ecstulistclmain">
                                <div class="ClassSearchDiv">
                                    <span class="student-class">
                                        <%# Eval("Name") %></span> <span class="student-line"></span>
                                    <div class="KenduoComboBoxClassDiv">
                                        <span class="btn btn-general" onclick="ShowClassMembers(<%# Eval("GroupId") %>,<%# Container.DataItemIndex %>,this)">
                                            Hide</span>
                                        <div id='ClassListDiv<%# Eval("GroupId") %>' class="ClassnameSpan ClassNameSpanDown">
                                        </div>
                                    </div>
                                </div>
                                <span class="HideItems">
                                    <%# Eval("GroupId") %></span> <span class="HideItems">
                                        <%# Eval("StudentsCount") %></span>
                                <div id='Class<%# Eval("GroupId") %>' class="ShowItems ClassMembersList">
                                    <asp:UpdatePanel ID="StudentsPanel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Repeater ID="StudentsList" runat="server">
                                                <ItemTemplate>
                                                    <div class="ecstulistRPst">
                                                        <div class="ecstulistRPdiv3">
                                                            <div class="ecstulistRPdiv4">
                                                                <input type="checkbox" id="StudentsCheckBoxes" runat="server" clientidmode="Static"
                                                                    style="display: none;" value='<%# Eval("StudentId")%>' />
                                                                <span id="CustSubUserSk" class="srtbtnshide">
                                                                    <%# Eval("CustSubUserSk")%></span> <span id="SubsSKLabel" class="srtbtnshide">
                                                                        <%# Eval("SubscriptionId")%></span>
                                                                <div id='CheckDiv<%# Eval("ClassId")%><%# Eval("StudentId")%>' onclick="Checked(<%# Eval("ClassId")%><%# Eval("StudentId")%>)"
                                                                    class="checkdiv">
                                                                    <span class="ico-uncheck"></span>
                                                                </div>
                                                                <div id="UnCheckDiv<%# Eval("ClassId")%><%# Eval("StudentId")%>" onclick="UnChecked(<%# Eval("ClassId")%><%# Eval("StudentId")%>)"
                                                                    class="checkdiv srtbtnshide">
                                                                    <span class="ico-check"></span>
                                                                </div>
                                                            </div>
                                                            <div class="List_Contents">
                                                                <div class="ecstulistRPNamediv1" title="<%# Eval("FirstName")%> <%# Eval("LastName")%> (<%# Eval("UserLoginName")%>)">
                                                                    <span id="StudentNameLabel" class="StudentDashBoard_StudentNames">
                                                                        <%# Eval("FullName")%></span> <span id="CurrRLLabel" class="srtbtnshide">
                                                                            <%# Eval("CurrentReadingLevel")%></span> <span id="UserNameLabel" class="srtbtnshide">
                                                                                <%# Eval("UserLoginName")%></span>
                                                                </div>
                                                                <div class="ecstulistRPprofilebtn">
                                                                    <a class="btn btn-general" href='<%# Eval("editlink")%>'>Edit</a> <a class="btn btn-affermative"
                                                                        href='<%# Eval("profilelink")%>'>View Profile</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="PagerHdr">
                                    <div id='Pager<%# Eval("GroupId") %>' class="StuListPager k-pager-wrap" style="display: none;">
                                    </div>
                                </div>
                                <span id='Load<%# Eval("GroupId") %>' class="HideItems">Empty</span>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div id="Delete-message" title="Confirm Delete" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Confirm Delete</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="MessageLiteral" style="font-size: 10pt; color: #707070; padding: 23px;
                float: left;">Are you sure you want to delete student/s?</span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
            <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
        </div>
    </div>
</div>
<asp:HiddenField ID="ClassId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ContainerId" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="StudentsCount" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="SelStudid" runat="server" ClientIDMode="Static" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Button ID="ClickedGroup" runat="server" ClientIDMode="Static" Style="display: none;"
            OnClick="ClickedGroup_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
<div id="searcheditems" class="srtbtnshide">
</div>
<div id="selecteditems" class="srtbtnshide">
</div>
