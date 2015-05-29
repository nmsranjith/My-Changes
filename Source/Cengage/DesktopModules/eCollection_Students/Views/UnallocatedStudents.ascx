<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnallocatedStudents.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.UnallocatedStudents" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="UnallocatedStudentsPager.ascx" TagName="Pager" TagPrefix="PG" %>
	
<style type="text/css">
    .Licpopup
    {
        width: 30px;
        height: 30px;
        margin-top: -45px;
        z-index: 100;
        float: right !important;
    margin-left: 650px;
        cursor: pointer;
		position: absolute;
    }
    .k-state-selected.k-state-focused
    {
        background-color: #707070 !important;
    }
    .k-window-action
    {
        background-image: url('Portals/0/images/close.png') !important;
        opacity: 1 !important;
        height: 26px !important;
        width: 26px !important;
        z-index: 1003 !important;
        margin-right: -32px !important;
        margin-top: 12px !important;
        border: none !important;
        margin-left: -10px;
    }
    .k-icon
    {
        background-image: url('Portals/0/images/sprite.png') !important;
    }
    .eCollectionTxtBxSaf
    {
        height: 21px !important;
    }
    .button
    {
        font-size: 8.2pt;
        background-color: rgb(244,244,244);
        font-weight: normal;
        text-decoration: none;
        border-color: lightgray;
        border-width: 1px;
        border-style: solid;
        text-align: center;
        font-family: Raleway-regular,Raleway,Arial;
        color: #707070;
        display: inline-block;
        width: 43%;
        margin-right: 7px;
        padding-top: 5px;
        padding-bottom: 5px;
        cursor: pointer;
    }
</style>
<div id="MsgDiv" style="margin-top: 17px;">
	<Msg:Message ID="Messages" runat="server">
	</Msg:Message>
</div>
<div>
    <div class="std-menu">
        <div id="ManualSwitch" class="menu manual" onclick="ManualCreateClick()">
            Manual</div>
        <div id="UploadSwitch" class="menu upload bdr-left" onclick="UploadStudentsClick()">
            Upload</div>
        <div id="SearchSwitch" class="menu search bdr-left" onclick="SearchStudentsClick()">
            Search</div>
		<div class="menu empty-menu bdr-left" ></div>
    </div>
    <div id="CreateProfileDiv" class="HideItems">      
        <div id="MessageOuterDiv" runat="server" style="width: 100%; position: static; display: none;">
            <div class="bubble1">
                <asp:Label ID="Message" runat="server" Text="" />
            </div>
        </div>
        <div id="FirstDiv" style="width: 100%; float: left; height: 580px; font-size: 10pt;">
            <div style="width: 94.3%; float: left; margin-left: 20px;">
                <div id="MessageDiv" class="CreateStudentProfile_MessageDiv" runat="server" clientidmode="Static"
                    style="display: none;">
                    <div class="CreateStudentProfile_MessageInnerDiv" style="display: none;">
                        <div style="width: 63%; float: left;">
                            <span id="AddedName" runat="server" class="blkAfterName"></span>
                        </div>
                        <div style="float: left; width: 63%;">
                            <span id="StudentsCount" runat="server" class="blkAfterNum"></span><span class="blkAfterNum">
                                ] student/s</span>
                        </div>
                        <div class="blkPrnt">
                            <asp:Button ID="PrintStudentbtn1" runat="server" CssClass="button" OnClientClick="window.document.forms[0].target='_blank';"
                               Text="PRINT" OnClick="PrintStudentCards" />
                            <div class="button" style="padding-top: 4px; padding-bottom: 4px;">
                                <a id="SeeAllLink" runat="server" style="text-decoration: none!important; color: #707070!important;">
                                    SEE ALL</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="CreateForm" class="CreateStudentProfile_TopDiv" runat="server" clientidmode="Static">
                    <div style="width: 98%; height: 57px; padding-left: 15px; margin-top: 18px; float: left;">
                        <div style="width: 49.9%; float: left;">
                            <h5>
                                First name:</h5>
                            <div class="eCollectionEditDiv">
                                <div class="eCollectionTbxHolder">
                                    <asp:TextBox ID="FirstNameTextBox" runat="server" MaxLength="60" autocomplete="off"
                                        CssClass="eCollectionTextBox" onkeydown="return event.keyCode != 13 && event.which != 13;"
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
                                        CssClass="eCollectionTextBox" onkeydown="return event.keyCode != 13 && event.which != 13;"
                                        ClientIDMode="Static"></asp:TextBox>
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
                                    <input type="text" id="DateofBirthTextBox" placeholder="Date of birth (dd/mm/yyyy)"
                                        autocomplete="off" runat="server" clientidmode="Static" class="eCollectionTextBox"
                                        style="font-style: italic;" />
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
                                            float: left; position: absolute; width: 130px; display: block;">
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
                                            float: left; position: absolute; width: 130px;">
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
                    <div style="padding-left: 15px; height: 66px; padding-top: 20px; width: 100%; float: left;">
                        <div style="width: 100%; height: 37px; float: left;">
                            <div style="width: 73%; float: left;">
                                <h5>
                                    PM reading level:
                                </h5>
                            </div>
                            <div style="width: 73%; float: left;">
                                <div class="Div_FullWidth">
                                    <select id="PMReadingLevel" runat="server" clientidmode="Static" style="height: 31px;
                                        float: left; position: absolute; width: 160px;">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                        <option value="11">11</option>
                                        <option value="12">12</option>
                                        <option value="13">13</option>
                                        <option value="14">14</option>
                                        <option value="15">15</option>
                                        <option value="16">16</option>
                                        <option value="17">17</option>
                                        <option value="18">18</option>
                                        <option value="19">19</option>
                                        <option value="20">20</option>
                                        <option value="21">21</option>
                                        <option value="22">22</option>
                                        <option value="23">23</option>
                                        <option value="24">24</option>
                                    </select>
                                </div>
                                <div style="float: right; width: 63%;">
                                    <div style="width: 52%; float: left;">
                                        <div id="ReadingRecoveryCheckBox" class="ReadingRecovery" onclick="ReadingRecovery();">
                                            <div style="float: left; width: 19%;">
                                                <img id="RRChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>' alt=""
                                                    style="display: none;" />
                                                <img id="RRUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                                                    alt="" />
                                            </div>
                                            <div style="float: right; width: 81%; margin-top: 6px;">
                                                READING RECOVERY</div>
                                        </div>
                                    </div>
                                    <div style="width: 41%; float: right;">
                                        <div id="ESLCheckBox" class="ESL" onclick="ESL();">
                                            <img id="EslChk" src='<%=Page.ResolveUrl("Portals/0/Images/tick_student.png")%>'
                                                alt="" style="display: none;" />
                                            <img id="EslUnChk" src='<%=Page.ResolveUrl("Portals/0/Images/circle_white.png")%>'
                                                alt="" />
                                            <div style="float: right; width: 60%; margin-top: 6px;">
                                                ESL</div>
                                        </div>
                                    </div>
                                    <asp:CheckBox ID="ReadingRecoveryCheck" runat="server" Text="N" ClientIDMode="Static"
                                        Style="display: none;" />
                                    <asp:CheckBox ID="ESLCheck" runat="server" Text="N" ClientIDMode="Static" Style="display: none;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="width: 98.5%; height: 68px; padding-left: 15px; margin-top: 18px; float: left;">
                        <div style="width: 29%; float: left;">
                            <h5>
                                Username:
                            </h5>
                            <div class="eCollectionEditDiv">
                                <div class="eCollectionTbxHolder1">
                                    <asp:TextBox ID="StudentUserNameTextBox" runat="server" autocomplete="off" MaxLength="60"
                                        CssClass="eCollectionTextBox" onkeydown="return event.keyCode != 13 && event.which != 13;"
                                        ClientIDMode="Static">
                                    </asp:TextBox>
                                </div>
                                <div class="eCollectionEditSpan">
                                    <span>*</span></div>
                            </div>
                        </div>
                        <div style="width: 29%; float: left;">
                            <h5>
                                Password:
                            </h5>
                            <div class="eCollectionEditDiv">
                                <div class="eCollectionTbxHolder1">
                                    <asp:TextBox ID="PasswordTextBox" runat="server" MaxLength="60" CssClass="eCollectionTextBox PwdTxtBox"
                                        TextMode="Password" onkeydown="return event.keyCode != 13 && event.which != 13;"
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
                                    CssClass="eCollectionTextBox" onkeydown="return event.keyCode != 13 && event.which != 13;"
                                    ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; padding-left: 15px; padding-top: 15px; float: left;">
                        <h5>
                            Books available on the student's bookshelf:
                        </h5>
                        <div class="eCollectionEditLbl">
                            <%-- style="margin-top: 30px">--%>
                            <rl:rlslider id="ReadingLevelSlider" runat="server">
                    </rl:rlslider>
                            <asp:HiddenField ID="SliderValue" ClientIDMode="Static" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; text-align: center;">
                <hr class="createHrs" />
            </div>
            <div class="Div_FullWidth">
                <div class="BtnsHdr">
                    <div class="AddBtnHdr">
                        <div class="addBtns">
                            <asp:Button ID="AddStudentProfileButton" CssClass="btn btn-affermative" Text="ADD STUDENT"
                                ClientIDMode="Static" runat="server"  OnClick="AddStudentProfile_Click"/> 
                        </div>
                    </div>
                    <div class="cancelBtnHdr">
                        <div class="cancelBtns">
                            <asp:Button ID="CancelButton" CssClass="btn btn-cancel" Text="CANCEL" ClientIDMode="Static"
                                 runat="server" OnClick="CancelButton_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="RRHdn" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="ESLHdn" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="UserCount" runat="server" ClientIDMode="Static" />
        <div id="LicenceExhaust" style="display: none; background: white !important;">
            <div style="background-image: url('../Portals/0/images/topband.png'); background-color: White;
                height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
                <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
            </div>
            <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
                box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
                height: 87%;">
                <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                    box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                    -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
					<span id="LicExhaustMsg" Style="font-family: Raleway-regular, Arial, sans-serif;
                        font-size: 10pt; color: #707070; padding: 30px; float: left;"></span>
                    <asp:Label ID="TeacherTxt" runat="server" Visible="false" Style="font-family: Raleway-regular, Arial, sans-serif;
                        font-size: 10pt; color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please contact your Administrator</asp:Label>
                    <asp:Label ID="AdminText" runat="server" Visible="false" Style="font-family: Raleway-regular, Arial, sans-serif;
                        font-size: 10pt; color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please buy/update the subscription. Please visit <a href="/pmecollection">BUY</a></asp:Label>
                </div>
                <div style="width: 92%;">
                    <input type="button" id="OkButton" value="Ok" class="popupokbtn" />
                </div>
            </div>
        </div>
        <div id="UserNameExist" title="Alert message!" style="display: none; background: white !important;">
            <div style="background-image: url('../Portals/0/images/topband.png'); background-color: White;
                height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
                <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
            </div>
            <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
                box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
                height: 87%;">
                <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                    box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                    -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                    <span id="MessageLiteral" style="font-family: Raleway-regular, Arial, sans-serif;
                        font-size: 10pt; color: #707070; padding: 23px; float: left;">User name already
                        exists, Do you want to proceed with system defined user name?</span>
                </div>
                <div style="width: 92%;">
                    <input type="button" id="YesBtn" style="margin-left: 192px;" value="Yes" class="popupokbtn" />
                    <input type="button" id="NoBtn" style="margin-left: 18px;" value="No" class="popupokbtn" />
                </div>
            </div>
        </div>
    </div>
    <div id="UploadProfilesDiv" class="HideItems">
        <div id="image_preview" runat="server" clientidmode="Static" class="dnnLoading" style="margin-left: 17%;
            width: 50%; height: 19%; text-align: center; padding-top: 25px; margin-top: 18%;
            border: 1px solid #707070; display: none;">
        </div>
        <%--<div id="Div1">
            <Msg:Message ID="Messages" runat="server">
            </Msg:Message>
        </div>--%>
        <div style="float: left; width: 94%; margin: 15px 0px -15px 20px">
            <div id="Div2" class="CreateStudentProfile_MessageDiv" style="display: none;">
                <div class="CreateStudentProfile_MessageInnerDiv">
                    <div style="width: 63%; float: left;">
                        <span id="Span1" runat="server" class="blkAfterNum blktext"></span>
                    </div>
                    <div style="width: 63%; float: left;">
                        <span id="TotalAddedStudents" runat="server" class="blkAfterNum"></span><span class="blkAfterNum">
                            ] student/s </span>
                    </div>
                    <div class="blkPrnt">
                        <asp:Button ID="PrintStudentCardsButton1" runat="server" CssClass="button" OnClientClick="window.document.forms[0].target='_blank';"
                            Text="PRINT"  OnClick="PrintStudentCards" />
                        <asp:HyperLink ID="HyperLink1" runat="server" Text="SEE ALL" ClientIDMode="Static"></asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
        <div id="BulkUpload_MainDiv" class="BulkUpload_MainDiv">
            <div class="BulkUpload_SecondDiv">
                <div class="BulkUpload_UploadHdr">
                    <h4 class="BulkUpload_Uploadlabel">
                        Bulk upload</h4>
                </div>
                <div class="BulkUpload_DownLoadFormatDiv">
                    <h4 class="BulkUpload_HeaderColor">
                        How to upload students:</h4>
                    <p>
                        a. Download the Excel file below.</p>
                    <p>
                        b. Complete all mandatory fields for all students.</p>
                    <p>
                        c. Complete non-mandatory fields if required for your subscription.</p>
                    <p>
                        d. Save the file on your system.</p>
                    <p>
                        e. Return to the page and upload the file using the browse button below.</p>
                    <div class="DLExcelFormatBtnHolder">
                        <asp:Button ID="DownLoadExcelButton" CssClass="DownLoadExcelFormatBtn" runat="server"
                            Text="DOWNLOAD EXCEL FILE"  OnClick="DownLoadExcelButton_Click"/> 
                    </div>
                </div>
                <div class="blkHrHdr">
                    <hr class="createHrs" />
                </div>
                <div class="BulkUpload_UploadDiv">
                    <h5>
                        Bulk upload</h5>
                    <div class="BulkUpload_BrowseDiv">
						<div class="BulkUpload_label bdr-left bdr-top bdr-btmb">
							<span class="bk-label">Choose File</span>
						</div>
                        <div class="BulkUpload_TxtBxHolder">
                            <input id="txtAttachment" type="text" name="txtAttachment" class="BulkUpload_TxtBx"
                                maxlength="250" onclick='return false' ondblclick="BrowseFile();" onkeydown="return event.keyCode != 13 && event.which != 13;"
                                runat="server" clientidmode="Static" />
                        </div>
                        <div class="BulkUpload_BwsBtnHolder" style="position: relative;">
                            <input type="button" id="AttachFileButton" onclick="BrowseFile();" value="BROWSE"
                                class="btn btn-general" />
                        </div>
                        <asp:FileUpload ID="AttachAFile" runat="server" onchange="SetValue(this.value)" ClientIDMode="Static"
                            Style="width: 15.5%; margin-top: -35px; float: left; opacity: 0; filter: alpha(opacity=0);
                            background-color: transparent; color: transparent; height: 32px; margin-left: 7px;" />
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 100%; float: left; text-align: center;">
            <hr class="createHrs" />
        </div>
        <div class="Div_FullWidth">
            <div class="BtnsHdr">
                <div class="AddBtnHdr">
                    <div class="addBtns">
                        <asp:Button ID="UploadButton" CssClass="btn btn-affermative" Text="UPLOAD" ClientIDMode="Static"
                          runat="server"  OnClick="UploadFileImageButton_Click" /> 
                        <input type="reset" id="Reset2" style="display: none;" />
                    </div>
                </div>
                <div class="cancelBtnHdr">
                    <div class="cancelBtns">
                        <asp:Button ID="Button1" CssClass="btn btn-cancel" Text="CANCEL" ClientIDMode="Static"
                            runat="server" OnClick="CancelButton_Click"/> 
                    </div>
                </div>
            </div>
        </div>
        <asp:GridView ID="tab" runat="server">
        </asp:GridView>
        <div id="dialog-message1" style="display: none; background: white !important;">
            <div style="background-image: url('Portals/0/images/topband.png'); background-color: White;
                height: 110px; margin-top: -9px; width: 102%; margin-left: -7px;">
                <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
            </div>
            <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
                box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
                height: 87%;">
                <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
                    box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
                    -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
                    <literal style="font-family: Raleway, Arial, sans-serif; font-size: 10pt; color: #707070;
                        padding: 30px; float: left;">Licences for the subscription are exhausted, Please contact your Administrator</literal>
                </div>
                <div style="width: 92%;">
                    <input type="button" id="Button2" value="Ok" class="popupokbtn" />
                </div>
            </div>
        </div>
    </div>
    <div id="UnallocatedDiv" class="HideItems">
        
		<div class="commontextdiv">
			<label class="lblStyle" id="SrchDiv" for="UnallocatedStudentSearch">
				Search for past students:</label>
			<div class="inputStyle ser-prisecsearchdiv UnallocatedSearchTopDiv">
				<input name="SearchStudents" clientidmode="Static" runat="server" type="text" id="UnallocatedStudentSearch"
					class="k-input ser-prisearchbox UnallocatedSrhStudents" value="Search by name or username"
					onblur="if(this.value=='') {this.value='Search by name or username';this.style.fontStyle='normal'} else {this.style.fontStyle='normal'};"
					onfocus="if(this.value=='Search by name or username') this.value='';this.style.fontStyle='normal';"
					data-role="autocomplete" autocomplete="off"  onkeypress="javascript:clickfocusevent(event);"  />				
			</div>
			<input type="button" id="UnallocatedStudentSearchBtn" class="Srchbtntop" onClick="SearchStudentsClick()" />
		</div>
        <div>
            <asp:Repeater ID="UnAllocatedStudentsRptr" runat="server">
                <ItemTemplate>
                    <div class="full-name">
                        <span class="ico-person"></span> <span class="user-name"><%# Eval("USER_LOGIN_NAME") %></span>
                        <input type="button" value="ADD" class="addbtn" onclick='UpdateStudentAllocation(this,<%# Eval("USER_SK") %>)' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>            
        </div>
        <div id="UnallocatedPagerDiv" runat="server" visible="false">
        <PG:Pager ID="UnallocatedPager" runat="server"></PG:Pager>
        </div>
		<div id="NoSearchResult" class="searchnoresult" runat="server" visible="false">No students found</div>
    </div>
</div>
<span id="AfterSwitchUrl" runat="server" clientidmode="Static" class="HideItems"></span>
<script type="text/javascript">

    jQuery(function () {

        $('#dialog-message').parent().children().first().remove();
        $('<a class="popupClosebg Licpopup" href="students.aspx"></a>').prependTo($('#dialog-message').parent('.k-window'));
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery(".ECollLeftModule").css('margin-top', '-345px');
        }

        $('#CreatePageBtn').removeClass('srtbtnshide');
        $('#CreatePageFinishBtn').removeClass('srtbtnshide');

        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#DateofBirthTextBox').val('Date of birth (dd/mm/yyyy)');
            $("#DateofBirthTextBox").focus(function () {
                if ($(this).val() == 'Date of birth (dd/mm/yyyy)') {
                    $(this).val("");
                }
            });
            $("#DateofBirthTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#DateofBirthTextBox').val('Date of birth (dd/mm/yyyy)');
                }
            });
        }
        jQuery('#eCollectionContent').addClass('CreatePageContent');
        jQuery('#eCollectionMenu').addClass('CreatePageMenu');


        jQuery('#HeaderBtn').css('display', 'none');

        jQuery('#GradeDropDown').kendoDropDownList({ animation: false });
        jQuery('#GenderDropDown').kendoDropDownList({ animation: false });
        jQuery('#PMReadingLevel').kendoDropDownList({ animation: false });


        jQuery('#FirstNameTextBox').focus();

        jQuery('#FirstNameTextBox').change(function () {
            jQuery('#StudentUserNameTextBox').val(jQuery('#FirstNameTextBox').val().toLowerCase().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '') + jQuery('#LastNameTextBox').val().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '').substr(0, 1).toLowerCase());
            FetchUserName(jQuery('#StudentUserNameTextBox').val());
        });

        jQuery('#LastNameTextBox').change(function () {
            jQuery('#StudentUserNameTextBox').val(jQuery('#FirstNameTextBox').val().toLowerCase().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '') + jQuery('#LastNameTextBox').val().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '').substr(0, 1).toLowerCase());
            FetchUserName(jQuery('#StudentUserNameTextBox').val());
        });

        jQuery('#CreateProfileDiv input').keyup(function (e) {
            if (e.keyCode == 13) {
                var ret = jQuery('#AddStudentProfileButton').click();
                return ret;
            }
        });

        $("#YesBtn").click(function () {
            kwindow.data("kendoWindow").close();
            jQuery('#StudentUserNameTextBox').val(jQuery('#FirstNameTextBox').val().toLowerCase().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '') + jQuery('#LastNameTextBox').val().replace(/\s/g, "").replace(/[^a-z0-9]/gi, '').substr(0, 1).toLowerCase() + jQuery('#UserCount').val());
            return false;
        });
        $("#NoBtn").click(function () {
            kwindow.data("kendoWindow").close();
            return false;
        });

        jQuery('#AddStudentProfileButton').click(
        function () {
            $("#MsgDiv").removeAttr('class');
            $("#MsgDiv").text('');
            jQuery('#SliderValue').val(jQuery('#amount').val());
            var err = 0;
            if (ValidateText('FirstNameTextBox')) {
                jQuery('#FirstNameTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
            }
            else {
                err++;
                jQuery('#FirstNameTextBox').focus();
                GetMessage("FIRSTNAME_MANDATORY");
            }
            if (ValidateText('LastNameTextBox')) {
                jQuery('#LastNameTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
            }
            else {
                if (err == 0) {
                    err++;
                    jQuery('#LastNameTextBox').focus();
                    GetMessage("LASTNAME_MANDATORY");
                }
            }

            if (ValidateText('StudentUserNameTextBox')) {
                jQuery('#StudentUserNameTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
            }
            else {
                if (err == 0) {
                    err++;
                    jQuery('#StudentUserNameTextBox').focus();
                    GetMessage("USERNAME_MANDATORY");
                }
            }

            if (ValidateText('PasswordTextBox')) {
                jQuery('#PasswordTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
            }
            else {
                if (err == 0) {
                    err++;
                    jQuery('#PasswordTextBox').focus();
                    GetMessage("PASSWORD_MANDATORY");
                }
            }

            if (err == 0) {
                if (checkForName('FirstNameTextBox', 'VALIDATE_FIRSTNAME')) {
                    if (checkForName('LastNameTextBox', 'VALIDATE_LASTNAME')) {
                        if (checkForUserName('StudentUserNameTextBox', 'VALIDATE_USERNAME')) {
                            if (checkPassword('PasswordTextBox', 'VALIDATE_PASSWORD')) {
                                $("#MsgDiv").hide();
                                if (isValidEmailAddress(jQuery('#EmailTextBox').val())) {
                                    jQuery('#EmailTextBox').parent().css('border', '1px solid lightgrey');
                                    jQuery('#RRHdn').val(jQuery("label[for=ReadingRecoveryCheck]").html());
                                    jQuery('#ESLHdn').val(jQuery("label[for=ESLCheck]").html());
                                    if (isValidDate(jQuery('#DateofBirthTextBox').val())) {
                                        return true;
                                    }
                                    else {
                                        $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript").css('height', 'auto');
                                        return false;
                                    }
                                }
                                else {
                                    $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript").css('height', 'auto');

                                    GetMessage("VALIDATE_EMAIL");
                                    jQuery('#EmailTextBox').parent().css('border', '1px solid #ED175B');
                                    return false;
                                }
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;


            }
            return false;
        }
        );

    });

    function FetchUserName(username) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=fetchusername&names=' + username),
            dataType: "json",
            success: function (value) {
                jQuery('#StudentUserNameTextBox').val(value);
            }
        });
    }
    function CreateProfilesSuccess() {
        $("#MsgDiv").addClass("dnnFormMessage dnnFormSuccess dnnMsgScript").css({ 'height': '40px', 'margin-top': '17px' });
        jQuery('#MsgDiv').html(jQuery('.CreateStudentProfile_MessageInnerDiv').html()).show()
        disp_clear();
    }

    function disp_clear() {
        jQuery('#CreateForm input').val('');
        jQuery('select').find('option:first-child').attr('selected', true);
        window.scroll(0, 0);
    }

    function SubscriptionExpired() {
        $("#LicenceExhaust").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        kwindow = $("#LicenceExhaust");
        if (!kwindow.data("kendoWindow")) {
            kwindow.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            kwindow.data("kendoWindow").center();
        }
        kwindow.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').click(function () { window.location.href = "students.aspx"; return; });
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().addClass("popupClosebg");
            return false;
        });
    }

    function UserNameExists() {
        $("#UserNameExist").css({ 'display': 'block' });
        $('.k-window-actions.k-header').css('cursor', 'pointer');
        kwindow = $("#UserNameExist");
        if (!kwindow.data("kendoWindow")) {
            kwindow.kendoWindow({
                width: "665px",
                height: "300px",
                modal: true,
                draggable: false
            });
            kwindow.data("kendoWindow").center();
        }
        kwindow.data("kendoWindow").open();
        $(".k-icon.k-i-close").hide();
        $('a.k-window-action.k-link').mouseover(function () {
            $('a.k-window-action.k-link').parent().addClass("popupClosebg");
            return false;
        });
    }
    var addFlag = false;

    /****************************************************************************************************/
    /************************************  New Create design ********************************************/
    /****************************************************************************************************/

    function SearchStudentsClick() {
        if (GetQueryStringParams("pagename") == "unallocated") {
            if (decodeURIComponent($('#UnallocatedStudentSearch').val().trim()) != 'Search by name or username')
                window.location.href = $('#AfterSwitchUrl').text() + "?pagename=unallocated&unq=" + $('#UnallocatedStudentSearch').val().replace(/^,|,$/g, '');
            else
                window.location.href = $('#AfterSwitchUrl').text() + "?pagename=unallocated";
        }
        else {
            var unq = GetQueryStringParams("unq");
            if (unq != undefined && unq != '')
                window.location.href = $('#AfterSwitchUrl').text() + "?pagename=unallocated&unq=" + unq.replace(/^,|,$/g, '');
            else
                window.location.href = $('#AfterSwitchUrl').text() + "?pagename=unallocated";
        }
    }
    function ManualCreateClick() {
        var unq = GetQueryStringParams("unq");
        if (unq != undefined && unq != '')
            window.location.href = $('#AfterSwitchUrl').text() + "?pagename=createnew&unq=" + unq.replace(/^,|,$/g, '');
        else
            window.location.href = $('#AfterSwitchUrl').text() + "?pagename=createnew";
    }
    function UploadStudentsClick() {
        var unq = GetQueryStringParams("unq");
        if (unq != undefined && unq != '')
            window.location.href = $('#AfterSwitchUrl').text() + "?pagename=bulkupload&unq=" + unq.replace(/^,|,$/g, '');
        else
            window.location.href = $('#AfterSwitchUrl').text() + "?pagename=bulkupload";
    }
    $(function () {
        jQuery('#UnallocatedStudentSearch').keyup(function (e) {
            if (e.keyCode == 13) {
                var ret = SearchStudentsClick();
                return ret;
            }
        });

        switch (GetQueryStringParams("pagename")) {
            case "unallocated":
                {
                    ShowTab('UnallocatedDiv', 'CreateProfileDiv', 'UploadProfilesDiv');
                    var unq = GetQueryStringParams("unq");
                    if (unq != undefined && unq != '')
                        $('#UnallocatedStudentSearch').val(decodeURIComponent(unq));
                    break;
                }
            case "bulkupload":
                ShowTab('UploadProfilesDiv', 'CreateProfileDiv', 'UnallocatedDiv');
                break;
            default:
                ShowTab('CreateProfileDiv', 'UploadProfilesDiv', 'UnallocatedDiv');
                break;
        }

        var data = new kendo.data.DataSource({
            transport: {
                read: {
                    url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=getunallocatedstudents&searchtxt=' + encodeURIComponent($('#UnallocatedStudentSearch').val())),
                    dataType: "json",
                    serverFiltering: true
                }
            }
        });
        $("#UnallocatedStudentSearch").kendoAutoComplete({
            dataSource: data,
            filter: "contains",
            separator: ",",
            placeholder: "Search by name or username"
        });
        $('#UnallocatedStudentSearch').parent().addClass("search-kautocomplete");


    });
    function UpdateStudentAllocation(btn, studentSk) {
        $.ajax({
            url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=addremove&studentsk=' + studentSk + '&type=' + $(btn).attr("value")),
            dataType: "json",
            async: false,
            success: function (value) {
                if (value == 1) {
                    $(btn).val("REMOVE");
                    $(btn).addClass('add-remove');
                }
                else if (value == 2) {
                    $(btn).val("ADD");
                    $(btn).removeClass('add-remove');
                }
                else if (value == 3) {
                    SearchStudentsClick();
                }
                else { }
            }
        });
    }
    function ShowTab(tab1, tab2, tab3) {
        $('#MessageOuterDiv').html('');
        $('#' + tab1).addClass('ShowItems').removeClass('HideItems');
        $('#' + tab2).addClass('HideItems').removeClass('ShowItems');
        $('#' + tab3).addClass('HideItems').removeClass('ShowItems');

        switch (tab1) {
            case 'UnallocatedDiv':
                $('#SearchSwitch').addClass('bgd-white blue-clr bdr-btm');
                $('#ManualSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#UploadSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#eCollectionMenu').height($('#eCollectionContent').height() + 15).removeClass('zerocontentheight');
                $('.ContentPlaceHolder').height($('#eCollectionContent').height() + 20).removeClass('zerocontentheight');
                break;
            case 'UploadProfilesDiv':
                $('#UploadSwitch').addClass('bgd-white blue-clr bdr-btm');
                $('#SearchSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#ManualSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#eCollectionMenu').addClass('zerocontentheight');
                $('.ContentPlaceHolder').addClass('zerocontentheight');
                break;
            default:
                $('#ManualSwitch').addClass('bgd-white blue-clr bdr-btm');
                $('#SearchSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#UploadSwitch').removeClass('bgd-white blue-clr bdr-btm');
                $('#eCollectionMenu').addClass('zerocontentheight');
                $('.ContentPlaceHolder').addClass('zerocontentheight');
                break;
        }

    }
    function OpenLicenseExhaustPopUp() {
        SubscriptionExpired();
    }
    function BrowseFile() {
        $('#AttachAFile').click();
        return false;
    }
    function SetValue(Uploadingfile) {
        document.getElementById('txtAttachment').value = Uploadingfile;
    }
    jQuery(
    function () {

        $('#CreatePageBtn').removeClass('srtbtnshide');
        $('#CreatePageFinishBtn').removeClass('srtbtnshide');



        jQuery('#UploadProfilesDiv input').keyup(function (e) {
            if (e.keyCode == 13) {
                var ret = jQuery('#UploadButton').click();
                return ret;
            }
        });

        jQuery('#UploadButton').click(function () {
            var MandatoryText;
            $("#MsgDiv").removeAttr('class');
            if (checkBwsTxtBx()) {
                $("#image_preview").show();
                $("#MsgDiv").hide();
                return true;
            }

            $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
            $("#MsgDiv").text(GetMessage('VALIDATE_BROWSETXTBX')).show();
            jQuery('select').find('option:first-child').attr('selected', true);
            return false;
        });
    }
    );
    function CreateProfilesSuccess() {
        $("#MsgDiv").addClass("dnnFormMessage dnnFormSuccess dnnMsgScript").css('height', '40px');
        jQuery('#MsgDiv').html(jQuery('.CreateStudentProfile_MessageInnerDiv').html()).show()
        disp_clear();
    }

    function disp_clear() {
        jQuery('#CreateForm input').val('');
        jQuery('select').find('option:first-child').attr('selected', true);
        window.scroll(0, 0);
    }
    function checkBwsTxtBx() {
        if (jQuery('#txtAttachment').val().length == 0) {
            jQuery('#txtAttachment').css('border', '1px solid #ED175B');
            return false;
        }
        jQuery('#txtAttachment').css('border', '1px solid #CCC');
        return true;
    }
</script>
