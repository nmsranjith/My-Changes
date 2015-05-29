<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateStudentProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.CreateStudentProfile" %>
<%@ Register Src="~/controls/eCollectionControls/ReadingLevelSlider.ascx" TagName="RLSlider"
    TagPrefix="RL" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<style type="text/css">
    .Licpopup
    {
        width: 30px;
        height: 30px;
        margin-top: -17px;
        position: relative;
        z-index: 100;
        float: right !important;
        margin-left: -13px;
        cursor: pointer;
    }
    .k-state-selected.k-state-focused
    {
        background-color: #707070 !important;
    }
    .k-window-action
    {
        background-image: url('/Portals/0/images/close.png') !important;
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
<div id="MessageOuterDiv" runat="server" style="width: 100%; position: static; display: none;">
    <div class="bubble1">
        <asp:Label ID="Message1" runat="server" Text="" />
    </div>
</div>
<div id="FirstDiv" style="width: 100%; float: left; height: 580px; font-size: 10pt;">
    <div style="width: 94.3%; float: left; margin-left: 20px; margin-top: 17px;">
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
                        OnClick="PrintStudentCards" Text="PRINT" />
                    <div class="button" style="padding-top:4px;padding-bottom:4px;">
                        <a id="SeeAllLink" runat="server" style="text-decoration:none!important;color:#707070!important;">SEE ALL</a>
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
                    <RL:RLSlider ID="ReadingLevelSlider" runat="server">
                    </RL:RLSlider>
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
                <div class="ActiveAddButtonsHolder addBtns">
                    <asp:Button ID="AddStudentProfileButton" CssClass="AddButton" Text="ADD STUDENT"
                        ClientIDMode="Static" OnClick="AddStudentProfile_Click" runat="server" />
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
<asp:HiddenField ID="RRHdn" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="ESLHdn" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="UserCount" runat="server" ClientIDMode="Static" />
<div id="LicenceExhaust" style="display: none; background: white !important;">
    <div class="popupHdrBG">
        <span class="PopupHeaderSpan" style="margin-top: 36px;">Alert Message!</span>
    </div>
    <div style="moz-box-shadow: -1px -1px 8px #B8B8B8; -webkit-box-shadow: -1px -1px 8px #B8B8B8;
        box-shadow: -1px -1px 8px #B8B8B8; float: left; width: 103%; margin-left: -7px;
        height: 87%;">
        <div style="width: 91.5%; height: 76px; float: left; margin: 25px; border: 1px solid lightgray;
            box-shadow: -2px 7px 5px -6px lightgray; -moz-box-shadow: 2px 7px 5px -6px lightgray;
            -webkit-box-shadow: 2px 7px 5px -6px lightgray;">
             <asp:Label ID="TeacherTxt" runat="server" Visible="false" style="font-family: Raleway-regular, Arial, sans-serif; font-size: 10pt;
                color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please contact your Administrator</asp:Label>
            <asp:Label ID="AdminText" runat="server" Visible="false" style="font-family: Raleway-regular, Arial, sans-serif; font-size: 10pt;
                color: #707070; padding: 30px; float: left;">Licences for the subscription are exhausted, Please buy/update the subscription. Please <a id="VisitLink" href="/pmecollection" runat="server">visit</a></asp:Label>
      
        </div>
        <div style="width: 92%;">
            <input type="button" id="PopuOkButton" value="Ok" class="popupokbtn" />
        </div>
    </div>
</div>
<div id="UserNameExist" title="Alert message!" style="display: none; background: white !important;">
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

        jQuery('input').keyup(function (e) {
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
   

</script>
