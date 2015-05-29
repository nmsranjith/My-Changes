var srfdkwindow, srfdkwindow1, srfdkwindow2,srfdkwindow4, verifiedcheck = 0,lgnVerification=0,logContent = "Client-side Log Start",sendReqClick=0;
$(function () {

    jQuery("#SRFSemesterDpn").kendoDropDownList({
        animation: false
    });
    jQuery("#SRFResourceDpn").kendoDropDownList({
        animation: false
    })
    jQuery("#SRFContactDpn").kendoDropDownList({
        animation: false
    });
    jQuery("#SRFStateDpn").kendoDropDownList({
        animation: false
    });
    jQuery("#SRFCountry").kendoDropDownList({
        animation: false
    });
    $('.access-dashboard').click(function () {
        $('#updateProgress1').show();
    });
    if (GetQueryStringParams('div') != undefined) {
        switch (GetQueryStringParams('div').toLowerCase()) {
            case "vocational":
                $('#dnn_CENGAGESUBMENU_VocationalLink').addClass('current-menu-parent');
                break;
            case "gale":
                $('#dnn_CENGAGESUBMENU_GaleLink').addClass('current-menu-parent');
                break;
            case "primary":
                $('#dnn_CENGAGESUBMENU_PrimaryLink').addClass('current-menu-parent');
                break;
            case "secondary":
                $('#dnn_CENGAGESUBMENU_SecondaryLink').addClass('current-menu-parent');
                break;
            default:
                $('#dnn_CENGAGESUBMENU_HigherEducation').addClass('current-menu-parent');
                break
        }
    }
    srfdkwindow = $("#WrongRepPopup");
    srfdkwindow1 = $("#SalesRepPopup");
    srfdkwindow2 = $("#SampleBeforeSuccessPopup");
	srfdkwindow4 = $("#SRFInstructorPopUp");
	$('#SRFEmailid').blur(function(){OpenInstructorPopUp()});
	$("#SRFLgnBtn").click(function () {
        srfdkwindow4.data("kendoWindow").close();		
		$('#SRFLgnHdn').val('yes');
		lgnVerification=1;
		SRFLoginClick();
        EndUpdateProgress();
		lgnVerification=0;
        return false
    });
	$("#SRFNoThxButton").click(function () {       
        srfdkwindow4.data("kendoWindow").close();
        EndUpdateProgress();
        return false
    });
	
    $("#SRFOKBtn").click(function () {
        srfdkwindow.data("kendoWindow").close();
        EndUpdateProgress();
        return false
    });
    $("#SRFOKBtn1").click(function () {
        srfdkwindow1.data("kendoWindow").close();
        EndUpdateProgress();
        return false
    });
    $("#SRFCancelButton").click(function () {
        verifiedcheck = 0;
        srfdkwindow2.data("kendoWindow").close();
        EndUpdateProgress();
        return false
    });
    $("#SRFOKBtn2").click(function () {
        srfdkwindow2.data("kendoWindow").close();
        verifiedcheck = 1;
        SRFSubmit();
        return true;
    });
	$('.k-overlay').live('click', function () { 
	EndUpdateProgress();
	$(".k-window").each(function(e){
	  var shoppopup = $(this)[0].children[1].id;
	   var window = $('#' + shoppopup).data("kendoWindow"); if (window) window.close(); 

	});});
    $("#SRFContactDpn").change(function () {
        switch ($(this).val()) {
            case "1":
                $('#SRFPhone').val($('#ContactMobNumber').val());
                $('#SRFPhone').attr("maxlength", "15");
                break;
            case "2":
                $('#SRFPhone').val($('#ContactWorkNumber').val());
                $('#SRFPhone').attr("maxlength", "15");
                break;
            case "3":
                $('#SRFPhone').val($('#SRFEmailid').val());
                $('#SRFPhone').attr("maxlength", "254");
                break;
            default:
                break;
        }
    });
    $("#SRFCountry").change(function () {
        if ($(this).val() == 'AU')
            $('#SRFStateDiv').addClass('ShowItems').removeClass('HideItems');
        else
            $('#SRFStateDiv').addClass('HideItems').removeClass('ShowItems');
        $('#SRFStateError').addClass('HideItems').removeClass('ShowItems');
    });
    if (window.location.href.split('?')[1].indexOf('higher') != -1 && typeof SetFilteration !== 'undefined' && $.isFunction(SetFilteration)) {
        SetFilteration('#HELabel label');
    }

    if (window.location.href.split('?')[1].indexOf('vocational') != -1 && typeof SetFilteration !== 'undefined' && $.isFunction(SetFilteration)) {
        SetFilteration('#VOCATIONALLabel label');
    }
	$('#conSendReq').click(
	function(){
	SRFSubmit();
	});
    var contusheight = $('#ContactUs').height();
    $('.srf-submit-btn').css('margin-top', contusheight + 30 + "px");
});
function SRFConfirmPasswordClick() {
    StartUpdateProgress();
    //$('#overlay').css('display','block');
    //$('#UpdateProgressImg').css('display','block');
    $('#SRFSecondaryPasswordError').removeClass('ShowItems').addClass('HideItems');
    $('#SRFSecondarySorryError').removeClass('ShowItems').addClass('HideItems');
    $('#SRFSecondaryIncorrectError').removeClass('ShowItems').addClass('HideItems');
    var secEmail = $('#SRFSecondaryEmail').val().trim(), secPassword = $('#SRFPassword').val();
    if (secPassword != '') {
        var records = httpGet(GetFile('/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=checkpassword&email=' + secEmail + "&password=" + secPassword));
        if (records != "success") {
            if (records == "invalidpassword") {
               EndUpdateProgress();
				$('#SRFSecondaryIncorrectError').addClass('ShowItems').removeClass('HideItems');                
                logContent = logContent + "\r\n SSO Password --> Incorrect";
            }
            else {
                EndUpdateProgress();
				$('#SRFSecondarySorryError').addClass('ShowItems').removeClass('HideItems');               
                logContent = logContent + "\r\n SSO Password --> We are unable to process  your request at this time.";
            }
            $('#SRFPassword').focus();
            return 1;
        }
        else {
			EndUpdateProgress();
            logContent = logContent + "\r\n SSO Password Validation --> SUCCESS";
        }
    }
    else {       
		EndUpdateProgress();
		$('#SRFSecondaryPasswordError').addClass('ShowItems').removeClass('HideItems');        
        $('#SRFPassword').focus();
        logContent = logContent + "\r\n SSO Password --> EMPTY";
        return 1;
    }
   
	EndUpdateProgress();
	logContent = logContent + "\r\n Student id Linked --> " + secEmail;
	$('#CurrentSecEmailId').text('Currently you are using [' + secEmail + ']');
	$('#SRFSecondaryEmailDiv').addClass('HideItems');
	$('#SRFSecondaryEmailPara').addClass('HideItems');
	$('#SRFSecondaryPasswordDiv').addClass('HideItems');  
    
    LogContent(logContent);    
    return 0;
}
function StartUpdateProgress() {
     $('.load-srf').show();
	  showIELoader();	
}
function EndUpdateProgress() {   
    $('.load-srf').hide();
	if ($.browser.msie && parseInt($.browser.version, 10) < 10) 
	{
		$('#progressBar').hide();
	}
}
function SRFSecondaryEmailTabOut() {
    //$('#overlay').css('display','block');
    //$('#UpdateProgressImg').css('display','block');
    StartUpdateProgress();
    $('#SRFSecondaryCreateEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryProcessEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryUsedEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryValidEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryPasswordDiv').removeClass('ShowItems').addClass('HideItems');
    if ($('#SRFSecondaryEmail').val().trim() == '') {       
		EndUpdateProgress();
		$('#SRFSecondaryEmailError').addClass('ShowItems').removeClass('HideItems');        
        logContent = logContent + "\r\n Secondary Email EMPTY";
        return 1;
    }
    else {
        if (isValidEmailAddress($('#SRFSecondaryEmail'), $('#SRFSecondaryValidEmailError')) == 0) {
            var records = httpGet(GetFile('/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=secondaryemail&data=' + $('#SRFSecondaryEmail').val()));
            if (records != 0) {
				EndUpdateProgress();
				$('#SRFSecondaryUsedEmailError').addClass('ShowItems').removeClass('HideItems');                
                logContent = logContent + "\r\n Secondary Email  already in use";
                return 1;
            }
            else {
                //ssoemailcheck
                var records = httpGet(GetFile('/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=ssoemailcheck&data=' + $('#SRFSecondaryEmail').val()));
                var i='"1"';
				if (records == i) {
                    EndUpdateProgress();
                    $('#SRFSecondaryProcessEmailError').addClass('ShowItems').removeClass('HideItems');                   
                    logContent = logContent + "\r\n Secondary Email --> This email is already associated with another lecturer account. Please enter a different email address. Alternatively you can enter a fake email address. e.g.yourfirstname.yourlastname@mail.com  --> Already linked";
                    return 1;
                }
                else {
                    if (records == "2") {
                        EndUpdateProgress();
						$('#SRFSecondaryPasswordDiv').addClass('ShowItems').removeClass('HideItems');
						$('#SRFPassword').focus();                       
                        logContent = logContent + "\r\n Secondary Email Exists --> Asks SSO Password";
                        return 2;
                    } else {

                    }
                }
            }
        }
        else {
            EndUpdateProgress();
             $('#SRFSecondaryValidEmailError').addClass('ShowItems').removeClass('HideItems');           
            logContent = logContent + "\r\n Secondary Email --> INVALID";
            return 1;
        }
    }
    EndUpdateProgress();
    return 0;
}
function CreateSSOStudentAccount() {
    //$('#overlay').css('display','block');
    //$('#UpdateProgressImg').css('display','block');
    StartUpdateProgress();
    $('#SRFSecondaryCreateEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryProcessEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryUsedEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryValidEmailError').addClass('HideItems').removeClass('ShowItems');
    $('#SRFSecondaryPasswordDiv').removeClass('ShowItems').addClass('HideItems');
    var records = httpGet(GetFile('/DesktopModules/Dashboard/Components/Handlers/DashboardHandler.ashx?key=createssoaccount&createemail=' + $('#SRFSecondaryEmail').val()));
    if (records.indexOf("resource cannot be found") != -1) {        
		EndUpdateProgress();
		$('#SRFSecondaryCreateEmailError').addClass('ShowItems').removeClass('HideItems');
		logContent = logContent + "\r\n Create SSO Student Account Call -->  An error occurred when processing your request.";           
		$('#SRFSecondaryEmail').focus();       
        return 1;
    }
    else {
        logContent = logContent + "\r\n Create SSO Student Account Call --> Success";
    }
    EndUpdateProgress();
    return 0;
}
function GetQueryStringParams(d) {
    var b = window.location.search.substring(1);
    var c = b.split("&");
    for (var a = 0; a < c.length; a++) {
        var f = c[a].split("=");
        if (f[0] == d) {
            return f[1]
        }
    }
}
function SRFSubmit() {   
	try {
	//adddlert("Welcome guest!");	
    window.scroll(0, 0);
    //return false;
    logContent = logContent + "\r\n Field Validation Starts";
    var errCount = 0, locerrcnt = 0;
    errCount = errCount + SRFRequiredValidation($('#SRFInstitution'), $('#SRFInstitutionerror'));
    errCount = errCount + SRFRequiredValidation($('#SRFBookCurrently'), $('#SRFBookCurrentlyerror'));
    errCount = errCount + SRFRequiredValidation($('#SRFSemesterEnrolment'), $('#SRFSemesterEnrolmenterror'));
    errCount = errCount + SRFRequiredValidation($('#SRFcoursesCode'), $('#SRFcoursesCodeerror'));
    errCount = errCount + SRFRequiredValidation($('#SRFcourseName'), $('#SRFcourseNameerror'));
    errCount = errCount + SRFDropdownRequiredValidation($('#SRFSemesterDpn'), $('#SRFSemesterError'));

    if ($('#SRFAddressDiv') != undefined && $('#SRFAddressDiv').text() != "") {
        logContent = logContent + "\r\n SRFAddressDiv EXISTS";
        var UrbCheck = 0, POCheck = 0;
        if ($("#SRFCountry").val() == 'AU') {
            logContent = logContent + "\r\n Country --> AU";
            errCount = errCount + SRFDropdownRequiredValidation($('#SRFStateDpn'), $('#SRFStateError'));
        }
        else {
            logContent = logContent + "\r\n Country --> OTHER THAN AU";
        }

        UrbCheck = UrbCheck + SRFRequiredValidation($('#SRFSuburbTxt'), $('#SRFsubrubpostError'));
        if (UrbCheck == 1) {
            $('#SRFSubUrbErrorSpan').text('Please enter suburb.');
            logContent = logContent + "\r\n Sub Urb not exists, Check for post code";
            POCheck = POCheck + SRFRequiredValidation($('#SRFPostcode'), $('#SRFsubrubpostError1'));
        }
        else {           
            POCheck = POCheck + SRFRequiredValidation($('#SRFPostcode'), $('#SRFsubrubpostError'));
            if (POCheck >= 1) {
                $('#SRFSubUrbErrorSpan').text('Please enter postcode.');
                logContent = logContent + "\r\n Post code not exists";
            }
        }
        errCount = errCount + UrbCheck + POCheck;

        if (UrbCheck + POCheck == 2) {
            $('#SRFSubUrbErrorSpan').text('Please enter suburb and postcode.');
            logContent = logContent + "\r\n Both suburb and Post code not exists";
        }
        UrbCheck = 0; POCheck = 0;

        errCount = errCount + SRFRequiredValidation($('#SRFShippingAddressTxt1'), $('#SRFShippingAddressError1'));
    }
    else {
        logContent = logContent + "\r\n SRFAddressDiv NOT EXISTS";
    }

    locerrcnt = SRFDropdownRequiredValidation($('#SRFContactDpn'), $('#SRFPhoneerror1'));
    errCount = errCount + locerrcnt;
    if (locerrcnt == 0) {
        errCount = errCount + SRFRequiredValidation($('#SRFPhone'), $('#SRFPhoneerror'));
        locerrcnt = 0;
    }
    else {
        $('#SRFPhoneerror').addClass('HideItems').removeClass('ShowItems');
        if ($('#SRFContactDpn').val() == 3) {
            logContent = logContent + "\r\n Contact --> Email address selected";
            errCount = errCount + isValidEmailAddress($('#SRFPhone'), $('#SRFPhoneerror'));
        }
    }

    locerrcnt = SRFRequiredValidation($('#SRFEmailid'), $('#SRFEmailiderror'));
    errCount = errCount + locerrcnt;
    if (locerrcnt == 0) {
        errCount = errCount + isValidEmailAddress($('#SRFEmailid'), $('#SRFEmailInvaliderror'));
        locerrcnt = 0;
    }
    else
        $('#SRFEmailInvaliderror').addClass('HideItems').removeClass('ShowItems');
    errCount = errCount + SRFRequiredValidation($('#SRFPersonalName'), $('#SRFPersonalNameerror'));

    logContent = logContent + "\r\n Field Validation ENDS";
    if (errCount > 0) {
        logContent = logContent + "\r\n Field Validation Status -- > Fail";        
		LogContent(logContent);
        return false;
    }
    else {
        logContent = logContent + "\r\n Field Validation Status -- > Pass";       
        var setRepBtn, salesRepName, salesRepEmail;
        logContent = logContent + "\r\n Sales rep check starts";
        switch (GetQueryStringParams('div')) {
            case "higher":
                logContent = logContent + "\r\n Division --> Higher";
                if ($('#SelectedDiv').text().trim() != 'Higher Education') {
                    logContent = logContent + "\r\n Salesrep Division selection --> WRONG --> Display invalid rep and division selection pop up";
                    OpenInvalidSalesRepPopUp();
					LogContent(logContent);
                    return false;
                }
                else {
                    logContent = logContent + "\r\n Salesrep Division selection --> Correct";
                }
                if ($('#DetailRep').css('display') == 'none') {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> FAIL --> Display rep select request pop up";
                    OpenSalesRepPopUp();
					LogContent(logContent);
                    return false;
                }
                else {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> PASS";
                }
                salesRepName = $('#DetailRep .repname label').text();
                salesRepEmail = $('#DetailRep .address label a').text();
                setRepBtn = $('#DetailRep #SetMYRep');
                logContent = logContent + "\r\n Salesrep Details --> Sales Rep Name : " + salesRepName +  "\t Sales Rep Email : " + salesRepEmail;
                break;
            case "vocational":
                logContent = logContent + "\r\n Division --> Vocational";
                if ($('#SelectedDiv').text().trim() != 'Vocational') {
                    logContent = logContent + "\r\n Salesrep Division selection --> WRONG --> Display invalid rep and division selection pop up";
                    OpenInvalidSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep Division selection --> Correct";
                }
                if ($('#VPG').css('display') == 'none') {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> FAIL --> Display rep select request pop up";
                    OpenSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> PASS";
                }
                salesRepName = $('#VPG .repname label').text();
                salesRepEmail = $('#VPG .address label a').text();
                setRepBtn = $('#VPG #SetMYRep');
                logContent = logContent + "\r\n Salesrep Details --> Sales Rep Name : " + salesRepName + "\t Sales Rep Email : " + salesRepEmail;
                break;
            case "gale":
                logContent = logContent + "\r\n Division --> Gale";
                if ($('#SelectedDiv').text().trim() != 'Gale') {
                    logContent = logContent + "\r\n Salesrep Division selection --> WRONG --> Display invalid rep and division selection pop up";
                    OpenInvalidSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep Division selection --> Correct";
                }
                if ($('#GaleRep').css('display') == 'none') {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> FAIL --> Display rep select request pop up";
                    OpenSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> PASS";
                }
                salesRepName = $('#GaleRep .repname label').text();
                salesRepEmail = $('#GaleRep .address label a').text();
                setRepBtn = $('#GaleRep #SetMYRep');
                logContent = logContent + "\r\n Salesrep Details --> Sales Rep Name : " + salesRepName + "\t Sales Rep Email : " + salesRepEmail;
                break;
            case "secondary":
                logContent = logContent + "\r\n Division --> Secondary";
                if ($('#SelectedDiv').text().trim() != 'Secondary') {
                    logContent = logContent + "\r\n Salesrep Division selection --> WRONG --> Display invalid rep and division selection pop up";
                    OpenInvalidSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep Division selection --> Correct";
                }
                if ($('#secrep').css('display') == 'none') {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> FAIL --> Display rep select request pop up";
                    OpenSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> PASS";
                }
                salesRepName = $('#secrep .repname label').text();
                salesRepEmail = $('#secrep .address label a').text();
                setRepBtn = $('#secrep #SetMYRep');
                logContent = logContent + "\r\n Salesrep Details --> Sales Rep Name : " + salesRepName + "\t Sales Rep Email : " + salesRepEmail;
                break;
            case "primary":
                logContent = logContent + "\r\n Division --> Primary";
                if ($('#SelectedDiv').text().trim() != 'Primary') {
                    logContent = logContent + "\r\n Salesrep Division selection --> WRONG --> Display invalid rep and division selection pop up";
                    OpenInvalidSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep Division selection --> Correct";
                }
                if ($('#prirep').css('display') == 'none') {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> FAIL --> Display rep select request pop up";
                    OpenSalesRepPopUp();
					LogContent(logContent);
                    return false;
                } else {
                    logContent = logContent + "\r\n Salesrep status --> REP SELECTION PAGE OPEN --> PASS";
                }
                salesRepName = $('#prirep .repname label').text();
                salesRepEmail = $('#prirep .address label a').text();
                setRepBtn = $('#prirep #SetMYRep');
                logContent = logContent + "\r\n Salesrep Details --> Sales Rep Name : " + salesRepName + "\t Sales Rep Email : " + salesRepEmail;
                break;
            default:
                break;
        }
       
        window.scrollBy(0, -1000);
        if ($('#RestrictedToSample').is(':visible'))//.css('display')=='block')
        {
            logContent = logContent + "\r\n Salesrep Availablity --> NOT AVAILABLE";      
			OpenSalesRepPopUp();			
			LogContent(logContent);
            return false;
        }
        else {
            logContent = logContent + "\r\n Salesrep Availablity --> AVAILABLE";
        }
        if ($('#UserTypeHdn').val().toUpperCase() != 'VERIFIED') {
            logContent = logContent + "\r\n User Status --> NOT VERIFIED/ANONYMOUS";
            if (verifiedcheck == 1) {
                verifiedcheck = 0;
                logContent = logContent + "\r\n CLOSE BEFORE SUCCESS POP UP";
                logContent = logContent + "\r\n SET SALES REP DETAILS";
                SetSalesRepDetails(salesRepName, salesRepEmail);
				StartUpdateProgress();
                window.scroll(0, 0);
                logContent = logContent + "\r\n CLIENT SIDE END, SERVER SIDE CALL BEGINS";
                LogContent(logContent);
				
                $('#SubmitRequestBtn').click();
            }
            else {
                logContent = logContent + "\r\n OPEN BEFORE SUCCESS POP UP";
                OpenSampleBeforeSuccessPopUp();
            }
        }
        else {
            logContent = logContent + "\r\n User Status --> VERIFIED";
            if (setRepBtn != undefined && setRepBtn.is(':visible'))// setRepBtn.css('display')=='block')
            {
                logContent = logContent + "\r\n SALES REP SELECTION STATUS --> NOT SELECTED --> Open Set sales rep pop up";   
				OpenSalesRepPopUp();				
				LogContent(logContent);
                return false;
            }
            else {
                logContent = logContent + "\r\n SALES REP SELECTION STATUS --> SELECTED";
            }
            logContent = logContent + "\r\n SET SALES REP DETAILS";
            SetSalesRepDetails(salesRepName, salesRepEmail);
            if (SetSecondaryEmail()) {
               StartUpdateProgress();
				 window.scroll(0, 0);
                logContent = logContent + "\r\n CLIENT SIDE END, SERVER SIDE CALL BEGINS";
                LogContent(logContent);
				
                $('#SubmitRequestBtn').click();
            }
            else {                
				LogContent(logContent);
				EndUpdateProgress();
                return false;
            }
        }
        //setTimeout(function(){EndUpdateProgress();}, 1000); 
        return true;
    }
	}
	catch(err) {
		logContent = logContent + "\r\n "+err.message;
		LogContent(logContent);
		EndUpdateProgress();
	}
}
function LogContent(logValue) {   
    var jsonData = {
        'Content': logValue       
    }
    jsonData = JSON.stringify(jsonData);
    $.ajax({
        url: GetFile('desktopmodules/samplerequestform/handlers/SRFHandler.ashx?log=yes'),
        cache: false,
        type: 'POST',
        async: false,
        data: jsonData,
        success: function (data) {		
        }
    });
}
function isDefined(func) {
    if (func !== undefined) return true;
}
function SetSecondaryEmail() {
    logContent = logContent + "\r\n SET SECONDARY EMAIL Function START";
    StartUpdateProgress();
    var errCount = 0;
    var se = 0, sp = 0;
    if ($('#SRFSecondaryEmailDiv') != undefined && $('#SRFSecondaryEmailDiv').text() != "") {
        logContent = logContent + "\r\n SECONDARY EMAIL Validation START";
        var secemailtab = SRFSecondaryEmailTabOut();
        logContent = logContent + "\r\n SECONDARY EMAIL Validation END";
        if (secemailtab == 0) {
            logContent = logContent + "\r\n Create SSO Student Account START";
            errCount = errCount + CreateSSOStudentAccount();
            logContent = logContent + "\r\n Create SSO Student Account END";
        }
        else if (secemailtab == 2) {
            logContent = logContent + "\r\n Confirm password click START";
            if (SRFConfirmPasswordClick() != 0) {
                logContent = logContent + "\r\n Confirm password Validation --> FAIL";
                $('#SRFPassword').focus();
                errCount = errCount + 1;
                sp = 1;
            }
            else {
                logContent = logContent + "\r\n Confirm password Validation --> PASS";
            }
        }
        else {
            if ($('#SRFSecondaryEmailDiv').attr('class') != 'control-group HideItems') {
                logContent = logContent + "\r\n Secondary email Validation --> FAIL";
                $('#SRFSecondaryEmail').focus();
                errCount = errCount + 1;
                se = 1;
            }
        }
    }

    if (errCount > 0) {
        if (sp == 1) {
            logContent = logContent + "\r\n SSO password Validation --> FAIL";
            $('#SRFPassword').focus();
            window.scrollBy(0, -1000);
        }
        if (se == 1) {
            logContent = logContent + "\r\n Secondary email Validation --> FAIL";
            $('#SRFSecondaryEmail').focus();
        }
        setTimeout(function () { EndUpdateProgress(); }, 1000);
        return false;
    }
    else {
        logContent = logContent + "\r\n SET SECONDARY EMAIL Function END";
        return true;
    }
}
function SetSalesRepDetails(salesRepName, salesRepEmail) {
    //salesRepEmail = salesRepEmail.replace("Email: ", "");
    $.ajax({
        url: GetFile('/desktopmodules/SampleRequestForm/handlers/SRFHandler.ashx?SRepName=' + salesRepName.trim() + '&SRepEmail=' + salesRepEmail.trim()),
        type: "POST",
        cache: false,
        async: false,
        success: function () {
        }
    });
}
function OpenSampleBeforeSuccessPopUp() {
    if (!srfdkwindow2.data("kendoWindow")) {
        srfdkwindow2.kendoWindow({
            modal: true,
            draggable: false
        });
    }
    srfdkwindow2.data("kendoWindow").center();
    srfdkwindow2.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    $("a.k-window-action.k-link").click(function () {
        EndUpdateProgress();
    });
}

function OpenInvalidSalesRepPopUp() {
    if (!srfdkwindow.data("kendoWindow")) {
        srfdkwindow.kendoWindow({
            modal: true,
            draggable: false
        });
    }
    srfdkwindow.data("kendoWindow").center();
    srfdkwindow.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    $("a.k-window-action.k-link").click(function () {
        EndUpdateProgress();
    });
}

function OpenSalesRepPopUp() {
    if (!srfdkwindow1.data("kendoWindow")) {
        srfdkwindow1.kendoWindow({
            modal: true,
            draggable: false
        });
    }
    srfdkwindow1.data("kendoWindow").center();
    srfdkwindow1.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $("a.k-window-action.k-link").mouseover(function () {
        return false
    });
    $("a.k-window-action.k-link").click(function () {
        EndUpdateProgress();
    });
}
function SRFDropdownRequiredValidation(dpnId, errorId) {
    if ($(dpnId).val() == '0') {
        window.scroll(0, 0);
        logContent = logContent +"\r\n "+ $(dpnId).attr('id') + " --> Dropdown Validation Fail";
        $(errorId).addClass('ShowItems').removeClass('HideItems');
        return 1;
    }
    else {
        logContent = logContent +"\r\n "+ $(dpnId).attr('id') + " --> " + $(dpnId).val() + " --> Dropdown Validation Pass";
        $(errorId).addClass('HideItems').removeClass('ShowItems');
        return 0;
    }
}
function SRFRequiredValidation(txtid, errorid) {
    if ($(txtid).val().trim() == '') {
        logContent = logContent +"\r\n "+ $(txtid).attr('id') + " --> Required Field Validation Fail";
        window.scroll(0, 0);
        $(txtid).focus();
        $(errorid).removeClass('HideItems').addClass('ShowItems');
        return 1;
    }
    else {
        logContent = logContent +"\r\n "+ $(txtid).attr('id') + " --> " + $(txtid).val() + " --> Required Field Validation Pass";
        $(errorid).addClass('HideItems').removeClass('ShowItems');
        return 0;
    }
}

function SRFLoginClick() {	
	if(lgnVerification==0)
		$('#SRFLgnHdn').val('no');
    //$('#loginlnk').click();	
	OpenLoginWindow($('#loginlnk'));
}
function closeAlertMsg(a) {
    $(a).parent().addClass("HideItems");
}

function isValidEmailAddress(emailAddress, errorid) {
    var email = jQuery(emailAddress).val().trim();
    var pattern = new RegExp(/^\b[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b$/i);
    if (pattern.test(email)) {
        logContent = logContent +"\r\n "+ $(emailAddress).attr('id') + " --> Is Valid Email Address pass";
        $(errorid).addClass('HideItems').removeClass('ShowItems');
        return 0;
    }
    else {
        $(errorid).removeClass('HideItems').addClass('ShowItems');
        logContent = logContent +"\r\n "+ $(emailAddress).attr('id') + " --> " + $(emailAddress).val() + " --> Is Valid Email Address fail";
        return 1;
    }
}

function validationnumeric() {
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
	(event.keyCode == 65 && event.ctrlKey === true) || (event.keyCode >= 35 && event.keyCode <= 39)) {
        return;
    }
    else {
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault();
        }
    }
}

function ValidateDropPasteNumeric(txtId) {
    if (isNaN($(txtId).val().trim()))
        event.preventDefault();
    else
        return true;
}

function SRFContactKeyDown(dpnId) {
    switch ($(dpnId).val()) {
        case "1":
            validationnumeric();
            break;
        case "2":
            validationnumeric();
            break;
        case "3":
            break;
        default:
            break;
    }
}

function OpenInstructorPopUp() {
  $.ajax({
        url: GetFile('/desktopmodules/SampleRequestForm/handlers/SRFHandler.ashx?log=userexists&Email=' + $('#SRFEmailid').val().trim()),
        type: "POST",
        cache: false,
        async: false,
        success: function (role) {		
		var rl='"P"';
			if(role==rl)
			{
				if (!srfdkwindow4.data("kendoWindow")) {
				srfdkwindow4.kendoWindow({
				modal: true,
				draggable: false
				});
				}
				srfdkwindow4.data("kendoWindow").center();
				srfdkwindow4.data("kendoWindow").open();
				$(".k-icon.k-i-close").hide();
				$("a.k-window-action.k-link").mouseover(function () {
				return false
				});
				$("a.k-window-action.k-link").click(function () {
				EndUpdateProgress();
				});
			}
        }
    });
}

function showIELoader() {	
if ($.browser.msie && parseInt($.browser.version, 10) < 10) 
	{
    var pb = document.getElementById("progressBar");
    pb.innerHTML = '<img src="/Portals/0/images/ajax-loader1.gif" class="loadergifsize"/>';
    pb.style.display = 'block';	
	}
}