<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTeachersToSession.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.AddTeachersToSession" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Sessions/Scripts/AddTeacher.js"
    ForceProvider="DnnFormBottomProvider" />
	
<div id="StudentListDiv" class="StudentDashboard">
    <div style="width: 93.4%; margin: 10px 0px 20px 21px; height: auto; border: 1px solid lightGrey;
        float: left;">
        <div style="float: left; width: 3%; margin: 25px 0px 10px 6px;">
            <span style="float: left; position: absolute; width: 0px; padding-left: 13px;" class="Session">
                TO:</span>
        </div>
        <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: 25px 0px 10px 1px;
            height: auto; overflow: hidden">
        </div>
        <div class="ActiveAddButtonsHolder BackBtn">
            <asp:Button ID="BackToSession" CssClass="BackToSession" Enabled="true" runat="server"
                Text="BACK" OnClick="BackToSession_Click" />
        </div>
        <div class="GrayLineSession">
        </div>
        <div class="GroupAddtoinnersnddiv">
            <ul id="SelectedStudentList" runat="server" clientidmode="Static">
            </ul>
            <div id="dialog" title="Alert Message" style="display: none;">
                <p>
                    <span id="StudentNamespan"></span>is Already added</p>
            </div>
            <asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueGroupTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
            <asp:TextBox ID="SelectedValueTeacherTextBox" runat="server" ClientIDMode="Static"
                Style="display: none;"></asp:TextBox>
        </div>
    </div>
	<div class="GroupTopDiv">
<div class="GroupSelectAll">	
	<span id="SelectAllChkbx" class="ico-uncheck "></span><span class="SelectAllSpan" onclick="javascript:document.getElementById('SelectallCheckBox').click()">
		Select All</span>
</div>
<div class="categorybysort">
	<label for="groupssortdpn" class="">
		Sort Results:</label>
	<select  id="groupssortdpn">
		<option value="">Recommended</option>
		<option value="">Title (A to Z)</option>
		<option value="">Title (Z to A)</option>
		<option value="">Latest</option>
	</select>
</div>
</div>
    <div class="HideItems" style="margin-top: -7px;">
        <div class="TeachersList_CheckAllDiv" style="margin-left: 0px;">
            <div onclick="javascript:CheckAll(this);" style="float: left; height: 20px; width: 9%;">
                <img alt="" id="SelectAll" style="height: 21px; width: 20px; cursor: pointer;" src="../Portals/0/images/circle_big.png" /></div>
            <div onclick="javascript:CheckAll(this);" style="cursor: pointer; width: 70%; float: left;
                margin: 5px 0px 0px 15px; font-size: 10pt;">
                SELECT ALL</div>
        </div>
        <div class="HideItems" style="width: 60%;">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Button ID="NamesAscButton" Text="A - Z" runat="server" OnClick="NamesAscButton_Click"
                        CssClass="TeachersList_NamesAscSortingButtons" Style="margin-top: 5px; font-size: 9pt;
                        color: #707070; text-indent: 37px; width: 100px; font-weight: normal;"  />
                    <asp:Button ID="NamesDescButton" Text="A - Z" runat="server" OnClick="NamesDescButton_Click"
                        CssClass="TeachersList_NamesDescSortingButtons" Style="margin-top: 5px; font-size: 9pt;
                        color: #707070; text-indent: 37px; width: 100px; font-weight: normal;"
                        Visible="False" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
        <div class="ProgressInnerDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg" alt="Processing" /> 
        </div>
    </div>
    <div class="HideItems" style="width: 648px; border-style: solid; height: 34px; margin-left: 24px; border: 1px solid #CCCCCC;
        float: left; background-color: #EEEEEE; position: relative;">
        <asp:UpdatePanel ID="BookUpdatePanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="eClUserSearchDiv">
                    <input type="text" id="SearchTextBox" class="classSearchwater" spellcheck="false"
                        autocomplete="off" clientidmode="Static" runat="server" title="Enter your search here..." />
                </div>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClick="SearchButton_Click"
                        OnClientClick="ShowUpdate()" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="RepeaterLftConntrLine" class="RepeaterLftConntrLine"  >
                <div style="float: left; width: 100%; margin-top: 5px;">
                    <%-- <div id="Teachers">--%>
                    <asp:Repeater ID="TeacherList" runat="server">
                        <ItemTemplate>
                            <div id="ClassRepeaterDiv" class="HideItems">
                                <div id="ClassRepeaterContentDiv" class="TeachersList_Contents">
                                    <div style="width: 7%; float: left; padding-top: 12px;">
                                        <input type="checkbox" runat="server" style="display: none;" checked='<%# Eval("Checked") %>'
                                            id="CheckBoxes" />
                                        <img id="CheckAllImage" onclick="SelectTeacher(this,'image')" style="width: 20px; margin-top: 4px; margin-left: 19px; cursor: pointer;"
                                            src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" />
                                    </div>
                                    <div id="StudentIDDiv" style="width: 67%; float: left; padding-top: 15px; height: 30px;
                                        padding-left: 18px;">
                                        <asp:Label ID="TeacherId" runat="server" ClientIDMode="Static" Text='<%# Eval("Id") %>'
                                            Style="display: none"></asp:Label>
                                        <asp:Label ID="TeacherNameLabel1" runat="server" ClientIDMode="Static" Text='<%# Eval("Text") %>' CssClass="TeacherDashBoard_TeacherNames"></asp:Label>
                                    </div>
                                    <div id="ClassRepeaterSubmit" class="RepeaterFrthCol" style="width: 23%; margin-top: 8px !important;
                                        float: left;">
                                        <div class="greenBtn">
                                            <%--<asp:Button ID="AddTeacherButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton" />--%>
                                            <input type="button" id="AddTeacherButtonn<%# Container.ItemIndex %>" value="ADD"
                                                class="SessionAddButton"  onclick="SelectTeacher(this,'button')"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
							 <div class="teacher-lists">
                        <div class="checkdiv gschkbx">
                            <input id="TeacherCheckBoxes" clientidmode="Static" runat="server" type="checkbox"
                                style="display: none" value='<%# Eval("Id") %>' />
                            <asp:Label ID="studentid" runat="server" Text='<%# Eval("Id") %>' Style="display: none"></asp:Label>							
                            <span id="CustSubUserSk" class="srtbtnshide">                              
                            </span><span id="SubsSKLabel" class="srtbtnshide">                              
                            </span>
                            <div id='CheckDiv<%# Eval("Id")%>' class="checkdiv gchkbx">                               
                                <span class="ico-uncheck "></span>
                            </div>                           
                        </div>
                        <div class="List_Contents">
                            <asp:Label ID="TeacherNameLabel" CssClass="TeachersList_TeacherNames" runat="server"
                                Text='<%# Eval("Text") %>'></asp:Label> 
								<span class="gausername srtbtnshide"><%# Eval("Text")%></span>
							<div id='AddBtnDiv<%# Eval("Id")%>' class="gasaddbtn">                       
                        <input id="AddTeacherButton<%# Container.ItemIndex %>" type="button" value="ADD" class="btn btn-affermative" />
                    </div>                   
						</div>						
                    </div>  
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <CP:CustomPaging ID="CustomPaging" runat="server">
            </CP:CustomPaging>
        </ContentTemplate>
    </asp:UpdatePanel>    
</div>
<asp:HiddenField ID="SelectedTeachers" runat="server" ClientIDMode="Static" />

