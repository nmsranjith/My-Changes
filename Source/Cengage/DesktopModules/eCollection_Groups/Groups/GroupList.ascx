<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupList.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.GroupList" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="EGMessages" TagPrefix="EG" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Groups/Scripts/GroupList.js"
    ForceProvider="DnnFormBottomProvider" />
<div class="GroupsDivset">
    <asp:HiddenField ID="PageName" runat="server" ClientIDMode="Static" />
    <EG:EGMessages ID="Messages" runat="server">
    </EG:EGMessages>
    <div id="dialog-message" title="Alert message!" class="eg-dlg-msg" >
        <div class="deletegrphdrpp">
            <span class="AfterRenewelHeaderSpan eg-headspan" >Alert Message!</span>
        </div>
        <div class="eg-dlg-child" >
            <div class="eg-editdiv" >
                <literal class="eg-literal">Please select only one group to edit</literal>
            </div>
            <div class="eg-width">
                <input type="button" id="PopuOkButton" value="Ok" class="popupokbtn" />
            </div>
        </div>
    </div>
    <div id="MessageOuterDiv" runat="server" class="eg-out-msg" >
        <div class="bubble1">
            <asp:Label ID="Message1" runat="server" Text="" />
            <asp:HyperLink ID="CreateLink" ClientIDMode="Static" NavigateUrl="" Text="Create the first one now?"
                CssClass="CreateLinkStyle" runat="server" />
        </div>
    </div>
    <div id="GroupsMainDiv" runat="server">
        <h2>
            Groups</h2>
        <div class="ProgressDivClass" id="UpdateProgressImg" style="display:none;">
            <div class="ProgressInnerDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                    alt="Processing" />
            </div>
        </div>
        <asp:UpdatePanel ID="AllUpdates" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <%--  <asp:UpdatePanel ID="CheckALLUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
                <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
                <div class="SearchDiv">
                    <div class="SearchInnerDiv">
                        <%-- <asp:UpdatePanel ID="SearchUpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                        <label for="SearchTextBox">
                            Search</label>
                        <input type="text" id="SearchTextBox" class="classSearchwater" clientidmode="Static"
                            autocomplete="off" spellcheck="false" runat="server" title="group name" />
                        <%-- <label id="eClSearchLabel" for="SearchTextBox">group name</label>--%>
                        <div class="Searchbtndiv" id="SrchDiv">
                            <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" CssClass="SearchButton"
                                OnClick="SearchButton_Click" OnClientClick="ShowUpdate()" />
                        </div>
                        <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
                    </div>
                </div>
                <div class="GroupTopDiv">
                    <div class="GroupSelectAll">
                        <div id="SelectAllDiv " class="eg-lfloat" >
                            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
                                id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal></div>
                        <span id="SelectAllChkbx" class="ico-uncheck "></span><span class="SelectAllSpan" onclick="javascript:document.getElementById('SelectallCheckBox').click()">
                            Select All</span>
                    </div>
                    <div class="GroupsSortReaddiv eg-grp-srt" >
                        <span class="eg-readlvl-span"  onclick="javascript:document.getElementById('ReadingLevelButton').click();">
                            PM READING LEVEL</span>
                        <asp:Button ID="ReadingLevelButton" ClientIDMode="Static" CommandName="Ascending"
                            UseSubmitBehavior="false" OnClientClick="javascript:FillSelectedValues();"  runat="server" CssClass="SortRead Reading eg-sort-btn"
                            OnClick="ReadingLevelButton_Click" />
                        <b id="Boldline" class="eg-b" >|</b>
                        <span class="eg-asc"  onclick="javascript:document.getElementById('SortingButton').click();">
                            A – Z</span>
                        <asp:Button ID="SortingButton" ClientIDMode="Static" CssClass="SortRead Sort" runat="server"
                            CommandName="Ascending" OnClientClick="javascript:FillSelectedValues();" UseSubmitBehavior="false"
                            OnClick="SortingButton_Click" />
                    </div>
                    <div class="categorybysort">
                        <label for="" class="">
                            Sort Results:</label>
                        <select name="" id="groupssortdpn">
                            <option value="">Recommended</option>
                            <option value="">Title (A to Z)</option>
                            <option value="">Title (Z to A)</option>
                            <option value="">Latest</option>
                        </select>
                    </div>
                </div>
                <div class="LeftSecondLineDiv">
                </div>
                <div class="ClassSearchDiv" id="ClassContainer" runat="server" clientidmode="Static">
                    <span class="student-class">Your class</span> <span class="student-line"></span>
                    <div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
                        <asp:Button ID="ClassButton" ClientIDMode="Static" CommandName="Expand" runat="server"
                            Text="Hide" CssClass="btn btn-general" />
                    </div>
                </div>
                <div id="LeftLineDivBorder" class="LeftLineDiv eg-hide">
                </div>
                <div class="GroupContentDiv">
                    <div id="RepeaterClassDiv" class="RepeaterDiv eg-show" >
                        <asp:Repeater ID="ClassRepeater" runat="server" OnItemDataBound="ClassRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id="ClassRepeaterDiv" class="RepeaterContentDiv">
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="ClassCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            checked='<%# Eval("checked") %>' class="eg-hide" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="eg-hide"></asp:Label>
                                        <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                        <%--<span>&#10003; &#10004;</span>--%>
                                        <span class="ico-uncheck"></span>
                                    </div>
                                    <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="MembersLabel" ClientIDMode="Static" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ClientIDMode="Static" ID="ClassNameSpan" Text=' <%# Eval("Name")%>'
                                                CssClass="RepeaterSndColSpan">
											</asp:Label></div>
                                            <div><asp:Label ID="TeacherNameLabel" ClientIDMode="Static" CssClass="RepeaterThrdCollbl"
                                                runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
										    <a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=chandrut">Edit</a>
                                            <asp:Button ID="ClassProfileButton" runat="server" UseSubmitBehavior="false" CssClass="btn btn-affermative"
                                                formtarget="_blank" Text="PAGE" OnClick="ClassProfileButton_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="ClassSearchDiv" id="GroupsContainer" runat="server" clientidmode="Static">
                    <span class="student-class">Your group/s</span> <span class="student-line"></span>
                    <div id="kenduoComboGrpDiv" class="KenduoComboBoxClassDiv">
                        <asp:Button ID="GroupButton" ClientIDMode="Static" CommandName="Expand" runat="server"
                            Text="Hide" CssClass="btn btn-general" />
                    </div>
                </div>
                <div id="GroupContentDivID" class="GroupContentDiv">
                    <div id="RepeaterGroupDiv" class="RepeaterDiv eg-show" >
                        <asp:Repeater ID="GroupsRepeater" runat="server" OnItemDataBound="GroupsRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id="GroupsRepeaterDiv" class="RepeaterContentDiv">
                                    <div class="RepeaterFstCol gchkbx">
									 <img id="groupImage1" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                        <input id="GroupCheckBoxes" clientidmode="Static" type="checkbox" class="eg-hide"
                                            checked='<%# Eval("checked") %>' runat="server" />
                                        <asp:Label ID="groupid" runat="server" Text='<%# Eval("GroupId") %>' CssClass="eg-hide"></asp:Label>                                       
                                        <span class="ico-uncheck "></span>
                                    </div>
                                    <div id="GroupRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="GroupMembersLabel" CssClass="RepeaterSndCollbl" runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ClientIDMode="Static" ID="GroupNameSpan" Text=' <%# Eval("Name")%>'
                                                CssClass="RepeaterSndColSpan"></asp:Label></div>
                                            
                                            <div><asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
											<a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=chandrut">Edit</a>
                                            <asp:Button ID="GroupsProfileButton" runat="server" UseSubmitBehavior="false" CssClass="btn btn-affermative"
                                                Text="PAGE" OnClick="GroupsProfileButton_Click" formtarget="_blank" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div id="allotherLeftLineDiv" 
                    class="LeftLineDiv eg-leftlinediv">
                </div>
                <div class="ClassSearchDiv" id="AllOtherGroupBtnDiv" runat="server" clientidmode="Static">
                    <span class="student-class">Other groups</span> <span class="student-line"></span>
                    <div id="Div2" class="KenduoComboBoxClassDiv">
                        <asp:Button ID="AllOtherGroupButton" CommandName="Collpase" ClientIDMode="Static"
                            runat="server" Text="Hide" CssClass="btn btn-general" />
                    </div>
                </div>
                <div id="AllOtherGroupBtnDivID" class="GroupContentDiv">
                    <div id="AllotherGroupContent" class="RepeaterDiv eg-show">
                        <asp:Repeater ID="AllotherGroupRepeater" runat="server" OnItemDataBound="AllotherGroupRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id="AllotherGroupRepeaterDiv" class="RepeaterContentDiv">
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="AllotherGroupCheckBoxes" clientidmode="Static" type="checkbox" class="eg-hide"
                                            checked='<%# Eval("checked") %>' runat="server" />
                                        <asp:Label ID="groupid" runat="server" Text='<%# Eval("GroupId") %>' class="eg-hide"></asp:Label>
                                        <img id="Img2" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                        <span class="ico-uncheck "></span>
                                    </div>
                                    <div id="AllotherGroupRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="AllotherGroupMembersLabel" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ClientIDMode="Static" ID="AllotherGroupNameSpan" Text=' <%# Eval("Name")%>'
                                                CssClass="RepeaterSndColSpan"></asp:Label></div>
                                            <div> <asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
											<a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=chandrut">Edit</a>
                                            <asp:Button ID="AllotherGroupProfileButton" runat="server" UseSubmitBehavior="false"
                                                formtarget="_blank" CssClass="btn btn-affermative" Text="PAGE" OnClick="AllotherGroupProfileButton_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div id="Div1"  class="LeftLineDiv eg-leftlinediv">
                </div>
                <div class="ClassSearchDiv" id="AllOtherClassBtnDiv" runat="server" clientidmode="Static">
                    <span class="student-class">Other classes</span> <span class="student-line"></span>
                    <div id="Div7" class="KenduoComboBoxClassDiv">
                        <asp:Button ID="AllOtherClassButton" CommandName="Collpase" ClientIDMode="Static"
                            runat="server" Text="Hide" CssClass="btn btn-general" />
                    </div>
                </div>
                <div id="AllOtherClassBtnDivID" class="GroupContentDiv">
                    <div id="AllotherClassContent" class="RepeaterDiv eg-show" >
                        <asp:Repeater ID="AllotherClassRepeater" runat="server" OnItemDataBound="AllotherClassRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id="AllotherClassRepeaterDiv" class="RepeaterContentDiv">
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="AllotherClassCheckBoxes" clientidmode="Static" type="checkbox" class="eg-hide"
                                            checked='<%# Eval("checked") %>' runat="server" />
                                        <asp:Label ID="OtherClassId" runat="server" Text='<%# Eval("GroupId") %>' class="eg-hide"></asp:Label>
                                        <img id="Img1" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                        <span class="ico-uncheck "></span>
                                    </div>
                                    <div id="AllotherClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="AllotherClassMembersLabel" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ClientIDMode="Static" ID="AllotherClassNameSpan" Text=' <%# Eval("Name")%>'
                                                CssClass="RepeaterSndColSpan"></asp:Label></div>
                                            <div><asp:Label ID="OCTeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                       <div class="Repeater3rdCol">
											<a class="btn btn-general" href="http://s3.cengagelearning.com.au/ecollection/students?bk=list&amp;pagename=editprofile&amp;username=chandrut">Edit</a>
                                            <asp:Button ID="AllotherClassProfileButton" runat="server" UseSubmitBehavior="false"
                                                formtarget="_blank" CssClass="btn btn-affermative" Text="PAGE" OnClick="AllotherClassProfileButton_Click" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:HiddenField ID="AllOtherVisible" ClientIDMode="Static" runat="server" />
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <%-- </div>--%>
    <asp:HiddenField ID="AllOtherGroupToggFlag" ClientIDMode="Static" runat="server"
        Value="true" />
    <asp:HiddenField ID="AllOtherClassToggFlag" ClientIDMode="Static" runat="server"
        Value="true" />
    <asp:HiddenField ID="selectedClsID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="selectedGrpID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="selectedAllGrpID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="selectedAllClsID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="SelectedGrps" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="SelectedClassGrps" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="SelectedAllOtherGrps" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="SelectedAllOtherCls" ClientIDMode="Static" runat="server" Value="" />
    <%--</div>--%>
</div>
