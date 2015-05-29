<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Feedback.ascx.cs" Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.Feedback" %>
<style type="text/css">
.errorStyle {
color: #FFFFFF;
float: left;
font-weight: bold;
height: 32px;
margin-left: 50px;
margin-top: 13px;
text-transform: uppercase;
width: inherit;
} 
    .lblStyle
    {
        border-right: 1px solid #CCCCCC;
        box-shadow: 0 2px 2px 0 #cccccc;
        color: #0080C8;
        float: left;
        height: 24px;
        padding: 11px 12px 0;
        background-image: -ms-linear-gradient(top left, #ffffff 0%, #f2f2f2 100%) !important;
        background: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#f2f2f2)) !important;
        background: -moz-linear-gradient(to bottom,#ffffff, #f2f2f2) !important;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffff', endColorstr='#f2f2f2', gradientType='0') !important;
        font-weight: 600;
    }
    .inputStyle
    {
        float: left;
        height: 34px;
        background: none repeat scroll 0 0 #FFFFFF;
    }
    .starMandatory
    {
        color: #0080C8;
        float: left;
        font-size: 35px;
        height: 21px;
        padding: 11px 10px;
        margin-left: 10px;
    }
    .errormessageclass
    {
        width: 417px !important;
        margin-left: 0px !important;
    }
    .titDiv
    {
        float: left;
        width: 105%;
    }
</style> 
<script type="text/javascript">

    function EmptyErrorMessageGenerate(msg, ErrorId) {
        if (ErrorId.innerHTML != "") {
            ErrorId.innerHTML = "";
        }
        var div = document.createElement('DIV');
        div.style.width = "400px";
        div.style.cssFloat = "left";
        div.innerHTML = GetDynamicRecipientTextBox(msg);
        ErrorId.appendChild(div);
        ErrorId.className = "errormessageclass";
    }
    function GetDynamicRecipientTextBox(value) {
        return '<div class="errorStyle">' + value + '</div>';
    }
    window.onload = function () {
        $('#NameTextBox').val("");
        $('#UserEmail').val("eg: name@domain.com");
        $("#SignupPartnerPhone").val("");
        $("#<%=CommentsTextArea.ClientID %>").val("");
    };

    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var validate = false;

    $(document).ready(function () {
        $('#SignupPartnerPhone').bind('paste', function (e) {
            var data = e.originalEvent.clipboardData.getData('Text');
            if (isNaN(data))
                event.preventDefault();
        });

        $('#FeedBackSubmitbutton').click(function () {
            var textBoxValidate = false;
            var emailValidate = false;
            if ($('#NameTextBox').val() == "") {
                var Id = document.getElementById("signupfirstNameerrorDiv");
                var msg = "PLEASE ENTER FIRST NAME.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#signupfirstNameerrorDiv").show();
                validate = false;
                textBoxValidate = false;
            }
            else {
                $("#signupfirstNameerrorDiv").hide();
                validate = true;
                textBoxValidate = true;
            }
            if ($('#UserEmail').val().trim() == "" || $('#UserEmail').val().trim().toLowerCase() == "eg: name@domain.com") {
                var Id = document.getElementById("signupemailerrorDiv");
                var msg = "PLEASE ENTER EMAIL.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#signupemailerrorDiv").show();
                validate = false;
                emailValidate = false;
            }
            else {
                if (!filter.test($('#UserEmail').val().trim())) {
                    var Id = document.getElementById("signupemailerrorDiv");
                    var msg = "PLEASE ENTER VALID EMAIL.";
                    EmptyErrorMessageGenerate(msg, Id);
                    $("#signupemailerrorDiv").show();
                    validate = false;
                    emailValidate = false;
                }
                else {
                    $("#signupemailerrorDiv").hide();
                    validate = true;
                    emailValidate = true;
                }
            }
            $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
            $('#eCollectionMenu').css({ "width": "0px" });
            if (textBoxValidate && emailValidate)
                return true;
            else
                return false;
            //return validate;
        });
        $("#lblbannertxt")[0].style.color = "#0089c5";
        if ($('#FeedBackContentDiv')[0] != undefined) {
            $("#lblbannertxt")[0].innerHTML = "We love feedback!";
        }
        else {
            $("#lblbannertxt")[0].innerHTML = "Thank you";
        }
        $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
        $('#eCollectionMenu').css({ "width": "0px" });
        $('.bannertitle').css('padding-left', '16px');
    });
    function changestyle(id) {
        $(id).css('font-style', 'normal');
    }
    function focusstyle(id) {
        if ($(id)[0].value == 'eg: name@domain.com') {
            $(id)[0].value = '';
            $(id).css('font-style', 'normal');
        }
    }
    function Emailblurstyle(id) {
        if (id.value != "") {
            var records = httpGet(GetFile('/DesktopModules/SignUp/Components/Handlers/GetErrorMessage.ashx?request=error&data=' + id.value));
            if (records != 0) {
                var emailId = document.getElementById("signupemailerrorDiv");
                var msg = GetErrorMsg(19);
                EmptyErrorMessageGenerate(msg, emailId);
                id.focus();
                $("#signupemailerrorDiv").show();
            }
            else {
                $("#signupemailerrorDiv").hide();
            }
        }
        if ($(id)[0].value == '') { $(id)[0].value = 'eg: name@domain.com'; $(id).css('font-style', 'italic'); }
    }
    function countryfocusstyle(id) {
        if ($(id)[0].value == 'Please specify which country') {
            $(id)[0].value = '';
            $(id).css('font-style', 'normal');
        }
    }
    function changestyle(id) {
        $(id).css('font-style', 'normal');
    }
    function validatePHN(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
        //comparing pressed keycodes
        if ((keycode < 48 || keycode > 57) && (keycode < 40 || keycode > 41) && (keycode < 4 || keycode > 45)) { return false; }
    }
</script>
<asp:PlaceHolder ID="FeedBackFormContent" runat="server">
    <div id="FeedBackContentDiv">
        <div class='RequestAccessHeader'>
            <h2 style="font-weight: 700; color: #0089c5 !important; font-family: Raleway, Arial !important;">
                The PM eCollection is a brand new platform that<br />
                re-imagines guided and independent<br />
                reading for the digital age.</h2>
        </div>
        <div style="float: left; width: 55%">
            <h4 style="margin: 0px; font-size: 12pt !important; font-weight: 800 !important;
                font-family: Raleway, Arail !important; margin-top: 30px; margin-bottom: 10px;">
                Key contact information</h4>
            <div class="labelledtxtbox">
                <div class="lblStyle" style="" id="spnFirstName">
                    
                    <label for="NameTextBox">Your name:</label></div>
                <div class="inputStyle" style="">
                    <input type="text" id="NameTextBox" isvalidate="true" name="firstName" class="RequestTextCommon"
                        maxlength="50" runat="server" clientidmode="Static" onfocus="javascript:changestyle(this)" /></div>
            </div>
            <div class="starMandatory" style="padding: 13px 0px 10px 15px;">
                *
            </div>
            <div id="signupfirstNameerrorDiv">
            </div>
            <div class="labelledtxtbox">
                <div class="lblStyle" id="Div2">
                    <label for="UserEmail">Your email:</label></div>
                <div class="inputStyle">
                    <input id="UserEmail" type="text" isemailvalidate="true" value="eg: name@domain.com"
                        runat="server" clientidmode="Static" maxlength="254" onblur="javascript:Emailblurstyle(this);"
                        onfocus="javascript:focusstyle(this);" name="firstName" class="RequestTextCommon"
                        onclick="javascript:changestyle(this)" /></div>
            </div>
            <div class="starMandatory" style="padding: 13px 0px 16px 15px;">
                *
            </div>
            <div id="signupemailerrorDiv">
            </div>
            <div style="float: left; width: 414px; border: 1px solid #CCC; margin-bottom: 30px;
                margin-top: 8px;">
                <div class="lblStyle" id="Div11" style="padding-right: 8px;">
                    <label for="SignupPartnerPhone">Your phone:</label></div>
                <div class="inputStyle" style="">
                    <input type="text" maxlength="15" id="SignupPartnerPhone" runat="server" clientidmode="Static"
                        name="firstName" class="RequestTextCommon" onfocus="javascript:changestyle(this)"
                        onkeypress="return validatePHN(event)" /></div>
            </div>
            <%--<div class="starMandatory" style="padding: 15px 0px 12px 15px;">
                *
            </div>
            <div id="SignupPartnerPhoneerrorDiv">
            </div>--%>
        </div>
        <div style="float: left; width: 58%; margin-top: 10px;">
            <h4 style="margin: 0px; font-size: 12pt !important; font-weight: 800 !important;
                font-family: Raleway, Arail !important;">
                Feedback</h4>
            <asp:TextBox ID="CommentsTextArea" MaxLength="100" Style="min-height: 250px; width: 93% !important;
                margin-top: 10px; resize: none;border: 1px solid lightgray;" TextMode="MultiLine" runat="server"></asp:TextBox>
            <asp:Button ID="FeedBackSubmitbutton" runat="server" OnClick="FeedBackSubmitbutton_Click"
                CssClass="ActiveAddButtonsHolder FeedBackButton" Text="SEND FEEDBACK" ClientIDMode="Static" />
        </div>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="FeedBackSuccess" Visible="false" runat="server">
    <h2 style="font-weight: 700; color: #0089c5 !important; font-family: Raleway, Arial !important;">
        Thank you for taking the time to provide your feedback</h2>
</asp:PlaceHolder>
