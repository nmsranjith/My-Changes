<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditStudentProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.EditStudentProfile" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement"
    Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnJsInclude runat="server" FilePath="DesktopModules/eCollection_Students/Scripts/EditProfile.js"
    ForceProvider="DnnFormBottomProvider" />
<style type="text/css">
    .k-state-selected.k-state-focused
    {
        background-color: #707070 !important;
    }
	.selectedTabHolder
	{
		z-index:10000 !important;
	}
</style>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<div id="FirstDiv" class="EditProfile_TopDiv" style="height: 320px"> 
    <div style="width: 98%; height: 57px; padding-left: 15px; padding-top: 16px; float: left;">
        <div style="width: 49.9%; float: left;">
            <h5>
                First name:</h5>
            <div class="eCollectionEditDiv">
                <div class="eCollectionTbxHolder">
                    <asp:TextBox ID="NameTextBox" runat="server" autocomplete="off" MaxLength="60" CssClass="eCollectionTextBox inspectletIgnore"
                        ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="eCollectionEditSpan">
                    <span>*</span></div>
            </div>
        </div>
        <div style="width: 49.9%; float: left;">
            <h5>
                Last name:
            </h5>
            <div class="eCollectionEditDiv">
                <div class="eCollectionTbxHolder">
                    <asp:TextBox ID="LastNameTextBox" runat="server" autocomplete="off" MaxLength="60"
                        CssClass="eCollectionTextBox inspectletIgnore" ClientIDMode="Static"></asp:TextBox>
                </div>
                <div class="eCollectionEditSpan">
                    <span>*</span></div>
            </div>
        </div>
    </div>
    <div style="width: 100%; padding-left: 15px; margin-top: 24px; float: left;">
        <div style="width: 40%; float: left;">
            <%-- <h5>
                        Date of Birth :
                    </h5>--%>
            <div class="eCollectionEditDiv">
                <div class="eCollectionTbxHolder">
                    <input type="text" id="DateofBirthTextBox" placeholder="Date of birth (dd/mm/yyyy)" autocomplete="off"
                        runat="server" clientidmode="Static" class="eCollectionTextBox inspectletIgnore" style="font-style: italic;" />
                    <asp:HiddenField ID="DOBHdFld" runat="server" ClientIDMode="Static" />
                </div>
            </div>
        </div>
        <div style="width: 59%; float: left;">
            <div style="width: 37%; height: 27px; float: left;">
                <%--<h5>
                            Gender :
                        </h5>--%>
                <div class="GenderDD">
                    <div class="Div_FullWidth">
                        <select id="GenderDropDown" runat="server" clientidmode="Static" style="height: 27px;
                            float: left; position: absolute; width: 130px; display: block;" class="inspectletIgnore">
                            <option value="F">Female</option>
                            <option value="M">Male</option>
                        </select>
                    </div>
                </div>
            </div>
            <div style="width: 47%; height: 27px; float: left;">
                <%-- <h5>
                            Grade :
                        </h5>--%>
                <div class="GradesDD">
                    <div class="Div_FullWidth">
                        <select id="GradeDropDown" runat="server" clientidmode="Static" style="height: 31px;
                            float: left; position: absolute; width: 130px;" class="inspectletIgnore">
                            <option value="F">Year F</option>
                            <option value="1">Year 1</option>
                            <option value="2">Year 2</option>
                            <option value="3">Year 3</option>
                            <option value="4">Year 4</option>
                            <option value="5">Year 5</option>
                            <option value="6">Year 6</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 98.5%; height: 68px; padding-left: 15px; margin-top: 25px; float: left;">
        <div style="width: 29%; float: left;">
            <h5>
                Username:
            </h5>
            <div class="eCollectionEditSpanDiv">
                <div class="eCollectionTbxHolder1">
                    <asp:Label ID="StudentUserNameTextBox" autocomplete="off" runat="server" CssClass="eCollectionTextBox UserNameLbl"
                        ClientIDMode="Static">
                    </asp:Label>
                </div>
            </div>
        </div>
        <div style="width: 29%; float: left;">
            <h5>
                Password:
            </h5>
            <div class="eCollectionEditDiv">
                <div class="eCollectionTbxHolder1">
                    <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="60" CssClass="eCollectionTextBox inspectletIgnore"
                        onkeydown="return event.keyCode != 13 && event.which != 13;"
                        autocomplete="off" ClientIDMode="Static">
                    </asp:TextBox></div>
                <div class="eCollectionEditSpan">
                    <span>*</span></div>
            </div>
        </div>
        <div style="width: 41%; float: left;">
            <h5>
                Email:
            </h5>
            <div class="eCollectionEditDiv">
                <asp:TextBox ID="EmailTextBox" runat="server" autocomplete="off" MaxLength="250"
                    CssClass="eCollectionTextBox inspectletIgnore" onkeydown="return event.keyCode != 13 && event.which != 13;"
                    ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
    </div>
    <div style="padding-left: 15px; height: 66px; padding-top: 21px; width: 100%; float: left;">
        <div style="width: 50%; height: 37px; float: left;">
            <div style="width: 49%; float: left;">               
                <div id="ReadingRecoveryCheckBox" class="ReadingRecovery" onclick="ReadingRecovery();">
                    <img id="RRChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>' alt=""
                        style="display: none;" />
                    <img id="RRUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                        alt="" />
                    <div style="float: right; width: 81%; margin-top: 6px;">
                        READING RECOVERY</div>
                </div>
            </div>
            <div style="width: 41%; float: left;margin-left:5%;">                
                <div id="ESLCheckBox" class="ESL" onclick="ESL();">
                    <div style="float: left; width: 19%;">
                        <img id="EslChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>'
                            alt="" style="display: none;" />
                        <img id="EslUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                            alt="" />
                    </div>
                    <div style="float: right; width: 60%; margin-top: 6px;">
                        ESL</div>
                </div>
            </div>
            <asp:CheckBox ID="ReadingRecoveryCheck" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="inspectletIgnore"  />
            <asp:CheckBox ID="ESLCheck" runat="server" ClientIDMode="Static" Style="display: none;" CssClass="inspectletIgnore"/>
        </div>
    </div>
</div>
<div style="width: 99%; float: left; margin-top: 15px; margin-left: 20px;">
    <hr style="width: 95%; float: left; background-color: Gray; height: 1px; color: Gray;
        border: 0px solid white;" />
</div>
<div class="editstudprofsavediv">
	<a id="SwitchSubscriptionLink" runat="server" clientidmode="Static" class="different-sub" onclick="ChangeStudentSubscription()">Move this student to a different subscription</a>
    <div class="ActiveAddButtonsHolder" style="width: 25%;float: right;">
        <asp:Button ID="SaveStudentProfile" CssClass="AddButton" Text="SAVE STUDENT" ClientIDMode="Static"
            OnClick="SaveStudentProfile_Click" runat="server" Style="float: right;" />
    </div>
</div>
<div id="move-popup" class="move-studentname" style="display:none;">
	<div class="std-title">
		<h1 id="StudentNameHeader" runat="server">Move to:</h1>
	</div>
	<div class="std-content">
		<div class="std-msg">
			<h5>choose a subscription</h5>
			<asp:DropDownList ID="MoveSubscriptionsDpn" runat="server" CssClass="inspectletIgnore" ClientIDMode="Static" DataTextField="Text" DataValueField="Id"></asp:DropDownList>
			<br>
			<p class="txt-red">This will move the student and all of the reading history to the new subscription and they will no longer appear in this subscription.</p>
			<h6>Note:</h6>
			<ul>
				<li>The student will be removed from all current groups and reading sessions</li>
				<li>Only teachers that have access to the new subscription will be able to view and edit the students information and reading history</li>
			</ul>
		</div>
		<div class="std-btn">			
			<input id="SwitchCancelBtn" class="btn btn-cancel" type="button" value="Cancel"  />
			<input id="SwitchMoveBtn" class="btn btn-affermative" type="button" value="Move" onclick="SwitchSubscriptions()" />
		</div>
	</div>
</div>
<asp:HiddenField ID="RRHdn" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ESLHdn" runat="server" ClientIDMode="Static" />
<span id="AfterSwitchUrl" runat="server" clientidmode="Static" class="HideItems"></span>
<asp:HiddenField ID="StudentSk" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="StudentUserName" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="CurrentSubsSk" runat="server" ClientIDMode="Static" />
