<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateTeacherProfile.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.CreateTeacherProfile" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<div id="FirstDiv" style="width: 100%; float: left;">
    <div style="width: 94%; float: left; margin-left: 20px; margin-top: 17px;">
        <div id="MessageDiv" class="CreateTeacherProfile_MessageDiv" style="display: none;">
            <div class="CreateTeacherProfile_MessageInnerDiv">
                <div>
                    <span id="AddedName" runat="server" class="blkAfterNum"></span></div>
                <div class="Div_FullWidth HideItems">
                    <span id="TeachersCount" runat="server" class="blkAfterNum HideItems"></span>
                     teachers.<i>
                        <asp:HyperLink ID="SeeAllLink" runat="server" CssClass="blkSeeall HideItems" ClientIDMode="Static" Text="See all"></asp:HyperLink></i>
                </div>
            </div>
        </div>
        <div id="CreateForm" class="CreateTeacherProfile_TopDiv">      
            <div style="width: 98%; height: 57px; padding-left: 15px; margin-top: 25px; float: left;">
                <div style="width: 49.9%; float: left;">
                    <h5>
                        First name:</h5>
                    <div class="eCollectionEditDiv">
                        <div class="eCollectionTbxHolder">
                            <asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="eCollectionTextBox" autocomplete="off"
                                onkeydown="return event.keyCode != 13 && event.which != 13;" MaxLength="30" 
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
                            <asp:TextBox ID="LastNameTextBox" runat="server" CssClass="eCollectionTextBox" autocomplete="off"
                                onkeydown="return event.keyCode != 13 && event.which != 13;" MaxLength="30" 
                                ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="eCollectionEditSpan">
                            <span>*</span></div>
                    </div>
                </div>
            </div>
            <div style="width: 98%; height: 60px; padding-left: 15px; margin-top: 20px; float: left;">
                <div style="width: 49.9%; float: left;">
                    <h5>
                        Email:
                    </h5>
                    <div id="EmailTxtBxParent" runat="server" clientidmode="Static" class="eCollectionEditDiv">
                        <div class="eCollectionTbxHolder">
                            <asp:TextBox ID="EmailTextBox" runat="server" CssClass="eCollectionTextBox" autocomplete="off"
                                onkeydown="return event.keyCode != 13 && event.which != 13;" MaxLength="240"
                                ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="eCollectionEditSpan">
                            <span>*</span></div>
                    </div>
                </div>
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
                <asp:Button ID="AddTeacherProfileButton" CssClass="AddButton" Text="ADD TEACHER"
                    ClientIDMode="Static" OnClick="AddTeacherProfile_Click"
                    runat="server" />
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
<asp:HiddenField ID="SubsSkHdn" runat="server" ClientIDMode="Static" />
<script type="text/javascript">
    function Addteacher() {
        jQuery('#MessageDiv').css('display', 'block');
        jQuery('#FirstDiv').height('475px');
        jQuery('#eCollectionContent').height((jQuery('#FirstDiv').height() + 100) + 'px');
        window.scroll(0, 0);
    }
    jQuery(
        function () {
            jQuery('#HeaderBtn').css('display', 'none');
            jQuery('#FirstNameTextBox').focus();
            jQuery('input').keyup(function (e) {
                if (e.keyCode == 13) {
                    var ret = jQuery('#AddTeacherProfileButton').click();
                    return ret;
                }
            });
            jQuery("#TeachersTabHolder").css("position", "relative");
            jQuery("#TeachersTabHolder").css("z-index", "1000");
            jQuery("#TeachersTabHolder").addClass('CreateTabSelected');

            jQuery("#eCollectionContent").addClass('CreateTab');
            if ($('#RoleChkHdn').val() == 'true') {
                jQuery(".ECollLeftModule").css('margin-top','-310px');
            }
            else {
                jQuery(".ECollLeftModule").addClass('CreateTabECollLeftModule');
            }
            jQuery("#eCollectionMenu").addClass('CreateTabMenu');
        }
    );


        $("#AddTeacherProfileButton").click(function () {
            //   $("#MsgDiv").addClass("dnnFormMessage dnnFormSuccess dnnMsgScript").css('height', 'auto');
            // var errorText = ' is mandatory for teacher. Please specify the details and  then click Add Teacher button';
            // var MandatoryText;
            $("#MsgDiv").removeAttr('class');
            $("#MsgDiv").text('');
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

            if (ValidateText('EmailTextBox')) {
                jQuery('#EmailTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
            }
            else {
                if (err == 0) {
                    err++;
                    jQuery('#EmailTextBox').focus();
                    GetMessage("EMAIL_MANDATORY");
                }
            }
            if (err == 0) {
                if (checkForName('FirstNameTextBox', 'VALIDATE_FIRSTNAME')) {
                    if (checkForName('LastNameTextBox', 'VALIDATE_LASTNAME')) {
                        $("#MsgDiv").hide();
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;

                $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
                // $("#MsgDiv").text(MandatoryText + errorText).show();
            }
            return false;
        });

    function InviteTeacher() {
        $("#MsgDiv").addClass("dnnFormMessage dnnFormSuccess dnnMsgScript").css('height', '73px');
        jQuery('#MsgDiv').html(jQuery('.CreateTeacherProfile_MessageInnerDiv').html()).show()
        jQuery().addClass('');

        jQuery("#eCollectionContent").addClass('CreateTabSuccess');
        jQuery("#eCollectionMenu").addClass('CreateTabMenuSuccess');
        disp_clear();
    }
      

    function checkForName(textboxid, value) {
        var pattern = new RegExp(/^[a-zA-Z .']+$/);

        jQuery('#CreateForm input[type=text]').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        if (!pattern.test(jQuery('#' + textboxid).val().trim())) {
            $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
            GetMessage(value);
            //$("#MsgDiv").text('Please avoid special characters/Numbers in ' + value).show();
            jQuery('#' + textboxid).focus();
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        return true;
    }

    function ValidateText(textboxid) {       
        if (jQuery('#' + textboxid).val().trim().length == 0) {
            jQuery('#' + textboxid).parent().parent().css('border', '1px solid #ED175B');
            return false;
        }
        return true;
    }   
    
</script>
