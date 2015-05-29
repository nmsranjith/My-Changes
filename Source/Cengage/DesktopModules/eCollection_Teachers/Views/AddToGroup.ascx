<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToGroup.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.AddToGroup" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<div id="MsgDiv">
    <Msg:message id="Messages" runat="server">
    </Msg:message>
</div>
<style type="text/css">
    .ClassSearchDiv
    {
        width: 97%;
        margin-top: -40px;
    }
    
    .ClassSearchDiv img
    {
        margin-left: 2px;
        margin-top: 36px;
    }
    #SearchTextBox-list
    {
        width: 610px !important;
        margin-left: -1px !important;
    }
</style>
<div style="float: left; width: 100%; margin-top: -13px;">
    <div class="ToBox">
        <div class="ToBoxText">
            <h5>
                TO:</h5>
            <div class="GrayLine">
            </div>
            <ul id="SelectedTeacherList" runat="server" clientidmode="Static">
            </ul>
        </div>
        <div id="StudentNames" class="Class_FullWidth">
        </div>
    </div>
    <%--<div style="float: left; width: 50%" id="SelectAllDiv">
        <div id="CheckAllDiv" onclick="CheckAll()" style="cursor: pointer;">
            <img src='<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>' class="SelectallCheckBoxclass"
                id="CheckBoxAll" alt="Image cannot be displayed" style="width: 20px;" /></div>
        <div id="UnCheckAllDiv" onclick="UnCheckAll()" style="display: none; cursor: pointer;">
            <img src='<%=Page.ResolveUrl("Portals/0/images/tick_student.png")%>' class="SelectallCheckBoxclass"
                id="UnCheckBoxAll" alt="Image cannot be displayed" style="width: 20px;" /></div>
        <span class="SelectAllSpan" onclick="javascript:ClickSelectAll();" style="color: gray;
            cursor: pointer;">SELECT ALL</span>
    </div>
    <div class="GroupsSortReaddiv" id="SortDiv">
        <asp:UpdatePanel ID="SortingPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="PMAscButton" runat="server" Text="PM READING LEVEL" OnClick="PmAscButton_Click"
                    CssClass="SortRead SortPM" />
                <asp:Button ID="PMDescButton" runat="server" Text="PM READING LEVEL" OnClick="PmDescButton_Click"
                    CssClass="SortReadDesc SortPM" Visible="False" />
                <b style="margin: 0px 1px 0px 10px; float: left;">|</b>
                <asp:Button ID="NamesAscButton" runat="server" Text="A – Z" OnClick="NamesAscButton_Click "
                    CssClass="SortRead SortNm" />
                <asp:Button ID="NamesDescButton" runat="server" Text="A – Z" OnClick="NamesDescButton_Click"
                    CssClass="SortReadDesc SortNm" Visible="False" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div> --%>
	<div class="TeacherAddtoGroup_SearchList">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="SearchInnerDiv ">
								<label for="SearchTextBox">
								Search</label>
							 
						    <asp:TextBox ID="SearchTextBox" CssClass="eClSearchbox" ClientIDMode="Static" runat="server"
                                placeholder="Enter your search here ..."></asp:TextBox>
							 <div class="Searchbtndiv">
                            <asp:Button ID="SearchButton" runat="server" ClientIDMode="Static"   />
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 
            </div>
	<div class="TeacherAddtoGroup_SelectAllDiv"  id="SelectAllDiv">
        <div id="SelectAllDiv " class="eg-lfloat" >
                            <img src="<%=Page.ResolveUrl("Portals/0/images/circle_big.png")%>" class="SelectallCheckBoxclass"
                                id="SelectallCheckBox" alt="Image cannot be displayed" /><literal id="literal"></literal>
							</div>
							<span id="SelectAllChkbx" class="ico-uncheck "></span>
							<span class="SelectAllSpan" onclick="javascript:document.getElementById('SelectallCheckBox').click()">Select All</span>
    </div>
    <div class="AddtoGroupsSortReaddiv" id="SortDiv">
       <%--
                <span style="float: left;">PM READING LEVEL</span>
                <asp:Button ID="PMAscButton" ClientIDMode="Static" Text="PM READING LEVEL" runat="server"
                    OnClick="PmAscButton_Click" CssClass="SortRead SortPM" />
                <asp:Button ID="PMDescButton" ClientIDMode="Static" Text="PM READING LEVEL" runat="server"
                    OnClick="PmDescButton_Click" CssClass="SortReadDesc SortPM" Visible="False" />
                <b style="margin: 0px 0px 0px 10px; float: left;">| </b>
                <span style="float: left;">
					A - Z</span> 
                <asp:Button ID="NamesAscButton" runat="server" Text="A – Z" ClientIDMode="Static"
                    OnClick="NamesAscButton_Click" CssClass="SortRead SortNm" />
                <asp:Button ID="NamesDescButton" runat="server" Text="A – Z" ClientIDMode="Static"
                    OnClick="NamesDescButton_Click" CssClass="SortReadDesc SortNm" Visible="False" />
            --%>
		 <asp:UpdatePanel ID="SortingPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate><div class="categorybysort">
                        <label for="" class="">
                            Sort Results:</label>
                        <select name="" id="groupssortdpn">
                            <option value="">Recommended</option>
                            <option value="">Title (A to Z)</option>
                            <option value="">Title (Z to A)</option>
                            <option value="">Latest</option>
                        </select>
        </div></ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<div id="MessageOuterDiv" runat="server" style="width:100%;position:static ;display:none;">
    <div  class="bubble1" >
        <asp:Label ID="Message1" runat="server" Text="" />                        
    </div>
</div> 
<div id="SecondDiv" runat="server">
   
    <img src="<%=Page.ResolveUrl("Portals/0/images/ajax-loader1.gif")%>" style="vertical-align: middle;display:none;position:absolute; margin-top:18%;margin-left:44%" id="UpdateProgressImg"  alt="Processing" /> 
   
    <div class="SearchDiv HideItems" >
        <asp:UpdatePanel ID="UpdatePanel1s" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="eClUserSearchDiv ">
                    <asp:TextBox ID="SearchTextBox1" CssClass="eClSearchbox" ClientIDMode="Static" runat="server"
                        placeholder="Enter your search here ..."></asp:TextBox>
                    <label id="eClSearchLabel1" for="SearchTextBox">
                        Enter your search here</label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="BtnPnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="Searchbtndiv">
                    <asp:Button ID="SearchButton1" runat="server" ClientIDMode="Static" OnClick="GroupSearch_Click" OnClientClick="ShowUpdate()" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div class="AddtoGroupClassSearchDiv" id="ClassDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Your class</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="ClassButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="Div_FullWidth" id="ClsRepeaterContentDiv">
        <div id="RepeaterClassDiv" style="float: left; width: 100%; margin-top: 15px; display: block">
            <asp:UpdatePanel ID="ClassPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="ClassRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                           <div class="RepeaterContentDiv" title='<%# Eval("NameToolTip")%>'>
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="ClassCheckBoxes" runat="server" type="checkbox"
                                            class="HideItems" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="HideItems"></asp:Label>
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
                                    </div>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="AddtoGroupClassSearchDiv" id="GroupsDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Your group/s</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="GroupButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="Div_FullWidth" id="GrpRepeaterContentdiv">
        <div id="RepeaterGroupDiv" style="float: left; width: 100%; margin-top: 15px; display: block">
            <asp:UpdatePanel ID="GroupPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="GroupsRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                            <div class="RepeaterContentDiv" title='<%# Eval("NameToolTip")%>'>
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="GroupCheckBoxes" runat="server"  type="checkbox"
                                            class="HideItems" />
                                        <asp:Label ID="classid" runat="server"  Text='<%# Eval("GroupId") %>'
                                            CssClass="HideItems"></asp:Label>
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
                                    </div>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="AddtoGroupClassSearchDiv" id="OtherGroupsDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Other group/s</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="OtherGroupsButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="Div_FullWidth" id="OthrGrpRepeaterContentdiv">
        <div id="RepeaterOtherGroupDiv" style="float: left; width: 100%; margin-top: 15px;
            display: block">
            <asp:UpdatePanel ID="OtherGrpPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="OtherGrpRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                           	<div class="RepeaterContentDiv" title='<%# Eval("NameToolTip")%>'>
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="OtherGroupCheckBoxes" runat="server" type="checkbox"
                                            class="HideItems" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="HideItems"></asp:Label>
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
                                    </div>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="AddtoGroupClassSearchDiv" id="OtherClassDivHdr" runat="server" clientidmode="Static">
		<span class="student-class">Other classes</span> <span class="student-line"></span>
		<div id="KenduoComboBoxDiv" class="KenduoComboBoxClassDiv">
			<asp:Button ID="OtherClassButton" ClientIDMode="Static" CommandName="Expand" runat="server"
				Text="Hide" CssClass="btn btn-general" />
		</div>
	</div>
    <div class="Div_FullWidth" >
        <div id="RepeaterOtherClassDiv" style="float: left; width: 100%; margin-top: 15px;
            display: block">
            <asp:UpdatePanel ID="OtherClsPanel" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="OtherClsRepeater" runat="server" ClientIDMode="Static">
                        <ItemTemplate>
                          <div class="RepeaterContentDiv" title='<%# Eval("NameToolTip")%>'>
                                    <div class="RepeaterFstCol gchkbx">
                                        <input id="OtherClassCheckBoxes" runat="server" type="checkbox"
                                            class="HideItems" />
                                        <asp:Label ID="classid" runat="server" ClientIDMode="Static" Text='<%# Eval("GroupId") %>'
                                            CssClass="HideItems"></asp:Label>
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
                                    </div>
                                </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div style="width: 98%; float: left; text-align: center; margin-left: 1%;">
        <hr class="createHrs" />
    </div>
    <div class="BtmBtnsHdr">
        <div class="BtnsHdr">
            <div class="AddBtnHdr">
                <div class="ActiveAddButtonsHolder addBtns">
                    <asp:Button ID="AddTeacherToGroup" CssClass="AddButton" Text="ADD TO GROUP" ClientIDMode="Static"
                        OnClick="AddTeacherToGroup_Click" runat="server" />
                </div>
            </div>
            <div class="cancelBtnHdr">
                <div class="CancelBtnHolder cancelBtns">
                    <asp:Button ID="CancelButton" CssClass="CancelBtn cnBtn" Text="CANCEL" ClientIDMode="Static"
                        OnClick="CancelButton_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="SelectedGrpIds" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="SelectedStudIds" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="searchhdn" runat="server" ClientIDMode="Static" />
<input type="hidden" id="lastsearch" runat="server" clientidmode="Static" />
<input type="hidden" id="newsearch" runat="server" clientidmode="Static" />
<script type="text/javascript">
    var checkedcount = 0;
    var totalCheckbx = 0;
    function showtabs(tabid, val) {
        if (val == 1)
            $('#' + tabid.id).show();
        else
            $('#' + tabid.id).hide();
        ChangeStyle();
    }
    function ChangeStyle() {
        //Added By kalai
        var othergrp = true;
        if ($("#ClassDivHdr")[0].style.display == "none" && $("#GroupsDivHdr")[0].style.display == "none" && $("#OtherGroupsDivHdr")[0].style.display == "none" && $("#OtherClassDivHdr")[0].style.display == "none") {
            $("#GroupLeftBorderdiv")[0].style.display = "none";
            $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
        }
        else {
            $("#GroupLeftBorderdiv")[0].style.display = "block";
            $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "55px";
            $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "55px";
            $("#GroupsDivHdr")[0].style.marginTop = "-55px";
            $("#OtherGroupsDivHdr")[0].style.marginTop = "-51px";
            othergrp = false;
        }

        if ($("#GroupsDivHdr")[0].style.display == "none" && $("#OtherGroupsDivHdr")[0].style.display == "none") {
            $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "0px";
            $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
        }
        if ($("#ClassDivHdr")[0].style.display == "none" && $("#GroupsDivHdr")[0].style.display == "none") {
            othergrp = true;
        }
        if ($("#OtherGroupsDivHdr")[0].style.display == "none") {
            $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
            //$("#AllOtherGroupBtnDivID")[0].style.display = "none";
            //$("#allotherLeftLineDiv")[0].style.display = "none";
        }
        else {

            if (othergrp) {
                $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                $("#OtherGroupsDivHdr")[0].style.marginTop = "-80px";
            }
        }
        if ($("#OtherGroupsDivHdr")[0].style.display == "none" && $("#ClassDivHdr")[0].style.display == "none") {
            $("#GroupsDivHdr")[0].style.marginTop = "-80px";
        }

        //Added by kalai

        if ($("#ClassDivHdr")[0].style.display != "none" && $("#GroupsDivHdr")[0].style.display != "none" && $("#OtherGroupsDivHdr")[0].style.display != "none" && $("#OtherClassDivHdr")[0].style.display != "none") {
            $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
            $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "24px";
        }
        else if (($("#ClassDivHdr")[0].style.display != "none" || $("#GroupsDivHdr")[0].style.display != "none" || $("#OtherGroupsDivHdr")[0].style.display != "none") && $("#OtherClassDivHdr")[0].style.display != "none") {
            if ($("#ClassDivHdr")[0].style.display == "none") {
                if ($("#GroupsDivHdr")[0].style.display == "none") {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-44px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }
                else {
                    $("#GroupsDivHdr")[0].style.marginTop = "-44px";
                    $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "24px";
                    $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-51px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }

            }
            else {
                if ($("#GroupsDivHdr")[0].style.display == "none") {
                    if ($("#OtherGroupsDivHdr")[0].style.display == "none") {
                        $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                        $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                    }
                    else {
                        $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                        $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "20px";
                    }

                }
                else {
                    $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                    $("#GroupsDivHdr")[0].style.marginTop = "-20px";
                    $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }

            }
        }
        else if ($("#OtherClassDivHdr")[0].style.display != "none") {
            $("#OtherClassDivHdr")[0].style.marginTop = "-44px";
            $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
        }
        if ($("#OtherClassDivHdr")[0].style.display == "none") {
            if ($("#OtherGroupsDivHdr")[0].style.display != "none") {
                if ($("#GroupsDivHdr")[0].style.display != "none") {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-54px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                }
                else {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-72px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "40px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                }
            }
            else {
                if ($("#GroupsDivHdr")[0].style.display != "none") {
                    if ($("#ClassDivHdr")[0].style.display != "none") {
                        $("#GroupsDivHdr")[0].style.marginTop = "-51px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";

                    }
                    else {
                        $("#GroupsDivHdr")[0].style.marginTop = "-72px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                    }
                }
            }
        }

    }
    function Check(chk, unchk) {
        jQuery('#' + chk).parent().children("input").attr('checked', 'checked');
        jQuery('#' + chk).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
        jQuery('#' + chk).hide();
        jQuery('#' + unchk).show();
        checkedcount++;
        CheckAllCheck(checkedcount);
    }
    function UnCheck(chk, unchk) {
        jQuery('#' + unchk).parent().children("input").removeAttr('checked');
        jQuery('#' + chk).parent().parent()[0].className = "RepeaterContentScndDiv";
        jQuery('#' + chk).show();
        jQuery('#' + unchk).hide();
        if (checkedcount > 0)
            checkedcount--;
        CheckAllCheck(checkedcount);
    }
    function CheckAllCheck(checkedCount) {

        var totalCheckbxCnt = $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length + $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length + $("#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]").length + $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').length;
        if (checkedCount == totalCheckbxCnt) {
            jQuery("#CheckAllDiv").hide();
            jQuery("#UnCheckAllDiv").show();
        }
        else {
            jQuery("#CheckAllDiv").show();
            jQuery("#UnCheckAllDiv").hide();
        }
    }
    function CheckAll() {
        jQuery("#CheckAllDiv").hide();
        jQuery("#UnCheckAllDiv").show();
        $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").attr('checked', 'checked');
        $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").attr('checked', 'checked');
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').attr('checked', 'checked');
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').attr('checked', 'checked');
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().next().show();
        for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "rowclick RepeaterContentScndDiv";
        }
        for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "rowclick RepeaterContentScndDiv";
        }
        for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "rowclick RepeaterContentScndDiv";
        }
        for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "rowclick RepeaterContentScndDiv";
        }
        checkedcount = totalCheckbx;
    }
    function UnCheckAll() {
        jQuery("#CheckAllDiv").show();
        jQuery("#UnCheckAllDiv").hide();
        $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").removeAttr('checked');
        $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").removeAttr('checked');
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').removeAttr('checked');
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').removeAttr('checked');
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().show();
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().show();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().show();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().show();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().next().hide()
        for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "RepeaterContentScndDiv";
        }
        for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "RepeaterContentScndDiv";
        }

        for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "RepeaterContentScndDiv";
        }
        for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent()[i].className = "RepeaterContentScndDiv";
        }
        checkedcount = 0;
    }

    function Remove(obj) {

        var SelectedValues = $("#SelectedValueTextBox").val().trim().split(',');
        document.getElementById("SelectedValueTextBox").value = " ";
        for (var i = 0; i < SelectedValues.length; i++) {
            if (SelectedValues[i].trim() != obj.parentNode.children[1].innerHTML && SelectedValues[i].trim() != '') {
                $("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + SelectedValues[i].trim() + ",")
            }
        }
        obj.parentNode.parentNode.removeChild(obj.parentNode);

    }

    jQuery(document).ready(function () {
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery(".ECollLeftModule").css('margin-top', '-310px');
        }
        else {
            jQuery(".ECollLeftModule").addClass('CreateTabECollLeftModule');
        }
        totalCheckbx = $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length + $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length + $("#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]").length+$('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').length;
        $('#FinishAddtoGrpBtn').removeClass('srtbtnshide');
        $('#eCollection_Menu_MidHolder').addClass('srtbtnshide');
        $('#BlkUpldBtn').addClass('srtbtnshide');
        $('#BcktoCreateGrpBtn').addClass('srtbtnshide');
        jQuery("#TeachersTabHolder").addClass('CreateTabSelected');


        if ($('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type="checkbox"]').length > 0) {
            jQuery('#borderleftdiv').addClass('borderleftdiv');
        }

        $("#KenduoComboBoxDiv").click(function () {
            if ($("#RepeaterClassDiv")[0].style.display == "block") {
                $("#ClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterClassDiv").stop().fadeTo(300, 0);
                $("#RepeaterClassDiv").css("display", "none");
            }
            else {
                $("#ClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterClassDiv").stop().fadeTo(300, 1);
                $("#RepeaterClassDiv").css("display", "block");
            }

        });
        $("#kenduoComboGrpDiv").click(function () {
            if ($("#RepeaterGroupDiv")[0].style.display == "block") {
                $("#GroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterGroupDiv").stop().fadeTo(300, 0);
                $("#RepeaterGroupDiv").css("display", "none");
                $("#grpBorders").css("display", "none");
                jQuery('#borderleftdiv').removeClass('borderleftdiv');
            }
            else {
                $("#GroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterGroupDiv").stop().fadeTo(300, 1);
                $("#RepeaterGroupDiv").css("display", "block");
                $("#grpBorders").css("display", "block");
                jQuery('#borderleftdiv').addClass('borderleftdiv');
            }
            //$("#RepeaterClassDiv").slideToggle(); 
        });

        $("#OtherGroupsDiv").click(function () {
            if ($("#RepeaterOtherGroupDiv")[0].style.display == "block") {
                $("#OtherGroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterOtherGroupDiv").stop().fadeTo(300, 0);
                $("#RepeaterOtherGroupDiv").css("display", "none");
            }
            else {
                $("#OtherGroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterOtherGroupDiv").stop().fadeTo(300, 1);
                $("#RepeaterOtherGroupDiv").css("display", "block");
            }
            //$("#RepeaterClassDiv").slideToggle(); 
        });

        $("#OtherClassesDiv").click(function () {
            if ($("#RepeaterOtherClassDiv")[0].style.display == "block") {
                $("#OtherClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterOtherClassDiv").stop().fadeTo(300, 0);
                $("#RepeaterOtherClassDiv").css("display", "none");
            }
            else {
                $("#OtherClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterOtherClassDiv").stop().fadeTo(300, 1);
                $("#RepeaterOtherClassDiv").css("display", "block");
            }
            //$("#RepeaterClassDiv").slideToggle();
        });
        var checkallFlag = true;
        var popupFlag = false;

        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#SearchTextBox').val('Enter your search here ...');
            $("#SearchTextBox").focus(function () {
                if ($(this).val() == 'Enter your search here ...') {
                    $(this).val("");
                }

            });
            $("#SearchTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#SearchTextBox').val('Enter your search here ...');
                }

            });
        }

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');  && $(this).val() != this.title
                //return false;
            }

            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0) {

                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
            }
        });

        $('#SearchTextBox').keyup(function (e) {
            $('#SearchTextBox').focus();
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                }
            }
        });


        var data = new kendo.data.DataSource({
            transport: {
                read: {
                    url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=groupsclasssearch'),
                    dataType: "json"
                }
            }
        });

        //create AutoComplete UI component
        $("#SearchTextBox").kendoAutoComplete({
            dataTextField: "Name",
            dataSource: data,
            filter: "contains",
            placeholder: "Enter your search here ...",
            separator: ", "
        });

        LoadClick();

        jQuery('#AddTeacherToGroup').click(function () {
            var selGroups = '';
            var selStuds = '';
            $("#MsgDiv").removeAttr('class');
            $('#ClassRepeaterDiv #ClassRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val();
            });
            $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val();
            });
            $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val();
            });
            $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val();
            });
            jQuery('#SelectedGrpIds').val(selGroups);
            $('#SelectedTeacherList li').each(function () {
                selStuds = selStuds + ',' + jQuery(this).children().next().html();
            });
            jQuery('#SelectedStudIds').val(selStuds);
            $("#MsgDiv").hide();
            if (selGroups == '') {
                $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
                // $("#MsgDiv").text('Please select atleast one group or class.').show();
                GetMessage('GROUPS_SELECTED');
                return false;
            }
            return true;
        });

        jQuery("#dialog-message").dialog({
            Modal: true, autoOpen: false,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });

        jQuery("#TeachersTabHolder").css("position", "relative");
        jQuery("#TeachersTabHolder").css("z-index", "100000");
        //        jQuery("#ClassSearchTextBox").kendoComboBox({
        //            placeholder: "Your class",
        //            dataTextField: "Name",
        //            dataValueField: "GroupId",
        //            filter: "contains",
        //            dataSource: {
        //                transport: {
        //                    read: {
        //                        url: GetFile('/DesktopModules/eCollection_Teachers/Handlers/eCollectionHandler.ashx?autocomplete=subclasses'),
        //                        type: "GET",
        //                        contentType: "application/json; charset=utf-8",
        //                        dataType: "json",
        //                        serverFiltering: true,
        //                        serverPaging: true,
        //                        pageSize: 20
        //                    }
        //                }
        //            }
        //        });


        $('#ClassRepeaterDiv #ClassRepeaterContentDiv #ClassRepeaterSubmit input[type=submit]').click(function () {

            if ($("#SelectedTeacherList li").length >= 1) {
                for (var i = 0; i < $("#SelectedTeacherList")[0].childNodes.length; i++) {
                    var tis = $("#SelectedTeacherList")[0].childNodes[i];
                    if (tis.nodeName != "#text") {
                        if ($("#SelectedTeacherList")[0].childNodes[i].childNodes[1].innerHTML.trim() == this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML.trim()) {
                            document.getElementById("StudentNamespan").innerHTML = this.parentNode.parentNode.parentNode.children[1].children[0].innerHTML.trim();
                            $("#dialog").css({ 'display': 'block' });
                            $("#dialog").dialog("open");
                            return false;
                        }
                    }
                }
            }
            $("#SelectedTeacherList").append("<li class='SelectedGroupItem'><span>" +
            this.parentNode.parentNode.parentNode.children[2].children[0].innerHTML +
            "</span><span style='display:none'>" +
            this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML +
            "</span><a onclick='RemoveGroup(this);'>x</a></li>");

            $("#SelectedValueGroupTextBox").val($("#SelectedValueGroupTextBox").val() +
            this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML + ",");
            //}
            return false;
        });


        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv #GroupRepeaterSubmit input[type=submit]').click(function () {

            if ($("#SelectedTeacherList li").length >= 1) {
                for (var i = 0; i < $("#SelectedTeacherList")[0].childNodes.length; i++) {
                    var tis = $("#SelectedTeacherList")[0].childNodes[i];
                    if (tis.nodeName != "#text") {
                        if ($("#SelectedTeacherList")[0].childNodes[i].childNodes[1].innerHTML.trim() == this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML.trim()) {
                            document.getElementById("StudentNamespan").innerHTML = this.parentNode.parentNode.parentNode.children[1].children[0].innerHTML.trim();
                            $("#dialog").css({ 'display': 'none' });
                            //$("#dialog").dialog("open");
                            return false;
                        }
                    }
                }
            }
            $("#SelectedTeacherList").append("<li class='SelectedGroupItem'><span>" +
            this.parentNode.parentNode.parentNode.children[2].children[0].innerHTML +
            "</span><span style='display:none'>" +
            this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML +
            "</span><a onclick='RemoveGroup(this);'>x</a></li>");

            $("#SelectedValueGroupTextBox").val($("#SelectedValueGroupTextBox").val() +
            this.parentNode.parentNode.parentNode.children[0].children[1].innerHTML + ",");
            //}
            return false;
        });
    });
    function LoadClick() {
        $("#SearchButton").click(function () {
            $('#searchhdn').val('search');
            $('#lastsearch').val($('#newsearch').val());
        });
    }
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    function ShowHideGrpDivs() {
        if ($('#<%=SecondDiv.ClientID %>').is(':visible')) {
            $('#SelectAllDiv').css('display', 'block');
            $('#SortDiv').css('display', 'block');
        }
        else {
            $('#SelectAllDiv').css('display', 'none');
            $('#SortDiv').css('display', 'none');
        }
    }
</script>
