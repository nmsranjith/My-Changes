    var checkedcount = 0;
    var totalCheckbx = 0;
    function showtabs(tabid, val) {
        if (val == 1)
            $('#' + tabid.id).show();
        else
            $('#' + tabid.id).hide();

        ChangeStyle();
    }
	function AddSelectedGroups()
	{
		$('#SelectedStudentList').html('');
		$("#SelectedValueTextBox").val('');
		$('.gchkbx span').each(function(){
			if($(this).attr('class').trim()=='ico-check')
			{
				var teacherName = $(this).parent().parent().next().children().children().children('span.gausername').text().trim(),
				studID=$(this).parent().parent().children('input[type="checkbox"]').val().trim();				
				$("<li id='S" + studID + "' class=\'SelectedGroupItem\'><span title=" + teacherName + ">" + (teacherName.length > 10 ? teacherName.substring(0, 9) + ' ...' : teacherName) + "</span><span style='display:none'>" +studID+ "</span><a onclick='Remove(this)'>x</a></li>").appendTo("#SelectedStudentList");
				$("#SelectedValueTextBox").val($("#SelectedValueTextBox").val().trim() + studID + ",");
			}
		});
	}
	function AutoSelectGroups()
	{
		var studsList=$("#SelectedValueTextBox").val().split(',');
		for(var i=0;i<studsList.length;i++)
		{
			$('#CheckDiv'+studsList[i]).children().click();		
		}
	}
	
	
    function OnPageLoad() {
	
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
		AddSelectedGroups();
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
		AddSelectedGroups();
	});
	AutoSelectGroups();
	jQuery("#groupssortdpn").kendoDropDownList({
		animation: false
	});
        totalCheckbx = $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').length + $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').length + $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').length;
        $('#SelectedStudentList li.SelectedGroupItem').each(function () {
            // if ($(this).attr('class') == 'SelectedGroupItem') {
            for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]');
                if ($(this).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).hide();
                    $('#' + $(classids)[i].parentNode.children[2].id).show();
                    classids.attr('checked', 'checked');
                    $('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
                    checkedcount++;
                }
            }
            for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]');
                if ($(this).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).hide();
                    $('#' + $(classids)[i].parentNode.children[2].id).show();
                    classids.attr('checked', 'checked');
                    jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
                    checkedcount++;
                }
            }
            for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]');
                if ($(this).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).hide();
                    $('#' + $(classids)[i].parentNode.children[2].id).show();
                    classids.attr('checked', 'checked');
                    jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
                    checkedcount++;
                }
            }
            for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]');
                if ($(this).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).hide();
                    $('#' + $(classids)[i].parentNode.children[2].id).show();
                    classids.attr('checked', 'checked');
                    jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
                    checkedcount++;
                }
            }
            //}
        });
        checkedcount = $('#ClassRepeaterDiv #ClassRepeaterContentDiv  input:checked').length + $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input:checked').length + $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv  input:checked').length + $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv  input:checked').length;
        CheckAllCheck(checkedcount);
    }
    function BackToCreate() {
        var selGrps = '', selStuds = '', selTeachers = '';
        $('#SelectedStudentList li').each(function () {
            if (jQuery(this).attr('class') == 'SelectedGroupItem')
                selGrps = selGrps + ',' + jQuery(this).children().next().html();
            else if (jQuery(this).attr('class') == 'SelectedStudentItem') {
                selStuds = selStuds + ',' + jQuery(this).children().next().html();
            }
            else {
                selTeachers = selTeachers + ',' + jQuery(this).children().next().html();
            }
        });
        $('#SelectedValueGroupTextBox').val(selGrps);
        $('#SelectedValueTextBox').val(selStuds);
        $('#SelectedValueTeacherTextBox').val(selTeachers);
    }
    function Remove(ele) {
        if ($('#' + ele.id).attr('class') == 'SelectedGroupItem') {
            for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]');
                if ($('#' + ele.id).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).show();
                    $('#' + $(classids)[i].parentNode.children[2].id).hide();
                    $('#' + ele.id).remove();
                    $('#' + classids[i].value).remove(); jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
                    return false;
                }
            }
            for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]');
                if ($('#' + ele.id).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).show();
                    $('#' + $(classids)[i].parentNode.children[2].id).hide();
                    $('#' + ele.id).remove();
                    $('#' + classids[i].value).remove();
                    $('#' + classids[i].value).remove(); jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
                    return false;
                }
            }
            for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]');
                if ($('#' + ele.id).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).show();
                    $('#' + $(classids)[i].parentNode.children[2].id).hide();
                    $('#' + ele.id).remove();
                    $('#' + classids[i].value).remove();
                    $('#' + classids[i].value).remove(); jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
                    return false;
                }
            }
            for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
                var classids = $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]');
                if ($('#' + ele.id).children().next().html() == classids[i].value) {
                    $('#' + $(classids)[i].parentNode.children[1].id).show();
                    $('#' + $(classids)[i].parentNode.children[2].id).hide();
                    $('#' + ele.id).remove();
                    $('#' + classids[i].value).remove();
                    $('#' + classids[i].value).remove(); jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "RepeaterContentScndDiv";
                    $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
                    $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
                    $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
                    return false;
                }
            }
        }
        $('#' + ele.id).remove();
        SetContentHeight();
    }

    function RemoveGroup(id, chk, unchk) {
        $('#' + id).remove();
        jQuery(unchk).parent().children("input").removeAttr('checked');
        jQuery(chk).parent().parent()[0].className = "RepeaterContentScndDiv";
        jQuery(chk).show();
        jQuery(unchk).hide();
        if (checkedcount > 0)
            checkedcount--;
        CheckAllCheck(checkedcount);
        $('#G' + id).remove();
        $($(jQuery(unchk).parent().children("input")).parent().parent().children()[3].children[0]).addClass('GpAddStudentRptbtn');
        $($(jQuery(unchk).parent().children("input")).parent().parent().children()[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
        $($(jQuery(unchk).parent().children("input")).parent().parent().children()[3].children[0]).val("ADD");
        SetContentHeight();
    }
    function Check(chk, unchk) {
        
        if ($($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).val() == "ADD") {
            jQuery('#' + chk).parent().children("input").attr('checked', 'checked');
            jQuery('#' + chk).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
            jQuery('#' + chk).hide();
            jQuery('#' + unchk).show();
            checkedcount++;
            CheckAllCheck(checkedcount);
            var GrpName = jQuery('#' + chk).parent().next().next().children().first().text();
            //$('<li class="SelectedGroupItem" id="' + jQuery('#' + chk).parent().children("input").val() + '"><span>' + jQuery('#' + chk).parent().next().next().children().first().text() + '</span></li>').appendTo('#SelectedStudentList');
            $("<li id='" + jQuery('#' + chk).parent().children("input").val() + "' class='SelectedGroupItem'><span>" +
            (GrpName.length > 10 ? GrpName.substring(0, 9) + ' ...' : GrpName) +
            "</span><span style='display:none'>" +
            jQuery('#' + chk).parent().children("input").val() +
            "</span><a onclick='RemoveGroup(" + jQuery('#' + chk).parent().children("input").val() + ',' + chk + ',' + unchk + ");'>x</a></li>").appendTo('#SelectedStudentList');
            $($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).removeClass('GpAddStudentRptbtn');
            $($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).val("REMOVE");
        }
        else {
            $($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).val("ADD");
            jQuery('#' + unchk).click();
        }
        
        SetContentHeight();
    }

    function UnCheck(chk, unchk) {
        jQuery('#' + unchk).parent().children("input").removeAttr('checked');
        jQuery('#' + chk).parent().parent()[0].className = "RepeaterContentScndDiv";
        jQuery('#' + chk).show();
        jQuery('#' + unchk).hide();
        if (checkedcount > 0)
            checkedcount--;
        CheckAllCheck(checkedcount);
        $('#' + jQuery('#' + chk).parent().children("input").val()).remove();
        $('#G' + jQuery('#' + chk).parent().children("input").val()).remove();
        $($(jQuery('#' + unchk).parent().children("input")).parent().parent().children()[3].children[0]).addClass('GpAddStudentRptbtn');
        $($(jQuery('#' + unchk).parent().children("input")).parent().parent().children()[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
        $($(jQuery('#' + chk).parent().children("input")).parent().parent().children()[3].children[0]).val("ADD");
        SetContentHeight();
    }
    function CheckAllCheck(checkedCount) {
        var totalCheckbxCnt = $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length + $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length + $("#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]").length + $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').length;
        if (checkedCount == totalCheckbxCnt && totalCheckbxCnt > 0) {
            jQuery("#CheckAllDiv").hide();
            jQuery("#UnCheckAllDiv").show();
        }
        else {
            jQuery("#CheckAllDiv").show();
            jQuery("#UnCheckAllDiv").hide();
        }
        SetContentHeight();
    }
    function CheckAll() {
        var selStuds = '', selTeachers = '';
        $('#SelectedStudentList li').each(function () {
            if (jQuery(this).attr('class') == 'SelectedStudentItem') {
                selStuds = selStuds + '<li id="' + jQuery(this).attr('id') + '" class="SelectedStudentItem">' + jQuery(this).html() + '</li>';
            }
            else if (jQuery(this).attr('class') == 'SelectedTeacherItem') {
                selTeachers = selTeachers + '<li id="' + jQuery(this).attr('id') + '" class="SelectedTeacherItem">' + jQuery(this).html() + '</li>';
            }
        });
        $('#SelectedStudentList').html('');
        jQuery("#CheckAllDiv").hide();
        jQuery("#UnCheckAllDiv").show();
        checkedcount = totalCheckbx;
        $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").attr('checked', 'checked');
        $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").attr('checked', 'checked');
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').attr('checked', 'checked');
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').attr('checked', 'checked');
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().next().show();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().hide();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().next().show();
        $(selStuds).appendTo('#SelectedStudentList');
        $(selTeachers).appendTo('#SelectedStudentList');

        for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]');
            var grpNames = $('#' + $(classids)[i].parentNode.children[1].id).parent().next().next().children().first().text();
            $(classids).parent().parent()[i].className = "rowclick RepeaterContentScndDiv";

            $("<li id='" + $(classids)[i].value + "' class='SelectedGroupItem'><span>" +
            (grpNames.length > 10 ? grpNames.substring(0, 9) + ' ...' : grpNames) +
            "</span><span style='display:none'>" + $(classids)[i].value +
            "</span><a onclick='RemoveGroup(" + $(classids)[i].value + ',' + $(classids)[i].parentNode.children[1].id + ',' + $(classids)[i].parentNode.children[2].id + ");'>x</a></li>").appendTo('#SelectedStudentList');

            jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
        }
        for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]');
            var grpNames = $('#' + $(classids)[i].parentNode.children[1].id).parent().next().next().children().first().text();
            $(classids).parent().parent()[i].className = "rowclick RepeaterContentScndDiv";

            $("<li id='" + $(classids)[i].value + "' class='SelectedGroupItem'><span>" +
            (grpNames.length > 10 ? grpNames.substring(0, 9) + ' ...' : grpNames) +
            "</span><span style='display:none'>" + $(classids)[i].value +
            "</span><a onclick='RemoveGroup(" + $(classids)[i].value + ',' + $(classids)[i].parentNode.children[1].id + ',' + $(classids)[i].parentNode.children[2].id + ");'>x</a></li>").appendTo('#SelectedStudentList');

            jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
        }
        for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]');
            var grpNames = $('#' + $(classids)[i].parentNode.children[1].id).parent().next().next().children().first().text();
            $(classids).parent().parent()[i].className = "rowclick RepeaterContentScndDiv";

            $("<li id='" + $(classids)[i].value + "' class='SelectedGroupItem'><span>" +
            (grpNames.length > 10 ? grpNames.substring(0, 9) + ' ...' : grpNames) +
            "</span><span style='display:none'>" + $(classids)[i].value +
            "</span><a onclick='RemoveGroup(" + $(classids)[i].value + ',' + $(classids)[i].parentNode.children[1].id + ',' + $(classids)[i].parentNode.children[2].id + ");'>x</a></li>").appendTo('#SelectedStudentList');

            jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
        }
        for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]');
            var grpNames = $('#' + $(classids)[i].parentNode.children[1].id).parent().next().next().children().first().text();
            $(classids).parent().parent()[i].className = "rowclick RepeaterContentScndDiv";

            $("<li id='" + $(classids)[i].value + "' class='SelectedGroupItem'><span>" +
            (grpNames.length > 10 ? grpNames.substring(0, 9) + ' ...' : grpNames) +
            "</span><span style='display:none'>" + $(classids)[i].value +
            "</span><a onclick='RemoveGroup(" + $(classids)[i].value + ',' + $(classids)[i].parentNode.children[1].id + ',' + $(classids)[i].parentNode.children[2].id + ");'>x</a></li>").appendTo('#SelectedStudentList');

            jQuery('#' + $(classids)[i].parentNode.children[1].id).parent().parent()[0].className = "rowclick RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("REMOVE");
        }
        SetContentHeight();
    }
    function UnCheckAll() {
        var selStuds = '', selTeachers = '';
        $('#SelectedStudentList li').each(function () {
            if (jQuery(this).attr('class') == 'SelectedStudentItem') {
                selStuds = selStuds + '<li id="' + jQuery(this).attr('id') + '" class="SelectedStudentItem">' + jQuery(this).html() + '</li>';
            }
            else if (jQuery(this).attr('class') == 'SelectedTeacherItem') {
                selTeachers = selTeachers + '<li id="' + jQuery(this).attr('id') + '" class="SelectedTeacherItem">' + jQuery(this).html() + '</li>';
            }
        });
        $('#SelectedStudentList').html('');
        $(selStuds).appendTo('#SelectedStudentList');
        $(selTeachers).appendTo('#SelectedStudentList');
        jQuery("#CheckAllDiv").show();
        jQuery("#UnCheckAllDiv").hide();
        checkedcount = 0;
        $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").removeAttr('checked');
        $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").removeAttr('checked');
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').removeAttr('checked');
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').removeAttr('checked');
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().show();
        $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().show();
        $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().show();
        $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').next().next().hide();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().show();
        $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').next().next().hide();
        for (var i = 0; i < $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]');
            classids.parent().parent()[i].className = "RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");

        }
        for (var i = 0; i < $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]');
            classids.parent().parent()[i].className = "RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
        }

        for (var i = 0; i < $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]');
            classids.parent().parent()[i].className = "RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
        }
        for (var i = 0; i < $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]').parent().parent().length; i++) {
            var classids = $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input[type=checkbox]');
            classids.parent().parent()[i].className = "RepeaterContentScndDiv";
            $($(classids).parent().parent()[i].children[3].children[0]).addClass('GpAddStudentRptbtn');
            $($(classids).parent().parent()[i].children[3].children[0]).removeClass('GpAddStudentRptbtnDisable');
            $($(classids).parent().parent()[i].children[3].children[0]).val("ADD");
        }
        SetContentHeight();
    }
    function SetContentHeight() {
        jQuery('#eCollectionContent').height((jQuery('.GroupsDivtest').height() + 25) + 'px');
        jQuery('#eCollectionMenu').height((jQuery('#eCollectionContent').height() - 14) + 'px');
    }

    var contentHeight = $('#eCollectionContent').height();

    function ChangeStyle() {
        //Added By kalai
        var othergrp = true;
        if ($("#ClassDivHdr")[0].style.display == "none" && $("#GroupsDivHdr")[0].style.display == "none" && $("#OtherGroupsDivHdr")[0].style.display == "none") {
          //  $("#GroupLeftBorderdiv")[0].style.display = "none";
          //  $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";      
        }
        else {
            //$("#GroupLeftBorderdiv")[0].style.display = "block";
           // $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "55px";
           // $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "55px";
           // $("#GroupsDivHdr")[0].style.marginTop = "-55px";
           // $("#OtherGroupsDivHdr")[0].style.marginTop = "-51px";
            othergrp = false;
        }

        if ($("#GroupsDivHdr")[0].style.display == "none" && $("#OtherGroupsDivHdr")[0].style.display == "none") {
           // $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "0px";
          //  $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
        }
        if ($("#ClassDivHdr")[0].style.display == "none" && $("#GroupsDivHdr")[0].style.display == "none") {
            othergrp = true;
        }
        if ($("#OtherGroupsDivHdr")[0].style.display == "none") {
           // $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
            //$("#AllOtherGroupBtnDivID")[0].style.display = "none";
            //$("#allotherLeftLineDiv")[0].style.display = "none";
        }
        else {

            if (othergrp) {
                $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                $("#OtherGroupsDivHdr")[0].style.marginTop = "-80px";
            }
        }
        if ($("#OtherGroupsDivHdr")[0].style.display == "none" && $("#ClassDivHdr")[0].style.display == "none") {
            $("#GroupsDivHdr")[0].style.marginTop = "-80px";
        }

        //Added by kalai


        if ($("#ClassDivHdr")[0].style.display != "none" && $("#GroupsDivHdr")[0].style.display != "none" && $("#OtherGroupsDivHdr")[0].style.display != "none" && $("#OtherClassDivHdr")[0].style.display != "none") {
            $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
            $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "24px";
        }
        else if (($("#ClassDivHdr")[0].style.display != "none" || $("#GroupsDivHdr")[0].style.display != "none" || $("#OtherGroupsDivHdr")[0].style.display != "none") && $("#OtherClassDivHdr")[0].style.display != "none") {
            if ($("#ClassDivHdr")[0].style.display == "none") {
                if ($("#GroupsDivHdr")[0].style.display == "none") {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-44px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }
                else {
                    $("#GroupsDivHdr")[0].style.marginTop = "-44px";
                    $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "24px";
                    $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-51px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }

            }
            else {
                if ($("#GroupsDivHdr")[0].style.display == "none") {
                    if ($("#OtherGroupsDivHdr")[0].style.display == "none") {
                        $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                        $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                    }
                    else {
                        $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                        $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "20px";
                    }

                }
                else {
                    $("#OtherClassDivHdr")[0].style.marginTop = "-20px";
                    $("#GroupsDivHdr")[0].style.marginTop = "-20px";
                    $("#GrpRepeaterContentdiv")[0].style.paddingBottom = "20px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "24px";
                }

            }
        }
        else if ($("#OtherClassDivHdr")[0].style.display != "none") {
            $("#OtherClassDivHdr")[0].style.marginTop = "-34px";
            $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "51px";
        }
        if ($("#OtherClassDivHdr")[0].style.display == "none") {
            if ($("#OtherGroupsDivHdr")[0].style.display != "none") {
                if ($("#GroupsDivHdr")[0].style.display != "none") {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-54px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                }
                else {
                    $("#OtherGroupsDivHdr")[0].style.marginTop = "-72px";
                    $("#ClsRepeaterContentDiv")[0].style.paddingBottom = "40px";
                    $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                }
            }
            else {
                if ($("#GroupsDivHdr")[0].style.display != "none") {
                    if ($("#ClassDivHdr")[0].style.display != "none") {
                        $("#GroupsDivHdr")[0].style.marginTop = "-51px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";

                    }
                    else {
                        $("#GroupsDivHdr")[0].style.marginTop = "-72px";
                        $("#OthrGrpRepeaterContentdiv")[0].style.paddingBottom = "0px";
                    }
                }
            }
        }
        $('#eCollectionMenu').height($('#eCollectionContent').height() - 12);
    }

    function setHeight() {
        $('#eCollectionMenu').height($('#eCollectionContent').height() - 12);
    }
    $(document).ready(function () {
        PostBack();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(PostBack);
        // This will call the function after postback..
    });
    function PostBack() {
        if ($('#RoleChkHdn').val() == 'true') {
            jQuery("#Backtocreategroupbtn").parent().css('margin-top', '-245px');
        }

        totalCheckbx = $("#ClassRepeaterDiv #ClassRepeaterContentDiv input[type=checkbox]").length + $("#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type=checkbox]").length + $("#OtherGrpRepeaterDiv #OtherGrpRepeaterContentDiv input[type=checkbox]").length;
        OnPageLoad();
        $('#FinishAddtoGrpBtn').removeClass('srtbtnshide');
        $('#eCollection_Menu_MidHolder').addClass('srtbtnshide');

        var checkallFlag = true;
        var popupFlag = false;
        if ($('#GroupsRepeaterDiv #GroupRepeaterContentDiv input[type="checkbox"]').length > 0) {
            jQuery('#borderleftdiv').addClass('borderleftdiv');
        }

        $("#KenduoComboBoxDiv").click(function () {
            if ($("#RepeaterClassDiv").is(':visible')) {
                $("#ClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterClassDiv").fadeToggle("slow", "linear");                                
            }
            else {
                $("#ClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterClassDiv").fadeToggle("slow", "linear");                                
            }
            setHeight();
        });
        $("#kenduoComboGrpDiv").click(function () {
            if ($("#RepeaterGroupDiv").is(':visible')) {
                $("#GroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterGroupDiv").fadeToggle("slow", "linear");                
                $("#grpBorders").css("display", "none");
                jQuery('#borderleftdiv').removeClass('borderleftdiv');
            }
            else {
                $("#GroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterGroupDiv").fadeToggle("slow", "linear");
                $("#RepeaterGroupDiv").css("display", "block");
                $("#grpBorders").css("display", "block");
                jQuery('#borderleftdiv').addClass('borderleftdiv');
            }
            setHeight();
            //$("#RepeaterClassDiv").slideToggle();
        });
        function stopOtherGrp() {
            $("#RepeaterOtherGroupDiv").css("display", "none");
        }
        $("#OtherGroupsDiv").click(function () {
            if ($("#RepeaterOtherGroupDiv").is(':visible')) {
                $("#OtherGroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterOtherGroupDiv").fadeToggle("slow", "linear");
                setTimeout("stopOtherGrp()", 700);
            }
            else {
                $("#OtherGroupArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterOtherGroupDiv").fadeToggle("slow", "linear");
                $("#RepeaterOtherGroupDiv").css("display", "block");
            }
            setHeight();
            //$("#RepeaterClassDiv").slideToggle();
        });
        $("#OtherClassesDiv").click(function () {
            if ($("#RepeaterOtherClassDiv").is(':visible')) {
                $("#OtherClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_Up.png");
                $("#RepeaterOtherClassDiv").fadeToggle("slow", "linear");
                setTimeout("stopOtherGrp()", 700);
            }
            else {
                $("#OtherClassArrowimg")[0].src = GetFile("/Portals/0/images/Arrow_down.png");
                $("#RepeaterOtherClassDiv").fadeToggle("slow", "linear");
                $("#RepeaterOtherClassDiv").css("display", "block");
            }
            setHeight();
            //$("#RepeaterClassDiv").slideToggle();
        });
        var checkallFlag = true;
        var popupFlag = false;

        if ($.browser.msie && parseInt($.browser.version, 10) < 10) {
            $('#SearchTextBox').val('Enter your search here ...');
            $("#SearchTextBox").focus(function () {
                if ($(this).val() == 'Enter your search here ...') {
                    $(this).val("");
                }
                
            });
            $("#SearchTextBox").blur(function () {
                if ($(this).val().trim() == "") {
                    $('#SearchTextBox').val('Enter your search here ...');
                }
                
            });
        }

        $('#SearchTextBox').keypress(function (e) {
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, ''); && $(this).val() != this.title
                //return false;
            }

            var code = (e.keyCode ? e.keyCode : e.which);
            
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
           
        });

        $('#SearchTextBox').keyup(function (e) {           
            if (this.value.match(/[^a-zA-Z0-9 ]/g)) {
                //this.value = this.value.replace(/[^a-zA-Z0-9 ]/g, '');
                //return false;
            }
            var code = (e.keyCode ? e.keyCode : e.which);
                if (code == 13) {
                    e.preventDefault();
                    $('#SearchButton').click();
                }
            
        });

        
        
        $("#SearchTextBox").kendoAutoComplete({
            dataSource: {
                transport: {
                    read: {
                        url: GetFile('/DesktopModules/eCollection_Sessions/SessionHandler.ashx?SessionStatus=groupsAutoComplete'),
                        type: "GET",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    }
                }
            },
            filter: "contains",
            separator: ", ",
            minLength: 1,
            placeholder: "Enter your search here ..."
        });

        LoadClick();

        jQuery('#AddTeacherToGroup').click(function () {
            var selGroups = '';
            var selStuds = '';
            $("#MsgDiv").removeAttr('class');
            $('#ClassRepeaterDiv #ClassRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val() + ',' + jQuery(this).next().text();
            });
            $('#GroupsRepeaterDiv #GroupRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val() + ',' + jQuery(this).next().text();
            });
            $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val() + ',' + jQuery(this).next().text();
            });
            $('#OtherClsRepeaterDiv #OtherClsRepeaterContentDiv input:checked').each(function () {
                selGroups = selGroups + ',' + jQuery(this).val() + ',' + jQuery(this).next().text();
            });
            jQuery('#SelectedGrpIds').val(selGroups);
            $('#SelectedTeacherList li').each(function () {
                selStuds = selStuds + ',' + jQuery(this).children().html();
            });
            jQuery('#SelectedStudIds').val(selStuds);
            $("#MsgDiv").hide();
            if (selGroups == '') {
                $("#MsgDiv").addClass("dnnFormMessage dnnFormWarning dnnMsgScript");
                // $("#MsgDiv").text('Please select atleast one group or class.').show();
                GetMessage('GROUPS_SELECTED');
                return false;
            }
            return true;
        });

        jQuery("#dialog-message").dialog({
            Modal: true, autoOpen: false,
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    function LoadClick() {
        $("#SearchButton").click(function () {
            $('#searchhdn').val('search');
            $('#lastsearch').val($('#newsearch').val());
        });
    }
    function ShowUpdate() {
        $("#UpdateProgressImg").css("display", "block");
    }
    function EndUpdateProgress() {
        $("#UpdateProgressImg").css("display", "none");
    }
    function ShowHideGrpDivs() {
        if ($('#<%=SecondDiv.ClientID %>').is(':visible')) {
            $('#SelectAllDiv').css('display', 'block');
            $('#SortDiv').css('display', 'block');
        }
        else {
            $('#SelectAllDiv').css('display', 'none');
            $('#SortDiv').css('display', 'none');
        }
    }