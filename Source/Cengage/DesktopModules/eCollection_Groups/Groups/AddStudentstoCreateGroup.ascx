<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddStudentstoCreateGroup.ascx.cs" 
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.AddStudentstoCreateGroup" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Groups/Scripts/AddStudent.js"
    ForceProvider="DnnFormBottomProvider" />
<style type="text/css">
    #SelectedStudentList
    {
        list-style-type: none;
        margin: 0;
        padding: 0;
    }
    .GpStudentRptcontentinthrdiv
    {
        border:none;
    }
    .GpStudentRptcontentinthrbtn
    {
        text-shadow:none!important;
    }
    #SelectedStudentList li
    {
        display: inline;
    }
    
    #SearchTextBox-list
    {
        width: 614px !important;
        margin-left: -1px !important;
        margin-top: 6px !important;
        box-shadow: none !important;
        -webkit-box-shadow: none !important;
    }
    #SearchTextBox-list ul
    {
        background: -moz-linear-gradient(center top , white 0%, white 5%, #DBD9D9 130%) repeat scroll 0 0 transparent;
        background: -webkit-gradient(linear, left top, left bottom, from(#FDFDFD), to(#EAE9E9));
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#fdfdfd', endColorstr='#EAE9E9', gradientType='0');
        background: -ms-linear-gradient(top, white 5%, #EAE9E9 130%) !important;
    }
    
    #SearchTextBox-list .k-state-hover, #SearchTextBox-list .k-state-focused, .k-state-hover:hover
    {
        background: #707070 !important;
        border: 0px solid #707070 !important;
        color: White !important;
    }
    /*.classSearchwater:hover
    {
        border: none !important;
        padding-left: 8px !important;
        padding-top: 1px !important;
        background: transparent !important;
        width: 607px !important;
        box-shadow: none !important;
    }
    .classSearchwater
    {
        width: 607px !important;
        height: 29px !important;
        background: transparent !important;
        padding-left: 8px !important;
        padding-top: 1px !important;
        box-shadow: none !important;
        border: none !important;
        font-size: 10pt !important;
    }*/
</style>
<div id="StudentDashboard" runat="server" clientidmode="Static" style="margin-top: 10px;
    float: left; width: 100%;">
    <div class="GroupAddto">
        <div class="GroupAddtoinnerfstdiv">
            <h5>
                TO:</h5>
            <asp:Button ID="BacktoCreateBtn" runat="server" Text="BACK" CssClass="BackBtnGroup"
                OnClick="BacktoCreateBtn_Click" />
            <div class="GrayLineGroup" ></div>
        </div>
        <div id="DetailsDiv" runat="server" style="float: left; width: 60%; margin: -10px 0px 10px 1px;
            height: auto; overflow: hidden">
        </div>
        <div class="GroupAddtoinnersnddiv">
            <ul id="SelectedStudentList" runat="server" clientidmode="Static">
            </ul>
            <div id="dialog" title="Alert Message" style="display: none;">
                <p>
                    <span id="StudentNamespan"></span>is already added</p>
            </div>
            <asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
        </div>
        <%--<asp:Button  ID="BacktoCollectionButton" runat="server" Text="<< Back to Create Group" style="margin-bottom: 12px;margin-right: 9px;margin-top: 12px;cursor:pointer; float: right;background-color: #F9F9F9;width: 180px;height: 30px;border: 1px solid lightgray;border-radius: 5px;box-shadow: 1px 2px lightgray;" />--%>
    </div>
    <div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
        <div  class="bubble1" >
            <asp:Label ID="Message1" runat="server" Text="" />                        
        </div>
    </div> 
    <div class="SearchDiv eg-at-searchdiv">
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
    <div id="StudentsContentDiv" runat="server">
    <asp:UpdatePanel ID="AddStudentToGroupUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="StudentDiv" runat="server" clientidmode="Static" style="display: block;
                float: left; width: 100%;">
               
                <div class="ProgressDivClass" style="display:none" id="UpdateProgressImg">
                    <div class="ProgressInnerDiv">
                        <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"  alt="Processing" /> 
                    </div>
                </div> 
                <div id="RepeaterLftConntrLine" class="">
                    <div id="RepeaterStudentDiv" class="GpRptitmcontentdiv">
                        <asp:Repeater ID="StudentRepeater1" runat="server" >
                            <ItemTemplate>
                                <div id="StudentRepeaterDiv" class="GpStudentRptcontentdiv">
                                    <div id="StudentRepeaterContentDiv" class="GpStudentRptcontentinnerdiv">
                                        <div class="GpStudentRptcontentinrfstdiv">
                                            <input id="StudentCheckBoxes" runat="server" clientidmode="Static" type="checkbox"
                                                style="display: none" checked='<%# Eval("checked") %>' />
                                            <asp:Label ID="studentid" runat="server" Text='<%# Eval("UserID") %>' Style="display: none"></asp:Label>
                                            <div id="chk<%# Container.ItemIndex %><%# Eval("UserID") %>" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("UserID") %>','unchk<%# Container.ItemIndex %><%# Eval("UserID") %>')"
                                                style="cursor: pointer;">
                                                <img id="ClassCheckBoxImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>"
                                                style="width: 20px;" />
                                            </div>
                                            <div id="unchk<%# Container.ItemIndex %><%# Eval("UserID") %>" onclick="UnCheck('chk<%# Container.ItemIndex %><%# Eval("UserID") %>','unchk<%# Container.ItemIndex %><%# Eval("UserID") %>')"
                                                style="display: none; cursor: pointer;">
                                                <img id="ClassUnCheckImg" alt="" clientidmode="Static" src="<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>"
                                                style="width: 20px;" />
                                            </div>
                                        </div>
                                        <div class="GpStudentRptcontentinsnddiv">
                                            <asp:Label ID="StudentNameLabel" ClientIDMode="Static" CssClass="CreateStTchlbl"
                                                runat="server" Text='<%# Eval("StudentNames") %>'></asp:Label>
                                            <asp:Label ID="StudentLoginName" runat="server" Text='<%# string.Format("({0})",Eval("StudentLoginName"))%>'
                                                Style="color: #707070; font-weight: bold; font-family: Raleway-regular,Raleway,Arial, Sans-Serif;
                                                font-size: 11pt; padding-left: 3px; padding-right: 3px; float: left;"></asp:Label>
                                        </div>
                                        <div class="GpStudentRptcontentinthrdiv" style="border:1px solid #74A738">
                                           <input type="button" value="ADD" class="GpStudentRptcontentinthrbtn GpAddStudentRptbtn" onclick="Check('chk<%# Container.ItemIndex %><%# Eval("UserID") %>','unchk<%# Container.ItemIndex %><%# Eval("UserID") %>')" />
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
						<asp:Repeater ID="StudentRepeater" runat="server" OnItemDataBound="StudentRepeater_ItemDataBound">
						<ItemTemplate>
							<div class="teacher-lists">
								<div class="checkdiv gschkbx">
									<input id="TeacherCheckBoxes" type="checkbox"
										class="HideItems" value='<%# Eval("UserID")%>'/>
									<asp:Label ID="studentid" runat="server" Text='<%# Eval("UserID") %>' Style="display: none"></asp:Label>
									<span id="CustSubUserSk" class="srtbtnshide">
										<%--<%# Eval("CustSubUserSk")%> --%>
									</span><span id="SubsSKLabel" class="srtbtnshide">
										<%-- <%# Eval("SubscriptionId")%> --%>
									</span>
									<div id='CheckDiv<%# Eval("UserID")%>' class="checkdiv gchkbx">                               
										<span class="ico-uncheck"></span>
									</div>
								</div>
								<div class="List_Contents">
									<asp:Label ID="StudentNameLabel" CssClass="TeachersList_TeacherNames" runat="server"
										Text='<%# Eval("StudentNames") %>'></asp:Label>
									<%-- CssClass='<%#  Eval("ProfileBtnClass")%>' --%>
									<%--      <%# Eval("FullName")%> <%# Eval("StudentLoginName")%>--%>
									<span class="gausername srtbtnshide">
										<%# Eval("StudentNames") %></span>
										 <asp:Label ID="StudentLoginName" runat="server" Text='<%# string.Format("({0})",Eval("StudentLoginName"))%>'
                                               CssClass="HideItems"></asp:Label>
									<div id='AddBtnDiv<%# Eval("UserID")%>' class="gasaddbtn">
								<%-- class='<%# Eval("ProfileBtnClass")%>'>--%>
								<%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
								<input type="button" value="ADD" class="btn btn-affermative"  />
							</div>
							<div class="HideItems">
								<%-- class='<%# Eval("InviteBtnClass")%>' --%>
								<%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
								<asp:Button ID="InviteButton" class="btn btn-general" runat="server" Text="Remove"
									CommandArgument='<%# Eval("StudentLoginName").ToString()+"|"+Eval("StudentNames").ToString() %>' />
								<%-- OnClick="RemoveButton_Click"/> --%>
							</div>
								</div>						
							</div>                    
						</ItemTemplate>
					</asp:Repeater>
                    </div>
                </div>
                <CP:CustomPaging ID="CustomPaging" runat="server">
                </CP:CustomPaging>
                <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
                <div id="AllOtherGroupsDiv" class="AllOtherGroupsDiv" runat="server">
                    <%--  <div class="AllOtherGroupsImgDiv">
                <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" alt="" />
            </div>--%>
                    <%--<asp:UpdatePanel ID="AllotherGroupUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <div id="AllotherGroupDivBtn" clientidmode="Static" class="AllotherGroupDivBtn" runat="server"
                        style="width: 93%; margin-right: 20px;">
                        <asp:Button ID="AllOtherGroupButton" runat="server" Text="All Other Students" CssClass="AllOtherGroupBtn"
                            OnClick="AllOtherGroupButton_Click" />
                    </div>
                    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</div>

