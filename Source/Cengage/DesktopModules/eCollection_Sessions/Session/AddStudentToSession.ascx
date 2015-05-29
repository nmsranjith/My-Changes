<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddStudentToSession.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Sessions.Session.AddStudentToSession" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Sessions/Scripts/AddStudent.js"
    ForceProvider="DnnFormBottomProvider" />
<div id="StudentDashboard" style="margin-top: 10px; float: left; width: 100%;">
    <div style="width: 93.4%; margin-top: 10px; height: auto; border: 1px solid lightGrey;
        float: left; margin-left: 21px;">
        <div style="float: left; width: 3%; margin: 25px 0px 10px 6px;">
            <span style="float: left; position: absolute; width: 0px; padding-left: 13px;" class="Session">
                TO:</span>
        </div>
        <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: 25px 0px 10px 1px;
            height: auto; overflow: hidden">
        </div>
        <%--<div style="float: left;width: 35%;height: auto; margin: 20px 0px 10px 10px;border: 0px solid lightGrey;">
    <asp:Button ID="Button13" OnClientClick="removeFormField(this)" style="background-color:#F69E47;float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold; height:28px;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"   runat="server" Text="Student Name x" />
    <asp:Button ID="Button1" OnClientClick="removeFormField(this)" style="background-color:#F69E47;float: left;width:128px;background-image:none;border-radius:0px;font-weight: bold; height:28px;margin-top:5px;padding-left:14px;font-size:10pt" CssClass="ButtonStyle" Enabled="true"   runat="server" Text="Student Name x" />
</div> --%>
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
<div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;">
    <div class="bubble1" >
        <asp:Label ID="Message1" runat="server" Text="" />            
    </div>
</div> 
    <div id="StudentDiv" runat="server" style="display: block; float: left; width: 100%;">
        <asp:UpdatePanel ID="CheckALLUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="GroupTopDiv HideItems">
                    <div class="GroupSelectAll">
                        <div id="SelectAllDiv" style="float: left">
                            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass" onclick="javascript:CheckAll(this);"
                                id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal></div>
                        <span class="SelectAllSpan" onclick="javascript:document.getElementById('SelectallCheckBox').click()" style="color: gray;
                            cursor: pointer;">SELECT ALL</span>
                    </div>
                    <div class="GroupsSortReaddiv" style="width: auto; margin-top: 0px;">
                        <span style="float: left; font-size: 10pt; color: #707070; cursor: pointer;" onclick="javascript:document.getElementById('ReadingLevelButton').click();">
                            PM READING LEVEL</span>
                        <asp:Button ID="ReadingLevelButton" ClientIDMode="Static" CommandName="Ascending"
                            Style="width: 1px  !important; background-position: 99% 55%; float: left; margin-top: -1px;
                            margin-right: 8px;" runat="server" CssClass="SortRead Reading" OnClick="ReadingLevelButton_Click" />
                        <b id="borderLine" style="float: left; margin-top: 0px;">|</b> <span style="float: left;
                            font-size: 10pt; margin-left: 10px; cursor: pointer;" onclick="javascript:document.getElementById('SortingButton').click();">
                            A-Z</span>
                        <asp:Button ID="SortingButton" ClientIDMode="Static" CssClass="SortRead Sort" runat="server"
                            CommandName="Ascending" Style="width: 1px !important; margin-right: 23px !important;
                            margin-left: 4px;" OnClick="SortingButton_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
            <div class="ProgressInnerDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg" alt="Processing" /> 
            </div>
        </div>
        <div class="SearchDiv HideItems" style="width: 655px; position: relative;">
            <div class="SearchInnerDiv" style="width: 655px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <input type="text" id="SearchTextBox1" class="classSearchwater" spellcheck="false"
                            autocomplete="off" clientidmode="Static" runat="server"  />
                        <div class="Searchbtndiv">
                            <asp:Button ID="SearchButton1" runat="server"  ClientIDMode="Static"
                                CssClass="SearchButton" OnClick="SearchButton_Click" OnClientClick="ShowUpdate()" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>   

<div class="ess-SearchDiv eg-at-searchdiv">
	<div class="SearchInnerDiv">                       
		<label for="SearchTextBox">
			Search</label>
		<input type="text" id="SearchTextBox" class="classSearchwater" clientidmode="Static"
			autocomplete="off" spellcheck="false" runat="server" title="group name" />
	  
		<div class="Searchbtndiv" id="SrchDiv">
			<asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" CssClass="SearchButton"
				OnClick="SearchButton_Click" OnClientClick="ShowUpdate()" />
		</div>                       
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
		
        <asp:UpdatePanel ID="StudentRepeaterUpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>                
                <div class="Addthrtedigrp" id="test">
                    <div id="RepeaterStudentDiv" class="GpRptitmcontentdiv">
                        <asp:Repeater ID="StudentRepeater1" runat="server" OnItemDataBound="StudentRepeater_ItemDataBound">
                            <ItemTemplate>
                                <div id="StudentRepeaterDiv" class="GpStudentRptcontentdiv">
                                    <div id="StudentRepeaterContentDiv" class="GpStudentRptcontentinnerdiv">
                                        <div class="GpStudentRptcontentinrfstdiv">
                                            <input id="StudentCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                                style="display: none" checked='<%# Eval("checked") %>' />
                                            <asp:Label ID="studentid" runat="server" Text='<%# Eval("CUST_SUBS_SK") %>' Style="display: none"></asp:Label>
                                            <div id="chk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>','unchk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>')"
                                                style="cursor: pointer;">
                                                <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" />
                                            </div>
                                            <div id="unchk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>" onclick="UnCheck('chk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>','unchk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>')"
                                                style="display: none; cursor: pointer;">
                                                <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" />
                                            </div>
                                        </div>
                                        <div class="GpStudentRptcontentinsnddiv">
                                            <asp:Label ID="StudentNameLabel" ClientIDMode="Static" CssClass="CreateStTchlbl"
                                                runat="server" Text='<%# (Eval("StudentName").ToString().Length>=38 ? Eval("StudentName").ToString().Substring(0,34) + "..." :  Eval("StudentName").ToString()) %>'></asp:Label>
                                            <asp:Label ID="StudentLoginName" runat="server" Text='<%# string.Format("({0})",Eval("StudentLoginName"))%>'
                                                Style="color: #707070; font-weight: bold; font-size: 11pt; padding-left: 3px; padding-right: 3px; float: left;"></asp:Label>
                                           
                                        </div>
                                        <div class="GpStudentRptcontentinthrdiv" style="border:1px solid #74A738;">
                                            <input type="button" value="ADD"  class="GpStudentRptcontentinthrbtn GpAddStudentRptbtn" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>','unchk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>')" />

                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="StudentRepeater" runat="server" OnItemDataBound="StudentRepeater_ItemDataBound">
						<ItemTemplate>
							<div class="teacher-lists">
								<div class="checkdiv gschkbx">
									<input id="TeacherCheckBoxes" clientidmode="Static" runat="server" type="checkbox"
										style="display: none" value='<%# Eval("CUST_SUBS_SK") %>' />
									<asp:Label ID="studentid" runat="server" Text='<%# Eval("CUST_SUBS_SK") %>' Style="display: none"></asp:Label>
									<span id="CustSubUserSk" class="srtbtnshide">
										<%--<%# Eval("CustSubUserSk")%> --%>
									</span><span id="SubsSKLabel" class="srtbtnshide">
										<%-- <%# Eval("SubscriptionId")%> --%>
									</span>
									<div id='CheckDiv<%# Eval("CUST_SUBS_SK")%>' class="checkdiv gchkbx">                               
										<span class="ico-uncheck "></span>
									</div>									
								</div>
								<div class="List_Contents">
									<asp:Label ID="StudentNameLabel" CssClass="TeachersList_TeacherNames" runat="server"
										Text='<%# Eval("StudentName") %>'></asp:Label>
									<%-- CssClass='<%#  Eval("ProfileBtnClass")%>' --%>
									<%--      <%# Eval("FullName")%> --%>
									<span class="gausername srtbtnshide">
										<%# Eval("StudentLoginName")%></span>
										 <asp:Label ID="StudentLoginName" runat="server" Text='<%# string.Format("({0})",Eval("StudentLoginName"))%>'
                                               CssClass="HideItems"></asp:Label>
									<div id='AddBtnDiv<%# Eval("CUST_SUBS_SK")%>' class="gasaddbtn">
								<%-- class='<%# Eval("ProfileBtnClass")%>'>--%>
								<%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
								<input type="button" value="ADD" class="btn btn-affermative" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>','unchk<%# Container.ItemIndex %><%# Eval("CUST_SUBS_SK") %>')" />
							</div>
							<div class="HideItems">
								<%-- class='<%# Eval("InviteBtnClass")%>' --%>
								<%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
								<asp:Button ID="InviteButton" class="btn btn-general" runat="server" Text="Remove"
									CommandArgument='<%# Eval("StudentLoginName").ToString()+"|"+Eval("StudentName").ToString() %>' />
								<%-- OnClick="RemoveButton_Click"/> --%>
							</div>
								</div>						
							</div>                    
						</ItemTemplate>
					</asp:Repeater>
                    </div>
                </div>
                <div style="float:left;width:96%; margin-left:9%;">
                <CP:CustomPaging ID="CustomPaging" runat="server">
                </CP:CustomPaging>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="AllOtherGroupsDiv" class="AllOtherGroupsDiv" runat="server">
            <div class="AllOtherGroupsImgDiv">
                <%-- <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />--%>
            </div>
            <asp:UpdatePanel ID="AllotherGroupUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="AllotherGroupDivBtn" clientidmode="Static" class="AllotherGroupDivBtn" runat="server"
                        style="width: 93%; margin-right: 20px; display: none;">
                        <asp:Button ID="AllOtherGroupButton" runat="server" Text="All Other Students" CssClass="AllOtherGroupBtn"
                            OnClick="AllOtherGroupButton_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<script src="/dotnetnuke/DesktopModules/eCollection/Scripts/kendo.web.min.js" type="text/javascript"></script>
