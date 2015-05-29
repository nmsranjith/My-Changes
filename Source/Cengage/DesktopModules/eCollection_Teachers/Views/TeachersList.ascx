<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeachersList.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.TeachersList" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Teachers/Scripts/TeachersList.js"
    ForceProvider="DnnFormBottomProvider" />
<div id="TeachersListDiv" class="TeachersList">
    <div class="TeachersList_TopDiv">
	
	<h2>Teachers</h2>
	
	 <div class="ProgressDivClass" style="display: none" id="UpdateProgressImg">
        <div class="ProgressInnerDiv">
            <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                alt="Processing" />
        </div>
    </div>
	
        
   
 
        <div class="eClUserSearchDiv ">
		<label id="eClSearchLabel" for="SearchTextBox">
                Search:</label>
				
            <asp:TextBox ID="SearchTextBox" class="eClSearchbox" runat="server" placeholder="teachers list"
                ClientIDMode="Static"></asp:TextBox>
				
				 <asp:UpdatePanel ID="SearchPanel" runat="server">
            <ContentTemplate>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static" OnClientClick="ShowUpdate()"
                        OnClick="SearchButton_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
            
        </div>
		
		
		
		
		<div class="TeachersList_CheckAllDiv">
            <asp:UpdatePanel ID="CheckPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="CheckAllDiv" onclick="javascript:CheckAll();">
                        <asp:Image ID="Image4" ClientMode="Static" runat="server" ImageUrl="~/Portals/0/images/circle_big.png" />
													
							<span class="ico-uncheck "></span>
						</div>
                    <div id="UnCheckAllDiv" style="display: none;
                        cursor: pointer;" onclick="javascript:UnCheckAll();">
                        <asp:Image ID="Image5" ClientMode="Static" runat="server" Height="21px" Width="20px"
                            ImageUrl="~/Portals/0/images/tick_student.png" CssClass="HideItems" />
						<span class="ico-check "></span>
					</div>							
							
							
                    <div onclick="javascript:ClickSelectAll();" class="select-all">
                        Select All</div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
		
        <asp:UpdatePanel ID="SortingPanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <div class="TeachersList_SortingDiv">
                    <span style="float: left; margin-right: 4px; cursor: pointer; margin-top: 4px;" onclick="javascript:ClickAZ();">
                        A – Z</span>
                    <asp:Button ID="NamesAscButton" runat="server" ClientIDMode="Static" CssClass="NamesAscSortingButtons" CommandArgument="desc" OnClick="SortingButton_Click"
                        Style="margin-top: 4px;"/>
                    <asp:Button ID="NamesDescButton" runat="server" ClientIDMode="Static" CssClass="NamesDescSortingButtons" CommandArgument="asc" Visible="false" OnClick="SortingButton_Click"
                        Style="margin-top: 4px;" />
                </div>
				<div class="categorybysort">	
						<label for="" class="">Sort Results:</label>				
						<select name="" id="teacherssortdpn">
							<option value="">Recommended</option>
							<option value="">Title (A to Z)</option>
							<option value="">Title (Z to A)</option>
							<option value="">Latest</option>
						</select>
						</div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
      <div class="ClassSearchDiv">
									<span class="student-class">Teachers</span>
									<span class="student-line"></span>									
                                    
 </div> 

    
    <div id="listView">
        <asp:UpdatePanel ID="TeachersListPanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:ListView ID="TeachersListView" runat="server">
                    <ItemTemplate>
                        <div class="teacher-lists">
                           
								<div class="checkdiv">
                                            <input type="checkbox" id="TeachersCheckBoxes" runat="server" clientidmode="Static"
                                                style="display: none;" value='<%# Eval("TeacherId")%>' />
                                            <span id="CustSubUserSk" class="srtbtnshide">
                                                <%# Eval("CustSubUserSk")%></span> <span id="SubsSKLabel" class="srtbtnshide">
                                                    <%# Eval("SubscriptionId")%></span>
                                            <div id='CheckDiv<%# Eval("TeacherId")%>' onclick="Checked(<%# Eval("TeacherId")%>)"
                                                class="checkdiv">
                                                <asp:Image ID="CheckAllImage" ClientIDMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/circle_big.png" />
												<span class="ico-uncheck "></span>
                                            </div>
                                            <div id="UnCheckDiv<%# Eval("TeacherId")%>" onclick="UnChecked(<%# Eval("TeacherId")%>)"
                                                class="checkdiv srtbtnshide">
                                                <asp:Image ID="Image3" ClientMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/tick_student.png" />
												<span class="ico-check "></span>
                                            </div>
											
											
                                 </div>
								
                                    <div class="List_Contents">
                                        
                                        <div class='<%# Eval("ProfileBtnClass")%>' title="<%# Eval("FirstName")%> <%# Eval("LastName")%> (<%# Eval("UserLoginName")%>)">
                                             <span id="TeacherNameLabel" class="TeachersList_TeacherNames"><span><%# Eval("FirstName")%> <%# Eval("LastName")%></span>(<span><%# Eval("UserLoginName")%></span>)</span>
                                          <%--      <%# Eval("FullName")%> --%>  
										  <span id="UserNameLabel" class="srtbtnshide"><%# Eval("UserLoginName")%></span>
                                        </div>
										<div class='<%# Eval("InviteBtnClass")%>' title="<%# Eval("FirstName")%> <%# Eval("LastName")%> (<%# Eval("UserLoginName")%>)">
                                             <span id="TeacherNameLabel" class="TeachersList_TeacherNames"><span><%# Eval("FirstName")%> <%# Eval("LastName")%></span>(<span>Pending</span>)</span>
                                          <%--      <%# Eval("FullName")%> --%>  
										  <span id="UserNameLabel" class="srtbtnshide"><%# Eval("UserLoginName")%></span>
                                        </div>
                                        <div class='<%# Eval("ProfileBtnClass")%>'>
                                            
                                               <%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
											   
											      											   
                                                <asp:HyperLink ID="ProfileButton" runat="server" NavigateUrl='<%# Eval("profilelink") %>' CssClass="btn btn-affermative" Text="View Profile"></asp:HyperLink>
                                            
                                        </div>
										<div class='<%# Eval("InviteBtnClass")%>' >                                         
                                               <%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
											   <asp:Button ID="InviteButton" class="btn btn-general" runat="server" Text="Re-send invitation email" CommandArgument='<%# Eval("UserLoginName").ToString()+"|"+Eval("FirstName").ToString() %>' OnClick="ReSendInviteButton_Click"/>                                               
                                         
                                        </div>
                                    
                                </div>
                           
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
  
            <div class="PagerHdr">
                <div id="Pager" class="TeacherListPager k-pager-wrap">
                </div>
            </div>
        
</div>
<asp:HiddenField ID="SelTeachersCnt" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="SelectedTeacherId" runat="server" ClientIDMode="Static" />
<script type="text/x-kendo-tmpl" id="template">
    <div style="width: 94.8%; float: left; margin-left: 54px; border-left: 1px solid lightgray;">
        <div style="float: left; width: 90%;">
            <div >
			<div style="width: 6.9%; float: left; padding-top: 9px;">
                        <input type="checkbox" id="TeachersCheckBoxes" runat="server" clientidmode="Static"
                            style="display: none;" value='${TeacherId}' />
                        <span id="CustSubUserSk" class="srtbtnshide">${CustSubUserSk}</span> <span id="SubsSKLabel"
                            class="srtbtnshide">${SubscriptionId}</span>
                        <div id='CheckDiv${TeacherId}' onclick="Checked(${TeacherId})" class="checkdiv">
                            <asp:Image ID="CheckAllImage" ClientIDMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/circle_big.png" />
                        </div>
                        <div id="UnCheckDiv${TeacherId}" onclick="UnChecked(${TeacherId})" class="checkdiv srtbtnshide">
                            <asp:Image ID="Image3" ClientMode="Static" runat="server" Width="20px" ImageUrl="~/Portals/0/images/tick_student.png" />
                        </div>
                    </div>
			
                <div class="List_Contents">
                    
                    <div title="${FirstName} ${LastName} ( ${UserLoginName} )">
                        <span id="TeacherNameLabel" class="TeachersList_TeacherNames">${FullName}</span>
                        <span id="UserNameLabel" class="srtbtnshide">${UserLoginName}</span>
                    </div>
                    <div style="width: 22.3%; float: left; padding-top: 7px;">
                        <div class="greenBtn">
                            <a class="ProfileButton" href='${profilelink}'>PROFILE</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</script>
<div id="Delete-message" title="Confirm Delete" style="display: none; background: white !important;">
    <div class="deleteteacherheaderpp">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Confirm Delete</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <span id="MessageLiteral" style="font-size: 10pt; color: #707070; padding: 23px; float: left;">Are you sure you want
                to delete teacher/s?</span>
        </div>
        <div style="width: 92%;">
            <input type="button" id="YesButton" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
            <input type="button" id="NoButton" style="margin-left: 18px;" value="No" class="popupokbtn" />
        </div>
    </div>
</div>
<div id="MultipleSubsTeachers" style="display: none; background: white !important;">
    <div class="deleteteacherheaderpp">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
            <literal style="font-size: 10pt;
                color: #707070; padding: 30px; float: left;">Please select teachers belongs to same subscription.</literal>
        </div>
        <div style="width: 92%;">
            <input type="button" id="OkButton" value="Ok" class="popupokbtn" />
        </div>
    </div>
</div>
<div id="searcheditems" class="srtbtnshide" runat="server" clientidmode="Static">
</div>
