<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="customerservice.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.customerservice" %>
<%@ Register Src="eCollectionMessages.ascx" TagName="Message" TagPrefix="Msg" %>
<div id="MsgDiv">
    <Msg:Message ID="Messages" runat="server">
    </Msg:Message>
</div>
<div style="width: 64%; float: left; margin-left: 16%; background: -moz-linear-gradient(center top , white 0%, #F8FDFF 5%, #E3F5FF 130%) repeat scroll 0 0 transparent; background: -webkit-gradient(linear, left top, left bottom, from(#F9FDFF), to(#E3F5FF)); filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#F9FDFF', endColorstr='#E3F5FF', gradientType='0'); border: 1px solid #E2E2E2;margin-top: 10%;">
    <h3 style="margin-left: 14px; margin-top: 14px; margin-bottom: 15px; font-family: Raleway,Arial;
        font-weight: 700; font-size: 12pt; color: #707070; font-weight: 700 !important;
        font-size: 12pt !important;" class="H4">
        Upload
    </h3>
    <div class="BulkUpload_BrowseDiv" style="height: 48px;">
        <input id="txtAttachment" runat="server" type="text" clientidmode="Static" style="width: 437px;
            height: 30px; border-radius: 4px; float: left; margin-left: 16px; border: 1px solid lightgray;
            border-top: 2px solid lightgray;" maxlength="250" onclick="return false" ondblclick="BrowseFile();" />
        <input type="button" id="AttachFileButton" class="ActiveAddButtonsHolder" style="text-shadow: 1px 2px 2px #557183;
            padding: 10px; width: 126px; height: 35px; color: White; cursor: pointer; float: left;
            border: 1px solid #6EB8C6; -moz-border-radius: 3px; -webkit-border-radius: 3px;
            border-radius: 3px; -khtml-border-radius: 3px; float: right; margin-right: 20px;
            padding-top: 9px; margin-top: 0px; margin-left: 0px; margin-bottom: 0px; font-weight: normal;
            font-family: Raleway, Arial;" onclick="BrowseFile();" value="BROWSE" />
        <asp:FileUpload ID="AttachAFile" runat="server" UseSubmitBehavior="false" onchange="SetValue(this.value)"
            ClientIDMode="Static" Style="width: 15.5%; margin-top: -35px; float: left; opacity: 0;
            filter: alpha(opacity=0); background-color: transparent; color: transparent;
            height: 32px; margin-left: 7px;" />
    </div>
    <div style="width: 100%;">
        <div id="uploaddiv" style="width: 27%; float: left; height: 50px;">
            <asp:Button Text="UPLOAD" CssClass="UploadBooksBtn" Style="font-family: Raleway, Arial;
                font-weight: normal;" ID="UploadBooksButton" runat="server" ClientIDMode="Static"
                OnClick="UploadBooksButton_Click" UseSubmitBehavior="false" />
        </div>
        <div style="width: 40%; float: left; padding-top: 6px;">
            <asp:Button ID="CancelButton" runat="server" Style="color: #1FB5E7; border: 0px solid white;
                float: left; background: transparent; font-size: 10pt; cursor: pointer;" Text="Cancel"
                OnClick="CancelButton_Click" UseSubmitBehavior="false" />
        </div>
    </div>
</div>
<script type="text/javascript">
    function BrowseFile() {
        document.getElementById('AttachAFile').click();

    }
    function SetValue(Uploadingfile) {
        document.getElementById('txtAttachment').value = Uploadingfile;
    }

    $(function () {
        $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
        $('#eCollectionMenu').css({ "width": "0px" });
    });
</script>
