
    $(document).ready(function () {

        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
        if ($.browser.mozilla) {
            $('#SortingButton').css('margin-top', '1px');
            $('.LeftLineDiv').css('margin-top', '-2px');
        }
    });
    
    function PostBack() {
	
	$('.gchkbx span').click(function(){
		var selGrps='';
		var attr = $(this).attr('class');
		if (typeof attr !== typeof undefined && attr !== false && attr!='eg-hide') { 
			if(attr.trim()=='ico-uncheck')		
				$(this).removeClass('ico-uncheck').addClass('ico-check');			
			else
				$(this).removeClass('ico-check').addClass('ico-uncheck');
			$(this).parent().children('input[type="checkbox"]').click();
		}
		else
		return;
		var chkBxs=0,cChkBxs=0,type='';
		if($('#RepeaterClassDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#RepeaterClassDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#RepeaterClassDiv input:checked').length;
			$('#RepeaterClassDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();			
				type='C';				
			});
			
		}
		if($('#RepeaterGroupDiv').is(':visible'))
		{
			chkBxs=chkBxs+$('#RepeaterGroupDiv input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#RepeaterGroupDiv input:checked').length;
			$('#RepeaterGroupDiv input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();
				type='G';
			});			
		}
		if($('#AllotherGroupContent').is(':visible'))
		{
			chkBxs=chkBxs+$('#AllotherGroupContent input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#AllotherGroupContent input:checked').length;
			$('#AllotherGroupContent input:checked').each(function(){
			if($(this).next().text()!='undefined')
				selGrps=selGrps+','+$(this).next().text();
				type='G';
			});			
		}
		if($('#AllotherClassContent').is(':visible'))
		{
			chkBxs=chkBxs+$('#AllotherClassContent input[type="checkbox"]').length;
			cChkBxs=cChkBxs+$('#AllotherClassContent input:checked').length;
			$('#AllotherClassContent input:checked').each(function(){
				selGrps=selGrps+','+$(this).next().text();
				type='C';
			});
		}
			
		if(chkBxs==cChkBxs)
			$('#SelectAllChkbx').removeClass('ico-uncheck').addClass('ico-check');	
		else
			$('#SelectAllChkbx').removeClass('ico-check').addClass('ico-uncheck');	
			
		if(cChkBxs==1)
		{		
			selGrps=selGrps+','+type;
			$('#EditGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
			$('#EditGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
			$('#DeleteGroupDiv').addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteGroupButton').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');
			$('#StartReadingSessionDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
			$('#StartReadingSessionButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
			$('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
		}
		else if (cChkBxs==0)
		{
			$('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			$('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			$('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
			$('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
			$('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
		}
		else
		{
			$('#MergeGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
			$('#MergeGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
			$('#DeleteGroupDiv').addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteGroupButton').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');
			$('#StartReadingSessionDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
			$('#StartReadingSessionButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
			$('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
		}
		selGrps=selGrps.replace(/^,|,$/g, "");
		$('#selectedGroupID').val(selGrps);
	});
	$('#SelectAllChkbx').click(function(){
		var selGrps='';
		if($(this).attr('class').trim()=='ico-uncheck')	
		{
			var cChkBxs=0;
			$(this).removeClass('ico-uncheck').addClass('ico-check');	
			if($('#RepeaterClassDiv').is(':visible'))
			{
				$('#RepeaterClassDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-check' && attr!='eg-hide') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#RepeaterClassDiv input:checked').length;
				$('#RepeaterClassDiv input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if($('#RepeaterGroupDiv').is(':visible'))
			{
				$('#RepeaterGroupDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-check' && attr!='eg-hide') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#RepeaterGroupDiv input:checked').length;
				$('#RepeaterGroupDiv input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if($('#AllotherGroupContent').is(':visible'))
			{
				$('#AllotherGroupContent .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false&& attr != 'ico-check' && attr!='eg-hide') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#AllotherGroupContent input:checked').length;
				$('#AllotherGroupContent input:checked').each(function(){
				if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if($('#AllotherClassContent').is(':visible'))
			{
				$('#AllotherClassContent .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-check' && attr!='eg-hide') { 
						$(this).removeClass('ico-uncheck').addClass('ico-check');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
				cChkBxs=cChkBxs+$('#AllotherClassContent input:checked').length;
				$('#AllotherClassContent input:checked').each(function(){
					if($(this).next().text()!='undefined')
					selGrps=selGrps+','+$(this).next().text();
				});
			}
			
			if(cChkBxs==1)
			{
				$('#EditGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#EditGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
				$('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			}
			else
			{
				$('#MergeGroupDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
				$('#MergeGroupButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
				$('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
				$('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			}
			$('#DeleteGroupDiv').addClass("ActiveDeleteButtonHolder").removeClass("DisabledDeleteButtonHolder");
			$('#DeleteGroupButton').removeAttr("disabled", "disabled").addClass("CancelBtn").removeClass('DbldDelBtn');
			$('#StartReadingSessionDiv').addClass("ActiveAddButtonsHolder").removeClass("DisabledAddButtonHolder");
			$('#StartReadingSessionButton').removeAttr("disabled", "disabled").addClass("BtnStyle").removeClass('DbldBtn');
		}
		else
		{
			$(this).removeClass('ico-check').addClass('ico-uncheck');
			if($('#RepeaterClassDiv').is(':visible'))
			{
				$('#RepeaterClassDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr!='eg-hide') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			
			if($('#RepeaterGroupDiv').is(':visible'))
			{
				$('#RepeaterGroupDiv .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr!='eg-hide') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			
			if($('#AllotherGroupContent').is(':visible'))
			{
				$('#AllotherGroupContent .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr!='eg-hide') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			if($('#AllotherClassContent').is(':visible'))
			{
				$('#AllotherClassContent .gchkbx span').each(function(){
					var attr = $(this).attr('class');
					if (typeof attr !== typeof undefined && attr !== false && attr != 'ico-uncheck' && attr!='eg-hide') { 
						$(this).removeClass('ico-check').addClass('ico-uncheck');
						$(this).parent().children('input[type="checkbox"]').click();
					}
				});
			}
			$('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			$('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			$('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
			$('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
			$('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
			$('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
			selGrps='';
		}	
		selGrps=selGrps.replace(/^,|,$/g, "");
		$('#selectedGroupID').val(selGrps);
	});
		
jQuery("#groupssortdpn").kendoDropDownList({
		animation: false
	});
        $('#ClassButton').click(function (e) {
            e.preventDefault();
            if ($('#RepeaterClassDiv').css('display') == 'block') {
                $('#RepeaterClassDiv').fadeToggle('slow', 'linear');
                $('#ClassButton').removeClass("AllOtherGroupBtn AllOtherGroupDnBtn").addClass("AllOtherGroupBtn");
            }
            else {
                $('#RepeaterClassDiv').fadeToggle('slow', 'linear');
                $('#ClassButton').removeClass("AllOtherGroupBtn").addClass("AllOtherGroupBtn AllOtherGroupDnBtn");
            }
            setHeight();
        });

        $('#GroupButton').click(function (e) {
            e.preventDefault();
            if ($('#RepeaterGroupDiv').css('display') == 'block') {
                $('#RepeaterGroupDiv').fadeToggle('slow', 'linear');
                $('#GroupButton').removeClass("AllOtherGroupBtn AllOtherGroupDnBtn").addClass("AllOtherGroupBtn");
            }
            else {
                $('#RepeaterGroupDiv').fadeToggle('slow', 'linear');
                $('#GroupButton').removeClass("AllOtherGroupBtn").addClass("AllOtherGroupBtn AllOtherGroupDnBtn");
            }
            setHeight();
        });

        $('#AllOtherGroupButton').click(function (e) {
            e.preventDefault();
            if ($('#AllotherGroupContent').css('display') == 'block') {
                $('#AllotherGroupContent').fadeToggle('slow', 'linear');
                $('#AllOtherGroupButton').removeClass("AllOtherGroupBtn AllOtherGroupDnBtn").addClass("AllOtherGroupBtn");
            }
            else {
                $('#AllotherGroupContent').fadeToggle('slow', 'linear');
                $('#AllOtherGroupButton').removeClass("AllOtherGroupBtn").addClass("AllOtherGroupBtn AllOtherGroupDnBtn");
            }
            setHeight();
        });
        $('#AllOtherClassButton').click(function (e) {
            e.preventDefault();
            if ($('#AllotherClassContent').css('display') == 'block') {
                $('#AllotherClassContent').fadeToggle('slow', 'linear');
                $('#AllOtherClassButton').removeClass("AllOtherGroupBtn AllOtherGroupDnBtn").addClass("AllOtherGroupBtn");
            }
            else {
                $('#AllotherClassContent').fadeToggle('slow', 'linear');
                $('#AllOtherClassButton').removeClass("AllOtherGroupBtn").addClass("AllOtherGroupBtn AllOtherGroupDnBtn");
            }
            setHeight();

        });

        var version = getInternetExplorerVersion();
        if ($("#AllOtherGroupBtnDiv")[0].style.display == "none") {
            $("#GroupContentDivID")[0].style.paddingBottom = "0px";
            $("#AllOtherGroupBtnDivID")[0].style.display = "none";
            $("#allotherLeftLineDiv")[0].style.display = "none";
        }

        if ($("#AllOtherClassBtnDiv")[0].style.display == "none") {
            $("#AllOtherGroupBtnDivID")[0].style.paddingBottom = "0px";
            $("#AllOtherClassBtnDivID")[0].style.display = "none";
        }

        if ($("#ClassContainer")[0].style.display == "none") {
            $(".GroupContentDiv")[0].style.display = "none";
            $(".LeftSecondLineDiv")[0].style.display = "none";
            $("#GroupsContainer")[0].style.marginTop = "-82px";
        }
        else {
            $(".LeftSecondLineDiv")[0].style.display = "block";
        }
        if ($("#GroupsContainer")[0].style.display == "none") {
            $("#GroupContentDivID")[0].style.display = "none";
            $(".GroupContentDiv")[0].style.paddingBottom = "0px";
            $("#allotherLeftLineDiv")[0].style.display = "block";
            $("#AllOtherGroupBtnDiv")[0].style.marginTop = "-24px";
        }
        else {
            $(".GroupContentDiv")[0].style.display = "block";
            $(".LeftSecondLineDiv")[0].style.display = "block";
        }
        if ($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherGroupBtnDiv")[0].style.display == "none") {
            $("#LeftLineDivBorder")[0].style.display = "none";
            $("#allotherLeftLineDiv")[0].style.display = "none";
            if ($("#AllOtherClassBtnDiv")[0].style.display != "none") {
                $("#AllOtherClassBtnDiv").prev()[0].style.display = "block";
                $('#AllOtherClassBtnDiv').prev()[0].style.height = "33px"
                $("#AllOtherClassBtnDiv")[0].style.marginTop = "-24px";
            }

        }
        if ($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherGroupBtnDiv")[0].style.display != "none") {
            $("#LeftLineDivBorder")[0].style.display = "none";
            $("#AllOtherGroupBtnDiv")[0].style.marginTop = "-24px";
        }
        if ($("#AllOtherGroupBtnDiv")[0].style.display == "none" && $("#GroupsContainer")[0].style.display == "none") {
            $("#allotherLeftLineDiv")[0].style.display = "none";
        }

        if (($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display != "none" && $("#AllOtherGroupBtnDiv")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none") || ($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherGroupBtnDiv")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none") || ($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherGroupBtnDiv")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none")) {
            $("#allotherLeftLineDiv")[0].style.display = "block";
            $("#AllOtherClassBtnDiv")[0].style.marginTop = "-24px";
        }

        if (($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display != "none" && $("#AllOtherGroupBtnDiv")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none") || ($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherGroupBtnDiv")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none") || ($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display != "none" && $("#AllOtherGroupBtnDiv")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none")) {
            $("#allotherLeftLineDiv")[0].style.display = "block";
            $("#AllOtherClassBtnDiv")[0].style.marginTop = "-24px";
        }
        if ($("#AllOtherGroupBtnDiv")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none") {
           // $("#AllOtherClassBtnDiv")[0].style.marginTop = "-55px";
        }
        if ($("#ClassContainer")[0].style.display == "none" && $("#GroupsContainer")[0].style.display != "none" && ($("#AllOtherGroupBtnDiv")[0].style.display != "none" || $("#AllOtherClassBtnDiv")[0].style.display != "none")) {
            if ($("#AllOtherGroupBtnDiv")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none")
                $("#AllOtherGroupBtnDiv")[0].style.marginTop = "-67px";

            else if ($("#AllOtherGroupBtnDiv")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none")
                $('#RepeaterClassDiv').parent()[0].style.marginBottom = "0px";
        }

        if ($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display != "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none"  && $("#AllOtherGroupBtnDiv")[0].style.display == "none"){
        $("#GroupContentDivID")[0].style.paddingBottom = "12px";
        }
        if (($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none" && $("#AllOtherGroupBtnDiv")[0].style.display != "none") || ($("#ClassContainer")[0].style.display != "none" && $("#GroupsContainer")[0].style.display == "none" && $("#AllOtherClassBtnDiv")[0].style.display != "none" && $("#AllOtherGroupBtnDiv")[0].style.display == "none")) {
        $('#GroupsContainer').prev()[0].style.paddingBottom = "12px";
        $('#GroupsContainer').prev()[0].style.marginBottom = "7px";
        }

        var searchAutoComplete = $("#SearchTextBox").data("kendoAutoComplete");
        if (searchAutoComplete == undefined) {
            $("#SearchTextBox").kendoAutoComplete({
                dataSource: {
                    transport: {
                        read: {
                            url: GetFile('/DesktopModules/eCollection_Groups/GroupsHandler.ashx?Search=groupAutoComplete'),
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json"
                        }
                    }
                },
                filter: "contains",
                separator: ", ",
                minLength: 1,
                placeholder: "group name"
            });
        }
        var classCount = 0;
        var groupCount = 0;
        var allOtherGroupCount = 0;
        var allOtherClassCount = 0;
        window.onload = function () {
            for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].click();
                    $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";

                }

            }
            for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].click();
                    $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                }
            }

            for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].click();
                    $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                }
            }
            for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                    $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].click();
                    $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                    $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                }
            }

        };
        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
        $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
        $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
        $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
        $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
        if ($.browser.msie || $.browser.mozilla) {
            $("#ReadingLevelButton").css("margin-top", "0px");
            $("#SortingButton").css("margin-top", "0px");
        }
        if ($.browser.msie) {
            var textvalue = $("#SearchTextBox").val();
            $("#SearchTextBox").val('gropup name');
            if (version > -1) {
                if (version != 9.0 && version != 10.0) {
                    if (textvalue.trim().length != 0) {
                        $("#SearchTextBox").val(textvalue);
                    }
                    else {
                        $("#SearchTextBox").val("");
                    }
                    $("#SearchTextBox").css("line-height", "27.5px");
                }
                else {
                    $("#SearchTextBox").val(textvalue);
                }
            }
            if ($("#SearchTextBox").val().trim() == "group name") {
                $("#ClassButton").focus();
            }

        }
        if ($.browser.msie) {
            $("#Boldline").css("margin-top", "1px");
        }
        if ($.browser.mozilla) {
            $("#Boldline").css("margin-top", "-1px");
        }


        

        if ((classCount != 0 || groupCount != 0 || allOtherGroupCount != 0 && allOtherClassCount != 0)) {
            if ((classCount >= 2 || groupCount >= 2 || allOtherGroupCount >= 2 || allOtherClassCount >= 2) || (classCount != 0 && groupCount != 0) || (classCount != 0 && allOtherClassCount != 0) || (groupCount != 0 && allOtherGroupCount != 0) || (groupCount != 0 && allOtherClassCount != 0) || (allOtherClassCount != 0 && allOtherGroupCount != 0) || (allOtherGroupCount != 0 && classCount != 0)) {
                $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
            }
            else {
                $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
            }
            $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
            $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
            $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
            $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
        }
        setHeight();
        jQuery('#GroupsTabHolder').addClass('selectedTabHolder');
        jQuery('#GroupsTab').addClass('selectedTab');
        $('#SearchTextBox').keypress(function (e) {
            $('#SearchTextBox').focus();

            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
        });

        $('#SearchTextBox').keyup(function (e) {

            var code = (e.keyCode ? e.keyCode : e.which);
            if ($("#SearchTextBox").length > 0 && $(this).val() != this.title) {

                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
            else {
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            }
        });


        var checkallFlag = true;
        var popupFlag = false;
        var cnt1 = $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input:checked").length;
        var cnt2 = $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input:checked").length;
        var cnt3 = $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input:checked").length;
        var cnt4 = $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input:checked").length;

        if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length == cnt2 && $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length == cnt1 && $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length == cnt3 && $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length == cnt4 && cnt1 + cnt2 + cnt3 + cnt4 != 0) {
            $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
            checkallFlag = false;
        }
        $("#SelectallCheckBox").click(function () {
            if (checkallFlag) {
                checkallFlag = false;
                var classCheckedCount = 0;
                var groupCheckedCount = 0;
                var allOtherGroupCheckedCount = 0;
                var allOtherClassCheckedCount = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        var selGrpID = $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].parentNode.children[1].innerHTML.trim();
                        if ($('#SelectedClassGrps').val().indexOf(selGrpID) == -1) {
                            $('#SelectedClassGrps').val($('#SelectedClassGrps').val() + ',' + selGrpID)
                        }
                        classCheckedCount++;
                    }
                }

                popupFlag = true;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        var selGrpID = $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].parentNode.children[1].innerHTML.trim();
                        if ($('#SelectedGrps').val().indexOf(selGrpID) == -1) {
                            $('#SelectedGrps').val($('#SelectedGrps').val() + ',' + selGrpID)
                        }
                        groupCheckedCount++;
                    }

                }
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        var selGrpID = $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].parentNode.children[1].innerHTML.trim();
                        if ($('#SelectedAllOtherGrps').val().indexOf(selGrpID) == -1) {
                            $('#SelectedAllOtherGrps').val($('#SelectedAllOtherGrps').val() + ',' + selGrpID)
                        }
                        allOtherGroupCheckedCount++;
                    }
                }
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if (!$("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        var selGrpID = $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].parentNode.children[1].innerHTML.trim();
                        if ($('#SelectedAllOtherCls').val().indexOf(selGrpID) == -1) {
                            $('#SelectedAllOtherCls').val($('#SelectedAllOtherCls').val() + ',' + selGrpID)
                        }
                        allOtherClassCheckedCount++;
                    }
                }


                if (groupCheckedCount != 0 || classCheckedCount != 0 || allOtherGroupCheckedCount != 0 || allOtherClassCheckedCount != 0) {
                    $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                    $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                    $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    if ((groupCheckedCount+classCheckedCount+allOtherGroupCheckedCount+allOtherClassCheckedCount)>=2) {
                        $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');

                    }
                    else {
                        $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    }
                }
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");

            }
            else {
                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                checkallFlag = true;
                $('#SelectedClassGrps').val("");
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv")[i].className = " RepeaterContentScndDiv";
                    }

                }
                $('#SelectedGrps').val("");
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                    }

                }
                $('#SelectedAllOtherGrps').val("");
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                    }
                }
                $('#SelectedAllOtherCls').val("");
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].click();
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv")[i].className = "RepeaterContentScndDiv";
                    }
                }
                $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
                $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
                $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                popupFlag = false;
            }

        });
        $("#MergeGroupButton").click(function () {
            var val = "MergeGroup";
            document.getElementById("PageName").value = val;
        });
        var kwindow;
        $("#EditGroupButton").click(function () {
            var val = "EditGroup";
            if (popupFlag) {
                $("#dialog-message").css({ 'display': 'block' });
                $('.k-window-actions.k-header').css('cursor', 'pointer');
                kwindow = $("#dialog-message"); //Give ur div id here
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
                    $('a.k-window-action.k-link').parent().css("background-image", "url('Portals/0/images/close.png') !important");
                    return false;
                });
                return false;
            }
            document.getElementById("PageName").value = val;
        });
        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].parentNode.parentNode.className = "rowclick RepeaterContentScndDiv";
                        if ($('#SelectedClassGrps').val().indexOf(this.parentNode.children[1].innerHTML) == -1) {
                            $('#SelectedClassGrps').val($('#SelectedClassGrps').val() + ',' + this.parentNode.children[1].innerHTML.trim())
                        }
                        count++;
                    }
                    else {

                    }
                }
                var Groupcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        Groupcheckedcount++;
                    }
                }
                var allGroupcheckedcount = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allGroupcheckedcount++;
                    }
                }
                var allClasscheckedcount = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allClasscheckedcount++;
                    }
                }
                if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length == count && $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length == Groupcheckedcount && $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length == allGroupcheckedcount && $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length == allClasscheckedcount) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }
                if (count + Groupcheckedcount + allGroupcheckedcount + allClasscheckedcount >= 2) {
                    $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');

                }
                else {
                    if (count + Groupcheckedcount + allGroupcheckedcount + allClasscheckedcount != 0) {
                        $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = false;
                    }
                }
                $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
            }
            else {
                var count = 0;
                var SelectedValues = $("#SelectedClassGrps").val().trim().split(',');
                $("#SelectedClassGrps").val("");
                var SelectedID = this.parentNode.children[1].innerHTML.trim();
                for (var i = 0; i < (SelectedValues.length - 1); i++) {
                    if (SelectedValues[i].trim() != SelectedID && SelectedValues[i].trim() != '') {
                        $("#SelectedClassGrps").val($("#SelectedClassGrps").val().trim() + SelectedValues[i].trim() + ",")
                    }
                }

                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");

                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv img")[i].parentNode.parentNode.className = "RepeaterContentScndDiv";
                    }
                }
                var Groupcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        Groupcheckedfor++;
                    }
                }
                var AllotherGroupcheckedfor = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        AllotherGroupcheckedfor++;
                    }
                }
                var AllotherClasscheckedfor = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        AllotherClasscheckedfor++;
                    }
                }
                if (count != 0)
                    checkallFlag = true;
                if (count != 0) {
                    if (count + Groupcheckedfor + AllotherGroupcheckedfor + AllotherClasscheckedfor >= 2) {
                        $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');

                    }
                    else {
                        if (count + Groupcheckedfor + AllotherGroupcheckedfor + AllotherClasscheckedfor != 0) {
                            $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                            $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                            popupFlag = false;
                        }
                    }

                    $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                    $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                    $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                }
                else {
                    if (Groupcheckedfor != 0 || AllotherGroupcheckedfor != 0 || AllotherClasscheckedfor != 0) {
                        if (Groupcheckedfor + AllotherGroupcheckedfor + AllotherClasscheckedfor >= 2) {
                            $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                            $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        }
                        else {
                            $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                            $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        }
                        $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                        $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                        $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    }
                    else {
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
                        $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
                        $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    }
                }

            }
        });
        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < jQuery("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].parentNode.parentNode.className = "rowclick RepeaterContentScndDiv";
                        if ($('#SelectedGrps').val().indexOf(this.parentNode.children[1].innerHTML) == -1) {
                            $('#SelectedGrps').val($('#SelectedGrps').val() + ',' + this.parentNode.children[1].innerHTML.trim())
                        }
                        count++;
                    }
                }
                var classcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedcount++;
                    }
                }
                var allOtherGroupChekcedCount = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherGroupChekcedCount++;
                    }
                }
                var allOtherClassChekcedCount = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherClassChekcedCount++;
                    }
                }
                if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length == count && $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length == classcheckedcount && $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length == allOtherGroupChekcedCount && $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length == allOtherClassChekcedCount) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }
                if (count + classcheckedcount + allOtherGroupChekcedCount + allOtherClassChekcedCount >= 2) {
                    $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    popupFlag = true;
                }
                else {
                    if (count + classcheckedcount + allOtherGroupChekcedCount + allOtherClassChekcedCount != 0) {
                        $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = false;
                    }
                }

                $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
            }
            else {
                var count = 0;
                var SelectedValues = $("#SelectedGrps").val().trim().split(',');
                $("#SelectedGrps").val("");
                var SelectedID = this.parentNode.children[1].innerHTML.trim();
                for (var i = 0; i < (SelectedValues.length - 1); i++) {
                    if (SelectedValues[i].trim() != SelectedID && SelectedValues[i].trim() != '') {
                        $("#SelectedGrps").val($("#SelectedGrps").val().trim() + SelectedValues[i].trim() + ",")
                    }
                }

                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                for (var i = 0; i < jQuery("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv img")[i].parentNode.parentNode.className = "RepeaterContentScndDiv";
                    }
                }
                var classcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedfor++;
                    }
                }
                var allOtherGroupChekcedfor = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherGroupChekcedfor++;
                    }
                }
                var allOtherClassChekcedfor = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherClassChekcedfor++;
                    }
                }

                if (count + classcheckedfor + allOtherClassChekcedfor + allOtherGroupChekcedfor != 0) {
                    checkallFlag = true;
                    if (count + classcheckedfor + allOtherClassChekcedfor + allOtherGroupChekcedfor >= 2) {
                        $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = true;
                    }
                    else {
                        if (count + classcheckedfor + allOtherClassChekcedfor + allOtherGroupChekcedfor != 0) {
                            $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                            $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                            popupFlag = false;
                        }
                    }

                    $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                    $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                    $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                }
                else {

                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
                    $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
                    $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                }
            }

        });

        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        if ($('#SelectedAllOtherGrps').val().indexOf(this.parentNode.children[1].innerHTML) == -1) {
                            $('#SelectedAllOtherGrps').val($('#SelectedAllOtherGrps').val() + ',' + this.parentNode.children[1].innerHTML.trim())
                        }
                        count++;
                    }
                }

                var classcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedcount++;
                    }
                }
                var groupcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        groupcheckedcount++;
                    }
                }

                var allOtherClassCheckedCount = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherClassCheckedCount++;
                    }
                }

                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length == count && $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length == classcheckedcount && $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length == groupcheckedcount && $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length == allOtherClassCheckedCount) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }
                if (count + classcheckedcount + groupcheckedcount + allOtherClassCheckedCount >= 2) {
                    $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    popupFlag = true;
                }
                else {
                    if (count + classcheckedcount + groupcheckedcount + allOtherClassCheckedCount != 0) {
                        $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = false;
                    }
                }

                $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
            }
            else {
                var count = 0;
                var SelectedValues = $("#SelectedAllOtherGrps").val().trim().split(',');
                $("#SelectedAllOtherGrps").val("");
                var SelectedID = this.parentNode.children[1].innerHTML.trim();
                for (var i = 0; i < (SelectedValues.length - 1); i++) {
                    if (SelectedValues[i].trim() != SelectedID && SelectedValues[i].trim() != '') {
                        $("#SelectedAllOtherGrps").val($("#SelectedAllOtherGrps").val().trim() + SelectedValues[i].trim() + ",")
                    }
                }

                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].parentNode.parentNode.className = "RepeaterContentScndDiv";
                    }
                }
                var classcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedfor++;
                    }
                }
                var groupcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        groupcheckedfor++;
                    }
                }
                var allOtherClassCheckedfor = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherClassCheckedfor++;
                    }
                }

                if (count + classcheckedfor + groupcheckedfor + allOtherClassCheckedfor != 0) {
                    checkallFlag = true;
                    if (count + classcheckedfor + groupcheckedfor + allOtherClassCheckedfor >= 2) {
                        $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = true;
                    }
                    else {
                        if (count + classcheckedfor + groupcheckedfor + allOtherClassCheckedfor != 0) {
                            $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                            $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                            popupFlag = false;
                        }
                    }


                    $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                    $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                    $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                }
                else {
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
                    $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
                    $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                }

            }
        });

        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img").click(function () {
            this.parentNode.children[0].click();
            if (this.parentNode.children[0].checked) {
                var count = 0;
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv")[i].className = "rowclick RepeaterContentScndDiv";
                        if ($('#SelectedAllOtherCls').val().indexOf(this.parentNode.children[1].innerHTML) == -1) {
                            $('#SelectedAllOtherCls').val($('#SelectedAllOtherCls').val() + ',' + this.parentNode.children[1].innerHTML.trim())
                        }
                        count++;
                    }
                }

                var classcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedcount++;
                    }
                }
                var groupcheckedcount = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        groupcheckedcount++;
                    }
                }

                var allOtherGroupCheckedCount = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherGroupCheckedCount++;
                    }
                }

                if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length == allOtherGroupCheckedCount && $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length == classcheckedcount && $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length == groupcheckedcount && $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length == count) {
                    $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/tick_student.png");
                    checkallFlag = false;
                }
                if (count + classcheckedcount + groupcheckedcount + allOtherGroupCheckedCount >= 2) {
                    $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    popupFlag = true;
                }
                else {
                    if (count + classcheckedcount + groupcheckedcount + allOtherGroupCheckedCount != 0) {
                        $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = false;
                    }
                }

                $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
            }
            else {
                var count = 0;
                var SelectedValues = $("#SelectedAllOtherCls").val().trim().split(',');
                $("#SelectedAllOtherCls").val("");
                var SelectedID = this.parentNode.children[1].innerHTML.trim();
                for (var i = 0; i < (SelectedValues.length - 1); i++) {
                    if (SelectedValues[i].trim() != SelectedID && SelectedValues[i].trim() != '') {
                        $("#SelectedAllOtherCls").val($("#SelectedAllOtherCls").val().trim() + SelectedValues[i].trim() + ",")
                    }
                }

                $("#SelectallCheckBox")[0].src = GetFile("/Portals/0/images/circle_big.png");
                for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        count++;
                    }
                    else {
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/circle_big.png");
                        $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv img")[i].parentNode.parentNode.className = "RepeaterContentScndDiv";
                    }
                }
                var classcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        classcheckedfor++;
                    }
                }
                var groupcheckedfor = 0;
                for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv div img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        groupcheckedfor++;
                    }
                }
                var allOtherGroupCheckedfor = 0;
                for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
                    if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                        $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv img")[i].src = GetFile("/Portals/0/images/tick_student.png");
                        allOtherGroupCheckedfor++;
                    }
                }

                if (count + classcheckedfor + groupcheckedfor + allOtherGroupCheckedfor != 0) {
                    checkallFlag = true;
                    if (count + classcheckedfor + groupcheckedfor + allOtherGroupCheckedfor >= 2) {
                        $('#MergeGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                        $('#MergeGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                        $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                        $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                        popupFlag = true;
                    }
                    else {
                        if (count + classcheckedfor + groupcheckedfor + allOtherGroupCheckedfor != 0) {
                            $('#EditGroupDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                            $('#EditGroupButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                            $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                            $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                            popupFlag = false;
                        }
                    }


                    $('#DeleteGroupDiv').removeClass("DisabledDeleteButtonHolder").addClass("ActiveDeleteButtonHolder");
                    $('#DeleteGroupButton').removeAttr("disabled").removeClass("DbldDelBtn").addClass('CancelBtn');
                    $('#StartReadingSessionDiv').removeClass("DisabledAddButtonHolder").addClass("ActiveAddButtonsHolder");
                    $('#StartReadingSessionButton').removeAttr("disabled").removeClass("DbldBtn").addClass('BtnStyle');
                }
                else {
                    $('#EditGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#EditGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#MergeGroupDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#MergeGroupButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');
                    $('#DeleteGroupDiv').removeClass("ActiveDeleteButtonHolder").addClass("DisabledDeleteButtonHolder");
                    $('#DeleteGroupButton').attr("disabled", "disabled").removeClass("CancelBtn").addClass('DbldDelBtn');
                    $('#StartReadingSessionDiv').removeClass("ActiveAddButtonsHolder").addClass("DisabledAddButtonHolder");
                    $('#StartReadingSessionButton').attr("disabled", "disabled").removeClass("BtnStyle").addClass('DbldBtn');

                }

            }
        });
        $("#PopuOkButton").click(function () { kwindow.data("kendoWindow").close(); return false; });

        $("#classSearchTextBox").change(function () { return true; });
        
    }

    function FillSelectedValues() {
        document.getElementById("selectedClsID").value = "";
        document.getElementById("selectedGrpID").value = "";
        for (var i = 0; i < $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedClsID").value += $("#RepeaterClassDiv #ClassRepeaterDiv #ClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }
        for (var i = 0; i < $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedGrpID").value += $("#RepeaterGroupDiv #GroupsRepeaterDiv #GroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }
        for (var i = 0; i < $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedAllGrpID").value += $("#AllotherGroupContent #AllotherGroupRepeaterDiv #AllotherGroupRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }
        for (var i = 0; i < $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]").length; i++) {
            if ($("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv input[type=checkbox]")[i].checked) {
                document.getElementById("selectedAllClsID").value += $("#AllotherClassContent #AllotherClassRepeaterDiv #AllotherClassRepeaterContentDiv .RepeaterFstCol span")[i].innerHTML.trim() + ",";
            }
        }
    }
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    function setHeight() {
        $('#eCollectionContent').height($('#eCollectionContent').children().height() + 20);
        $('#eCollectionMenu').height($('#eCollectionContent').height() - 12)
    }
