<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentsProfileBulkUpload.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Students.Views.StudentsProfileBulkUpload" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<style type="text/css">
    .k-state-selected.k-state-focused
    {
        background-color: #707070 !important;
    }
    #SubscriptionDrpList-list
    {
        width: 281px !important;
        margin-top: 3px;
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
        cursor:pointer;
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
</style>
<div id="image_preview" runat="server" clientidmode="Static" class="dnnLoading stublkimgprv">
</div>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<div class="stublkMsgfirstdiv">
    <div id="MessageDiv" class="CreateStudentProfile_MessageDiv" style="display: none;">
        <div class="CreateStudentProfile_MessageInnerDiv">
            <div class="stublkMsgspHolder">
            <span id="StudentsCount" runat="server"  class="blkAfterNum blktext"></span>
            </div>
            <div class="stublkMsgspHolder">
                <span id="TotalAddedStudents" runat="server" class="blkAfterNum">
                </span>  
                 <span class="blkAfterNum">
                 ] student/s
                </span>
            </div>
            <div class="blkPrnt">
                <asp:Button ID="PrintStudentCardsButton1" runat="server" CssClass="button" OnClientClick="window.document.forms[0].target='_blank';" 
                    OnClick="PrintStudentCards" Text="PRINT" />
                    
                    <asp:HyperLink ID="SeeAllLink" runat="server"  Text="SEE ALL"
                        ClientIDMode="Static"></asp:HyperLink>
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
                        maxlength="250" onclick='return false' ondblclick="BrowseFile();" onkeydown="return event.keyCode != 13 && event.which != 13;"
                        runat="server" clientidmode="Static" />
                </div>
                <div class="BulkUpload_BwsBtnHolder" style="position: relative;">
                    <input type="button" id="AttachFileButton" onclick="BrowseFile();" value="BROWSE"
                        class="BulkUpload_BwsBtn" />
                </div>
                <asp:FileUpload ID="AttachAFile" runat="server" onchange="SetValue(this.value)" ClientIDMode="Static"
                    CssClass="stublkFileUpload" />
            </div>     
        </div>
    </div>
</div>
<div class="stublkhrdiv">
    <hr class="createHrs" />
</div>
<div class="Div_FullWidth">
    <div class="BtnsHdr">
        <div class="AddBtnHdr">
            <div class="ActiveAddButtonsHolder addBtns">
                <asp:Button ID="UploadButton" CssClass="AddButton" Text="UPLOAD" ClientIDMode="Static"
                    OnClick="UploadFileImageButton_Click" runat="server" />
                <input type="reset" id="Reset2" style="display: none;" />
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
<asp:GridView ID="tab" runat="server">
</asp:GridView>
<div id="dialog-message1" style="display: none;" class="stublkpopup">
    <div class="stublkpopuphdrbg">
        <span class="PopupHeaderSpan stublkMsgPPspan">Alert Message!</span>
    </div>
    <div class="stublkPPMsgDiv">
        <div class="stublkPPMsgInnerdiv">
            <literal class="stublkPPMsgliteral">Licences for the subscription are exhausted, Please contact your Administrator</literal>
        </div>
        <div class="stublkPPBtnDiv">
            <input type="button" id="OkButton" value="Ok" class="popupokbtn" />
        </div>
    </div>
</div>
<div id="LicenceExhaust"  style="display: none; background: white !important;">
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
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery(".ECollLeftModule").css('margin-top', '-345px');
        }

        jQuery('#eCollectionContent').addClass('UploadPageContent');
        jQuery('#eCollectionMenu').addClass('UploadPageMenu');

        $('#CreatePageBtn').removeClass('srtbtnshide');
        $('#CreatePageFinishBtn').removeClass('srtbtnshide');

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
//    function SubscriptionExpired() {
//        $("#dialog-message1").css({ 'display': 'block' });
//        $('.k-window-actions.k-header').css('cursor', 'pointer');
//        kwindow = $("#dialog-message1"); 
//        if (!kwindow.data("kendoWindow")) {
//            kwindow.kendoWindow({
//                width: "665px",
//                height: "300px",
//                modal: true,
//                draggable: false
//            });
//            kwindow.data("kendoWindow").center();
//        }
//        kwindow.data("kendoWindow").open();
//        $(".k-icon.k-i-close").hide();
//        $('a.k-window-action.k-link').mouseover(function () {
//            $('a.k-window-action.k-link').parent().addClass("popupClosebg");
//            return false;
//        });
//    }

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
</script>
