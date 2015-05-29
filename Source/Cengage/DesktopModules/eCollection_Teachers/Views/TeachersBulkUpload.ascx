<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeachersBulkUpload.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Teachers.Views.TeachersBulkUpload" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<style type="text/css">
    .k-state-selected.k-state-focused
    {
        background-color: #707070 !important;
    }
    #SubscriptionDrpList-list
    {
        width: 289px !important;
        margin-top: 3px;
    }
</style>
<div id="image_preview" runat="server" clientidmode="Static" class="dnnLoading" style="margin-left: 17%;
    width: 50%; height: 19%; text-align: center; padding-top: 25px; margin-top: 18%;
    border: 1px solid #707070; display: none;">
</div>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server"></Msg:Message>
</div>
 <Msg:Message ID="Messages1" runat="server"></Msg:Message>
 <Msg:Message ID="Messages2" runat="server"></Msg:Message>
 <Msg:Message ID="Messages3" runat="server"></Msg:Message>
<div style="float: left; width: 94%; margin: 15px 0px -15px 20px">
    <div id="MessageDiv" class="CreateStudentProfile_MessageDiv" style="display: none;">
        <div class="CreateTeacherProfile_MessageInnerDiv">
            <div>
               <span id="TeachersCount" runat="server" class="blkAfterNum"></span></div>
            <div style="float: left;display:none;">
                 <span id="TotalAddedTeachers" runat="server" class="blkAfterNum">
                </span> teachers <i>
                    <asp:HyperLink ID="SeeAllLink" runat="server" CssClass="blkSeeall HideItems" Text="See all"
                        ClientIDMode="Static"></asp:HyperLink></i>
            </div>
        </div>
    </div>
</div>
<div class="BulkUpload_MainDiv">
    <div class="BulkUpload_SecondDiv">
        <div class="BulkUpload_UploadHdr">
            <h4 class="BulkUpload_Uploadlabel">
                Bulk upload</h4>
        </div>
        <div class="BulkUpload_DownLoadFormatDiv">
            <h4 class="BulkUpload_HeaderColor">
                How to upload teachers:</h4>
            <p>
                a. Download the Excel file below.
            </p>
            <p>
                b. Complete all mandatory fields for all teachers.</p>
            <p>
                c. Save the file on your system.</p>
            <p>
                d. Return to the page and upload the file using the browse button below.</p>
            <div class="DLExcelFormatBtnHolder">
                <asp:Button ID="DownLoadExcelButton" CssClass="DownLoadExcelFormatBtn" runat="server"
                    Text="DOWNLOAD EXCEL FILE" OnClick="DownLoadExcelButton_Click" />
            </div>
        </div>
        <div class="blkHrHdr">
            <hr class="createHrs" />
        </div> 
        <div class="BulkUpload_UploadDiv">
            <h5>
                Bulk upload:</h5>
            <div class="BulkUpload_BrowseDiv">
                <div class="BulkUpload_TxtBxHolder">
                    <input id="txtAttachment" type="text" name="txtAttachment" class="BulkUpload_TxtBx"
                        autocomplete="off" onclick='return false' ondblclick="BrowseFile();" runat="server"
                        onkeydown="return event.keyCode != 13 && event.which != 13;" clientidmode="Static" />
                </div>
                <div class="BulkUpload_BwsBtnHolder" style="position: relative;">
                    <input type="button" id="AttachFileButton" onclick="BrowseFile();" value="BROWSE"
                        class="BulkUpload_BwsBtn" />
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
            <div class="ActiveAddButtonsHolder addBtns">
                <asp:Button ID="UploadButton" CssClass="AddButton" Text="UPLOAD" ClientIDMode="Static"
                    OnClick="UploadFileImageButton_Click" runat="server" />
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
<asp:HiddenField ID="SelectedSubscriptions" runat="server" ClientIDMode="Static" />
<script type="text/javascript">

    function BrowseFile() {
        $('#AttachAFile').click();
        return false;
    }
    function SetValue(Uploadingfile) {
        document.getElementById('txtAttachment').value = Uploadingfile;
    }
    jQuery(
    function () {
        jQuery("#TeachersTabHolder").addClass('CreateTabSelected');

        if ($('#RoleChkHdn').val() == 'true') {
            jQuery(".ECollLeftModule").css('margin-top', '-310px');
        }
        else {
            jQuery(".ECollLeftModule").addClass('CreateTabECollLeftModule');
        }


        jQuery('#HeaderBtn').css('display', 'none');
        if (!$.browser.mozilla) {
            jQuery('#txtAttachment').addClass('BlkUpld_TxtBx_Others');
        }
        else {
            jQuery('#txtAttachment').addClass('BlkUpld_TxtBx_Mox');
        }

        jQuery('input').keyup(function (e) {
            if (e.keyCode == 13) {
                var ret = jQuery('#UploadButton').click();
                return ret;
            }
        });

        jQuery('#UploadButton').click(function () {
            var MandatoryText;
            jQuery("#MsgDiv").removeAttr('class');
            var subsSk = '';
            if (checkBwsTxtBx()) {
                $("#image_preview").show();
                $("#MsgDiv").hide();
                return true;
            }
            else
            //MandatoryText = "Please browse and select the excel file to be uploaded and then click upload button.";
                $("#MsgDiv").text(GetMessage('VALIDATE_BROWSETXTBX')).show();
            $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
            //$("#MsgDiv").text(MandatoryText).show();
            jQuery('select').find('option:first-child').attr('selected', true);
            return false;
        });

    }
    );

    function CreateProfilesSuccess() {
        $("#MsgDiv").addClass("dnnFormMessage dnnFormSuccess dnnMsgScript").css('height', '73px');
        jQuery('#MsgDiv').html(jQuery('.CreateTeacherProfile_MessageInnerDiv').html()).show()
        disp_clear();
    }

    function checkBwsTxtBx() {
        if (jQuery('#txtAttachment').val().length == 0) {
            jQuery('#txtAttachment').addClass('MandatoryClass');
            return false;
        }
        jQuery('#txtAttachment').removeClass('MandatoryClass');
        return true;
    }
</script>
