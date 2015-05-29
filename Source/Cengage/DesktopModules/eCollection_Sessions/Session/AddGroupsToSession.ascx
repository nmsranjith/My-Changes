<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddGroupsToSession.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.AddGroupsToSession" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Sessions/Scripts/AddGroup.js"
    ForceProvider="DnnFormBottomProvider" />
<div id="MsgDiv">
    <Msg:message id="Messages" runat="server">
    </Msg:message>
</div>
<div class='GroupsDivtest'>
    <div style="float: left; width: 100%; margin-top: 13px;">
        <div style="width: 93.4%; margin-top: 10px; height: auto; border: 1px solid lightGrey; float: left; margin-left: 21px;">
            <div style="float: left; width: 3%; margin: 19px 0px 10px 6px;">
                <span style="float: left; position: absolute; width: 0px;padding-left: 13px;"
                    class="Session">TO:</span>
            </div>
            <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: 25px 0px 10px 1px;
                height: auto; overflow: hidden">
            </div>
            <%--<div style="float: left;width: 35%;height: auto; margin: 20px 0px 10px 10px;border: 0px solid lightGrey;">
<asp:Button ID="Button13" OnClientClick="removeFormField(this)" style="background-color:#91B76C;float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold; height:28px;padding-left:24px;font-size:10pt" CssClass="ButtonStyle" Enabled="true" runat="server" Text="Group Name x" />
<asp:Button ID="Button1" OnClientClick="removeFormField(this)" style="background-color:#91B76C;float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold; height:28px;margin-top:5px;padding-left:24px;font-size:10pt" CssClass="ButtonStyle" Enabled="true" runat="server" Text="Group Name x" />
</div> --%>
            <div class="ActiveAddButtonsHolder BackBtn">
                <asp:Button ID="BackToSession" CssClass="BackToSession" Enabled="true" runat="server"
                    OnClientClick="BackToCreate()" OnClick="BackToSession_Click" Text="BACK" />
            </div>
            <div class="GrayLineSession" ></div>
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
		<div class="SearchDiv">
        <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
            <div class="ProgressInnerDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg" alt="Processing" /> 
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
				<div class="SearchInnerDiv">   
                    <asp:TextBox ID="SearchTextBox" CssClass="classSearchwater" ClientIDMode="Static" runat="server"
                        placeholder="Enter your search here ..."></asp:TextBox>
                    <label for="SearchTextBox">
                        Search</label>
						 <div class="Searchbtndiv">
							<asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClick="GroupSearch_Click"  OnClientClick="ShowUpdate()" />
						</div>
				</div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>
       <div class="GroupTopDiv">
			<div class="GroupSelectAll">
				<div id="SelectAllDiv " class="eg-lfloat" >
					<img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
						id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal></div>
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
       
    </div>
    <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
        <div  class="bubble1" >
            <asp:Label ID="Message1" runat="server" Text="" />                        
        </div>
    </div> 
    <div id="SecondDiv" runat="server">
    
    <div class="ClassSearchDiv HideItems" id="ClassDivHdr">
        <div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDivGroups">
            <span style="float: left; margin: 15px; margin-left: 58px; font-size: 12pt; color: #707070; font-weight: bold;">Your class</span>
            <img alt="" id="ClassArrowimg" style="float: right; margin: 21px;" src="<%=Page.ResolveUrl("Portals/0/images/Arrow_down.png")%>" />
        </div>
    </div>
	<div class="ClassSearchDiv" id="ClassDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Your class</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="ClassButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="ess-gp-RepeaterContentDiv" id="ClsRepeaterContentDiv">
        <div id="RepeaterClassDiv" class="Div_FullWidth ShowItems">
            <asp:UpdatePanel ID="ClassPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="ClassRepeater1" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                            <div id="ClassRepeaterDiv" class="RepeaterContentDiv">
                                <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv" title='<%# Eval("NameToolTip")%>'
                                    style="margin-left: -31px !important; width: 96.2% !important;">
                                    <div class="RepeaterFstCol">
                                        <input id="ClassCheckBoxes" clientidmode="Static" type="checkbox" style="display: none"
                                            runat="server" value='<%# Eval("GroupId") %>' />
                                        <div id="chk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="cursor: pointer;">
                                            <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" /></div>
                                        <div id="unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="UnCheck('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="display: none; cursor: pointer;">
                                            <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" /></div>
                                    </div>
                                    <div class="RepeaterSndCol" style="text-align:center; margin-left:19px;">
                                        <asp:Label ID="MembersLabel" CssClass="RepeaterSndCollbl" runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label><br />
                                        <span class="RepeaterSndColSpan">Members</span>
                                    </div>
                                    <div class="RepeaterSndCol" style="margin-left:25px;">
                                        <asp:Label ID="GroupName" runat="server" class="RepeaterSndColSpan" Text='<%# Eval("Name")%>' /><br />
                                        <asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>
                                    </div>
                                    <div id="ClassRepeaterSubmit" class="RepeaterFrthCol">
                                        <%--<asp:Button ID="GroupsProfileButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton"
CommandArgument='<%# Eval("GroupId") %>' />--%>
                                        <input type="button" value="ADD" class="GpAddStudentRptbtn" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
					<asp:Repeater ID="ClassRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
								<div id="ClassRepeaterDiv" class="RepeaterContentDiv">
                                    <div class="ess-eg-RepeaterFstCol">
                                        <input id="ClassCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            value='<%# Eval("GroupId") %>' class="eg-hide" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="eg-hide"></asp:Label>                                      
                                         <div id="chk<%# Container.ItemIndex %><%# Eval("GroupId") %>" 
                                            style="cursor: pointer;" class="gchkbx">
                                           <span class="ico-uncheck"></span>
										</div>
                                    </div>
                                    <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="MembersLabel" ClientIDMode="Static" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ID="GroupName" Text=' <%# Eval("Name")%>'
                                                CssClass="gausername RepeaterSndColSpan">
											</asp:Label></div>
                                            <div><asp:Label ID="TeacherNameLabel" ClientIDMode="Static" CssClass="RepeaterThrdCollbl"
                                                runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
										    <input type="button" value="ADD" class="btn btn-affermative" 
											onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                        </div>
                                    </div>
                                </div>
					 </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="ClassSearchDiv" style="margin-top: -55px;display:none;" id="GroupsDivHdr">
        <div id="kenduoComboGrpDiv" class="KenduoComboBoxClassDivGroups">
            <span style="float: left; margin: 15px; margin-left: 58px; font-size: 12pt; color: #707070; font-weight: bold;">Your group/s</span>
            <img alt="" id="GroupArrowimg" style="float: right; margin: 21px;" src="<%=Page.ResolveUrl("Portals/0/images/Arrow_down.png")%>" />
        </div>
    </div>
	<div class="ClassSearchDiv" id="GroupsDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Your group/s</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="GroupButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="ess-gp-RepeaterContentDiv" id="GrpRepeaterContentdiv">
        <div id="RepeaterGroupDiv" style="float: left; width: 100%; margin-top: 15px; display: block">
            <asp:UpdatePanel ID="GroupPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="GroupsRepeater1" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                            <div id="GroupsRepeaterDiv" class="RepeaterContentDiv">
                                <div id="GroupRepeaterContentDiv" class="RepeaterContentScndDiv" title='<%# Eval("NameToolTip")%>'
                                    style="margin-left: -30px !important; width: 96.3% !important;">
                                    <div class="RepeaterFstCol">
                                        <input id="GroupCheckBoxes" clientidmode="Static" type="checkbox" style="display: none"
                                            runat="server" value='<%# Eval("GroupId") %>' />
                                        <div id="chckg<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="Check('chckg<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchckg<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="cursor: pointer;">
                                            <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" /></div>
                                        <div id="unchckg<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="UnCheck('chckg<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchckg<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="display: none; cursor: pointer;">
                                            <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" /></div>
                                    </div>
                                    <div class="RepeaterSndCol" style="text-align:center; margin-left:19px;">
                                        <asp:Label ID="MembersLabel" CssClass="RepeaterSndCollbl" runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label><br />
                                        <span class="RepeaterSndColSpan">Members</span>
                                    </div>
                                    <div class="RepeaterSndCol" style="margin-left:25px;">
                                        <asp:Label ID="GroupName" runat="server" class="RepeaterSndColSpan" Text='<%# Eval("Name")%>' /><br />
                                        <asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>
                                    </div>
                                    <div id="ClassRepeaterSubmit" class="RepeaterFrthCol">
                                        <%--<asp:Button ID="GroupsProfileButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton"
CommandArgument='<%# Eval("GroupId") %>' />--%>
                                        <input type="button" value="ADD" class="GpAddStudentRptbtn" onclick="Check('chckg<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchckg<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
					<asp:Repeater ID="GroupsRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
								<div class="RepeaterContentDiv">
                                    <div class="ess-eg-RepeaterFstCol gchkbx">
                                        <input id="GroupCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            value='<%# Eval("GroupId") %>' class="eg-hide" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="eg-hide"></asp:Label>                                      
                                         <div id="chk<%# Container.ItemIndex %><%# Eval("GroupId") %>" class="gchkbx"
                                            style="cursor: pointer;">
                                           <span class="ico-uncheck"></span>
										</div>
                                    </div>
                                    <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="MembersLabel" ClientIDMode="Static" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ID="GroupName" Text=' <%# Eval("Name")%>'
                                                CssClass="gausername RepeaterSndColSpan">
											</asp:Label></div>
                                            <div><asp:Label ID="TeacherNameLabel" ClientIDMode="Static" CssClass="RepeaterThrdCollbl"
                                                runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
										    <input type="button" value="ADD" class="btn btn-affermative" 
											onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                        </div>
                                    </div>
                                </div>
					 </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="ClassSearchDiv" style="margin-top: -51px;display:none;" id="OtherGroupsDivHdr">
        <div id="OtherGroupsDiv" class="KenduoComboBoxClassDivGroups">
            <span style="float: left; margin: 15px; margin-left: 58px; font-size: 12pt; color: #707070; font-weight: bold;">Other groups</span>
            <img alt="" id="OtherGroupArrowimg" style="float: right; margin: 21px;" src="<%=Page.ResolveUrl("Portals/0/images/Arrow_down.png")%>" />
        </div>
    </div>
	<div class="ClassSearchDiv" id="OtherGroupsDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Other group/s</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="OtherGroupButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="ess-gp-RepeaterContentDiv" id="OthrGrpRepeaterContentdiv">
        <div id="RepeaterOtherGroupDiv" style="float: left; width: 100%; margin-top: 15px;
            display: block">
            <asp:UpdatePanel ID="OtherGrpPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="OtherGrpRepeater1" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                            <div id="OtherGrpRepeaterDiv" class="RepeaterContentDiv">
                                <div id="OtherGrpRepeaterContentDiv" class="RepeaterContentScndDiv" title='<%# Eval("NameToolTip")%>'
                                    style="margin-left: -31px !important; width: 96.2% !important;">
                                    <div class="RepeaterFstCol">
                                        <input id="OtherClassCheckBoxes" clientidmode="Static" type="checkbox" style="display: none"
                                            runat="server" value='<%# Eval("GroupId") %>' />
                                        <div id="ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="Check('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="cursor: pointer;">
                                            <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" /></div>
                                        <div id="ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="UnCheck('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="display: none; cursor: pointer;">
                                            <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" /></div>
                                    </div>
                                    <div class="RepeaterSndCol" style="text-align:center; margin-left:19px;">
                                        <asp:Label ID="MembersLabel" CssClass="RepeaterSndCollbl" runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label><br />
                                        <span class="RepeaterSndColSpan">Members</span>
                                    </div>
                                    <div class="RepeaterSndCol" style="margin-left:25px;">
                                        <asp:Label ID="GroupName" runat="server" class="RepeaterSndColSpan" Text='<%# Eval("Name")%>' /><br />
                                        <asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>
                                    </div>
                                    <div id="ClassRepeaterSubmit" class="RepeaterFrthCol">
                                        <%--<asp:Button ID="GroupsProfileButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton"
CommandArgument='<%# Eval("GroupId") %>' />--%>
                                        <input type="button" value="ADD" class="GpAddStudentRptbtn" onclick="Check('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
					<asp:Repeater ID="OtherGrpRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
								<div class="RepeaterContentDiv">
                                    <div class="ess-eg-RepeaterFstCol gchkbx">
                                        <input id="OtherClassCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            value='<%# Eval("GroupId") %>' class="eg-hide" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="eg-hide"></asp:Label>                                      
                                         <div id="chk<%# Container.ItemIndex %><%# Eval("GroupId") %>" class="gchkbx"
                                            style="cursor: pointer;">
                                           <span class="ico-uncheck"></span>
										</div>										
                                    </div>
                                    <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="MembersLabel" ClientIDMode="Static" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ID="GroupName" Text=' <%# Eval("Name")%>'
                                                CssClass="gausername RepeaterSndColSpan">
											</asp:Label></div>
                                            <div><asp:Label ID="TeacherNameLabel" ClientIDMode="Static" CssClass="RepeaterThrdCollbl"
                                                runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
										    <input type="button" value="ADD" class="btn btn-affermative" 
											onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                        </div>
                                    </div>
                                </div>
					 </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     <div class="ClassSearchDiv HideItems" id="OtherClassDivHdr">
        <div id="OtherClassesDiv" class="KenduoComboBoxClassDivGroups">
            <span style="float: left; margin: 15px; margin-left: 58px; font-size: 12pt; color: #707070; font-weight: bold;">Other classes</span>
            <img alt="" id="OtherClassArrowimg" style="float: right; margin: 21px;" src="<%=Page.ResolveUrl("Portals/0/images/Arrow_down.png")%>" />
        </div>
    </div>
	<div class="ClassSearchDiv" id="OtherClassDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Other class</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="OtherClassButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="ess-gp-RepeaterContentDiv">
        <div id="RepeaterOtherClassDiv" style="float: left; width: 100%; margin-top: 15px;
            display: block">
            <asp:UpdatePanel ID="OtherClsPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="OtherClsRepeater1" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                            <div id="OtherClsRepeaterDiv" class="RepeaterContentDiv">
                                <div id="OtherClsRepeaterContentDiv" class="RepeaterContentScndDiv" title='<%# Eval("NameToolTip")%>'
                                    style="margin-left: -31px !important; width: 96.2% !important;">
                                    <div class="RepeaterFstCol">
                                        <input id="OtherClassCheckBoxes" clientidmode="Static" type="checkbox" style="display: none"
                                            runat="server" value='<%# Eval("GroupId") %>' />
                                        <div id="ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="Check('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="cursor: pointer;">
                                            <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" /></div>
                                        <div id="ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>" onclick="UnCheck('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')"
                                            style="display: none; cursor: pointer;">
                                            <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" /></div>
                                    </div>
                                    <div class="RepeaterSndCol" style="text-align:center; margin-left:19px;">
                                        <asp:Label ID="MembersLabel" CssClass="RepeaterSndCollbl" runat="server" Text='<%# Eval("MemberCount") %>'></asp:Label><br />
                                        <span class="RepeaterSndColSpan">Members</span>
                                    </div>
                                    <div class="RepeaterSndCol" style="margin-left:25px;">
                                        <asp:Label ID="GroupName" runat="server" class="RepeaterSndColSpan" Text='<%# Eval("Name")%>' /><br />
                                        <asp:Label ID="TeacherNameLabel" CssClass="RepeaterThrdCollbl" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>
                                    </div>
                                    <div id="ClassRepeaterSubmit" class="RepeaterFrthCol">
                                        <%--<asp:Button ID="GroupsProfileButton" runat="server" Text="ADD" class="ProfileButton SessionAddButton"
CommandArgument='<%# Eval("GroupId") %>' />--%>
                                        <input type="button" value="ADD" class="GpAddStudentRptbtn" onclick="Check('ochk<%# Container.ItemIndex %><%# Eval("GroupId") %>','ounchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
					<asp:Repeater ID="OtherClsRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
								<div class="RepeaterContentDiv">
                                    <div class="ess-eg-RepeaterFstCol">
                                        <input id="OtherClassCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                            value='<%# Eval("GroupId") %>' class="eg-hide" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="eg-hide"></asp:Label>                                      
                                         <div id="chk<%# Container.ItemIndex %><%# Eval("GroupId") %>" class="gchkbx"
                                            style="cursor: pointer;">
                                           <span class="ico-uncheck"></span>
										</div>
                                    </div>
                                    <div id="ClassRepeaterContentDiv" class="RepeaterContentScndDiv">
                                        <div class="Repeater1stCol">
                                            <div><asp:Label ID="MembersLabel" ClientIDMode="Static" CssClass="RepeaterSndCollbl" runat="server"
                                                Text='<%# Eval("MemberCount") %>'></asp:Label></div>
                                            <div><span class="RepeaterSndColSpan">Members</span></div>
                                        </div>
                                        <div class="RepeaterSndCol">
                                            <div><asp:Label runat="server" ID="GroupName" Text=' <%# Eval("Name")%>'
                                                CssClass="gausername RepeaterSndColSpan">
											</asp:Label></div>
                                            <div><asp:Label ID="TeacherNameLabel" ClientIDMode="Static" CssClass="RepeaterThrdCollbl"
                                                runat="server" Text='<%# Eval("TeacherName") %>'></asp:Label></div>
                                        </div>
                                        <div class="Repeater3rdCol">
										    <input type="button" value="ADD" class="btn btn-affermative" 
											onclick="Check('chk<%# Container.ItemIndex %><%# Eval("GroupId") %>','unchk<%# Container.ItemIndex %><%# Eval("GroupId") %>')" />
                                        </div>
                                    </div>
                                </div>
					 </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
    <asp:HiddenField ID="SelectedGrpIds" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="SelectedStudIds" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="searchhdn" runat="server" ClientIDMode="Static" />
    <input type="hidden" id="lastsearch" runat="server" clientidmode="Static" />
    <input type="hidden" id="newsearch" runat="server" clientidmode="Static" />
</div>

