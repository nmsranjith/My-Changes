<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTeachersToCreateGroup.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Groups.Groups.AddTeachersToCreateGroup" %>
<%@ Register Src="CustomPaging.ascx" TagName="CustomPaging" TagPrefix="CP" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Groups/Scripts/AddTeacher.js"
    ForceProvider="DnnFormBottomProvider" />
<style type="text/css">
    #SearchTextBox-list
    {
        width: 614px !important;
        margin-left: -1px !important;
        margin-top: 6px !important;
        box-shadow: none !important;
        -webkit-box-shadow: none !important;
    }
    .GpStudentRptcontentinthrdiv
    {
        border: none;
    }
    .GpStudentRptcontentinthrbtn
    {
        text-shadow: none !important;
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
        border: 1px solid #707070 !important;
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
<div class="GroupAddto">
    <div class="GroupAddtoinnerfstdiv">
        <h5>
            TO:</h5>
        <asp:Button ID="BacktoCreateBtn" runat="server" Text="BACK" CssClass="BackBtnGroup"
            OnClick="BacktoCreateBtn_Click" />
        <div class="GrayLineGroup">
        </div>
    </div>
    <div id="DetailsDiv" runat="server" class="AddtchDetailsDiv">
    </div>
    <div class="GroupAddtoinnersnddiv">
        <ul id="SelectedStudentList" runat="server" clientidmode="Static">
        </ul>
        <div id="dialog" title="Alert Message" style="display: none;">
            <p>
                <span id="StudentNamespan"></span>is Already added</p>
        </div>
        <asp:TextBox ID="SelectedValueTextBox" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox></div>
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
<asp:UpdatePanel ID="AddTeacherToGroupUpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <div id="StudentDiv" runat="server" clientidmode="Static" class="AddtchStudentsDiv">
            <div class="GroupTopDiv">
                <div class="GroupSelectAll" style="display: none;">
                    <div id="SelectAllDiv" class="Addtchselectalldiv">
                        <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
                            onclick="javascript:CheckAll(this);" id="SelectallCheckBox" alt="Image cannot be displayed" /><literal
                                id="literal"></literal></div>
                    <span class="SelectAllSpan" onclick="javascript:document.getElementById('SelectallCheckBox').click()">
                        SELECT ALL</span>
                </div>
                <div class="GroupsSortReaddiv Addtchgrpsrtdiv" style="display: none;">
                    <span class="Addtchazspan" onclick="javascript:document.getElementById('SortingButton').click();">
                        A – Z</span>
                    <asp:Button ID="SortingButton" ClientIDMode="Static" CssClass="SortRead Sort Addtchsrtbtn"
                        runat="server" CommandName="Ascending" OnClick="SortingButton_Click" />
                </div>
            </div>
            
            <div class="ProgressDivClass" style="display: none" id="UpdateProgressImg">
                <div class="ProgressInnerDiv">
                    <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" class="AjaxLoaderImg"
                        alt="Processing" />
                </div>
            </div>
            <div style="display: none;" class="SearchDiv Addtchsrchdiv">
                <div class="SearchInnerDiv Addtchsrchdiv">
                    <input type="text" id="SearchTextBox1" class="classSearchwater" spellcheck="false"
                        autocomplete="off" clientidmode="Static" runat="server" title="Enter your search here ..." />
                    <div class="Searchbtndiv">
                        <asp:Button ID="SearchButton1" runat="server" CssClass="SearchButton" OnClick="SearchButton_Click"
                            OnClientClick="ShowUpdate()" />
                    </div>
                </div>
            </div>            
            <asp:Repeater ID="StudentRepeater" runat="server">
                <ItemTemplate>
                    <div class="teacher-lists">
                        <div class="checkdiv gschkbx">
                            <input id="TeacherCheckBoxes" type="checkbox"
                                class="HideItems" value='<%# Eval("Id")%>' />
                            <asp:Label ID="studentid" runat="server" Text='<%# Eval("Id") %>' class="HideItems"></asp:Label>
                            <span id="CustSubUserSk" class="srtbtnshide">
                                <%--<%# Eval("CustSubUserSk")%> --%>
                            </span><span id="SubsSKLabel" class="srtbtnshide">
                                <%-- <%# Eval("SubscriptionId")%> --%>
                            </span>
                            <div id='CheckDiv<%# Eval("Id")%>' class="checkdiv gchkbx">                               
                                <span class="ico-uncheck "></span>
                            </div>                          
                        </div>
                        <div class="List_Contents">
                            <asp:Label ID="TeacherNameLabel" CssClass="TeachersList_TeacherNames" runat="server"
                                Text='<%# Eval("Text") %>'></asp:Label>
                            <%-- CssClass='<%#  Eval("ProfileBtnClass")%>' --%>
                            <%--      <%# Eval("FullName")%> <%# Eval("UserName")%>--%>
                            <span class="gausername srtbtnshide">
                               <%# Eval("Text") %> </span>
							<div id='AddBtnDiv<%# Eval("Id")%>' class="gasaddbtn">
                        <%-- class='<%# Eval("ProfileBtnClass")%>'>--%>
                        <%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
                        <input type="button" value="ADD" class="btn btn-affermative"  />
                    </div>
                    <div class="HideItems">
                        <%-- class='<%# Eval("InviteBtnClass")%>' --%>
                        <%-- <a class="ProfileButton" href='<%# Eval("profilelink")%>'>PROFILE</a>--%>
                        <asp:Button ID="InviteButton" class="btn btn-general" runat="server" Text="Remove"
                            CommandArgument='<%# Eval("UserName").ToString()+"|"+Eval("FirstName").ToString() %>' />
                        <%-- OnClick="RemoveButton_Click"/> --%>
                    </div>
						</div>						
                    </div>                    
                </ItemTemplate>
            </asp:Repeater>
            <CP:CustomPaging ID="CustomPaging" runat="server">
            </CP:CustomPaging>           
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="LastNode" runat="server" Value="empty" />
