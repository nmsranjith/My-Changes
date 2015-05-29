<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestAccess.ascx.cs"
    Inherits="DotNetNuke.Modules.eCollection_Dashboards.Views.RequestAccess" %>
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
        $('#SchoolName').val("");
        $('#SchoolPhoneNumber').val("");
        $("#SignUpPartnerAddress").val("line 1");
        $("#Line1Address").val("line 2");
        $("#SuburbTextBox").val("");
        $("#PostCodeTextBox").val("");
        $("#<%=CommentsTextArea.ClientID %>").val("");
    };

    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var validate = false;

    $(document).ready(function () {
        $('#SchoolPhoneNumber').bind('paste', function (e) {
            var data = e.originalEvent.clipboardData.getData('Text');           
            if(isNaN(data))
                event.preventDefault();
        });
        $('#PostCodeTextBox').bind('paste', function (e) {
            var data = e.originalEvent.clipboardData.getData('Text');
            if (isNaN(data))
                event.preventDefault();
        });
        $('#SignupPartnerPhone').bind('paste', function (e) {
            var data = e.originalEvent.clipboardData.getData('Text');
            if (isNaN(data))
                event.preventDefault();
        });
        $('#RequestEarlyAccessbutton').click(function () {
            var textBoxValidate = false;
            var emailValidate = false;
            var phoneValidate = false;
            var schoolValidate = false;
            var schoolPhone = false;
            var addressValidate = false;
            var subUrbValidate = false;
            var postCodeValidate = false;
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
                textBoxValidate = true;
                validate = true;
            }
            if ($('#UserEmail').val().trim() == "" || $('#UserEmail').val().trim().toLowerCase() == "eg: name@domain.com") {
                var Id = document.getElementById("signupemailerrorDiv");
                var msg = "PLEASE ENTER EMAIL.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#signupemailerrorDiv").show();
                emailValidate = false;
                validate = false;
            }
            else {
                if (!filter.test($('#UserEmail').val().trim())) {
                    var Id = document.getElementById("signupemailerrorDiv");
                    var msg = "PLEASE ENTER VALID EMAIL.";
                    EmptyErrorMessageGenerate(msg, Id);
                    $("#signupemailerrorDiv").show();
                    emailValidate = false;
                    validate = false;
                }
                else {
                    $("#signupemailerrorDiv").hide();
                    emailValidate = true;
                    validate = true;
                }
            }
            if ($('#SignupPartnerPhone').val() == "") {
                var Id = document.getElementById("SignupPartnerPhoneerrorDiv");
                var msg = "PLEASE ENTER PHONE NUMBER.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#SignupPartnerPhoneerrorDiv").css("marginTop", "-22px");
                $("#SignupPartnerPhoneerrorDiv").show();
                phoneValidate = false;
                validate = false;
            }
            else {
                $("#SignupPartnerPhoneerrorDiv").css("marginTop", "6px");
                $("#SignupPartnerPhoneerrorDiv").hide();
                phoneValidate = true;
                validate = true;
            }

            if ($('#SchoolName').val() == "") {
                var Id = document.getElementById("SchoolNameerrorDiv");
                var msg = "PLEASE ENTER SCHOOL NAME.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#SchoolNameerrorDiv").css("marginTop", "4px");
                $("#SchoolNameerrorDiv").show();
                schoolValidate = false;
                validate = false;
            }
            else {
                $("#SchoolNameerrorDiv").css("marginTop", "0px");
                $("#SchoolNameerrorDiv").hide();
                schoolValidate = true;
                validate = true;
            }

            if ($('#SchoolPhoneNumber').val() == "") {
                var Id = document.getElementById("SchoolPhoneNumbererrorDiv");
                var msg = "PLEASE ENTER SCHOOL PHONE NUMBER.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#SchoolPhoneNumbererrorDiv").css("marginTop", "-22px");
                $("#SchoolPhoneNumbererrorDiv").show();
                schoolPhone = false;
                validate = false;
            }
            else {
                $("#SchoolPhoneNumbererrorDiv").css("marginTop", "0px");
                $("#SchoolPhoneNumbererrorDiv").hide();
                schoolPhone = true;
                validate = true;
            }

            if (($('#SignUpPartnerAddress').val().trim() == "" || $('#SignUpPartnerAddress').val().trim() == "line 1") && ($('#Line1Address').val().trim() == "" || $('#Line1Address').val().trim() == "line 2")) {
                var Id = document.getElementById("SignUpPartnerAddresserrorDiv");
                var msg = "PLEASE ENTER ADDRESS.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#SignUpPartnerAddresserrorDiv").css("marginTop", "4px");
                $("#SignUpPartnerAddresserrorDiv").show();
                addressValidate = false;
                validate = false;
            }
            else {
                $("#SignUpPartnerAddresserrorDiv").css("marginTop", "0px");
                $("#SignUpPartnerAddresserrorDiv").hide();
                addressValidate = true;
                validate = true;
            }
            /******************/

            if ($('#SuburbTextBox').val().trim() == "" && $('#PostCodeTextBox').val().trim() == "") {
                var Id = document.getElementById("ShippingAddressErrorDiv");
                var msg = "PLEASE ENTER SUBURB AND POSTCODE.";
                EmptyErrorMessageGenerate(msg, Id);
                $("#ShippingAddressErrorDiv").show();
                subUrbValidate = false;
                postCodeValidate = false;
                validate = false;
            }
            else {
                if ($('#SuburbTextBox').val().trim() == "") {
                    var Id = document.getElementById("ShippingAddressErrorDiv");
                    var msg = "PLEASE ENTER SUBURB.";
                    EmptyErrorMessageGenerate(msg, Id);
                    $("#ShippingAddressErrorDiv").show();
                    subUrbValidate = false;
                    validate = false;
                }
                else {
                    subUrbValidate = true;
                    if ($('#PostCodeTextBox').val().trim() == "") {
                        var Id = document.getElementById("ShippingAddressErrorDiv");
                        var msg = "PLEASE ENTER POSTCODE.";
                        EmptyErrorMessageGenerate(msg, Id);
                        $("#ShippingAddressErrorDiv").show();
                        postCodeValidate = false;
                        validate = false;
                    }
                    else {
                        $("#ShippingAddressErrorDiv").css("marginTop", "0px");
                        $("#ShippingAddressErrorDiv").hide();
                        postCodeValidate = true;
                        validate = true;
                    }
                }


            }



            $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
            $('#eCollectionMenu').css({ "width": "0px" });
            if (textBoxValidate && emailValidate && phoneValidate && schoolValidate && schoolPhone && addressValidate && subUrbValidate && postCodeValidate)
                return true;
            else
                return false;
            //return validate;
        });
        $("#lblbannertxt")[0].style.color = "#0089c5";
        if ($('#RequestAccessContentDiv')[0] != undefined) {
            $("#lblbannertxt")[0].innerHTML = "Request Early Access";
            $("#lblbannertxt")[0].style.marginLeft = "7px";
        }
        else {
            $("#lblbannertxt")[0].innerHTML = "Thank you";
        }
        $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
        $('#eCollectionMenu').css({ "width": "0px" });
        $('.bannertitle').css('padding-left', '16px');
    });
</script>
<asp:PlaceHolder ID="RequestAccessContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            if (/chrome/.test(navigator.userAgent.toLowerCase())) {
                $.browser.safari = false;
            }
            if ($.browser.safari) {
                $("#LineAddressBox").css({ "width": "333px", "margin-left": "5px" });
            }

            $('#eCollectionContent').css({ "width": "950px", "height": "auto", 'margin': '0px', 'box-shadow': 'none', '-webkit-box-shadow': 'none', 'moz-box-shadow': 'none' });
            $('#eCollectionMenu').css({ "width": "0px" });
            $("#PostalAddressCountryDropDownList").kendoDropDownList({ select: OnSelectCountry });
            $('#PostalAddressCountryDropDownList').change(function () {
                if ($('#PostalAddressCountryDropDownList option:selected').text() == "Australia") {
                    $('#statediv').show();
                    $('#EnterCountryDiv').hide();
                }
                else {
                    if ($("#PostalAddressCountryDropDownList option:selected").text() == "Other") {
                        $('#EnterCountryDiv').show();
                        $('#statediv').hide();
                        $('#OtherCountryTextBox').focus();
                    }
                    else {
                        $('#statediv').hide();
                        $('#EnterCountryDiv').hide();
                    }
                }
            });

            $("#PostalAddressStateDropDownList").kendoDropDownList({ select: onStateSelect });
            //$("#PostalAddressStateDropDownList").data('kendoDropDownList').text("State");

        });
        function validatePHN(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if ((keycode < 48 || keycode > 57) && (keycode < 40 || keycode > 41) && (keycode < 4 || keycode > 45)) { return false; }
        }
        function countryfocusstyle(id) {
            if ($(id)[0].value == 'Please specify which country') {
                $(id)[0].value = '';
                $(id).css('font-style', 'normal');
            }
        }
        function countryblurstyle(id) {
            if ($(id)[0].value == '') { $(id)[0].value = 'Please specify which country'; $(id).css('font-style', 'italic'); }
        }
        function changestyle(id) {
            $(id).css('font-style', 'normal');
        }
        function onStateSelect(e) {
            var dataItem = this.dataItem(e.item.index());
            $("#PostalAddressStateDropDownList").data('kendoDropDownList').text(dataItem.text);
        }
        function OnSelectCountry(e) {
            var dataItem = this.dataItem(e.item.index());
            $("#PostalAddressCountryDropDownList").data('kendoDropDownList').text(dataItem.text);
            //dataItem.text
            if (dataItem.text == "Australia") {
                $('#countrydropdiv .coutrylist')[0].style.marginLeft = "";
                // $("#PostalAddressCountryDropDownList-list").css("margin-left", "37px");
                // $('#countrydropdiv .k-dropdown-wrap').css('padding-left', '7.5px !important');
                $('#statediv').show();
                $('#EnterCountryDiv').hide();
            }
            else {
                if (dataItem.text == "Other") {
                    $('#countrydropdiv .coutrylist')[0].style.marginLeft = "6px";
                    //$("#PostalAddressCountryDropDownList-list").css("margin-left", "8px");
                    //$('#countrydropdiv span[unselectable="on"]').css('width', '120px');
                    // $('#countrydropdiv .k-dropdown-wrap').css('width', '120px');
                    //$('#countrydropdiv .k-dropdown-wrap').css('box-shadow', '0px 1px 0px 0px #CCC');
                    $('#EnterCountryDiv').show();
                    $('#statediv').hide();
                    $('#OtherCountryTextBox').focus();
                    drpcount = 3;
                }
                else {
                    // $("#PostalAddressCountryDropDownList-list").css("margin-left", "8px");
                    $('#countrydropdiv .coutrylist')[0].style.marginLeft = "6px";
                    //$('#countrydropdiv span[unselectable="on"]').css('width', '205px');
                    //$('#countrydropdiv .k-dropdown-wrap').css('width', '205px');
                    //$('#countrydropdiv .k-dropdown-wrap').css('box-shadow', '1px 1px 1px 1px #CCC');
                    $('#statediv').hide();
                    $('#EnterCountryDiv').hide();
                    drpcount = 0;
                }
            }
        }

        function Emailblurstyle(id) {
            if (id.value != "") {
                var records=0; // = httpGet(GetFile('/DesktopModules/SignUp/Components/Handlers/GetErrorMessage.ashx?request=error&data=' + id.value));
                $.ajax({
                    url: GetFile('/DesktopModules/eCollection_Dashboards/Handlers/Dashboard_Handler.ashx?pageContent=error&data=' + id.value),
                    dataType: "json",
                    success: function (value) {
                        records = value;
                        if (records != 0) {
					            var emailId = document.getElementById("signupemailerrorDiv");
					            var msg = 'EMAIL ID ALREADY REGISTERED.';
					            EmptyErrorMessageGenerate(msg, emailId);
					            id.focus();
					            $("#signupemailerrorDiv").show();
					        }
					        else {
					            $("#signupemailerrorDiv").hide();
					        }
					    }                    
                });
               
            }
            if ($(id)[0].value == '') { $(id)[0].value = 'eg: name@domain.com'; $(id).css('font-style', 'italic'); }
        }
        function focusstyle(id) {
            if ($(id)[0].value == 'eg: name@domain.com') {
                $(id)[0].value = '';
                $(id).css('font-style', 'normal');
            }
        }
        function line1blurstyle(id) {
            if ($(id)[0].value == '') { $(id)[0].value = 'line 1'; $(id).css('font-style', 'italic'); }
        }

        function line2focusstyle(id) {
            if ($(id)[0].value == 'line 2') {
                $(id)[0].value = '';
                $(id).css('font-style', 'normal');
            }
        }
        function validatePC(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if ((keycode < 48 || keycode > 57)) { return false; }
        }
        function line1focusstyle(id) {
            if ($(id)[0].value == 'line 1') {
                $(id)[0].value = '';
                $(id).css('font-style', 'normal');
            }
        }
        function line2blurstyle(id) {
            if ($(id)[0].value == '') { $(id)[0].value = 'line 2'; $(id).css('font-style', 'italic'); }
        }
    </script>
    <div id="RequestAccessContentDiv">
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
                    <label for="NameTextBox" >Your name:</label></div>
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
            <div class="starMandatory" style="padding: 15px 0px 12px 15px;">
                *
            </div>
            <div id="SignupPartnerPhoneerrorDiv" style="margin-top: 6px;">
            </div>
            <h4 style="margin: 0px; width: 50%; font-size: 12pt !important; font-weight: 800 !important;
                font-family: Raleway, Arail !important;">
                School information</h4>
            <div class="labelledtxtbox" style="margin-top: 12px;">
                <div class="lblStyle" style="padding-right: 16px;" id="Div1">
                    <label for="SchoolName">School name:</label></div>
                <div class="inputStyle" style="">
                    <input type="text" id="SchoolName" isvalidate="true" name="firstName" class="RequestTextCommon"
                        maxlength="50" runat="server" clientidmode="Static" onfocus="javascript:changestyle(this)" /></div>
            </div>
            <div class="starMandatory" style="padding: 22px 0px 10px 15px;">
                *
            </div>
            <div id="SchoolNameerrorDiv">
            </div>
            <div style="float: left; width: 414px; border: 1px solid #CCC; margin-bottom: 35px;
                margin-top: 12px;">
                <div class="lblStyle" id="Div4">
                    <label for="SchoolPhoneNumber">School phone:</label> </div>
                <div class="inputStyle" style="">
                    <input type="text" maxlength="15" id="SchoolPhoneNumber" runat="server" clientidmode="Static"
                        name="firstName" class="RequestTextCommon" onfocus="javascript:changestyle(this)"
                        onkeypress="return validatePHN(event)" /></div>
            </div>
            <div class="starMandatory" style="padding: 15px 0px 12px 15px;">
                *
            </div>
            <div id="SchoolPhoneNumbererrorDiv">
            </div>
            <div class="titDiv">
                <h4 style="margin: 0px; font-size: 12pt !important; font-weight: 800 !important;
                    font-family: Raleway, Arail !important;">
                    Address :</h4>
                <div style="float: left; width: 100%; margin-top: -10px;">
                    <div>
                        <div style="float: left; width: 414px; border: 1px solid #CCC; margin-bottom: 20px;
                            margin-top: 20px;">
                            <div class="lblStyle" id="Div13">
                                <label for="SignUpPartnerAddress">Address:</label> </div>
                            <div class="inputStyle" style="">
                                <input type="text" style="width: 300px;" id="SignUpPartnerAddress" name="firstName"
                                    runat="server" clientidmode="Static" class="RequestTextCommon" maxlength="100"
                                    onclick="javascript:changestyle(this)" value="line 1" onblur="javascript:line1blurstyle(this);"
                                    onfocus="javascript:line1focusstyle(this);" /></div>
                        </div>
                        <div class="starMandatory" style="margin-top: 20px; padding: 15px 0px 12px 15px;
                            display: block;">
                            *
                        </div>
                        <div class="inputStyle" style="float: left; margin-top: -18px; width: 420px;">
                            <div class="lblStyle" style="width: 52px; box-shadow: none; border: none; background: none !important;
                                filter: none !important;" id="Div3">
                            </div>
                            <div class="inputStyle" id="LineAddressBox" style="width: 338px; margin-left: 0px; border: 1px solid lightgray;
                                margin-top: -3px;">
                                <input type="text" style="height: 22px; width: 320px; margin-top: 5px;" maxlength="100"
                                    id="Line1Address" runat="server" clientidmode="Static" name="firstName" class="RequestTextCommon"
                                    onclick="javascript:changestyle(this)" value="line 2" onblur="javascript:line2blurstyle(this);"
                                    onfocus="javascript:line2focusstyle(this);" /></div>
                        </div>
                        <div id="SignUpPartnerAddresserrorDiv">
                        </div>
                    </div>
                    <div>
                        <div style="float: left; width: 215px; margin-right: 7px; border: 1px solid #CCC;
                            margin-bottom: 20px; margin-top: 12px;">
                            <div class="lblStyle" id="Div14">
                                <label for="SuburbTextBox">Suburb:</label> </div>
                            <div class="inputStyle">
                                <input type="text" style="width: 130px;" maxlength="20" id="SuburbTextBox" name="firstName"
                                    runat="server" clientidmode="Static" class="RequestTextCommon" onfocus="javascript:changestyle(this)" /></div>
                        </div>
                        <div style="float: left; width: 185px; border: 1px solid #CCC; margin-bottom: 20px;
                            margin-top: 12px;">
                            <div class="lblStyle" id="Div15">
                                <label for="PostCodeTextBox">Postcode:</label> </div>
                            <div class="inputStyle" style="">
                                <input type="text" style="width: 75px;" maxlength="9" id="PostCodeTextBox" name="firstName"
                                    runat="server" clientidmode="Static" class="RequestTextCommon" onfocus="javascript:changestyle(this)"
                                    onkeypress="return validatePC(event)" /></div>
                        </div>
                        <div class="starMandatory" style="padding: 15px 0px 12px 15px;
                            display: block;">
                            *
                        </div>
                        <div id="ShippingAddressErrorDiv" style="margin-top: -16px;">
                        </div>
                    </div>
                </div>
                <br />
                <div style="float: left; width: 94%; margin-top: -10px; margin-left: -6px; height: 43px;">
                    <div style="border-radius: 3px; margin-right: 1%; height: 28px; float: left; width: 35%;
                        border: 1px solid lightgrey; background-color: White; display: none;">
                        <div style="float: left; width: 97%;">
                            <input id="" type="text" maxlength="4" class="psttextbox" onkeypress="return validatePC(event)" />
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="psttextbox" Style="display: none;">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div id="statediv" style="float: left; margin-right: 10px; width: 41%; height: 33px;">
                        <select id="PostalAddressStateDropDownList" runat="server" clientidmode="Static"
                            class="statelist">
                            <option class="text" value="1">NSW</option>
                            <option class="text" value="2">VIC</option>
                            <option class="text" value="3">QLD</option>
                            <option class="text" value="4">SA</option>
                            <option class="text" value="5">WA</option>
                            <option class="text" value="6">TAS</option>
                            <option class="text" value="7">NT</option>
                            <option class="text" value="8">ACT</option>
                        </select>
                    </div>
                    <div id="countrydropdiv" class="CountryDropdwnClass">
                        <asp:DropDownList ID="PostalAddressCountryDropDownList" ClientIDMode="Static" DataTextField="COUNTRY_NAME"
                            DataValueField="COUNTRY_CODE" runat="server" CssClass="coutrylist">
                        </asp:DropDownList>
                    </div>
                    <div id="EnterCountryDiv" style="display: none; height: 36px; width: 247px; border: 1px solid lightgrey;
                        background-color: white; margin-left: 171px; margin-top: 3px; padding: 0.5px;">
                        <div style="width: 265px;">
                            <input id="OtherCountryTextBox" runat="server" clientidmode="Static" type="text"
                                class="sgntextbox text" style="width: 232px;font-style: italic;height: 22px;padding-top: 5px;" value="Please specify which country"
                                onblur="javascript:countryblurstyle(this);" onfocus="javascript:countryfocusstyle(this);" />
                        </div>
                        <div class="starMandatory countrymandatory" style="display: none;">
                            *
                        </div>
                    </div>
                    <div id="OtherCountryTextBoxerrorDiv" class="OtherCountryTextBoxerrorDiv othercountrymrgn">
                    </div>
                </div>
            </div>
            <div style="float: left; width: 102%; margin-top: 70px;">
                <h4 style="margin: 0px; font-size: 12pt !important; font-weight: 800 !important;
                    font-family: Raleway, Arail !important;">
                    Comments :</h4>
                <asp:TextBox ID="CommentsTextArea" MaxLength="100" Style="min-height: 250px; width: 100%  !important;margin-top: 10px;
                    resize: none; border: 1px solid lightgray;" TextMode="MultiLine" runat="server"></asp:TextBox>
                <asp:Button ID="RequestEarlyAccessbutton" runat="server" ClientIDMode="Static" OnClick="RequestEarlyAccessbutton_Click"
                    CssClass="ActiveAddButtonsHolder RequestAccessButton" Text="REQUEST EARLY ACCESS" />
            </div>
        </div>
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="RequestAccessConfirmationContent" Visible="false" runat="server">
    <h2 style="font-weight: 700; color: #0089c5 !important; font-family: Raleway, Arial !important;">
        We have received your request for early access<br />
        and will be in contact shortly.</h2>
</asp:PlaceHolder>
