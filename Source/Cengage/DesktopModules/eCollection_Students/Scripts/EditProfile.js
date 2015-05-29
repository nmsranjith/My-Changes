
var swicthPopUp;
jQuery(function () {
    if ($('#RoleChkHdn').val() == 'true') {
        jQuery(".ECollLeftModule").css('margin-top', '-355px');
    }
    else {
        jQuery(".ECollLeftModule").css('margin-top', '-255px');
    }
	swicthPopUp=$('#move-popup');
	$('#SwitchCancelBtn').click(function(){
		swicthPopUp.data("kendoWindow").close();
        return false;
	});

    if (jQuery('#DOBHdFld').val().trim() != '')
        jQuery('#DateofBirthTextBox').val(jQuery('#DOBHdFld').val().trim());
    if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
        if (jQuery('#DateofBirthTextBox').val().trim() == '')
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

    jQuery('#eCollectionContent').addClass('EditPageContent');
    jQuery('#eCollectionMenu').addClass('EditPageMenu');

    $('#BackToProfileBtn').removeClass('srtbtnshide');		
		
	$('#MoveSubscriptionsDpn').change(function(){			
		if($(this).val()==-1111)
		{	
			$(this).val($('#CurrentSubsSk').val());
			jQuery(this).kendoDropDownList({ animation: false });
			$('#MoveSubscriptionsDpn_listbox .k-item').each(function(){
				$(this).html($(this).html().replace(/^#RJ9098#|#RJ9098#$/g, "<span class='redtext'>(Subscription is full)</span>"));
			});
		}
	});
		
    jQuery('.Div_FullWidth #GradeDropDown').kendoDropDownList({ animation: false });
    jQuery('.Div_FullWidth #GenderDropDown').kendoDropDownList({ animation: false });
	jQuery('#MoveSubscriptionsDpn').kendoDropDownList({ animation: false });

	$('#MoveSubscriptionsDpn_listbox .k-item').each(function(){
		$(this).html($(this).html().replace(/^#RJ9098#|#RJ9098#$/g, "<span class='redtext'>(Subscription is full)</span>"));
	});
			
    jQuery("#StudentsTabHolder").css("position", "relative");
    jQuery("#StudentsTabHolder").css("z-index", "100000");

    ValidateESL();
    ValidateRR();
        
    jQuery('#SaveStudentProfile').click(
    function () {
            
        $("#MsgDiv").removeAttr('class');
        $("#MsgDiv").text('');
        var err = 0;
        if (ValidateText('NameTextBox')) {
            jQuery('#NameTextBox').parent().parent(".eCollectionEditDiv").css('border', '1px solid lightgrey');
        }
        else {
            err++;
            jQuery('#NameTextBox').focus();
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
                
            if (checkForName('NameTextBox', 'VALIDATE_FIRSTNAME')) {
                if (checkForName('LastNameTextBox', 'VALIDATE_LASTNAME')) {
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
                                $("#MsgDiv").css('height', 'auto');
                                return false;
                            }
                        }
                        else {
                            $("#MsgDiv").css('height', 'auto');
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
        return false;
    }
    );
});
function SwitchSubscriptions()
{
	var newSubsSk=0;
	newSubsSk=$('#MoveSubscriptionsDpn').val();
	if(newSubsSk!=-1111)
	{
		if($('#CurrentSubsSk').val()!=newSubsSk)
		{
			$.ajax({
			url: GetFile('/DesktopModules/eCollection_Students/Handlers/eCollectionHandler.ashx?autocomplete=switchsubscription&SName='+$('#StudentUserName').val()+'&studentsk=' + $('#StudentSk').val()+'&newsubssk=' + newSubsSk),
			dataType: "json",
			async:false,
			success: function (value) {
				if(value==1)
					window.location.href=$('#AfterSwitchUrl').text();
			}
		});
		}
	}
	else
	{
			
	}
}
function ChangeStudentSubscription()
{
	$("#move-popup").css({ 'display': 'block' });
    $('.k-window-actions.k-header').css('cursor', 'pointer');
    swicthPopUp = $("#move-popup"); 
    if (!swicthPopUp.data("kendoWindow")) {
        swicthPopUp.kendoWindow({
            modal: true,
            draggable: false
        });
        swicthPopUp.data("kendoWindow").center();
    }
    swicthPopUp.data("kendoWindow").open();
    $(".k-icon.k-i-close").hide();
    $('a.k-window-action.k-link').mouseover(function () {
        $('a.k-window-action.k-link').parent().addClass("popupClosebg");
        return false;
    });
}

